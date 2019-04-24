using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CNAMT_sim
{
    public partial class Form3_id : Form
    {
        public Form3_id()
        {
            InitializeComponent();
        }

        string a1;//8個欄位用A1-a8 存
        string a2;
        string a3;
        string a4;
        string a5;
        string a6;
        string a7;
        string a8;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form3_id_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            a1 = textBox1.Text;
            a2 = textBox2.Text;
            a3 = textBox3.Text;
            a4 = textBox4.Text;
            a5 = textBox5.Text;
            a6 = textBox6.Text;
            a7 = textBox7.Text;
            a8 = textBox8.Text;
            DirectoryInfo dir = new DirectoryInfo(System.Windows.Forms.Application.StartupPath);
            string s = dir.Parent.Parent.Parent.FullName;
            s = s + "\\Record\\" + a1 + a7;
            System.IO.Directory.CreateDirectory(s);
            string q = s + "\\";
            s = s + "\\" + "PersonData.txt";
            StreamWriter sw = new StreamWriter(s);      //建立資料夾   
            sw.WriteLine(a1);  //開始寫入
            sw.WriteLine(a2);  //開始寫入
            sw.WriteLine(a3);  //開始寫入
            sw.WriteLine(a4);  //開始寫入
            sw.WriteLine(a5);  //開始寫入
            sw.WriteLine(a6);  //開始寫入
            sw.WriteLine(a7);  //開始寫入
            sw.WriteLine(a8);  //開始寫入
            sw.Flush();               //清空緩衝區    
            sw.Close();     //關閉流
            this.Close();
        }
    }
}
