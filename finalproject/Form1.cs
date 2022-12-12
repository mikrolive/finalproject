using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace finalproject
{
    public partial class Form1 : Form
    {
        const int n = 3;
        const int sizeButton = 60;
        public int[,] map = new int[n * n, n * n];
        int m, s;
        public Button[,] buttons = new Button[n * n, n * n];
        public Form1()
        {
            InitializeComponent();
            Generate();
            timer1.Interval = 500;
            m = 0;
            s = 0;
            timer1.Enabled = true;
            label1.Text = "00";
            label2.Visible = true;
            label3.Text = "00";
        }

        public void Generate()
        {
            for(int i = 0; i < n * n; i++)
            {
                for (int j = 0; j < n * n; j++)
                {
                    map[i, j] = (i * n + i / n + j) % (n * n) + 1;
                }
            }
            Transpose();
            Rows();
            Columns();
            BlockRow();
            BlockColumn();
            Random r = new Random();
            for(int i = 0; i <10; i++)
            {
                Shuffle(r.Next(0, 5));
            }
            Create();
            HideCell();
            m = 0;
            s = 0;
            timer1.Enabled = true;
            label1.Text = "00";
            label2.Visible = true;
            label3.Text = "00";
        }
        public void Create()
        {
            for (int i = 0; i < n * n; i++)
            {
                for (int j = 0; j < n * n; j++)
                {
                    Button button = new Button();
                    buttons[i, j] = new Button();
                    buttons[i, j] = button;
                    button.Size = new Size(sizeButton, sizeButton);
                    button.Text = map[i,j].ToString();
                    button.Click += PresstheCell;
                    button.Location = new Point(j * sizeButton,i * sizeButton);
                    this.Controls.Add(button);
                }
            }
        }
        public void Transpose()
        {
            int[,] tMap = new int[n * n, n * n];
            for(int i = 0; i < n *n; i++)
            {
                for(int j = 0; j <n * n; j++)
                {
                    tMap[i, j] = map[j, i];               
                }
            }
        }
        public void Rows()
        {
            Random r = new Random();
            var block = r.Next(0, n);
            var row1 = r.Next(0, n);
            var line1 = block * n + row1;
            var row2 = r.Next(0, n);
            while (row1 == row2)
                row2 = r.Next(0, n);
            var line2 = block * n + row2;
            for(int i = 0; i < n * n; i++)
            {
                var temp = map[line1, i];
                map[line1, i] = map[line2, i];
                map[line2, i] = temp;
            }
        }
        public void Columns()
        {
            Random r = new Random();
            var block = r.Next(0, n);
            var row1 = r.Next(0, n);
            var line1 = block * n + row1;
            var row2 = r.Next(0, n);
            while (row1 == row2)
                row2 = r.Next(0, n);
            var line2 = block * n + row2;
            for (int i = 0; i < n * n; i++)
            {
                var temp = map[i, line1];
                map[i, line1] = map[i, line2];
                map[i, line2] = temp;
            }
        }
        public void BlockRow()
        {
            Random r = new Random();
            var block1 = r.Next(0, n);
            var block2 = r.Next(0, n);
            while (block1 == block2)
                block2 = r.Next(0, n);
            block1 *= n;
            block2 *= n;
            for(int i = 0; i < n * n; i++)
            {
                var a = block2;
                for(int j = block1; j < block1 + n; j++)
                {
                    var temp = map[j, i];
                    map[j, i]= map[a, i];
                    map[a, i]= temp;
                    a++;
                }
            }
        }
        public void BlockColumn()
        {
            Random r = new Random();
            var block1 = r.Next(0, n);
            var block2 = r.Next(0, n);
            while (block1 == block2)
                block2 = r.Next(0, n);
            block1 *= n;
            block2 *= n;
            for (int i = 0; i < n * n; i++)
            {
                var a = block2;
                for (int j = block1; j < block1 + n; j++)
                {
                    var temp = map[i, j];
                    map[i, j] = map[i, a];
                    map[i, a] = temp;
                    a++;
                }
            }
        }
        public void Shuffle(int i)
        {
            switch (i)
            {
                case 0:
                    Transpose();
                    break;
                case 1:
                    Rows();
                    break;
                case 2:
                    Columns();
                    break;
                case 3:  
                    BlockRow();
                    break;
                case 4: 
                    BlockColumn();
                    break;
            }
        }
        public void HideCell()
        {
            int N = 50; // change this to hide more or less cells
            Random r = new Random();
            while (N > 0)
            {
                for (int i = 0; i < n * n; i++)
                {
                    for (int j = 0; j < n * n; j++)
                    {
                        if (!string.IsNullOrEmpty(buttons[i, j].Text))
                        {
                            int f = r.Next(0, 3);
                            buttons[i, j].Text = f == 0 ? "" : buttons[i, j].Text;
                            buttons[i, j].Enabled = f == 0 ? true : false;
                            if (f == 0)
                                N--;
                            if (N <= 0) break;
                        }
                    }
                    if(N <= 0) break;
                }
            }
        }
        public void PresstheCell(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;
            string buttonText = pressedButton.Text;
            if (string.IsNullOrEmpty(buttonText))
            {
                pressedButton.Text = "1";           }
            else
            {
                int num = int.Parse(buttonText);
                num++;
                if (num == 10)
                    num = 1;
                pressedButton.Text = num.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n * n; j++)
                {
                    var textbutton = buttons[i, j].Text;
                    if(textbutton != map[i, j].ToString())
                    {
                        MessageBox.Show("Sudoku is wrong");
                        return;
                    }
                }
            }
            MessageBox.Show("Sudoku is right");
            for (int i = 0; i < n * n; i++)
            {
                for (int j = 0; j < n * n; j++)
                {
                    this.Controls.Remove(buttons[i, j]);
                }
            }
            Generate();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (label2.Visible)
            {
                if (s < 59)
                {
                    s++;
                    if (s < 10)
                    {
                        label3.Text = "0" + s.ToString();
                    }
                    else
                    {
                        label3.Text = s.ToString();
                    }
                }
                else
                {
                    if (m < 59)
                    {
                        m++;
                        if (m < 10)
                            label1.Text = "0" + m.ToString();
                        else
                            label1.Text = m.ToString();
                        s = 0;
                        label3.Text = "00";
                    }
                    else
                    {
                        m = 0;
                        label1.Text = "00";
                    }
                }
                label2.Visible = false;
            }
            else
            {
                label2.Visible = true;
            }
        }
    }
}
