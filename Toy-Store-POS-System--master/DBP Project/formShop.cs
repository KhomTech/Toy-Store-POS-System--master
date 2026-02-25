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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace DBP_Project
{
    public partial class formShop : Form
    {
        const string strFileName
            = "ConnectionString.ini";

        SqlConnection storeConnection;
        SqlCommand productCommand;
        SqlDataAdapter productAdapter;
        DataTable productTable;
        CurrencyManager productManager;
        private List<DataRow> cart = new List<DataRow>();
        private int currentPage = 1; // หน้าปัจจุบัน
        private const int itemsPerPage = 8; // จำนวนสินค้าที่จะแสดงในแต่ละหน้า
        private int totalPages; // จำนวนหน้าทั้งหมด
        private List<DataRow> products; // รายการสินค้าทั้งหมด

        public formShop()
        {
            InitializeComponent();
        }

        private void formShop_Load(object sender, EventArgs e)
        {
            try
            {
                string strConnectionString = "";
                if (File.Exists(strFileName))
                    strConnectionString = File.ReadAllText(strFileName, Encoding.GetEncoding("Windows-874"));

                storeConnection = new SqlConnection(strConnectionString);
                storeConnection.Open();

                // ตรวจสอบว่า user เป็น Admin หรือไม่
                if (CurrentUser.EmpPosition == "Stock Manager")
                {
                    buttonManageCus.Enabled = false;
                    buttonManageEmp.Enabled = false;
                    buttonOrder.Enabled = false;
                    buttonReportProfix.Enabled = false;
                    buttonReportSell.Enabled = false;
                }
                if (CurrentUser.EmpPosition == "Customer Service")
                {
                    buttonManageProduct.Enabled = false;
                    buttonManageEmp.Enabled = false;
                    buttonReportProfix.Enabled = false;
                    buttonReportSell.Enabled = false;
                }
                if (CurrentUser.EmpPosition == "Cashier")
                {
                    buttonManageProduct.Enabled = false;
                    buttonManageCus.Enabled = false;
                    buttonManageEmp.Enabled = false;
                    buttonReportProfix.Enabled = false;
                    buttonReportSell.Enabled = false;
                }
                if (CurrentUser.EmpPosition == "Finance")
                {
                    buttonManageProduct.Enabled = false;
                    buttonManageCus.Enabled = false;
                    buttonManageEmp.Enabled = false;
                }

                // ดึงข้อมูลหมวดหมู่สินค้าจากฐานข้อมูล
                SqlCommand categoryCommand = new SqlCommand("SELECT * FROM Product_Category ORDER BY Category_Name", storeConnection);
                SqlDataAdapter categoryAdapter = new SqlDataAdapter(categoryCommand);
                DataTable categoryTable = new DataTable();
                categoryAdapter.Fill(categoryTable);

                // เพิ่ม "ทุกประเภท" ใน ComboBox
                DataRow allCategoriesRow = categoryTable.NewRow();
                allCategoriesRow["Category_ID"] = 0; // กำหนดให้ "ทุกประเภท" เป็น 0
                allCategoriesRow["Category_Name"] = "All";
                categoryTable.Rows.InsertAt(allCategoriesRow, 0);

                // เพิ่มหมวดหมู่สินค้าใน ComboBox
                comboCategory.DataSource = categoryTable;
                comboCategory.DisplayMember = "Category_Name";
                comboCategory.ValueMember = "Category_ID";

                comboCategory.SelectedIndexChanged += ComboCategory_SelectedIndexChanged;

                // ดึงข้อมูลสินค้าทั้งหมดที่สถานะเป็น "Sell"
                productCommand = new SqlCommand("SELECT * FROM Product WHERE Status = 'Sell' ORDER BY Product_ID", storeConnection);
                productAdapter = new SqlDataAdapter();
                productAdapter.SelectCommand = productCommand;
                productTable = new DataTable();
                productAdapter.Fill(productTable);

                products = productTable.AsEnumerable().ToList(); // แปลง DataTable เป็น List<DataRow>
                totalPages = (int)Math.Ceiling((double)products.Count / itemsPerPage); // คำนวณจำนวนหน้าทั้งหมด

                DisplayProducts(); // เรียกใช้ฟังก์ชันสำหรับแสดงสินค้าบนฟอร์ม
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการโหลดข้อมูลสินค้า: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                storeConnection.Close();
            }
        }
        private void ComboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPage = 1; // รีเซ็ตหน้าปัจจุบันเมื่อเปลี่ยนหมวดหมู่
            FilterProducts(); // กรองสินค้าตามหมวดหมู่ที่เลือก
            DisplayProducts(); // แสดงสินค้าตามหมวดหมู่ที่เลือก
        }

        private void FilterProducts()
        {
            string selectedCategoryId = comboCategory.SelectedValue.ToString();

            // กรองสินค้าตามหมวดหมู่
            if (selectedCategoryId == "0") // ถ้าเลือก "ทุกประเภท"
            {
                products = productTable.AsEnumerable().ToList(); // แสดงสินค้าทั้งหมด
            }
            else
            {
                products = productTable.AsEnumerable()
                    .Where(p => p["Category_ID"].ToString() == selectedCategoryId)
                    .ToList(); // กรองสินค้าตามหมวดหมู่ที่เลือก
            }

            totalPages = (int)Math.Ceiling((double)products.Count / itemsPerPage); // คำนวณจำนวนหน้าทั้งหมด
        }
        private void DisplayProducts()
        {
            flowLayoutPanelProducts.Controls.Clear(); // ลบสินค้าก่อนหน้า

            // คำนวณสินค้าในหน้าปัจจุบัน
            int startIndex = (currentPage - 1) * itemsPerPage;
            int endIndex = Math.Min(startIndex + itemsPerPage, products.Count);

            // ตรวจสอบว่า `currentPage` ไม่เกินขอบเขตของจำนวนสินค้า
            if (startIndex >= products.Count)
            {
                MessageBox.Show("ไม่มีสินค้า", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // แสดงสินค้าบนหน้าจากหน้าปัจจุบัน
            for (int i = startIndex; i < endIndex; i++)
            {
                DataRow row = products[i];
                int stock = Convert.ToInt32(row["Product_Stock"]); // ดึงจำนวนสินค้าที่เหลือในสต็อก

                // สร้าง Panel สำหรับสินค้า
                Panel productPanel = new Panel();
                productPanel.Size = new Size(200, 350); // ขนาดของ Panel

                // สร้าง PictureBox สำหรับแสดงรูปภาพ
                PictureBox productImage = new PictureBox();
                productImage.Size = new Size(180, 150);
                productImage.Location = new Point(10, 10);
                productImage.SizeMode = PictureBoxSizeMode.StretchImage;

                // แสดงรูปภาพจากฐานข้อมูล
                if (row["Product_Image"] != DBNull.Value)
                {
                    byte[] imageData = (byte[])row["Product_Image"];
                    productImage.Image = ByteArrayToImage(imageData);
                }

                // สร้าง Label สำหรับชื่อสินค้า
                Label productNameLabel = new Label();
                productNameLabel.Text = row["Product_Name"].ToString();
                productNameLabel.TextAlign = ContentAlignment.MiddleCenter;
                productNameLabel.Font = new Font("Comic Sans MS", 12);
                productNameLabel.Location = new Point(10, 170);
                productNameLabel.Size = new Size(180, 30);

                // สร้าง Label สำหรับราคา
                Label productPriceLabel = new Label();
                productPriceLabel.Text = "$" + row["Product_Price"].ToString();
                productPriceLabel.TextAlign = ContentAlignment.MiddleCenter;
                productPriceLabel.Font = new Font("Comic Sans MS", 12);
                productPriceLabel.Location = new Point(10, 210);
                productPriceLabel.Size = new Size(180, 30);

                // สร้าง NumericUpDown สำหรับเลือกจำนวน
                NumericUpDown quantityUpDown = new NumericUpDown();
                quantityUpDown.Size = new Size(180, 30);
                quantityUpDown.Minimum = 1;
                quantityUpDown.Maximum = stock; // ตั้งค่าจำนวนสูงสุดให้เป็นจำนวนที่มีในสต็อก
                quantityUpDown.Location = new Point(10, 250);

                // ปุ่ม "เพิ่มลงในตะกร้า"
                Button addToCartButton = new Button();
                addToCartButton.Text = "Add to cart";
                addToCartButton.Size = new Size(180, 30);
                addToCartButton.Font = new Font("Comic Sans MS", 12);
                addToCartButton.Location = new Point(10, 280);
                addToCartButton.Click += (sender, e) =>
                {
                    int selectedQuantity = (int)quantityUpDown.Value; // จำนวนสินค้าที่ผู้ใช้เลือก

                    // เรียกฟังก์ชัน AddToCart เพื่อเพิ่มสินค้าลงในตะกร้า
                    AddToCart(row, selectedQuantity);
                };

                // ปุ่ม "ดูรายละเอียดสินค้า"
                Button viewDetailsButton = new Button();
                viewDetailsButton.Text = "View detail";
                viewDetailsButton.Size = new Size(180, 30);
                viewDetailsButton.Font = new Font("Comic Sans MS", 12);
                viewDetailsButton.Location = new Point(10, 310);
                viewDetailsButton.Click += (sender, e) => ViewProductDetails(row); // เมื่อคลิกจะเรียกฟังก์ชันดูรายละเอียดสินค้า

                // เพิ่มคอนโทรลลงใน Panel
                productPanel.Controls.Add(productImage);
                productPanel.Controls.Add(productNameLabel);
                productPanel.Controls.Add(productPriceLabel);
                productPanel.Controls.Add(quantityUpDown);
                productPanel.Controls.Add(addToCartButton);
                productPanel.Controls.Add(viewDetailsButton);

                // เพิ่ม Panel ลงใน FlowLayoutPanel
                flowLayoutPanelProducts.Controls.Add(productPanel);
            }

            // อัปเดตปุ่ม "ถัดไป" และ "ย้อนกลับ"
            buttonPrevious.Enabled = currentPage > 1;
            buttonNext.Enabled = currentPage < totalPages;
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
        private void AddToCart(DataRow product, int quantity)
        {
            int stock = Convert.ToInt32(product["Product_Stock"]); // จำนวนสินค้าคงเหลือจากฐานข้อมูล

            // ตรวจสอบว่าในตะกร้ามีสินค้านี้อยู่หรือไม่
            var existingProduct = cart.FirstOrDefault(p => p["Product_ID"].ToString() == product["Product_ID"].ToString());

            if (existingProduct != null)
            {
                // หากสินค้าถูกเพิ่มไว้แล้วในตะกร้า, ตรวจสอบจำนวนในตะกร้า + จำนวนที่เลือก
                int existingQuantity = cart.Count(p => p["Product_ID"].ToString() == product["Product_ID"].ToString());

                if (existingQuantity + quantity > stock)
                {
                    MessageBox.Show($"จำนวนสินค้าคงเหลือไม่เพียงพอ! มีสินค้า {stock} ชิ้นในสต็อก.", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                // หากสินค้าครั้งแรกที่เพิ่มลงในตะกร้า
                if (quantity > stock)
                {
                    MessageBox.Show($"จำนวนสินค้าคงเหลือไม่เพียงพอ! มีสินค้า {stock} ชิ้นในสต็อก.", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // ถ้าไม่เกินจำนวนสต็อกให้เพิ่มสินค้าลงในตะกร้า
            for (int i = 0; i < quantity; i++)
            {
                cart.Add(product); // เพิ่มสินค้าลงในตะกร้าตามจำนวนที่เลือก
            }

            MessageBox.Show($"เพิ่มสินค้า {quantity} ชิ้นลงในตะกร้าเรียบร้อยแล้ว", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ViewProductDetails(DataRow product)
        {
            // แสดงข้อมูลสินค้ารายละเอียด
            string productName = product["Product_Name"].ToString();
            string productDescription = product["Product_Description"].ToString();
            string productPrice = "$" + product["Product_Price"].ToString();

            MessageBox.Show($"ชื่อสินค้า: {productName}\nรายละเอียด: {productDescription}\nราคา: {productPrice}", "รายละเอียดสินค้า", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonViewCart_Click(object sender, EventArgs e)
        {
            // เปิดฟอร์มตะกร้าสินค้า
            formCart cartForm = new formCart(cart); // ส่งข้อมูลตะกร้าไปยังฟอร์ม CartForm
            cartForm.Show();
            
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                DisplayProducts(); // แสดงสินค้าของหน้าถัดไป
            }
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                DisplayProducts(); // แสดงสินค้าของหน้าก่อนหน้า
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            formLogin formLogin = new formLogin();
            formLogin.Show();   
           
        }

        private void buttonManageCus_Click(object sender, EventArgs e)
        {
            // ปิดฟอร์มปัจจุบัน (Store Menu)
            this.Hide();

            // แสดงฟอร์มล็อกอิน
            formManageCus formManageCus = new formManageCus();  // สร้างฟอร์ม Login ใหม่
            formManageCus.Show(); // แสดงฟอร์ม Login
        }

        private void buttonManageProduct_Click(object sender, EventArgs e)
        {
            Form newFormMNProd = new formManageProduct(); // เปลี่ยนเป็นฟอร์มที่ต้องการเปิด
            newFormMNProd.Show();  // เปิดฟอร์มใหม่ในลักษณะ modal
            this.Hide(); // ซ่อนฟอร์มปัจจุบัน
        }

        private void buttonManageEmp_Click(object sender, EventArgs e)
        {
            this.Hide();

            // แสดงฟอร์มล็อกอิน
            formManageEmp formManageEmp = new formManageEmp();  // สร้างฟอร์ม Login ใหม่
            formManageEmp.Show(); // แสดงฟอร์ม Login
        }

        private void buttonReportSell_Click(object sender, EventArgs e)
        {
            this.Hide();

            // แสดงฟอร์มล็อกอิน
            formReportSell formReportSell = new formReportSell();// สร้างฟอร์ม Login ใหม่
            formReportSell.Show(); // แสดงฟอร์ม Login
        }

        private void buttonReportProfix_Click(object sender, EventArgs e)
        {
            this.Hide();

            // แสดงฟอร์มล็อกอิน
            formReportProfit formReportProfix = new formReportProfit();// สร้างฟอร์ม Login ใหม่
            formReportProfix.Show(); // แสดงฟอร์ม Login
        }

        private void buttonOrder_Click(object sender, EventArgs e)
        {
            // เปิดฟอร์มตะกร้าสินค้า
            formOrder orderForm = new formOrder(); // ส่งข้อมูลตะกร้าไปยังฟอร์ม CartForm
            orderForm.Show();
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            SearchProducts();
        }
        private void SearchProducts()
        {
            string searchText = textFind.Text.ToLower(); // แปลงเป็นตัวเล็กหมด
            string selectedCategoryId = comboCategory.SelectedValue?.ToString() ?? "0"; // ป้องกัน null

            if (selectedCategoryId == "0") // ถ้าเลือก "ทุกประเภท"
            {
                products = productTable.AsEnumerable()
                    .Where(p => p["Product_Name"].ToString().ToLower().Contains(searchText))
                    .ToList();
            }
            else
            {
                products = productTable.AsEnumerable()
                    .Where(p => p["Product_Name"].ToString().ToLower().Contains(searchText) &&
                                p["Category_ID"].ToString() == selectedCategoryId)
                    .ToList();
            }

            DisplayProducts();
        }

        private void formShop_FormClosing(object sender, FormClosingEventArgs e)
        {   
            storeConnection.Close(); // ปิดการเชื่อมต่อฐานข้อมูล
            storeConnection.Dispose();
            productCommand.Dispose();
            productAdapter.Dispose();
            productTable.Dispose();
            // ปิดโปรแกรมทั้งหมด
            Application.Exit();
        }
    }
}
