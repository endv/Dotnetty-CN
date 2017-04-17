using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testform
{
    using System.Threading;//引用此命名
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.backgroundWorker1.RunWorkerAsync(); // 运行 backgroundWorker 组件
                                                     //static void Main() => RunServer.RunServerAsync().Wait();//1
            void Main() => RunServer.RunServerAsync().Wait();//1
            Form2 form = new Form2(this.backgroundWorker1);// 显示进度条窗体 
            form.ShowDialog(this);
            form.Close();
        }

    }
}
