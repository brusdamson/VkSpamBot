   M S B A M L   `   `   `  ����E  AWPFSpamBot, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:WPFSpamBot
WPFSpamBot  5   	   $    WPFSpamBot.WindowAuthorization   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:WPFSpamBot     5   	   b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5          � Title$;  5Пожалуйста, авторизируйтесь!��5	   	   $	��600��6.   $	��800��6;    � 
ResizeMode$ NoResize=�6G    � WindowStartupLocation$ CenterScreen=�6]   .��5
      �  � ColumnDefinitions 5   
   �� 5      ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ 	#FFA0A6DF��6   � 5      $ 	#FF20252C��6   5      .+�5   
   �� -   5         ��Name$ 
AuthBorder��5      $	��400��6%   $��15,95,15,89q�64   $��Center=�6I    �� Padding$ 30q�6d   $	��550��6q   	 �� CornerRadius$	 20��6�   $	��387��6�   ��5      �� 5      $ 0.5,1&�6&   $ 0.5,0&�67   .��5      � $ 	#FFA0A6E2��6#   
 � Offset$
 0.017Q�65   � 5      $ 	#FF6F707C��6#   $
 0.719Q�65   5       J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 11Q�6#      Color$ 	LightGray��63      ShadowDepth$ 1Q�6E   ._�5      �� .+�5      �� $��Авторизация��6   $��30(�6/   $
��Bold"�6=   $��0 0 0 20q�6O   �� 5!      -   5         ��Name$ LoginBox��5!      $��20(�6,   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$  Введите логин:��6:    %  !MaterialDesignFloatingHintTextBox#��[  6j   G� 5"      -   5         G�Name$ PassBox��5"      $��20(�6/   $" Введите пароль:��6=    ) %MaterialDesignFloatingHintPasswordBox#��[ 6n   �� 5#      -   5         ��Name$ 
AuthButton��5#   �   $��Center=�6   $&�� Авторизироваться��66   $��
10,20,10,0q�6Q    " MaterialDesignFlatDarkBgButton#��[ 6e   $	��470��6�   $��37��6�   ���	#FF8174746�   �� 5$      -   5         ��Name$ SaveData��5$      $&�� Сохранить данные ��6-   $��55��6I   5%      5&      5(      5)      