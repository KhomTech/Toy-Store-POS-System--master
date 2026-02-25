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
using Dapper;
using System.IO;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace DBP_Project
{
    public partial class formManageProduct : Form
    {
        const string strFileName = "ConnectionString.ini";
        private string strConnectionString = "";

        SqlConnection storeConnection;
        SqlCommand productCommand;
        SqlDataAdapter productAdapter;
        DataTable productTable;
        CurrencyManager productManager;

        string myState;
        int myProduct;


        SqlCommand categoryCommand;
        SqlDataAdapter categoryAdapter;
        DataTable categoryTable;

        // ประกาศตัวแปร myImageData ไว้ที่ระดับคลาส
        private byte[] myImageData;  // ตัวแปรสำหรับเก็บข้อมูลรูปภาพ
        public formManageProduct()
        {
            InitializeComponent();
        }

        private Image ByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
                return null;

            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }
        private void LoadProductImage(int productId)
        {
            try
            {
                if (productTable.Rows.Count > 0)
                {
                    // หาข้อมูลที่ตรงกับ Product_ID ที่เลือก
                    DataRow[] rows = productTable.Select("Product_ID = " + productId);
                    if (rows.Length > 0 && !(rows[0]["Product_Image"] is DBNull))
                    {
                        byte[] imageData = (byte[])rows[0]["Product_Image"];
                        pictureProduct.Image = ByteArrayToImage(imageData);
                        pictureProduct.Visible = true; // แสดงรูปภาพ
                    }
                    else
                    {
                        pictureProduct.Visible = false; // ซ่อน PictureBox เมื่อไม่มีรูปภาพ
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการโหลดรูปภาพ: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ManageProduct_Load(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(strFileName))
                {
                    strConnectionString = File.ReadAllText(strFileName, Encoding.GetEncoding("Windows-874"));
                }

                if (string.IsNullOrEmpty(strConnectionString))
                {
                    MessageBox.Show("ยังไม่ได้กำหนดค่า Connection String", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // เปิดการเชื่อมต่อฐานข้อมูล
                storeConnection = new SqlConnection(strConnectionString);
                storeConnection.Open();

                // คำสั่ง SQL สำหรับดึงข้อมูลสินค้า
                productCommand = new SqlCommand("SELECT * FROM Product ORDER BY Product_ID", storeConnection);
                productAdapter = new SqlDataAdapter(productCommand);
                productTable = new DataTable();
                productAdapter.Fill(productTable);

                // Bind controls to data table
                textProdID.DataBindings.Add("Text", productTable, "Product_ID");
                textProdName.DataBindings.Add("Text", productTable, "Product_Name");
                textProdDes.DataBindings.Add("Text", productTable, "Product_Description");
                textProdPrice.DataBindings.Add("Text", productTable, "Product_Price");
                textProdStock.DataBindings.Add("Text", productTable, "Product_Stock");
                textProdCostPrice.DataBindings.Add("Text", productTable, "Cost_Price");
                comboProdStatus.DataBindings.Add("Text", productTable, "Status");

                // โหลดรูปภาพของสินค้า
                LoadProductImage(Convert.ToInt32(textProdID.Text));

                // คำสั่ง SQL สำหรับดึงข้อมูลหมวดหมู่สินค้า
                categoryCommand = new SqlCommand("SELECT * FROM Product_Category ORDER BY Category_ID", storeConnection);
                categoryAdapter = new SqlDataAdapter(categoryCommand);
                categoryTable = new DataTable();
                categoryAdapter.Fill(categoryTable);

                // กำหนดค่าให้ ComboBox
                comboCategoryID.DataSource = categoryTable;
                comboCategoryID.DisplayMember = "Category_Name";
                comboCategoryID.ValueMember = "Category_ID";
                comboCategoryID.DataBindings.Add("SelectedValue", productTable, "Category_ID");

                // กำหนดตัวจัดการข้อมูล
                productManager = (CurrencyManager)this.BindingContext[productTable];

            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการโหลดข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Show();
            SetState("View");
        }
        private void buttonFirst_Click(object sender, EventArgs e)
        {
            productManager.Position = 0;
            LoadProductImage(Convert.ToInt32(textProdID.Text));

        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if (productManager.Position == 0)
            {
                return;
            }
            productManager.Position--;
            LoadProductImage(Convert.ToInt32(textProdID.Text));
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (productManager.Position == productManager.Count - 1)
            {
                return;
            }
            productManager.Position++;
            LoadProductImage(Convert.ToInt32(textProdID.Text));
        }

        private void buttonLast_Click(object sender, EventArgs e)
        {
            productManager.Position = productManager.Count - 1;
            LoadProductImage(Convert.ToInt32(textProdID.Text));
        }
        private void SetState(string appState)
        {
            myState = appState;
            switch (appState)
            {
                case "View":
                    textProdID.BackColor = Color.White;
                    textProdID.ForeColor = Color.Black;
                    comboCategoryID.Enabled = false;
                    textProdName.ReadOnly = true;
                    textProdID.ReadOnly = true;
                    textProdDes.ReadOnly = true;
                    textProdPrice.ReadOnly = true;
                    comboProdStatus.Enabled = false;
                    textProdStock.ReadOnly = true;
                    textProdCostPrice.ReadOnly = true;
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
                    buttonUploadImage.Enabled = false;
                    textProdName.Focus();
                    break;
                default: // Add or Edit if not View
                    textProdID.BackColor = Color.Red;
                    textProdID.ForeColor = Color.White;
                    comboCategoryID.Enabled = true;
                    textProdName.ReadOnly = false;
                    textProdID.ReadOnly = true;
                    textProdDes.ReadOnly = false;
                    textProdPrice.ReadOnly = false;
                    comboProdStatus.Enabled = true;
                    textProdStock.ReadOnly = false;
                    textProdCostPrice.ReadOnly = false;
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
                    buttonUploadImage.Enabled = true;
                    textProdName.Focus();
                    break;
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            SetState("Edit");
        }
        private bool ValidateData()
        {
            string message = "";
            bool allOK = true;
            // ตรวจสอบประเภทสินค้า
            if (string.IsNullOrWhiteSpace(comboCategoryID.Text))
            {
                message += "คุณต้องป้อนประเภทสินค้า\n";
                comboCategoryID.Focus();
                allOK = false;
            }

            // ตรวจสอบชื่อสินค้า
            if (string.IsNullOrWhiteSpace(textProdName.Text))
            {
                message = "คุณต้องป้อนชื่อสินค้า\n";
                textProdName.Focus();
                allOK = false;
            }

            // ตรวจสอบราคาสินค้า
            if (string.IsNullOrWhiteSpace(textProdPrice.Text) || Convert.ToDecimal(textProdPrice.Text) <= 0)
            {
                message += "คุณต้องป้อนราคาสินค้าที่ถูกต้อง\n";
                textProdPrice.Focus();
                allOK = false;
            }
            // ตรวจสอบสถานะสินค้า
            if (string.IsNullOrWhiteSpace(comboProdStatus.Text))
            {
                message += "คุณต้องป้อนสถานะสินค้\n";
                comboProdStatus.Focus();
                allOK = false;
            }
            // ตรวจสอบจำนวนสินค้า
            if (string.IsNullOrWhiteSpace(textProdStock.Text) || Convert.ToInt32(textProdStock.Text) <= 0)
            {
                message += "คุณต้องป้อนจำนวนสินค้าที่ถูกต้อง\n";
                textProdStock.Focus();
                allOK = false;
            }
            // ตรวจสอบราคาต้นทุน
            if (string.IsNullOrWhiteSpace(textProdCostPrice.Text) || Convert.ToDecimal(textProdCostPrice.Text) <= 0)
            {
                message += "คุณต้องป้อนราคาต้นทุนสินค้าที่ถูกต้อง\n";
                textProdCostPrice.Focus();
                allOK = false;
            }

            // ตรวจสอบรูปภาพสินค้า
            if (myImageData == null || myImageData.Length == 0)
            {
                message += "คุณต้องอัปโหลดรูปภาพสินค้าก่อน\n";
                buttonUploadImage.Focus();
                allOK = false;
            }

            // หากตรวจสอบไม่ผ่าน จะขึ้นข้อความแจ้งเตือน
            if (!allOK)
            {
                MessageBox.Show(message, "ข้อผิดพลาดในการตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return allOK;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                // ตรวจสอบว่าอยู่ในสถานะการแก้ไขข้อมูลหรือไม่
                if (myState == "Edit")
                {
                    using (SqlConnection conn = new SqlConnection(strConnectionString))
                    {
                        int currentPosition = productManager.Position;
                        conn.Open();

                        // เช็คว่าไม่มีการเลือกภาพใหม่จากผู้ใช้หรือไม่
                        if (myImageData == null || myImageData.Length == 0)
                        {
                            // โหลดรูปภาพเดิมจากฐานข้อมูลก่อน
                            string selectImageQuery = "SELECT Product_Image FROM Product WHERE Product_ID = @ProductID";
                            SqlCommand selectImageCmd = new SqlCommand(selectImageQuery, conn);
                            selectImageCmd.Parameters.AddWithValue("@ProductID", Convert.ToInt32(textProdID.Text));

                            var imageData = selectImageCmd.ExecuteScalar();
                            if (imageData != DBNull.Value && imageData != null)
                            {
                                myImageData = (byte[])imageData;  // ใช้รูปภาพเดิม
                            }
                            else
                            {
                                myImageData = null;  // ถ้าไม่มีรูปภาพเดิมให้ใช้ค่า null
                            }
                        }
                        // ตรวจสอบข้อมูลก่อนบันทึก
                        if (!ValidateData())
                        {
                            return; // ถ้าข้อมูลไม่ถูกต้องจะไม่ทำการบันทึก
                        }

                        // คำสั่ง SQL สำหรับการอัปเดตข้อมูลสินค้า
                        string updateQuery = "UPDATE Product SET Product_Name = @ProductName, Product_Description = @ProductDescription, " +
                                             "Product_Price = @ProductPrice, Product_Stock = @ProductStock, Cost_Price = @CostPrice, " +
                                             "Status = @Status, Category_ID = @CategoryID, Product_Image = @ProductImage WHERE Product_ID = @ProductID";

                        SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                        updateCmd.Parameters.AddWithValue("@ProductName", textProdName.Text);
                        updateCmd.Parameters.AddWithValue("@ProductDescription", textProdDes.Text);
                        updateCmd.Parameters.AddWithValue("@ProductPrice", Convert.ToDecimal(textProdPrice.Text));
                        updateCmd.Parameters.AddWithValue("@ProductStock", Convert.ToInt32(textProdStock.Text));
                        updateCmd.Parameters.AddWithValue("@CostPrice", Convert.ToDecimal(textProdCostPrice.Text));
                        updateCmd.Parameters.AddWithValue("@Status", comboProdStatus.Text);
                        updateCmd.Parameters.AddWithValue("@CategoryID", comboCategoryID.SelectedValue);
                        updateCmd.Parameters.AddWithValue("@ProductID", Convert.ToInt32(textProdID.Text));  // ใช้ Product_ID จาก textProdID

                        // ตรวจสอบว่ามีรูปภาพใหม่หรือไม่
                        if (myImageData != null && myImageData.Length > 0)
                        {
                            updateCmd.Parameters.AddWithValue("@ProductImage", myImageData); // เพิ่มการบันทึกรูปภาพใหม่
                        }
                        else
                        {
                            // ถ้าไม่มีการเลือกภาพใหม่ ให้บันทึกเป็น null (หรือคงรูปภาพเดิมไว้)
                            updateCmd.Parameters.Add("@ProductImage", SqlDbType.VarBinary).Value = DBNull.Value;
                        }

                        int rowsAffected = updateCmd.ExecuteNonQuery();  // ส่งคำสั่ง UPDATE ไปยังฐานข้อมูล

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("ข้อมูลสินค้าอัปเดตสำเร็จ", "บันทึก", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // รีเฟรชข้อมูลใน productTable
                            productTable.Clear();
                            productAdapter.Fill(productTable);
                            LoadProductImage(Convert.ToInt32(textProdID.Text));  // โหลดรูปภาพใหม่หลังการอัปเดต
                            pictureProduct.Image = null;  // เคลียร์รูปภาพใน PictureBox
                            myImageData = null; // เคลียร์ข้อมูลรูปภาพที่ถูกเก็บไว้
                            productManager.Position = currentPosition;
                            SetState("View"); // เปลี่ยนสถานะกลับไปที่ View
                        }
                        else
                        {
                            MessageBox.Show("ไม่พบข้อมูลที่ต้องการอัปเดต", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else // ในกรณีที่ไม่ได้อยู่ในโหมดแก้ไขหรือเพิ่ม (ใช้สำหรับการเพิ่มข้อมูลใหม่)
                {                // ตรวจสอบข้อมูลก่อนบันทึก
                    if (!ValidateData())
                    {
                        return; // ถ้าข้อมูลไม่ถูกต้องจะไม่ทำการบันทึก
                    }

                    using (SqlConnection conn = new SqlConnection(strConnectionString))
                    {
                        conn.Open();

                        // ตรวจสอบว่า Product_Name นี้มีในฐานข้อมูลแล้วหรือไม่
                        string checkProductQuery = "SELECT COUNT(*) FROM Product WHERE Product_Name = @ProductName";
                        SqlCommand checkProductCmd = new SqlCommand(checkProductQuery, conn);
                        checkProductCmd.Parameters.AddWithValue("@ProductName", textProdName.Text);
                        int productExists = (int)checkProductCmd.ExecuteScalar();

                        if (productExists > 0)
                        {
                            MessageBox.Show("ชื่อสินค้านี้มีอยู่ในระบบแล้ว กรุณาลองใหม่อีกครั้ง.", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // คำสั่ง SQL สำหรับการเพิ่มข้อมูลสินค้าใหม่
                        string insertQuery = "INSERT INTO Product (Product_Name, Product_Description, Product_Price, Product_Stock, Cost_Price, Status, Category_ID, Product_Image) " +
                                             "VALUES (@ProductName, @ProductDescription, @ProductPrice, @ProductStock, @CostPrice, @Status, @CategoryID, @ProductImage); " +
                                             "SELECT SCOPE_IDENTITY();";

                        SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                        insertCmd.Parameters.AddWithValue("@ProductName", textProdName.Text);
                        insertCmd.Parameters.AddWithValue("@ProductDescription", textProdDes.Text);
                        insertCmd.Parameters.AddWithValue("@ProductPrice", Convert.ToDecimal(textProdPrice.Text));
                        insertCmd.Parameters.AddWithValue("@ProductStock", Convert.ToInt32(textProdStock.Text));
                        insertCmd.Parameters.AddWithValue("@CostPrice", Convert.ToDecimal(textProdCostPrice.Text));
                        insertCmd.Parameters.AddWithValue("@Status", comboProdStatus.Text);
                        insertCmd.Parameters.AddWithValue("@CategoryID", comboCategoryID.SelectedValue);

                        // ตรวจสอบว่า myImageData มีค่าเป็น null หรือไม่
                        if (myImageData == null || myImageData.Length == 0)
                        {
                            insertCmd.Parameters.AddWithValue("@ProductImage", DBNull.Value);
                        }
                        else
                        {
                            insertCmd.Parameters.AddWithValue("@ProductImage", myImageData);
                        }

                        int productId = Convert.ToInt32(insertCmd.ExecuteScalar());  // รับ Product_ID ที่ถูกสร้างใหม่

                        MessageBox.Show("ข้อมูลสินค้าใหม่ถูกบันทึกแล้ว", "บันทึก", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // รีเฟรชข้อมูลใน productTable
                        productTable.Clear();
                        productAdapter.Fill(productTable);

                        // ค้นหาตำแหน่งของข้อมูลใหม่ใน productTable
                        int newPosition = productTable.Rows.IndexOf(productTable.Select("Product_ID = " + productId)[0]);

                        // กำหนดตำแหน่งของ productManager ให้ชี้ไปยังข้อมูลใหม่
                        productManager.Position = newPosition;

                        LoadProductImage(productId); // โหลดรูปภาพใหม่หลังการเพิ่ม

                        pictureProduct.Image = null;  // เคลียร์รูปภาพใน PictureBox
                        myImageData = null; // เคลียร์ข้อมูลรูปภาพที่ถูกเก็บไว้
                        SetState("View"); // เปลี่ยนสถานะกลับไปที่ View
                    }
                }
                LoadProductImage(Convert.ToInt32(textProdID.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการบันทึกข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            productManager.CancelCurrentEdit();
            if (myState.Equals("Add"))
            {
                productManager.Position = myProduct;
            }
            SetState("View");
        }

        private void buttonAddNew_Click(object sender, EventArgs e)
        {
            try
            { // ล้างรูปภาพใน PictureBox เมื่อกดปุ่ม Add
                pictureProduct.Image = null;  // ล้างรูปที่แสดงอยู่
                myProduct = productManager.Position;
                productManager.AddNew();
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
                    string deleteQuery = "DELETE FROM Product WHERE Product_ID = @ProductID";
                    SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn);
                    deleteCmd.Parameters.AddWithValue("@ProductID", Convert.ToInt32(textProdID.Text));  // ใช้ Product_ID ที่แสดงในฟอร์ม

                    int rowsAffected = deleteCmd.ExecuteNonQuery();  // ส่งคำสั่งลบไปยังฐานข้อมูล

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("ลบข้อมูลสินค้าเรียบร้อยแล้ว", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // รีเฟรชข้อมูลใน productTable
                        productTable.Clear();  // เคลียร์ข้อมูลเดิมในตาราง
                        productAdapter.Fill(productTable);  // โหลดข้อมูลใหม่จากฐานข้อมูล

                        // ปรับตำแหน่งใน productManager
                        if (productManager.Position >= productManager.Count)
                        {
                            productManager.Position = productManager.Count - 1;  // กรณีที่ลบรายการสุดท้าย
                        }

                        LoadProductImage(Convert.ToInt32(textProdID.Text));  // โหลดรูปภาพใหม่หลังการลบ

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

        private void ManageProduct_FormClosing(object sender, FormClosingEventArgs e)
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
                    SqlCommandBuilder productAdapterCommands
                        = new SqlCommandBuilder(productAdapter);
                    productAdapter.Update(productTable);
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
            productCommand.Dispose();
            productAdapter.Dispose();
            productTable.Dispose();

            Application.Exit();
        }

        private void buttonUploadImage_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(strConnectionString))
            {
                MessageBox.Show("ยังไม่ได้กำหนดค่า Connection String กรุณาตรวจสอบไฟล์ ConnectionString.ini",
                    "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"; // กำหนดชนิดของไฟล์ที่อนุญาตให้เลือก
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                // แสดงภาพใน PictureBox
                pictureProduct.Image = Image.FromFile(filePath); // แสดงภาพใน PictureBox

                // แปลงไฟล์ภาพเป็น byte[] สำหรับบันทึกในฐานข้อมูล
                myImageData = File.ReadAllBytes(filePath); // อ่านไฟล์ภาพเป็น byte[]
            }
        }
        private void SaveImageToDatabase(byte[] imageData, string connectionString)
        {
            try
            {
                string updateQuery = "UPDATE Product SET Product_Image = @Product_Image WHERE Product_ID = @Product_ID";
                using (var connection = new SqlConnection(connectionString))  // ใช้ connectionString ที่ถูกส่งมา
                {
                    connection.Open();
                    connection.Execute(updateQuery, new
                    {
                        Product_Image = imageData,
                        Product_ID = Convert.ToInt32(textProdID.Text) // ใช้ ID ของสินค้าในการอัปเดต
                    });
                }
                MessageBox.Show("บันทึกรูปภาพสำเร็จ", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshProductData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการบันทึกรูปภาพ: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RefreshProductData()
        {
            try
            {
                // โหลดข้อมูลใหม่
                productTable.Clear();
                productAdapter.Fill(productTable);

                // โหลดรูปภาพใหม่
                LoadProductImage(Convert.ToInt32(textProdID.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการโหลดข้อมูลใหม่: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonSellAll_Click(object sender, EventArgs e)
        {
            try
            {
                // แสดงข้อความยืนยันก่อนการอัปเดต
                DialogResult result = MessageBox.Show("คุณต้องการเปลี่ยนสถานะของสินค้าทั้งหมดเป็น 'Sell' หรือไม่?", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กดยืนยัน (Yes)
                if (result == DialogResult.Yes)
                {
                    // เชื่อมต่อฐานข้อมูล
                    using (SqlConnection conn = new SqlConnection(strConnectionString))
                    {
                        conn.Open();

                        // คำสั่ง SQL สำหรับการอัปเดตสถานะสินค้าทั้งหมดเป็น "Sell"
                        string updateQuery = "UPDATE Product SET Status = 'Sell'";

                        SqlCommand updateCmd = new SqlCommand(updateQuery, conn);

                        // ดำเนินการอัปเดต
                        int rowsAffected = updateCmd.ExecuteNonQuery();

                        // ตรวจสอบว่ามีการอัปเดตข้อมูล
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("สถานะสินค้าทั้งหมดถูกอัปเดตเป็น 'Sell' เรียบร้อยแล้ว", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // รีเฟรชข้อมูลสินค้าทั้งหมดใน productTable
                            productTable.Clear();
                            productAdapter.Fill(productTable);

                            // หากต้องการให้ฟอร์มแสดงผลการอัปเดตใหม่
                            SetState("View");
                        }
                        else
                        {
                            MessageBox.Show("ไม่พบสินค้าที่ต้องการอัปเดต", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการอัปเดตสถานะสินค้า: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            if (textFind.Text.Equals(""))
            {
                return; // ถ้าไม่ได้กรอกข้อความให้หยุดการทำงาน
            }

            int savedRow = productManager.Position; // บันทึกตำแหน่งของแถวที่เลือกในตาราง
            DataRow[] foundRows; // ตัวแปรสำหรับเก็บแถวที่พบ

            // ค้นหาชื่อสินค้าที่ตรงกับข้อความใน TextBox (ยังไม่เรียง)
            foundRows = productTable.Select("Product_Name LIKE '%" + textFind.Text + "%'");

            // ถ้าไม่พบข้อมูลใดๆ ให้รีเซ็ตตำแหน่งกลับไปยังตำแหน่งเดิม
            if (foundRows.Length == 0)
            {
                productManager.Position = savedRow; // กำหนดตำแหน่งกลับไปที่แถวเดิม
            }
            else
            {
                // ถ้าพบข้อมูล ให้เลือกแถวที่พบ
                int foundRowIndex = productTable.Rows.IndexOf(foundRows[0]);

                // กำหนดตำแหน่งของ productManager
                productManager.Position = foundRowIndex;

                // โหลดรูปภาพที่ตรงกับสินค้าที่เลือก
                LoadProductImage(Convert.ToInt32(foundRows[0]["Product_ID"]));
            }

            // เรียงลำดับกลับเป็น Product_ID
            productTable.DefaultView.Sort = "Product_ID";
        }
    }
}
