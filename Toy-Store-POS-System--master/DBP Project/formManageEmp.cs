using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Dapper;
using System.Text.RegularExpressions;

namespace DBP_Project
{
    public partial class formManageEmp : Form
    {

        const string strFileName = "ConnectionString.ini";
        private string strConnectionString = "";  // ประกาศตัวแปรสำหรับ Connection String

        SqlConnection storeConnection;
        SqlCommand employeeCommand;
        SqlDataAdapter employeeAdapter;
        DataTable employeeTable;
        CurrencyManager employeeManager;

        string myState;
        int myEmp;
        public formManageEmp()
        {
            InitializeComponent();
        }

        private void formManageEmp_Load(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(strFileName))
                    strConnectionString = File.ReadAllText(strFileName, Encoding.GetEncoding("Windows-874"));

                if (string.IsNullOrEmpty(strConnectionString))
                {
                    MessageBox.Show("ไม่สามารถอ่านค่า Connection String ได้จากไฟล์", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Connect to books database
                storeConnection = new SqlConnection(strConnectionString);
                storeConnection.Open();

                // Establish command object
                employeeCommand = new SqlCommand("SELECT * FROM Employee ORDER BY Emp_ID", storeConnection);

                // Establish data adapter/data table
                employeeAdapter = new SqlDataAdapter(employeeCommand);
                employeeTable = new DataTable();
                employeeAdapter.Fill(employeeTable);

                // Bind controls to data table
                txtEmpID.DataBindings.Add("Text", employeeTable, "Emp_ID");
                textEmpUsername.DataBindings.Add("Text", employeeTable, "Username");
                textEmpPass.DataBindings.Add("Text", employeeTable, "Password");
                textEmpFName.DataBindings.Add("Text", employeeTable, "Emp_FName");
                textEmpLName.DataBindings.Add("Text", employeeTable, "Emp_LName");
                textEmpEmail.DataBindings.Add("Text", employeeTable, "Emp_Email");
                textEmpPhone.DataBindings.Add("Text", employeeTable, "Emp_Phone");
                textEmpPosition.DataBindings.Add("Text", employeeTable, "Emp_Position");
                textSalary.DataBindings.Add("Text", employeeTable, "Emp_Salary");
                dateTimePickerHireDate.DataBindings.Add("Text", employeeTable, "Emp_HireDate");
                // Establish currency manager
                employeeManager = (CurrencyManager)this.BindingContext[employeeTable];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "เกิดข้อผิดพลาดในการสร้างการทำงานกับตารางผู้แต่ง", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Show();
            SetState("View");
        }
        private void SetState(string appState)
        {
            myState = appState;
            switch (appState)
            {
                case "View":
                    txtEmpID.BackColor = Color.White;
                    txtEmpID.ForeColor = Color.Black;
                    txtEmpID.ReadOnly = true;
                    textEmpUsername.ReadOnly = true;
                    textEmpPass.ReadOnly = true;
                    textEmpFName.ReadOnly = true;
                    textEmpLName.ReadOnly = true;
                    textEmpPhone.ReadOnly = true;
                    textEmpEmail.ReadOnly = true;
                    textEmpPosition.ReadOnly = true;
                    textSalary.ReadOnly = true;
                    dateTimePickerHireDate.Enabled = false;

                    buttonFirst.Enabled = true;
                    buttonPrevious.Enabled = true;
                    buttonNext.Enabled = true;
                    buttonLast.Enabled = true;
                    buttonAddNew.Enabled = true;
                    buttonSave.Enabled = false;
                    buttonCancel.Enabled = false;
                    buttonEdit.Enabled = true;
                    buttonDelete.Enabled = true;
                    buttonDone.Enabled = true;
                    textEmpUsername.Focus();
                    break;
                default: // Add or Edit if not View
                    txtEmpID.BackColor = Color.Red;
                    txtEmpID.ForeColor = Color.White;
                    txtEmpID.ReadOnly = true;
                    textEmpUsername.ReadOnly = false;
                    textEmpPass.ReadOnly = false;
                    textEmpFName.ReadOnly = false;
                    textEmpLName.ReadOnly = false;
                    textEmpPhone.ReadOnly = false;
                    textEmpEmail.ReadOnly = false;
                    textEmpPosition.ReadOnly = false;
                    textSalary.ReadOnly = false;
                    dateTimePickerHireDate.Enabled = true;

                    buttonFirst.Enabled = false;
                    buttonPrevious.Enabled = false;
                    buttonNext.Enabled = false;
                    buttonLast.Enabled = false;
                    buttonAddNew.Enabled = false;
                    buttonSave.Enabled = true;
                    buttonCancel.Enabled = true;
                    buttonEdit.Enabled = false;
                    buttonDelete.Enabled = false;
                    buttonDone.Enabled = false;
                    textEmpUsername.Focus();
                    break;
            }
        }

        private void buttonFirst_Click(object sender, EventArgs e)
        {
            employeeManager.Position = 0;
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if (employeeManager.Position == 0)
            {
                return;
            }
            employeeManager.Position--;
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (employeeManager.Position == employeeManager.Count - 1)
            {
                return;
            }
            employeeManager.Position++;
        }

        private void buttonLast_Click(object sender, EventArgs e)
        {
            employeeManager.Position = employeeManager.Count - 1;

        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            SetState("Edit");
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                // ตรวจสอบข้อมูลก่อนบันทึก
                if (!ValidateData())
                {
                    return; // ถ้าข้อมูลไม่ถูกต้องจะหยุดการทำงาน
                }

                // ตรวจสอบสถานะว่าอยู่ในโหมดแก้ไขหรือไม่
                if (myState == "Edit")
                {
                    // โค้ดสำหรับแก้ไขข้อมูล (เดิม)
                    using (SqlConnection conn = new SqlConnection(strConnectionString))
                    {
                        conn.Open();

                        // คำสั่ง SQL สำหรับการอัปเดตข้อมูล
                        string updateQuery = @"UPDATE Employee SET 
                                        Emp_FName = @Emp_FName,
                                        Emp_LName = @Emp_LName,
                                        Emp_Phone = @Emp_Phone,
                                        Emp_Position = @Emp_Position,
                                        Emp_Email = @Emp_Email,
                                        Emp_Salary = @Emp_Salary,
                                        Emp_HireDate = @Emp_HireDate,
                                        Username = @Username,
                                        Password = @Password
                                        WHERE Emp_ID = @Emp_ID";

                        using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@Emp_ID", Convert.ToInt32(txtEmpID.Text));
                            cmd.Parameters.AddWithValue("@Emp_FName", textEmpFName.Text);
                            cmd.Parameters.AddWithValue("@Emp_LName", textEmpLName.Text);
                            cmd.Parameters.AddWithValue("@Emp_Phone", textEmpPhone.Text);
                            cmd.Parameters.AddWithValue("@Emp_Position", textEmpPosition.Text);
                            cmd.Parameters.AddWithValue("@Emp_Email", textEmpEmail.Text);
                            cmd.Parameters.AddWithValue("@Emp_Salary", Convert.ToDecimal(textSalary.Text));
                            cmd.Parameters.AddWithValue("@Emp_HireDate", DateTime.Parse(dateTimePickerHireDate.Text));
                            cmd.Parameters.AddWithValue("@Username", textEmpUsername.Text);
                            cmd.Parameters.AddWithValue("@Password", textEmpPass.Text);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("อัปเดตข้อมูลสำเร็จ", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadEmployeeData();
                                SetState("View");
                            }
                            else
                            {
                                MessageBox.Show("ไม่มีข้อมูลที่ถูกอัปเดต", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                else // ในกรณีที่ไม่ได้อยู่ในโหมดแก้ไข (ใช้สำหรับการเพิ่มข้อมูลใหม่)
                {
                    using (SqlConnection conn = new SqlConnection(strConnectionString))
                    {
                        conn.Open();

                        // ตรวจสอบว่า Username มีในฐานข้อมูลแล้วหรือไม่
                        string checkUsernameQuery = "SELECT COUNT(*) FROM Employee WHERE Username = @Username";
                        SqlCommand checkUsernameCmd = new SqlCommand(checkUsernameQuery, conn);
                        checkUsernameCmd.Parameters.AddWithValue("@Username", textEmpUsername.Text);
                        int usernameExists = (int)checkUsernameCmd.ExecuteScalar();

                        if (usernameExists > 0)
                        {
                            MessageBox.Show("ชื่อผู้ใช้นี้ถูกใช้แล้วในระบบ กรุณาลองใหม่อีกครั้ง.", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // คำสั่ง SQL สำหรับการเพิ่มข้อมูลใหม่
                        string insertQuery = @"INSERT INTO Employee (Emp_FName, Emp_LName, Emp_Phone, Emp_Email, Emp_Position, Emp_Salary, Emp_HireDate, Username, Password) 
                                        VALUES (@Emp_FName, @Emp_LName, @Emp_Phone, @Emp_Email, @Emp_Position, @Emp_Salary, @Emp_HireDate, @Username, @Password);
                                        SELECT SCOPE_IDENTITY();";

                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                        {
                            insertCmd.Parameters.AddWithValue("@Emp_FName", textEmpFName.Text);
                            insertCmd.Parameters.AddWithValue("@Emp_LName", textEmpLName.Text);
                            insertCmd.Parameters.AddWithValue("@Emp_Phone", textEmpPhone.Text);
                            insertCmd.Parameters.AddWithValue("@Emp_Email", textEmpEmail.Text);
                            insertCmd.Parameters.AddWithValue("@Emp_Position", textEmpPosition.Text);
                            insertCmd.Parameters.AddWithValue("@Emp_Salary", Convert.ToDecimal(textSalary.Text));
                            insertCmd.Parameters.AddWithValue("@Emp_HireDate", DateTime.Parse(dateTimePickerHireDate.Text));
                            insertCmd.Parameters.AddWithValue("@Username", textEmpUsername.Text);
                            insertCmd.Parameters.AddWithValue("@Password", textEmpPass.Text);

                            int newEmpId = Convert.ToInt32(insertCmd.ExecuteScalar()); // รับ Emp_ID ที่ถูกสร้างใหม่

                            MessageBox.Show("ข้อมูลพนักงานใหม่ถูกบันทึกแล้ว", "บันทึก", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            employeeTable.Clear();
                            employeeAdapter.Fill(employeeTable);
                            // เลื่อนไปยังข้อมูลที่เพิ่มใหม่
                            int newPosition = employeeTable.Rows.IndexOf(employeeTable.Select("Emp_ID = " + newEmpId)[0]);
                            employeeManager.Position = newPosition;

                            SetState("View"); // เปลี่ยนสถานะกลับไปที่ View
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการบันทึกข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidateData()
        {
            string message = "";
            bool allOK = true;

            // ตรวจสอบชื่อผู้ใช้
            if (string.IsNullOrWhiteSpace(textEmpUsername.Text))
            {
                message += "กรุณากรอกชื่อผู้ใช้\n";
                textEmpUsername.Focus();
                allOK = false;
            }

            // ตรวจสอบรหัสผ่าน
            if (string.IsNullOrWhiteSpace(textEmpPass.Text))
            {
                message += "กรุณากรอกรหัสผ่าน\n";
                textEmpPass.Focus();
                allOK = false;
            }

            // ตรวจสอบชื่อจริง
            if (string.IsNullOrWhiteSpace(textEmpFName.Text))
            {
                message += "กรุณากรอกชื่อจริง\n";
                textEmpFName.Focus();
                allOK = false;
            }

            // ตรวจสอบนามสกุล
            if (string.IsNullOrWhiteSpace(textEmpLName.Text))
            {
                message += "กรุณากรอกนามสกุล\n";
                textEmpLName.Focus();
                allOK = false;
            }
            // ตรวจสอบหมายเลขโทรศัพท์
            if (string.IsNullOrWhiteSpace(textEmpPhone.Text) || textEmpPhone.Text.Length < 10 || textEmpPhone.Text.Length > 10)
            {
                message += "กรุณากรอกหมายเลขโทรศัพท์ที่ถูกต้อง\n";
                textEmpPhone.Focus();
                allOK = false;
            }
            // ตรวจสอบตำแหน่ง
            if (string.IsNullOrWhiteSpace(textEmpPosition.Text))
            {
                message += "กรุณากรอกตำแหน่ง\n";
                textEmpPosition.Focus();
                allOK = false;
            }

            // ตรวจสอบเงินเดือน
            if (string.IsNullOrWhiteSpace(textSalary.Text) || Convert.ToDecimal(textSalary.Text) <= 0)
            {
                message += "กรุณากรอกเงินเดือนที่ถูกต้อง\n";
                textSalary.Focus();
                allOK = false;
            }
            // ตรวจสอบวันที่เข้าทำงาน
            if (dateTimePickerHireDate.Value >= DateTime.Now)
            {
                message += "กรุณากรอกวันที่เข้าทำงานที่ถูกต้อง\n";
                dateTimePickerHireDate.Focus();
                allOK = false;
            }
            // ตรวจสอบอีเมล
            if (string.IsNullOrWhiteSpace(textEmpEmail.Text) || !ValidateEmail(textEmpEmail.Text))
            {
                message += "กรุณากรอกอีเมลที่ถูกต้อง\n";
                textEmpEmail.Focus();
                allOK = false;
            }
            // หากตรวจสอบไม่ผ่าน จะขึ้นข้อความแจ้งเตือน
            if (!allOK)
            {
                MessageBox.Show(message, "ข้อผิดพลาดในการตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return allOK;
        }

        // ตรวจสอบรูปแบบอีเมล
        private bool ValidateEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(emailPattern);
            return regex.IsMatch(email);
        }
        private void LoadEmployeeData()
            {
                try
                {
                    string strConnectionString = "";
                    if (System.IO.File.Exists(strFileName))
                    {
                        strConnectionString = System.IO.File.ReadAllText(strFileName);
                    }
                    else
                    {
                        MessageBox.Show("ไม่พบไฟล์ ConnectionString.ini", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    using (SqlConnection conn = new SqlConnection(strConnectionString))
                    {
                        conn.Open();

                        // ดึงข้อมูลใหม่จาก Shipping Table
                        string selectQuery = "SELECT * FROM Employee ORDER BY Emp_ID";
                        employeeTable = new DataTable();
                        employeeTable.Load(conn.ExecuteReader(selectQuery));

                        // อัปเดต DataBinding
                        txtEmpID.DataBindings.Clear();
                        textEmpUsername.DataBindings.Clear();
                        textEmpPass.DataBindings.Clear();
                        textEmpFName.DataBindings.Clear();
                        textEmpLName.DataBindings.Clear();
                        textEmpPhone.DataBindings.Clear();
                        textEmpEmail.DataBindings.Clear();
                        textEmpPosition.DataBindings.Clear();
                        textSalary.DataBindings.Clear();
                        dateTimePickerHireDate.DataBindings.Clear();

                        // Bind ข้อมูลใหม่
                        txtEmpID.DataBindings.Add("Text", employeeTable, "Emp_ID");
                        textEmpUsername.DataBindings.Add("Text", employeeTable, "Username");
                        textEmpPass.DataBindings.Add("Text", employeeTable, "Password");
                        textEmpFName.DataBindings.Add("Text", employeeTable, "Emp_FName");
                        textEmpLName.DataBindings.Add("Text", employeeTable, "Emp_LName");
                        textEmpPhone.DataBindings.Add("Text", employeeTable, "Emp_Phone");
                        textEmpEmail.DataBindings.Add("Text", employeeTable, "Emp_Email");
                        textEmpPosition.DataBindings.Add("Text", employeeTable, "Emp_Position");
                        textSalary.DataBindings.Add("Text", employeeTable, "Emp_Salary");

                        // แสดงเฉพาะวันที่ใน Estimated Delivery
                        dateTimePickerHireDate.DataBindings.Add("Text", employeeTable, "Emp_HireDate", true, DataSourceUpdateMode.OnValidation, "", "yyyy-MM-dd");

                        // ตั้งค่า Currency Manager
                        employeeManager = (CurrencyManager)this.BindingContext[employeeTable];
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการโหลดข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            employeeManager.CancelCurrentEdit();
            if (myState.Equals("Add"))
            {
                employeeManager.Position = myEmp;
            }
            SetState("View");
        }

        private void buttonAddNew_Click(object sender, EventArgs e)
        {
            try
            { // ล้างรูปภาพใน PictureBox เมื่อกดปุ่ม Add
                myEmp = employeeManager.Position;
                employeeManager.AddNew();
                SetState("Add");
            }
            catch
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการเพิ่มข้อมูล", "ข้อผิดพลาด",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DialogResult response;
            response = MessageBox.Show("คุณแน่ใจหรือไม่ว่าต้องการลบข้อมูลนี้", "ลบ",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button2);

            if (response == DialogResult.No)
            {
                return;
            }

            try
            {
                // ลบข้อมูลจากฐานข้อมูล
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();

                    // คำสั่ง SQL สำหรับการลบข้อมูลจาก Product
                    string deleteQuery = "DELETE FROM Employee WHERE Emp_ID = @EmployeeID";
                    SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn);
                    deleteCmd.Parameters.AddWithValue("@EmployeeID", Convert.ToInt32(txtEmpID.Text));  // ใช้ Emp_ID ที่แสดงในฟอร์ม

                    int rowsAffected = deleteCmd.ExecuteNonQuery();  // ส่งคำสั่งลบไปยังฐานข้อมูล

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("ลบข้อมูลสินค้าเรียบร้อยแล้ว", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // รีเฟรชข้อมูลใน productTable
                        employeeTable.Clear();  // เคลียร์ข้อมูลเดิมในตาราง
                        employeeAdapter.Fill(employeeTable);  // โหลดข้อมูลใหม่จากฐานข้อมูล

                        // ปรับตำแหน่งใน productManager
                        if (employeeManager.Position >= employeeManager.Count)
                        {
                            employeeManager.Position = employeeManager.Count - 1;  // กรณีที่ลบรายการสุดท้าย
                        }
                    }
                    else
                    {
                        MessageBox.Show("ไม่พบข้อมูลที่ต้องการลบ", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการลบข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            Form newShop = new formShop(); // เปลี่ยนเป็นฟอร์มที่ต้องการเปิด
            newShop.Show();  // เปิดฟอร์มใหม่ในลักษณะ modal
            this.Hide(); // ซ่อนฟอร์มปัจจุบัน
        }

        private void formManageEmp_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (myState.Equals("Edit") || myState.Equals("Add"))
            {
                MessageBox.Show("คุณต้องแก้ไขข้อมูลปัจจุบันให้เสร็จก่อนที่จะหยุดโปรแกรม", "",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                e.Cancel = true;
            }
            else
            {
                try
                {
                    SqlCommandBuilder employeeAdapterCommands
                        = new SqlCommandBuilder(employeeAdapter);
                    employeeAdapter.Update(employeeTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการบันทึกฐานข้อมูล: \r\n" + ex.Message,
                        "ข้อผิดพลาดในการบันทึก",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            // Close the connection
            storeConnection.Close();
            // Dispose of the objects
            storeConnection.Dispose();
            employeeCommand.Dispose();
            employeeAdapter.Dispose();
            employeeTable.Dispose();

            Application.Exit();
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            if (textFind.Text.Equals(""))
            {
                return; // ถ้าไม่ได้กรอกข้อความให้หยุดการทำงาน
            }
            int savedRow = employeeManager.Position; // บันทึกตำแหน่งของแถวที่เลือกในตาราง
            DataRow[] foundRows; // ตัวแปรสำหรับเก็บแถวที่พบ

            // เรียงข้อมูลใน DataTable ตาม Emp_ID ก่อนค้นหา
            employeeTable.DefaultView.Sort = "Emp_ID ASC";

            // ค้นหาชื่อพนักงานที่มี textFind.Text ปรากฏอยู่ที่ไหนก็ได้
            foundRows = employeeTable.Select("Emp_FName LIKE '%" + textFind.Text + "%'");

            // ถ้าไม่พบข้อมูลใดๆ ให้รีเซ็ตตำแหน่งกลับไปยังตำแหน่งเดิม
            if (foundRows.Length == 0)
            {
                employeeManager.Position = savedRow; // กำหนดตำแหน่งกลับไปที่แถวเดิม
            }
            else
            {
                // ถ้าพบข้อมูล ให้เลือกแถวที่พบ (เลือกตาม Emp_ID ลำดับแรกที่เจอ)
                int foundRowIndex = employeeTable.Rows.IndexOf(foundRows[0]);

                // กำหนดตำแหน่งของ employeeManager
                employeeManager.Position = foundRowIndex;
            }
        }
    }
    }
