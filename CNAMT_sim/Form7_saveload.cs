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
    public partial class Form7_saveload : Form
    {
        public Form7_saveload()
        {
            InitializeComponent();
        }

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


        int[][] resulttimeread = new int[][] //結果存這   單位: MS  //最後面2個是等待&&燈亮
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
            new int[38]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0, 0, 0, 0, 0,0, 0, 0, 0, 0, 0 }//9
        };

        //0 代表沒按 1右手 2左手
        int[] leftright = new int[74] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //左右手的反應   理論上只有七會錯手  只有7要記
        int[] leftrightcorrect = new int[74] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2,1, 2 }; //左右手的反應   理論上只有七會錯手  只有7要記


        double[][] forsd1 = new double[][] //結果存這   單位: MS
{
            new double[]{ },//0
            new double[20]{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},//1
            new double[20]{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},//2
            new double[20]{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},//3
            new double[20]{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},//4
            new double[38]{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},//5
            new double[38]{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},//6
            new double[74]{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},//7
            new double[38]{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},//8
            new double[38]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0, 0, 0, 0, 0,0, 0, 0, 0, 0, 0 }//9
};

        //delay[test][當次測驗第幾次燈泡亮] = 本次燈亮所需延遲
        int[][] delaycnat = new int[][]
       {
        new int[0] {},//0
        new int[18] {1600, 2400, 3200, 1000, 3800, 2600, 4400, 1200, 4200, 2800, 3600, 2000, 1800, 3400, 3000, 4000, 2200, 1400},//1
        new int[18] {3400, 1800, 4400, 1200, 2600, 4200, 2200, 1600, 1000, 3600, 2400, 3200, 4000, 1400, 3800, 2000, 3000, 2800},//2
        new int[20-2] {1600, 2400, 3200, 1000, 3800, 2600, 4400, 1200, 4200, 2800, 3600, 2000, 1800, 3400, 3000, 4000, 2000, 1400},//3
        new int[20-2] {3400, 1800, 4400, 1200, 2600, 4200, 2200, 1600, 3000, 3600, 2400, 3200, 4000, 1400, 2000, 3800, 1000, 2800},//4
        new int[38-2] {1600, 2400, 3200, 1000, 3800, 2600, 4400, 1200, 4200, 2800, 3600, 2000, 1800, 3400, 3000, 4000, 2200, 1400, 3400, 1800, 4400, 1200, 2600, 4200, 2200, 1600, 3000, 3600, 2400, 3200, 4000, 1400, 2000, 3800, 1000, 2800},//5
        new int[38-2] {3400, 1800, 4400, 1200, 2600, 4200, 2200, 1600, 3000, 3600, 2400, 3200, 4000, 1400, 2000, 3800, 1000, 2800, 1600, 2400, 3200, 1000, 3800, 2600, 4400, 1200, 4200, 2800, 3600, 2000, 1800, 3400, 3000, 4000, 2200, 1400},//6
        new int[74-2] {3400, 1600, 1800, 2400, 4400, 3200, 1200, 1000, 2600, 3800, 4200, 2600, 2200, 4400, 1600, 1200, 3000, 4200, 3600, 2800, 2400, 3600, 3200, 2000, 4000, 1800, 1400, 3400, 2000, 3000, 3800, 4000, 1000, 2200, 2800, 1400, 1600, 3400, 2400, 1800, 3200, 4400, 1000, 1200, 3800, 2600, 2600, 4200, 4400, 2200, 1200, 1600, 4200, 3000, 2800, 3600, 3600, 2400, 2000, 3200, 1800, 4000, 3400, 1400, 3000, 2000, 4000, 3800, 2200, 1000, 1400, 2800},//7
        new int[38-2] {1600, 2400, 3200, 1000, 3800, 2600, 4400, 1200, 4200, 2800, 3600, 2000, 1800, 3400, 3000, 4000, 2200, 1400, 3400, 1800, 4400, 1200, 2600, 4200, 2200, 1600, 3000, 3600, 2400, 3200, 4000, 1400, 2000, 3800, 1000, 2800},//8
        new int[38-2] {3400, 1800, 4400, 1200, 2600, 4200, 2200, 1600, 3000, 3600, 2400, 3200, 4000, 1400, 2000, 3800, 1000, 2800, 1600, 2400, 3200, 1000, 3800, 2600, 4400, 1200, 4200, 2800, 3600, 2000, 1800, 3400, 3000, 4000, 2200, 1400},//9
       };

        int toint;
        string fromfile;//讀起來地1
        string fromfile2;//左右手
        string fromfile3;//7讀1個
        int tofile1;
        int loadbig;

        public void cnat1load()//要在這裡回填
        {
            loadbig = 1;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select file";
            dialog.InitialDirectory = ".\\";
            dialog.Filter = "txt files (*.*)|*.txt";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show(dialog.FileName);
            }
            else
            {
                goto here;
            }

            StreamReader str = new StreamReader(dialog.FileName);

            for (int i = 0; i < 20; i++)
            {
                fromfile = str.ReadLine();
                //textBox1.AppendText("fromfile= " + fromfile + " \r\n");
                if (fromfile == "-1")
                {
                    //fromfile2 = "0000";//-1讀到
                    //fromfile2 = "-1";//-1讀到
                    tofile1 = -1;
                    //textBox1.AppendText("fromfile2= " + fromfile2 + " \r\n");
                }
                else
                {
                    //fromfile2 = fromfile.Substring(0, 4);
                    tofile1 = Int32.Parse(fromfile.Substring(0, 4));
                    //textBox1.AppendText("fromfile2= " + fromfile2 + " \r\n");
                }//跑18次+後面2次是等待時間+燈亮時間  //要讀20條
                //toint = Int32.Parse(fromfile2);
                resulttimeread[loadbig][i] = tofile1;//存form7

                Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
                form1.readfromsaveload(tofile1, loadbig, i);//存form1
            }//跑18次+後面2次是等待時間+燈亮時間  //要讀20條
            str.Close();

            here:;
        }//讀取CNAT?測驗檔案
        public void cnat2load()//要在這裡回填
        {
            loadbig = 2;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select file";
            dialog.InitialDirectory = ".\\";
            dialog.Filter = "txt files (*.*)|*.txt";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show(dialog.FileName);
            }
            else
            {
                goto here;
            }

            StreamReader str = new StreamReader(dialog.FileName);

            for (int i = 0; i < 20; i++)
            {
                fromfile = str.ReadLine();
                if (fromfile == "-1")
                {
                    tofile1 = -1;
                }
                else
                {
                    tofile1 = Int32.Parse(fromfile.Substring(0, 4));
                }
                resulttimeread[loadbig][i] = tofile1;//存form7
                Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
                form1.readfromsaveload(tofile1, loadbig, i);//存form1
            }//跑18次+後面2次是等待時間+燈亮時間  //要讀20條
            str.Close();

            here:;
        }//讀取CNAT?測驗檔案
        public void cnat3load()
        {
            loadbig = 3;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select file";
            dialog.InitialDirectory = ".\\";
            dialog.Filter = "txt files (*.*)|*.txt";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
               // MessageBox.Show(dialog.FileName);
            }
            else
            {
                goto here;
            }

            StreamReader str = new StreamReader(dialog.FileName);

            for (int i = 0; i < 20; i++)
            {
                fromfile = str.ReadLine();
                if (fromfile == "-1")
                {
                    tofile1 = -1;
                }
                else
                {
                    tofile1 = Int32.Parse(fromfile.Substring(0, 4));
                }
                resulttimeread[loadbig][i] = tofile1;//存form7
            }//跑18次+後面2次是等待時間+燈亮時間  //要讀20條

            for(int i = 1; i<19; i++)
            {
                int k;
                Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
                k = resulttimeread[loadbig][i-1];

                form1.readfromsaveload(k, loadbig, i);//存form1
            }

            str.Close();

            here:;
        }
        public void cnat4load()
        {
            loadbig = 4;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select file";
            dialog.InitialDirectory = ".\\";
            dialog.Filter = "txt files (*.*)|*.txt";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // MessageBox.Show(dialog.FileName);
            }
            else
            {
                goto here;
            }

            StreamReader str = new StreamReader(dialog.FileName);

            for (int i = 0; i < 20; i++)
            {
                fromfile = str.ReadLine();
                if (fromfile == "-1")
                {
                    tofile1 = -1;
                }
                else
                {
                    tofile1 = Int32.Parse(fromfile.Substring(0, 4));
                }
                resulttimeread[loadbig][i] = tofile1;//存form7
            }//跑18次+後面2次是等待時間+燈亮時間  //要讀20條

            for (int i = 1; i < 19; i++)
            {
                int k;
                Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
                k = resulttimeread[loadbig][i-1];

                form1.readfromsaveload(k, loadbig, i);//存form1
            }

            str.Close();

            here:;
        }
        public void cnat5load()
        {
            loadbig = 5;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select file";
            dialog.InitialDirectory = ".\\";
            dialog.Filter = "txt files (*.*)|*.txt";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // MessageBox.Show(dialog.FileName);
            }
            else
            {
                goto here;
            }

            StreamReader str = new StreamReader(dialog.FileName);

            for (int i = 0; i < 38; i++)
            {
                fromfile = str.ReadLine();
                if (fromfile == "-1")
                {
                    tofile1 = -1;
                }
                else
                {
                    tofile1 = Int32.Parse(fromfile.Substring(0, 4));
                }
                resulttimeread[loadbig][i] = tofile1;//存form7
            }//跑18次+後面2次是等待時間+燈亮時間  //要讀20條

            for (int i = 1; i < 37; i++)
            {
                int k;
                Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
                k = resulttimeread[loadbig][i-1];

                form1.readfromsaveload(k, loadbig, i);//存form1
            }

            str.Close();

            here:;
        }
        public void cnat6load()
        {
            loadbig = 6;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select file";
            dialog.InitialDirectory = ".\\";
            dialog.Filter = "txt files (*.*)|*.txt";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // MessageBox.Show(dialog.FileName);
            }
            else
            {
                goto here;
            }

            StreamReader str = new StreamReader(dialog.FileName);

            for (int i = 0; i < 38; i++)
            {
                fromfile = str.ReadLine();
                if (fromfile == "-1")
                {
                    tofile1 = -1;
                }
                else
                {
                    tofile1 = Int32.Parse(fromfile.Substring(0, 4));
                }
                resulttimeread[loadbig][i] = tofile1;//存form7
            }//跑18次+後面2次是等待時間+燈亮時間  //要讀20條

            for (int i = 1; i < 37; i++)
            {
                int k;
                Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
                k = resulttimeread[loadbig][i-1];

                form1.readfromsaveload(k, loadbig, i);//存form1
            }

            str.Close();

            here:;
        }
        public void cnat7load()
        {
            loadbig = 7;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select file";
            dialog.InitialDirectory = ".\\";
            dialog.Filter = "txt files (*.*)|*.txt";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // MessageBox.Show(dialog.FileName);
            }
            else
            {
                goto here;
            }

            StreamReader str = new StreamReader(dialog.FileName);

            for (int i = 0; i < 72; i++)
            {
                fromfile = str.ReadLine();
                if (fromfile == "-1")
                {
                    tofile1 = -1;
                }
                else
                {
                    tofile1 = Int32.Parse(fromfile.Substring(0, 4));

                    //存LR到from7
                    //fromfile.Substring(7, 1);  轉成12  //右1左2  R=1  L=2
                    fromfile2 = fromfile.Substring(7, 1);   //左右存到form7   //不用存回form1
                    if (fromfile2 == "R")
                    { leftright[i] = 1; }
                    if (fromfile2 == "L")
                    { leftright[i] = 2; }

                }
                resulttimeread[loadbig][i] = tofile1;//存form7
            }//跑18次+後面2次是等待時間+燈亮時間  //要讀20條

            for (int i = 1; i < 73; i++)
            {
                int k;
                Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
                k = resulttimeread[loadbig][i - 1];

                form1.readfromsaveload(k, loadbig, i);//存form1

            }

            str.Close();

            here:;
        }
        public void cnat8load()
        {
            loadbig = 8;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select file";
            dialog.InitialDirectory = ".\\";
            dialog.Filter = "txt files (*.*)|*.txt";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // MessageBox.Show(dialog.FileName);
            }
            else
            {
                goto here;
            }

            StreamReader str = new StreamReader(dialog.FileName);

            for (int i = 0; i < 38; i++)
            {
                fromfile = str.ReadLine();
                if (fromfile == "-1")
                {
                    tofile1 = -1;
                }
                else
                {
                    tofile1 = Int32.Parse(fromfile.Substring(0, 4));
                }
                resulttimeread[loadbig][i] = tofile1;//存form7
            }//跑18次+後面2次是等待時間+燈亮時間  //要讀20條

            for (int i = 1; i < 37; i++)
            {
                int k;
                Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
                k = resulttimeread[loadbig][i - 1];

                form1.readfromsaveload(k, loadbig, i);//存form1
            }

            str.Close();

            here:;
        }
        public void cnat9load()
        {
            loadbig = 9;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select file";
            dialog.InitialDirectory = ".\\";
            dialog.Filter = "txt files (*.*)|*.txt";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // MessageBox.Show(dialog.FileName);
            }
            else
            {
                goto here;
            }

            StreamReader str = new StreamReader(dialog.FileName);

            for (int i = 0; i < 38; i++)
            {
                fromfile = str.ReadLine();
                if (fromfile == "-1")
                {
                    tofile1 = -1;
                }
                else
                {
                    tofile1 = Int32.Parse(fromfile.Substring(0, 4));
                }
                resulttimeread[loadbig][i] = tofile1;//存form7
            }//跑18次+後面2次是等待時間+燈亮時間  //要讀20條

            for (int i = 1; i < 37; i++)
            {
                int k;
                Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
                k = resulttimeread[loadbig][i - 1];

                form1.readfromsaveload(k, loadbig, i);//存form1
            }

            str.Close();

            here:;
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            cnat1load();
            button1.Enabled = false;
        }


        private void Button2_Click(object sender, EventArgs e)  // 存檔
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            int cnat1_missing = 0;
            int cnat1_impulsive = 0;
            int cnat1_sd_count = 0;
            int cnat1_sd_sum = 0;
            int cnat1_delay = 0;
            int cnat1_correct= 0;
            int cnat1_correct_sum = 0;
            double cnat1_correct_avg = 0;
            double cnat1_sd_avg = 0;
            double cnat1_sd_ts = 0;
            double cnat1_sd_final = 0;

            int cnat2_missing = 0;
            int cnat2_impulsive = 0;
            int cnat2_sd_count = 0;
            int cnat2_sd_sum = 0;
            int cnat2_delay = 0;
            int cnat2_correct = 0;
            int cnat2_correct_sum = 0;
            double cnat2_correct_avg = 0;
            double cnat2_sd_avg = 0;
            double cnat2_sd_ts = 0;
            double cnat2_sd_final = 0;

            int cnat3_missing = 0;
            int cnat3_impulsive = 0;
            int cnat3_sd_count = 0;
            int cnat3_sd_sum = 0;
            int cnat3_delay = 0;
            int cnat3_correct = 0;
            int cnat3_correct_sum = 0;
            double cnat3_correct_avg = 0;
            double cnat3_sd_avg = 0;
            double cnat3_sd_ts = 0;
            double cnat3_sd_final = 0;

            int cnat4_missing = 0;
            int cnat4_impulsive = 0;
            int cnat4_sd_count = 0;
            int cnat4_sd_sum = 0;
            int cnat4_delay = 0;
            int cnat4_correct = 0;
            int cnat4_correct_sum = 0;
            double cnat4_correct_avg = 0;
            double cnat4_sd_avg = 0;
            double cnat4_sd_ts = 0;
            double cnat4_sd_final = 0;

            int cnat5_missing = 0;
            int cnat5_impulsive = 0;
            int cnat5_sd_count = 0;
            int cnat5_sd_sum = 0;
            int cnat5_delay = 0;
            int cnat5_correct = 0;
            int cnat5_correct_sum = 0;
            int cnat5_commission = 0;
            double cnat5_correct_avg = 0;
            double cnat5_sd_avg = 0;
            double cnat5_sd_ts = 0;
            double cnat5_sd_final = 0;


            int cnat6_missing = 0;
            int cnat6_impulsive = 0;
            int cnat6_sd_count = 0;
            int cnat6_sd_sum = 0;
            int cnat6_delay = 0;
            int cnat6_correct = 0;
            int cnat6_correct_sum = 0;
            int cnat6_commission = 0;
            double cnat6_correct_avg = 0;
            double cnat6_sd_avg = 0;
            double cnat6_sd_ts = 0;
            double cnat6_sd_final = 0;


            int cnat7_missing = 0;
            int cnat7_impulsive = 0;
            int cnat7_sd_count = 0;
            int cnat7_sd_sum = 0;
            int cnat7_delay = 0;
            int cnat7_correct = 0;
            int cnat7_correct_sum = 0;
            int cnat7_commission = 0;
            double cnat7_correct_avg = 0;
            double cnat7_sd_avg = 0;
            double cnat7_sd_ts = 0;
            double cnat7_sd_final = 0;


            int cnat8_missing = 0;
            int cnat8_impulsive = 0;
            int cnat8_sd_count = 0;
            int cnat8_sd_sum = 0;
            int cnat8_delay = 0;
            int cnat8_correct = 0;
            int cnat8_correct_sum = 0;
            int cnat8_commission = 0;
            double cnat8_correct_avg = 0;
            double cnat8_sd_avg = 0;
            double cnat8_sd_ts = 0;
            double cnat8_sd_final = 0;



            int cnat9_missing = 0;
            int cnat9_impulsive = 0;
            int cnat9_sd_count = 0;
            int cnat9_sd_sum = 0;
            int cnat9_delay = 0;
            int cnat9_correct = 0;
            int cnat9_correct_sum = 0;
            int cnat9_commission = 0;
            double cnat9_correct_avg = 0;
            double cnat9_sd_avg = 0;
            double cnat9_sd_ts = 0;
            double cnat9_sd_final = 0;





            int cnatlocationbig = 0;



            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    StreamWriter wText = new StreamWriter(myStream);

                    //以上都是純粹存檔用
                    //計算放這
                    //cnat1 標準差
                    //


                    //測驗1
                    cnatlocationbig = 1;
                    wText.WriteLine("cnat測驗"+ cnatlocationbig);

                    for (int i = 0; i < 18; i++)
                    {
                        if (resulttimeread[cnatlocationbig][i] == -1)//沒按  missing error
                        {
                            cnat1_missing = cnat1_missing + 1;
                        }
                        else if (resulttimeread[cnatlocationbig][i] < delaycnat[cnatlocationbig][i] + 100)//太快 impulsive
                        {
                            cnat1_impulsive = cnat1_impulsive + 1;
                        }
                        else
                        {
                            forsd1[cnatlocationbig][cnat1_sd_count] = resulttimeread[cnatlocationbig][i];//把成功的放入forsd裡面供之後計算
                            cnat1_sd_count = cnat1_sd_count + 1; //記述++
                            cnat1_sd_sum = cnat1_sd_sum + resulttimeread[cnatlocationbig][i] - delaycnat[cnatlocationbig][i];//算平均值用++
                        }
                    }//計算SD用

                    if(cnat1_sd_count == 0)
                    {
                        cnat1_sd_count = 1;
                    }//防BUG

                    cnat1_sd_avg = cnat1_sd_sum / cnat1_sd_count;

                    for (int i = 0; i < cnat1_sd_count; i++)//求差平方
                    {
                        cnat1_sd_ts = cnat1_sd_ts + (forsd1[cnatlocationbig][i] - cnat1_sd_avg) * (forsd1[cnatlocationbig][i] - cnat1_sd_avg);
                    }

                    cnat1_sd_final = Math.Sqrt(cnat1_sd_ts / cnat1_sd_count);

                    cnat1_missing = 0;
                    cnat1_impulsive = 0;


                    for (int i = 0; i < 18; i++)
                    {
                        if (resulttimeread[cnatlocationbig][i] == -1)//沒按  missing error
                        {
                            cnat1_missing = cnat1_missing + 1;
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    missing");
                        }
                        else if (resulttimeread[cnatlocationbig][i] < delaycnat[cnatlocationbig][i] + 100)//太快 impulsive
                        {
                            cnat1_impulsive = cnat1_impulsive + 1;
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    impulsive");
                        }
                        else if (resulttimeread[cnatlocationbig][i] > cnat1_sd_avg + 2.5 * cnat1_sd_final)//遲緩  算標準差
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    delay");
                            cnat1_delay = cnat1_delay + 1;
                        }
                        else//正常
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] );
                            cnat1_correct = cnat1_correct + 1;
                            cnat1_correct_sum = cnat1_correct_sum + resulttimeread[cnatlocationbig][i] - delaycnat[cnatlocationbig][i];
                        }
                    }//輸出到txt  同時記錄 cnat1_delay 數量
                    if (cnat1_correct == 0)//防BUG
                    { cnat1_correct = 1; }
                    cnat1_correct_avg = cnat1_correct_sum / cnat1_correct;
                    wText.WriteLine("missing: "+ cnat1_missing );
                    wText.WriteLine("impulsive: " + cnat1_impulsive);
                    wText.WriteLine("delay: " + cnat1_delay);
                    wText.WriteLine("correct: " + cnat1_correct);
                    wText.WriteLine("correct_avg: " + cnat1_correct_avg);
                    wText.WriteLine("" );





                    //測驗2
                    cnatlocationbig = 2;
                    wText.WriteLine("cnat測驗"+ cnatlocationbig);

                    for (int i = 0; i < 18; i++)
                    {
                        if (resulttimeread[cnatlocationbig][i] == -1)//沒按  missing error
                        {
                            cnat2_missing = cnat2_missing + 1;
                        }
                        else if (resulttimeread[cnatlocationbig][i] < delaycnat[cnatlocationbig][i] + 100)//太快 impulsive
                        {
                            cnat2_impulsive = cnat2_impulsive + 1;
                        }
                        else
                        {
                            forsd1[cnatlocationbig][cnat2_sd_count] = resulttimeread[cnatlocationbig][i];//把成功的放入forsd裡面供之後計算
                            cnat2_sd_count = cnat2_sd_count + 1; //記述++
                            cnat2_sd_sum = cnat2_sd_sum + resulttimeread[cnatlocationbig][i] - delaycnat[cnatlocationbig][i];//算平均值用++
                        }
                    }//計算SD用

                    if (cnat2_sd_count == 0)
                    {
                        cnat2_sd_count = 1;
                    }//防BUG

                    cnat2_sd_avg = cnat2_sd_sum / cnat2_sd_count;

                    for (int i = 0; i < cnat2_sd_count; i++)//求差平方
                    {
                        cnat2_sd_ts = cnat2_sd_ts + (forsd1[cnatlocationbig][i] - cnat2_sd_avg) * (forsd1[cnatlocationbig][i] - cnat2_sd_avg);
                    }

                    cnat2_sd_final = Math.Sqrt(cnat2_sd_ts / cnat2_sd_count);

                    cnat2_missing = 0;
                    cnat2_impulsive = 0;


                    for (int i = 0; i < 18; i++)
                    {
                        if (resulttimeread[cnatlocationbig][i] == -1)//沒按  missing error
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    missing");
                            cnat2_missing = cnat2_missing + 1;
                        }
                        else if (resulttimeread[cnatlocationbig][i] < delaycnat[cnatlocationbig][i] + 100)//太快 impulsive
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    impulsive");
                            cnat2_impulsive = cnat2_impulsive + 1;
                        }
                        else if (resulttimeread[cnatlocationbig][i] > cnat2_sd_avg + 2.5 * cnat2_sd_final)//遲緩  算標準差
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    delay");
                            cnat2_delay = cnat2_delay + 1;
                        }
                        else//正常
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i]);
                            cnat2_correct = cnat2_correct + 1;
                            cnat2_correct_sum = cnat2_correct_sum + resulttimeread[cnatlocationbig][i] - delaycnat[cnatlocationbig][i];
                        }
                    }//輸出到txt  同時記錄 cnat2_delay 數量
                    if (cnat2_correct == 0)//防BUG
                    { cnat2_correct = 1; }
                    cnat2_correct_avg = cnat2_correct_sum / cnat2_correct;
                    wText.WriteLine("missing: " + cnat2_missing);
                    wText.WriteLine("impulsive: " + cnat2_impulsive);
                    wText.WriteLine("delay: " + cnat2_delay);
                    wText.WriteLine("correct: " + cnat2_correct);
                    wText.WriteLine("correct_avg: " + cnat2_correct_avg);
                    wText.WriteLine("");






                    //測驗3
                    cnatlocationbig = 3;
                    wText.WriteLine("cnat測驗" + cnatlocationbig);

                    for (int i = 0; i < 18; i++)
                    {
                        if (resulttimeread[cnatlocationbig][i] == -1)//沒按  missing error
                        {
                            cnat3_missing = cnat3_missing + 1;
                        }
                        else if (resulttimeread[cnatlocationbig][i] < delaycnat[cnatlocationbig][i] + 100)//太快 impulsive
                        {
                            cnat3_impulsive = cnat3_impulsive + 1;
                        }
                        else
                        {
                            forsd1[cnatlocationbig][cnat3_sd_count] = resulttimeread[cnatlocationbig][i];//把成功的放入forsd裡面供之後計算
                            cnat3_sd_count = cnat3_sd_count + 1; //記述++
                            cnat3_sd_sum = cnat3_sd_sum + resulttimeread[cnatlocationbig][i] - delaycnat[cnatlocationbig][i];//算平均值用++
                        }
                    }//計算SD用

                    if (cnat3_sd_count == 0)
                    {
                        cnat3_sd_count = 1;
                    }//防BUG

                    cnat3_sd_avg = cnat3_sd_sum / cnat3_sd_count;

                    for (int i = 0; i < cnat3_sd_count; i++)//求差平方
                    {
                        cnat3_sd_ts = cnat3_sd_ts + (forsd1[cnatlocationbig][i] - cnat3_sd_avg) * (forsd1[cnatlocationbig][i] - cnat3_sd_avg);
                    }

                    cnat3_sd_final = Math.Sqrt(cnat3_sd_ts / cnat3_sd_count);

                    cnat3_missing = 0;
                    cnat3_impulsive = 0;

                    for (int i = 0; i < 18; i++)
                    {
                        if (resulttimeread[cnatlocationbig][i] == -1)//沒按  missing error
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    missing");
                            cnat3_missing = cnat3_missing + 1;
                        }
                        else if (resulttimeread[cnatlocationbig][i] < delaycnat[cnatlocationbig][i] + 100)//太快 impulsive
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    impulsive");
                            cnat3_impulsive = cnat3_impulsive + 1;
                        }
                        else if (resulttimeread[cnatlocationbig][i] > cnat3_sd_avg + 2.5 * cnat3_sd_final)//遲緩  算標準差
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    delay");
                            cnat3_delay = cnat3_delay + 1;
                        }
                        else//正常
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i]);
                            cnat3_correct = cnat3_correct + 1;
                            cnat3_correct_sum = cnat3_correct_sum + resulttimeread[cnatlocationbig][i] - delaycnat[cnatlocationbig][i];
                        }
                    }//輸出到txt  同時記錄 cnat3_delay 數量
                    if (cnat3_correct == 0)//防BUG
                    { cnat3_correct = 1; }
                    cnat3_correct_avg = cnat3_correct_sum / cnat3_correct;
                    wText.WriteLine("missing: " + cnat3_missing);
                    wText.WriteLine("impulsive: " + cnat3_impulsive);
                    wText.WriteLine("delay: " + cnat3_delay);
                    wText.WriteLine("correct: " + cnat3_correct);
                    wText.WriteLine("correct_avg: " + cnat3_correct_avg);
                    wText.WriteLine("");









                    //測驗4
                    cnatlocationbig = 4;
                    wText.WriteLine("cnat測驗" + cnatlocationbig);

                    for (int i = 0; i < 18; i++)
                    {
                        if (resulttimeread[cnatlocationbig][i] == -1)//沒按  missing error
                        {
                            cnat4_missing = cnat4_missing + 1;
                        }
                        else if (resulttimeread[cnatlocationbig][i] < delaycnat[cnatlocationbig][i] + 100)//太快 impulsive
                        {
                            cnat4_impulsive = cnat4_impulsive + 1;
                        }
                        else
                        {
                            forsd1[cnatlocationbig][cnat4_sd_count] = resulttimeread[cnatlocationbig][i];//把成功的放入forsd裡面供之後計算
                            cnat4_sd_count = cnat4_sd_count + 1; //記述++
                            cnat4_sd_sum = cnat4_sd_sum + resulttimeread[cnatlocationbig][i] - delaycnat[cnatlocationbig][i];//算平均值用++
                        }
                    }//計算SD用

                    if (cnat4_sd_count == 0)
                    {
                        cnat4_sd_count = 1;
                    }//防BUG

                    cnat4_sd_avg = cnat4_sd_sum / cnat4_sd_count;


                    for (int i = 0; i < cnat4_sd_count; i++)//求差平方
                    {
                        cnat4_sd_ts = cnat4_sd_ts + (forsd1[cnatlocationbig][i] - cnat4_sd_avg) * (forsd1[cnatlocationbig][i] - cnat4_sd_avg);
                    }

                    cnat4_sd_final = Math.Sqrt(cnat4_sd_ts / cnat4_sd_count);

                    cnat4_missing = 0;
                    cnat4_impulsive = 0;


                    for (int i = 0; i < 18; i++)
                    {
                        if (resulttimeread[cnatlocationbig][i] == -1)//沒按  missing error
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    missing");
                            cnat4_missing = cnat4_missing + 1;
                        }
                        else if (resulttimeread[cnatlocationbig][i] < delaycnat[cnatlocationbig][i] + 100)//太快 impulsive
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    impulsive");
                            cnat4_impulsive = cnat4_impulsive + 1;
                        }
                        else if (resulttimeread[cnatlocationbig][i] > cnat4_sd_avg + 2.5 * cnat4_sd_final)//遲緩  算標準差
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    delay");
                            cnat4_delay = cnat4_delay + 1;
                        }
                        else//正常
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i]);
                            cnat4_correct = cnat4_correct + 1;
                            cnat4_correct_sum = cnat4_correct_sum + resulttimeread[cnatlocationbig][i] - delaycnat[cnatlocationbig][i];
                        }
                    }//輸出到txt  同時記錄 cnat4_delay 數量
                    if (cnat4_correct == 0)//防BUG
                    { cnat4_correct = 1; }
                    cnat4_correct_avg = cnat4_correct_sum / cnat4_correct;
                    wText.WriteLine("missing: " + cnat4_missing);
                    wText.WriteLine("impulsive: " + cnat4_impulsive);
                    wText.WriteLine("delay: " + cnat4_delay);
                    wText.WriteLine("correct: " + cnat4_correct);
                    wText.WriteLine("correct_avg: " + cnat4_correct_avg);
                    wText.WriteLine("");




                    //測驗5
                    cnatlocationbig = 5;
                    wText.WriteLine("cnat測驗" + cnatlocationbig);

                    for (int i = 0; i < 36; i++)
                    {
                        if (Array.IndexOf(test5press, i + 1) == -1 && resulttimeread[cnatlocationbig][i] != -1 )// 不該按的按了  或按錯手(only test7) commission
                        {
                            cnat5_commission = cnat5_commission + 1;
                        }
                        else if(Array.IndexOf(test5press, i + 1) == -1 && resulttimeread[cnatlocationbig][i] == -1)// 不該按的沒按 
                        {
                            //
                        }
                        else if (Array.IndexOf(test5notpress, i + 1) == -1 && resulttimeread[cnatlocationbig][i] == -1 )//該按沒按  missing error
                        {
                            cnat5_missing = cnat5_missing + 1;
                        }
                        else if (resulttimeread[cnatlocationbig][i] < delaycnat[cnatlocationbig][i] + 100)//該按的按太快太快 impulsive
                        {
                            cnat5_impulsive = cnat5_impulsive + 1;
                        }
                        else//該按  按了 等於成功
                        {
                            forsd1[cnatlocationbig][cnat5_sd_count] = resulttimeread[cnatlocationbig][i];//把成功的放入forsd裡面供之後計算
                            cnat5_sd_count = cnat5_sd_count + 1; //記述++
                            cnat5_sd_sum = cnat5_sd_sum + resulttimeread[cnatlocationbig][i] - delaycnat[cnatlocationbig][i];//算平均值用++
                        }
                    }//計算SD用

                    if (cnat5_sd_count == 0)
                    {
                        cnat5_sd_count = 1;
                    }//防BUG

                    cnat5_sd_avg = cnat5_sd_sum / cnat5_sd_count;

                    for (int i = 0; i < cnat5_sd_count; i++)//求差平方
                    {
                        cnat5_sd_ts = cnat5_sd_ts + (forsd1[cnatlocationbig][i] - cnat5_sd_avg) * (forsd1[cnatlocationbig][i] - cnat5_sd_avg);
                    }

                    cnat5_sd_final = Math.Sqrt(cnat5_sd_ts / cnat5_sd_count);

                    cnat5_commission = 0;
                    cnat5_missing = 0;
                    cnat5_impulsive = 0;

                    for (int i = 0; i < 36; i++)
                    {

                        if (Array.IndexOf(test5press, i + 1) == -1 && resulttimeread[cnatlocationbig][i] != -1)// 不該按的按了  或按錯手(only test7)
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    commission");
                            cnat5_commission = cnat5_commission + 1;
                        }
                        else if (Array.IndexOf(test5press, i + 1) == -1 && resulttimeread[cnatlocationbig][i] == -1)// 不該按的沒按 不分類
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i]);

                        }
                        else if (Array.IndexOf(test5notpress, i + 1) == -1 && resulttimeread[cnatlocationbig][i] == -1)////該按沒按  missing error
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    missing");
                            cnat5_missing = cnat5_missing + 1;
                        }
                        else if (resulttimeread[cnatlocationbig][i] < delaycnat[cnatlocationbig][i] + 100)//太快 impulsive
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    impulsive");
                            cnat5_impulsive = cnat5_impulsive + 1;
                        }
                        else if (resulttimeread[cnatlocationbig][i] > cnat5_sd_avg + 2.5 * cnat5_sd_final)//遲緩  算標準差
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    delay");
                            cnat5_delay = cnat5_delay + 1;
                        }
                        else
                        {//正常
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i]);
                            cnat5_correct = cnat5_correct + 1;
                            cnat5_correct_sum = cnat5_correct_sum + resulttimeread[cnatlocationbig][i] - delaycnat[cnatlocationbig][i];
                        }
                    }//輸出到txt  同時記錄 cnat5_delay 數量
                    if (cnat5_correct == 0)//防BUG
                    { cnat5_correct = 1; }
                    cnat5_correct_avg = cnat5_correct_sum / cnat5_correct;
                    wText.WriteLine("missing: " + cnat5_missing);
                    wText.WriteLine("impulsive: " + cnat5_impulsive);
                    wText.WriteLine("delay: " + cnat5_delay);
                    wText.WriteLine("commission: " + cnat5_commission);
                    wText.WriteLine("correct: " + cnat5_correct);
                    wText.WriteLine("correct_avg: " + cnat5_correct_avg);
                    wText.WriteLine("");









                    //測驗6
                    cnatlocationbig = 6;
                    wText.WriteLine("cnat測驗" + cnatlocationbig);

                    for (int i = 0; i < 36; i++)
                    {
                        if (Array.IndexOf(test6press, i + 1) == -1 && resulttimeread[cnatlocationbig][i] != -1)// 不該按的按了  或按錯手(only test7) commission
                        {
                            cnat6_commission = cnat6_commission + 1;
                        }
                        else if (Array.IndexOf(test6press, i + 1) == -1 && resulttimeread[cnatlocationbig][i] == -1)// 不該按的沒按 
                        {
                            //
                        }
                        else if (Array.IndexOf(test6notpress, i + 1) == -1 && resulttimeread[cnatlocationbig][i] == -1)//該按沒按  missing error
                        {
                            cnat6_missing = cnat6_missing + 1;
                        }
                        else if (resulttimeread[cnatlocationbig][i] < delaycnat[cnatlocationbig][i] + 100)//該按的按太快太快 impulsive
                        {
                            cnat6_impulsive = cnat6_impulsive + 1;
                        }
                        else//該按  按了 等於成功
                        {
                            forsd1[cnatlocationbig][cnat6_sd_count] = resulttimeread[cnatlocationbig][i];//把成功的放入forsd裡面供之後計算
                            cnat6_sd_count = cnat6_sd_count + 1; //記述++
                            cnat6_sd_sum = cnat6_sd_sum + resulttimeread[cnatlocationbig][i] - delaycnat[cnatlocationbig][i];//算平均值用++
                        }
                    }//計算SD用

                    if (cnat6_sd_count == 0)
                    {
                        cnat6_sd_count = 1;
                    }//防BUG

                    cnat6_sd_avg = cnat6_sd_sum / cnat6_sd_count;

                    for (int i = 0; i < cnat6_sd_count; i++)//求差平方
                    {
                        cnat6_sd_ts = cnat6_sd_ts + (forsd1[cnatlocationbig][i] - cnat6_sd_avg) * (forsd1[cnatlocationbig][i] - cnat6_sd_avg);
                    }

                    cnat6_sd_final = Math.Sqrt(cnat6_sd_ts / cnat6_sd_count);

                    cnat6_commission = 0;
                    cnat6_missing = 0;
                    cnat6_impulsive = 0;

                    for (int i = 0; i < 36; i++)
                    {

                        if (Array.IndexOf(test6press, i + 1) == -1 && resulttimeread[cnatlocationbig][i] != -1)// 不該按的按了  或按錯手(only test7)
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    commission");
                            cnat6_commission = cnat6_commission + 1;
                        }
                        else if (Array.IndexOf(test6press, i + 1) == -1 && resulttimeread[cnatlocationbig][i] == -1)// 不該按的沒按 不分類
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i]);

                        }
                        else if (Array.IndexOf(test6notpress, i + 1) == -1 && resulttimeread[cnatlocationbig][i] == -1)////該按沒按  missing error
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    missing");
                            cnat6_missing = cnat6_missing + 1;
                        }
                        else if (resulttimeread[cnatlocationbig][i] < delaycnat[cnatlocationbig][i] + 100)//太快 impulsive
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    impulsive");
                            cnat6_impulsive = cnat6_impulsive + 1;
                        }
                        else if (resulttimeread[cnatlocationbig][i] > cnat6_sd_avg + 2.5 * cnat6_sd_final)//遲緩  算標準差
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    delay");
                            cnat6_delay = cnat6_delay + 1;
                        }
                        else
                        {//正常
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i]);
                            cnat6_correct = cnat6_correct + 1;
                            cnat6_correct_sum = cnat6_correct_sum + resulttimeread[cnatlocationbig][i] - delaycnat[cnatlocationbig][i];
                        }
                    }//輸出到txt  同時記錄 cnat6_delay 數量
                    if (cnat6_correct == 0)//防BUG
                    { cnat6_correct = 1; }
                    cnat6_correct_avg = cnat6_correct_sum / cnat6_correct;
                    wText.WriteLine("missing: " + cnat6_missing);
                    wText.WriteLine("impulsive: " + cnat6_impulsive);
                    wText.WriteLine("delay: " + cnat6_delay);
                    wText.WriteLine("commission: " + cnat6_commission);
                    wText.WriteLine("correct: " + cnat6_correct);
                    wText.WriteLine("correct_avg: " + cnat6_correct_avg);
                    wText.WriteLine("");







                    //測驗7
                    cnatlocationbig = 7;
                    wText.WriteLine("cnat測驗" + cnatlocationbig);

                    for (int i = 0; i < 72; i++)
                    {
                        if (Array.IndexOf(test7press, i + 1) == -1 && resulttimeread[cnatlocationbig][i] != -1 )// 不該按的按了   commission
                        {
                            cnat7_commission = cnat7_commission + 1;
                        }
                        else if (Array.IndexOf(test7press, i + 1) == -1 && resulttimeread[cnatlocationbig][i] == -1)// 不該按的沒按 
                        {
                            //
                        }
                        else if (Array.IndexOf(test7notpress, i + 1) == -1 && resulttimeread[cnatlocationbig][i] != 0 && leftright[i] != leftrightcorrect[i])//該按的按了但是 按錯手(only test7) commission
                        {
                            cnat7_commission = cnat7_commission + 1;
                        }
                        else if (Array.IndexOf(test7notpress, i + 1) == -1 && resulttimeread[cnatlocationbig][i] == -1)//該按沒按  missing error
                        {
                            cnat7_missing = cnat7_missing + 1;
                        }
                        else if (resulttimeread[cnatlocationbig][i] < delaycnat[cnatlocationbig][i] + 100)//該按的按太快太快 impulsive
                        {
                            cnat7_impulsive = cnat7_impulsive + 1;
                        }
                        else//該按  按了 等於成功
                        {
                            forsd1[cnatlocationbig][cnat7_sd_count] = resulttimeread[cnatlocationbig][i];//把成功的放入forsd裡面供之後計算
                            cnat7_sd_count = cnat7_sd_count + 1; //記述++
                            cnat7_sd_sum = cnat7_sd_sum + resulttimeread[cnatlocationbig][i] - delaycnat[cnatlocationbig][i];//算平均值用++
                        }
                    }//計算SD用

                    if (cnat7_sd_count == 0)
                    {
                        cnat7_sd_count = 1;
                    }//防BUG

                    cnat7_sd_avg = cnat7_sd_sum / cnat7_sd_count;

                    for (int i = 0; i < cnat7_sd_count; i++)//求差平方
                    {
                        cnat7_sd_ts = cnat7_sd_ts + (forsd1[cnatlocationbig][i] - cnat7_sd_avg) * (forsd1[cnatlocationbig][i] - cnat7_sd_avg);
                    }

                    cnat7_sd_final = Math.Sqrt(cnat7_sd_ts / cnat7_sd_count);

                    cnat7_commission = 0;
                    cnat7_missing = 0;
                    cnat7_impulsive = 0;

                    for (int i = 0; i < 72; i++)
                    {

                        if (Array.IndexOf(test7press, i + 1) == -1 && resulttimeread[cnatlocationbig][i] != -1 )// 不該按的按了 
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    commission   " + leftright[i]);
                            cnat7_commission = cnat7_commission + 1;
                        }
                        else if (Array.IndexOf(test7press, i + 1) == -1 && resulttimeread[cnatlocationbig][i] == -1)// 不該按的沒按 不分類
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] +"   " +leftright[i]);

                        }
                        else if (Array.IndexOf(test7notpress, i + 1) == -1 && resulttimeread[cnatlocationbig][i] != -1 && leftright[i] != leftrightcorrect[i])//該按的按了但是 按錯手(only test7) commission
                        {
                            cnat7_commission = cnat7_commission + 1;
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    commission   " +leftright[i]);
                        }
                        else if (Array.IndexOf(test7notpress, i + 1) == -1 && resulttimeread[cnatlocationbig][i] == -1)////該按沒按  missing error
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    missing   " +leftright[i]);
                            cnat7_missing = cnat7_missing + 1;
                        }
                        else if (resulttimeread[cnatlocationbig][i] < delaycnat[cnatlocationbig][i] + 100)//太快 impulsive
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    impulsive   " + leftright[i]);
                            cnat7_impulsive = cnat7_impulsive + 1;
                        }
                        else if (resulttimeread[cnatlocationbig][i] > cnat7_sd_avg + 2.5 * cnat7_sd_final)//遲緩  算標準差
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    delay   " +leftright[i]);
                            cnat7_delay = cnat7_delay + 1;
                        }
                        else
                        {//正常
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] +"   "+ leftright[i]);
                            cnat7_correct = cnat7_correct + 1;
                            cnat7_correct_sum = cnat7_correct_sum + resulttimeread[cnatlocationbig][i] - delaycnat[cnatlocationbig][i];
                        }
                    }//輸出到txt  同時記錄 cnat7_delay 數量
                    if (cnat7_correct == 0)//防BUG
                    { cnat7_correct = 1; }
                    cnat7_correct_avg = cnat7_correct_sum / cnat7_correct;
                    wText.WriteLine("missing: " + cnat7_missing);
                    wText.WriteLine("impulsive: " + cnat7_impulsive);
                    wText.WriteLine("delay: " + cnat7_delay);
                    wText.WriteLine("commission: " + cnat7_commission);
                    wText.WriteLine("correct: " + cnat7_correct);
                    wText.WriteLine("correct_avg: " + cnat7_correct_avg);
                    wText.WriteLine("");









                    //測驗8
                    cnatlocationbig = 8;
                    wText.WriteLine("cnat測驗" + cnatlocationbig);

                    for (int i = 0; i < 36; i++)
                    {
                        if (Array.IndexOf(test8press, i + 1) == -1 && resulttimeread[cnatlocationbig][i] != -1)// 不該按的按了  或按錯手(only test7) commission
                        {
                            cnat8_commission = cnat8_commission + 1;
                        }
                        else if (Array.IndexOf(test8press, i + 1) == -1 && resulttimeread[cnatlocationbig][i] == -1)// 不該按的沒按 
                        {
                            //
                        }
                        else if (Array.IndexOf(test8notpress, i + 1) == -1 && resulttimeread[cnatlocationbig][i] == -1)//該按沒按  missing error
                        {
                            cnat8_missing = cnat8_missing + 1;
                        }
                        else if (resulttimeread[cnatlocationbig][i] < delaycnat[cnatlocationbig][i] + 100)//該按的按太快太快 impulsive
                        {
                            cnat8_impulsive = cnat8_impulsive + 1;
                        }
                        else//該按  按了 等於成功
                        {
                            forsd1[cnatlocationbig][cnat8_sd_count] = resulttimeread[cnatlocationbig][i];//把成功的放入forsd裡面供之後計算
                            cnat8_sd_count = cnat8_sd_count + 1; //記述++
                            cnat8_sd_sum = cnat8_sd_sum + resulttimeread[cnatlocationbig][i] - delaycnat[cnatlocationbig][i];//算平均值用++
                        }
                    }//計算SD用

                    if (cnat8_sd_count == 0)
                    {
                        cnat8_sd_count = 1;
                    }//防BUG

                    cnat8_sd_avg = cnat8_sd_sum / cnat8_sd_count;

                    for (int i = 0; i < cnat8_sd_count; i++)//求差平方
                    {
                        cnat8_sd_ts = cnat8_sd_ts + (forsd1[cnatlocationbig][i] - cnat8_sd_avg) * (forsd1[cnatlocationbig][i] - cnat8_sd_avg);
                    }

                    cnat8_sd_final = Math.Sqrt(cnat8_sd_ts / cnat8_sd_count);

                    cnat8_commission = 0;
                    cnat8_missing = 0;
                    cnat8_impulsive = 0;

                    for (int i = 0; i < 36; i++)
                    {

                        if (Array.IndexOf(test8press, i + 1) == -1 && resulttimeread[cnatlocationbig][i] != -1)// 不該按的按了  或按錯手(only test7)
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    commission");
                            cnat8_commission = cnat8_commission + 1;
                        }
                        else if (Array.IndexOf(test8press, i + 1) == -1 && resulttimeread[cnatlocationbig][i] == -1)// 不該按的沒按 不分類
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i]);

                        }
                        else if (Array.IndexOf(test8notpress, i + 1) == -1 && resulttimeread[cnatlocationbig][i] == -1)////該按沒按  missing error
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    missing");
                            cnat8_missing = cnat8_missing + 1;
                        }
                        else if (resulttimeread[cnatlocationbig][i] < delaycnat[cnatlocationbig][i] + 100)//太快 impulsive
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    impulsive");
                            cnat8_impulsive = cnat8_impulsive + 1;
                        }
                        else if (resulttimeread[cnatlocationbig][i] > cnat8_sd_avg + 2.5 * cnat8_sd_final)//遲緩  算標準差
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    delay");
                            cnat8_delay = cnat8_delay + 1;
                        }
                        else
                        {//正常
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i]);
                            cnat8_correct = cnat8_correct + 1;
                            cnat8_correct_sum = cnat8_correct_sum + resulttimeread[cnatlocationbig][i] - delaycnat[cnatlocationbig][i];
                        }
                    }//輸出到txt  同時記錄 cnat8_delay 數量
                    if (cnat8_correct == 0)//防BUG
                    { cnat8_correct = 1; }
                    cnat8_correct_avg = cnat8_correct_sum / cnat8_correct;
                    wText.WriteLine("missing: " + cnat8_missing);
                    wText.WriteLine("impulsive: " + cnat8_impulsive);
                    wText.WriteLine("delay: " + cnat8_delay);
                    wText.WriteLine("commission: " + cnat8_commission);
                    wText.WriteLine("correct: " + cnat8_correct);
                    wText.WriteLine("correct_avg: " + cnat8_correct_avg);
                    wText.WriteLine("");






                    //測驗9
                    cnatlocationbig = 9;
                    wText.WriteLine("cnat測驗" + cnatlocationbig);

                    for (int i = 0; i < 36; i++)
                    {
                        if (Array.IndexOf(test6press, i + 1) == -1 && resulttimeread[cnatlocationbig][i] != -1)// 不該按的按了  或按錯手(only test7) commission
                        {
                            cnat9_commission = cnat9_commission + 1;
                        }
                        else if (Array.IndexOf(test6press, i + 1) == -1 && resulttimeread[cnatlocationbig][i] == -1)// 不該按的沒按 
                        {
                            //
                        }
                        else if (Array.IndexOf(test6notpress, i + 1) == -1 && resulttimeread[cnatlocationbig][i] == -1)//該按沒按  missing error
                        {
                            cnat9_missing = cnat9_missing + 1;
                        }
                        else if (resulttimeread[cnatlocationbig][i] < delaycnat[cnatlocationbig][i] + 100)//該按的按太快太快 impulsive
                        {
                            cnat9_impulsive = cnat9_impulsive + 1;
                        }
                        else//該按  按了 等於成功
                        {
                            forsd1[cnatlocationbig][cnat9_sd_count] = resulttimeread[cnatlocationbig][i];//把成功的放入forsd裡面供之後計算
                            cnat9_sd_count = cnat9_sd_count + 1; //記述++
                            cnat9_sd_sum = cnat9_sd_sum + resulttimeread[cnatlocationbig][i] - delaycnat[cnatlocationbig][i];//算平均值用++
                        }
                    }//計算SD用

                    if (cnat9_sd_count == 0)
                    {
                        cnat9_sd_count = 1;
                    }//防BUG

                    cnat9_sd_avg = cnat9_sd_sum / cnat9_sd_count;

                    for (int i = 0; i < cnat9_sd_count; i++)//求差平方
                    {
                        cnat9_sd_ts = cnat9_sd_ts + (forsd1[cnatlocationbig][i] - cnat9_sd_avg) * (forsd1[cnatlocationbig][i] - cnat9_sd_avg);
                    }

                    cnat9_sd_final = Math.Sqrt(cnat9_sd_ts / cnat9_sd_count);

                    cnat9_commission = 0;
                    cnat9_missing = 0;
                    cnat9_impulsive = 0;

                    for (int i = 0; i < 36; i++)
                    {

                        if (Array.IndexOf(test6press, i + 1) == -1 && resulttimeread[cnatlocationbig][i] != -1)// 不該按的按了  或按錯手(only test7)
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    commission");
                            cnat9_commission = cnat9_commission + 1;
                        }
                        else if (Array.IndexOf(test6press, i + 1) == -1 && resulttimeread[cnatlocationbig][i] == -1)// 不該按的沒按 不分類
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i]);

                        }
                        else if (Array.IndexOf(test6notpress, i + 1) == -1 && resulttimeread[cnatlocationbig][i] == -1)////該按沒按  missing error
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    missing");
                            cnat9_missing = cnat9_missing + 1;
                        }
                        else if (resulttimeread[cnatlocationbig][i] < delaycnat[cnatlocationbig][i] + 100)//太快 impulsive
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    impulsive");
                            cnat9_impulsive = cnat9_impulsive + 1;
                        }
                        else if (resulttimeread[cnatlocationbig][i] > cnat9_sd_avg + 2.5 * cnat9_sd_final)//遲緩  算標準差
                        {
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i] + "    delay");
                            cnat9_delay = cnat9_delay + 1;
                        }
                        else
                        {//正常
                            wText.WriteLine("第" + Convert.ToString(i + 1) + "題: " + resulttimeread[cnatlocationbig][i]);
                            cnat9_correct = cnat9_correct + 1;
                            cnat9_correct_sum = cnat9_correct_sum + resulttimeread[cnatlocationbig][i] - delaycnat[cnatlocationbig][i];
                        }
                    }//輸出到txt  同時記錄 cnat9_delay 數量
                    if (cnat9_correct == 0)//防BUG
                    { cnat9_correct = 1; }
                    cnat9_correct_avg = cnat9_correct_sum / cnat9_correct;
                    wText.WriteLine("missing: " + cnat9_missing);
                    wText.WriteLine("impulsive: " + cnat9_impulsive);
                    wText.WriteLine("delay: " + cnat9_delay);
                    wText.WriteLine("commission: " + cnat9_commission);
                    wText.WriteLine("correct: " + cnat9_correct);
                    wText.WriteLine("correct_avg: " + cnat9_correct_avg);
                    wText.WriteLine("");




                    //以下都是存檔用
                    wText.Flush();
                    wText.Close();
                    myStream.Close();
                }
            }

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnat_continue();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            cnat2load();
            button4.Enabled = false;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            cnat3load();
            button5.Enabled = false;
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            cnat4load();
            button6.Enabled = false;
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            cnat5load();
            button7.Enabled = false;
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            cnat6load();
            button8.Enabled = false;
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            cnat7load();
            button9.Enabled = false;
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            cnat8load();
            button10.Enabled = false;
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            cnat9load();
            button11.Enabled = false;
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;
            button10.Enabled = true;
            button11.Enabled = true;
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 74; i++)
            {
                textBox1.AppendText(Convert.ToString(resulttimeread[7][i]) + "   " + leftright[i] + "  \r\n");
            }
        }
    }
}
