using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testform
{
    using System.Threading;//引用此命名
    public partial class Form2 : Form
    {
        public Form2(BackgroundWorker backgroundWorker1)
        {
            InitializeComponent(); 

            this.backgroundWorker1 = backgroundWorker1;
            this.backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
        }
        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //this.Close();//执行完之后，直接关闭页面
        }

        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //this.progressBar1.Value = e.ProgressPercentage;
            //this.textBox1.AppendText(e.UserState.ToString());//主窗体传过来的值，通过e.UserState.ToString()来接受

            this.progressBar1.Value = e.ProgressPercentage;
            this.textBox1.Text += e.UserState.ToString(); //主窗体传过来的值，通过e.UserState.ToString()来接受
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.backgroundWorker1.CancelAsync();
            this.button1.Enabled = false;
            this.Close();
        }
    }
}
