using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private string currentNumber = "";
        private string previousOperation = "";
        private double result = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string content = button.Content.ToString();

            if (currentNumber == "0" && content != ".")
            {
                currentNumber = content;
            }
            else
            {
                currentNumber += content;
            }

            currentNumberTextBox.Text = currentNumber;
        }

        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
            if (!currentNumber.Contains("."))
            {
                currentNumber += ".";
                currentNumberTextBox.Text = currentNumber;
            }
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string operation = button.Content.ToString();

            if (!string.IsNullOrEmpty(currentNumber))
            {
                if (!string.IsNullOrEmpty(previousOperation))
                {
                    Calculate();
                    previousOperationTextBox.Text = result.ToString();
                }
                else
                {
                    result = double.Parse(currentNumber);
                    previousOperationTextBox.Text = result.ToString();
                }

                previousOperation = operation;
                currentNumber = "";
                currentNumberTextBox.Text = "";
            }
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(currentNumber) && !string.IsNullOrEmpty(previousOperation))
            {
                Calculate();
                previousOperationTextBox.Text = "";
                previousOperation = "";
                currentNumberTextBox.Text = result.ToString();
            }
        }

        private void Calculate()
        {
            double current = double.Parse(currentNumber);

            switch (previousOperation)
            {
                case "+":
                    result += current;
                    break;
                case "-":
                    result -= current;
                    break;
                case "*":
                    result *= current;
                    break;
                case "/":
                    if (current != 0)
                    {
                        result /= current;
                    }
                    else
                    {
                        MessageBox.Show("Cannot divide by zero");
                    }
                    break;
            }
        }

        private void ClearEntryButton_Click(object sender, RoutedEventArgs e)
        {
            currentNumber = "";
            currentNumberTextBox.Text = "";
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            currentNumber = "";
            currentNumberTextBox.Text = "";
            previousOperation = "";
            previousOperationTextBox.Text = "";
            result = 0;
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(currentNumber))
            {
                currentNumber = currentNumber.Substring(0, currentNumber.Length - 1);
                currentNumberTextBox.Text = currentNumber;
            }
        }
    }
}
