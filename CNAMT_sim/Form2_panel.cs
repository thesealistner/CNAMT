using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;

namespace CNAMT_sim
{
    public partial class Form2_panel : Form
    {
        public Form2_panel()// 燈號和位置的對應
        {
            InitializeComponent();
            redlightpic[1] = pictureBox3;
            redlightpic[2] = pictureBox8;
            redlightpic[3] = pictureBox10;
            redlightpic[4] = pictureBox12;
            redlightpic[5] = pictureBox14;
            redlightpic[6] = pictureBox16;
            redlightpic[7] = pictureBox18;
            redlightpic[8] = pictureBox20;
            redlightpic[9] = pictureBox22;
            redlightpic[10] = pictureBox82;
            redlightpic[11] = pictureBox24;
            redlightpic[12] = pictureBox26;
            redlightpic[13] = pictureBox38;
            redlightpic[14] = pictureBox36;
            redlightpic[15] = pictureBox34;
            redlightpic[16] = pictureBox32;
            redlightpic[17] = pictureBox30;
            redlightpic[18] = pictureBox28;
            redlightpic[19] = pictureBox6;
            redlightpic[20] = pictureBox96;
            redlightpic[21] = pictureBox94;
            redlightpic[22] = pictureBox92;
            redlightpic[23] = pictureBox90;
            redlightpic[24] = pictureBox88;
            redlightpic[25] = pictureBox84;
            redlightpic[26] = pictureBox86;
            redlightpic[27] = pictureBox40;
            redlightpic[28] = pictureBox4;
            redlightpic[29] = pictureBox56;
            redlightpic[30] = pictureBox54;
            redlightpic[31] = pictureBox52;
            redlightpic[32] = pictureBox50;
            redlightpic[33] = pictureBox48;
            redlightpic[34] = pictureBox46;
            redlightpic[35] = pictureBox44;
            redlightpic[36] = pictureBox42;
            redlightpic[37] = pictureBox79;
            redlightpic[38] = pictureBox80;
            greenlightpic[1] = pictureBox1;
            greenlightpic[2] = pictureBox7;
            greenlightpic[3] = pictureBox9;
            greenlightpic[4] = pictureBox11;
            greenlightpic[5] = pictureBox13;
            greenlightpic[6] = pictureBox15;
            greenlightpic[7] = pictureBox17;
            greenlightpic[8] = pictureBox19;
            greenlightpic[9] = pictureBox21;
            greenlightpic[10] = pictureBox81;
            greenlightpic[11] = pictureBox23;
            greenlightpic[12] = pictureBox25;
            greenlightpic[13] = pictureBox37;
            greenlightpic[14] = pictureBox35;
            greenlightpic[15] = pictureBox33;
            greenlightpic[16] = pictureBox31;
            greenlightpic[17] = pictureBox29;
            greenlightpic[18] = pictureBox27;
            greenlightpic[19] = pictureBox5;
            greenlightpic[20] = pictureBox95;
            greenlightpic[21] = pictureBox93;
            greenlightpic[22] = pictureBox91;
            greenlightpic[23] = pictureBox89;
            greenlightpic[24] = pictureBox87;
            greenlightpic[25] = pictureBox85;
            greenlightpic[26] = pictureBox83;
            greenlightpic[27] = pictureBox39;
            greenlightpic[28] = pictureBox2;
            greenlightpic[29] = pictureBox55;
            greenlightpic[30] = pictureBox53;
            greenlightpic[31] = pictureBox51;
            greenlightpic[32] = pictureBox49;
            greenlightpic[33] = pictureBox47;
            greenlightpic[34] = pictureBox45;
            greenlightpic[35] = pictureBox43;
            greenlightpic[36] = pictureBox41;
            greenlightpic[37] = pictureBox73;
            greenlightpic[38] = pictureBox74;
            greenlightpic[39] = pictureBox75;
            greenlightpic[40] = pictureBox76;
            greenlightpic[41] = pictureBox77;
            greenlightpic[42] = pictureBox78;

        }

        private void Form2_panel_Load(object sender, EventArgs e)
        {

        }

        public PictureBox[] redlightpic = new PictureBox[39];    //lightchange要用
        public PictureBox[] greenlightpic = new PictureBox[43];   //lightchange要用

