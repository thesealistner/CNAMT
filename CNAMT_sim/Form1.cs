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
using System.Media;
using System.Threading;


namespace CNAMT_sim
{

    public partial class Form1 : Form
    {

        public CNAMT_sim.FormParameter parm1 = new FormParameter();  //跨form拿資料用
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] serialPorts = SerialPort.GetPortNames();
            foreach (string serialPort in serialPorts)
                comboBox1.Items.Add(serialPort);

            timer11.Interval = 1000;

        }//載入serial port 等等

        private void button1_Click(object sender, EventArgs e)//把手連接
        {
            serialPort1 = new SerialPort(comboBox1.SelectedItem.ToString(), 9600, Parity.None, 8, StopBits.Two);
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            try
            {
                serialPort1.Open();
                button1.Enabled = false;
                button2.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Open() error: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)//關閉連接
        {
            serialPort1.Close();
            button2.Enabled = false;
            button1.Enabled = true;
        }

        public delegate void AddDataLelegate();

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            this.Invoke(new AddDataLelegate(AddData));
        }//把手用

        string buttoninput; //右藍色是3  左紅色是2   左紅右藍

        int cnatgo2 = 0;
        public void AddData()//從把手讀資料
        {
            string dataLine = serialPort1.ReadLine();//dataline進來

            /*textBox1.AppendText("dataline=");//顯手把資訊
            textBox1.AppendText(dataLine);
            textBox1.AppendText("\r\n");*/

            buttoninput = dataLine.Substring(0, 1);//右藍色是3  左紅色是2   左紅右藍
            /*textBox1.AppendText("input=");
            textBox1.AppendText(buttoninput);
            textBox1.AppendText("\r\n");*/
            turnofflight();//關燈

            //直接進1秒
            cnatgo2 = 1;
            timer5.Enabled = false;
            timer6.Enabled = false;
            timer7.Enabled = false;
            timer8.Enabled = false;
            cnatgo = 1;


            if (buttontest1 == 1)   //右手測試1       //回丟的數字決定要怎摸處理把手的input
            {
                if (buttoninput == "3")
                {
                    right15_1 = right15_1 + 1;
                    //textBox1.AppendText("AddData \r\n");
                    //textBox1.AppendText(Convert.ToString(right15_1)+"\r\n");
                }
            }
            if (buttontest1 == 2)   //左手測試1
            {
                if (buttoninput == "2")
                {
                    left15_1 = left15_1 + 1;
                    //textBox1.AppendText("AddData \r\n");
                    //textBox1.AppendText(Convert.ToString(right15_1)+"\r\n");
                }
            }
            if (buttontest1 == 3)   //右手測試2
            {
                if (buttoninput == "3")
                {
                    right15_2 = right15_2 + 1;
                    //textBox1.AppendText("AddData \r\n");
                    //textBox1.AppendText(Convert.ToString(right15_1)+"\r\n");
                }
            }
            if (buttontest1 == 4)   //左手測試2
            {
                if (buttoninput == "2")
                {
                    left15_2 = left15_2 + 1;
                    //textBox1.AppendText("AddData \r\n");
                    //textBox1.AppendText(Convert.ToString(right15_1)+"\r\n");
                }
            }

            //條件達到，要記錄
            if (recordpress == 1)//第一題 只收左手
            {
                if (buttoninput == "2")
                {
                    presstimetick = System.Environment.TickCount;
                    recordpress = 0;
                    press = 1;
                    timelength = presstimetick - beeptimetick;
                }
            }
            if (recordpress == 2)//第2題 只收又手
            {
                if (buttoninput == "3")
                {
                    presstimetick = System.Environment.TickCount;
                    recordpress = 0;
                    press = 1;
                    timelength = presstimetick - beeptimetick;
                }
            }
            if (recordpress == 3)//第3題 只收左手
            {
                if (buttoninput == "2")
                {
                    presstimetick = System.Environment.TickCount;
                    recordpress = 0;
                    press = 1;
                    timelength = presstimetick - beeptimetick;
                }
            }
            if (recordpress == 4)//第4題 只收右手
            {
                if (buttoninput == "3")
                {
                    presstimetick = System.Environment.TickCount;
                    recordpress = 0;
                    press = 1;
                    timelength = presstimetick - beeptimetick;
                }
            }
            if (recordpress == 5)//第5題 只收左手
            {
                if (buttoninput == "2")
                {
                    presstimetick = System.Environment.TickCount;
                    recordpress = 0;
                    press = 1;
                    timelength = presstimetick - beeptimetick;
                }
            }
            if (recordpress == 6)//第6題 只收右手
            {
                if (buttoninput == "3")
                {
                    presstimetick = System.Environment.TickCount;
                    recordpress = 0;
                    press = 1;
                    timelength = presstimetick - beeptimetick;
                }
            }
            if (recordpress == 7)//第7題 左右手
            {
                if (buttoninput == "3")
                {
                    presstimetick = System.Environment.TickCount;
                    recordpress = 0;
                    press = 1;
                    timelength = presstimetick - beeptimetick;
                    //0 代表沒按 1右手 2左手
                    lr7 = 1;
                }
                if (buttoninput == "2")
                {
                    presstimetick = System.Environment.TickCount;
                    recordpress = 0;
                    press = 1;
                    timelength = presstimetick - beeptimetick;
                    //0 代表沒按 1右手 2左手
                    lr7 = 2;
                }
            }
            if (recordpress == 8)//第8題 只收左手
            {
                if (buttoninput == "2")
                {
                    presstimetick = System.Environment.TickCount;
                    recordpress = 0;
                    press = 1;
                    timelength = presstimetick - beeptimetick;
                }
            }
            if (recordpress == 9)//第9題 只收左手
            {
                if (buttoninput == "2")
                {
                    presstimetick = System.Environment.TickCount;
                    recordpress = 0;
                    press = 1;
                    timelength = presstimetick - beeptimetick;
                }
            }
        }

        public void AddData2()//從螢幕按鍵讀資料
        {
            //string dataLine = serialPort1.ReadLine();//dataline進來

            /*textBox1.AppendText("dataline=");//顯手把資訊
            textBox1.AppendText(dataLine);
            textBox1.AppendText("\r\n");*/

            //buttoninput = dataLine.Substring(0, 1);//右藍色是3  左紅色是2   左紅右藍
            /*textBox1.AppendText("input=");
            textBox1.AppendText(buttoninput);
            textBox1.AppendText("\r\n");*/
            turnofflight();//關燈

            timer5.Enabled = false;
            timer6.Enabled = false;
            timer7.Enabled = false;
            timer8.Enabled = false;
            cnatgo2 = 1;
            cnatgo = 1;


            if (buttontest1 == 1)   //右手測試1       //回丟的數字決定要怎摸處理把手的input
            {
                if (buttoninput == "3")
                {
                    right15_1 = right15_1 + 1;
                    //textBox1.AppendText("AddData \r\n");
                    //textBox1.AppendText(Convert.ToString(right15_1)+"\r\n");
                }
            }
            if (buttontest1 == 2)   //左手測試1
            {
                if (buttoninput == "2")
                {
                    left15_1 = left15_1 + 1;
                    //textBox1.AppendText("AddData \r\n");
                    //textBox1.AppendText(Convert.ToString(right15_1)+"\r\n");
                }
            }
            if (buttontest1 == 3)   //右手測試2
            {
                if (buttoninput == "3")
                {
                    right15_2 = right15_2 + 1;
                    //textBox1.AppendText("AddData \r\n");
                    //textBox1.AppendText(Convert.ToString(right15_1)+"\r\n");
                }
            }
            if (buttontest1 == 4)   //左手測試2
            {
                if (buttoninput == "2")
                {
                    left15_2 = left15_2 + 1;
                    //textBox1.AppendText("AddData \r\n");
                    //textBox1.AppendText(Convert.ToString(right15_1)+"\r\n");
                }
            }

            //條件達到，要記錄
            if (recordpress == 1)//第一題 只收左手
            {
                if (buttoninput == "2")
                {
                    presstimetick = System.Environment.TickCount;
                    recordpress = 0;
                    press = 1;
                    timelength = presstimetick - beeptimetick;
                }
            }
            if (recordpress == 2)//第2題 只收又手
            {
                if (buttoninput == "3")
                {
                    presstimetick = System.Environment.TickCount;
                    recordpress = 0;
                    press = 1;
                    timelength = presstimetick - beeptimetick;
                }
            }
            if (recordpress == 3)//第3題 只收左手
            {
                if (buttoninput == "2")
                {
                    presstimetick = System.Environment.TickCount;
                    recordpress = 0;
                    press = 1;
                    timelength = presstimetick - beeptimetick;
                }
            }
            if (recordpress == 4)//第4題 只收右手
            {
                if (buttoninput == "3")
                {
                    presstimetick = System.Environment.TickCount;
                    recordpress = 0;
                    press = 1;
                    timelength = presstimetick - beeptimetick;
                }
            }
            if (recordpress == 5)//第5題 只收左手
            {
                if (buttoninput == "2")
                {
                    presstimetick = System.Environment.TickCount;
                    recordpress = 0;
                    press = 1;
                    timelength = presstimetick - beeptimetick;
                }
            }
            if (recordpress == 6)//第6題 只收右手
            {
                if (buttoninput == "3")
                {
                    presstimetick = System.Environment.TickCount;
                    recordpress = 0;
                    press = 1;
                    timelength = presstimetick - beeptimetick;
                }
            }
            if (recordpress == 7)//第7題 左右手
            {
                if (buttoninput == "3")
                {
                    presstimetick = System.Environment.TickCount;
                    recordpress = 0;
                    press = 1;
                    timelength = presstimetick - beeptimetick;
                    //0 代表沒按 1右手 2左手
                    lr7 = 1;
                }
                if (buttoninput == "2")
                {
                    presstimetick = System.Environment.TickCount;
                    recordpress = 0;
                    press = 1;
                    timelength = presstimetick - beeptimetick;
                    //0 代表沒按 1右手 2左手
                    lr7 = 2;
                }
            }
            if (recordpress == 8)//第8題 只收左手
            {
                if (buttoninput == "2")
                {
                    presstimetick = System.Environment.TickCount;
                    recordpress = 0;
                    press = 1;
                    timelength = presstimetick - beeptimetick;
                }
            }
            if (recordpress == 9)//第9題 只收左手
            {
                if (buttoninput == "2")
                {
                    presstimetick = System.Environment.TickCount;
                    recordpress = 0;
                    press = 1;
                    timelength = presstimetick - beeptimetick;
                }
            }
        }

        Form2_panel panel = new Form2_panel();//創建子視窗 

        private void button3_Click(object sender, EventArgs e)// 燈號面板
        {
            panel.Visible = true;//顯示第二個視窗
            panel.Owner = this;
        }

        private void button4_Click(object sender, EventArgs e)//ID 面板
        {
            Form3_id id = new Form3_id(); //創建子視窗
            id.Owner = this;//要有這個不然不能傳
            id.Visible = true;//顯示第二個視窗
            //id.FormClosed += new FormClosedEventHandler(id_Closed);
        }

        /*void id_Closed(object sender, FormClosedEventArgs e)//ID關掉拉回來  ID
        {
            Form3_id sub = (Form3_id)sender;
            parm1 = sub.parm2;
            this.textBox3.Text = parm1.FuncNum;
        }*/

        string temp1 = "0"; //用來放回傳  沒動作==0  //pop1
        string temp2 = "0"; // pop2
        string temp3 = "0"; // id1

        public void readfrompop(string text) //pop回傳第一個
        {
            temp1 = text;
            textBox1.AppendText("readfrompop \r\n");
            textBox1.AppendText(temp1 + "\r\n");
        }

        public void readfrompop2(string text) //pop回傳第二個
        {
            temp2 = text;
            textBox1.AppendText("readfrompop2 \r\n");
            textBox1.AppendText(temp1 + "\r\n");
        }

        string d1, d2, d3, d4, d5, d6, d7, d8; //8個個資放這

        public void readfromid1(string text1, string text2, string text3, string text4, string text5, string text6, string text7, string text8, string temp, string text9) //id 回傳
        {
            //temp1 = text;

            d1 = text1;
            d2 = text2;
            d3 = text3;
            d4 = text4;
            d5 = text5;
            d6 = text6;
            d7 = text7;
            d8 = text8;
            temp3 = temp;
            oftenmodel = text9;

            textBox1.AppendText("readfromid1 \r\n");
            textBox1.AppendText(temp3 + "\r\n");
        }

        
        public void readfromsaveload(int int1, int locationbig, int loactionsmall ) //把資料回填
        {
            resulttime[locationbig][loactionsmall] = int1;
        }
        //-------------------------------------------------------------------------------------------------------------------
        //單一變數都盡量放這
        // start back ground code



        string oftenmodel; //存常模
        double oftenmodel2;
        private void playaudiocnat1() // 聲音播放 CNAT
        {
            SoundPlayer audio = new SoundPlayer(CNAMT_sim.Properties.Resources.beep_07); // here WindowsFormsApplication1 is the namespace and Connect is the audio file name
            audio.Play();
        }


        //練習題所需之delay
        int[][] delaycnatpractice = new int[][]
       {
        new int[] {2700, 1000 , 2000,  3000},//0
        new int[] {2700, 1000 , 2000,  3000},//1
        new int[] {2700, 1000 , 2000,  3000},//2
        new int[] {2700, 1000 , 2000,  3000, 2000,1000},//3
        new int[] {2700, 1000 , 2000,  3000,2000,1000},//4
        new int[] {2700, 3000 , 2000,  3000,2000,3000 },//5
        new int[] {2700, 3000 , 2000,  3000,2000,3000},//6
        new int[] {2700, 1000 , 2000,  3000,2000,1000},//7
        new int[] {2700, 1000 , 2000,  3000,2000,1000},//8
       };


        //練習題燈號
        //
        int[][] lightcnatpractice = new int[][]
        {
        new int[] {},//0 不存在  用來平衡直觀
        new int[] {39 ,46 , 46,  46},//1  第一項不存在  用來平衡直觀
        new int[] {39 , 4 , 4,  4, 4 , 4},//2
        new int[] {39 , 70 , 51,  48, 63 , 55},//3
        new int[] {39 , 29 , 31,  25 , 32 ,5 },//4
        new int[] {39 , 29+41 , 3+80,  5+80, 25+41 , 11+41 },//5
        new int[] {39 , 11 , 30,  33, 23 ,7},//6
        new int[] {39 , 11, 30,  3+80,5+80, 25+41},//7
        new int[] {39 , 89 , 45,  55, 100 ,71},//8
        };



        //delay01[test][當次測驗第幾次燈泡亮] = 本次燈亮所需延遲
        int[][] delaycnat = new int[][]
       {
        new int[0] {},//0
        new int[18] {1600, 2400, 3200, 1000, 3800, 2600, 4400, 1200, 4200, 2800, 3600, 2000, 1800, 3400, 3000, 4000, 2200, 1400},//1
        new int[18] {3400, 1800, 4400, 1200, 2600, 4200, 2200, 1600, 1000, 3600, 2400, 3200, 4000, 1400, 3800, 2000, 3000, 2800},//2
        new int[20] {2700, 1600, 2400, 3200, 1000, 3800, 2600, 4400, 1200, 4200, 2800, 3600, 2000, 1800, 3400, 3000, 4000, 2000, 1400, 2700},//3
        new int[20] {2700, 3400, 1800, 4400, 1200, 2600, 4200, 2200, 1600, 3000, 3600, 2400, 3200, 4000, 1400, 2000, 3800, 1000, 2800, 2700},//4
        new int[38] {2700, 1600, 2400, 3200, 1000, 3800, 2600, 4400, 1200, 4200, 2800, 3600, 2000, 1800, 3400, 3000, 4000, 2200, 1400, 3400, 1800, 4400, 1200, 2600, 4200, 2200, 1600, 3000, 3600, 2400, 3200, 4000, 1400, 2000, 3800, 1000, 2800, 2700},//5
        new int[38] {2700, 3400, 1800, 4400, 1200, 2600, 4200, 2200, 1600, 3000, 3600, 2400, 3200, 4000, 1400, 2000, 3800, 1000, 2800, 1600, 2400, 3200, 1000, 3800, 2600, 4400, 1200, 4200, 2800, 3600, 2000, 1800, 3400, 3000, 4000, 2200, 1400, 2700},//6
        new int[74] {2700, 3400, 1600, 1800, 2400, 4400, 3200, 1200, 1000, 2600, 3800, 4200, 2600, 2200, 4400, 1600, 1200, 3000, 4200, 3600, 2800, 2400, 3600, 3200, 2000, 4000, 1800, 1400, 3400, 2000, 3000, 3800, 4000, 1000, 2200, 2800, 1400, 1600, 3400, 2400, 1800, 3200, 4400, 1000, 1200, 3800, 2600, 2600, 4200, 4400, 2200, 1200, 1600, 4200, 3000, 2800, 3600, 3600, 2400, 2000, 3200, 1800, 4000, 3400, 1400, 3000, 2000, 4000, 3800, 2200, 1000, 1400, 2800, 2700 },//7
        new int[38] {2700, 1600, 2400, 3200, 1000, 3800, 2600, 4400, 1200, 4200, 2800, 3600, 2000, 1800, 3400, 3000, 4000, 2200, 1400, 3400, 1800, 4400, 1200, 2600, 4200, 2200, 1600, 3000, 3600, 2400, 3200, 4000, 1400, 2000, 3800, 1000, 2800, 2700},//8
        new int[38] {2700, 3400, 1800, 4400, 1200, 2600, 4200, 2200, 1600, 3000, 3600, 2400, 3200, 4000, 1400, 2000, 3800, 1000, 2800, 1600, 2400, 3200, 1000, 3800, 2600, 4400, 1200, 4200, 2800, 3600, 2000, 1800, 3400, 3000, 4000, 2200, 1400, 2700},//9

        //new int[38]{2700,1600,2400,3200,1000,3800,2600,4400,1200,4200,2800,3600,2000,1800,3400,3000,4000,2200,1400,3400,1800,4400,1200,2600,4200,2200,1600,3000,3600,2400,3200,4000,1400,2000,3800,1000,2800,2700},//10
        //new int[38]{2700, 1800, 2600, 3400, 1200, 4000, 2800, 4600, 1400, 4400, 3000, 3800, 2200, 2000, 3600, 3200, 4200, 2400, 1600, 3600, 2000, 4600, 1400, 2800, 4400, 2400, 1800, 3200, 3800, 2600, 3400, 4200, 1600, 2200, 4000, 1200, 3000, 2700 },//11
       };


        // 燈號儲存於此   
        //
        //delay01[test][當次測驗第幾次燈泡亮] = 本次燈亮所需延遲
 
        //綠燈42個  紅燈38個  兩個一起 36+2個
        //42+36+38+2=116+2
        //lightcnat[測驗幾號][燈號]=
        //燈號0~41有42個  代表綠燈1~42
        //燈號42~79有38個  代表紅燈1~38
        //燈號80~115有36個  代表紅綠燈1~36號一起亮
        //燈號116~117有2個  代表上面兩個燈號

        int[][] lightcnat = new int[][]
    {
        new int[] {0 , 1 , 2,  3},//0 不存在  用來平衡直觀
        new int[18] {46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46},//1    紅燈燈號+41
        new int[18] {4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4},//2    綠燈燈號-1
        new int[18+2] {79,29+41,3+41,28+41,17+41,9+41,21+41,12+41,31+41,10+41,34+41,24+41,7+41,35+41,5+41,25+41,4+41,14+41,22+41,39},//3
        new int[18+2] {39,20-1,16-1,2-1,11-1,13-1,36-1,19-1,18-1,30-1,32-1,1-1,8-1,6-1,27-1,26-1,33-1,23-1,15-1,39},//4
        new int[36+2] {79,29+41,3+80,28+80,17+80,9+41,21+80,12+41,31+41,10+80,34+41,24+80,7+80,35+41,5+80,25+41,4+80,14+80,22+41,20+41,16+80,2+41,11+41,13+80,36+41,19+80,18+80,30+80,32+41,1+80,8+41,6+41,27+80,26+41,33+41,23+41,15+80,39},//5
        new int[36+2] {39,20-1,16-1,2-1,11-1,13-1,36-1,19-1,18-1,30-1,32-1,1-1,8-1,6-1,27-1,26-1,33-1,23-1,15-1,29-1,3-1,28-1,17-1,9-1,21-1,12-1,31-1,10-1,34-1,24-1,7-1,35-1,5-1,25-1,4-1,14-1,22-1,39},//6
        new int[74]   {39, 19, 70, 15, 83, 1, 108, 10, 97, 12, 50, 35, 101, 18, 53, 17, 72, 29, 90, 31, 75, 0, 104, 7, 87, 5, 76, 26, 85, 25, 66, 32, 84, 22, 94, 14, 63, 28, 61, 2, 96, 27, 43, 16, 52, 8, 93, 20, 77, 11, 99, 30, 98, 9, 110, 33, 73, 23, 81, 6, 49, 34, 47, 4, 107, 24, 67, 3, 74, 13, 64, 21, 95, 39},//7=6[1]+5[1]+6[2]+5[2]....
        new int[36+2] {39 ,29+80,3+41,28+41,17+41,9+80,21+41,12+80,31+80,10+41,34+80,24+41,7+41,35+80,5+41,25+80,4+41,14+41,22+80,20+80,16+41,2+80,11+80,13+41,36+80,19+41,18+41,30+41,32+80,1+41,8+80,6+80,27+41,26+80,33+80,23+80,15+41,39},//8
        new int[36+2] { 39, 20 - 1, 16 - 1, 2 - 1, 11 - 1, 13 - 1, 36 - 1, 19 - 1, 18 - 1, 30 - 1, 32 - 1, 1 - 1, 8 - 1, 6 - 1, 27 - 1, 26 - 1, 33 - 1, 23 - 1, 15 - 1, 29 - 1, 3 - 1, 28 - 1, 17 - 1, 9 - 1, 21 - 1, 12 - 1, 31 - 1, 10 - 1, 34 - 1, 24 - 1, 7 - 1, 35 - 1, 5 - 1, 25 - 1, 4 - 1, 14 - 1, 22 - 1 ,39}//9
    };

        // RESULT HERE  //--------------------------------------------------------------------------------------
       int[][] resulttime = new int[][] //結果存這   單位: MS
        {
            new int[]{ },//0
            new int[20]{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},//1
            new int[20]{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},//2
            new int[20]{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},//3
            new int[20]{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},//4
            new int[38]{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},//5
            new int[38]{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},//6
            new int[74]{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},//7
            new int[38]{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},//8
            new int[38]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0, 0, 0, 0, 0, 0 }//9
        };




        //0 代表沒按 1右手 2左手
        int lr7 = 0;
        int[] leftright = new int[74]        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //左右手的反應   理論上只有七會錯手  只有7要記
        int[] leftrightcorrect = new int[74] { 0 ,1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 0 }; //左右手的反應   理論上只有七會錯手  只有7要記






        //儲存燈號用  應該直接映射燈號
        public static int[] redlight = new int[39] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static int[] greenlight = new int[43] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public int waitaction = 1000; // 1000    3500     代表燈亮完熄滅後反應時間
        public int lighttime = 1500; // 1500     3000     //代表燈亮時長



        /* private void Button9_Click(object sender, EventArgs e)  // 燈號變化example
         {
             redlight[1] = 1;
             redlight[38] = 1;
             greenlight[1] = 1;
             greenlight[42] = 1;
             textBox1.AppendText("redlight[1] = ");
             textBox1.AppendText(Convert.ToString(redlight[1]));

             panel.Lightchange();//呼叫使用燈號

         }*/


        int right15_1 = 0;//按鍵基本能力測式用  紀錄按鍵次數
        int left15_1 = 0;
        int right15_2 = 0;
        int left15_2 = 0;

        //1234 為記錄 左右手用 
        int buttontest1;
        //buttontest1 = 5 紀錄


        //int reacttime3500 = 0; // 0為正常  1為增加



        private void Button10_Click(object sender, EventArgs e)  //CNAT 開始   //CNAT 開始    //CNAT 開始     //CNAT 開始     //CNAT 開始    //CNAT 開始     //CNAT 開始     //CNAT 開始 
        {
            //button10.Enabled = false;//先把自己關掉  
            checkleftright1();//按鍵基本能力測試



        }


        public void checkleftright1()//按鍵基本能力測試     右手1
        {
            // 按鍵基本能力測試   參考手冊第8頁
            textBox2.AppendText("CNAT開始 \r\n");
            textBox2.AppendText("準備按鍵基本能力測試 \r\n");
            textBox2.AppendText("右手第一輪 \r\n");

            //form4的操作
            //右手15秒
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("按下開始後請連續按右手按鍵15秒", "開始", "1", "", "");//temp1=1
            //textBox1.AppendText("before \r\n");
            //textBox1.AppendText(temp1 + "\r\n");//測試用  等於0
        }


        public void checkleftright2()//按鍵基本能力測試     左手1
        {
            // 按鍵基本能力測試   參考手冊第8頁
            textBox2.AppendText("左手第一輪 \r\n");

            //左手15秒
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("按下開始後請連續按左手按鍵15秒", "開始", "2", "", "");//temp1=1 
            //textBox1.AppendText("before \r\n");
            //textBox1.AppendText(temp1 + "\r\n");//測試用  等於0
        }

        public void checkleftright3()//按鍵基本能力測試     右手2
        {
            // 按鍵基本能力測試   參考手冊第8頁
            textBox2.AppendText("右手第二輪 \r\n");

            //form4的操作
            //右手15秒
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("按下開始後請連續按右手按鍵15秒", "開始", "3", "", "");//temp1=1
            //textBox1.AppendText("before \r\n");
            //textBox1.AppendText(temp1 + "\r\n");//測試用  等於0
        }

        public void checkleftright4()//按鍵基本能力測試     左手2
        {
            // 按鍵基本能力測試   參考手冊第8頁
            textBox2.AppendText("左手第二輪 \r\n");

            //左手15秒
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("按下開始後請連續按左手按鍵15秒", "開始", "4", "", "");//temp1=1 
            //textBox1.AppendText("before \r\n");
            //textBox1.AppendText(temp1 + "\r\n");//測試用  等於0
        }






        // ////SAVE-SAVE-SAVE-SAVESAVE---------------------SAVE------------------SAVE-----------------SAVE----------------------SAVE--------------

        private void Button13_Click(object sender, EventArgs e)//讀取資料
        {
            saveload_show();
        }


        public void load1()  //讀取資料
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select file";
            dialog.InitialDirectory = ".\\";
            dialog.Filter = "txt files (*.*)|*.txt";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(dialog.FileName);
            }

        }

        private void saveload_show()//存儲面板
        {
            Form7_saveload saveload = new Form7_saveload(); //創建子視窗
            saveload.Owner = this;//要有這個不然不能傳
            saveload.Visible = true;//顯示第二個視窗
        }

        public void savedatainfo()  //存檔1號  存入基本資料  //只在ID按下確定時會出現
        {
            DirectoryInfo dir = new DirectoryInfo(System.Windows.Forms.Application.StartupPath);
            string savelocation1 = dir.Parent.Parent.Parent.FullName;
            savelocation1 = savelocation1 + "\\Record\\" + d1 + "_" + d7;  // 編碼+日期
            System.IO.Directory.CreateDirectory(savelocation1);//新資料夾  名稱為編碼+日期


            string savelocation2 = savelocation1 + "\\";//存到資料夾下面
            string filename1 = savelocation2 + "\\" + d1 + "_" + d7 +"_" + "info" +".txt";//檔名

            savelabel1:
            if (System.IO.File.Exists(filename1))//如果有重複名字，加上txt做為區別
            {
                filename1 = filename1 + ".txt";
                goto savelabel1;
            }

            StreamWriter sw = new StreamWriter(filename1, true);      //建立txt  //true等於繼續加不會洗掉   

            sw.WriteLine("編碼:     " + d1);  //開始寫入
            sw.WriteLine("性別:     " + d2);  //開始寫入
            sw.WriteLine("年級:     " + d3);  //開始寫入
            sw.WriteLine("班別:     " + d4);  //開始寫入
            sw.WriteLine("座號:     " + d5);  //開始寫入
            sw.WriteLine("生日:     " + d6);  //開始寫入
            sw.WriteLine("施測日期: " + d7);  //開始寫入
            sw.WriteLine("組別:     " + d8);  //開始寫入
            sw.Flush();               //清空緩衝區    
            sw.Close();     //關閉流
        }



        public void savedatacnatbig()  //每一大題存一次
        {
            DirectoryInfo dir = new DirectoryInfo(System.Windows.Forms.Application.StartupPath);
            string savelocation1 = dir.Parent.Parent.Parent.FullName;
            savelocation1 = savelocation1 + "\\Record\\" + d1 + "_" + d7;  // 編碼+日期
            System.IO.Directory.CreateDirectory(savelocation1);//新資料夾  名稱為編碼+日期


            string savelocation2 = savelocation1 + "\\";//存到資料夾下面
            string filename1 = savelocation2 + "\\" + d1 + "_" + d7 + "_" + "cnattest"+ locationbig + ".txt";//檔名

            savelabel1:
            if (System.IO.File.Exists(filename1))//如果有重複名字，加上txt做為區別
            {
                filename1 = filename1 + ".txt";
                goto savelabel1;
            }

            StreamWriter sw = new StreamWriter(filename1, true);      //建立txt  //true等於繼續加不會洗掉   

            //sw.WriteLine( waitaction);  //開始寫入
            //sw.WriteLine(lighttime);  //開始寫入

            if (locationbig == 1)
            {
                for (int i = 0; i < 18; i++)
                {
                    sw.WriteLine("" + resulttime[locationbig][i]  );
                }
            }
            if (locationbig == 2)
            {
                for (int i = 0; i < 18; i++)
                {
                    sw.WriteLine("" + resulttime[locationbig][i]  );
                }
            }
            if (locationbig == 3)
            {
                for (int i = 1; i < 19; i++)
                {
                    sw.WriteLine("" + resulttime[locationbig][i] );
                }
            }
            if (locationbig == 4)
            {
                for (int i = 1; i < 19; i++)
                {
                    sw.WriteLine("" + resulttime[locationbig][i] );
                }
            }
            if (locationbig == 5)
            {
                for (int i = 1; i < 37; i++)
                {
                    sw.WriteLine("" + resulttime[locationbig][i] );
                }
            }
            if (locationbig == 6)
            {
                for (int i = 1; i < 37; i++)
                {
                    sw.WriteLine("" + resulttime[locationbig][i] );
                }
            }
            if (locationbig == 7)
            {
                for (int i = 1; i < 73; i++)
                {
                    if (leftright[i] == 1) //右手
                    {
                        sw.WriteLine("" + resulttime[locationbig][i] + "   R   " );  //開始寫入
                    }
                    if (leftright[i] == 2) //左手
                    {
                        sw.WriteLine("" + resulttime[locationbig][i] + "   L   " );  //開始寫入
                    }
                    if (leftright[i] == 0)
                    {
                        sw.WriteLine("" + resulttime[locationbig][i] );
                    }
                }
            }
            if (locationbig == 8)
            {
                for (int i = 1; i < 37; i++)
                {
                    sw.WriteLine("" + resulttime[locationbig][i] );
                }
            }
            if (locationbig == 9)
            {
                for (int i = 1; i < 37; i++)
                {
                    sw.WriteLine("" + resulttime[locationbig][i] );
                }
            }

            sw.WriteLine(waitaction);  //開始寫入
            sw.WriteLine(lighttime);  //開始寫入

            textBox1.AppendText("存檔" + " \r\n");//debug

            sw.Flush();               //清空緩衝區    
            sw.Close();     //關閉流
        }




        public void savedata2()  //存檔2號  存入受試者反應時間   //開始時會存  //        //改成結束時在存  先不管
        {
            DirectoryInfo dir = new DirectoryInfo(System.Windows.Forms.Application.StartupPath);
            string savelocation = dir.Parent.Parent.Parent.FullName;
            savelocation = savelocation + "\\Record\\" + d1 + "_" + d7;  // 編碼+日期
            System.IO.Directory.CreateDirectory(savelocation);
            string q = savelocation + "\\";
            savelocation = savelocation + "\\" + d1 + "_" + d7 + ".txt";
            StreamWriter sw = new StreamWriter(savelocation, true);      //建立資料夾   

            sw.WriteLine("燈亮完熄滅後等待受試者反應時間(ms): " + waitaction);  //開始寫入
            sw.WriteLine("燈亮時間(ms): " + lighttime);  //開始寫入
            sw.Flush();               //清空緩衝區    
            sw.Close();     //關閉流
        }



        public void savedata3()  //存檔3號  大題結束後存入TXT   //管他3721  初始板本
        {
            DirectoryInfo dir = new DirectoryInfo(System.Windows.Forms.Application.StartupPath);
            string savelocation = dir.Parent.Parent.Parent.FullName;
            savelocation = savelocation + "\\Record\\" + d1 + "_" + d7;  // 編碼+日期
            System.IO.Directory.CreateDirectory(savelocation);
            string q = savelocation + "\\";
            savelocation = savelocation + "\\" + d1 + "_" + d7 + ".txt";
            StreamWriter sw = new StreamWriter(savelocation, true);      //建立資料夾   

            if (locationbig == 1)
            {
                sw.WriteLine("測驗一");  //開始寫入  0~17
                for (int i = 0; i < 18; i++)
                {
                    sw.WriteLine("第" + (i + 1) + "題: " + resulttime[locationbig][i]);  //開始寫入
                }
                sw.WriteLine("測驗一成功數量" + cnatsuccess_1 + " \r\n");
            }
            if (locationbig == 2)
            {
                sw.WriteLine("測驗二");  //開始寫入  0~17
                for (int i = 0; i < 18; i++)
                {
                    sw.WriteLine("第" + (i + 1) + "題: " + resulttime[locationbig][i]);  //開始寫入
                }
                sw.WriteLine("測驗二成功數量" + cnatsuccess_2 + " \r\n");//debug
            }
            if (locationbig == 3)
            {
                sw.WriteLine("測驗三");  //開始寫入  1~18
                for (int i = 1; i < 19; i++)
                {
                    sw.WriteLine("第" + (i) + "題: " + resulttime[locationbig][i]);  //開始寫入
                }
                sw.WriteLine("測驗3成功數量" + cnatsuccess_3 + " \r\n");//debug
                sw.WriteLine("測驗3左視野成功數量" + cnatsuccess3_left + " \r\n");//debug
                sw.WriteLine("測驗3右視野成功數量" + cnatsuccess3_right + " \r\n");//debug
            }
            if (locationbig == 4)
            {
                sw.WriteLine("測驗4");  //開始寫入  1~18
                for (int i = 1; i < 19; i++)
                {
                    sw.WriteLine("第" + (i) + "題: " + resulttime[locationbig][i]);  //開始寫入
                }
                sw.WriteLine("測驗4成功數量" + cnatsuccess_4 + " \r\n");//debug
                sw.WriteLine("測驗4左視野成功數量" + cnatsuccess4_left + " \r\n");//debug
                sw.WriteLine("測驗4右視野成功數量" + cnatsuccess4_right + " \r\n");//debug
            }
            if (locationbig == 5)
            {
                sw.WriteLine("測驗5");  //開始寫入  1~18
                for (int i = 1; i < 37; i++)
                {
                    sw.WriteLine("第" + (i) + "題: " + resulttime[locationbig][i]);  //開始寫入
                }
                sw.WriteLine("測驗5成功數量" + cnatsuccess_5 + " \r\n");//debug
                sw.WriteLine("測驗5左視野成功數量" + cnatsuccess5_left + " \r\n");//debug
                sw.WriteLine("測驗5右視野成功數量" + cnatsuccess5_right + " \r\n");//debug
            }
            if (locationbig == 6)
            {
                sw.WriteLine("測驗6");  //開始寫入  1~18
                for (int i = 1; i < 37; i++)
                {
                    sw.WriteLine("第" + (i) + "題: " + resulttime[locationbig][i]);  //開始寫入
                }
                sw.WriteLine("測驗6成功數量" + cnatsuccess_6 + " \r\n");//debug
                sw.WriteLine("測驗6左視野成功數量" + cnatsuccess6_left + " \r\n");//debug
                sw.WriteLine("測驗6右視野成功數量" + cnatsuccess6_right + " \r\n");//debug
            }
            if (locationbig == 7)
            {
                sw.WriteLine("測驗7");  //開始寫入  1~18
                for (int i = 1; i < 74; i++)
                {
                    if (leftright[i] == 1) //右手
                    {
                        sw.WriteLine("第" + (i) + "題: " + " 右手: " + resulttime[locationbig][i]);  //開始寫入
                    }
                    if (leftright[i] == 2) //左手
                    {
                        sw.WriteLine("第" + (i) + "題: " + " 左手: " + resulttime[locationbig][i]);  //開始寫入
                    }
                    if (leftright[i] == 0)
                    {
                        sw.WriteLine("第" + (i) + "題: " + resulttime[locationbig][i]);  //開始寫入
                    }
                }
                sw.WriteLine("測驗7成功數量" + cnatsuccess_7 + " \r\n");//debug
                sw.WriteLine("測驗7左視野成功數量" + cnatsuccess7_left + " \r\n");//debug
                sw.WriteLine("測驗7右視野成功數量" + cnatsuccess7_right + " \r\n");//debug
            }
            if (locationbig == 8)
            {
                sw.WriteLine("測驗8");  //開始寫入  
                for (int i = 1; i < 37; i++)
                {
                    sw.WriteLine("第" + (i) + "題: " + resulttime[locationbig][i]);  //開始寫入
                }
                sw.WriteLine("測驗8成功數量" + cnatsuccess_8 + " \r\n");//debug
                sw.WriteLine("測驗8左視野成功數量" + cnatsuccess8_left + " \r\n");//debug
                sw.WriteLine("測驗8右視野成功數量" + cnatsuccess8_right + " \r\n");//debug
            }
            sw.Flush();               //清空緩衝區    
            sw.Close();     //關閉流
            textBox1.AppendText("存檔" + " \r\n");//debug
        }

        public void savedata4()//測驗cnmt存檔  //管他3721都先這樣存
        {
            DirectoryInfo dir = new DirectoryInfo(System.Windows.Forms.Application.StartupPath);
            string savelocation = dir.Parent.Parent.Parent.FullName;
            savelocation = savelocation + "\\Record\\" + d1 + "_" + d7;  // 編碼+日期
            System.IO.Directory.CreateDirectory(savelocation);
            string q = savelocation + "\\";
            savelocation = savelocation + "\\" + d1 + "_" + d7 + "cnmt.txt";
            StreamWriter sw = new StreamWriter(savelocation, true);      //建立資料夾  

            for (int b = 0; b < 9; b++)
            {
                for (int i = 0; i < 600; i++)
                {
                    if (cnmtresultpress[b][i] == 0)
                    { break; }
                    sw.WriteLine("第" + b + "大題，" + "按鍵為:  " + cnmtresultpress[b][i] + "反應時間(ms): " + cnmtresulttime[b][i]);  //開始寫入
                }
            }

            sw.Flush();               //清空緩衝區    
            sw.Close();     //關閉流
        }


        private void Timer1_Tick(object sender, EventArgs e)// timer1 時間到了  右手一
        {
            temp1 = "0";
            timer1.Enabled = false;
            buttontest1 = 0;
            textBox1.AppendText("右手1時間到 \r\n");
            textBox1.AppendText("次數:  " + Convert.ToString(right15_1) + "次 \r\n");
            if (right15_1 < 31)
            {
                //進入右二
                checkleftright3();

            }
            else
            {
                //進入左一
                checkleftright2();
            }

        }

        private void Timer2_Tick(object sender, EventArgs e)//左手一結束  檢查是否左2
        {
            temp1 = "0";
            timer2.Enabled = false;
            buttontest1 = 0;
            textBox1.AppendText("左手1時間到 \r\n");
            textBox1.AppendText("次數:  " + Convert.ToString(left15_1) + "次 \r\n");
            if (left15_1 < 31)
            {
                //進入左二
                checkleftright4();
            }
            else
            {
                //開始測驗  判斷
                if (right15_1 < 31)//右手壞的
                {
                    if (right15_2 < 31)
                    {
                        waitaction = 3500;
                        textBox1.AppendText("dealy 3500ms \r\n");
                    }
                }
                else
                {
                    //不做事
                    waitaction = 1000;
                    textBox1.AppendText("dealy 1000ms \r\n");
                }
                //waitaction = 1000; 
                cnatp1();
            }
        }

        private void Timer3_Tick(object sender, EventArgs e) //右一 結束  右手第2次
        {
            temp1 = "0";
            timer3.Enabled = false;
            buttontest1 = 0;
            textBox1.AppendText("右手2時間到 \r\n");
            textBox1.AppendText("次數:  " + Convert.ToString(right15_2) + "次 \r\n");

            //進入左一


            checkleftright2();

        }

        private void Timer4_Tick(object sender, EventArgs e) //左手第二次   
        {
            temp1 = "0";
            timer4.Enabled = false;
            buttontest1 = 0;
            textBox1.AppendText("左手2時間到 \r\n");
            textBox1.AppendText("次數:  " + Convert.ToString(left15_2) + "次 \r\n");
            if (left15_2 < 31)//左手壞的
            {

                //增加時間
                waitaction = 3500;
                textBox1.AppendText("dealy 3500ms \r\n");
                cnatp1();
            }
            else//左手好的
            {
                if (right15_1 < 31)//看右手
                {
                    if (right15_2 < 31)//右手壞的
                    {
                        waitaction = 3500;
                    }
                }
                else//右手好的
                {
                    //不做事
                    waitaction = 1000;
                    textBox1.AppendText("dealy 1000ms \r\n");
                }
                //waitaction = 1000;
                cnatp1();
            }
        }



        //
        //
        //PRACTICE

        public void cnatp1()  //cnat  練習1  起始畫面
        {
            //在
            textBox2.AppendText("cnat 測驗一練習題 \r\n");


            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行測驗一練習題", "開始練習", "cnatp1", "", "");//


            /*for(int i=1; i<4; i++)
            {
               cnatmove1(delaycnatpractice[1][i],lighttime,waitaction,lightcnatpractice[1][i]);
            }*/
        }


        int cnatgo = 0;//等於1才跑下一輪

        public void cnatp1_2()  //cnat  練習1  執行迴圈
        {
            panel.closepicturetest6();
            locationbig = 1;
            for (int i = 1; i < 4; i++)
            {
                //textBox1.AppendText("迴圈開始" + Convert.ToString(i) + " \r\n");//debug
                cnatgo = 0;
                cnatmove1(delaycnatpractice[1][i], lighttime, waitaction, lightcnatpractice[1][i]);
                while (cnatgo == 0)
                {
                    Application.DoEvents();
                }
                if(cnatgo2 == 1)
                {
                    while (cnatgo2 == 1)
                    {
                        timer11.Enabled = true;
                    }
                }

                textBox1.AppendText("迴圈結束" + Convert.ToString(i) + " \r\n");//debug
            }
            cnatp1_3();
        }

        public void cnatp1_3()  //cnat 練習1  結束畫面
        {
            textBox2.AppendText("cnat 測驗一練習題完畢 \r\n");
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行測驗一練習題", "再次進行練習", "cnatp1", "進入正式測驗", "cnat1");//

        }

        public void cnatp2()  //cnat  練習2  起始畫面
        {
            //在
            textBox2.AppendText("cnat 測驗二練習題 \r\n");
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行測驗二練習題", "開始練習", "cnatp2", "", "");//
        }

        public void cnatp2_2()  //cnat  練習2  執行迴圈
        {
            panel.closepicturetest6();
            locationbig = 2;
            for (int i = 01; i < 4; i++)
            {
                //textBox1.AppendText("迴圈開始" + Convert.ToString(i) + " \r\n");//debug
                cnatgo = 0;
                cnatmove1(delaycnatpractice[2][i], lighttime, waitaction, lightcnatpractice[2][i]);
                while (cnatgo == 0)
                {
                    Application.DoEvents();
                }
                textBox1.AppendText("迴圈結束" + Convert.ToString(i) + " \r\n");//debug
            }
            cnatp2_3();
        }

        public void cnatp2_3()  //cnat 練習2  結束畫面
        {
            textBox2.AppendText("cnat 測驗二練習題完畢 \r\n");
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行測驗二練習題?", "再次進行練習", "cnatp2", "進入正式測驗", "cnat2");//
        }

        public void cnatp3()  //cnat  練習3  起始畫面
        {
            //在
            textBox2.AppendText("cnat 測驗三練習題 \r\n");
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行測驗三練習題", "開始練習", "cnatp3", "", "");//
        }

        public void cnatp3_2()  //cnat  練習3  執行迴圈
        {
            panel.closepicturetest6();
            locationbig = 3;
            for (int i = 0; i < 6; i++)
            {
                //textBox1.AppendText("迴圈開始" + Convert.ToString(i) + " \r\n");//debug
                cnatgo = 0;
                cnatmove1(delaycnatpractice[locationbig][i], lighttime, waitaction, lightcnatpractice[locationbig][i]);
                while (cnatgo == 0)
                {
                    Application.DoEvents();
                }
                textBox1.AppendText("迴圈結束" + Convert.ToString(i) + " \r\n");//debug
            }
            cnatp3_3();
        }

        public void cnatp3_3()  //cnat 練習3  結束畫面
        {
            textBox2.AppendText("cnat 測驗三練習題完畢 \r\n");
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行測驗三練習題?", "再次進行練習", "cnatp3", "進入正式測驗", "cnat3");//
        }

        public void cnatp4()  //cnat  練習4  起始畫面
        {
            //在
            textBox2.AppendText("cnat 測驗4練習題 \r\n");
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行測驗4練習題", "開始練習", "cnatp4", "", "");//
        }

        public void cnatp4_2()  //cnat  練習4  執行迴圈
        {
            panel.closepicturetest6();
            locationbig = 4;
            for (int i = 0; i < 6; i++)
            {
                //textBox1.AppendText("迴圈開始" + Convert.ToString(i) + " \r\n");//debug
                cnatgo = 0;
                cnatmove1(delaycnatpractice[locationbig][i], lighttime, waitaction, lightcnatpractice[locationbig][i]);
                while (cnatgo == 0)
                {
                    Application.DoEvents();
                }
                textBox1.AppendText("迴圈結束" + Convert.ToString(i) + " \r\n");//debug
            }
            cnatp4_3();
        }

        public void cnatp4_3()  //cnat 練習4  結束畫面
        {
            textBox2.AppendText("cnat 測驗4練習題完畢 \r\n");
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行測驗4練習題?", "再次進行練習", "cnatp4", "進入正式測驗", "cnat4");//
        }

        public void cnatp5()  //cnat  練習5  起始畫面
        {
            //在
            textBox2.AppendText("cnat 測驗5練習題 \r\n");
            locationbig = 5;
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行測驗5練習題", "開始練習", "cnatp5", "", "");//
            //panel.openpicturetest5();
        }

        public void cnatp5_2()  //cnat  練習5  執行迴圈
        {
            panel.closepicturetest6();
            locationbig = 5;
            for (int i = 0; i < 6; i++)
            {
                //textBox1.AppendText("迴圈開始" + Convert.ToString(i) + " \r\n");//debug
                cnatgo = 0;
                cnatmove1(delaycnatpractice[locationbig][i], lighttime, waitaction, lightcnatpractice[locationbig][i]);
                while (cnatgo == 0)
                {
                    Application.DoEvents();
                }
                textBox1.AppendText("迴圈結束" + Convert.ToString(i) + " \r\n");//debug
            }
            cnatp5_3();
        }

        public void cnatp5_3()  //cnat 練習5  結束畫面
        {
            textBox2.AppendText("cnat 測驗5練習題完畢 \r\n");
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行測驗5練習題?", "再次進行練習", "cnatp5", "進入正式測驗", "cnat5");//
        }

        public void cnatp6()  //cnat  練習6  起始畫面
        {
            //在
            textBox2.AppendText("cnat 測驗6練習題 \r\n");
            locationbig = 6;
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行測驗6練習題", "開始練習", "cnatp6", "", "");//
            //panel.openpicturetest5();
        }

        public void cnatp6_2()  //cnat  練習6  執行迴圈
        {
            panel.openpicturetest6(); //show picture
            locationbig = 6;
            for (int i = 0; i < 6; i++)
            {
                //textBox1.AppendText("迴圈開始" + Convert.ToString(i) + " \r\n");//debug
                cnatgo = 0;
                cnatmove1(delaycnatpractice[locationbig][i], lighttime, waitaction, lightcnatpractice[locationbig][i]);
                while (cnatgo == 0)
                {
                    Application.DoEvents();
                }
                textBox1.AppendText("迴圈結束" + Convert.ToString(i) + " \r\n");//debug
            }
            cnatp6_3();
        }

        public void cnatp6_3()  //cnat 練習6 結束畫面
        {
            textBox2.AppendText("cnat 測驗6練習題完畢 \r\n");
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行測驗6練習題?", "再次進行練習", "cnatp6", "進入正式測驗", "cnat6");//
        }

        public void cnatp7()  //cnat  練習7  起始畫面
        {
            //在
            textBox2.AppendText("cnat 測驗7練習題 \r\n");
            locationbig = 7;
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行測驗7練習題", "開始練習", "cnatp7", "", "");//
            //panel.openpicturetest5();
        }

        public void cnatp7_2()  //cnat  練習7  執行迴圈
        {
            panel.openpicturetest6(); //show picture
            locationbig = 7;
            for (int i = 0; i < 6; i++)
            {
                //textBox1.AppendText("迴圈開始" + Convert.ToString(i) + " \r\n");//debug
                cnatgo = 0;
                cnatmove1(delaycnatpractice[locationbig][i], lighttime, waitaction, lightcnatpractice[locationbig][i]);
                while (cnatgo == 0)
                {
                    Application.DoEvents();
                }
                textBox1.AppendText("迴圈結束" + Convert.ToString(i) + " \r\n");//debug
            }
            cnatp7_3();
        }

        public void cnatp7_3()  //cnat 練習7 結束畫面
        {
            textBox2.AppendText("cnat 測驗7練習題完畢 \r\n");
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行測驗7練習題?", "再次進行練習", "cnatp7", "進入正式測驗", "cnat7");//
        }

        public void cnatp8()  //cnat  練習8  起始畫面
        {
            //在
            textBox2.AppendText("cnat 測驗8練習題 \r\n");
            locationbig = 8;
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行測驗8練習題", "開始練習", "cnatp8", "", "");//
            //panel.openpicturetest5();
        }

        public void cnatp8_2()  //cnat  練習8  執行迴圈
        {
            panel.closepicturetest6(); //show picture
            locationbig = 8;
            for (int i = 0; i < 6; i++)
            {
                //textBox1.AppendText("迴圈開始" + Convert.ToString(i) + " \r\n");//debug
                cnatgo = 0;
                cnatmove1(delaycnatpractice[locationbig][i], lighttime, waitaction, lightcnatpractice[locationbig][i]);
                while (cnatgo == 0)
                {
                    Application.DoEvents();
                }
                textBox1.AppendText("迴圈結束" + Convert.ToString(i) + " \r\n");//debug
            }
            cnatp8_3();
        }

        public void cnatp8_3()  //cnat 練習7 結束畫面
        {
            textBox2.AppendText("cnat 測驗8練習題完畢 \r\n");
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行測驗8練習題?", "再次進行練習", "cnatp8", "進入正式測驗", "cnat8");//
        }

        //--------------------------------------------------
        //cnat


        int cnatsuccess_1;// 測驗1成功數
        int cnatsuccess_2;// 
        int cnatsuccess_3;//
        int cnatsuccess_4;// 
        int cnatsuccess_5;//
        int cnatsuccess_6;//
        int cnatsuccess_7;//
        int cnatsuccess_8;//
        int cnatsuccess_9;//
        //
        //左右視野成功數量
        int cnatsuccess3_left;
        int cnatsuccess4_left;
        int cnatsuccess5_left;
        int cnatsuccess5_leftpressonly;
        int cnatsuccess6_left;
        int cnatsuccess6_leftpressonly;
        int cnatsuccess7_left;
        int cnatsuccess8_left;
        int cnatsuccess9_left;

        int cnatsuccess3_right;
        int cnatsuccess4_right;
        int cnatsuccess5_right;
        int cnatsuccess5_rightpressonly;//只有按鍵成功
        int cnatsuccess6_right;
        int cnatsuccess6_rightpressonly;
        int cnatsuccess7_right;
        int cnatsuccess8_right;
        int cnatsuccess9_right;

        int dotest9;    // 是0的話不用做9  =1 要做9

        int onefourwrong = 0;  // =1代表1~4錯了
        int onefourredo = 0; //  =1 代表已經重做1-4 又碰到4了

        //t-test
        int NL; //左視野成功次數     
        int LRTT;//左視野有成功反應之按鍵時間總和
        double lrtm;// 左視野按鍵平均時間
        int NR;// 右視野成功次數
        int RRTT;//RRTT=右視野有成功反應之按鍵時間總和   
        double rrtm;// 左視野按鍵平均時間
        double lrts;//左視野按鍵平均時間變異數
        double rrts; // 右視野按鍵平均時間變異數
        double sp; //計算用
        double t;//計算用
        double lrti_lrtm;
        double rrti_rrtm;


        //test5   該按的是 1.5.7.8.10.13.15.18.19.21.22.24.28.30.31.33.34.35
        //test5 不該按的是 2.3.4.6. 9.11.12.14.16.17.20.23.25.26.27.29.32.36
        int[] test5press = new int[18] { 1, 5, 7, 8, 10, 13, 15, 18, 19, 21, 22, 24, 28, 30, 31, 33, 34, 35 };
        int[] test5notpress = new int[18] { 2, 3, 4, 6, 9, 11, 12, 14, 16, 17, 20, 23, 25, 26, 27, 29, 32, 36 };


        //test6   該按的是 1.2.4.10.11.12.14.15.18.20.21.23.24.28.29.34.35.36
        //test6 不該按的是 3.5.6.7.8.9.13.16.17.19.22.25.26.27.30.31.32.33
        int[] test6press = new int[18] { 1, 2, 4, 10, 11, 12, 14, 15, 18, 20, 21, 23, 24, 28, 29, 34, 35, 36 };
        int[] test6notpress = new int[18] { 3, 5, 6, 7, 8, 9, 13, 16, 17, 19, 22, 25, 26, 27, 30, 31, 32, 33 };

        //test 7
        int[] test7press = new int[36] { 1, 2, 3, 10, 7, 14, 19, 16, 21, 20, 23, 26, 27, 30, 29, 36, 35, 38, 39, 42, 41, 44, 45, 48, 47, 56, 55, 60, 57, 62, 67, 66, 69, 68, 71, 70 };
        int[] test7notpress = new int[36] { 5, 4, 9, 6, 11, 8, 13, 12, 15, 18, 17, 22, 25, 24, 31, 28, 33, 32, 37, 34, 43, 40, 49, 46, 51, 50, 53, 52, 59, 54, 61, 58, 63, 64, 65, 72 };

        // test8
        int[] test8press = new int[18] { 1, 5, 7, 8, 10, 13, 15, 18, 19, 21, 22, 24, 28, 30, 31, 33, 34, 35 };
        int[] test8notpress = new int[18] { 2, 3, 4, 6, 9, 11, 12, 14, 16, 17, 20, 23, 25, 26, 27, 29, 32, 36 };

       
        public void cnat_continue()//要繼續  //確定要從哪開始
        {
            int continuepoint = 0; 

            for (int i = 1; i < 9; i++) //1~8
            {
                if (resulttime[i][1] != 0 && resulttime[i + 1][1] == 0)
                {
                    continuepoint = i+1;  //2~9
                }
            }

            cnat_continue2(continuepoint);
            //textBox1.AppendText("(continuepoint= " + Convert.ToString(continuepoint)+" \r\n");

        }

        public void cnat_continue2(int continuepoint)
        {
            if(resulttime[1][19] == 3500)//如果讀回去
            {
                waitaction = 3500;
            }
            if (continuepoint == 2)
            {
                cnat1_judge2();
                cnat1_3();
            }
            else if (continuepoint == 3)
            {
                cnat1_judge2();
                cnat2_judge2();
                cnat2_3();
            }
            else if (continuepoint == 4)
            {
                cnat1_judge2();
                cnat2_judge2();
                cnat3_2judge();
                cnat3_3();
            }
            else if (continuepoint == 5)
            {
                cnat1_judge2();
                cnat2_judge2();
                cnat3_2judge();
                cnat4_2judge();
                cnat4_3();
                //textBox1.AppendText(" " + Convert.ToString(cnatsuccess_1 + cnatsuccess_2)+ "      "+ Convert.ToString(cnatsuccess_3 + cnatsuccess_4) + " \r\n");
            }
            else if (continuepoint == 6)
            {
                cnat1_judge2();
                cnat2_judge2();
                cnat3_2judge();
                cnat4_2judge();
                cnat5_2judge();
                cnat5_3();
            }
            else if (continuepoint == 7)
            {
                cnat1_judge2();
                cnat2_judge2();
                cnat3_2judge();
                cnat4_2judge();
                cnat5_2judge();
                cnat6_2judge();
                cnat6_3();
            }
            else if (continuepoint == 8)
            {
                cnat1_judge2();
                cnat2_judge2();
                cnat3_2judge();
                cnat4_2judge();
                cnat5_2judge();
                cnat6_2judge();
                cnat7_2judge();
                cnat7_3();
            }
            else if (continuepoint == 9)
            {
                cnat1_judge2();
                cnat2_judge2();
                cnat3_2judge();
                cnat4_2judge();
                cnat5_2judge();
                cnat6_2judge();
                cnat7_2judge();
                cnat8_2judge();
                cnat8_3();
            }
        }


        public void cnat1() // CNAT 測驗1  起始畫面
        {
            textBox2.AppendText("cnat 測驗一 \r\n");


            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行測驗一", "開始", "cnat1_2", "", "");//
            savedata2();
            locationbig = 1; //第一大題
        }

        public void cnat1_judge2() //CNAT 測驗1 判斷式
        {
            locationbig = 1;
            for (int i = 0; i < 18; i++)
            {
                if (resulttime[locationbig][locationsmall] != -1 && resulttime[locationbig][i] > delaycnat[locationbig][i] + 100)
                {
                    cnatsuccess_1 = cnatsuccess_1 + 1;
                }
            }
        }
        public void cnat1_2()  //cnat  測驗1  執行迴圈
        {
            panel.closepicturetest6();
            locationbig = 1;
            for (int i = 0; i < 18; i++)
            {
                locationsmall = i;// 小題題號
                //textBox1.AppendText("迴圈開始" + Convert.ToString(i) + " \r\n");//debug
                cnatgo = 0;
                //紀錄:測驗??  的第i 小題
                cnatmove2(delaycnat[locationbig][i], lighttime, waitaction, lightcnat[locationbig][i]);
                while (cnatgo == 0)//要進下一輪
                {
                    Application.DoEvents();
                }
                if (cnatgo2 == 1)
                {
                    while (cnatgo2 == 1)
                    {
                        timer11.Enabled = true;
                    }
                }
                //先存這輪的資料
                if (press == 1) //沒有按: result 存入-1
                {
                    resulttime[locationbig][locationsmall] = timelength;
                    press = 0;
                }
                else
                {
                    resulttime[locationbig][locationsmall] = -1;
                }
                textBox1.AppendText("測驗一題號" + Convert.ToString(i + 1) + "  " + Convert.ToString(resulttime[locationbig][i]) + " \r\n");//debug
            }

            cnat1_judge2();
            textBox1.AppendText("測驗一迴圈結束" + " \r\n");//debug
            textBox1.AppendText("測驗一成功數量" + cnatsuccess_1 + " \r\n");//debug
            //savedata3();//做完測驗存檔
            savedatacnatbig();
            cnat1_3();
        }

        public void cnat1_3()  //cnat 2  結束畫面  把檔案存入TXT
        {
            textBox2.AppendText("cnat 測驗1完畢 \r\n");
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("cnat 測驗1完畢\n進行測驗2練習題", "OK", "cnatp2", "", "");//

        }

        public void cnat2() // CNAT 測驗2  起始畫面
        {
            textBox2.AppendText("cnat 測驗二 \r\n");


            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行測驗二", "開始", "cnat2_2", "", "");//
            savedata2();
            locationbig = 2; //第2大題
        }
        public void cnat2_judge2()
        {
            locationbig = 2;
            for (int i = 0; i < 18; i++)
            {
                if (resulttime[locationbig][locationsmall] != -1 && resulttime[locationbig][i] > delaycnat[locationbig][i] + 100)
                {
                    cnatsuccess_2 = cnatsuccess_2 + 1;
                }
            }
        }
        public void cnat2_2()  //cnat  測驗2  執行迴圈
        {
            panel.closepicturetest6();
            locationbig = 2;
            for (int i = 0; i < 18; i++)
            {
                locationsmall = i;// 小題題號
                //textBox1.AppendText("迴圈開始" + Convert.ToString(i) + " \r\n");//debug
                cnatgo = 0;
                //紀錄:測驗??  的第i 小題
                cnatmove2(delaycnat[locationbig][i], lighttime, waitaction, lightcnat[locationbig][i]);
                while (cnatgo == 0)//要進下一輪
                {
                    Application.DoEvents();
                }
                //先存這輪的資料
                if (press == 1) //有按: 
                {
                    resulttime[locationbig][locationsmall] = timelength;
                    press = 0;
                }
                else        //沒按result 存入-1
                {
                    resulttime[locationbig][locationsmall] = -1;
                }



                textBox1.AppendText("測驗二題號" + Convert.ToString(i + 1) + "  " + Convert.ToString(resulttime[locationbig][i]) + " \r\n");//debug

            }

            //判斷成功
            cnat2_judge2();
            textBox1.AppendText("測驗二迴圈結束" + " \r\n");//debug
            textBox1.AppendText("測驗二成功數量" + cnatsuccess_2 + " \r\n");//debug
            //savedata3();//做完測驗存檔
            savedatacnatbig();
            cnat2_3();
        }

        public void cnat2_3()  //cnat 2  結束畫面  把檔案存入TXT
        {
            textBox2.AppendText("cnat 測驗二完畢 \r\n");
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("cnat 測驗二完畢\n進行測驗三練習題", "OK", "cnatp3", "", "");//

        }

        public void cnat3() // CNAT 測驗3  起始畫面
        {
            textBox2.AppendText("cnat 測驗三 \r\n");


            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行測驗三", "開始", "cnat3_2", "", "");//
            savedata2();
            locationbig = 3; //第3大題
        }

        public void cnat3_2judge()
        {
            locationbig = 3;
            for (int i = 1; i < 19; i++)
            {
                //判斷成功
                if (resulttime[locationbig][locationsmall] != -1 && resulttime[locationbig][i] > delaycnat[locationbig][i] + 100)
                {
                    cnatsuccess_3 = cnatsuccess_3 + 1;  //這裡依測驗要改
                    if (lightcnat[locationbig][i] >= 0 && lightcnat[locationbig][i] <= 19)
                    {
                        cnatsuccess3_left = cnatsuccess3_left + 1;
                    }
                    if (lightcnat[locationbig][i] >= 42 && lightcnat[locationbig][i] <= 61)
                    {
                        cnatsuccess3_left = cnatsuccess3_left + 1;
                    }
                    if (lightcnat[locationbig][i] >= 80 && lightcnat[locationbig][i] <= 99)
                    {
                        cnatsuccess3_left = cnatsuccess3_left + 1;
                    }
                    if (lightcnat[locationbig][i] >= 16 && lightcnat[locationbig][i] <= 35)
                    {
                        cnatsuccess3_right = cnatsuccess3_right + 1;
                    }
                    if (lightcnat[locationbig][i] >= 58 && lightcnat[locationbig][i] <= 77)
                    {
                        cnatsuccess3_right = cnatsuccess3_right + 1;
                    }
                    if (lightcnat[locationbig][i] >= 96 && lightcnat[locationbig][i] <= 115)
                    {
                        cnatsuccess3_right = cnatsuccess3_right + 1;
                    }
                }
            }
        }
        public void cnat3_2()  //cnat  測驗3  執行迴圈
        {
            panel.closepicturetest6();
            locationbig = 3;
            for (int i = 0; i < 19; i++)
            {
                locationsmall = i;// 小題題號
                //textBox1.AppendText("迴圈開始" + Convert.ToString(i) + " \r\n");//debug
                cnatgo = 0;
                //紀錄:測驗??  的第i 小題
                cnatmove2(delaycnat[locationbig][i], lighttime, waitaction, lightcnat[locationbig][i]);
                while (cnatgo == 0)//要進下一輪
                {
                    Application.DoEvents();
                }
                //先存這輪的資料
                if (press == 1) //沒有按: result 存入-1
                {
                    resulttime[locationbig][locationsmall] = timelength;
                    press = 0;
                }
                else
                {
                    resulttime[locationbig][locationsmall] = -1;
                }
                textBox1.AppendText("測驗3題號" + Convert.ToString(i) + "  " + Convert.ToString(resulttime[locationbig][i]) + " \r\n");//debug
            }

            cnat3_2judge();

            textBox1.AppendText("測驗3迴圈結束" + " \r\n");//debug
            textBox1.AppendText("測驗3成功數量" + cnatsuccess_3 + " \r\n");//debug
            textBox1.AppendText("測驗3左視野成功數量" + cnatsuccess3_left + " \r\n");//debug
            textBox1.AppendText("測驗3右視野成功數量" + cnatsuccess3_right + " \r\n");//debug
            //savedata3();//做完測驗存檔
            savedatacnatbig();
            cnat3_3();
        }

        public void cnat3_3()  //cnat 3  結束畫面  把檔案存入TXT
        {
            textBox2.AppendText("cnat 測驗三完畢 \r\n");
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行測驗四練習題", "OK", "cnatp4", "", "");//

        }

        public void cnat4() // CNAT 測驗4  起始畫面
        {
            textBox2.AppendText("cnat 測驗4 \r\n");


            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行測驗4", "開始", "cnat4_2", "", "");//
            savedata2();
            locationbig = 4; //第4大題
        }

        public void cnat4_2judge()
        {
            locationbig = 4;
            for (int i = 1; i < 19; i++)
            {
                if (resulttime[locationbig][locationsmall] != -1 && resulttime[locationbig][i] > delaycnat[locationbig][i] + 100)
                {
                    cnatsuccess_4 = cnatsuccess_4 + 1;  //這裡依測驗要改
                    if (lightcnat[locationbig][i] >= 0 && lightcnat[locationbig][i] <= 19)
                    {
                        cnatsuccess4_left = cnatsuccess4_left + 1;
                    }
                    if (lightcnat[locationbig][i] >= 42 && lightcnat[locationbig][i] <= 62)
                    {
                        cnatsuccess4_left = cnatsuccess4_left + 1;
                    }
                    if (lightcnat[locationbig][i] >= 80 && lightcnat[locationbig][i] <= 99)
                    {
                        cnatsuccess4_left = cnatsuccess4_left + 1;
                    }
                    if (lightcnat[locationbig][i] >= 16 && lightcnat[locationbig][i] <= 35)
                    {
                        cnatsuccess4_right = cnatsuccess4_right + 1;
                    }
                    if (lightcnat[locationbig][i] >= 58 && lightcnat[locationbig][i] <= 77)
                    {
                        cnatsuccess4_right = cnatsuccess4_right + 1;
                    }
                    if (lightcnat[locationbig][i] >= 96 && lightcnat[locationbig][i] <= 115)
                    {
                        cnatsuccess4_right = cnatsuccess4_right + 1;
                    }
                }
            }
        }

        public void cnat4_2()  //cnat  測驗4  執行迴圈
        {
            panel.closepicturetest6();
            locationbig = 4;
            for (int i = 0; i < 20; i++)
            {
                locationsmall = i;// 小題題號
                //textBox1.AppendText("迴圈開始" + Convert.ToString(i) + " \r\n");//debug
                cnatgo = 0;
                //紀錄:測驗??  的第i 小題
                cnatmove2(delaycnat[locationbig][i], lighttime, waitaction, lightcnat[locationbig][i]);
                while (cnatgo == 0)//要進下一輪
                {
                    Application.DoEvents();
                }
                //先存這輪的資料
                if (press == 1) //沒有按: result 存入-1
                {
                    resulttime[locationbig][locationsmall] = timelength;
                    press = 0;
                }
                else
                {
                    resulttime[locationbig][locationsmall] = -1;
                }
                //判斷成功    
                textBox1.AppendText("測驗4題號" + Convert.ToString(i) + "  " + Convert.ToString(resulttime[locationbig][i]) + " \r\n");//debug
            }

            //判斷拉出來
            cnat4_2judge();

            textBox1.AppendText("測驗4迴圈結束" + " \r\n");//debug
            textBox1.AppendText("測驗4成功數量" + cnatsuccess_4 + " \r\n");//debug
            textBox1.AppendText("測驗4左視野成功數量" + cnatsuccess4_left + " \r\n");//debug
            textBox1.AppendText("測驗4右視野成功數量" + cnatsuccess4_right + " \r\n");//debug
            //savedata3();//做完測驗存檔
            savedatacnatbig();
            cnat4_3();
        }


        public void cnat4_3()  //cnat 4  結束畫面   //亮燈時間檢查  1~4是否要重做
        {
            textBox2.AppendText("cnat 測驗4完畢 \r\n");
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;

            //要判斷了
            //1+2  3+4 miss >=10   變成  正確<26

            if (onefourredo == 0) //沒有重做過
            {
                if (Math.Abs(36 - (cnatsuccess_1 + cnatsuccess_2)) >= 10 || Math.Abs(36 - (cnatsuccess_3 + cnatsuccess_4)) >= 10)  //出問題
                {
                    onefourredo = 1;
                    lighttime = 3000;
                    textBox1.AppendText(Convert.ToString(cnatsuccess_1) +" "+ Convert.ToString(cnatsuccess_2)+" "+Convert.ToString(cnatsuccess_3)+" "+Convert.ToString(cnatsuccess_4));
                    pop.readfrom1("彈性施測2觸發，重做測驗1-4", "按下直接開始測驗練習1", "cnatp1", "", "");//
                }
                else   //沒出問題
                {
                    //練習五
                    pop.readfrom1("進行測驗5練習題", "按下直接開始測驗練習5", "cnatp5", "", "");//
                }
            }
            else
            {     // 已經重做過了 進5
                pop.readfrom1("進行測驗5練習題", "按下直接開始測驗練習5", "cnatp5", "", "");//
            }
        }

        public void cnat5() // CNAT 測驗5  起始畫面
        {
            textBox2.AppendText("cnat 測驗5 \r\n");


            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行測驗5", "開始", "cnat5_2", "", "");//
            savedata2();
            locationbig = 5; //第5大題
        }

        public void cnat5_2judge()
        {
            //test5   該按的是 1.5.7.8.10.13.15.18.19.21.22.24.28.30.31.33.34.35
            //test5 不該按的是 2.3.4.6. 9.11.12.14.16.17.20.23.25.26.27.29.32.36
            //int[] test5press = new int[18] { 1, 5, 7, 8, 10, 13, 15, 18, 19, 21, 22, 24, 28, 30, 31, 33, 34, 35 };
            //int[] test5notpress = new int[18] { 2, 3, 4, 6, 9, 11, 12, 14, 16, 17, 20, 23, 25, 26, 27, 29, 32, 36 };
            locationbig = 5;
            for (int i = 0; i < 18; i++)                      //不該按的沒按
            {
                if (resulttime[locationbig][test5notpress[i]] == -1)//不該按的沒按
                {
                    cnatsuccess_5 = cnatsuccess_5 + 1;  //這裡依測驗要改  //正確+1

                    if (lightcnat[locationbig][test5notpress[i]] >= 0 && lightcnat[locationbig][test5notpress[i]] <= 19)//左右視野判定
                    {
                        cnatsuccess5_left = cnatsuccess5_left + 1;
                    }
                    if (lightcnat[locationbig][test5notpress[i]] >= 43 && lightcnat[locationbig][test5notpress[i]] <= 63)
                    {
                        cnatsuccess5_left = cnatsuccess5_left + 1;
                    }
                    if (lightcnat[locationbig][test5notpress[i]] >= 81 && lightcnat[locationbig][test5notpress[i]] <= 100)
                    {
                        cnatsuccess5_left = cnatsuccess5_left + 1;
                    }
                    if (lightcnat[locationbig][test5notpress[i]] >= 16 && lightcnat[locationbig][test5notpress[i]] <= 35)
                    {
                        cnatsuccess5_right = cnatsuccess5_right + 1;
                    }
                    if (lightcnat[locationbig][test5notpress[i]] >= 59 && lightcnat[locationbig][test5notpress[i]] <= 78)
                    {
                        cnatsuccess5_right = cnatsuccess5_right + 1;
                    }
                    if (lightcnat[locationbig][test5notpress[i]] >= 97 && lightcnat[locationbig][test5notpress[i]] <= 116)
                    {
                        cnatsuccess5_right = cnatsuccess5_right + 1;
                    }
                }
            }//不該按的沒按

            for (int i = 0; i < 18; i++)
            {
                if (resulttime[locationbig][test5press[i]] > delaycnat[locationbig][test5press[i]] + 100)//該按的時間正確
                {
                    cnatsuccess_5 = cnatsuccess_5 + 1;  //這裡依測驗要改  //正確+1

                    if (lightcnat[locationbig][test5press[i]] >= 0 && lightcnat[locationbig][test5press[i]] <= 19)//左右視野判定
                    {
                        cnatsuccess5_left = cnatsuccess5_left + 1;//左視野
                        cnatsuccess5_leftpressonly = cnatsuccess5_leftpressonly + 1;
                        LRTT = LRTT + resulttime[locationbig][test5press[i]];//時間和 
                    }
                    if (lightcnat[locationbig][test5press[i]] >= 42 && lightcnat[locationbig][test5press[i]] <= 61)
                    {
                        cnatsuccess5_left = cnatsuccess5_left + 1;
                        cnatsuccess5_leftpressonly = cnatsuccess5_leftpressonly + 1;
                        LRTT = LRTT + resulttime[locationbig][test5press[i]];
                    }
                    if (lightcnat[locationbig][test5press[i]] >= 80 && lightcnat[locationbig][test5press[i]] <= 99)
                    {
                        cnatsuccess5_left = cnatsuccess5_left + 1;
                        cnatsuccess5_leftpressonly = cnatsuccess5_leftpressonly + 1;
                        LRTT = LRTT + resulttime[locationbig][test5press[i]];
                    }
                    if (lightcnat[locationbig][test5press[i]] >= 16 && lightcnat[locationbig][test5press[i]] <= 35)
                    {
                        cnatsuccess5_right = cnatsuccess5_right + 1;
                        cnatsuccess5_rightpressonly = cnatsuccess5_rightpressonly + 1;
                        RRTT = RRTT + resulttime[locationbig][test5press[i]];
                    }
                    if (lightcnat[locationbig][test5press[i]] >= 58 && lightcnat[locationbig][test5press[i]] <= 77)
                    {
                        cnatsuccess5_right = cnatsuccess5_right + 1;
                        cnatsuccess5_rightpressonly = cnatsuccess5_rightpressonly + 1;
                        RRTT = RRTT + resulttime[locationbig][test5press[i]];
                    }
                    if (lightcnat[locationbig][test5press[i]] >= 96 && lightcnat[locationbig][test5press[i]] <= 115)
                    {
                        cnatsuccess5_right = cnatsuccess5_right + 1;
                        cnatsuccess5_rightpressonly = cnatsuccess5_rightpressonly + 1;
                        RRTT = RRTT + resulttime[locationbig][test5press[i]];
                    }
                }
            }//該按的時間正確
        }
        public void cnat5_2()  //cnat  測驗5  執行迴圈
        {
            panel.closepicturetest6();
            locationbig = 5;
            for (int i = 0; i < 38; i++)
            {
                locationsmall = i;// 小題題號
                //textBox1.AppendText("迴圈開始" + Convert.ToString(i) + " \r\n");//debug
                cnatgo = 0;
                //紀錄:測驗??  的第i 小題
                cnatmove2(delaycnat[locationbig][i], lighttime, waitaction, lightcnat[locationbig][i]);
                while (cnatgo == 0)//要進下一輪
                {
                    Application.DoEvents();
                }
                //先存這輪的資料
                if (press == 1) //沒有按: result 存入-1
                {
                    resulttime[locationbig][locationsmall] = timelength;
                    press = 0;
                }
                else
                {
                    resulttime[locationbig][locationsmall] = -1;
                }
                //判斷成功

                textBox1.AppendText("測驗5題號" + Convert.ToString(i) + "  " + Convert.ToString(resulttime[locationbig][i]) + " \r\n");//debug

            }

            //判斷拉出來

            //test5   該按的是 1.5.7.8.10.13.15.18.19.21.22.24.28.30.31.33.34.35
            //test5 不該按的是 2.3.4.6. 9.11.12.14.16.17.20.23.25.26.27.29.32.36
            //int[] test5press = new int[18] { 1, 5, 7, 8, 10, 13, 15, 18, 19, 21, 22, 24, 28, 30, 31, 33, 34, 35 };
            //int[] test5notpress = new int[18] { 2, 3, 4, 6, 9, 11, 12, 14, 16, 17, 20, 23, 25, 26, 27, 29, 32, 36 };

            cnat5_2judge();

            textBox1.AppendText("測驗5迴圈結束" + " \r\n");//debug
            textBox1.AppendText("測驗5成功數量" + cnatsuccess_5 + " \r\n");//debug
            textBox1.AppendText("測驗5左視野成功數量" + cnatsuccess5_left + " \r\n");//debug
            textBox1.AppendText("測驗5右視野成功數量" + cnatsuccess5_right + " \r\n");//debug
            textBox1.AppendText("測驗5左視野嘗試成功數量" + cnatsuccess5_leftpressonly + " \r\n");//debug
            textBox1.AppendText("測驗5右視野嘗試成功數量" + cnatsuccess5_rightpressonly + " \r\n");//debug
            //savedata3();//做完測驗存檔
            savedatacnatbig();
            cnat5_3();
        }

        public void cnat5_3()  //cnat 5  結束畫面   
        {
            textBox2.AppendText("cnat 測驗5完畢 \r\n");
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行測驗6練習題", "OK", "cnatp6", "", "");//


        }

        public void cnat6() // CNAT 測驗6  起始畫面
        {
            textBox2.AppendText("cnat 測驗6 \r\n");


            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行測驗6", "開始", "cnat6_2", "", "");//
            savedata2();
            locationbig = 6; //第6大題
        }

        public void cnat6_2judge()
        {
            locationbig = 6;
            for (int i = 0; i < 18; i++)//不該按的沒按
            {
                if (resulttime[locationbig][test6notpress[i]] == -1)//不該按的沒按
                {
                    cnatsuccess_6 = cnatsuccess_6 + 1;  //這裡依測驗要改  //正確+1

                    if (lightcnat[locationbig][test6notpress[i]] >= 0 && lightcnat[locationbig][test6notpress[i]] <= 19)//左右視野判定
                    {
                        cnatsuccess6_left = cnatsuccess6_left + 1;
                    }
                    if (lightcnat[locationbig][test6notpress[i]] >= 43 && lightcnat[locationbig][test6notpress[i]] <= 63)
                    {
                        cnatsuccess6_left = cnatsuccess6_left + 1;
                    }
                    if (lightcnat[locationbig][test6notpress[i]] >= 81 && lightcnat[locationbig][test6notpress[i]] <= 100)
                    {
                        cnatsuccess6_left = cnatsuccess6_left + 1;
                    }
                    if (lightcnat[locationbig][test6notpress[i]] >= 16 && lightcnat[locationbig][test6notpress[i]] <= 35)
                    {
                        cnatsuccess6_right = cnatsuccess6_right + 1;
                    }
                    if (lightcnat[locationbig][test6notpress[i]] >= 59 && lightcnat[locationbig][test6notpress[i]] <= 78)
                    {
                        cnatsuccess6_right = cnatsuccess6_right + 1;
                    }
                    if (lightcnat[locationbig][test6notpress[i]] >= 97 && lightcnat[locationbig][test6notpress[i]] <= 116)
                    {
                        cnatsuccess6_right = cnatsuccess6_right + 1;
                    }
                }
            }//不該按的沒按

            for (int i = 0; i < 18; i++)//該按的時間正確
            {
                if (resulttime[locationbig][test6press[i]] > delaycnat[locationbig][test6press[i]] + 100)//該按的時間正確
                {
                    cnatsuccess_6 = cnatsuccess_6 + 1;  //這裡依測驗要改  //正確+1

                    if (lightcnat[locationbig][test6press[i]] >= 0 && lightcnat[locationbig][test6press[i]] <= 19)//左右視野判定 
                    {
                        cnatsuccess6_left = cnatsuccess6_left + 1;
                        cnatsuccess6_leftpressonly = cnatsuccess6_leftpressonly + 1;
                        LRTT = LRTT + resulttime[locationbig][test6press[i]];
                    }
                    if (lightcnat[locationbig][test6press[i]] >= 42 && lightcnat[locationbig][test6press[i]] <= 61)
                    {
                        cnatsuccess6_left = cnatsuccess6_left + 1;
                        cnatsuccess6_leftpressonly = cnatsuccess6_leftpressonly + 1;
                        LRTT = LRTT + resulttime[locationbig][test6press[i]];
                    }
                    if (lightcnat[locationbig][test6press[i]] >= 80 && lightcnat[locationbig][test6press[i]] <= 99)
                    {
                        cnatsuccess6_left = cnatsuccess6_left + 1;
                        cnatsuccess6_leftpressonly = cnatsuccess6_leftpressonly + 1;
                        LRTT = LRTT + resulttime[locationbig][test6press[i]];
                    }
                    if (lightcnat[locationbig][test6press[i]] >= 16 && lightcnat[locationbig][test6press[i]] <= 35)
                    {
                        cnatsuccess6_right = cnatsuccess6_right + 1;
                        cnatsuccess6_rightpressonly = cnatsuccess6_rightpressonly + 1;
                        RRTT = RRTT + resulttime[locationbig][test6press[i]];
                    }
                    if (lightcnat[locationbig][test6press[i]] >= 58 && lightcnat[locationbig][test6press[i]] <= 77)
                    {
                        cnatsuccess6_right = cnatsuccess6_right + 1;
                        cnatsuccess6_rightpressonly = cnatsuccess6_rightpressonly + 1;
                        RRTT = RRTT + resulttime[locationbig][test6press[i]];
                    }
                    if (lightcnat[locationbig][test6press[i]] >= 96 && lightcnat[locationbig][test6press[i]] <= 115)
                    {
                        cnatsuccess6_right = cnatsuccess6_right + 1;
                        cnatsuccess6_rightpressonly = cnatsuccess6_rightpressonly + 1;
                        RRTT = RRTT + resulttime[locationbig][test6press[i]];
                    }
                }
            }//該按的時間正確
        }
        public void cnat6_2()  //cnat  測驗6 執行迴圈
        {
            panel.openpicturetest6(); //show picture
            locationbig = 6;
            for (int i = 0; i < 38; i++)
            {
                locationsmall = i;// 小題題號
                //textBox1.AppendText("迴圈開始" + Convert.ToString(i) + " \r\n");//debug
                cnatgo = 0;
                //紀錄:測驗??  的第i 小題
                cnatmove2(delaycnat[locationbig][i], lighttime, waitaction, lightcnat[locationbig][i]);
                while (cnatgo == 0)//要進下一輪
                {
                    Application.DoEvents();
                }
                //先存這輪的資料
                if (press == 1) //沒有按: result 存入-1
                {
                    resulttime[locationbig][locationsmall] = timelength;
                    press = 0;
                }
                else
                {
                    resulttime[locationbig][locationsmall] = -1;
                }
                //判斷成功

                textBox1.AppendText("測驗6題號" + Convert.ToString(i) + "  " + Convert.ToString(resulttime[locationbig][i]) + " \r\n");//debug

            }

            //判斷拉出來

            //test6   該按的是 1.2.4.10.11.12.14.15.18.20.21.23.24.28.29.34.35.36
            //test6 不該按的是 3.5.6.7.8.9.13.16.17.19.22.25.26.27.30.31.32.33
            //int[] test6press = new int[18] { 1, 2, 4, 10, 11, 12, 14, 15, 18, 20, 21, 23, 24, 28, 29, 34, 35, 36 };
            //int[] test6notpress = new int[18] { 3, 5, 6, 7, 8, 9, 13, 16, 17, 19, 22, 25, 26, 27, 30, 31, 32, 33 };

            cnat6_2judge();

            textBox1.AppendText("測驗6迴圈結束" + " \r\n");//debug
            textBox1.AppendText("測驗6成功數量" + cnatsuccess_6 + " \r\n");//debug
            textBox1.AppendText("測驗6左視野成功數量" + cnatsuccess6_left + " \r\n");//debug
            textBox1.AppendText("測驗6右視野成功數量" + cnatsuccess6_right + " \r\n");//debug
            textBox1.AppendText("測驗6左視野嘗試成功數量" + cnatsuccess6_leftpressonly + " \r\n");//debug
            textBox1.AppendText("測驗6右視野嘗試成功數量" + cnatsuccess6_rightpressonly + " \r\n");//debug
            //savedata3();//做完測驗存檔
            savedatacnatbig();
            cnat6_3();
        }

        public void cnat6_3()  //cnat 6  結束畫面   //計算左右偏差
        {
            textBox2.AppendText("cnat 測驗6完畢 \r\n");
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;

            //要判斷了
            // 左右不平衡條件1
            //測驗3 + 測驗4  之 𝑎𝑏𝑠(𝑁𝐿−𝑁𝑅)> 3  則加測測驗9
            if (Math.Abs(cnatsuccess3_left + cnatsuccess4_left - cnatsuccess3_right - cnatsuccess4_right) > 3)
            {
                dotest9 = 1;
            }

            //左右不平衡條件2
            //測驗5 +測驗6  之 𝑎𝑏𝑠(𝑁𝐿−𝑁𝑅)>3則加測測驗9     //要不要pressonly
            if (Math.Abs(cnatsuccess5_leftpressonly + cnatsuccess6_leftpressonly - cnatsuccess5_rightpressonly - cnatsuccess6_rightpressonly) > 3)
            {
                dotest9 = 1;
            }

            //左右不平衡條件3
            //t-test

            NL = cnatsuccess5_leftpressonly + cnatsuccess6_leftpressonly;
            NR = cnatsuccess5_rightpressonly + cnatsuccess6_rightpressonly;

            if (NL == 0)
            {
                NL = 1;
            }

            if (NR == 0)
            {
                NR = 1;
            }

            lrtm = LRTT / NL;
            rrtm = RRTT / NR;

            for (int i = 0; i < 18; i++)
            {
                if (resulttime[locationbig][test6press[i]] > delaycnat[locationbig][test6press[i]] + 100)  //test6 //該按的時間正確
                {
                    if (lightcnat[locationbig][test6press[i]] >= 0 && lightcnat[locationbig][test6press[i]] <= 19)//左右視野判定
                    {
                        lrti_lrtm = lrti_lrtm + (resulttime[locationbig][test6press[i]] - lrtm) * (resulttime[locationbig][test6press[i]] - lrtm);
                    }
                    if (lightcnat[locationbig][test6press[i]] >= 43 && lightcnat[locationbig][test6press[i]] <= 63)
                    {
                        lrti_lrtm = lrti_lrtm + (resulttime[locationbig][test6press[i]] - lrtm) * (resulttime[locationbig][test6press[i]] - lrtm);
                    }
                    if (lightcnat[locationbig][test6press[i]] >= 81 && lightcnat[locationbig][test6press[i]] <= 100)
                    {
                        lrti_lrtm = lrti_lrtm + (resulttime[locationbig][test6press[i]] - lrtm) * (resulttime[locationbig][test6press[i]] - lrtm);
                    }
                }
            }  //西格瑪 test6 (左視野-平均值)^2

            for (int i = 0; i < 18; i++)
            {
                if (resulttime[locationbig][test5press[i]] > delaycnat[locationbig][test5press[i]] + 100)  //test5 //該按的時間正確
                {
                    if (lightcnat[locationbig][test5press[i]] >= 0 && lightcnat[locationbig][test5press[i]] <= 19)//左右視野判定
                    {
                        lrti_lrtm = lrti_lrtm + (resulttime[locationbig][test5press[i]] - lrtm) * (resulttime[locationbig][test5press[i]] - lrtm);
                    }
                    if (lightcnat[locationbig][test5press[i]] >= 43 && lightcnat[locationbig][test5press[i]] <= 63)
                    {
                        lrti_lrtm = lrti_lrtm + (resulttime[locationbig][test5press[i]] - lrtm) * (resulttime[locationbig][test5press[i]] - lrtm);
                    }
                    if (lightcnat[locationbig][test5press[i]] >= 81 && lightcnat[locationbig][test5press[i]] <= 100)
                    {
                        lrti_lrtm = lrti_lrtm + (resulttime[locationbig][test5press[i]] - lrtm) * (resulttime[locationbig][test5press[i]] - lrtm);
                    }
                }
            }  //西格瑪 test5 (左視野-平均值)^2

            lrts = lrti_lrtm / (NL - 1);

            for (int i = 0; i < 18; i++)
            {
                if (resulttime[locationbig][test6press[i]] > delaycnat[locationbig][test6press[i]] + 100)//該按的時間正確
                {
                    if (lightcnat[locationbig][test6press[i]] >= 16 && lightcnat[locationbig][test6press[i]] <= 35)
                    {
                        rrti_rrtm = rrti_rrtm + (resulttime[locationbig][test6press[i]] - rrtm) * (resulttime[locationbig][test6press[i]] - rrtm);
                    }
                    if (lightcnat[locationbig][test6press[i]] >= 59 && lightcnat[locationbig][test6press[i]] <= 78)
                    {
                        rrti_rrtm = rrti_rrtm + (resulttime[locationbig][test6press[i]] - rrtm) * (resulttime[locationbig][test6press[i]] - rrtm);
                    }
                    if (lightcnat[locationbig][test6press[i]] >= 97 && lightcnat[locationbig][test6press[i]] <= 116)
                    {
                        rrti_rrtm = rrti_rrtm + (resulttime[locationbig][test6press[i]] - rrtm) * (resulttime[locationbig][test6press[i]] - rrtm);
                    }
                }
            }//西格瑪 test6 (右視野-平均值)^2

            for (int i = 0; i < 18; i++)
            {
                if (resulttime[locationbig][test5press[i]] > delaycnat[locationbig][test5press[i]] + 100)//該按的時間正確
                {
                    if (lightcnat[locationbig][test5press[i]] >= 16 && lightcnat[locationbig][test5press[i]] <= 35)
                    {
                        rrti_rrtm = rrti_rrtm + (resulttime[locationbig][test5press[i]] - rrtm) * (resulttime[locationbig][test5press[i]] - rrtm);
                    }
                    if (lightcnat[locationbig][test5press[i]] >= 59 && lightcnat[locationbig][test5press[i]] <= 78)
                    {
                        rrti_rrtm = rrti_rrtm + (resulttime[locationbig][test5press[i]] - rrtm) * (resulttime[locationbig][test5press[i]] - rrtm);
                    }
                    if (lightcnat[locationbig][test5press[i]] >= 97 && lightcnat[locationbig][test5press[i]] <= 116)
                    {
                        rrti_rrtm = rrti_rrtm + (resulttime[locationbig][test5press[i]] - rrtm) * (resulttime[locationbig][test5press[i]] - rrtm);
                    }
                }
            }//西格瑪 test5 (右視野-平均值)^2

            rrts = rrti_rrtm / (NR - 1);

            sp = (lrts * (NL - 1) + rrts * (NR - 1)) / (NL + NR - 2);

            t = (lrtm - rrtm) / Math.Sqrt(sp * (1 / NL + 1 / NR));

            if (Math.Abs(t) >= 1.919)
            {
                dotest9 = 1;
            }
            if (dotest9 == 1)
            {
                textBox1.AppendText("加測測驗9" + " \r\n");//debug
            }
            pop.readfrom1("進行測驗7練習題", "OK", "cnatp7", "", "");//
        }

        public void cnat7() // CNAT 測驗7  起始畫面
        {
            textBox2.AppendText("cnat 測驗7 \r\n");


            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行測驗7", "開始", "cnat7_2", "", "");//
            savedata2();
            locationbig = 7; //第7大題
        }

        public void cnat7_2judge()
        {
            locationbig = 7;
            for (int i = 0; i < 36; i++)//不該按的沒按
            {
                if (resulttime[locationbig][test7notpress[i]] == -1)//不該按的沒按
                {
                    cnatsuccess_7 = cnatsuccess_7 + 1;  //這裡依測驗要改  //正確+1

                    if (lightcnat[locationbig][test7notpress[i]] >= 0 && lightcnat[locationbig][test7notpress[i]] <= 19)//左右視野判定
                    {
                        cnatsuccess7_left = cnatsuccess7_left + 1;
                    }
                    if (lightcnat[locationbig][test7notpress[i]] >= 42 && lightcnat[locationbig][test7notpress[i]] <= 61)
                    {
                        cnatsuccess7_left = cnatsuccess7_left + 1;
                    }
                    if (lightcnat[locationbig][test7notpress[i]] >= 80 && lightcnat[locationbig][test7notpress[i]] <= 99)
                    {
                        cnatsuccess7_left = cnatsuccess7_left + 1;
                    }
                    if (lightcnat[locationbig][test7notpress[i]] >= 16 && lightcnat[locationbig][test7notpress[i]] <= 35)
                    {
                        cnatsuccess7_right = cnatsuccess7_right + 1;
                    }
                    if (lightcnat[locationbig][test7notpress[i]] >= 58 && lightcnat[locationbig][test7notpress[i]] <= 77)
                    {
                        cnatsuccess7_right = cnatsuccess7_right + 1;
                    }
                    if (lightcnat[locationbig][test7notpress[i]] >= 96 && lightcnat[locationbig][test7notpress[i]] <= 115)
                    {
                        cnatsuccess7_right = cnatsuccess7_right + 1;
                    }
                }
            }//不該按的沒按

            for (int i = 0; i < 36; i++)//該按的時間正確
            {
                if (resulttime[locationbig][test7press[i]] > delaycnat[locationbig][test7press[i]] + 100)//該按的時間正確
                {
                    cnatsuccess_7 = cnatsuccess_7 + 1;  //這裡依測驗要改  //正確+1

                    if (lightcnat[locationbig][test7press[i]] >= 0 && lightcnat[locationbig][test7press[i]] <= 19)//左右視野判定  //且手要對
                    {
                        cnatsuccess7_left = cnatsuccess7_left + 1;
                        //LRTT = LRTT + resulttime[locationbig][test7press[i]];
                    }
                    if (lightcnat[locationbig][test7press[i]] >= 42 && lightcnat[locationbig][test7press[i]] <= 61)
                    {
                        cnatsuccess7_left = cnatsuccess7_left + 1;
                        //LRTT = LRTT + resulttime[locationbig][test7press[i]];
                    }
                    if (lightcnat[locationbig][test7press[i]] >= 80 && lightcnat[locationbig][test7press[i]] <= 99)
                    {
                        cnatsuccess7_left = cnatsuccess7_left + 1;
                        //LRTT = LRTT + resulttime[locationbig][test7press[i]];
                    }
                    if (lightcnat[locationbig][test7press[i]] >= 16 && lightcnat[locationbig][test7press[i]] <= 35)
                    {
                        cnatsuccess7_right = cnatsuccess7_right + 1;
                        //RRTT = RRTT + resulttime[locationbig][test7press[i]];
                    }
                    if (lightcnat[locationbig][test7press[i]] >= 58 && lightcnat[locationbig][test7press[i]] <= 77)
                    {
                        cnatsuccess7_right = cnatsuccess7_right + 1;
                        //RRTT = RRTT + resulttime[locationbig][test6press[i]];
                    }
                    if (lightcnat[locationbig][test7press[i]] >= 96 && lightcnat[locationbig][test7press[i]] <= 115)
                    {
                        cnatsuccess7_right = cnatsuccess7_right + 1;
                        //RRTT = RRTT + resulttime[locationbig][test7press[i]];
                    }
                }
            }//該按的時間正確
        }
        public void cnat7_2()  //cnat  測驗7 執行迴圈
        {
            panel.openpicturetest6(); //show picture
            locationbig = 7;
            for (int i = 0; i < 36; i++)
            {
                //locationsmall = i;// 小題題號
                //textBox1.AppendText("迴圈開始" + Convert.ToString(i) + " \r\n");//debug

                //  2*I
                cnatgo = 0;
                //紀錄:測驗??  的第i 小題
                cnatmove2(delaycnat[locationbig][2*i], lighttime, waitaction, lightcnat[locationbig][2*i]);
                while (cnatgo == 0)//要進下一輪  //timer 到變成1
                {
                    Application.DoEvents();
                }
                //先存這輪的資料
                if (press == 1) //沒有按: result 存入-1  有按存時間
                {
                    resulttime[locationbig][2*i] = timelength;
                    press = 0;
                }
                else
                {
                    resulttime[locationbig][2*i] = -1;
                }
                //判斷成功

                //左右     //0 代表沒按 1右手 2左手
                if (lr7 == 1)
                {
                    leftright[2*i] = 1;
                }
                if (lr7 == 2)
                {
                    leftright[2*i] = 2;
                }
                lr7 = 0;
                textBox1.AppendText("測驗7題號" + Convert.ToString(2 * i) + "  " + Convert.ToString(resulttime[locationbig][2 * i]) + " \r\n");//debug
                // 2*i +1
                cnatgo = 0;
                //紀錄:測驗??  的第i 小題
                cnatmove3(delaycnat[locationbig][(2 * i)+1], lighttime, waitaction, lightcnat[locationbig][(2 * i)+1]);
                while (cnatgo == 0)//要進下一輪  //timer 到變成1
                {
                    Application.DoEvents();
                }
                //先存這輪的資料
                if (press == 1) //沒有按: result 存入-1  有按存時間
                {
                    resulttime[locationbig][2*i+1] = timelength;
                    press = 0;
                }
                else
                {
                    resulttime[locationbig][2*i+1] = -1;
                }
                //判斷成功

                //左右     //0 代表沒按 1右手 2左手
                if (lr7 == 1)
                {
                    leftright[(2*i)+1] = 1;
                }
                if (lr7 == 2)
                {
                    leftright[(2 * i) + 1] = 2;
                }
                lr7 = 0;
                textBox1.AppendText("測驗7題號" + Convert.ToString((2*i)+1) + "  " + Convert.ToString(resulttime[locationbig][(2*i)+1]) + " \r\n");//debug
            }

            //判斷拉出來

            //test6   該按的是 1.2.4.10.11.12.14.15.18.20.21.23.24.28.29.34.35.36
            //test6 不該按的是 3.5.6.7.8.9.13.16.17.19.22.25.26.27.30.31.32.33
            //int[] test6press = new int[18] { 1, 2, 4, 10, 11, 12, 14, 15, 18, 20, 21, 23, 24, 28, 29, 34, 35, 36 };
            //int[] test6notpress = new int[18] { 3, 5, 6, 7, 8, 9, 13, 16, 17, 19, 22, 25, 26, 27, 30, 31, 32, 33 };




            textBox1.AppendText("測驗7迴圈結束" + " \r\n");//debug
            textBox1.AppendText("測驗7成功數量" + cnatsuccess_7 + " \r\n");//debug
            //textBox1.AppendText("測驗7左視野成功數量" + cnatsuccess7_left + " \r\n");//debug
            //textBox1.AppendText("測驗7右視野成功數量" + cnatsuccess7_right + " \r\n");//debug
            //savedata3();//做完測驗存檔
            savedatacnatbig();
            cnat7_3();
        }

        public void cnat7_3()  //cnat 7  結束畫面   
        {
            textBox2.AppendText("cnat 測驗7完畢 \r\n");
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行測驗8練習題", "OK", "cnatp8", "", "");//


        }

        public void cnat8() // CNAT 測驗8  起始畫面
        {
            textBox2.AppendText("cnat 測驗8 \r\n");


            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行測驗8", "開始", "cnat8_2", "", "");//
            savedata2();
            locationbig = 8; //第8大題
        }

        public void cnat8_2judge()
        {
            locationbig = 8;
            for (int i = 0; i < 18; i++)//不該按的沒按
            {
                if (resulttime[locationbig][test7notpress[i]] == -1)//不該按的沒按
                {
                    cnatsuccess_8 = cnatsuccess_8 + 1;  //這裡依測驗要改  //正確+1

                    if (lightcnat[locationbig][test8notpress[i]] >= 0 && lightcnat[locationbig][test8notpress[i]] <= 19)//左右視野判定
                    {
                        cnatsuccess8_left = cnatsuccess8_left + 1;
                    }
                    if (lightcnat[locationbig][test8notpress[i]] >= 42 && lightcnat[locationbig][test8notpress[i]] <= 61)
                    {
                        cnatsuccess8_left = cnatsuccess8_left + 1;
                    }
                    if (lightcnat[locationbig][test8notpress[i]] >= 80 && lightcnat[locationbig][test8notpress[i]] <= 99)
                    {
                        cnatsuccess8_left = cnatsuccess8_left + 1;
                    }
                    if (lightcnat[locationbig][test8notpress[i]] >= 16 && lightcnat[locationbig][test8notpress[i]] <= 35)
                    {
                        cnatsuccess8_right = cnatsuccess8_right + 1;
                    }
                    if (lightcnat[locationbig][test8notpress[i]] >= 58 && lightcnat[locationbig][test8notpress[i]] <= 77)
                    {
                        cnatsuccess8_right = cnatsuccess8_right + 1;
                    }
                    if (lightcnat[locationbig][test8notpress[i]] >= 96 && lightcnat[locationbig][test8notpress[i]] <= 115)
                    {
                        cnatsuccess8_right = cnatsuccess8_right + 1;
                    }
                }
            }//不該按的沒按

            for (int i = 0; i < 18; i++)//該按的時間正確
            {
                if (resulttime[locationbig][test8press[i]] > delaycnat[locationbig][test8press[i]] + 100)//該按的時間正確
                {
                    cnatsuccess_8 = cnatsuccess_8 + 1;  //這裡依測驗要改  //正確+1

                    if (lightcnat[locationbig][test8press[i]] >= 0 && lightcnat[locationbig][test8press[i]] <= 19)//左右視野判定
                    {
                        cnatsuccess8_left = cnatsuccess8_left + 1;
                        //LRTT = LRTT + resulttime[locationbig][test7press[i]];
                    }
                    if (lightcnat[locationbig][test8press[i]] >= 42 && lightcnat[locationbig][test8press[i]] <= 61)
                    {
                        cnatsuccess8_left = cnatsuccess8_left + 1;
                        //LRTT = LRTT + resulttime[locationbig][test7press[i]];
                    }
                    if (lightcnat[locationbig][test8press[i]] >= 80 && lightcnat[locationbig][test8press[i]] <= 99)
                    {
                        cnatsuccess8_left = cnatsuccess8_left + 1;
                        //LRTT = LRTT + resulttime[locationbig][test7press[i]];
                    }
                    if (lightcnat[locationbig][test8press[i]] >= 16 && lightcnat[locationbig][test8press[i]] <= 35)
                    {
                        cnatsuccess8_right = cnatsuccess8_right + 1;
                        //RRTT = RRTT + resulttime[locationbig][test7press[i]];
                    }
                    if (lightcnat[locationbig][test8press[i]] >= 58 && lightcnat[locationbig][test8press[i]] <= 77)
                    {
                        cnatsuccess8_right = cnatsuccess8_right + 1;
                        //RRTT = RRTT + resulttime[locationbig][test6press[i]];
                    }
                    if (lightcnat[locationbig][test8press[i]] >= 96 && lightcnat[locationbig][test8press[i]] <= 115)
                    {
                        cnatsuccess8_right = cnatsuccess8_right + 1;
                        //RRTT = RRTT + resulttime[locationbig][test7press[i]];
                    }
                }
            }//該按的時間正確
        }
        public void cnat8_2()  //cnat  測驗8 執行迴圈
        {
            panel.closepicturetest6();
            locationbig = 8;
            panel.closepicturetest6();
            for (int i = 0; i < 38; i++)
            {
                locationsmall = i;// 小題題號
                //textBox1.AppendText("迴圈開始" + Convert.ToString(i) + " \r\n");//debug
                cnatgo = 0;
                //紀錄:測驗??  的第i 小題
                cnatmove2(delaycnat[locationbig][i], lighttime, waitaction, lightcnat[locationbig][i]);
                while (cnatgo == 0)//要進下一輪
                {
                    Application.DoEvents();
                }
                //先存這輪的資料
                if (press == 1) //沒有按: result 存入-1  有按存時間
                {
                    resulttime[locationbig][locationsmall] = timelength;
                    press = 0;
                }
                else
                {
                    resulttime[locationbig][locationsmall] = -1;
                }
                //判斷成功

                textBox1.AppendText("測驗8題號" + Convert.ToString(i) + "  " + Convert.ToString(resulttime[locationbig][i]) + " \r\n");//debug

            }

            //判斷拉出來


            textBox1.AppendText("測驗8迴圈結束" + " \r\n");//debug
            textBox1.AppendText("測驗8成功數量" + cnatsuccess_8 + " \r\n");//debug
            //textBox1.AppendText("測驗8左視野成功數量" + cnatsuccess8_left + " \r\n");//debug
            //textBox1.AppendText("測驗8右視野成功數量" + cnatsuccess8_right + " \r\n");//debug
            //savedata3();//做完測驗存檔
            savedatacnatbig();
            cnat8_3();
        }

        public void cnat8_3()  //cnat 8  結束畫面   
        {
            textBox2.AppendText("cnat 測驗8完畢 \r\n");
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            if (dotest9 == 1)
            {
                pop.readfrom1("進行測驗9", "實驗9開始", "cnat9", "", "");//
            }
            else
            {
                pop.readfrom1("cnat 結束", "OK", "", "", "");
            }

        }

        public void cnat9() // CNAT 測驗9  起始畫面
        {
            textBox2.AppendText("cnat 測驗9 \r\n");


            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行測驗9", "開始", "cnat9_2", "", "");//
            savedata2();
            locationbig = 9; //第8大題
        }

        public void cnat9_2judge()
        {
            locationbig = 9;
            for (int i = 0; i < 18; i++)//不該按的沒按
            {
                if (resulttime[locationbig][test6notpress[i]] == -1)//不該按的沒按
                {
                    cnatsuccess_9 = cnatsuccess_9 + 1;  //這裡依測驗要改  //正確+1

                    if (lightcnat[locationbig][test6notpress[i]] >= 0 && lightcnat[locationbig][test6notpress[i]] <= 19)//左右視野判定
                    {
                        cnatsuccess9_left = cnatsuccess9_left + 1;
                    }
                    if (lightcnat[locationbig][test6notpress[i]] >= 42 && lightcnat[locationbig][test6notpress[i]] <= 61)
                    {
                        cnatsuccess9_left = cnatsuccess9_left + 1;
                    }
                    if (lightcnat[locationbig][test6notpress[i]] >= 80 && lightcnat[locationbig][test6notpress[i]] <= 99)
                    {
                        cnatsuccess9_left = cnatsuccess9_left + 1;
                    }
                    if (lightcnat[locationbig][test6notpress[i]] >= 16 && lightcnat[locationbig][test6notpress[i]] <= 35)
                    {
                        cnatsuccess9_right = cnatsuccess9_right + 1;
                    }
                    if (lightcnat[locationbig][test6notpress[i]] >= 58 && lightcnat[locationbig][test6notpress[i]] <= 77)
                    {
                        cnatsuccess9_right = cnatsuccess9_right + 1;
                    }
                    if (lightcnat[locationbig][test6notpress[i]] >= 96 && lightcnat[locationbig][test6notpress[i]] <= 115)
                    {
                        cnatsuccess9_right = cnatsuccess9_right + 1;
                    }
                }
            }//不該按的沒按

            for (int i = 0; i < 18; i++)//該按的時間正確
            {
                if (resulttime[locationbig][test6press[i]] > delaycnat[locationbig][test6press[i]] + 100)//該按的時間正確
                {
                    cnatsuccess_9 = cnatsuccess_9 + 1;  //這裡依測驗要改  //正確+1

                    if (lightcnat[locationbig][test6press[i]] >= 0 && lightcnat[locationbig][test6press[i]] <= 19)//左右視野判定
                    {
                        cnatsuccess9_left = cnatsuccess9_left + 1;
                        //LRTT = LRTT + resulttime[locationbig][test7press[i]];
                    }
                    if (lightcnat[locationbig][test6press[i]] >= 42 && lightcnat[locationbig][test6press[i]] <= 61)
                    {
                        cnatsuccess9_left = cnatsuccess9_left + 1;
                        //LRTT = LRTT + resulttime[locationbig][test7press[i]];
                    }
                    if (lightcnat[locationbig][test6press[i]] >= 80 && lightcnat[locationbig][test6press[i]] <= 99)
                    {
                        cnatsuccess9_left = cnatsuccess9_left + 1;
                        //LRTT = LRTT + resulttime[locationbig][test7press[i]];
                    }
                    if (lightcnat[locationbig][test6press[i]] >= 16 && lightcnat[locationbig][test6press[i]] <= 35)
                    {
                        cnatsuccess9_right = cnatsuccess9_right + 1;
                        //RRTT = RRTT + resulttime[locationbig][test7press[i]];
                    }
                    if (lightcnat[locationbig][test6press[i]] >= 58 && lightcnat[locationbig][test6press[i]] <= 77)
                    {
                        cnatsuccess9_right = cnatsuccess9_right + 1;
                        //RRTT = RRTT + resulttime[locationbig][test6press[i]];
                    }
                    if (lightcnat[locationbig][test6press[i]] >= 96 && lightcnat[locationbig][test6press[i]] <= 115)
                    {
                        cnatsuccess9_right = cnatsuccess9_right + 1;
                        //RRTT = RRTT + resulttime[locationbig][test7press[i]];
                    }
                }
            }//該按的時間正確
        }
        public void cnat9_2()  //cnat  測驗9 執行迴圈
        {
            panel.openpicturetest9(); //show picture
            locationbig = 9;
            for (int i = 0; i < 38; i++)
            {
                locationsmall = i;// 小題題號
                //textBox1.AppendText("迴圈開始" + Convert.ToString(i) + " \r\n");//debug
                cnatgo = 0;
                //紀錄:測驗??  的第i 小題
                cnatmove2(delaycnat[locationbig][i], lighttime, waitaction, lightcnat[locationbig][i]);
                while (cnatgo == 0)//要進下一輪
                {
                    Application.DoEvents();
                }
                //先存這輪的資料
                if (press == 1) //沒有按: result 存入-1  有按存時間
                {
                    resulttime[locationbig][locationsmall] = timelength;
                    press = 0;
                }
                else
                {
                    resulttime[locationbig][locationsmall] = -1;
                }
                //判斷成功

                textBox1.AppendText("測驗9題號" + Convert.ToString(i) + "  " + Convert.ToString(resulttime[locationbig][i]) + " \r\n");//debug

            }

            //判斷拉出來




            textBox1.AppendText("測驗9迴圈結束" + " \r\n");//debug
            textBox1.AppendText("測驗9成功數量" + cnatsuccess_9 + " \r\n");//debug
            //textBox1.AppendText("測驗9左視野成功數量" + cnatsuccess9_left + " \r\n");//debug
            //textBox1.AppendText("測驗9右視野成功數量" + cnatsuccess9_right + " \r\n");//debug
            //savedata3();//做完測驗存檔
            savedatacnatbig();
            cnat9_3();
        }

        public void cnat9_3()  //cnat 8  結束畫面   
        {
            textBox2.AppendText("cnat 測驗9完畢 \r\n");
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("cnat 結束", "OK", "", "", "");


        }


        //delay01 持續注意時間     lighttime 亮燈時間   waitaction 熄燈後等待時間  lightnum 要亮的燈號  go 代表繼續 1 才動
        //暫時使用timer5 timer6
        //先不儲存  能動 
        public void cnatmove1(int delay01, int lighttime, int waitaction, int lightnum)//有按鍵就使用  用以同步燈號面板 不紀錄     
        {
            timer5.Interval = delay01;
            timer6.Interval = lighttime;
            timer7.Interval = waitaction;
            timer8.Interval = 1000;
            changelightnum(lightnum);
            playaudiocnat1();//聲音
            textBox1.AppendText("燈號:  " + Convert.ToString(lightnum) + " \r\n");//debug

            //存檔之後在寫
            timer5.Enabled = true;
            //要可以收資料並記錄
        }

        int locationbig;//大題題號
        int locationsmall;//小題題號
        int beeptimetick; //聲響時之tick數(MS)  
        int presstimetick; //按下時之tick數(MS)  
        int timelength;
        int recordpress = 0; //等於  getdata 收資料  等於0 不收   聲響之後等於big  timer7結束等於0
        int press = 0; // 只有recordpress 不等於0  有按-> 等於true 沒按 等於false


        public void cnatmove2(int delay01, int lighttime, int waitaction, int lightnum)//有按鍵就使用  用以同步燈號面板 要紀錄     
        {
            timer5.Interval = delay01;
            timer6.Interval = lighttime;
            timer7.Interval = waitaction;
            timer8.Interval = 1000;
            playaudiocnat1();//聲音
            beeptimetick = System.Environment.TickCount;   //紀錄聲音時間
            recordpress = locationbig; //第一題收資

            changelightnum(lightnum);//變燈
            textBox1.AppendText("燈號:  " + Convert.ToString(lightnum) + " \r\n");//debug
            timer5.Enabled = true;

        }



        public void cnatmove3(int delay01, int lighttime, int waitaction, int lightnum)// test 7 專用  等於 cnatmove2  不逼+所有timer關掉     
        {
            timer5.Enabled = false;
            timer6.Enabled = false;
            timer7.Enabled = false;
            timer8.Enabled = false;
            recordpress = 0;
            //cnatgo = 1;
            timer5.Interval = delay01;
            timer6.Interval = lighttime;
            timer7.Interval = waitaction;
            timer8.Interval = 1000;
            //playaudiocnat1();//聲音
            beeptimetick = System.Environment.TickCount;   //紀錄聲音時間
            recordpress = locationbig; //第一題收資

            changelightnum(lightnum);//變燈
            textBox1.AppendText("燈號:  " + Convert.ToString(lightnum) + " \r\n");//debug
            timer5.Enabled = true;

        }






        private void Timer5_Tick(object sender, EventArgs e) //時間到了要開燈
        {
            //textBox1.AppendText("timer5 end" + " \r\n");//debug
            timer5.Enabled = false;
            turnonlight();//開燈
            timer6.Enabled = true;
        }

        private void changelightnum(int lightnum)  //把數字轉成redlight[]+greenlight[]  //把該位置0->1  //等於點亮
        {

            //綠燈42個  紅燈38個  兩個一起 36+2個
            //42+36+38+2=116+2
            //lightcnat[測驗幾號][燈號]=
            //燈號0~41有42個  代表綠燈1~42
            //燈號42~79有38個  代表紅燈1~38
            //燈號80~115有36個  代表紅綠燈1~36號一起亮
            //燈號116~117有2個  代表上面兩個燈號
            if (lightnum <= 41)//0~41 綠燈
            {
                greenlight[lightnum + 1] = 1;
            }
            if (lightnum >= 42 && lightnum <= 79) //42~79 紅燈
            {
                redlight[lightnum - 41] = 1;
            }
            if (lightnum >= 80 && lightnum <= 115) //80~115 兩燈
            {
                greenlight[lightnum - 80] = 1;
                redlight[lightnum - 80] = 1;
            }
            if (lightnum == 116) //117
            {
                greenlight[39] = 1;
                redlight[37] = 1;
            }
            if (lightnum == 117)//118
            {
                greenlight[40] = 1;
                redlight[38] = 1;
            }
            else { }
        }

        private void changelightnum2(int lightnum)  //把數字轉成redlight[]+greenlight[]  //是該位置1->0   //等於關燈
        {

            //綠燈42個  紅燈38個  兩個一起 36+2個
            //42+36+38+2=116+2
            //lightcnat[測驗幾號][燈號]=
            //燈號0~41有42個  代表綠燈1~42
            //燈號42~79有38個  代表紅燈1~38
            //燈號80~115有36個  代表紅綠燈1~36號一起亮
            //燈號116~117有2個  代表上面兩個燈號
            if (lightnum <= 41)//0~41 綠燈
            {
                greenlight[lightnum + 1] = 0;
            }
            if (lightnum >= 42 && lightnum <= 79) //42~79 紅燈
            {
                redlight[lightnum - 41] = 0;
            }
            if (lightnum >= 80 && lightnum <= 115) //80~115 兩燈
            {
                greenlight[lightnum - 80] = 0;
                redlight[lightnum - 80] = 0;
            }
            if (lightnum == 116) //116
            {
                greenlight[39] = 0;
                redlight[37] = 0;
            }
            if (lightnum == 117)//117
            {
                greenlight[40] = 0;
                redlight[38] = 0;
            }
            else { }
        }

        private void turnofflight()  //所有燈號改成關閉後同步
        {
            //燈號變動
            for (int i = 1; i < 43; i++)
            {
                greenlight[i] = 0;
            }
            for (int i = 1; i < 39; i++)
            {
                redlight[i] = 0;
            }
            panel.Lightchange();//呼叫使用燈號
            //textBox1.AppendText("關燈" + " \r\n");//debug

        }

        private void Button11_Click(object sender, EventArgs e)
        {
            buttoninput = "2";
            AddData2();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            buttoninput = "3";
            AddData2();
        }

        private void turnonlight()  //把燈號同步
        {
            //燈號之類
            panel.Lightchange();//呼叫使用燈號
        }

        private void Timer6_Tick(object sender, EventArgs e) //燈亮完了要關燈
        {
            timer6.Enabled = false;
            //textBox1.AppendText("timer6 end" + " \r\n");//debug
            turnofflight();
            timer7.Enabled = true;
        }

        private void Timer7_Tick(object sender, EventArgs e)// 燈熄了 等反應時間結束
        {
            //textBox1.AppendText("timer7 end" + " \r\n");//debug
            timer7.Enabled = false;
            recordpress = 0; //時間到 不收資料
            //停止該題收資料
            //休息1秒
            timer8.Enabled = true;


        }

        private void Timer8_Tick(object sender, EventArgs e)//等1秒完，進下一
        {
            //textBox1.AppendText("timer8 end" + " \r\n");//debug
            timer8.Enabled = false;
            //進入下一題呵
            cnatgo = 1;
        }


        private void Button8_Click(object sender, EventArgs e)
        {
            debugcnmt_show();
        }


        //= debug 
        //    以下是cnat debug用程式
        private void Button9_Click(object sender, EventArgs e)  // all debug
        {
            debugcnat_show();

        }
        private void debugcnat_show()//debug面板
        {
            Form5_debugcnat debugcnat = new Form5_debugcnat(); //創建子視窗
            debugcnat.Owner = this;//要有這個不然不能傳
            debugcnat.Visible = true;//顯示第二個視窗
            //id.FormClosed += new FormClosedEventHandler(id_Closed);
        }

        private void debugcnmt_show()//debug面板
        {
            Form6_debugcnmt debugcnmt = new Form6_debugcnmt(); //創建子視窗
            debugcnmt.Owner = this;//要有這個不然不能傳
            debugcnmt.Visible = true;//顯示第二個視窗
            //id.FormClosed += new FormClosedEventHandler(id_Closed);
        }



        public void showsmall(int big, int small)   //跑小題
        {
            int delay, ltime, wait, lightn;
            delay = delaycnat[big][small];
            ltime = lighttime;
            wait = waitaction;
            lightn = lightcnat[big][small];

            //cnatmove1(delaycnatpractice[locationbig][i], lighttime, waitaction, lightcnatpractice[locationbig][i]);
            cnatmove1(delay, ltime, wait, lightn);
        }

        public void showpic6()
        {
            panel.openpicturetest6();
        }
        public void showpic9()
        {
            panel.openpicturetest9();
        }

        public void close69()
        {
            panel.closepicturetest6();
        }

        public void waitaction3500()
        {
            waitaction = 3500;
            textBox1.AppendText("waitaction =  " + Convert.ToString(waitaction) + " \r\n");//debug

        }

        public void waitaction1500()
        {
            waitaction = 1500;
            textBox1.AppendText("waitaction =  " + Convert.ToString(waitaction) + " \r\n");//debug
        }

        public void waitaction1234success()
        {
            cnatsuccess_1 = 18;
            cnatsuccess_2 = 18;
            cnatsuccess_3 = 18;
            cnatsuccess_4 = 18;
            textBox1.AppendText("Cnat1~4 = 18 " + " \r\n");//debug
        }

        public void waitaction1234wrong()
        {
            cnatsuccess_1 = 0;
            cnatsuccess_2 = 0;
            cnatsuccess_3 = 0;
            cnatsuccess_4 = 0;
            textBox1.AppendText("Cnat1~4 = 0 " + " \r\n");//debug
        }

        public void onefourredone()
        {
            onefourredo = 1;
            textBox1.AppendText("onefourredo = " + Convert.ToString(onefourredo) + " \r\n");//debug
        }

        public void onefournotredoyet()
        {
            onefourredo = 0;
            textBox1.AppendText("onefourredo = " + Convert.ToString(onefourredo) + " \r\n");//debug
        }

        public void lighttime1500()
        {
            lighttime = 1500;
            textBox1.AppendText("lighttime =  " + Convert.ToString(lighttime) + " \r\n");//debug
        }

        public void lighttime3000()
        {
            lighttime = 3000;
            textBox1.AppendText("lighttime =  " + Convert.ToString(lighttime) + " \r\n");//debug
        }

        public void addtest9()
        {
            dotest9 = 1;
            textBox1.AppendText("dotest9 =" + Convert.ToString(dotest9) + " \r\n");//debug
        }

        public void notest9()
        {
            dotest9 = 0;
            textBox1.AppendText("dotest9 =" + Convert.ToString(dotest9) + " \r\n");//debug
        }


        //cnmt 程式  -------------------------------------------------------------------------------------------------------------------------------------------------------------------




        int buttoncnmt = 0; //0= 沒有, 123456對應按鍵
        //int[] cnmtpressarray = new int[] { 0, 0, 0, 0, 0, 0 }; //因為最多判定一組6個，放這
        int cnmtlocationbig = 0; // 0練習    12345678    大題
        int cnmtstage = 0; //4個階段
        int cnmtgetdata = 0;// =1  receive data  =0; do nothing


        int[,,] cnmtrightresult = new int[,,]    //應該按對的    cnmtright[大題   0~8][4個階段，12相同所以剩下3   0~2][每組6個  0~5]
        {
            {{ 2,1,5,3,4,6 },{ 2,1,5,3,4,6 }, { 1,2,4,6,3,5 } ,{ 1,3,5,0,0,0 }}, //P
            {{ 4,3,5,2,6,1 },{ 4,3,5,2,6,1 }, { 6,2,5,1,4,3 } ,{ 2,4,6,0,0,0 }},  //1
            {{ 2,4,5,6,1,3 },{ 2,4,5,6,1,3 }, { 1,3,5,2,6,4 } ,{ 1,5,6,0,0,0 }},  //2
            {{ 3,1,5,4,2,6 },{ 3,1,5,4,2,6 }, { 6,3,5,1,4,2 } ,{ 3,4,6,0,0,0 }},  //3
            {{ 5,2,4,3,1,6 },{ 5,2,4,3,1,6 }, { 3,1,2,6,5,4 } ,{ 1,2,5,0,0,0 }},  //4
            {{ 6,5,3,2,1,4 },{ 6,5,3,2,1,4 }, { 3,6,4,5,2,1 } ,{ 1,3,4,0,0,0 }},  //5
            {{ 3,4,2,1,5,6 },{ 3,4,2,1,5,6 }, { 5,4,1,6,2,3 } ,{ 2,3,4,0,0,0 }},  //6
            {{ 2,6,1,3,5,4 },{ 2,6,1,3,5,4 }, { 1,3,6,5,2,4 } ,{ 3,5,6,0,0,0 }},  //7
            {{ 5,6,1,4,2,3 },{ 5,6,1,4,2,3 }, { 4,3,1,6,5,2 } ,{ 2,4,5,0,0,0 }}   //8

        };



        /*int[ , , ] cnmtright = new int[8,3,6]
         {
              { 2,1,5,3,4,6}, {1,2,3,6,3,5 }, { 1,3,5,0,0,0} },
         };      */

        int cnmtresulttimesmall = 0;    //計小題題號

        int[][] cnmtresulttime = new int[][] //cnmt結果存這   單位: MS  //長度640  應該是夠用了
        {
            new int[640]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },//0
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },//1
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },//2
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },//3
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },//4
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },//5
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},//6
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },//7
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },//8
        };  //cnmt結果存這   單位: MS

        int cnmtresultpresssmall = 0;   //計小題題號

        int[][] cnmtresultpress = new int[][] //cnmt結果存這   單位: 123456
        {
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },//0
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },//1
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},//2
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},//3
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},//4
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },//5
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },//6
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },//7
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },//8
        };  //cnmt結果存這   單位: 123456


        int[,,,] cnmtresultpress2 = new int[9,4,12,6] //cnmt結果存這   單位: 123456   [大題][ABCD][第幾輪][按哪個//6個按鍵沒按是0]
        {
        { { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } } },

        { { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } } },

        { { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } } },

        { { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } } },

        { { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } } },

        { { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } } },

        { { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } } },

        { { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } } },

        { { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } } },
        };


        int[,,,] cnmtresultime2 = new int[9, 4, 12, 6] //cnmt結果存這   單位: 123456   [大題][ABCD][第幾輪][按哪個//6個按鍵沒按是0]
        {
        { { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } } },

        { { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } } },

        { { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } } },

        { { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } } },

        { { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } } },

        { { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } } },

        { { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } } },

        { { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } } },

        { { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 } } },
        };

        int[,,,,] cnmtresultpress3 = new int[9, 4, 11, 6, 6] // 9,4,11,6,6// [大題][ABCD][第幾輪][按哪個//6個按鍵沒按是0][6]
        {

              { 
                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, }, } ,

                            {
                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, }, } ,

                                          {
                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, }, } ,

                                                        {
                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, }, } ,

                                                                      {
                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, }, } ,

                                                                                    {
                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, }, } ,

                                                                                                  {
                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, }, } ,

                                                                                                                {
                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, }, } ,

                                                                                                                              {
                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, }, } ,




        }; //[9大題][第幾階段][嘗試第起次][次嘗試最多要按6次][當次情況]


        int[,,,,] cnmtresulttime3 = new int[9, 4, 11, 6, 6] // 9,4,11,6,6// [大題][ABCD][第幾輪][按哪個//6個按鍵沒按是0][6]
        {

              {
                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, }, } ,

                            {
                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, }, } ,

                                          {
                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, }, } ,

                                                        {
                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, }, } ,

                                                                      {
                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, }, } ,

                                                                                    {
                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, }, } ,

                                                                                                  {
                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, }, } ,

                                                                                                                {
                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, }, } ,

                                                                                                                              {
                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, },

                {   { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } },
                    { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 },{ 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }, }, } ,
        };


        public void savedatacnmtbig()  //每一大題存一次   //CNMT存到TXT
        {
            DirectoryInfo dir = new DirectoryInfo(System.Windows.Forms.Application.StartupPath);
            string savelocation1 = dir.Parent.Parent.Parent.FullName;
            savelocation1 = savelocation1 + "\\Record\\" + d1 + "_" + d7;  // 編碼+日期
            System.IO.Directory.CreateDirectory(savelocation1);//新資料夾  名稱為編碼+日期


            string savelocation2 = savelocation1 + "\\";//存到資料夾下面
            string filename1 = savelocation2 + "\\" + d1 + "_" + d7 + "_" + "cnmttest_" + cnmtlocationbig + ".txt";//檔名

            savelabel1:
            if (System.IO.File.Exists(filename1))//如果有重複名字，加上txt做為區別
            {
                filename1 = filename1 + ".txt";
                goto savelabel1;
            }

            StreamWriter sw = new StreamWriter(filename1, true);      //建立txt  //true等於繼續加不會洗掉   


            sw.WriteLine("再認分數(retained recognition):" + cnmtagainrightcount[cnmtlocationbig]);

            //暫存各數據用
            int[] ok_1 = new int[] { 0, 0, 0 };//stage 123
            int[] ok_2 = new int[] { 0, 0, 0 };//stage 123
            int[] cc = new int[] { 0, 0, 0 };//stage 123
            int[] clsq = new int[] { 0, 0, 0 };//stage 123
            int temp = 0;

            //stage0 ok1
            for (int i = 0; i < 10; i++)
            {
                if (cnmtresultpress3[cnmtlocationbig, 0, i, 5, 5] != 0)
                {
                    ok_1[0] = i + 1;
                }
            }
            if (ok_1[0] == 0)
            {
                sw.WriteLine("StageA OK-ONE:"+ ok_1[0]+"  NC");
            }
            else
            {
                sw.WriteLine("StageA OK-ONE:" + ok_1[0]);
            }

            for (int i = 0; i < 5; i++)
            {
                if (cnmtresultpress3[cnmtlocationbig, 1, i, 5, 5] != 0)
                {
                    ok_1[1] = i + 1;
                }
            }
            if (ok_1[1] == 0)
            {
                sw.WriteLine("StageB OK-ONE:" + ok_1[1] + "  NC");
            }
            else
            {
                sw.WriteLine("StageB OK-ONE:" + ok_1[1]);
            }

            for (int i = 0; i < 5; i++)
            {
                if (cnmtresultpress3[cnmtlocationbig, 2, i, 5, 5] != 0)
                {
                    ok_1[2] = i + 1;
                }
            }
            if (ok_1[2] == 0)
            {
                sw.WriteLine("StageC OK-ONE:" + ok_1[2] + "  NC");
            }
            else
            {
                sw.WriteLine("StageC OK-ONE:" + ok_1[2]);
            }




            //OK2
            for (int i = 0; i < 11; i++)
            {
                if (cnmtresultpress3[cnmtlocationbig, 0, i, 0, 5] != 0)
                {
                    ok_2[0] = i + 1;
                }
            }
            if (ok_2[0] == 0)
            {
                sw.WriteLine("StageA OK-TWO:" + ok_2[0] + "  NC");
            } else
            { 
             sw.WriteLine("StageA OK-TWO:" + ok_2[0]);
            }

            for (int i = 0; i < 6; i++)
            {
                if (cnmtresultpress3[cnmtlocationbig, 1, i, 0, 5] != 0)
                {
                    ok_2[1] = i + 1;
                }
            }
            if (ok_2[1] == 0)
            {
                sw.WriteLine("StageB OK-TWO:" + ok_2[1] + "  NC");
            }
            else
            {
                sw.WriteLine("StageB OK-TWO:" + ok_2[1]);
            }

            for (int i = 0; i < 6; i++)
            {
                if (cnmtresultpress3[cnmtlocationbig, 2, i, 0, 5] != 0)
                {
                    ok_2[2] = i + 1;
                }
            }
            if (ok_2[2] == 0)
            {
                sw.WriteLine("StageC OK-TWO:" + ok_2[2] + "  NC");
            }
            else
            {
                sw.WriteLine("StageC OK-TWO:" + ok_2[2]);
            }






            //CC
            cc[0] = 11 - ok_2[0] + ok_1[0];
            sw.WriteLine("StageA CC:" + cc[0]);
            cc[1] = 11 - ok_2[1] + ok_1[1];
            sw.WriteLine("StageB CC:" + cc[1]);
            cc[2] = 11 - ok_2[2] + ok_1[2];
            sw.WriteLine("StageC CC:" + cc[2]);

            //CLSQ
            if (ok_1[0] == 0)
            { clsq[0] = 0; }
            else
            {
                clsq[0] = 6 * (11 - ok_1[0]);
                for (int i = 0; i < ok_1[0] - 1; i++)
                {
                    for (int j = 5; j >= 0; j--)
                    {
                        if (cnmtresultpress3[cnmtlocationbig, 0, i, j, j] != 0)
                        {
                            clsq[0] = clsq[0] + j;
                            break;
                        }
                    }
                }
            }
            //clsq[0] = 6 * (11 - ok_1[0]); 
            sw.WriteLine("StageA CL:" + clsq[0]);

            if (ok_1[1] == 0)
            { clsq[1] = 0; }
            else
            {
                clsq[1] = 6 * (6 - ok_1[1]);
                for (int i = 0; i < ok_1[1] - 1; i++)
                {
                    for (int j = 5; j >= 0; j--)
                    {
                        if (cnmtresultpress3[cnmtlocationbig, 1, i, j, j] != 0)
                        {
                            clsq[1] = clsq[1] + j;
                            break;
                        }
                    }
                }
            }
            //clsq[0] = 6 * (11 - ok_1[0]); 
            sw.WriteLine("StageB CS:" + clsq[1]);

            if (ok_1[2] == 0)
            { clsq[2] = 0; }
            else
            {
                clsq[2] = 6 * (6 - ok_1[2]);
                for (int i = 0; i < ok_1[2] - 1; i++)
                {
                    for (int j = 5; j >= 0; j--)
                    {
                        if (cnmtresultpress3[cnmtlocationbig, 2, i, j, j] != 0)
                        {
                            clsq[2] = clsq[2] + j;
                            break;
                        }
                    }
                }
            }
            //clsq[0] = 6 * (11 - ok_1[0]); 
            sw.WriteLine("StageC CQ:" + clsq[2]);







            sw.WriteLine("過程記錄");
            //按鍵成果
            for (int cnmtstage = 0; cnmtstage < 4; cnmtstage++)
          {
                sw.WriteLine("cnmtstage:" + cnmtstage);
                for (int cnmt_count = 0; cnmt_count < 11; cnmt_count++)
                {
                    if (cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_count, 0, 0] != 0)//把0去掉
                    {
                        sw.WriteLine("cnmt_trytime:" + (cnmt_count));
                        for (int j = 0; j < 6; j++)
                        {
                                for (int i = 0; i < 6; i++)
                                {
                                    sw.Write("[" + cnmtrightresult[cnmtlocationbig, cnmtstage,i] + "," + cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_count, j, i] + "," + cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_count, j, i] + "], ");
                                }
                                sw.WriteLine(" "); 
                        }
                        sw.WriteLine(" ");
                    }
                }
          }

            textBox1.AppendText("存檔" + " \r\n");//debug

            sw.Flush();               //清空緩衝區    
            sw.Close();     //關閉流
        }


        public void cnmtbutton1press()
        {
            if (cnmtgetdata == 1)
            {
                buttoncnmt = 1;
                cnmtgo = 1;
                AddData3();
                cnmtbuttonlight1();//關燈
            }
            if (cnmtleftrightlighton == 1)//測左右手 亮起來
            {
                AddData3();
                cnmtbuttonlight1();//關燈
                lightbotton6();
            }
        }      //這一堆都是在 cnmtgetdata=1 的前提，更改buttoncnmt

        public void cnmtbutton2press()
        {
            if (cnmtgetdata == 1)
            {
                buttoncnmt = 2;
                cnmtgo = 1;
                AddData3();
                cnmtbuttonlight2();
            }
            if (cnmtleftrightlighton == 1)//測左右手 亮起來
            {
                AddData3();
                cnmtbuttonlight1();//關燈
                lightbotton6();
            }
        }

        public void cnmtbutton3press()
        {
            if (cnmtgetdata == 1)
            {
                buttoncnmt = 3;
                cnmtgo = 1;
                AddData3();
                cnmtbuttonlight3();
            }
            if (cnmtleftrightlighton == 1)//測左右手 亮起來
            {
                AddData3();
                cnmtbuttonlight1();//關燈
                lightbotton6();
            }
        }

        public void cnmtbutton4press()
        {
            if (cnmtgetdata == 1)
            {
                buttoncnmt = 4;
                cnmtgo = 1;
                AddData3();
                cnmtbuttonlight4();
            }
            if (cnmtleftrightlighton == 1)//測左右手 亮起來
            {
                AddData3();
                cnmtbuttonlight1();//關燈
                lightbotton6();
            }
        }

        public void cnmtbutton5press()
        {
            if (cnmtgetdata == 1)
            {
                buttoncnmt = 5;
                cnmtgo = 1;
                AddData3();
                cnmtbuttonlight5();
            }
            if (cnmtleftrightlighton == 1)//測左右手 亮起來
            {
                AddData3();
                cnmtbuttonlight1();//關燈
                lightbotton6();
            }
        }

        public void cnmtbutton6press()
        {
            if (cnmtgetdata == 1)
            {
                buttoncnmt = 6;
                cnmtgo = 1;
                AddData3();
                cnmtbuttonlight6();
            }
            if (cnmtleftrightlighton == 1)//測左右手 亮起來
            {
                AddData3();
                cnmtbuttonlight1();//關燈
                lightbotton6();
            }
        }

        private void Button5_Click(object sender, EventArgs e)   //CNMT 開始
        {
            cnmttest();
        }

        private void Timer9_Tick(object sender, EventArgs e)      //測試反應手用   15秒跑完輸出結果    左手
        {
            timer9.Enabled = false;
            cnmtgetdata = 0;//現在開始 在panel的按鍵就會更改  buttoncnmt  //close
            cnmtleftrightpress = 0;//按下的按鍵會轉為數字紀錄        //close
            textBox1.AppendText("CNMT 左手數目時間到  次數" + Convert.ToString(cnmtlefttestpressnum) + "\r\n");
            cnmtleftrightlighton = 0;
            cnmtright0();
        }

   

        public void cnmtbuttonnotpress()     //   buttoncnmt = 0 規0  先放著
        {
            buttoncnmt = 0;
        }

        //int data3loc = 0;//要存到   cnmtpressarray[]的哪裡     在錯誤或下一題要規0

        int cnmtleftrightlighton = 0;//讓測試左右手時燈是亮的

        int cnmtleftrightpress = 0; //等於1代表要做左手鑑測驗  等於2代表右手

        int cnmtlefttestpressnum = 0; //左手按鍵次數
        int cnmtrighttestpressnum = 0;// 右手按鍵次數
        int cnmtsixstep = 0;//adddata3  正常使用=1

        int cnmtbeeptimetick;       //進入題號聲響計時
        int cnmtpresstimetick;      //按下按鈕聲響計時
        int cnmtticktime;           //時間差

        int donetwice = 0; // 1= 對一次  2=對2次
        int[][] cnmtwrongtime = new int[][]   //cnatwrongtime[locationbig][cnmtstage]
        {
            new int[] {0,0,0,0}, //每個分測驗錯幾次
            new int[] {0,0,0,0},
            new int[] {0,0,0,0},
            new int[] {0,0,0,0},
            new int[] {0,0,0,0},
            new int[] {0,0,0,0},
            new int[] {0,0,0,0},
            new int[] {0,0,0,0},
            new int[] {0,0,0,0},
        };

        int[] cnmtagainrightcount = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //再認之計分  一大提存    
        int[][] cnmtagain = new int[][]        //cnatagain[locationbig][]
        {   new int[] {0,0,0,0,0,0},
            new int[] {0,0,0,0,0,0,},
            new int[] {0,0,0,0,0,0,},
            new int[] {0,0,0,0,0,0,},
            new int[] {0,0,0,0,0,0,},
            new int[] {0,0,0,0,0,0,},
            new int[] {0,0,0,0,0,0,},
            new int[] {0,0,0,0,0,0,},
            new int[] {0,0,0,0,0,0,},
        };
        int[] cnmtagain2 = new int[] { 0, 0, 0 };


        int cnmtgo = 0; //等於1才繼續

        SoundPlayer audio = new SoundPlayer(CNAMT_sim.Properties.Resources.beep_07); // here WindowsFormsApplication1 is the namespace and Connect is the audio file name
        SoundPlayer audio2 = new SoundPlayer(CNAMT_sim.Properties.Resources.beep_01);
        SoundPlayer audio3 = new SoundPlayer(CNAMT_sim.Properties.Resources.beep_02);


        public void playaudiocnmt1() // 聲音播放 CNAT
        {
            audio.Play();
        }

        public void playaudiocnmtright() // 聲音播放 CNAT
        {
            audio2.Play();
            //sleep(100);
        }

        public void playaudiocnmtwrong() // 聲音播放 CNAT
        {
            audio3.Play();
            //sleep(100);
        }



        //CNMT 燈號上面6個---------CNMT 燈號上面6個---------CNMT 燈號上面6個---------CNMT 燈號上面6個---------CNMT 燈號上面6個---------CNMT 燈號上面6個---------

        public void light6()  //把CNMT6個燈號改亮
        {
            changelightnum(90 + 1);
            changelightnum(93 + 1);
            changelightnum(96 + 1);
            changelightnum(108 + 1);
            changelightnum(111 + 1);
            changelightnum(114 + 1);
            turnonlight();
        }

        public void cnmttoplight(int num)    //上面6個燈，亮一滅其他  輸入要亮的燈
        {
            if (num == 0)
                cnmtlight1();
            if (num == 1)
                cnmtlight2();
            if (num == 2)
                cnmtlight3();
            if (num == 3)
                cnmtlight4();
            if (num == 4)
                cnmtlight5();
            if (num == 5)
                cnmtlight6();
            if (num == 6)
                cnmtlight7();
        }

        public void cnmtlight1()  //上面6個燈，亮一滅其他
        {
            changelightnum(37 - 1);
            changelightnum2(38 - 1);
            changelightnum2(39 - 1);
            changelightnum2(40 - 1);
            changelightnum2(41 - 1);
            changelightnum2(42 - 1);
            turnonlight();
        }

        public void cnmtlight2()  //上面6個燈，亮一滅其他
        {
            changelightnum2(37 - 1);
            changelightnum(38 - 1);
            changelightnum2(39 - 1);
            changelightnum2(40 - 1);
            changelightnum2(41 - 1);
            changelightnum2(42 - 1);
            turnonlight();
        }

        public void cnmtlight3()  //上面6個燈，亮一滅其他
        {
            changelightnum2(37 - 1);
            changelightnum2(38 - 1);
            changelightnum(39 - 1);
            changelightnum2(40 - 1);
            changelightnum2(41 - 1);
            changelightnum2(42 - 1);
            turnonlight();
        }

        public void cnmtlight4()  //上面6個燈，亮一滅其他
        {
            changelightnum2(37 - 1);
            changelightnum2(38 - 1);
            changelightnum2(39 - 1);
            changelightnum(40 - 1);
            changelightnum2(41 - 1);
            changelightnum2(42 - 1);
            turnonlight();
        }

        public void cnmtlight5()  //上面6個燈，亮一滅其他
        {
            changelightnum2(37 - 1);
            changelightnum2(38 - 1);
            changelightnum2(39 - 1);
            changelightnum2(40 - 1);
            changelightnum(41 - 1);
            changelightnum2(42 - 1);
            turnonlight();
        }

        public void cnmtlight6()  //上面6個燈，亮一滅其他
        {
            changelightnum2(37 - 1);
            changelightnum2(38 - 1);
            changelightnum2(39 - 1);
            changelightnum2(40 - 1);
            changelightnum2(41 - 1);
            changelightnum(42 - 1);
            turnonlight();
        }
        public void cnmtlight7()  //上面6個燈，通通關掉
        {
            changelightnum2(37 - 1);
            changelightnum2(38 - 1);
            changelightnum2(39 - 1);
            changelightnum2(40 - 1);
            changelightnum2(41 - 1);
            changelightnum2(42 - 1);
            turnonlight();
        }



        //6個下面的燈
        public void lightbotton6()  //把CNMT6個燈號改亮
        {
            changelightnum(90 + 1);
            changelightnum(93 + 1);
            changelightnum(96 + 1);
            changelightnum(108 + 1);
            changelightnum(111 + 1);
            changelightnum(114 + 1);
            turnonlight();
        }

        //CNMT燈號下面6個  按了要把自己熄掉

        public void cnmtbuttonlight1()
        {
            changelightnum2(91);
            turnonlight();
        }

        public void cnmtbuttonlight2()
        {
            changelightnum2(94);
            turnonlight();
        }

        public void cnmtbuttonlight3()
        {
            changelightnum2(97);
            turnonlight();
        }

        public void cnmtbuttonlight4()
        {
            changelightnum2(109);
            turnonlight();
        }

        public void cnmtbuttonlight5()
        {
            changelightnum2(112);
            turnonlight();
        }

        public void cnmtbuttonlight6()
        {
            changelightnum2(115);
            turnonlight();
        }


        public void AddData3()//cnmt 收資料   
        {
            if (cnmtleftrightpress == 1)     //CNMT 左手
            {
                cnmtlefttestpressnum = cnmtlefttestpressnum + 1;
                playaudiocnmt1();
            }
            if (cnmtleftrightpress == 2)     //CNMT 右手
            {
                cnmtrighttestpressnum = cnmtrighttestpressnum + 1;
                playaudiocnmt1();
            }
            if (cnmtgetdata == 1)  //要收資料
            {
                if (cnmtsixstep == 1)
                {
                    cnmtpresstimetick = System.Environment.TickCount;
                    cnmtticktime = cnmtpresstimetick - cnmtbeeptimetick;
                }
            }
        }


        public void cnmttest() //cnmt 開始進入反應手
        {
            textBox2.AppendText("CNMT 測反應左手  \r\n");

            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行cnmt 測反應左手", "按下開始", "cnmtleft", "", "");//
        }

        public void cnmtleft()           //反應右手
        {
            lightbotton6();
            timer9.Interval = 15000;//15sec
            cnmtgetdata = 0;
            cnmtleftrightpress = 1;//按下的按鍵會轉為左手數字紀錄
            timer9.Enabled = true;// timer9開始
            cnmtleftrightlighton = 1;
        }

        public void cnmtright0() //cnmt 開始進入反應右手錢畫面
        {
            textBox2.AppendText("CNMT 測反應右手  \r\n");

            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行cnmt 測反應右手", "按下開始", "cnmtright", "", "");//
        }
        public void cnmtright()           //反應右手
        {
            lightbotton6();
            cnmtleftrightlighton = 1;
            timer10.Interval = 15000;//15sec
            cnmtgetdata = 0;
            cnmtleftrightpress = 2;//按下的按鍵會轉為右手數字紀錄
            timer10.Enabled = true;// timer10開始
        }



        private void Button6_Click(object sender, EventArgs e)
        {
            for (int i=0; i<20; i++)
            {
                textBox1.AppendText( Convert.ToString(resulttime[1][i]) + "  \r\n");
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 74; i++)
            {
                textBox1.AppendText(Convert.ToString(resulttime[7][i]) +"   "+ leftright[i] +"  \r\n");
            }
        }

        private void Timer10_Tick(object sender, EventArgs e)      //測試反應手用   15秒跑完輸出結果    右手
        {
            timer10.Enabled = false;
            cnmtgetdata = 0;//現在開始 在panel的按鍵就會更改  buttoncnmt  //close
            cnmtleftrightpress = 0;//按下的按鍵會轉為數字紀錄        //close
            textBox1.AppendText("CNMT 右手數目時間到  次數" + Convert.ToString(cnmtrighttestpressnum) + "\r\n");
            cnmtleftrightlighton = 0;
            cnmt0();
        }


        //-------------------------CNMTP----------------------------------------
        int [,]cnmt_trycount = new int[9,4] { {0,0,0,0}, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, }  ;
        int[] cnmt_trycount2 = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int cnmt0_trycount;


        public void cnmt0() // CNMT   起始畫面
        {
            cnmtlocationbig = 0;    //大題位置
            textBox2.AppendText("cnmt" + cnmtlocationbig + "\r\n");
            cnmtgo = 0;

            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            if (cnmtlefttestpressnum > cnmtrighttestpressnum)
            {
                pop.readfrom1("請使用左手進行測試", "cnmt 0 開始", "cnmt0_a", "", "");//
            }
            else
            {
                pop.readfrom1("請使用右手進行測試", "cnmt 0 開始", "cnmt0_a", "", "");//
            }

        }
        public void cnmt0_a()  //cnmt
        {
            cnmtlocationbig = 0;    //大題位置
            textBox1.AppendText("進行CNMT"+ cnmtlocationbig+"stage0" + " \r\n");
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進行CNMT練習題_學習", "cnmt練習題_學習開始", "cnmt0_a_2", "", "");//
        }

    
        public void cnmt0_a_2() //學習階段
        {
            panel.showcnmtpic();
            panel.cnmt0_1();   //換r階段圖片
            cnmtstage = 0;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;


            //cnmtlabel_0_2_a:
            cnmtlabel:

            //cnmtwrongtime
            if (cnmtwrongtime[cnmtlocationbig][cnmtstage] >= 10) //錯10次，跳D
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                //清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                cnmt0_d();
            }

            if (donetwice == 2)  //做對2次了
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                // 清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                //因為對2次，要進B步
                cnmt0_b();
            }

            if (donetwice == 1)
            {
                Thread.Sleep(300);
                lightbotton6(); //下面燈
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                for (int j = 0; j < 6; j++)
                {
                    cnmttoplight(5);     //上面的燈號鎖定
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;


                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig,cnmtstage], 0, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            playaudiocnmtright();
                            panel.show1();//debug
                            ////Thread.Sleep(150);
                            donetwice = donetwice + 1;
                            goto cnmtlabel;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音  
                        panel.show2();//debug
                        ////Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
            }//已經對一次


            for (int i = 1; i < 7; i++)  //要做 1 12 123 1234 12345 123456 6輪
            {
                //light6();//下面6個燈號都要亮起來
                Thread.Sleep(300);
                lightbotton6();
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                                                                   //playaudiocnmt1();//聲音   小題開始
                for (int j = 0; j < i; j++)
                {
                    cnmttoplight(i - 1);     //上面的燈號
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;

                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        textBox1.AppendText("j= " + Convert.ToString(j));


                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            donetwice = donetwice + 1;
                            cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音
                        panel.show2();//debug
                        ////Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
                playaudiocnmtright();
                panel.show1();//debug
                ////Thread.Sleep(150);
            }

            if (donetwice < 2)  //還不夠 對2次
            {
                goto cnmtlabel;
            }

        }

        public void cnmt0_b()  //空間回憶
        {
            textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗 進入空間回憶  \r\n");
            cnmtlocationbig = 0;    //大題位置
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進型CNMT 第0大題 空間回憶", "繼續空間回憶", "cnmt0_b_2", "", "");//

            //cnmt0_3();//
        }
        public void cnmt0_b_2() //學習階段
        {
            panel.showcnmtpic();
            panel.cnmt0_2();   //換r階段圖片
            cnmtstage = 1;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;


            //cnmtlabel_0_2_a:
            cnmtlabel:

            //cnmtwrongtime
            if (cnmtwrongtime[cnmtlocationbig][cnmtstage] >= 5) 
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗"+ Convert.ToString(cnmtstage) + "階段結束  \r\n");
                //清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                cnmt0_d();
            }

            if (donetwice == 2)  //做對2次了
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                // 清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                //因為對2次，要進B步
                cnmt0_c();
            }

            if (donetwice == 1)
            {
                Thread.Sleep(300);
                lightbotton6(); //下面燈
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                for (int j = 0; j < 6; j++)
                {
                    cnmttoplight(5);     //上面的燈號鎖定
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;


                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            playaudiocnmtright();
                            panel.show1();//debug
                            ////Thread.Sleep(150);
                            donetwice = donetwice + 1;
                            goto cnmtlabel;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音  
                        panel.show2();//debug
                        ////Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
            }//已經對一次


            for (int i = 1; i < 7; i++)  //要做 1 12 123 1234 12345 123456 6輪
            {
                //light6();//下面6個燈號都要亮起來

                Thread.Sleep(300);
                lightbotton6();
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                                                                   //playaudiocnmt1();//聲音   小題開始
                for (int j = 0; j < i; j++)
                {
                    cnmttoplight(i - 1);     //上面的燈號
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;

                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        textBox1.AppendText("j= " + Convert.ToString(j));


                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            donetwice = donetwice + 1;
                            cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
                playaudiocnmtright();
                panel.show1();//debug
                //Thread.Sleep(150);
            }

            if (donetwice < 2)  //還不夠 對2次
            {
                goto cnmtlabel;
            }

        }



        public void cnmt0_c()  //順序回憶
        {
            textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗 進入順序回憶  \r\n");
            cnmtlocationbig = 0;    //大題位置
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進型CNMT 第0大題 順序回憶", "順序回憶", "cnmt0_c_2", "", "");//
        }

        public void cnmt0_c_2() //學習階段
        {
            panel.showcnmtpic();
            panel.cnmt0_3();   //換r階段圖片
            cnmtstage = 2;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;


            //cnmtlabel_0_2_a:
            cnmtlabel:

            //cnmtwrongtime
            if (cnmtwrongtime[cnmtlocationbig][cnmtstage] >= 5)
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                //清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                cnmt0_d();
            }

            if (donetwice == 2)  //做對2次了
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                // 清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                //因為對2次，要進B步
                cnmt0_d();
            }

            if (donetwice == 1)
            {
                Thread.Sleep(300);
                lightbotton6(); //下面燈
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                for (int j = 0; j < 6; j++)
                {
                    cnmttoplight(5);     //上面的燈號鎖定
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;


                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            playaudiocnmtright();
                            panel.show1();//debug
                            //Thread.Sleep(150);
                            donetwice = donetwice + 1;
                            goto cnmtlabel;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音  
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
            }//已經對一次


            for (int i = 1; i < 7; i++)  //要做 1 12 123 1234 12345 123456 6輪
            {
                Thread.Sleep(300);
                //light6();//下面6個燈號都要亮起來
                lightbotton6();
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                                                                   //playaudiocnmt1();//聲音   小題開始
                for (int j = 0; j < i; j++)
                {
                    cnmttoplight(i - 1);     //上面的燈號
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;

                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        textBox1.AppendText("j= " + Convert.ToString(j));


                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            donetwice = donetwice + 1;
                            cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
                playaudiocnmtright();
                panel.show1();//debug
                //Thread.Sleep(150);
            }

            if (donetwice < 2)  //還不夠 對2次
            {
                goto cnmtlabel;
            }

        }

        public void cnmt0_d()  //再認
        {
            textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗 進入再認  \r\n");
            cnmtlocationbig = 0;    //大題位置
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進型CNMT 第0大題 再認", "繼續再認", "cnmt0_d_2", "", "");//
        }

        public void cnmt0_d_2()  //再認
        {
            panel.showcnmtpic();
            panel.cnmt0_4();   //換r階段圖片
            cnmtstage = 3;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;

            cnmtlabel:

            if (donetwice == 2)//做完2次
            {
                cnmt0_3();
            }
            //playaudiocnmt1();//聲音   小題開始
            cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
            lightbotton6(); //下面燈全亮

            for (int j = 0; j < 3; j++)    //只有按3次
            {
                cnmttoplight(6);     //上面的燈號通通關掉
                //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                playaudiocnmt1();//聲音   小題開始 

                while (cnmtgo == 0) //等待按鍵
                {
                    Application.DoEvents();
                }
                cnmtgo = 0;

                //存入戰存
                // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                cnmtresultpresssmall = cnmtresultpresssmall + 1;


                cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                cnmtresulttimesmall = cnmtresulttimesmall + 1;

                cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], donetwice, j] = buttoncnmt;
                cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage],  donetwice , j] = cnmtticktime;

                //看正確與否決定下一步
                //先把前3步存起來
                cnmtagain[cnmtlocationbig][j] = buttoncnmt;   //存放3個按鍵
                cnmtagain2[j] = buttoncnmt;   //存放3個按鍵
            }

        
            //現在cnatagain裡面有 3個按鍵來判斷吧
            Array.Sort(cnmtagain2);
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[0])+  " \r\n");
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[1]) + " \r\n");
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[2]) + " \r\n");
            // textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtrightresult[cnmtlocationbig, cnmtstage, 0]) + " \r\n");
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtrightresult[cnmtlocationbig, cnmtstage, 1]) + " \r\n");
            // textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtrightresult[cnmtlocationbig, cnmtstage, 2]) + " \r\n");
            int k = 0;
            for (int i = 0; i < 3; i++)
            {
                if (cnmtagain2[i] == cnmtrightresult[cnmtlocationbig, cnmtstage, i])
                {
                    k = k + 1;
                    textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[0]) + "nkvidnj=  " + cnmtrightresult[cnmtlocationbig, cnmtstage, i] + " \r\n");
                }
            }

            if (k == 3)
            {
                playaudiocnmtright();
                //Thread.Sleep(150);
            }
            else
            {
                playaudiocnmtwrong();
                //Thread.Sleep(150);
            }
            Thread.Sleep(300);
            cnmtagainrightcount[cnmtlocationbig] = cnmtagainrightcount[cnmtlocationbig] + k;

            donetwice = donetwice + 1;
            goto cnmtlabel;
        }


        public void cnmt0_3()
        {
            //存檔放這 

            //
            savedatacnmtbig();
            cnmtresulttimesmall = 0;
            cnmtresultpresssmall = 0;
            donetwice = 0;
            cnmtgetdata = 0;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 0; //第二步

            textBox2.AppendText("cnmt 練習題完畢 \r\n");
            //Zsavedata4();
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("進cnmt練習題或是進入正式測驗", "cnmt練習題", "cnmt0", "進入正式測驗", "cnmt1");//
        }


        //--------------------------------------CNMT1
        public void cnmt1() // CNMT   起始畫面
        {
            cnmtlocationbig = 1;    //大題位置
            textBox2.AppendText("cnmt"+ (cnmtlocationbig-1) + "結束 \r\n");
            cnmtgo = 0;

            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;

        
            pop.readfrom1(Convert.ToString("進行cnmt第"+ cnmtlocationbig +"測驗"), "開始", "cnmt1_a", "", "");//


        }
        public void cnmt1_a()  //cnmt
        {
            cnmtlocationbig = 1;    //大題位置
            textBox1.AppendText("進行CNMT" + cnmtlocationbig + "stage" + " \r\n");
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageA"), "開始", "cnmt1_a_2", "", "");//
        }
        public void cnmt1_a_2() //學習階段
        {
            panel.showcnmtpic();
            panel.cnmt1_1();   //換r階段圖片
            cnmtstage = 0;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;


            //cnmtlabel_0_2_a:
            cnmtlabel:

            //cnmtwrongtime
            if (cnmtwrongtime[cnmtlocationbig][cnmtstage] >= 10) //錯10次，跳D
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                //清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                cnmt1_d();
            }

            if (donetwice == 2)  //做對2次了
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                // 清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                //因為對2次，要進B步
                cnmt1_b();
            }

            if (donetwice == 1)
            {
                Thread.Sleep(300);
                lightbotton6(); //下面燈
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                for (int j = 0; j < 6; j++)
                {
                    cnmttoplight(5);     //上面的燈號鎖定
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;


                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            playaudiocnmtright();
                            panel.show1();//debug
                            //Thread.Sleep(150);
                            donetwice = donetwice + 1;
                            goto cnmtlabel;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音  
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
            }//已經對一次


            for (int i = 1; i < 7; i++)  //要做 1 12 123 1234 12345 123456 6輪
            {
                //light6();//下面6個燈號都要亮起來
                Thread.Sleep(300);
                lightbotton6();
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                                                                   //playaudiocnmt1();//聲音   小題開始
                for (int j = 0; j < i; j++)
                {
                    cnmttoplight(i - 1);     //上面的燈號
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;

                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        textBox1.AppendText("j= " + Convert.ToString(j));


                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            donetwice = donetwice + 1;
                            cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
                playaudiocnmtright();
                panel.show1();//debug
                //Thread.Sleep(150);
            }

            if (donetwice < 2)  //還不夠 對2次
            {
                goto cnmtlabel;
            }

        }
        public void cnmt1_b()  //空間回憶
        {
            textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗 進入空間回憶  \r\n");
            cnmtlocationbig = 1;    //大題位置
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageB"), "開始", "cnmt1_b_2", "", "");//

            //cnmt1_3();//
        }
        public void cnmt1_b_2() //學習階段
        {
            panel.showcnmtpic();
            panel.cnmt1_2();   //換r階段圖片
            cnmtstage = 1;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;


            //cnmtlabel_0_2_a:
            cnmtlabel:

            //cnmtwrongtime
            if (cnmtwrongtime[cnmtlocationbig][cnmtstage] >= 5)
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                //清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                cnmt1_d();
            }

            if (donetwice == 2)  //做對2次了
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                // 清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                //因為對2次，要進B步
                cnmt1_c();
            }

            if (donetwice == 1)
            {
                Thread.Sleep(300);
                lightbotton6(); //下面燈
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                for (int j = 0; j < 6; j++)
                {
                    cnmttoplight(5);     //上面的燈號鎖定
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;


                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            playaudiocnmtright();
                            panel.show1();//debug
                            //Thread.Sleep(150);
                            donetwice = donetwice + 1;
                            goto cnmtlabel;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音  
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
            }//已經對一次


            for (int i = 1; i < 7; i++)  //要做 1 12 123 1234 12345 123456 6輪
            {
                //light6();//下面6個燈號都要亮起來
                Thread.Sleep(300);
                lightbotton6();
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                                                                   //playaudiocnmt1();//聲音   小題開始
                for (int j = 0; j < i; j++)
                {
                    cnmttoplight(i - 1);     //上面的燈號
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;

                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        textBox1.AppendText("j= " + Convert.ToString(j));


                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            donetwice = donetwice + 1;
                            cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
                playaudiocnmtright();
                panel.show1();//debug
                //Thread.Sleep(150);
            }

            if (donetwice < 2)  //還不夠 對2次
            {
                goto cnmtlabel;
            }

        }
        public void cnmt1_c()  //順序回憶
        {
            textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗 進入順序回憶  \r\n");
            cnmtlocationbig = 1;    //大題位置
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageC"), "開始", "cnmt1_c_2", "", "");//
        }
        public void cnmt1_c_2() //學習階段
        {
            panel.showcnmtpic();
            panel.cnmt1_3();   //換r階段圖片
            cnmtstage = 2;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;


            //cnmtlabel_0_2_a:
            cnmtlabel:

            //cnmtwrongtime
            if (cnmtwrongtime[cnmtlocationbig][cnmtstage] >= 5)
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                //清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                cnmt1_d();
            }

            if (donetwice == 2)  //做對2次了
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                // 清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                //因為對2次，要進B步
                cnmt1_d();
            }

            if (donetwice == 1)
            {
                Thread.Sleep(300);
                lightbotton6(); //下面燈
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                for (int j = 0; j < 6; j++)
                {
                    cnmttoplight(5);     //上面的燈號鎖定
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;


                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            playaudiocnmtright();
                            panel.show1();//debug
                            //Thread.Sleep(150);
                            donetwice = donetwice + 1;
                            goto cnmtlabel;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音  
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
            }//已經對一次


            for (int i = 1; i < 7; i++)  //要做 1 12 123 1234 12345 123456 6輪
            {
                //light6();//下面6個燈號都要亮起來
                Thread.Sleep(300);
                lightbotton6();
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                                                                   //playaudiocnmt1();//聲音   小題開始
                for (int j = 0; j < i; j++)
                {
                    cnmttoplight(i - 1);     //上面的燈號
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;

                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        textBox1.AppendText("j= " + Convert.ToString(j));


                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            donetwice = donetwice + 1;
                            cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
                playaudiocnmtright();
                panel.show1();//debug
                //Thread.Sleep(150);
            }

            if (donetwice < 2)  //還不夠 對2次
            {
                goto cnmtlabel;
            }

        }
        public void cnmt1_d()  //再認
        {
            textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗 進入再認  \r\n");
            cnmtlocationbig = 1;    //大題位置
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageD"), "繼續再認", "cnmt1_d_2", "", "");//
        }
        public void cnmt1_d_2()  //再認
        {
            panel.showcnmtpic();
            panel.cnmt1_4();   //換r階段圖片
            cnmtstage = 3;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;

            cnmtlabel:

            if (donetwice == 2)//做完2次
            {
                cnmt1_3();
            }
            //playaudiocnmt1();//聲音   小題開始
            cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
            lightbotton6(); //下面燈全亮

            for (int j = 0; j < 3; j++)    //只有按3次
            {
                cnmttoplight(6);     //上面的燈號通通關掉
                //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                playaudiocnmt1();//聲音   小題開始 

                while (cnmtgo == 0) //等待按鍵
                {
                    Application.DoEvents();
                }
                cnmtgo = 0;

                //存入戰存
                // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                cnmtresultpresssmall = cnmtresultpresssmall + 1;


                cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                cnmtresulttimesmall = cnmtresulttimesmall + 1;

                cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], donetwice, j] = buttoncnmt;
                cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], donetwice, j] = cnmtticktime;

                //看正確與否決定下一步
                //先把前3步存起來
                cnmtagain[cnmtlocationbig][j] = buttoncnmt;   //存放3個按鍵
                cnmtagain2[j] = buttoncnmt;   //存放3個按鍵
            }


            //現在cnmtagain裡面有 3個按鍵來判斷吧
            Array.Sort(cnmtagain2);
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[0])+  " \r\n");
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[1]) + " \r\n");
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[2]) + " \r\n");
            // textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtrightresult[cnmtlocationbig, cnmtstage, 0]) + " \r\n");
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtrightresult[cnmtlocationbig, cnmtstage, 1]) + " \r\n");
            // textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtrightresult[cnmtlocationbig, cnmtstage, 2]) + " \r\n");
            int k = 0;
            for (int i = 0; i < 3; i++)
            {
                if (cnmtagain2[i] == cnmtrightresult[cnmtlocationbig, cnmtstage, i])
                {
                    k = k + 1;
                    //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[0]) + "nkvidnj=  " + cnmtrightresult[cnmtlocationbig, cnmtstage, i] + " \r\n");
                }
            }

            if (k == 3)
            {
                playaudiocnmtright();
                //Thread.Sleep(150);
            }
            else
            {
                playaudiocnmtwrong();
                //Thread.Sleep(150);
            }
            Thread.Sleep(300);
            cnmtagainrightcount[cnmtlocationbig] = cnmtagainrightcount[cnmtlocationbig] + k;

            donetwice = donetwice + 1;
            goto cnmtlabel;
        }
        public void cnmt1_3()
        {
            //存檔放這 

            //
            savedatacnmtbig();
            cnmtresulttimesmall = 0;
            cnmtresultpresssmall = 0;
            donetwice = 0;
            cnmtgetdata = 0;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 0; //第二步

            textBox2.AppendText("cnmt" + cnmtlocationbig + " \r\n");
            //Zsavedata4();
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt"+ (cnmtlocationbig+1) +"測驗"), "cnmt2", "cnmt2", "", "");//
        }

        //CNMT2---------------------
        public void cnmt2() // CNMT   起始畫面
        {
            cnmtlocationbig = 2;    //大題位置
            textBox2.AppendText("cnmt" + (cnmtlocationbig - 1) + "結束 \r\n");
            cnmtgo = 0;

            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;


            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗"), "開始", "cnmt2_a", "", "");//


        }
        public void cnmt2_a()  //cnmt
        {
            cnmtlocationbig = 2;    //大題位置
            textBox1.AppendText("進行CNMT" + cnmtlocationbig + "stage" + " \r\n");
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageA"), "開始", "cnmt2_a_2", "", "");//
        }
        public void cnmt2_a_2() //學習階段
        {
            panel.showcnmtpic();
            panel.cnmt2_1();   //換r階段圖片
            cnmtstage = 0;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;


            //cnmtlabel_0_2_a:
            cnmtlabel:

            //cnmtwrongtime
            if (cnmtwrongtime[cnmtlocationbig][cnmtstage] >= 10) //錯10次，跳D
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                //清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                cnmt2_d();
            }

            if (donetwice == 2)  //做對2次了
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                // 清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                //因為對2次，要進B步
                cnmt2_b();
            }

            if (donetwice == 1)
            {
                Thread.Sleep(300);
                lightbotton6(); //下面燈
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                for (int j = 0; j < 6; j++)
                {
                    cnmttoplight(5);     //上面的燈號鎖定
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;


                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            playaudiocnmtright();
                            panel.show1();//debug
                            //Thread.Sleep(150);
                            donetwice = donetwice + 1;
                            goto cnmtlabel;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音  
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
            }//已經對一次


            for (int i = 1; i < 7; i++)  //要做 1 12 123 1234 12345 123456 6輪
            {
                Thread.Sleep(300);
                lightbotton6();
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                                                                   //playaudiocnmt2();//聲音   小題開始
                for (int j = 0; j < i; j++)
                {
                    cnmttoplight(i - 1);     //上面的燈號
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;

                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        textBox1.AppendText("j= " + Convert.ToString(j));


                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            donetwice = donetwice + 1;
                            cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
                playaudiocnmtright();
                panel.show1();//debug
                //Thread.Sleep(150);
            }

            if (donetwice < 2)  //還不夠 對2次
            {
                goto cnmtlabel;
            }

        }
        public void cnmt2_b()  //空間回憶
        {
            textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗 進入空間回憶  \r\n");
            cnmtlocationbig = 2;    //大題位置
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageB"), "開始", "cnmt2_b_2", "", "");//

            //cnmt2_3();//
        }
        public void cnmt2_b_2() //學習階段
        {
            panel.showcnmtpic();
            panel.cnmt2_2();   //換r階段圖片
            cnmtstage = 1;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;


            //cnmtlabel_0_2_a:
            cnmtlabel:

            //cnmtwrongtime
            if (cnmtwrongtime[cnmtlocationbig][cnmtstage] >= 5)
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                //清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                cnmt2_d();
            }

            if (donetwice == 2)  //做對2次了
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                // 清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                //因為對2次，要進B步
                cnmt2_c();
            }

            if (donetwice == 1)
            {
                Thread.Sleep(300);
                lightbotton6(); //下面燈
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                for (int j = 0; j < 6; j++)
                {
                    cnmttoplight(5);     //上面的燈號鎖定
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;


                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            playaudiocnmtright();
                            panel.show1();//debug
                            //Thread.Sleep(150);
                            donetwice = donetwice + 1;
                            goto cnmtlabel;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音  
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
            }//已經對一次


            for (int i = 1; i < 7; i++)  //要做 1 12 123 1234 12345 123456 6輪
            {
                Thread.Sleep(300);
                lightbotton6();
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                                                                   //playaudiocnmt2();//聲音   小題開始
                for (int j = 0; j < i; j++)
                {
                    cnmttoplight(i - 1);     //上面的燈號
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;

                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        textBox1.AppendText("j= " + Convert.ToString(j));


                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            donetwice = donetwice + 1;
                            cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
                playaudiocnmtright();
                panel.show1();//debug
                //Thread.Sleep(150);
            }

            if (donetwice < 2)  //還不夠 對2次
            {
                goto cnmtlabel;
            }

        }
        public void cnmt2_c()  //順序回憶
        {
            textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗 進入順序回憶  \r\n");
            cnmtlocationbig = 2;    //大題位置
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageC"), "開始", "cnmt2_c_2", "", "");//
        }
        public void cnmt2_c_2() //學習階段
        {
            panel.showcnmtpic();
            panel.cnmt2_3();   //換r階段圖片
            cnmtstage = 2;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;


            //cnmtlabel_0_2_a:
            cnmtlabel:

            //cnmtwrongtime
            if (cnmtwrongtime[cnmtlocationbig][cnmtstage] >= 5)
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                //清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                cnmt2_d();
            }

            if (donetwice == 2)  //做對2次了
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                // 清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                //因為對2次，要進B步
                cnmt2_d();
            }

            if (donetwice == 1)
            {
                Thread.Sleep(300);
                lightbotton6(); //下面燈
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                for (int j = 0; j < 6; j++)
                {
                    cnmttoplight(5);     //上面的燈號鎖定
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;


                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            playaudiocnmtright();
                            panel.show1();//debug
                            //Thread.Sleep(150);
                            donetwice = donetwice + 1;
                            goto cnmtlabel;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音  
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
            }//已經對一次


            for (int i = 1; i < 7; i++)  //要做 1 12 123 1234 12345 123456 6輪
            {
                Thread.Sleep(300);
                lightbotton6();
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                                                                   //playaudiocnmt2();//聲音   小題開始
                for (int j = 0; j < i; j++)
                {
                    cnmttoplight(i - 1);     //上面的燈號
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;

                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        textBox1.AppendText("j= " + Convert.ToString(j));


                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            donetwice = donetwice + 1;
                            cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
                playaudiocnmtright();
                panel.show1();//debug
                //Thread.Sleep(150);
            }

            if (donetwice < 2)  //還不夠 對2次
            {
                goto cnmtlabel;
            }

        }
        public void cnmt2_d()  //再認
        {
            textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗 進入再認  \r\n");
            cnmtlocationbig = 2;    //大題位置
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageD"), "繼續再認", "cnmt2_d_2", "", "");//
        }
        public void cnmt2_d_2()  //再認
        {
            panel.showcnmtpic();
            panel.cnmt2_4();   //換r階段圖片
            cnmtstage = 3;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;

            cnmtlabel:

            if (donetwice == 2)//做完2次
            {
                cnmt2_3();
            }
            //playaudiocnmt2();//聲音   小題開始
            cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
            lightbotton6(); //下面燈全亮

            for (int j = 0; j < 3; j++)    //只有按3次
            {
                cnmttoplight(6);     //上面的燈號通通關掉
                //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                playaudiocnmt1();//聲音   小題開始 

                while (cnmtgo == 0) //等待按鍵
                {
                    Application.DoEvents();
                }
                cnmtgo = 0;

                //存入戰存
                // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                cnmtresultpresssmall = cnmtresultpresssmall + 1;


                cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                cnmtresulttimesmall = cnmtresulttimesmall + 1;

                cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], donetwice, j] = buttoncnmt;
                cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], donetwice, j] = cnmtticktime;

                //看正確與否決定下一步
                //先把前3步存起來
                cnmtagain[cnmtlocationbig][j] = buttoncnmt;   //存放3個按鍵
                cnmtagain2[j] = buttoncnmt;   //存放3個按鍵
            }


            //現在cnmtagain裡面有 3個按鍵來判斷吧
            Array.Sort(cnmtagain2);
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[0])+  " \r\n");
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[1]) + " \r\n");
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[2]) + " \r\n");
            // textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtrightresult[cnmtlocationbig, cnmtstage, 0]) + " \r\n");
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtrightresult[cnmtlocationbig, cnmtstage, 1]) + " \r\n");
            // textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtrightresult[cnmtlocationbig, cnmtstage, 2]) + " \r\n");
            int k = 0;
            for (int i = 0; i < 3; i++)
            {
                if (cnmtagain2[i] == cnmtrightresult[cnmtlocationbig, cnmtstage, i])
                {
                    k = k + 1;
                    //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[0]) + "nkvidnj=  " + cnmtrightresult[cnmtlocationbig, cnmtstage, i] + " \r\n");
                }
            }

            if (k == 3)
            {
                playaudiocnmtright();
                //Thread.Sleep(150);
            }
            else
            {
                playaudiocnmtwrong();
                //Thread.Sleep(150);
            }
            Thread.Sleep(300);
            cnmtagainrightcount[cnmtlocationbig] = cnmtagainrightcount[cnmtlocationbig] + k;

            donetwice = donetwice + 1;
            goto cnmtlabel;
        }
        public void cnmt2_3()
        {
            //存檔放這 

            //
            savedatacnmtbig();
            cnmtresulttimesmall = 0;
            cnmtresultpresssmall = 0;
            donetwice = 0;
            cnmtgetdata = 0;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 0; //第二步

            textBox2.AppendText("cnmt" + cnmtlocationbig + " \r\n");
            //Zsavedata4();
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt" + (cnmtlocationbig + 1) + "測驗"), "cnmt2", "cnmt2", "", "");//
        }

        //-CNMT3--------------------------------------------------
        public void cnmt3() // CNMT   起始畫面
        {
            cnmtlocationbig = 3;    //大題位置
            textBox2.AppendText("cnmt" + (cnmtlocationbig - 1) + "結束 \r\n");
            cnmtgo = 0;

            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;


            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗"), "開始", "cnmt3_a", "", "");//


        }
        public void cnmt3_a()  //cnmt
        {
            cnmtlocationbig = 3;    //大題位置
            textBox1.AppendText("進行CNMT" + cnmtlocationbig + "stage" + " \r\n");
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageA"), "開始", "cnmt3_a_2", "", "");//
        }
        public void cnmt3_a_2() //學習階段
        {
            panel.showcnmtpic();
            panel.cnmt3_1();   //換r階段圖片
            cnmtstage = 0;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;


            //cnmtlabel_0_2_a:
            cnmtlabel:

            //cnmtwrongtime
            if (cnmtwrongtime[cnmtlocationbig][cnmtstage] >= 10) //錯10次，跳D
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                //清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                cnmt3_d();
            }

            if (donetwice == 2)  //做對2次了
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                // 清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                //因為對2次，要進B步
                cnmt3_b();
            }

            if (donetwice == 1)
            {
                Thread.Sleep(300);
                lightbotton6(); //下面燈
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                for (int j = 0; j < 6; j++)
                {
                    cnmttoplight(5);     //上面的燈號鎖定
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;


                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            playaudiocnmtright();
                            panel.show1();//debug
                            //Thread.Sleep(150);
                            donetwice = donetwice + 1;
                            goto cnmtlabel;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音  
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
            }//已經對一次


            for (int i = 1; i < 7; i++)  //要做 1 12 123 1234 12345 123456 6輪
            {
                Thread.Sleep(300);
                lightbotton6();
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                                                                   //playaudiocnmt3();//聲音   小題開始
                for (int j = 0; j < i; j++)
                {
                    cnmttoplight(i - 1);     //上面的燈號
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;

                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        textBox1.AppendText("j= " + Convert.ToString(j));


                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            donetwice = donetwice + 1;
                            cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
                playaudiocnmtright();
                panel.show1();//debug
                //Thread.Sleep(150);
            }

            if (donetwice < 2)  //還不夠 對2次
            {
                goto cnmtlabel;
            }

        }
        public void cnmt3_b()  //空間回憶
        {
            textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗 進入空間回憶  \r\n");
            cnmtlocationbig = 3;    //大題位置
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageB"), "開始", "cnmt3_b_2", "", "");//

            //cnmt3_3();//
        }
        public void cnmt3_b_2() //學習階段
        {
            panel.showcnmtpic();
            panel.cnmt3_2();   //換r階段圖片
            cnmtstage = 1;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;


            //cnmtlabel_0_2_a:
            cnmtlabel:

            //cnmtwrongtime
            if (cnmtwrongtime[cnmtlocationbig][cnmtstage] >= 5)
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                //清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                cnmt3_d();
            }

            if (donetwice == 2)  //做對2次了
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                // 清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                //因為對2次，要進B步
                cnmt3_c();
            }

            if (donetwice == 1)
            {
                Thread.Sleep(300);
                lightbotton6(); //下面燈
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                for (int j = 0; j < 6; j++)
                {
                    cnmttoplight(5);     //上面的燈號鎖定
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;


                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            playaudiocnmtright();
                            panel.show1();//debug
                            //Thread.Sleep(150);
                            donetwice = donetwice + 1;
                            goto cnmtlabel;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音  
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
            }//已經對一次


            for (int i = 1; i < 7; i++)  //要做 1 12 123 1234 12345 123456 6輪
            {
                Thread.Sleep(300);
                lightbotton6();
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                                                                   //playaudiocnmt3();//聲音   小題開始
                for (int j = 0; j < i; j++)
                {
                    cnmttoplight(i - 1);     //上面的燈號
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;

                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        textBox1.AppendText("j= " + Convert.ToString(j));


                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            donetwice = donetwice + 1;
                            cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
                playaudiocnmtright();
                panel.show1();//debug
                //Thread.Sleep(150);
            }

            if (donetwice < 2)  //還不夠 對2次
            {
                goto cnmtlabel;
            }

        }
        public void cnmt3_c()  //順序回憶
        {
            textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗 進入順序回憶  \r\n");
            cnmtlocationbig = 3;    //大題位置
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageC"), "開始", "cnmt3_c_2", "", "");//
        }
        public void cnmt3_c_2() //學習階段
        {
            panel.showcnmtpic();
            panel.cnmt3_3();   //換r階段圖片
            cnmtstage = 2;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;


            //cnmtlabel_0_2_a:
            cnmtlabel:

            //cnmtwrongtime
            if (cnmtwrongtime[cnmtlocationbig][cnmtstage] >= 5)
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                //清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                cnmt3_d();
            }

            if (donetwice == 2)  //做對2次了
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                // 清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                //因為對2次，要進B步
                cnmt3_d();
            }

            if (donetwice == 1)
            {
                Thread.Sleep(300);
                lightbotton6(); //下面燈
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                for (int j = 0; j < 6; j++)
                {
                    cnmttoplight(5);     //上面的燈號鎖定
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;


                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            playaudiocnmtright();
                            panel.show1();//debug
                            //Thread.Sleep(150);
                            donetwice = donetwice + 1;
                            goto cnmtlabel;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音  
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
            }//已經對一次


            for (int i = 1; i < 7; i++)  //要做 1 12 123 1234 12345 123456 6輪
            {
                Thread.Sleep(300);
                lightbotton6();
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                                                                   //playaudiocnmt3();//聲音   小題開始
                for (int j = 0; j < i; j++)
                {
                    cnmttoplight(i - 1);     //上面的燈號
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;

                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        textBox1.AppendText("j= " + Convert.ToString(j));


                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            donetwice = donetwice + 1;
                            cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
                playaudiocnmtright();
                panel.show1();//debug
                //Thread.Sleep(150);
            }

            if (donetwice < 2)  //還不夠 對2次
            {
                goto cnmtlabel;
            }

        }
        public void cnmt3_d()  //再認
        {
            textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗 進入再認  \r\n");
            cnmtlocationbig = 3;    //大題位置
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageD"), "繼續再認", "cnmt3_d_2", "", "");//
        }
        public void cnmt3_d_2()  //再認
        {
            panel.showcnmtpic();
            panel.cnmt3_4();   //換r階段圖片
            cnmtstage = 3;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;

            cnmtlabel:

            if (donetwice == 2)//做完2次
            {
                cnmt3_3();
            }
            //playaudiocnmt3();//聲音   小題開始
            cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
            lightbotton6(); //下面燈全亮

            for (int j = 0; j < 3; j++)    //只有按3次
            {
                cnmttoplight(6);     //上面的燈號通通關掉
                //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                playaudiocnmt1();//聲音   小題開始 

                while (cnmtgo == 0) //等待按鍵
                {
                    Application.DoEvents();
                }
                cnmtgo = 0;

                //存入戰存
                // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                cnmtresultpresssmall = cnmtresultpresssmall + 1;


                cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                cnmtresulttimesmall = cnmtresulttimesmall + 1;

                cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], donetwice, j] = buttoncnmt;
                cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], donetwice, j] = cnmtticktime;

                //看正確與否決定下一步
                //先把前3步存起來
                cnmtagain[cnmtlocationbig][j] = buttoncnmt;   //存放3個按鍵
                cnmtagain2[j] = buttoncnmt;   //存放3個按鍵
            }


            //現在cnmtagain裡面有 3個按鍵來判斷吧
            Array.Sort(cnmtagain2);
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[0])+  " \r\n");
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[1]) + " \r\n");
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[2]) + " \r\n");
            // textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtrightresult[cnmtlocationbig, cnmtstage, 0]) + " \r\n");
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtrightresult[cnmtlocationbig, cnmtstage, 1]) + " \r\n");
            // textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtrightresult[cnmtlocationbig, cnmtstage, 2]) + " \r\n");
            int k = 0;
            for (int i = 0; i < 3; i++)
            {
                if (cnmtagain2[i] == cnmtrightresult[cnmtlocationbig, cnmtstage, i])
                {
                    k = k + 1;
                    //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[0]) + "nkvidnj=  " + cnmtrightresult[cnmtlocationbig, cnmtstage, i] + " \r\n");
                }
            }

            if (k == 3)
            {
                playaudiocnmtright();
                //Thread.Sleep(150);
            }
            else
            {
                playaudiocnmtwrong();
                //Thread.Sleep(150);
            }
            Thread.Sleep(300);
            cnmtagainrightcount[cnmtlocationbig] = cnmtagainrightcount[cnmtlocationbig] + k;

            donetwice = donetwice + 1;
            goto cnmtlabel;
        }
        public void cnmt3_3()
        {
            //存檔放這 

            //
            savedatacnmtbig();
            cnmtresulttimesmall = 0;
            cnmtresultpresssmall = 0;
            donetwice = 0;
            cnmtgetdata = 0;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 0; //第二步

            textBox2.AppendText("cnmt" + cnmtlocationbig + " \r\n");
            //Zsavedata4();
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt" + (cnmtlocationbig + 1) + "測驗"), "cnmt3", "cnmt3", "", "");//
        }


        //CNMT4------------------------------------------------
        public void cnmt4() // CNMT   起始畫面
        {
            cnmtlocationbig = 4;    //大題位置
            textBox2.AppendText("cnmt" + (cnmtlocationbig - 1) + "結束 \r\n");
            cnmtgo = 0;

            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;


            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗"), "開始", "cnmt4_a", "", "");//


        }
        public void cnmt4_a()  //cnmt
        {
            cnmtlocationbig = 4;    //大題位置
            textBox1.AppendText("進行CNMT" + cnmtlocationbig + "stage" + " \r\n");
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageA"), "開始", "cnmt4_a_2", "", "");//
        }
        public void cnmt4_a_2() //學習階段
        {
            panel.showcnmtpic();
            panel.cnmt4_1();   //換r階段圖片
            cnmtstage = 0;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;


            //cnmtlabel_0_2_a:
            cnmtlabel:

            //cnmtwrongtime
            if (cnmtwrongtime[cnmtlocationbig][cnmtstage] >= 10) //錯10次，跳D
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                //清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                cnmt4_d();
            }

            if (donetwice == 2)  //做對2次了
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                // 清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                //因為對2次，要進B步
                cnmt4_b();
            }

            if (donetwice == 1)
            {
                Thread.Sleep(300);
                lightbotton6(); //下面燈
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                for (int j = 0; j < 6; j++)
                {
                    cnmttoplight(5);     //上面的燈號鎖定
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;


                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            playaudiocnmtright();
                            panel.show1();//debug
                            //Thread.Sleep(150);
                            donetwice = donetwice + 1;
                            goto cnmtlabel;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音  
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
            }//已經對一次


            for (int i = 1; i < 7; i++)  //要做 1 12 123 1234 12345 123456 6輪
            {
                Thread.Sleep(300);
                lightbotton6();
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                                                                   //playaudiocnmt4();//聲音   小題開始
                for (int j = 0; j < i; j++)
                {
                    cnmttoplight(i - 1);     //上面的燈號
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;

                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        textBox1.AppendText("j= " + Convert.ToString(j));


                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            donetwice = donetwice + 1;
                            cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
                playaudiocnmtright();
                panel.show1();//debug
                //Thread.Sleep(150);
            }

            if (donetwice < 2)  //還不夠 對2次
            {
                goto cnmtlabel;
            }

        }
        public void cnmt4_b()  //空間回憶
        {
            textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗 進入空間回憶  \r\n");
            cnmtlocationbig = 4;    //大題位置
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageB"), "開始", "cnmt4_b_2", "", "");//

            //cnmt4_3();//
        }
        public void cnmt4_b_2() //學習階段
        {
            panel.showcnmtpic();
            panel.cnmt4_2();   //換r階段圖片
            cnmtstage = 1;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;


            //cnmtlabel_0_2_a:
            cnmtlabel:

            //cnmtwrongtime
            if (cnmtwrongtime[cnmtlocationbig][cnmtstage] >= 5)
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                //清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                cnmt4_d();
            }

            if (donetwice == 2)  //做對2次了
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                // 清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                //因為對2次，要進B步
                cnmt4_c();
            }

            if (donetwice == 1)
            {
                Thread.Sleep(300);
                lightbotton6(); //下面燈
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                for (int j = 0; j < 6; j++)
                {
                    cnmttoplight(5);     //上面的燈號鎖定
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;


                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            playaudiocnmtright();
                            panel.show1();//debug
                            //Thread.Sleep(150);
                            donetwice = donetwice + 1;
                            goto cnmtlabel;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音  
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
            }//已經對一次


            for (int i = 1; i < 7; i++)  //要做 1 12 123 1234 12345 123456 6輪
            {
                Thread.Sleep(300);
                lightbotton6();
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                                                                   //playaudiocnmt4();//聲音   小題開始
                for (int j = 0; j < i; j++)
                {
                    cnmttoplight(i - 1);     //上面的燈號
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;

                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        textBox1.AppendText("j= " + Convert.ToString(j));


                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            donetwice = donetwice + 1;
                            cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
                playaudiocnmtright();
                panel.show1();//debug
                //Thread.Sleep(150);
            }

            if (donetwice < 2)  //還不夠 對2次
            {
                goto cnmtlabel;
            }

        }
        public void cnmt4_c()  //順序回憶
        {
            textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗 進入順序回憶  \r\n");
            cnmtlocationbig = 4;    //大題位置
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageC"), "開始", "cnmt4_c_2", "", "");//
        }
        public void cnmt4_c_2() //學習階段
        {
            panel.showcnmtpic();
            panel.cnmt4_3();   //換r階段圖片
            cnmtstage = 2;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;


            //cnmtlabel_0_2_a:
            cnmtlabel:

            //cnmtwrongtime
            if (cnmtwrongtime[cnmtlocationbig][cnmtstage] >= 5)
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                //清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                cnmt4_d();
            }

            if (donetwice == 2)  //做對2次了
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                // 清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                //因為對2次，要進B步
                cnmt4_d();
            }

            if (donetwice == 1)
            {
                Thread.Sleep(300);
                lightbotton6(); //下面燈
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                for (int j = 0; j < 6; j++)
                {
                    cnmttoplight(5);     //上面的燈號鎖定
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;


                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            playaudiocnmtright();
                            panel.show1();//debug
                            //Thread.Sleep(150);
                            donetwice = donetwice + 1;
                            goto cnmtlabel;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音  
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
            }//已經對一次


            for (int i = 1; i < 7; i++)  //要做 1 12 123 1234 12345 123456 6輪
            {
                Thread.Sleep(300);
                lightbotton6();
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                                                                   //playaudiocnmt4();//聲音   小題開始
                for (int j = 0; j < i; j++)
                {
                    cnmttoplight(i - 1);     //上面的燈號
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;

                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        textBox1.AppendText("j= " + Convert.ToString(j));


                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            donetwice = donetwice + 1;
                            cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
                playaudiocnmtright();
                panel.show1();//debug
                //Thread.Sleep(150);
            }

            if (donetwice < 2)  //還不夠 對2次
            {
                goto cnmtlabel;
            }

        }
        public void cnmt4_d()  //再認
        {
            textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗 進入再認  \r\n");
            cnmtlocationbig = 4;    //大題位置
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageD"), "繼續再認", "cnmt4_d_2", "", "");//
        }
        public void cnmt4_d_2()  //再認
        {
            panel.showcnmtpic();
            panel.cnmt4_4();   //換r階段圖片
            cnmtstage = 3;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;

            cnmtlabel:

            if (donetwice == 2)//做完2次
            {
                cnmt4_3();
            }
            //playaudiocnmt4();//聲音   小題開始
            cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
            lightbotton6(); //下面燈全亮

            for (int j = 0; j < 3; j++)    //只有按3次
            {
                cnmttoplight(6);     //上面的燈號通通關掉
                //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                playaudiocnmt1();//聲音   小題開始 

                while (cnmtgo == 0) //等待按鍵
                {
                    Application.DoEvents();
                }
                cnmtgo = 0;

                //存入戰存
                // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                cnmtresultpresssmall = cnmtresultpresssmall + 1;


                cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                cnmtresulttimesmall = cnmtresulttimesmall + 1;

                cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], donetwice, j] = buttoncnmt;
                cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], donetwice, j] = cnmtticktime;

                //看正確與否決定下一步
                //先把前3步存起來
                cnmtagain[cnmtlocationbig][j] = buttoncnmt;   //存放3個按鍵
                cnmtagain2[j] = buttoncnmt;   //存放3個按鍵
            }


            //現在cnmtagain裡面有 3個按鍵來判斷吧
            Array.Sort(cnmtagain2);
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[0])+  " \r\n");
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[1]) + " \r\n");
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[2]) + " \r\n");
            // textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtrightresult[cnmtlocationbig, cnmtstage, 0]) + " \r\n");
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtrightresult[cnmtlocationbig, cnmtstage, 1]) + " \r\n");
            // textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtrightresult[cnmtlocationbig, cnmtstage, 2]) + " \r\n");
            int k = 0;
            for (int i = 0; i < 3; i++)
            {
                if (cnmtagain2[i] == cnmtrightresult[cnmtlocationbig, cnmtstage, i])
                {
                    k = k + 1;
                    //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[0]) + "nkvidnj=  " + cnmtrightresult[cnmtlocationbig, cnmtstage, i] + " \r\n");
                }
            }

            if (k == 3)
            {
                playaudiocnmtright();
                //Thread.Sleep(150);
            }
            else
            {
                playaudiocnmtwrong();
                //Thread.Sleep(150);
            }
            Thread.Sleep(300);
            cnmtagainrightcount[cnmtlocationbig] = cnmtagainrightcount[cnmtlocationbig] + k;

            donetwice = donetwice + 1;
            goto cnmtlabel;
        }
        public void cnmt4_3()
        {
            //存檔放這 

            //
            savedatacnmtbig();
            cnmtresulttimesmall = 0;
            cnmtresultpresssmall = 0;
            donetwice = 0;
            cnmtgetdata = 0;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 0; //第二步

            textBox2.AppendText("cnmt" + cnmtlocationbig + " \r\n");
            //Zsavedata4();
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt" + (cnmtlocationbig + 1) + "測驗"), "cnmt4", "cnmt4", "", "");//
        }

        //CNMT5---------------------------------------------------------
        public void cnmt5() // CNMT   起始畫面
        {
            cnmtlocationbig = 5;    //大題位置
            textBox2.AppendText("cnmt" + (cnmtlocationbig - 1) + "結束 \r\n");
            cnmtgo = 0;

            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;


            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗"), "開始", "cnmt5_a", "", "");//


        }
        public void cnmt5_a()  //cnmt
        {
            cnmtlocationbig = 5;    //大題位置
            textBox1.AppendText("進行CNMT" + cnmtlocationbig + "stage" + " \r\n");
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageA"), "開始", "cnmt5_a_2", "", "");//
        }
        public void cnmt5_a_2() //學習階段
        {
            panel.showcnmtpic();
            panel.cnmt5_1();   //換r階段圖片
            cnmtstage = 0;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;


            //cnmtlabel_0_2_a:
            cnmtlabel:

            //cnmtwrongtime
            if (cnmtwrongtime[cnmtlocationbig][cnmtstage] >= 10) //錯10次，跳D
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                //清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                cnmt5_d();
            }

            if (donetwice == 2)  //做對2次了
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                // 清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                //因為對2次，要進B步
                cnmt5_b();
            }

            if (donetwice == 1)
            {
                Thread.Sleep(300);
                lightbotton6(); //下面燈
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                for (int j = 0; j < 6; j++)
                {
                    cnmttoplight(5);     //上面的燈號鎖定
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;


                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            playaudiocnmtright();
                            panel.show1();//debug
                            //Thread.Sleep(150);
                            donetwice = donetwice + 1;
                            goto cnmtlabel;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音  
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
            }//已經對一次


            for (int i = 1; i < 7; i++)  //要做 1 12 123 1234 12345 123456 6輪
            {
                Thread.Sleep(300);
                lightbotton6();
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                                                                   //playaudiocnmt5();//聲音   小題開始
                for (int j = 0; j < i; j++)
                {
                    cnmttoplight(i - 1);     //上面的燈號
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;

                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        textBox1.AppendText("j= " + Convert.ToString(j));


                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            donetwice = donetwice + 1;
                            cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
                playaudiocnmtright();
                panel.show1();//debug
                //Thread.Sleep(150);
            }

            if (donetwice < 2)  //還不夠 對2次
            {
                goto cnmtlabel;
            }

        }
        public void cnmt5_b()  //空間回憶
        {
            textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗 進入空間回憶  \r\n");
            cnmtlocationbig = 5;    //大題位置
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageB"), "開始", "cnmt5_b_2", "", "");//

            //cnmt5_3();//
        }
        public void cnmt5_b_2() //學習階段
        {
            panel.showcnmtpic();
            panel.cnmt5_2();   //換r階段圖片
            cnmtstage = 1;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;


            //cnmtlabel_0_2_a:
            cnmtlabel:

            //cnmtwrongtime
            if (cnmtwrongtime[cnmtlocationbig][cnmtstage] >= 5)
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                //清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                cnmt5_d();
            }

            if (donetwice == 2)  //做對2次了
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                // 清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                //因為對2次，要進B步
                cnmt5_c();
            }

            if (donetwice == 1)
            {
                Thread.Sleep(300);
                lightbotton6(); //下面燈
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                for (int j = 0; j < 6; j++)
                {
                    cnmttoplight(5);     //上面的燈號鎖定
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;


                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            playaudiocnmtright();
                            panel.show1();//debug
                            //Thread.Sleep(150);
                            donetwice = donetwice + 1;
                            goto cnmtlabel;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音  
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
            }//已經對一次


            for (int i = 1; i < 7; i++)  //要做 1 12 123 1234 12345 123456 6輪
            {
                Thread.Sleep(300);
                lightbotton6();
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                                                                   //playaudiocnmt5();//聲音   小題開始
                for (int j = 0; j < i; j++)
                {
                    cnmttoplight(i - 1);     //上面的燈號
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;

                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        textBox1.AppendText("j= " + Convert.ToString(j));


                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            donetwice = donetwice + 1;
                            cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
                playaudiocnmtright();
                panel.show1();//debug
                //Thread.Sleep(150);
            }

            if (donetwice < 2)  //還不夠 對2次
            {
                goto cnmtlabel;
            }

        }
        public void cnmt5_c()  //順序回憶
        {
            textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗 進入順序回憶  \r\n");
            cnmtlocationbig = 5;    //大題位置
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageC"), "開始", "cnmt5_c_2", "", "");//
        }
        public void cnmt5_c_2() //學習階段
        {
            panel.showcnmtpic();
            panel.cnmt5_3();   //換r階段圖片
            cnmtstage = 2;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;


            //cnmtlabel_0_2_a:
            cnmtlabel:

            //cnmtwrongtime
            if (cnmtwrongtime[cnmtlocationbig][cnmtstage] >= 5)
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                //清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                cnmt5_d();
            }

            if (donetwice == 2)  //做對2次了
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                // 清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                //因為對2次，要進B步
                cnmt5_d();
            }

            if (donetwice == 1)
            {
                Thread.Sleep(300);
                lightbotton6(); //下面燈
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                for (int j = 0; j < 6; j++)
                {
                    cnmttoplight(5);     //上面的燈號鎖定
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;


                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            playaudiocnmtright();
                            panel.show1();//debug
                            //Thread.Sleep(150);
                            donetwice = donetwice + 1;
                            goto cnmtlabel;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音  
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
            }//已經對一次


            for (int i = 1; i < 7; i++)  //要做 1 12 123 1234 12345 123456 6輪
            {
                Thread.Sleep(300);
                lightbotton6();
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                                                                   //playaudiocnmt5();//聲音   小題開始
                for (int j = 0; j < i; j++)
                {
                    cnmttoplight(i - 1);     //上面的燈號
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;

                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        textBox1.AppendText("j= " + Convert.ToString(j));


                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            donetwice = donetwice + 1;
                            cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
                playaudiocnmtright();
                panel.show1();//debug
                //Thread.Sleep(150);
            }

            if (donetwice < 2)  //還不夠 對2次
            {
                goto cnmtlabel;
            }

        }
        public void cnmt5_d()  //再認
        {
            textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗 進入再認  \r\n");
            cnmtlocationbig = 5;    //大題位置
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageD"), "繼續再認", "cnmt5_d_2", "", "");//
        }
        public void cnmt5_d_2()  //再認
        {
            panel.showcnmtpic();
            panel.cnmt5_4();   //換r階段圖片
            cnmtstage = 3;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;

            cnmtlabel:

            if (donetwice == 2)//做完2次
            {
                cnmt5_3();
            }
            //playaudiocnmt5();//聲音   小題開始
            cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
            lightbotton6(); //下面燈全亮

            for (int j = 0; j < 3; j++)    //只有按3次
            {
                cnmttoplight(6);     //上面的燈號通通關掉
                //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                playaudiocnmt1();//聲音   小題開始 

                while (cnmtgo == 0) //等待按鍵
                {
                    Application.DoEvents();
                }
                cnmtgo = 0;

                //存入戰存
                // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                cnmtresultpresssmall = cnmtresultpresssmall + 1;


                cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                cnmtresulttimesmall = cnmtresulttimesmall + 1;

                cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], donetwice, j] = buttoncnmt;
                cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], donetwice, j] = cnmtticktime;

                //看正確與否決定下一步
                //先把前3步存起來
                cnmtagain[cnmtlocationbig][j] = buttoncnmt;   //存放3個按鍵
                cnmtagain2[j] = buttoncnmt;   //存放3個按鍵
            }


            //現在cnmtagain裡面有 3個按鍵來判斷吧
            Array.Sort(cnmtagain2);
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[0])+  " \r\n");
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[1]) + " \r\n");
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[2]) + " \r\n");
            // textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtrightresult[cnmtlocationbig, cnmtstage, 0]) + " \r\n");
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtrightresult[cnmtlocationbig, cnmtstage, 1]) + " \r\n");
            // textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtrightresult[cnmtlocationbig, cnmtstage, 2]) + " \r\n");
            int k = 0;
            for (int i = 0; i < 3; i++)
            {
                if (cnmtagain2[i] == cnmtrightresult[cnmtlocationbig, cnmtstage, i])
                {
                    k = k + 1;
                    //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[0]) + "nkvidnj=  " + cnmtrightresult[cnmtlocationbig, cnmtstage, i] + " \r\n");
                }
            }

            if (k == 3)
            {
                playaudiocnmtright();
                //Thread.Sleep(150);
            }
            else
            {
                playaudiocnmtwrong();
                //Thread.Sleep(150);
            }
            Thread.Sleep(300);
            cnmtagainrightcount[cnmtlocationbig] = cnmtagainrightcount[cnmtlocationbig] + k;

            donetwice = donetwice + 1;
            goto cnmtlabel;
        }
        public void cnmt5_3()
        {
            //存檔放這 

            //
            savedatacnmtbig();
            cnmtresulttimesmall = 0;
            cnmtresultpresssmall = 0;
            donetwice = 0;
            cnmtgetdata = 0;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 0; //第二步

            textBox2.AppendText("cnmt" + cnmtlocationbig + " \r\n");
            //Zsavedata4();
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt" + (cnmtlocationbig + 1) + "測驗"), "cnmt5", "cnmt5", "", "");//
        }

        //CNMT 6--------------------------------------
        public void cnmt6() // CNMT   起始畫面
        {
            cnmtlocationbig = 6;    //大題位置
            textBox2.AppendText("cnmt" + (cnmtlocationbig - 1) + "結束 \r\n");
            cnmtgo = 0;

            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;


            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗"), "開始", "cnmt6_a", "", "");//


        }
        public void cnmt6_a()  //cnmt
        {
            cnmtlocationbig = 6;    //大題位置
            textBox1.AppendText("進行CNMT" + cnmtlocationbig + "stage" + " \r\n");
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageA"), "開始", "cnmt6_a_2", "", "");//
        }
        public void cnmt6_a_2() //學習階段
        {
            panel.showcnmtpic();
            panel.cnmt6_1();   //換r階段圖片
            cnmtstage = 0;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;


            //cnmtlabel_0_2_a:
            cnmtlabel:

            //cnmtwrongtime
            if (cnmtwrongtime[cnmtlocationbig][cnmtstage] >= 10) //錯10次，跳D
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                //清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                cnmt6_d();
            }

            if (donetwice == 2)  //做對2次了
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                // 清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                //因為對2次，要進B步
                cnmt6_b();
            }

            if (donetwice == 1)
            {
                Thread.Sleep(300);
                lightbotton6(); //下面燈
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                for (int j = 0; j < 6; j++)
                {
                    cnmttoplight(5);     //上面的燈號鎖定
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;


                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            playaudiocnmtright();
                            panel.show1();//debug
                            //Thread.Sleep(150);
                            donetwice = donetwice + 1;
                            goto cnmtlabel;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音  
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
            }//已經對一次


            for (int i = 1; i < 7; i++)  //要做 1 12 123 1234 12345 123456 6輪
            {
                Thread.Sleep(300);
                lightbotton6();
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                                                                   //playaudiocnmt6();//聲音   小題開始
                for (int j = 0; j < i; j++)
                {
                    cnmttoplight(i - 1);     //上面的燈號
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;

                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        textBox1.AppendText("j= " + Convert.ToString(j));


                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            donetwice = donetwice + 1;
                            cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
                playaudiocnmtright();
                panel.show1();//debug
                //Thread.Sleep(150);
            }

            if (donetwice < 2)  //還不夠 對2次
            {
                goto cnmtlabel;
            }

        }
        public void cnmt6_b()  //空間回憶
        {
            textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗 進入空間回憶  \r\n");
            cnmtlocationbig = 6;    //大題位置
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageB"), "開始", "cnmt6_b_2", "", "");//

            //cnmt6_3();//
        }
        public void cnmt6_b_2() //學習階段
        {
            panel.showcnmtpic();
            panel.cnmt6_2();   //換r階段圖片
            cnmtstage = 1;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;


            //cnmtlabel_0_2_a:
            cnmtlabel:

            //cnmtwrongtime
            if (cnmtwrongtime[cnmtlocationbig][cnmtstage] >= 5)
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                //清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                cnmt6_d();
            }

            if (donetwice == 2)  //做對2次了
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                // 清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                //因為對2次，要進B步
                cnmt6_c();
            }

            if (donetwice == 1)
            {
                Thread.Sleep(300);
                lightbotton6(); //下面燈
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                for (int j = 0; j < 6; j++)
                {
                    cnmttoplight(5);     //上面的燈號鎖定
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;


                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            playaudiocnmtright();
                            panel.show1();//debug
                            //Thread.Sleep(150);
                            donetwice = donetwice + 1;
                            goto cnmtlabel;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音  
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
            }//已經對一次


            for (int i = 1; i < 7; i++)  //要做 1 12 123 1234 12345 123456 6輪
            {
                Thread.Sleep(300);
                lightbotton6();
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                                                                   //playaudiocnmt6();//聲音   小題開始
                for (int j = 0; j < i; j++)
                {
                    cnmttoplight(i - 1);     //上面的燈號
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;

                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        textBox1.AppendText("j= " + Convert.ToString(j));


                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            donetwice = donetwice + 1;
                            cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
                playaudiocnmtright();
                panel.show1();//debug
                //Thread.Sleep(150);
            }

            if (donetwice < 2)  //還不夠 對2次
            {
                goto cnmtlabel;
            }

        }
        public void cnmt6_c()  //順序回憶
        {
            textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗 進入順序回憶  \r\n");
            cnmtlocationbig = 6;    //大題位置
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageC"), "開始", "cnmt6_c_2", "", "");//
        }
        public void cnmt6_c_2() //學習階段
        {
            panel.showcnmtpic();
            panel.cnmt6_3();   //換r階段圖片
            cnmtstage = 2;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;


            //cnmtlabel_0_2_a:
            cnmtlabel:

            //cnmtwrongtime
            if (cnmtwrongtime[cnmtlocationbig][cnmtstage] >= 5)
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                //清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                cnmt6_d();
            }

            if (donetwice == 2)  //做對2次了
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                // 清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                //因為對2次，要進B步
                cnmt6_d();
            }

            if (donetwice == 1)
            {
                Thread.Sleep(300);
                lightbotton6(); //下面燈
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                for (int j = 0; j < 6; j++)
                {
                    cnmttoplight(5);     //上面的燈號鎖定
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;


                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            playaudiocnmtright();
                            panel.show1();//debug
                            //Thread.Sleep(150);
                            donetwice = donetwice + 1;
                            goto cnmtlabel;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音  
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
            }//已經對一次


            for (int i = 1; i < 7; i++)  //要做 1 12 123 1234 12345 123456 6輪
            {
                Thread.Sleep(300);
                lightbotton6();
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                                                                   //playaudiocnmt6();//聲音   小題開始
                for (int j = 0; j < i; j++)
                {
                    cnmttoplight(i - 1);     //上面的燈號
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;

                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        textBox1.AppendText("j= " + Convert.ToString(j));


                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            donetwice = donetwice + 1;
                            cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
                playaudiocnmtright();
                panel.show1();//debug
                //Thread.Sleep(150);
            }

            if (donetwice < 2)  //還不夠 對2次
            {
                goto cnmtlabel;
            }

        }
        public void cnmt6_d()  //再認
        {
            textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗 進入再認  \r\n");
            cnmtlocationbig = 6;    //大題位置
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageD"), "繼續再認", "cnmt6_d_2", "", "");//
        }
        public void cnmt6_d_2()  //再認
        {
            panel.showcnmtpic();
            panel.cnmt6_4();   //換r階段圖片
            cnmtstage = 3;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;

            cnmtlabel:

            if (donetwice == 2)//做完2次
            {
                cnmt6_3();
            }
            //playaudiocnmt6();//聲音   小題開始
            cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
            lightbotton6(); //下面燈全亮

            for (int j = 0; j < 3; j++)    //只有按3次
            {
                cnmttoplight(6);     //上面的燈號通通關掉
                //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                playaudiocnmt1();//聲音   小題開始 

                while (cnmtgo == 0) //等待按鍵
                {
                    Application.DoEvents();
                }
                cnmtgo = 0;

                //存入戰存
                // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                cnmtresultpresssmall = cnmtresultpresssmall + 1;


                cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                cnmtresulttimesmall = cnmtresulttimesmall + 1;

                cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], donetwice, j] = buttoncnmt;
                cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], donetwice, j] = cnmtticktime;

                //看正確與否決定下一步
                //先把前3步存起來
                cnmtagain[cnmtlocationbig][j] = buttoncnmt;   //存放3個按鍵
                cnmtagain2[j] = buttoncnmt;   //存放3個按鍵
            }


            //現在cnmtagain裡面有 3個按鍵來判斷吧
            Array.Sort(cnmtagain2);
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[0])+  " \r\n");
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[1]) + " \r\n");
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[2]) + " \r\n");
            // textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtrightresult[cnmtlocationbig, cnmtstage, 0]) + " \r\n");
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtrightresult[cnmtlocationbig, cnmtstage, 1]) + " \r\n");
            // textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtrightresult[cnmtlocationbig, cnmtstage, 2]) + " \r\n");
            int k = 0;
            for (int i = 0; i < 3; i++)
            {
                if (cnmtagain2[i] == cnmtrightresult[cnmtlocationbig, cnmtstage, i])
                {
                    k = k + 1;
                    //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[0]) + "nkvidnj=  " + cnmtrightresult[cnmtlocationbig, cnmtstage, i] + " \r\n");
                }
            }

            if (k == 3)
            {
                playaudiocnmtright();
                //Thread.Sleep(150);
            }
            else
            {
                playaudiocnmtwrong();
                //Thread.Sleep(150);
            }
            Thread.Sleep(300);
            cnmtagainrightcount[cnmtlocationbig] = cnmtagainrightcount[cnmtlocationbig] + k;

            donetwice = donetwice + 1;
            goto cnmtlabel;
        }
        public void cnmt6_3()
        {
            //存檔放這 

            //
            savedatacnmtbig();
            cnmtresulttimesmall = 0;
            cnmtresultpresssmall = 0;
            donetwice = 0;
            cnmtgetdata = 0;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 0; //第二步

            textBox2.AppendText("cnmt" + cnmtlocationbig + " \r\n");
            //Zsavedata4();
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt" + (cnmtlocationbig + 1) + "測驗"), "cnmt6", "cnmt6", "", "");//
        }

        // CNMT 7-------------------------------------------------------
        public void cnmt7() // CNMT   起始畫面
        {
            cnmtlocationbig = 7;    //大題位置
            textBox2.AppendText("cnmt" + (cnmtlocationbig - 1) + "結束 \r\n");
            cnmtgo = 0;

            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;


            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗"), "開始", "cnmt7_a", "", "");//


        }
        public void cnmt7_a()  //cnmt
        {
            cnmtlocationbig = 7;    //大題位置
            textBox1.AppendText("進行CNMT" + cnmtlocationbig + "stage" + " \r\n");
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageA"), "開始", "cnmt7_a_2", "", "");//
        }
        public void cnmt7_a_2() //學習階段
        {
            panel.showcnmtpic();
            panel.cnmt7_1();   //換r階段圖片
            cnmtstage = 0;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;


            //cnmtlabel_0_2_a:
            cnmtlabel:

            //cnmtwrongtime
            if (cnmtwrongtime[cnmtlocationbig][cnmtstage] >= 10) //錯10次，跳D
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                //清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                cnmt7_d();
            }

            if (donetwice == 2)  //做對2次了
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                // 清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                //因為對2次，要進B步
                cnmt7_b();
            }

            if (donetwice == 1)
            {
                Thread.Sleep(300);
                lightbotton6(); //下面燈
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                for (int j = 0; j < 6; j++)
                {
                    cnmttoplight(5);     //上面的燈號鎖定
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;


                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            playaudiocnmtright();
                            panel.show1();//debug
                            //Thread.Sleep(150);
                            donetwice = donetwice + 1;
                            goto cnmtlabel;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音  
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
            }//已經對一次


            for (int i = 1; i < 7; i++)  //要做 1 12 123 1234 12345 123456 6輪
            {
                Thread.Sleep(300);
                lightbotton6();
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                                                                   //playaudiocnmt7();//聲音   小題開始
                for (int j = 0; j < i; j++)
                {
                    cnmttoplight(i - 1);     //上面的燈號
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;

                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        textBox1.AppendText("j= " + Convert.ToString(j));


                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            donetwice = donetwice + 1;
                            cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
                playaudiocnmtright();
                panel.show1();//debug
                //Thread.Sleep(150);
            }

            if (donetwice < 2)  //還不夠 對2次
            {
                goto cnmtlabel;
            }

        }
        public void cnmt7_b()  //空間回憶
        {
            textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗 進入空間回憶  \r\n");
            cnmtlocationbig = 7;    //大題位置
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageB"), "開始", "cnmt7_b_2", "", "");//

            //cnmt7_3();//
        }
        public void cnmt7_b_2() //學習階段
        {
            panel.showcnmtpic();
            panel.cnmt7_2();   //換r階段圖片
            cnmtstage = 1;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;


            //cnmtlabel_0_2_a:
            cnmtlabel:

            //cnmtwrongtime
            if (cnmtwrongtime[cnmtlocationbig][cnmtstage] >= 5)
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                //清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                cnmt7_d();
            }

            if (donetwice == 2)  //做對2次了
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                // 清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                //因為對2次，要進B步
                cnmt7_c();
            }

            if (donetwice == 1)
            {
                Thread.Sleep(300);
                lightbotton6(); //下面燈
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                for (int j = 0; j < 6; j++)
                {
                    cnmttoplight(5);     //上面的燈號鎖定
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;


                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            playaudiocnmtright();
                            panel.show1();//debug
                            //Thread.Sleep(150);
                            donetwice = donetwice + 1;
                            goto cnmtlabel;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音  
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
            }//已經對一次


            for (int i = 1; i < 7; i++)  //要做 1 12 123 1234 12345 123456 6輪
            {
                Thread.Sleep(300);
                lightbotton6();
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                                                                   //playaudiocnmt7();//聲音   小題開始
                for (int j = 0; j < i; j++)
                {
                    cnmttoplight(i - 1);     //上面的燈號
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;

                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        textBox1.AppendText("j= " + Convert.ToString(j));


                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            donetwice = donetwice + 1;
                            cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
                playaudiocnmtright();
                panel.show1();//debug
                //Thread.Sleep(150);
            }

            if (donetwice < 2)  //還不夠 對2次
            {
                goto cnmtlabel;
            }

        }
        public void cnmt7_c()  //順序回憶
        {
            textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗 進入順序回憶  \r\n");
            cnmtlocationbig = 7;    //大題位置
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageC"), "開始", "cnmt7_c_2", "", "");//
        }
        public void cnmt7_c_2() //學習階段
        {
            panel.showcnmtpic();
            panel.cnmt7_3();   //換r階段圖片
            cnmtstage = 2;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;


            //cnmtlabel_0_2_a:
            cnmtlabel:

            //cnmtwrongtime
            if (cnmtwrongtime[cnmtlocationbig][cnmtstage] >= 5)
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                //清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                cnmt7_d();
            }

            if (donetwice == 2)  //做對2次了
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                // 清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                //因為對2次，要進B步
                cnmt7_d();
            }

            if (donetwice == 1)
            {
                Thread.Sleep(300);
                lightbotton6(); //下面燈
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                for (int j = 0; j < 6; j++)
                {
                    cnmttoplight(5);     //上面的燈號鎖定
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;


                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            playaudiocnmtright();
                            panel.show1();//debug
                            //Thread.Sleep(150);
                            donetwice = donetwice + 1;
                            goto cnmtlabel;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音  
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
            }//已經對一次


            for (int i = 1; i < 7; i++)  //要做 1 12 123 1234 12345 123456 6輪
            {
                Thread.Sleep(300);
                lightbotton6();
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                                                                   //playaudiocnmt7();//聲音   小題開始
                for (int j = 0; j < i; j++)
                {
                    cnmttoplight(i - 1);     //上面的燈號
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;

                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        textBox1.AppendText("j= " + Convert.ToString(j));


                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            donetwice = donetwice + 1;
                            cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
                playaudiocnmtright();
                panel.show1();//debug
                //Thread.Sleep(150);
            }

            if (donetwice < 2)  //還不夠 對2次
            {
                goto cnmtlabel;
            }

        }
        public void cnmt7_d()  //再認
        {
            textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗 進入再認  \r\n");
            cnmtlocationbig = 7;    //大題位置
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageD"), "繼續再認", "cnmt7_d_2", "", "");//
        }
        public void cnmt7_d_2()  //再認
        {
            panel.showcnmtpic();
            panel.cnmt7_4();   //換r階段圖片
            cnmtstage = 3;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;

            cnmtlabel:

            if (donetwice == 2)//做完2次
            {
                cnmt7_3();
            }
            //playaudiocnmt7();//聲音   小題開始
            cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
            lightbotton6(); //下面燈全亮

            for (int j = 0; j < 3; j++)    //只有按3次
            {
                cnmttoplight(6);     //上面的燈號通通關掉
                //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                playaudiocnmt1();//聲音   小題開始 

                while (cnmtgo == 0) //等待按鍵
                {
                    Application.DoEvents();
                }
                cnmtgo = 0;

                //存入戰存
                // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                cnmtresultpresssmall = cnmtresultpresssmall + 1;


                cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                cnmtresulttimesmall = cnmtresulttimesmall + 1;

                cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], donetwice, j] = buttoncnmt;
                cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], donetwice, j] = cnmtticktime;

                //看正確與否決定下一步
                //先把前3步存起來
                cnmtagain[cnmtlocationbig][j] = buttoncnmt;   //存放3個按鍵
                cnmtagain2[j] = buttoncnmt;   //存放3個按鍵
            }


            //現在cnmtagain裡面有 3個按鍵來判斷吧
            Array.Sort(cnmtagain2);
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[0])+  " \r\n");
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[1]) + " \r\n");
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[2]) + " \r\n");
            // textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtrightresult[cnmtlocationbig, cnmtstage, 0]) + " \r\n");
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtrightresult[cnmtlocationbig, cnmtstage, 1]) + " \r\n");
            // textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtrightresult[cnmtlocationbig, cnmtstage, 2]) + " \r\n");
            int k = 0;
            for (int i = 0; i < 3; i++)
            {
                if (cnmtagain2[i] == cnmtrightresult[cnmtlocationbig, cnmtstage, i])
                {
                    k = k + 1;
                    //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[0]) + "nkvidnj=  " + cnmtrightresult[cnmtlocationbig, cnmtstage, i] + " \r\n");
                }
            }

            if (k == 3)
            {
                playaudiocnmtright();
                //Thread.Sleep(150);
            }
            else
            {
                playaudiocnmtwrong();
                //Thread.Sleep(150);
            }
            Thread.Sleep(300);
            cnmtagainrightcount[cnmtlocationbig] = cnmtagainrightcount[cnmtlocationbig] + k;

            donetwice = donetwice + 1;
            goto cnmtlabel;
        }
        public void cnmt7_3()
        {
            //存檔放這 

            //
            savedatacnmtbig();
            cnmtresulttimesmall = 0;
            cnmtresultpresssmall = 0;
            donetwice = 0;
            cnmtgetdata = 0;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 0; //第二步

            textBox2.AppendText("cnmt" + cnmtlocationbig + " \r\n");
            //Zsavedata4();
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt" + (cnmtlocationbig + 1) + "測驗"), "cnmt7", "cnmt7", "", "");//
        }

        //CNMT8--------------------------------------------
        public void cnmt8() // CNMT   起始畫面
        {
            cnmtlocationbig = 8;    //大題位置
            textBox2.AppendText("cnmt" + (cnmtlocationbig - 1) + "結束 \r\n");
            cnmtgo = 0;

            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;


            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗"), "開始", "cnmt8_a", "", "");//


        }
        public void cnmt8_a()  //cnmt
        {
            cnmtlocationbig = 8;    //大題位置
            textBox1.AppendText("進行CNMT" + cnmtlocationbig + "stage" + " \r\n");
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageA"), "開始", "cnmt8_a_2", "", "");//
        }
        public void cnmt8_a_2() //學習階段
        {
            panel.showcnmtpic();
            panel.cnmt8_1();   //換r階段圖片
            cnmtstage = 0;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;


            //cnmtlabel_0_2_a:
            cnmtlabel:

            //cnmtwrongtime
            if (cnmtwrongtime[cnmtlocationbig][cnmtstage] >= 10) //錯10次，跳D
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                //清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                cnmt8_d();
            }

            if (donetwice == 2)  //做對2次了
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                // 清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                //因為對2次，要進B步
                cnmt8_b();
            }

            if (donetwice == 1)
            {
                Thread.Sleep(300);
                lightbotton6(); //下面燈
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                for (int j = 0; j < 6; j++)
                {
                    cnmttoplight(5);     //上面的燈號鎖定
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;


                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            playaudiocnmtright();
                            panel.show1();//debug
                            //Thread.Sleep(150);
                            donetwice = donetwice + 1;
                            goto cnmtlabel;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音  
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
            }//已經對一次


            for (int i = 1; i < 7; i++)  //要做 1 12 123 1234 12345 123456 6輪
            {
                Thread.Sleep(300);
                lightbotton6();
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                                                                   //playaudiocnmt8();//聲音   小題開始
                for (int j = 0; j < i; j++)
                {
                    cnmttoplight(i - 1);     //上面的燈號
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;

                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        textBox1.AppendText("j= " + Convert.ToString(j));


                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            donetwice = donetwice + 1;
                            cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
                playaudiocnmtright();
                panel.show1();//debug
                //Thread.Sleep(150);
            }

            if (donetwice < 2)  //還不夠 對2次
            {
                goto cnmtlabel;
            }

        }
        public void cnmt8_b()  //空間回憶
        {
            textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗 進入空間回憶  \r\n");
            cnmtlocationbig = 8;    //大題位置
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageB"), "開始", "cnmt8_b_2", "", "");//

            //cnmt8_3();//
        }

        private void Timer11_Tick(object sender, EventArgs e)
        {
            timer11.Enabled = false;
            cnatgo2 = 0;
        }

        public void cnmt8_b_2() //學習階段
        {
            panel.showcnmtpic();
            panel.cnmt8_2();   //換r階段圖片
            cnmtstage = 1;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;


            //cnmtlabel_0_2_a:
            cnmtlabel:

            //cnmtwrongtime
            if (cnmtwrongtime[cnmtlocationbig][cnmtstage] >= 5)
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                //清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                cnmt8_d();
            }

            if (donetwice == 2)  //做對2次了
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                // 清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                //因為對2次，要進B步
                cnmt8_c();
            }

            if (donetwice == 1)
            {
                Thread.Sleep(300);
                lightbotton6(); //下面燈
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                for (int j = 0; j < 6; j++)
                {
                    cnmttoplight(5);     //上面的燈號鎖定
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;


                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            playaudiocnmtright();
                            panel.show1();//debug
                            //Thread.Sleep(150);
                            donetwice = donetwice + 1;
                            goto cnmtlabel;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音  
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
            }//已經對一次


            for (int i = 1; i < 7; i++)  //要做 1 12 123 1234 12345 123456 6輪
            {
                Thread.Sleep(300);
                lightbotton6();
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                                                                   //playaudiocnmt8();//聲音   小題開始
                for (int j = 0; j < i; j++)
                {
                    cnmttoplight(i - 1);     //上面的燈號
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;

                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        textBox1.AppendText("j= " + Convert.ToString(j));


                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            donetwice = donetwice + 1;
                            cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
                playaudiocnmtright();
                panel.show1();//debug
                //Thread.Sleep(150);
            }

            if (donetwice < 2)  //還不夠 對2次
            {
                goto cnmtlabel;
            }

        }
        public void cnmt8_c()  //順序回憶
        {
            textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗 進入順序回憶  \r\n");
            cnmtlocationbig = 8;    //大題位置
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageC"), "開始", "cnmt8_c_2", "", "");//
        }
        public void cnmt8_c_2() //學習階段
        {
            panel.showcnmtpic();
            panel.cnmt8_3();   //換r階段圖片
            cnmtstage = 2;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;


            //cnmtlabel_0_2_a:
            cnmtlabel:

            //cnmtwrongtime
            if (cnmtwrongtime[cnmtlocationbig][cnmtstage] >= 5)
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                //清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                cnmt8_d();
            }

            if (donetwice == 2)  //做對2次了
            {
                textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗" + Convert.ToString(cnmtstage) + "階段結束  \r\n");
                // 清空資料
                cnmtgetdata = 0;
                donetwice = 0;
                cnmtsixstep = 0;
                //因為對2次，要進B步
                cnmt8_d();
            }

            if (donetwice == 1)
            {
                Thread.Sleep(300);
                lightbotton6(); //下面燈
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                for (int j = 0; j < 6; j++)
                {
                    cnmttoplight(5);     //上面的燈號鎖定
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;


                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], 0, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            playaudiocnmtright();
                            panel.show1();//debug
                            //Thread.Sleep(150);
                            donetwice = donetwice + 1;
                            goto cnmtlabel;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音  
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
            }//已經對一次


            for (int i = 1; i < 7; i++)  //要做 1 12 123 1234 12345 123456 6輪
            {
                Thread.Sleep(300);
                lightbotton6();
                cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                                                                   //playaudiocnmt8();//聲音   小題開始
                for (int j = 0; j < i; j++)
                {
                    cnmttoplight(i - 1);     //上面的燈號
                    playaudiocnmt1();//聲音   小題開始
                    //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間

                    while (cnmtgo == 0) //等待按鍵
                    {
                        Application.DoEvents();
                    }
                    cnmtgo = 0;

                    //存入戰存
                    // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                    cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                    cnmtresultpresssmall = cnmtresultpresssmall + 1;

                    cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                    cnmtresulttimesmall = cnmtresulttimesmall + 1;

                    cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = buttoncnmt;
                    cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], i - 1, j] = cnmtticktime;


                    //看正確與否決定下一步
                    if (cnmtrightresult[cnmtlocationbig, cnmtstage, j] == buttoncnmt)  //正確
                    {
                        textBox1.AppendText("j= " + Convert.ToString(j));


                        //playaudiocnmtright();//正確要有正確聲音

                        if (j == 5)//最後一個 也對了
                        {
                            donetwice = donetwice + 1;
                            cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        }
                    }
                    else
                    {
                        playaudiocnmtwrong();//錯了要有聲音
                        panel.show2();//debug
                        //Thread.Sleep(150);
                        cnmtwrongtime[cnmtlocationbig][cnmtstage] = cnmtwrongtime[cnmtlocationbig][cnmtstage] + 1;  // 錯誤計次++
                        cnmt_trycount[cnmtlocationbig, cnmtstage] = cnmt_trycount[cnmtlocationbig, cnmtstage] + 1;
                        goto cnmtlabel;  //跳出迴圈
                    }
                }
                playaudiocnmtright();
                panel.show1();//debug
                //Thread.Sleep(150);
            }

            if (donetwice < 2)  //還不夠 對2次
            {
                goto cnmtlabel;
            }

        }
        public void cnmt8_d()  //再認
        {
            textBox2.AppendText("cnmt 第 " + Convert.ToString(cnmtlocationbig) + " 測驗 進入再認  \r\n");
            cnmtlocationbig = 8;    //大題位置
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1(Convert.ToString("進行cnmt第" + cnmtlocationbig + "測驗 stageD"), "繼續再認", "cnmt8_d_2", "", "");//
        }
        public void cnmt8_d_2()  //再認
        {
            panel.showcnmtpic();
            panel.cnmt8_4();   //換r階段圖片
            cnmtstage = 3;//第幾階段
            cnmtgetdata = 1;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 1; //第二步
            donetwice = 0;

            cnmtlabel:

            if (donetwice == 2)//做完2次
            {
                cnmt8_3();
            }
            //playaudiocnmt8();//聲音   小題開始
            cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
            lightbotton6(); //下面燈全亮

            for (int j = 0; j < 3; j++)    //只有按3次
            {
                cnmttoplight(6);     //上面的燈號通通關掉
                //cnmtbeeptimetick = System.Environment.TickCount;   //紀錄聲音時間
                playaudiocnmt1();//聲音   小題開始 

                while (cnmtgo == 0) //等待按鍵
                {
                    Application.DoEvents();
                }
                cnmtgo = 0;

                //存入戰存
                // cnmtpressarray[j] = buttoncnmt; //剛剛按的鍵存到判定對錯的array

                cnmtresultpress[cnmtlocationbig][cnmtresultpresssmall] = buttoncnmt;  // 只要有按就存   //存按鍵
                cnmtresultpresssmall = cnmtresultpresssmall + 1;


                cnmtresulttime[cnmtlocationbig][cnmtresulttimesmall] = cnmtticktime;  // 只要有按就存   //存時間
                cnmtresulttimesmall = cnmtresulttimesmall + 1;

                cnmtresultpress3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], donetwice, j] = buttoncnmt;
                cnmtresulttime3[cnmtlocationbig, cnmtstage, cnmt_trycount[cnmtlocationbig, cnmtstage], donetwice, j] = cnmtticktime;

                //看正確與否決定下一步
                //先把前3步存起來
                cnmtagain[cnmtlocationbig][j] = buttoncnmt;   //存放3個按鍵
                cnmtagain2[j] = buttoncnmt;   //存放3個按鍵
            }


            //現在cnmtagain裡面有 3個按鍵來判斷吧
            Array.Sort(cnmtagain2);
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[0])+  " \r\n");
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[1]) + " \r\n");
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[2]) + " \r\n");
            // textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtrightresult[cnmtlocationbig, cnmtstage, 0]) + " \r\n");
            //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtrightresult[cnmtlocationbig, cnmtstage, 1]) + " \r\n");
            // textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtrightresult[cnmtlocationbig, cnmtstage, 2]) + " \r\n");
            int k = 0;
            for (int i = 0; i < 3; i++)
            {
                if (cnmtagain2[i] == cnmtrightresult[cnmtlocationbig, cnmtstage, i])
                {
                    k = k + 1;
                    //textBox1.AppendText("cnmtagain2  " + Convert.ToString(cnmtagain2[0]) + "nkvidnj=  " + cnmtrightresult[cnmtlocationbig, cnmtstage, i] + " \r\n");
                }
            }

            if (k == 3)
            {
                playaudiocnmtright();
                //Thread.Sleep(150);
            }
            else
            {
                playaudiocnmtwrong();
                //Thread.Sleep(150);
            }
            Thread.Sleep(300);
            cnmtagainrightcount[cnmtlocationbig] = cnmtagainrightcount[cnmtlocationbig] + k;

            donetwice = donetwice + 1;
            goto cnmtlabel;
        }
        public void cnmt8_3()
        {
            //存檔放這 

            //
            savedatacnmtbig();
            cnmtresulttimesmall = 0;
            cnmtresultpresssmall = 0;
            donetwice = 0;
            cnmtgetdata = 0;//現在開始 在panel的按鍵就會更改  buttoncnmt
            cnmtsixstep = 0; //第二步

            textBox2.AppendText("cnmt" + cnmtlocationbig + " \r\n");
            //Zsavedata4();
            Form4_pop pop = new Form4_pop();
            pop.Owner = this;//要有這個不然不能傳
            pop.Visible = true;
            pop.readfrom1("FINISH", "END", "", "", "");//
        }

        public void temp1_check()//檢查FORM4丟回來的資料 決定動作 //由form234 call
        {
            if (temp1 == "1")      //temp1 等於timer1 開始動作  為15秒  //timer1234 給
            {
                timer1.Interval = 15000;//15秒
                timer1.Enabled = true;
                buttontest1 = 1;   //=1之後  開始啟動  getdata 在上面開始抽資料
                temp1 = "0";
            }
            if (temp1 == "2")      //temp1 等於timer2 開始動作  為15秒
            {
                timer2.Interval = 15000;//15秒
                timer2.Enabled = true;
                buttontest1 = 2;   //=2之後  開始啟動  getdata 在上面開始抽資料
                temp1 = "0";
            }
            if (temp1 == "3")
            {
                timer3.Interval = 15000;//15秒
                timer3.Enabled = true;
                buttontest1 = 3;   //=3之後  開始啟動  getdata 在上面開始抽資料
                temp1 = "0";
            }
            if (temp1 == "4")
            {
                timer4.Interval = 15000;//15秒
                timer4.Enabled = true;
                buttontest1 = 4;   //=4之後  開始啟動  getdata 在上面開始抽資料
                temp1 = "0";
            }
            if (temp1 == "cnatp1")  // cnat 測試1
            {
                textBox2.AppendText("cnat 測驗一練習題 \r\n");
                cnatp1_2();
                temp1 = "0";
            }
            if (temp3 == "id1")//把 ID的8個資料拉過來  // ID 按下確定之後會出來
            {
                temp3 = "0";
                textBox3.AppendText(d1 + "_" + d7);
                //savedata1();
                savedatainfo();
            }
            if (temp2 == "cnat1")  // cnat 實戰1  
            {
                temp2 = "0";
                textBox2.AppendText("cnat 測驗一正式開始 \r\n");
                cnat1();

            }
            if (temp1 == "cnat1_2")  // cnat 實戰1  迴圈畫面
            {
                temp1 = "0";
                textBox2.AppendText("cnat 測驗一跑燈號 \r\n");
                cnat1_2();
            }
            if (temp1 == "cnatp2")  // cnat 練習2
            {
                textBox2.AppendText("cnat 測驗二練習題 \r\n");
                cnatp2_2();
                temp1 = "0";
            }
            if (temp2 == "cnat2")  // cnat 實戰2  
            {
                temp2 = "0";
                textBox2.AppendText("cnat 測驗二正式開始 \r\n");
                cnat2();

            }
            if (temp1 == "cnat2_2")  // cnat 實戰1  迴圈畫面
            {
                temp1 = "0";
                textBox2.AppendText("cnat 測驗2跑燈號 \r\n");
                cnat2_2();
            }
            if (temp1 == "cnatp3")  // cnat 練習3
            {
                textBox2.AppendText("cnat 測驗3練習題 \r\n");
                cnatp3_2();
                temp1 = "0";
            }
            if (temp2 == "cnat3")  // cnat 實戰3  
            {
                temp2 = "0";
                textBox2.AppendText("cnat 測驗3正式開始 \r\n");
                cnat3();

            }
            if (temp1 == "cnat3_2")  // cnat 實戰3  迴圈畫面
            {
                temp1 = "0";
                textBox2.AppendText("cnat 測驗3跑燈號 \r\n");
                cnat3_2();
            }
            if (temp1 == "cnatp4")  // cnat 練習4
            {
                textBox2.AppendText("cnat 測驗4練習題 \r\n");
                cnatp4_2();
                temp1 = "0";
            }
            if (temp2 == "cnat4")  // cnat 實戰4  
            {
                temp2 = "0";
                textBox2.AppendText("cnat 測驗4正式開始 \r\n");
                cnat4();

            }
            if (temp1 == "cnat4_2")  // cnat 實戰4  迴圈畫面
            {
                temp1 = "0";
                textBox2.AppendText("cnat 測驗4跑燈號 \r\n");
                cnat4_2();
            }
            if (temp1 == "cnatp5")  // cnat 練習5
            {
                textBox2.AppendText("cnat 測驗5練習題 \r\n");
                cnatp5_2();
                temp1 = "0";
            }
            if (temp2 == "cnat5")  // cnat 實戰5
            {
                temp2 = "0";
                textBox2.AppendText("cnat 測驗5正式開始 \r\n");
                cnat5();

            }
            if (temp1 == "cnat5_2")  // cnat 實戰5  迴圈畫面
            {
                temp1 = "0";
                textBox2.AppendText("cnat 測驗5跑燈號 \r\n");
                cnat5_2();
            }
            if (temp1 == "cnatp6")  // cnat 練習6
            {
                textBox2.AppendText("cnat 測驗6練習題 \r\n");
                cnatp6_2();
                temp1 = "0";
            }
            if (temp2 == "cnat6")  // cnat 實戰6
            {
                temp2 = "0";
                textBox2.AppendText("cnat 測驗6正式開始 \r\n");
                cnat6();

            }
            if (temp1 == "cnat6_2")  // cnat 實戰6  迴圈畫面
            {
                temp1 = "0";
                textBox2.AppendText("cnat 測驗6跑燈號 \r\n");
                cnat6_2();
            }
            if (temp1 == "cnatp7")  // cnat 練習7
            {
                textBox2.AppendText("cnat 測驗7練習題 \r\n");
                cnatp7_2();
                temp1 = "0";
            }
            if (temp2 == "cnat7")  // cnat 實戰7
            {
                temp2 = "0";
                textBox2.AppendText("cnat 測驗7正式開始 \r\n");
                cnat7();

            }
            if (temp1 == "cnat7_2")  // cnat 實戰7  迴圈畫面
            {
                temp1 = "0";
                textBox2.AppendText("cnat 測驗7跑燈號 \r\n");
                cnat7_2();
            }
            if (temp1 == "cnatp8")  // cnat 練習8
            {
                textBox2.AppendText("cnat 測驗8練習題 \r\n");
                cnatp8_2();
                temp1 = "0";
            }
            if (temp2 == "cnat8")  // cnat 實戰8
            {
                temp2 = "0";
                textBox2.AppendText("cnat 測驗8正式開始 \r\n");
                cnat8();

            }
            if (temp1 == "cnat8_2")  // cnat 實戰8  迴圈畫面
            {
                temp1 = "0";
                textBox2.AppendText("cnat 測驗8跑燈號 \r\n");
                cnat8_2();
            }
            if (temp1 == "cnat9")  // cnat 實戰9
            {
                temp1 = "0";
                textBox2.AppendText("cnat 測驗9正式開始 \r\n");
                cnat9();

            }
            if (temp1 == "cnat9_2")  // cnat 實戰9  迴圈畫面
            {
                temp1 = "0";
                textBox2.AppendText("cnat 測驗9跑燈號 \r\n");
                cnat9_2();
            }









            if (temp1 == "cnmtleft")  // cnmt  左手
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 測驗左手 \r\n");
                cnmtleft();
            }
            if (temp1 == "cnmtright")  // cnmt  右手
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 測驗右手 \r\n");
                cnmtright();
            }


            if (temp2 == "cnmt1")  // cnmt 
            {
                temp2 = "0";
                textBox2.AppendText("cnmt 練習題_再認_迴圈 \r\n");
                cnmt1();
            }

            if (temp1 == "cnmt0_a")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 練習題開始  \r\n");
                cnmt0_a();
            }
            if (temp1 == "cnmt0_a_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 練習題_學習 \r\n");
                cnmt0_a_2();
            }
            if (temp1 == "cnmt0_b")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 練習題_空間回憶_跳出畫面 \r\n");
                cnmt0_b();
            }
            if (temp1 == "cnmt0_b_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 練習題_空間回憶_迴圈 \r\n");
                cnmt0_b_2();
            }
            if (temp1 == "cnmt0_c")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 練習題_順序回憶_跳出畫面 \r\n");
                cnmt0_c();
            }
            if (temp1 == "cnmt0_c_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 練習題_順序回憶_迴圈 \r\n");
                cnmt0_c_2();
            }
            if (temp1 == "cnmt0_d")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 練習題_再認_跳出畫面 \r\n");
                cnmt0_d();
            }
            if (temp1 == "cnmt0_d_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 練習題_再認_迴圈 \r\n");
                cnmt0_d_2();
            }

            if (temp1 == "cnmt1_a")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 1開始  \r\n");
                cnmt1_a();
            }
            if (temp1 == "cnmt1_a_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 1_學習 \r\n");
                cnmt1_a_2();
            }
            if (temp1 == "cnmt1_b")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 1_空間回憶_跳出畫面 \r\n");
                cnmt1_b();
            }
            if (temp1 == "cnmt1_b_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 1_空間回憶_迴圈 \r\n");
                cnmt1_b_2();
            }
            if (temp1 == "cnmt1_c")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 1_順序回憶_跳出畫面 \r\n");
                cnmt1_c();
            }
            if (temp1 == "cnmt1_c_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 1_順序回憶_迴圈 \r\n");
                cnmt1_c_2();
            }
            if (temp1 == "cnmt1_d")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 1_再認_跳出畫面 \r\n");
                cnmt1_d();
            }
            if (temp1 == "cnmt1_d_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 練習題_再認_迴圈 \r\n");
                cnmt1_d_2();
            }

            if (temp1 == "cnmt2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 2開始  \r\n");
                cnmt2();
            }

            if (temp1 == "cnmt2_a")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 2開始  \r\n");
                cnmt2_a();
            }
            if (temp1 == "cnmt2_a_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 2_學習 \r\n");
                cnmt2_a_2();
            }
            if (temp1 == "cnmt2_b")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 2_空間回憶_跳出畫面 \r\n");
                cnmt2_b();
            }
            if (temp1 == "cnmt2_b_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 2_空間回憶_迴圈 \r\n");
                cnmt2_b_2();
            }
            if (temp1 == "cnmt1_c")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 2_順序回憶_跳出畫面 \r\n");
                cnmt2_c();
            }
            if (temp1 == "cnmt2_c_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 2_順序回憶_迴圈 \r\n");
                cnmt2_c_2();
            }
            if (temp1 == "cnmt2_d")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 2_再認_跳出畫面 \r\n");
                cnmt2_d();
            }
            if (temp1 == "cnmt2_d_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 練習題_再認_迴圈 \r\n");
                cnmt2_d_2();
            }

            if (temp1 == "cnmt3")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 3開始  \r\n");
                cnmt3();
            }

            if (temp1 == "cnmt3_a")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 3開始  \r\n");
                cnmt3_a();
            }
            if (temp1 == "cnmt3_a_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 3_學習 \r\n");
                cnmt3_a_2();
            }
            if (temp1 == "cnmt3_b")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 3_空間回憶_跳出畫面 \r\n");
                cnmt3_b();
            }
            if (temp1 == "cnmt3_b_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 3_空間回憶_迴圈 \r\n");
                cnmt3_b_2();
            }
            if (temp1 == "cnmt3_c")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 3_順序回憶_跳出畫面 \r\n");
                cnmt3_c();
            }
            if (temp1 == "cnmt3_c_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 3_順序回憶_迴圈 \r\n");
                cnmt3_c_2();
            }
            if (temp1 == "cnmt3_d")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 3_再認_跳出畫面 \r\n");
                cnmt3_d();
            }
            if (temp1 == "cnmt3_d_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 3_再認_迴圈 \r\n");
                cnmt3_d_2();
            }





            if (temp1 == "cnmt4")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 4開始  \r\n");
                cnmt4();
            }
            if (temp1 == "cnmt4_a")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 4開始  \r\n");
                cnmt4_a();
            }
            if (temp1 == "cnmt4_a_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 4_學習 \r\n");
                cnmt4_a_2();
            }
            if (temp1 == "cnmt4_b")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 4_空間回憶_跳出畫面 \r\n");
                cnmt4_b();
            }
            if (temp1 == "cnmt4_b_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 4_空間回憶_迴圈 \r\n");
                cnmt4_b_2();
            }
            if (temp1 == "cnmt4_c")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 4_順序回憶_跳出畫面 \r\n");
                cnmt4_c();
            }
            if (temp1 == "cnmt4_c_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 4_順序回憶_迴圈 \r\n");
                cnmt4_c_2();
            }
            if (temp1 == "cnmt4_d")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 4_再認_跳出畫面 \r\n");
                cnmt4_d();
            }
            if (temp1 == "cnmt4_d_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 4_再認_迴圈 \r\n");
                cnmt4_d_2();
            }





            if (temp1 == "cnmt5")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 5開始  \r\n");
                cnmt5();
            }
            if (temp1 == "cnmt5_a")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 5開始  \r\n");
                cnmt5_a();
            }
            if (temp1 == "cnmt5_a_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 5_學習 \r\n");
                cnmt5_a_2();
            }
            if (temp1 == "cnmt5_b")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 5_空間回憶_跳出畫面 \r\n");
                cnmt5_b();
            }
            if (temp1 == "cnmt5_b_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 5_空間回憶_迴圈 \r\n");
                cnmt5_b_2();
            }
            if (temp1 == "cnmt5_c")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 5_順序回憶_跳出畫面 \r\n");
                cnmt5_c();
            }
            if (temp1 == "cnmt5_c_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 5_順序回憶_迴圈 \r\n");
                cnmt5_c_2();
            }
            if (temp1 == "cnmt5_d")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 5_再認_跳出畫面 \r\n");
                cnmt5_d();
            }
            if (temp1 == "cnmt5_d_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 5_再認_迴圈 \r\n");
                cnmt5_d_2();
            }



            if (temp1 == "cnmt6")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 6開始  \r\n");
                cnmt6();
            }
            if (temp1 == "cnmt6_a")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 6開始  \r\n");
                cnmt6_a();
            }
            if (temp1 == "cnmt6_a_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 6_學習 \r\n");
                cnmt6_a_2();
            }
            if (temp1 == "cnmt6_b")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 6_空間回憶_跳出畫面 \r\n");
                cnmt6_b();
            }
            if (temp1 == "cnmt6_b_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 6_空間回憶_迴圈 \r\n");
                cnmt6_b_2();
            }
            if (temp1 == "cnmt6_c")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 6_順序回憶_跳出畫面 \r\n");
                cnmt6_c();
            }
            if (temp1 == "cnmt6_c_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 6_順序回憶_迴圈 \r\n");
                cnmt6_c_2();
            }
            if (temp1 == "cnmt6_d")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 6_再認_跳出畫面 \r\n");
                cnmt6_d();
            }
            if (temp1 == "cnmt6_d_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 6_再認_迴圈 \r\n");
                cnmt6_d_2();
            }


            if (temp1 == "cnmt7")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 7開始  \r\n");
                cnmt7();
            }
            if (temp1 == "cnmt7_a")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 7開始  \r\n");
                cnmt7_a();
            }
            if (temp1 == "cnmt7_a_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 7_學習 \r\n");
                cnmt7_a_2();
            }
            if (temp1 == "cnmt7_b")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 7_空間回憶_跳出畫面 \r\n");
                cnmt7_b();
            }
            if (temp1 == "cnmt7_b_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 7_空間回憶_迴圈 \r\n");
                cnmt7_b_2();
            }
            if (temp1 == "cnmt7_c")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 7_順序回憶_跳出畫面 \r\n");
                cnmt7_c();
            }
            if (temp1 == "cnmt7_c_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 7_順序回憶_迴圈 \r\n");
                cnmt7_c_2();
            }
            if (temp1 == "cnmt7_d")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 7_再認_跳出畫面 \r\n");
                cnmt7_d();
            }
            if (temp1 == "cnmt7_d_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 7_再認_迴圈 \r\n");
                cnmt7_d_2();
            }




            if (temp1 == "cnmt8")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 8開始  \r\n");
                cnmt8();
            }
            if (temp1 == "cnmt8_a")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 8開始  \r\n");
                cnmt8_a();
            }
            if (temp1 == "cnmt8_a_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 8_學習 \r\n");
                cnmt8_a_2();
            }
            if (temp1 == "cnmt8_b")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 8_空間回憶_跳出畫面 \r\n");
                cnmt8_b();
            }
            if (temp1 == "cnmt8_b_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 8_空間回憶_迴圈 \r\n");
                cnmt8_b_2();
            }
            if (temp1 == "cnmt8_c")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 8_順序回憶_跳出畫面 \r\n");
                cnmt8_c();
            }
            if (temp1 == "cnmt8_c_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 8_順序回憶_迴圈 \r\n");
                cnmt8_c_2();
            }
            if (temp1 == "cnmt8_d")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 8_再認_跳出畫面 \r\n");
                cnmt8_d();
            }
            if (temp1 == "cnmt8_d_2")  // cnmt 
            {
                temp1 = "0";
                textBox2.AppendText("cnmt 8_再認_迴圈 \r\n");
                cnmt8_d_2();
            }
        }





    }
    public class FormParameter  //用來動TEXTBOX3 資料    https://dotblogs.com.tw/killysss/2010/09/15/17746
    {
        //測試參數
        private string _FuncNum;
        public string FuncNum
        {
            set { _FuncNum = value; }
            get { return _FuncNum; }
        }

    }
}
