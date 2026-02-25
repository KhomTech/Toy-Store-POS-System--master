using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBP_Project
{
    public partial class formIndex : Form
    {
        public formIndex()
        {
            InitializeComponent();
        }

        private void labelRegister_Click(object sender, EventArgs e)
        {
            Form newFormReg = new formRegister(); // เปลี่ยนเป็นฟอร์มที่ต้องการเปิด
            newFormReg.Show();  // เปิดฟอร์มใหม่ในลักษณะ modal
            this.Hide(); // ซ่อนฟอร์มปัจจุบัน
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {

            Form newFormLog = new formLogin(); // เปลี่ยนเป็นฟอร์มที่ต้องการเปิด
            newFormLog.Show();  // เปิดฟอร์มใหม่ในลักษณะ modal
            this.Hide(); // ซ่อนฟอร์มปัจจุบัน
        }
    }
}
