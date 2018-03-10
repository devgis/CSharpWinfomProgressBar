using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ProgressBar
{
    public partial class Form1 : Form
    {
        public Thread MyThread = null;
        public Form1()
        {
            InitializeComponent();
        }
        private frmProcessBar myProcessBar = null;

        private delegate bool IncreaseHandle(int nValue);

        

        private void ShowProcessBar()
        {

            myProcessBar = new frmProcessBar(true, true);
            myProcessBar.Title = "测试进度条";
            myProcessBar.MyThread = progressThread;

            myProcessBar.setMaxValue(99);

            // Init increase event


            myProcessBar.TopMost = true;
            myProcessBar.ShowDialog();

            myProcessBar = null;

        }
        private void ThreadFun()
        {

            MethodInvoker mi = new MethodInvoker(ShowProcessBar);

            this.BeginInvoke(mi);

            Thread.Sleep(1000);//Sleep a while to show window




            int tempcount = 100;

            for (int i = 0; i < tempcount; i++)
            {
                myProcessBar.Content = "Text" + i;
                Thread.Sleep(300);
                this.Invoke(new IncreaseHandle(myProcessBar.Increase),

                    new object[] { i });
            }
        }
        Thread progressThread = null;
        private void button1_Click(object sender, EventArgs e)
        {
            progressThread = new Thread(new ThreadStart(ThreadFun));
            progressThread.Start();
        }
    }
}