        public void Lightchange()
        {
            for (int i = 1; i < 39; i++)
            {
                if (Form1.redlight[i] == 0)
                {
                    redlightpic[i].Image = CNAMT_sim.Properties.Resources.reddark2;//紅燈暗
                }
                if (Form1.redlight[i] == 1)
                {
                    redlightpic[i].Image = CNAMT_sim.Properties.Resources.redlight;//紅燈亮
                }
            }

            for (int i = 1; i < 43; i++)//綠燈比較
            {
                if (Form1.greenlight[i] == 1)
                {
                    greenlightpic[i].Image = CNAMT_sim.Properties.Resources.greenlight2;//綠燈亮
                }
                if (Form1.greenlight[i] == 0)
                {
                    greenlightpic[i].Image = CNAMT_sim.Properties.Resources.greendark2;//綠燈暗 
                }
            }
            this.Refresh();
        }

        public void openpicturetest6()
        {
            pictureBox57.Image = CNAMT_sim.Properties.Resources.test6up5;
            pictureBox61.Image = CNAMT_sim.Properties.Resources.test6down1_2;
            pictureBox60.Image = CNAMT_sim.Properties.Resources.test6down2_2;
            pictureBox59.Image = CNAMT_sim.Properties.Resources.test6down3_2;
            pictureBox58.Image = CNAMT_sim.Properties.Resources.test6down4_3;

            pictureBox57.Visible = true;//top
            pictureBox61.Visible = true;
            pictureBox60.Visible = true;
            pictureBox59.Visible = true;
            pictureBox58.Visible = true;
            this.Refresh();
        }

        public void closepicturetest6()
        {
            pictureBox57.Visible = false;//top
            pictureBox61.Visible = false;
            pictureBox60.Visible = false;
            pictureBox59.Visible = false;
            pictureBox58.Visible = false;

            this.Refresh();
        }


        public void openpicturetest9()
        {
            pictureBox57.Image = CNAMT_sim.Properties.Resources.test9up6;
            pictureBox61.Image = CNAMT_sim.Properties.Resources.test9down1_2;
            pictureBox60.Image = CNAMT_sim.Properties.Resources.test9down2_2;
            pictureBox59.Image = CNAMT_sim.Properties.Resources.test9down3_2;
            pictureBox58.Image = CNAMT_sim.Properties.Resources.test9down4_2;

            pictureBox57.Visible = true;//top
            pictureBox61.Visible = true;
            pictureBox60.Visible = true;
            pictureBox59.Visible = true;
            pictureBox58.Visible = true;
            this.Refresh();
        }

        public void closepicturetest9()
        {
            pictureBox57.Visible = false;//top
            pictureBox61.Visible = false;
            pictureBox60.Visible = false;
            pictureBox59.Visible = false;
            pictureBox58.Visible = false;

            this.Refresh();
        }

   

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            cnmtbutton1();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            cnmtbutton2();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            cnmtbutton3();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            cnmtbutton4();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            cnmtbutton5();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            cnmtbutton6();
        }


