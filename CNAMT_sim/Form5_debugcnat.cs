using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNAMT_sim
{
    public partial class Form5_debugcnat : Form
    {
        public Form5_debugcnat()
        {
            InitializeComponent();

            comboBox1.Items.Add("1"); //大題題號
            comboBox1.Items.Add("2");
            comboBox1.Items.Add("3");
            comboBox1.Items.Add("4");
            comboBox1.Items.Add("5");
            comboBox1.Items.Add("6");
            comboBox1.Items.Add("7");
            comboBox1.Items.Add("8");
            comboBox1.Items.Add("9");
            comboBox1.SelectedIndex = 0;
        }
        private void Form5_debugcnat_Load(object sender, EventArgs e)
        {

        }

        int cnatfront; //大題題號
        int cnatsmall; //大題題號



        // 直接進大題
        private void Button1_Click(object sender, EventArgs e)
        {
            //this.Visible = false;
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnat1();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnat2();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnat3();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnat4();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnat5();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnat6();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnat7();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnat8();
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnat9();
        }

        private void Button10_Click(object sender, EventArgs e)      //選大題
        {

            cnatfront = 1;
            cnatfront = int.Parse(comboBox1.SelectedItem.ToString());
            textBox1.AppendText("大題號:  " + comboBox1.SelectedItem + " \r\n");
            comboBox2.Items.Clear();
            fillsmall();
            //更改panel圖案
            if(cnatfront == 6 || cnatfront == 7)
            {
                Form1 form1 = (Form1)this.Owner;
                form1.showpic6();
            }else if (cnatfront == 9)
            {
                Form1 form1 = (Form1)this.Owner;
                form1.showpic9();
            }else
            {
                Form1 form1 = (Form1)this.Owner;
                form1.close69();
            }
        }

        private void fillsmall()     //填入小題題號
        {
            if (cnatfront == 1 || cnatfront == 2 || cnatfront == 3 || cnatfront == 4)
            {
                for (int i = 1; i < 19; i++)
                {
                    comboBox2.Items.Add(Convert.ToString(i));
                }
            }
            else if (cnatfront == 5 || cnatfront == 6 || cnatfront == 8 || cnatfront == 9)
            {
                for (int i = 1; i < 37; i++)
                {
                    comboBox2.Items.Add(Convert.ToString(i));
                }
            }
            else
            {
                for (int i = 1; i < 73; i++)
                {
                    comboBox2.Items.Add(Convert.ToString(i));
                }
            }
        }

        private void Button11_Click(object sender, EventArgs e)    //選小題& run
        {
            cnatsmall = 1;
            cnatsmall = int.Parse(comboBox2.SelectedItem.ToString());
            textBox1.AppendText("大題號:  " + Convert.ToString(cnatfront) + " \r\n");
            textBox1.AppendText("小題號:  " + Convert.ToString(cnatsmall) + " \r\n");
            Form1 form1 = (Form1)this.Owner;
            form1.showsmall(cnatfront, cnatsmall);   //run
        }

        private void Button12_Click(object sender, EventArgs e)    
        {

        }

        private void Button13_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner;
            form1.waitaction3500();
            textBox1.AppendText("waitaction = 3500 \r\n");//debug
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner;
            form1.waitaction1500();
            textBox1.AppendText("waitaction = 1500 \r\n");//debug
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnatp1();
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnatp2();
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnatp3();
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnatp4();
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnatp5();
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnatp6();
        }

        private void Button21_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnatp7();
        }

        private void Button22_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnatp8();
        }

        private void Button27_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnat4_3();
        }

        private void Button25_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.waitaction1234success();
            textBox1.AppendText("1~4 全對 \r\n");//debug
        }

        private void Button26_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.waitaction1234wrong();
            textBox1.AppendText("1~4 全錯 \r\n");//debug
        }

        private void Button23_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.onefournotredoyet();
            textBox1.AppendText("1~4 還沒重做 \r\n");//debug
        }

        private void Button24_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.onefourredone();
            textBox1.AppendText("1~4 已經重做 \r\n");//debug
        }

        private void Button28_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.lighttime3000();
            textBox1.AppendText("亮燈=3秒 \r\n");//debug
        }

        private void Button29_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.lighttime1500();
            textBox1.AppendText("亮燈=1.5秒 \r\n");//debug
        }

        private void Button30_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.addtest9();
            textBox1.AppendText("做實驗9 \r\n");
        }

        private void Button31_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.notest9();
            textBox1.AppendText("不做實驗9 \r\n");
        }

        private void Button32_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnat_continue();
        }
    }
}
