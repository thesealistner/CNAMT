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
        public Form2_panel()
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
            redlightpic[20] = pictureBox95;
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
            /*System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();     //timer參考先放這
            timer1.Interval = 10 ; 
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
            Lightchange();*/
        }

        /* private void timer1_Tick(object sender, EventArgs e)
         {
             this.Refresh();//refresh here...
         }*///timer 用不到  壓掉

        public PictureBox[] redlightpic = new PictureBox[39];
        public PictureBox[] greenlightpic = new PictureBox[43];

        public void Lightchange()//燈號同步用  對映每個燈號和data
        {
            for (int i = 1; i < 39; i++)//redlight有38個
            {
                //textBox1.AppendText(i.ToString());
                if (Form1.redlight[i] == "1")
                {
                    redlightpic[i].Image = CNAMT_sim.Properties.Resources.redlight;//紅燈亮

                }
               else
                {
                    redlightpic[i].Image = CNAMT_sim.Properties.Resources.reddark2;//紅燈暗 
                }     

                this.Refresh();
            }
            for (int j = 1; j < 43; j++)//greenlight有42個
            {
                if (Form1.greenlight[j] == "1")
                {
                    greenlightpic[j].Image = CNAMT_sim.Properties.Resources.greenlight2;//綠燈亮

                }
                else
                {
                    greenlightpic[j].Image = CNAMT_sim.Properties.Resources.greendark2;//紅燈暗 
                }
                this.Refresh();
            }
            
        }
    }
}
