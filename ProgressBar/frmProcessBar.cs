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
    public partial class frmProcessBar : Form
    {
        public Thread MyThread = null;

        /// <summary>
        /// 是否显示正文文版
        /// </summary>
        public bool showContentText = true;
        public bool showFrame = true;


        public string Title = "请稍后。。。。。。";
        
        public string Content="操作中，请稍后... ...";
        public frmProcessBar(bool ShowContentText=true,bool ShowFrame = true)
        {
            
            this.showContentText = ShowContentText;
            this.showFrame=ShowFrame;
            //if (!showFrame)
            //{
            //    this.FormBorderStyle = FormBorderStyle.None;
                
            //}

            InitializeComponent();
            if (!showContentText)
            {
                label1.Text = string.Empty;
            }
            this.Refresh();
            this.FormClosing += new FormClosingEventHandler(frmProcessBar_FormClosing);
        }

        

        

        public void setMaxValue(int nValue)
        {
            prcBar.Maximum = nValue;
            
        }

        public void setValue(int nValue)
        {
            prcBar.Value = nValue;

        }

        public bool Increase(int nValue)
        {
            float percent = (float)nValue * 100 / (float)prcBar.Maximum;

            if (showContentText)
            {
                if (percent > 0)
                {
                    label1.Text = Content + "(" + percent.ToString(".") + "%)";
                }
                else
                {
                    label1.Text = Content + "(0%)";
                }
            }

            if (nValue > 0)
            {

                if (nValue < prcBar.Maximum)
                {

                    prcBar.Value = nValue;

                    return true;

                }

                else
                {

                    prcBar.Value = prcBar.Maximum;
                    Exited = true;
                    this.Close();

                    return false;

                }

            }

            return false;

        }
        bool Exited = false;
        private void btCancel_Click(object sender, EventArgs e)
        {
            if (!Exited)
            {
                if (MessageBox.Show("确认取消当前任务？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Exited = true;
                    Cancel();
                }
                else
                {
                    Exited = false;
                }
            }
        }
        void frmProcessBar_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Exited)
            {
                if (MessageBox.Show("确认取消当前任务？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Exited = true;
                    Cancel();
                    
                }
                else
                {
                    Exited = false;
                    e.Cancel = true;
                }
            }
        }
       

        public void Cancel()
        {
            
            if (MyThread != null)
            {
                MyThread.Abort();
                this.Close();
            }
        }
    }
}