using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApplication11
{
    public partial class WindowCreatePicture : Form
    {
        private Form1 _parent;
        public WindowCreatePicture(Form1 parent)
        {
            InitializeComponent();
            this._parent = parent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _parent.createPic((int)numericUpDown1.Value, (int)numericUpDown2.Value);
            this.Close();
        }
    }
}
