using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BasicCalcEmulator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private decimal num1, num2;
        private char op = '\0';
        private bool hasNum = false;

        private void button_Append_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button == null) return;

            if (!hasNum)
            {
                hasNum = true;
                textBox1.Text = button.Text;
            }
            else
            {
                if (button.Text == "." && textBox1.Text.Contains(".")) return;
                textBox1.AppendText(button.Text);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            op = '\0';
            //result1 = 0;
            num2 = 0;
            hasNum = false;
            textBox1.Text = "0";
        }

        private void showResult()
        {
            switch (op)
            {
                case '+':
                    num1 += num2;
                    break;
                case '-':
                    num1 -= num2;
                    break;
                case '*':
                    num1 *= num2;
                    break;
                case '/':
                    num1 /= num2;
                    break;
                default:
                    num1 = num2;
                    break;
            }
            textBox1.Text = num1.ToString();
            hasNum = false;
        }

        private decimal getNum()
        {
            decimal result;
            if (!decimal.TryParse(textBox1.Text, out result))
                return 0;
            return result;
        }

        private void buttonEquals_Click(object sender, EventArgs e)
        {
            if (hasNum) num2 = getNum();
            showResult();
        }

        private void button_Operator_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button == null) return;

            if (hasNum)
            {
                num2 = getNum();
                showResult();
            }
            op = button.Text[0];
        }
    }
}
