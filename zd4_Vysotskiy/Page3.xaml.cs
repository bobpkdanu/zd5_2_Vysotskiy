using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace zd4_Vysotskiy
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page3 :ContentPage
    {
        public Page3()
        {
            InitializeComponent();
        }

        private async void SignIn_Clicked(object sender, System.EventArgs e)
        {
            string login = LoginEntry.Text.Trim();
            string password = PasswordEntry.Text.Trim();
            if (string.IsNullOrEmpty(login))
            {
                ShowError("Введите логин");
                LoginEntry.Focus();
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                ShowError("Введите пароль");
                PasswordEntry.Focus();
                return;
            }
            if (password.Length < 8)
            {
                ShowError("Пароль должен быть минимум 8 символов");

                PasswordEntry.Focus();
                return;
            }
            ErrorLabel.IsVisible = false;

            MainTabPage.Login = login;
            if (signButton.IsPressed)
            {

            }
            await Navigation.PushAsync(new MainTabPage(login));
        }
        void ShowError(string text)
        {
            ErrorLabel.Text = text;
            ErrorLabel.IsVisible = true;
        }

    }
}