        //cnmt的按鍵呼叫，改form1的資料
        public void cnmtbutton1()
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmtbutton1press();
        }

        public void cnmtbutton2()
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmtbutton2press();
        }

        public void cnmtbutton3()
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmtbutton3press();
        }

        public void cnmtbutton4()
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmtbutton4press();
        }

        public void cnmtbutton5()
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmtbutton5press();
        }

        public void cnmtbutton6()
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmtbutton6press();
        }

        // CNMT 換圖片------------------ CNMT 換圖片------------------ CNMT 換圖片------------------ CNMT 換圖片------------------ CNMT 換圖片------------------ CNMT 換圖片------------------ CNMT 換圖片------------------ 

        public void showcnmtpic()
        {
            //pictureBox58.Image = CNAMT_sim.Properties.Resources.test6down4_3;
            pictureBox64.Visible = true;
            pictureBox62.Visible = true;
            pictureBox63.Visible = true;
        }

        public void hidecnmtpic()
        {
            pictureBox64.Visible = false;
            pictureBox62.Visible = false;
            pictureBox63.Visible = false;
        }

        public void cnmt0_1()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.cnmt_p_top;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.cnmt_p_a1;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.cnmt_p_a2;
        }

        public void cnmt0_2()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.cnmt_p_top;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.transparent01;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.transparent01;
        }

        public void cnmt0_3()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.transparent01;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.cnmt_p_b1;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.cnmt_p_b2;
        }

        public void cnmt0_4()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.transparent01;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.cnmt_p_c1;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.cnmt_p_c2;
        }

        public void cnmt1_1()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.cnmt_1_top;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.cnmt_1_a1;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.cnmt_1_a2;
        }

        public void cnmt1_2()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.cnmt_1_top;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.transparent01;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.transparent01;
        }

        public void cnmt1_3()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.transparent01;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.cnmt_1_b1;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.cnmt_1_b2;
        }

        public void cnmt1_4()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.transparent01;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.cnmt_1_c1;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.cnmt_1_c2;
        }

        public void cnmt2_1()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.cnmt_2_top;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.cnmt_2_a1;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.cnmt_2_a2;
        }

        public void cnmt2_2()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.cnmt_2_top;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.transparent01;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.transparent01;
        }

        public void cnmt2_3()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.transparent01;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.cnmt_2_b1;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.cnmt_2_b2;
        }

        public void cnmt2_4()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.transparent01;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.cnmt_2_c1;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.cnmt_2_c2;
        }

        public void cnmt3_1()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.cnmt_3_top;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.cnmt_3_a1;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.cnmt_3_a2;
        }

        public void cnmt3_2()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.cnmt_3_top;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.transparent01;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.transparent01;
        }

        public void cnmt3_3()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.transparent01;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.cnmt_3_b1;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.cnmt_3_b2;
        }

        public void cnmt3_4()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.transparent01;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.cnmt_3_c1;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.cnmt_3_c2;
        }

        public void cnmt4_1()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.cnmt_4_top;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.cnmt_4_a1;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.cnmt_4_a2;
        }

        public void cnmt4_2()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.cnmt_4_top;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.transparent01;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.transparent01;
        }

        public void cnmt4_3()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.transparent01;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.cnmt_4_b1;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.cnmt_4_b2;
        }

        public void cnmt4_4()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.transparent01;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.cnmt_4_c1;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.cnmt_4_c2;
        }

        public void cnmt5_1()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.cnmt_5_top;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.cnmt_5_a1;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.cnmt_5_a2;
        }

        public void cnmt5_2()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.cnmt_5_top;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.transparent01;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.transparent01;
        }

        public void cnmt5_3()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.transparent01;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.cnmt_5_b1;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.cnmt_5_b2;
        }

        public void cnmt5_4()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.transparent01;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.cnmt_5_c1;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.cnmt_5_c2;
        }

        public void cnmt6_1()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.cnmt_6_top;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.cnmt_6_a1;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.cnmt_6_a2;
        }

        public void cnmt6_2()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.cnmt_6_top;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.transparent01;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.transparent01;
        }

        public void cnmt6_3()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.transparent01;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.cnmt_6_b1;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.cnmt_6_b2;
        }

        public void cnmt6_4()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.transparent01;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.cnmt_6_c1;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.cnmt_6_c2;
        }
        public void cnmt7_1()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.cnmt_7_top;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.cnmt_7_a1;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.cnmt_7_a2;
        }

        public void cnmt7_2()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.cnmt_7_top;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.transparent01;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.transparent01;
        }

        public void cnmt7_3()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.transparent01;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.cnmt_7_b1;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.cnmt_7_b2;
        }

        public void cnmt7_4()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.transparent01;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.cnmt_7_c1;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.cnmt_7_c2;
        }

        public void cnmt8_1()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.cnmt_8_top;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.cnmt_8_a1;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.cnmt_8_a2;
        }

        public void cnmt8_2()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.cnmt_8_top;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.transparent01;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.transparent01;
        }

        public void cnmt8_3()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.transparent01;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.cnmt_8_b1;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.cnmt_8_b2;
        }

        public void cnmt8_4()
        {
            pictureBox64.Image = CNAMT_sim.Properties.Resources.transparent01;
            pictureBox62.Image = CNAMT_sim.Properties.Resources.cnmt_8_c1;
            pictureBox63.Image = CNAMT_sim.Properties.Resources.cnmt_8_c2;
        }

        public void show1()
        {
            textBox1.AppendText("正確音  \r\n");
        }

        public void show2()
        {
            textBox1.AppendText("錯誤音  \r\n");
        }
    }
}
