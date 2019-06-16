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
    public partial class Form4_pop : Form
    {
        public Form4_pop()
        {
            InitializeComponent();
        }

        string a1; //回傳1
        string a2; //回傳2

        private void Label1_Click(object sender, EventArgs e)
        {

        }


        //(上面的字  按鈕1的字  回傳1  按鈕2的字 回傳2 )
        public void readfrom1(string text, string text2, string text3, string text4, string text5) //實作一個公開方法，使其他Form可以傳遞資料進來   https://yuchungchuang.wordpress.com/2018/06/09/winform-%E5%A4%9A%E9%87%8D%E8%A6%96%E7%AA%97%E7%9A%84%E6%93%8D%E4%BD%9C-multiple-forms/
        {
            label1.Text = text;
            button1.Text = text2;
            a1 = text3;
            button2.Text = text4;
            a2 = text5;

            if(text4 == "")
            { button2.Visible = false; }
            else
            { button2.Visible = true; }

        }

        bool IsToForm1 = true; //紀錄是否要回到Form1


        private void Button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            IsToForm1 = true;
           Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
           form1.readfrompop(a1); //將pop中的資料透過公開方法傳遞給Form1
           form1.temp1_check();
            //this.Visible = false;
            this.Close(); //強制關閉Form4
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            button2.Visible = false;
            button1.Visible = false;
            IsToForm1 = true;
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.readfrompop2(a2); //將Form2中textBox的資料透過公開方法傳遞給Form1
            form1.temp1_check();
            //this.Visible = false;
            this.Close(); //強制關閉Form4
        }

        protected override void OnClosing(CancelEventArgs e) //在視窗關閉時觸發
        {
            base.OnClosing(e);
            if ( IsToForm1) //判斷是否要回到Form1
            {
                this.DialogResult = DialogResult.Yes; //利用DialogResult傳遞訊息
            
            }
            else
            {
                this.Close(); //強制關閉Form4
            }
        }


    }
}
