using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DBP_Project
{
    public partial class FormShippingAddress : Form
    {
        private int _memberId;
        const string strFileName = "ConnectionString.ini";
        string strConnectionString;
        private Form _previousForm; // ใช้เก็บฟอร์มก่อนหน้า

        public FormShippingAddress(int memberId, Form previousForm)
        {
            InitializeComponent();
            _memberId = memberId; // รับ Member_ID จากฟอร์ม Register
            _previousForm = previousForm; // เก็บฟอร์มก่อนหน้า
        }
        private bool ValidateData()
        {
            string message = "";
            bool allOK = true;

            // ตรวจสอบชื่อผู้รับ
            if (string.IsNullOrWhiteSpace(txtRecipientName.Text))
            {
                message += "กรุณากรอกชื่อผู้รับ\n";
                txtRecipientName.Focus();
                allOK = false;
            }

            // ตรวจสอบที่อยู่
            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                message += "กรุณากรอกที่อยู่\n";
                txtAddress.Focus();
                allOK = false;
            }

            // ตรวจสอบตำบล
            if (string.IsNullOrWhiteSpace(textBoxsubDistrict.Text))
            {
                message += "กรุณากรอกตำบล\n";
                textBoxsubDistrict.Focus();
                allOK = false;
            }

            // ตรวจสอบอำเภอ
            if (string.IsNullOrWhiteSpace(textDistrit.Text))
            {
                message += "กรุณากรอกอำเภอ\n";
                textDistrit.Focus();
                allOK = false;
            }

            // ตรวจสอบจังหวัด
            if (string.IsNullOrWhiteSpace(txtProvince.Text))
            {
                message += "กรุณากรอกจังหวัด\n";
                txtProvince.Focus();
                allOK = false;
            }

            // ตรวจสอบรหัสไปรษณีย์
            if (string.IsNullOrWhiteSpace(txtPostalCode.Text))
            {
                message += "กรุณากรอกรหัสไปรษณีย์\n";
                txtPostalCode.Focus();
                allOK = false;
            }

            // ตรวจสอบหมายเลขโทรศัพท์
            if (string.IsNullOrWhiteSpace(txtPhone.Text) || txtPhone.Text.Length < 10 || txtPhone.Text.Length > 10)
            {
                message += "กรุณากรอกหมายเลขโทรศัพท์ที่ถูกต้อง\n";
                txtPhone.Focus();
                allOK = false;
            }

            // หากข้อมูลไม่ครบถ้วน จะแสดงข้อความแจ้งเตือน
            if (!allOK)
            {
                MessageBox.Show(message, "ข้อผิดพลาดในการกรอกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return allOK;
        }

        private void buttonFinishShip_Click(object sender, EventArgs e)
        {
            // ตรวจสอบข้อมูลก่อนที่จะบันทึก
            if (!ValidateData())
            {
                return;  // ถ้าข้อมูลไม่ครบ จะไม่ดำเนินการต่อ
            }

            string recipientName = txtRecipientName.Text;
            string address = txtAddress.Text;
            string subdistrict = textBoxsubDistrict.Text;
            string distrit = textDistrit.Text;
            string province = txtProvince.Text;
            string postalCode = txtPostalCode.Text;
            string phone = txtPhone.Text;

            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                try
                {
                    conn.Open();

                    // คำสั่ง SQL สำหรับบันทึกข้อมูลที่อยู่
                    string insertAddressQuery = "INSERT INTO Shipping_Address (Member_ID, Recipient_Name, Address, Subdistrict, District, Province, Postal_Code, Phone) " +
                                                "VALUES (@Member_ID, @Recipient_Name, @Address, @Subdistrict, @District, @Province, @Postal_Code, @Phone);";

                    SqlCommand cmd = new SqlCommand(insertAddressQuery, conn);

                    // ส่งค่าพารามิเตอร์ไปยัง SQL
                    cmd.Parameters.AddWithValue("@Member_ID", _memberId);
                    cmd.Parameters.AddWithValue("@Recipient_Name", recipientName);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@Subdistrict", subdistrict);
                    cmd.Parameters.AddWithValue("@District", distrit);
                    cmd.Parameters.AddWithValue("@Province", province);
                    cmd.Parameters.AddWithValue("@Postal_Code", postalCode);
                    cmd.Parameters.AddWithValue("@Phone", phone);

                    // บันทึกข้อมูลที่อยู่
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("ข้อมูลที่อยู่ถูกบันทึกเรียบร้อยแล้ว!");

                    // ปิดฟอร์มและแสดงฟอร์มต่อไป
                    Form formIndex = new formIndex(); // สร้างออบเจ็กต์ใหม่ของ formIndex
                    formIndex.Show();  // แสดง formIndex
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void buttonBackShip_Click(object sender, EventArgs e)
        {
            // ลบข้อมูลที่ Member_ID ตรงกับ _memberId
            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                try
                {
                    conn.Open();

                    // คำสั่ง SQL สำหรับลบข้อมูลในตาราง Member ที่ตรงกับ _memberId
                    string deleteQuery = "DELETE FROM Member WHERE Member_ID = @Member_ID;";

                    SqlCommand cmd = new SqlCommand(deleteQuery, conn);
                    cmd.Parameters.AddWithValue("@Member_ID", _memberId);

                    // ลบข้อมูลในตาราง Member
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("ข้อมูลผู้ใช้ถูกลบเรียบร้อยแล้ว!");

                    // ปิดฟอร์มปัจจุบันและกลับไปยังฟอร์มก่อนหน้า
                    this.Close();
                    _previousForm.Show(); // แสดงฟอร์มก่อนหน้า
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void FormShippingAddress_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(strFileName))
                strConnectionString = System.IO.File.ReadAllText(strFileName);
        }

        private void FormShippingAddress_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
