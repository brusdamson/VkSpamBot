using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VkNet;
using VkNet.AudioBypassService.Extensions;
using VkNet.Model;

namespace WPFSpamBot
{
    class Authorize
    {
        public static VkApi _api;
        public static bool IsException { get; set; }
        
        public static VkApi Auth(string _Login, string _Password) //Метод который будет держать сессию открытой, он вернет волшебную штучку, которая будет содержать залогиненую информацию (сессию уже с введенным логином и паролем)
        {
            var services = new ServiceCollection(); 
            services.AddAudioBypass(); 

            _api = new VkApi(services); //Создаем объект класса VkApi и в конструктор передаем наш обход чтобы все методы работали
            try
            {
                IsException = true;
                _api.Authorize(new ApiAuthParams() //Метод для авторизации
                {
                    Login = _Login,
                    Password = _Password,

                });
            }
            catch (Exception ex)
            {
                IsException = false;
                MessageBox.Show("Ошибка авторизации! Проверьте корректность данных!\n Напоминаем! Аккаунт должен быть без двухфакторной авторизации");

            }
            if (_api.IsAuthorized) //Проверка на супешную авторизацию
            {
                MessageBox.Show("Успех!");
            }
            return _api;//Возвращает открытую сессию
        }
    }
}
