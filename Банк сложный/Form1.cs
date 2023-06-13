namespace Банк_сложный
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        value[] v = new value[3];
        account[] b = new account[2];
        private void Form1_Load(object sender, EventArgs e)
        {
            v[0] = new value("RUB", 1.0);
            v[1] = new value("TMT", 21.88);
            v[2] = new value("USD", 73.0);
            b[0] = new account(0);
            b[1] = new account(0);
            for (int i = 0; i < 3; i++)
            {
                comboBox1.Items.Add(v[i].name);
                comboBox2.Items.Add(v[i].name);
                comboBox3.Items.Add(v[i].name);
                comboBox4.Items.Add(v[i].name);
                comboBox5.Items.Add(v[i].name);
            }
            textBox1.Text = Convert.ToString(b[0].balance);
            textBox4.Text = Convert.ToString(b[1].balance);
            textBox2.Text = "0";
            textBox3.Text = "0";
            textBox5.Text = "0";
            comboBox1.Text = v[0].name;
            comboBox2.Text = v[0].name;
            comboBox3.Text = v[0].name;
            comboBox4.Text = v[0].name;
            comboBox5.Text = v[0].name;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < v.Length; i++)
            {
                if (comboBox1.Text == v[i].name)
                {
                    b[0].balance *= 0.99;
                    textBox1.Text = (b[0].balance / v[i].val).ToString("0.00");
                }
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < v.Length; i++)
            {
                if (comboBox4.Text == v[i].name)
                {
                    b[1].balance *= 0.99;
                    textBox4.Text = (b[1].balance / v[i].val).ToString("0.00");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (double.Parse(textBox2.Text) > 0)
            {
                for (int i = 0; i < v.Length; i++)
                {
                    if (comboBox2.Text == v[i].name)
                    {
                        b[0].balance += v[i].val * double.Parse(textBox2.Text);
                        for (int j = 0; j < v.Length; j++)
                        {
                            if (comboBox1.Text == v[j].name)
                            {
                                textBox1.Text = (b[0].balance / v[j].val).ToString("0.00");
                            }
                        }
                    }
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (double.Parse(textBox2.Text) > 0)
            {
                for (int i = 0; i < v.Length; i++)
                {
                    if (comboBox2.Text == v[i].name & b[0].balance - v[i].val * double.Parse(textBox2.Text) >= 0)
                    {
                        b[0].balance -= v[i].val * double.Parse(textBox2.Text);
                        for (int j = 0; j < v.Length; j++)
                        {
                            if (comboBox1.Text == v[j].name)
                            {
                                textBox1.Text = (b[0].balance / v[j].val).ToString("0.00");
                            }
                        }
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (double.Parse(textBox3.Text) > 0)
            {
                for (int i = 0; i < v.Length; i++)
                {
                    if (comboBox3.Text == v[i].name)
                    {
                        b[1].balance += v[i].val * double.Parse(textBox3.Text);
                        for (int j = 0; j < v.Length; j++)
                        {
                            if (comboBox4.Text == v[j].name)
                            {
                                textBox4.Text = (b[1].balance / v[j].val).ToString("0.00");
                            }
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (double.Parse(textBox3.Text) > 0)
            {
                for (int i = 0; i < v.Length; i++)
                {
                    if (comboBox3.Text == v[i].name & b[1].balance - v[i].val * double.Parse(textBox3.Text) >= 0)
                    {
                        b[1].balance -= v[i].val * double.Parse(textBox3.Text);
                        for (int j = 0; j < v.Length; j++)
                        {
                            if (comboBox4.Text == v[j].name)
                            {
                                textBox4.Text = (b[1].balance / v[j].val).ToString("0.00");
                            }
                        }
                    }
                }
            }
        }
        double kom1;
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text != "")
            {
                double temp1 = 1, temp2 = 1;
                if (comboBox1.Text != comboBox5.Text) temp1 = 0.99;
                if (comboBox5.Text != comboBox4.Text) temp2 = 0.99;
                for (int i = 0; i < v.Length; i++)
                {
                    if (comboBox5.Text == v[i].name & double.Parse(textBox5.Text) <= double.Parse(textBox1.Text))
                    {
                        kom1 = Convert.ToDouble(textBox5.Text) - Convert.ToDouble(textBox5.Text) * temp1 * temp2;
                        textBox8.Text = kom1.ToString("0.00");
                        kom1 *= v[i].val;
                    }
                }
            }
        }
        

        private void button5_Click(object sender, EventArgs e)
        {
            double kok = 0, mom = 0, geg = 0;
            for (int i = 0; i < v.Length; i++)
            {
                if (comboBox1.Text == v[i].name)
                {
                    kok = double.Parse(textBox1.Text) * v[i].val;
                }
                if (comboBox5.Text == v[i].name)
                {
                    mom = double.Parse(textBox5.Text) * v[i].val;
                }
                if (comboBox4.Text == v[i].name)
                {
                    geg = double.Parse(textBox4.Text) * v[i].val;
                }
            }
            double temp = 0;
            if (comboBox1.Text != comboBox5.Text)
            {
                temp = mom * 0.01;
            }
            for (int i = 0; i < v.Length; i++)
            {
                if (comboBox1.Text == v[i].name & kok - mom - temp>=0)
                {
                    textBox1.Text = ((kok - mom - temp) / v[i].val).ToString("0.00");
                    b[0].balance -= temp + mom;
                }
            }
            temp = 0;
            if (comboBox4.Text != comboBox5.Text)
            {
                temp += mom * 0.01;
            }
            for (int i = 0; i < v.Length; i++)
            {
                if (comboBox4.Text == v[i].name & kok - mom - temp >= 0)
                {
                    textBox4.Text = ((geg + mom - temp) / v[i].val).ToString("0.00");
                    b[1].balance += mom - temp;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            double kok = 0, mom = 0, geg = 0;
            for (int i = 0; i < v.Length; i++)
            {
                if (comboBox4.Text == v[i].name)
                {
                    kok = double.Parse(textBox4.Text) * v[i].val;
                }
                if (comboBox5.Text == v[i].name)
                {
                    mom = double.Parse(textBox5.Text) * v[i].val;
                }
                if (comboBox1.Text == v[i].name)
                {
                    geg = double.Parse(textBox1.Text) * v[i].val;
                }
            }
            double temp = 0;
            if (comboBox4.Text != comboBox5.Text)
            {
                temp = mom * 0.01;
            }
            for (int i = 0; i < v.Length; i++)
            {
                if (comboBox4.Text == v[i].name & kok - mom - temp >= 0)
                {
                    textBox4.Text = ((kok - mom - temp) / v[i].val).ToString("0.00");
                    b[1].balance = b[1].balance - temp + mom;
                }
            }
            temp = 0;
            if (comboBox1.Text != comboBox5.Text)
            {
                temp += mom * 0.01;
            }
            for (int i = 0; i < v.Length; i++)
            {
                if (comboBox1.Text == v[i].name & kok - mom - temp >= 0)
                {
                    textBox1.Text = ((geg + mom - temp) / v[i].val).ToString("0.00");
                    b[0].balance = b[0].balance + mom - temp;
                }
            }
        }
    }
    class value
    {
        public string name { get; set; }
        public double val { get; set; }
        public value(string name, double val)
        {
            this.val = val;
            this.name = name;
        }
    }
    class account
    {
        public double balance = 0;
        public account(double balance)
        {
            this.balance = balance;
        }
    }
}
