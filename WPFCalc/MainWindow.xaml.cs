using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BasicCalcEmulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Append_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null) return;
            append(button.Content.ToString());
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }

        private void buttonCalc_Click(object sender, RoutedEventArgs e)
        {
            calc();
        }

        private void button_Operator_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null) return;
            doOp(button.Content.ToString()[0]);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D0:
                    append("0");
                    break;
                case Key.D1:
                    append("1");
                    break;
                case Key.D2:
                    append("2");
                    break;
                case Key.D3:
                    append("3");
                    break;
                case Key.D4:
                    append("4");
                    break;
                case Key.D5:
                    append("5");
                    break;
                case Key.D6:
                    append("6");
                    break;
                case Key.D7:
                    append("7");
                    break;
                case Key.D8:
                    append("8");
                    break;
                case Key.D9:
                    append("9");
                    break;
                case Key.OemPeriod:
                    append(".");
                    break;
                case Key.Add:
                    doOp('+');
                    break;
                case Key.Subtract:
                case Key.OemMinus:
                    doOp('-');
                    break;
                case Key.Multiply:
                    doOp('*');
                    break;
                case Key.Divide:
                    doOp('/');
                    break;
                case Key.C:
                case Key.Clear:
                case Key.OemClear:
                case Key.Back:
                case Key.Escape:
                    clear();
                    break;
                case Key.OemPlus:
                    if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                    {
                        doOp('+');
                        break;
                    }
                    calc();
                    break;
                case Key.Enter:
                    calc();
                    break;
            }
        }

        private decimal result, curNum;
        private char op = '\0';
        private bool isEntering = false;

        private void calculate()
        {
            switch (op)
            {
                case '+':
                    result += curNum;
                    break;
                case '-':
                    result -= curNum;
                    break;
                case '*':
                    result *= curNum;
                    break;
                case '/':
                    result /= curNum;
                    break;
                default:
                    result = curNum;
                    break;
            }
            ResultBox.Text = result.ToString();
            isEntering = false;
        }

        private void append(string str)
        {
            if (!isEntering)
            {
                isEntering = true;
                ResultBox.Text = "";
            }

            if (str == "." && ResultBox.Text.Contains(".")) return;
            if (str == "0" && ResultBox.Text == "0") return;

            ResultBox.AppendText(str);
        }

        private void clear()
        {
            op = '\0';
            curNum = 0;
            isEntering = false;
            ResultBox.Text = "0";
        }

        private void updateCurNum()
        {
            if (!decimal.TryParse(ResultBox.Text, out curNum))
                curNum = 0;
        }

        private void calc()
        {
            if (isEntering) updateCurNum();
            calculate();
        }

        private void doOp(char newOp)
        {
            if (isEntering)
            {
                updateCurNum();
                calculate();
            }
            op = newOp;
        }
    }
}
