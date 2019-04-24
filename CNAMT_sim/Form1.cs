using System;
using System.Diagnostics;
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
using System.Runtime.InteropServices;


namespace CNAMT_sim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] serialPorts = SerialPort.GetPortNames();
            foreach (string serialPort in serialPorts)
                comboBox1.Items.Add(serialPort);

        }

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
        }

        public void AddData()//從把手讀資料
        {
            string dataLine = serialPort1.ReadLine();//dataline指讀進來

            textBox1.AppendText("dataline=");
            textBox1.AppendText(dataLine);
            textBox1.AppendText("\r\n");
            /*textBox1.AppendText("serialPort1.ReadLine()=");
            textBox1.AppendText(serialPort1.ReadLine());
            textBox1.AppendText("\r\n");*/

        }

        private void button3_Click(object sender, EventArgs e)// 燈號面板
        {

            //Form2_panel panel = new Form2_panel(); //創建子視窗
            panel.Visible = true;//顯示第二個視窗


        }

        private void button4_Click(object sender, EventArgs e)//ID 面板
        {
            Form3_id form3 = new Form3_id(); //創建子視窗
            form3.Visible = true;//顯示第二個視窗
        }

        //-------------------------------------------------------------------------------------------------------------------

        // start back ground code
        Form2_panel panel = new Form2_panel();//創建子視窗

        //delay01[test][當次測驗第幾次燈泡亮] = 本次燈亮所需延遲
        double[][] delay01 = new double[][]
       {
        new double[] {1.6, 2.4, 3.2, 1, 3.8, 2.6, 4.4, 1.2, 4.2, 2.8, 3.6, 2, 1.8, 3.4, 3, 4, 2.2, 1.4},
        new double[] {3.4, 1.8, 4.4, 1.2, 2.6, 4.2, 2.2, 1.6, 1, 3.6, 2.4, 3.2, 4, 1.4, 3.8, 2, 3, 2.8},
        new double[] {2.7, 1.6, 2.4, 3.2, 1, 3.8, 2.6, 4.4, 1.2, 4.2, 2.8, 3.6, 2, 1.8, 3.4, 3, 4, 2, 1.4, 2.7},
        new double[] {2.7, 3.4, 1.8, 4.4, 1.2, 2.6, 4.2, 2.2, 1.6, 3, 3.6, 2.4, 3.2, 4, 1.4, 2, 3.8, 1, 2.8, 2.7},
        new double[] {2.7, 1.6, 2.4, 3.2, 1, 3.8, 2.6, 4.4, 1.2, 4.2, 2.8, 3.6, 2, 1.8, 3.4, 3, 4, 2.2, 1.4, 3.4, 1.8, 4.4, 1.2, 2.6, 4.2, 2.2, 1.6, 3, 3.6, 2.4, 3.2, 4, 1.4, 2, 3.8, 1, 2.8, 2.7},
        new double[] {2.7, 3.4, 1.8, 4.4, 1.2, 2.6, 4.2, 2.2, 1.6, 3, 3.6, 2.4, 3.2, 4, 1.4, 2, 3.8, 1, 2.8, 1.6, 2.4, 3.2, 1, 3.8, 2.6, 4.4, 1.2, 4.2, 2.8, 3.6, 2, 1.8, 3.4, 3, 4, 2.2, 1.4, 2.7},
        new double[] {2.7, 3.4, 1.8, 4.4, 1.2, 2.6, 4.2, 2.2, 1.6, 3, 3.6, 2.4, 3.2, 4, 1.4, 2, 3.8, 1, 2.8, 1.6, 2.4, 3.2, 1, 3.8, 2.6, 4.4, 1.2, 4.2, 2.8, 3.6, 2, 1.8, 3.4, 3, 4, 2.2, 1.4, 2.7},
        new double[] {2.7, 1.6, 2.4, 3.2, 1, 3.8, 2.6, 4.4, 1.2, 4.2, 2.8, 3.6, 2, 1.8, 3.4, 3, 4, 2.2, 1.4, 3.4, 1.8, 4.4, 1.2, 2.6, 4.2, 2.2, 1.6, 3, 3.6, 2.4, 3.2, 4, 1.4, 2, 3.8, 1, 2.8, 2.7},
        new double[] {2.7, 1.6, 2.4, 3.2, 1, 3.8, 2.6, 4.4, 1.2, 4.2, 2.8, 3.6, 2, 1.8, 3.4, 3, 4, 2.2, 1.4, 3.4, 1.8, 4.4, 1.2, 2.6, 4.2, 2.2, 1.6, 3, 3.6, 2.4, 3.2, 4, 1.4, 2, 3.8, 1, 2.8, 2.7},
        new double[] {2.7, 3.4, 1.8, 4.4, 1.2, 2.6, 4.2, 2.2, 1.6, 3, 3.6, 2.4, 3.2, 4, 1.4, 2, 3.8, 1, 2.8, 1.6, 2.4, 3.2, 1, 3.8, 2.6, 4.4, 1.2, 4.2, 2.8, 3.6, 2, 1.8, 3.4, 3, 4, 2.2, 1.4, 2.7}
       };

        /*int[][] light01 = new int[][]
        {
        new int[] {45, 45, 45, 45, 45, 45, 45, 45, 45, 45, 45, 45, 45, 45, 45, 45, 45, 45},
        new int[] {5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5},
        new int[] {133, 69, 43, 68, 57, 49, 61, 52, 71, 50, 74, 64, 47, 75, 45, 65, 44, 54, 62, 134},
        new int[] {124, 20, 16, 20, 11, 13, 36, 19, 18, 30, 32, 1, 8, 6, 27, 26, 33, 23, 15, 124},
        new int[] {133, 69, 83, 108, 97, 49, 101, 52, 71, 90, 74, 104, 87, 75, 85, 65, 84, 94, 62, 60, 96, 42, 51, 93, 76, 99, 98, 110, 72, 81, 48, 46, 107, 66, 73, 63, 95, 134},
        new int[] {124, 20, 16, 2, 11, 13, 36, 19, 18, 30, 32, 1, 8, 6, 27, 26, 33, 23, 15, 29, 3, 28, 17, 9, 21, 12, 31, 10, 34, 24, 7, 35, 5, 25, 4, 14, 22, 124},
        new int[] {124, 20, 16, 2, 11, 13, 36, 19, 18, 30, 32, 1, 8, 6, 27, 26, 33, 23, 15, 29, 3, 28, 17, 9, 21, 12, 31, 10, 34, 24, 7, 35, 5, 25, 4, 14, 22, 124},
        new int[] {133, 69, 83, 108, 97, 49, 101, 52, 71, 90, 74, 104, 87, 75, 85, 65, 84, 94, 62, 60, 96, 42, 51, 93, 76, 99, 98, 110, 72, 81, 48, 46, 107, 66, 73, 63, 95, 134},
        new int[] {133, 69, 83, 108, 97, 49, 101, 52, 71, 90, 74, 104, 87, 75, 85, 65, 84, 94, 62, 60, 96, 42, 51, 93, 76, 99, 98, 110, 72, 81, 48, 46, 107, 66, 73, 63, 95, 134},
        new int[] {124, 20, 16, 2, 11, 13, 36, 19, 18, 30, 32, 1, 8, 6, 27, 26, 33, 23, 15, 29, 3, 28, 17, 9, 21, 12, 31, 10, 34, 24, 7, 35, 5, 25, 4, 14, 22, 124 }
        };
        //light01[test][當次測驗第幾次燈泡亮] = 哪種顆要亮		1-40綠燈 41-80紅燈 81-120兩燈 可再做調整
        int[] times01 = new int[] { 18, 18, 20, 20, 38, 38, 38, 38, 38, 38 };
        //times01[test] = 當次測驗燈泡亮的總次數*/

            //儲存燈號用  應該直接映射燈號
        public static string[] redlight = new string[39] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" }; 
        public static string[] greenlight = new string[43] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0","0", "0", "0", "0" };

        private void Button9_Click(object sender, EventArgs e)
        {
            redlight[1] = "1";
            redlight[38] = "1";
            greenlight[1] = "1";
            greenlight[42] = "1";
            textBox1.AppendText("redlight[1] = ");
            textBox1.AppendText(redlight[1]);
            panel.Lightchange();
        }

        private void panel_light_change() //用於同步燈號用的方程式  1.偵查redlight/greenlight有無變動  動了就change   
        {

        }


        /*private void Button9_Click(object sender, EventArgs e) form1 變動更改form2的紀錄
        {
            redlight[1] = "1";
            textBox1.AppendText("redlight[1] = ");
            textBox1.AppendText(redlight[1]);
            panel.Lightchange();
        }*/

    }
}
