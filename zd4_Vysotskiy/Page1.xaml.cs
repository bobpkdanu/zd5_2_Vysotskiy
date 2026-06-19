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
    public partial class Page1 : ContentPage
    {
        bool isAnnuity;
        public Page1()
        {
            InitializeComponent();

            typePicker.SelectedIndex = 0;

            percentSlider.Maximum = 30;
            percentSlider.Minimum = 1;
            percentSlider.Value = 5;

            percentLabel.Text = $"{percentSlider.Value:F0}%";

            percentSlider.ValueChanged += (s, e) =>
            {
                percentLabel.Text = $"{percentSlider.Value:F0}%";
                Calculate();
            };

            sumEntry.TextChanged += (s, e) => Calculate();
            monthEntry.TextChanged += (s, e) => Calculate();
            typePicker.SelectedIndexChanged += (s, e) =>
            {
                isAnnuity = (typePicker.SelectedIndex == 0);
                percentTitleLabel.IsVisible = isAnnuity;
                percentSlider.IsVisible = isAnnuity;
                percentLabel.IsVisible = isAnnuity;
                Calculate();
            };

            isAnnuity = (typePicker.SelectedIndex == 0);
            percentTitleLabel.IsVisible = isAnnuity;
            percentSlider.IsVisible = isAnnuity;
            percentLabel.IsVisible = isAnnuity;

            Calculate();
        }

        private void Calculate() //Вычисление платежа
        {
            try
            {
                if (string.IsNullOrEmpty(sumEntry.Text) ||
                    string.IsNullOrEmpty(monthEntry.Text) ||
                    typePicker.SelectedIndex == -1)
                {
                    return;
                }

                if (!double.TryParse(sumEntry.Text, out double sum) || sum <= 0) return;
                if (!double.TryParse(monthEntry.Text, out double months) || months <= 0) return;

                double percent = percentSlider.Value / 100 / 12;

                if (percent <= 0)
                {
                    payLabel.Text = "Ежемесячный платеж:";
                    totalLabel.Text = "Общая сумма:";
                    overpayLabel.Text = "Переплата:";
                    return;
                }

                if (typePicker.SelectedIndex == 0)
                {
                    double pow = Math.Pow(1 + percent, months);
                    double payment = sum * percent * pow / (pow - 1);
                    double total = payment * months;

                    payLabel.Text = $"Ежемесячный платеж: {payment:F2}";
                    totalLabel.Text = $"Общая сумма: {total:F2}";
                    overpayLabel.Text = $"Переплата: {total - sum:F2}";
                }
                else
                {
                    double principalPerMonth = sum / months;
                    double totalPayment = 0;
                    double remainingDebt = sum;

                    for (int i = 1; i <= months; i++)
                    {
                        double interest = remainingDebt * percent;
                        double monthlyPayment = principalPerMonth + interest;
                        totalPayment += monthlyPayment;
                        remainingDebt -= principalPerMonth;
                    }

                    double firstPayment = sum / months + sum * percent;
                    double lastPayment = sum / months + (sum - (sum / months) * (months - 1)) * percent;

                    payLabel.Text = $"Платеж: от {lastPayment:F2} до {firstPayment:F2}";
                    totalLabel.Text = $"Общая сумма: {totalPayment:F2}";
                    overpayLabel.Text = $"Переплата: {totalPayment - sum:F2}";
                }
            }
            catch
            {
                Console.WriteLine("Ошибочка");
            }
        }

        private void staticButton_Clicked(object sender, EventArgs e)
        {
            double curr = percentSlider.Value;
            double max = percentSlider.Maximum;
            string desc;
            string payType;
            if (typePicker.SelectedIndex == 0)
            {
                payType = "Аннуитетный";
                desc = "Аннуитетный платёж по кредиту — это фиксированная сумма, которую заёмщик платит каждый период\n" +
                    $"Текущая ставка {curr}%\n" +
                    $"Максимальная ставка {max}%";
            }
            else
            {
                payType = "Дифференцированный";
                desc = "Дифференцированный платёж по кредиту — это схема погашения, при которой размер ежемесячного платежа постепенно уменьшается";
            }
            DisplayAlert("Информация о платеже", $"Тип платежа {payType}\nОписание {desc}\n","Ура кредит");

        }
    }
}
    
