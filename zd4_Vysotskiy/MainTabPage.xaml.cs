using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;
using static Xamarin.Forms.Device;

namespace zd4_Vysotskiy
{
    public partial class MainTabPage : TabbedPage
    {
        public static string Login { get; set; }
        public MainTabPage(string login)
        {
            InitializeComponent();

            Login = login;

            Title = $"Добро пожаловать, {Login}";


            Children.Add(new Page1()
            {
                Title = "Кредиты"
            });


            Children.Add(new Page2()
            {
                Title = "Курсы валют"
            });
        }
    }
}
