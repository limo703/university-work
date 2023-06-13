using System.Windows.Forms;
using System.IO;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Calculator calc = new Calculator();
        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = "0";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            calc.Remember(richTextBox1.Text);
            calc.opp = "+";
            richTextBox1.Text = null;
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            calc.Remember(richTextBox1.Text);
            calc.opp = "-";
            richTextBox1.Text = null;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            calc.Remember(richTextBox1.Text);
            calc.opp = "*";
            richTextBox1.Text = null;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            calc.Remember(richTextBox1.Text);
            calc.opp = "/";
            richTextBox1.Text = null;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            calc.opp = "sin";
            richTextBox1.Text = calc.Trygonometry(richTextBox1.Text).ToString();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            calc.opp = "cos";
            richTextBox1.Text = calc.Trygonometry(richTextBox1.Text).ToString();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            calc.opp = "tg";
            richTextBox1.Text = calc.Trygonometry(richTextBox1.Text).ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            calc.opp = "ctg";
            richTextBox1.Text = calc.Trygonometry(richTextBox1.Text).ToString();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = calc.Res(richTextBox1.Text);
        }
        private void button11_Click(object sender, EventArgs e)
        {
            File.WriteAllText("D:\\proga.txt", String.Empty);
        }
        private void button10_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            calc.opp = "";
            calc.Remember("0");

        }

        private void button12_Click(object sender, EventArgs e)
        {
            calc.ForReadClose();
            StreamReader sr = new StreamReader("D:\\proga.txt");
            sr.BaseStream.Position = 0;
            string line = "";
            richTextBox1.Text = null;
            while((line = sr.ReadLine()) != null)
            {
                richTextBox1.AppendText(line + "\n");
            }
            sr.Close();
            calc.ForReadOpen();
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            calc.opp = "arcsin";
            richTextBox1.Text = calc.Trygonometry(richTextBox1.Text).ToString();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            calc.opp = "arccos";
            richTextBox1.Text = calc.Trygonometry(richTextBox1.Text).ToString();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            calc.opp = "arctg";
            richTextBox1.Text = calc.Trygonometry(richTextBox1.Text).ToString();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            calc.opp = "arcctg";
            richTextBox1.Text = calc.Trygonometry(richTextBox1.Text).ToString();
        }
    }
    public class Calculator
    {
        StreamWriter sw = new StreamWriter("D:\\proga.txt");//ссылка на txt-файл
        public void ForReadClose()
        {
            sw.Close();
        }
        public void ForReadOpen()
        {
            sw = new StreamWriter("D:\\proga.txt");
        }
        private double remember = 0;
        public string opp { get; set; }
        public void Remember(string a)
        {
            remember = Convert(a);
        }
        public string Res(string str)
        {
            double temp = Schet(str);
            return (temp%60/100+ (int)(temp/60)).ToString();
        }
        public double Trygonometry(string a)
        {
            switch (opp)
            {
                case "sin":
                    sw.Write($"sin{a} = {Math.Sin(double.Parse(a) * Math.PI / 180)};\n");
                    return Math.Sin(double.Parse(a) * Math.PI / 180);
                case "cos":
                    sw.Write($"cos{a} = {Math.Cos(double.Parse(a) * Math.PI / 180)};\n");
                    return Math.Cos(double.Parse(a) * Math.PI / 180);
                case "tg":
                    sw.Write($"tg{a} = {Math.Tan(double.Parse(a) * Math.PI / 180)};\n");
                    return Math.Tan(double.Parse(a) * Math.PI / 180);
                case "ctg":
                    sw.Write($"ctg{a} = {1/Math.Tan(double.Parse(a) * Math.PI / 180)};\n");
                    return 1 / Math.Tan(double.Parse(a) * Math.PI / 180);
                case "arcsin":
                    sw.Write($"arcsin{a} = {Math.Asin(double.Parse(a)) * 180 / Math.PI};\n");
                    return  Math.Asin(double.Parse(a))*180/Math.PI;
                case "arccos":
                    sw.Write($"arccos{a} = {Math.Acos(double.Parse(a)) * 180 / Math.PI};\n");
                    return Math.Acos(double.Parse(a) ) * 180 / Math.PI;
                case "arctg":
                    sw.Write($"arctg{a} = {Math.Atan(double.Parse(a)) * 180 / Math.PI};\n");
                    return Math.Atan(double.Parse(a) ) * 180 / Math.PI;
                case "arcctg":
                    sw.Write($"arcctg{a} = {90 -Math.Atan(double.Parse(a)) * 180 / Math.PI};\n");
                    return 90 - Math.Atan(double.Parse(a)) * 180 / Math.PI;
            }
            return 0;
        }
        private double Schet(string a)
        {
            double temp = Convert(a);
            double b = 0;
            switch (opp)
            {
                case "+":
                    sw.Write($"{remember % 60 / 100+(int)remember/60} + {temp % 60 / 100 + (int)(temp / 60)} = {(temp + remember) % 60 / 100 + (int)((temp + remember) / 60)};\n");
                    b = temp + remember;
                    break;
                case "-":
                    sw.Write($"{remember % 60 / 100 + (int)remember / 60} - {temp % 60 / 100 + (int)(temp / 60)} = {(remember - temp) % 60 / 100 + (int)((remember - temp  ) / 60)};\n");
                    b = remember -temp;
                    break;
                case "*":
                    sw.Write($"{remember % 60 / 100 + (int)remember / 60} * {temp % 60 / 100 + (int)(temp / 60)} = {(temp * remember) % 60 / 100/60 + (int)((temp * remember) / 60/60)};\n");
                    b = temp * remember/60;
                    break;
                case "/":
                    sw.Write($"{remember % 60 / 100 + (int)remember / 60} / {temp % 60 / 100 + (int)(temp / 60)} = {(remember / temp) % 60 / 100/60 + (int)((remember / temp) / 60/60)};\n");
                    b = remember /temp/60;
                    break;
            }
            return b;
        }
        private double Convert(string a)
        {
            if ((double.Parse(a) % 1) * 100 <= 60)
            {
                double temp = double.Parse(a);
                temp = ((int)temp) * 60 + (temp % 1) * 100;
                return temp;
            }
            else
            {
                MessageBox.Show("Ошибка");
                sw.Write("\nerror\n");
                return 0;
            }
        }
    }
}