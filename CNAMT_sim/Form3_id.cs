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
        public CNAMT_sim.FormParameter parm2 = new FormParameter();
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
         string a9;
         string a10;

        bool IsToForm1 = true; //紀錄是否要回到Form1

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
            a9 = "id1";
            a10 = textBox9.Text;

            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.readfromid1(a1,a2,a3,a4,a5,a6,a7,a8,a9,a10); //傳遞給Form1
            form1.temp1_check();

            /*DirectoryInfo dir = new DirectoryInfo(System.Windows.Forms.Application.StartupPath);
            string savelocation = dir.Parent.Parent.Parent.FullName;
            savelocation = savelocation + "\\Record\\" + a1 +"_" +a7;  // 編碼+日期
            System.IO.Directory.CreateDirectory(savelocation);
            string q = savelocation + "\\";
            savelocation = savelocation + "\\" + a1+"_"+a7 +".txt";
            StreamWriter sw = new StreamWriter(savelocation);      //建立資料夾   

            sw.WriteLine(a1);  //開始寫入
            sw.WriteLine(a2);  //開始寫入
            sw.WriteLine(a3);  //開始寫入
            sw.WriteLine(a4);  //開始寫入
            sw.WriteLine(a5);  //開始寫入
            sw.WriteLine(a6);  //開始寫入
            sw.WriteLine(a7);  //開始寫入
            sw.WriteLine(a8);  //開始寫入
            sw.Flush();               //清空緩衝區    
            sw.Close();     //關閉流*/
            //parm2.FuncNum = a1;



            this.Close();
        }

        /*protected override void OnClosing(CancelEventArgs e) //在視窗關閉時觸發
        {
            base.OnClosing(e);
            if (IsToForm1) //判斷是否要回到Form1
            {
                this.DialogResult = DialogResult.Yes; //利用DialogResult傳遞訊息

            }
            else
            {
                this.Close(); //強制關閉Form4
            }
        }*/
    }
}
