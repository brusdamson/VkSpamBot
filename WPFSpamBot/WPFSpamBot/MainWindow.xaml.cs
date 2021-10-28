using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using VkNet;
using VkNet.Exception;
using VkNet.Model;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;
namespace WPFSpamBot
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int NUMBEROFPOSTS = 2;
        private int idgroup;
        private VkApi VkApi { get; }
        public int Idgroup
        {
            get { return idgroup; }
            set
            {
                if (value < 0)
                {
                    IdGroupBox.Text = 204708399.ToString();
                    idgroup = 204708399;//По дефотлту поставится id на мою тестовую группу
                }
                else
                {
                    idgroup = value;
                }
            }
        }
        private int delay;
        public int Delay
        {
            get { return delay; }
            set
            {
                if (value < 1000)
                {
                    DelaySendMessageBox.Text = 1000.ToString();
                    delay = 1000;
                }
                else
                {
                    delay = value;
                }
            }
        }
        private int count;
        public int Count
        {
            get { return count; }
            set
            {
                if (value < 1)
                {
                    CountSendBox.Text = 1.ToString();
                    count = 1;
                }
                else
                {
                    count = value;
                }
            }
        }
        private List<string> CollectionGroups;
        public MainWindow()
        {
            InitializeComponent();

            WindowAuthorization form2 = new WindowAuthorization();
            form2.ShowDialog();

            if (Authorize.IsException == false)
            {
                Close();
            }
            VkApi = Authorize._api;
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void CommunButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://vk.com/dngdryde");
        }

        private void OpenMenuButton_Click(object sender, RoutedEventArgs e)
        {
            OpenMenuButton.Visibility = Visibility.Collapsed;
            CloseMenuButton.Visibility = Visibility.Visible;
        }

        private void CloseMenuButton_Click(object sender, RoutedEventArgs e)
        {
            OpenMenuButton.Visibility = Visibility.Visible;
            CloseMenuButton.Visibility = Visibility.Collapsed;
        }
        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            TabControl.SelectedIndex = 0;
        }
        private void ListViewItem_Selected_1(object sender, RoutedEventArgs e)
        {
            TabControl.SelectedIndex = 1;
        }
        string StringFromRichTextBox(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(
                rtb.Document.ContentStart,
                rtb.Document.ContentEnd
            );
            return textRange.Text;
        }
        private string GetCutName(string link)
        {
            string cutname;
            if (link.Contains("https://vk.com/public"))
            {
                cutname = link.Replace("https://vk.com/public", "");

            }
            else
            {
                cutname = link.Replace("https://vk.com/", "");
            }
            
            return cutname;
        }
        private int ParseLink(string link, VkApi api)
        {
            string result;
            try
            {
                var groups = api.Groups.GetById(null, GetCutName(link), null).FirstOrDefault();
                result = groups.Id.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Проверьте ссылки!");
                return 0;
            }
            return int.Parse(result);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string message, message2;
            WallGetObject posts;
            if (ListSpamCheckBox.IsChecked == true)
            {
                for (int i = 0; i < comboLinks.Items.Count; i++)
                {
                    try
                    {
                        Idgroup = ParseLink(comboLinks.Items[i].ToString(), VkApi);
                        int tempidgroup = Idgroup;
                        Delay = int.Parse(DelaySendMessageBox.Text);
                        Count = int.Parse(CountSendBox.Text);
                        message = StringFromRichTextBox(MessageTextBox);
                        message2 = StringFromRichTextBox(MessageTextBox2);
                        posts = VkApi.Wall.Get(new WallGetParams()
                        {
                            OwnerId = -Idgroup,
                            Count = NUMBEROFPOSTS
                        });
                        StartSpam(posts, VkApi, Count, Delay, message, message2, tempidgroup);
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Что то не так!");
                    }
                }
            }
            else
            {
                try
                {
                    Idgroup = ParseLink(IdGroupBox.Text, VkApi);
                    int tempidgroup = Idgroup;
                    Delay = int.Parse(DelaySendMessageBox.Text);
                    Count = int.Parse(CountSendBox.Text);

                    message = StringFromRichTextBox(MessageTextBox);
                    message2 = StringFromRichTextBox(MessageTextBox2);

                    posts = VkApi.Wall.Get(new WallGetParams()
                    {
                        OwnerId = -Idgroup,
                        Count = NUMBEROFPOSTS
                    });
                    StartSpam(posts, VkApi, Count, Delay, message, message2, tempidgroup);
                }
                catch (Exception)
                {

                    MessageBox.Show("Что то не так!");
                }
            }
        }
        private async Task<string> UploadFile(string serverUrl, string file, string fileExtension)
        {
            // Получение массива байтов из файла
            var data = GetBytes(file);

            // Создание запроса на загрузку файла на сервер
            using (var client = new HttpClient())
            {
                var requestContent = new MultipartFormDataContent();
                var content = new ByteArrayContent(data);
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                requestContent.Add(content, "file", $"file.{fileExtension}");

                var response = client.PostAsync(serverUrl, requestContent).Result;
                return Encoding.Default.GetString(await response.Content.ReadAsByteArrayAsync());
            }
        }
        private byte[] GetBytes(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }
        private async void StartSpam(WallGetObject posts, VkApi api, int count, int delay, string message, string message2, int tempidgroup)//Сама логика распространения сообщений по комментам под полученными постами с помощью Wall.Get()
        {
            for (int i = 0; i < count; i++)// Тут бот проведет спам по N количеству постам COUNT раз
            {
                await Task.Delay(delay);
                foreach (var item in posts.WallPosts)// Перебираем айтемы в переменной posts для извлечения айдишников постов
                {
                    await Task.Delay(delay);
                    LogBox.AppendText("Отправлен комментарий на пост с id: " + item.Id + "\n");

                    try
                    {
                        var idMyComment = api.Wall.CreateComment(new WallCreateCommentParams()
                        {
                            OwnerId = -tempidgroup, 
                            PostId = (long)item.Id, 
                            Message = message,// Сообщение которым будет спамится. В дальнейшем будет создана коллекция, чтобы можно было составить несколько вариантов комментариев и выбирать их рандомно для отправки 
                        });

                        await Task.Delay(delay);
                        api.Likes.Add(new LikesAddParams()//Добавляем лайк под свой коммент
                        {
                            OwnerId = -tempidgroup,
                            Type = VkNet.Enums.SafetyEnums.LikeObjectType.Comment,
                            ItemId = idMyComment

                        });

                        if (SecondMessageOn.IsChecked == true && message2.Replace(" ","").Replace("\r\n","") != "")
                        {
                            await Task.Delay(delay);
                            api.Wall.CreateComment(new WallCreateCommentParams()
                            {
                                OwnerId = -tempidgroup,
                                PostId = (long)item.Id,
                                Message = message2,
                                ReplyToComment = idMyComment
                            });
                            await Task.Delay(delay+delay);
                        }
                    }// Перехватываем ошибку в случае вылета капчи
                    catch (CaptchaNeededException ex)
                    {
                        Console.WriteLine("ХО-БААА, А Я КАПЧА");
                        Console.WriteLine(ex.Sid.ToString());
                        Console.WriteLine(ex.Img.ToString());
                        MessageBox.Show($"КАПЧА!!! \n{ex.Sid}\n{ex.Img}");
                    }// Чтобы исключение не положило программу, мы его выводим приятным образом, sid капчи и ссылка на капчу, сделал для дальнейшего возможного автоматического обрабатывания капч
                    await Task.Delay(delay);
                }
            }
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Close();
        }
        private void SecondMessageOn_Click(object sender, RoutedEventArgs e)
        {
            if (SecondMessageOn.IsChecked == true)
                MessageTextBox2.IsEnabled = true;
            else
                MessageTextBox2.IsEnabled = false;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CollectionGroups = new List<string>();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string [] stringColl = { };
            if (openFileDialog.ShowDialog() == true)
                stringColl = File.ReadAllLines(openFileDialog.FileName);
            foreach (var item in stringColl)
            {
                CollectionGroups.Add(item.ToString());
            }
            comboLinks.ItemsSource = CollectionGroups.Distinct();

        }
        private void comboLinks_DropDownClosed(object sender, EventArgs e)
        {
            IdGroupBox.Text = comboLinks.Text;
        }

        private void ListSpamCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ButtonLoadGroupCollection.IsEnabled = true;
            comboLinks.IsEnabled = true;
        }

        private void ListSpamCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ButtonLoadGroupCollection.IsEnabled = false;
            comboLinks.IsEnabled = false;
        }
    }
}
