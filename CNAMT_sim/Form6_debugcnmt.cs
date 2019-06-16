using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace CNAMT_sim
{
    public partial class Form6_debugcnmt : Form
    {
        public Form6_debugcnmt()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt0();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt0_a();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt0_b();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt0_c();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt0_d();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt1();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt1_a();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt1_b();
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt1_c();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt1_d();
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt2();
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt2_a();
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt2_b();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt2_c();
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt2_d();
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt3();
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt3_a();
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt3_b();
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt3_c();
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt3_d();
        }

        private void Button25_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt4();
        }

        private void Button24_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt4_a();
        }

        private void Button23_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt4_b();
        }

        private void Button22_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt4_c();
        }

        private void Button21_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt4_d();
        }

        private void Button30_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt5();
        }

        private void Button29_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt5_a();
        }

        private void Button28_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt5_b();
        }

        private void Button27_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt5_c();
        }

        private void Button26_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt5_d();
        }

        private void Button35_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt6();
        }

        private void Button34_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt6_a();
        }

        private void Button33_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt6_b();
        }

        private void Button32_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt6_c();
        }

        private void Button31_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt6_d();
        }

        private void Button40_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt7();
        }

        private void Button39_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt7_a();
        }

        private void Button38_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt7_b();
        }

        private void Button37_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt7_c();
        }

        private void Button36_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt7_d();
        }

        private void Button45_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt8();
        }

        private void Button44_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt8_a();
        }

        private void Button43_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt8_b();
        }

        private void Button42_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt8_c();
        }

        private void Button41_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.cnmt8_d();
        }

        private void Button47_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考
            form1.savedata4();
        }

        private void Button46_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考;
            form1.playaudiocnmt1();
            form1.playaudiocnmtright();

        }

        private void Button48_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考;
            form1.playaudiocnmt1();
            Task.Delay(1000);
            form1.playaudiocnmtwrong();
        }

        private void Button49_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner; //取得父視窗的參考;
            form1.playaudiocnmtwrong();
            form1.playaudiocnmt1();
        }
    }
}
