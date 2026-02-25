using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;


namespace DBP_Project
{
    public partial class formPurchase : Form
    {
        private List<DataRow> cartItems; // ตัวแปรสำหรับเก็บสินค้าที่เลือกในตะกร้า
        private string strConnectionString = ""; // สำหรับเก็บการเชื่อมต่อฐานข้อมูล
        SqlConnection storeConnection;
        SqlCommand memberCommand, addressCommand;
        SqlDataAdapter memberAdapter, addressAdapter;
        DataTable memberTable, addressTable;
        private decimal totalPrice = 0; // ตัวแปรเก็บราคาสินค้ารวมทั้งหมด
        private string status = "";
        private int totalQuan = 0;
        private int memberPhone; // เปลี่ยนเป็น int
        private Dictionary<int, Tuple<DataRow, int>> uniqueCartItems;

        public formPurchase(List<DataRow> cartItems, int memberPhone)
        {
            InitializeComponent();
            this.cartItems = cartItems; // เก็บสินค้าที่เลือกในตะกร้า
            this.memberPhone = memberPhone;
        }

        private void DisplayCartItems()
        {
            flowLayoutPanelCart.Controls.Clear(); // ลบสินค้าก่อนหน้า
            totalPrice = 0; // เริ่มต้นราคาสินค้ารวม

            // สร้าง Dictionary เพื่อเก็บสินค้าและจำนวน
            uniqueCartItems = new Dictionary<int, Tuple<DataRow, int>>(); // สร้าง Dictionary ใหม่ภายในฟังก์ชัน

            // วนลูปผ่านสินค้าที่เลือกทั้งหมดในตะกร้า
            foreach (DataRow row in cartItems)
            {
                int productId = Convert.ToInt32(row["Product_ID"]);
                if (uniqueCartItems.ContainsKey(productId))
                {
                    // หากสินค้านี้มีอยู่แล้วในตะกร้าให้เพิ่มจำนวน
                    uniqueCartItems[productId] = new Tuple<DataRow, int>(row, uniqueCartItems[productId].Item2 + 1);
                }
                else
                {
                    // ถ้าไม่มีสินค้านี้ในตะกร้าให้เพิ่มสินค้าใหม่
                    uniqueCartItems.Add(productId, new Tuple<DataRow, int>(row, 1));
                }
            }

            // แสดงสินค้าทุกชิ้นในตะกร้า
            foreach (var item in uniqueCartItems)
            {
                DataRow row = item.Value.Item1;
                int quantity = item.Value.Item2;  // ใช้ข้อมูล quantity ที่คำนวณได้จาก dictionary
                decimal price = Convert.ToDecimal(row["Product_Price"]);

                // คำนวณราคาสินค้ารวม
                totalPrice += price * quantity;

                // สร้าง Panel สำหรับสินค้าในตะกร้า
                Panel productPanel = new Panel();
                productPanel.Size = new Size(400, 170); // ขนาดของ Panel
                productPanel.BorderStyle = BorderStyle.FixedSingle; // เพิ่มกรอบให้กับ Panel

                // สร้าง PictureBox สำหรับแสดงรูปภาพ
                PictureBox productImage = new PictureBox();
                productImage.Size = new Size(100, 100); // ขนาดของรูปภาพ
                productImage.Location = new Point(10, 20); // ตำแหน่งของรูปภาพใน Panel
                productImage.SizeMode = PictureBoxSizeMode.StretchImage;

                // แสดงรูปภาพจากฐานข้อมูล
                if (row["Product_Image"] != DBNull.Value)
                {
                    byte[] imageData = (byte[])row["Product_Image"];
                    productImage.Image = ByteArrayToImage(imageData);
                }
                else
                {
                    productImage.Image = null;
                }

                // สร้าง Label สำหรับชื่อสินค้า
                Label productNameLabel = new Label();
                productNameLabel.Text = "Product: " + row["Product_Name"].ToString();
                productNameLabel.TextAlign = ContentAlignment.MiddleLeft;
                productNameLabel.Font = new Font("Comic Sans MS", 12);
                productNameLabel.Location = new Point(120, 20); // ตำแหน่ง Label ใต้รูปภาพ
                productNameLabel.Size = new Size(270, 30);

                // สร้าง Label สำหรับราคา
                Label productPriceLabel = new Label();
                productPriceLabel.Text = "Price: $" + price.ToString("F2");
                productPriceLabel.TextAlign = ContentAlignment.MiddleLeft;
                productPriceLabel.Font = new Font("Comic Sans MS", 12);
                productPriceLabel.Location = new Point(120, 60); // ตำแหน่ง Label ใต้ชื่อสินค้า
                productPriceLabel.Size = new Size(180, 30);

                // สร้าง Label สำหรับจำนวนที่ซื้อ
                Label productQuantityLabel = new Label();
                productQuantityLabel.Text = "Quantity: " + quantity.ToString();
                productQuantityLabel.TextAlign = ContentAlignment.MiddleLeft;
                productQuantityLabel.Font = new Font("Comic Sans MS", 12);
                productQuantityLabel.Size = new Size(180, 30);
                productQuantityLabel.Location = new Point(120, 100);

                // เพิ่ม PictureBox, Labels ลงใน Panel
                productPanel.Controls.Add(productImage);
                productPanel.Controls.Add(productNameLabel);
                productPanel.Controls.Add(productPriceLabel);
                productPanel.Controls.Add(productQuantityLabel);

                // เพิ่ม Panel ลงใน FlowLayoutPanel (เพื่อแสดงสินค้าในแนวนอน)
                flowLayoutPanelCart.Controls.Add(productPanel);
            }

            // แสดงราคาสินค้ารวมใน Label
            labelTotalPrice.Text = "Total Price: $" + totalPrice.ToString("F2");
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
        private void DisplayMemberAndAddress()
        {
            try
            {
                // เชื่อมต่อฐานข้อมูล
                if (File.Exists("ConnectionString.ini"))
                {
                    strConnectionString = File.ReadAllText("ConnectionString.ini", Encoding.GetEncoding("Windows-874"));
                }

                storeConnection = new SqlConnection(strConnectionString);
                storeConnection.Open();

                // ดึงข้อมูลจาก Member โดยใช้ Member_ID
                memberCommand = new SqlCommand("SELECT * FROM Member WHERE Member_Phone = @MemberPhone", storeConnection);
                memberCommand.Parameters.AddWithValue("@MemberPhone", CurrentUser.MemberID);
                memberAdapter = new SqlDataAdapter(memberCommand);
                memberTable = new DataTable();
                memberAdapter.Fill(memberTable);

                if (memberTable.Rows.Count > 0)
                {
                    labelMemberName.Text = memberTable.Rows[0]["Member_FName"].ToString() + " " + memberTable.Rows[0]["Member_LName"].ToString();
                    labelEmail.Text = memberTable.Rows[0]["Member_Email"].ToString();
                }

                // ดึงข้อมูลจาก Shipping_Address โดยใช้ Member_ID
                addressCommand = new SqlCommand("SELECT * FROM Shipping_Address JOIN Member ON Member.Member_ID = Shipping_Address.Member_ID WHERE Member_Phone = @MemberPhone", storeConnection);
                addressCommand.Parameters.AddWithValue("@MemberPhone", CurrentUser.MemberID);
                addressAdapter = new SqlDataAdapter(addressCommand);
                addressTable = new DataTable();
                addressAdapter.Fill(addressTable);

                if (addressTable.Rows.Count > 0)
                {
                    // รวมข้อมูลที่อยู่
                    string fullAddress = addressTable.Rows[0]["Recipient_Name"].ToString() + "\n" + "ที่อยู่ " + addressTable.Rows[0]["Address"].ToString() + ", " +
                                         addressTable.Rows[0]["Subdistrict"].ToString() + ", " +
                                         addressTable.Rows[0]["District"].ToString() + ", " +
                                         addressTable.Rows[0]["Province"].ToString() + ", " +
                                         addressTable.Rows[0]["Postal_Code"].ToString() + ", " +
                                         addressTable.Rows[0]["Phone"].ToString();
                    labelAddress.Text = fullAddress;  // แสดงข้อมูลที่อยู่ใน TextBox
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการโหลดข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                storeConnection.Close();
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide(); // ซ่อนฟอร์มปัจจุบัน
        }

        private void formPurchase_Load(object sender, EventArgs e)
        {
            comboPayment.SelectedIndex = 0;
            DisplayCartItems(); // แสดงสินค้าที่เลือกในตะกร้า
            DisplayMemberAndAddress();
            LoadPaymentMethods();
        }
        private int InsertOrders()
        {
            int orderId = -1; // เก็บค่า Order_ID ที่เพิ่มไปในฐานข้อมูล
            int memberId = -1; // ใช้เก็บ Member_ID ที่ค้นหาได้

            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();

                    // ค้นหา Member_ID จากเบอร์โทรศัพท์
                    string findMemberQuery = "SELECT Member_ID FROM Member WHERE Member_Phone = @MemberPhone";
                    using (SqlCommand findCmd = new SqlCommand(findMemberQuery, conn))
                    {
                        findCmd.Parameters.AddWithValue("@MemberPhone", memberPhone);
                        object result = findCmd.ExecuteScalar();

                        if (result != null)
                        {
                            memberId = Convert.ToInt32(result);
                        }
                        else
                        {
                            MessageBox.Show("ไม่พบสมาชิกที่มีเบอร์โทรนี้!", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return -1; // ออกจากฟังก์ชันถ้าไม่พบ Member_ID
                        }
                    }

                    // ถ้าค้นหา Member_ID ได้แล้ว ค่อย Insert ข้อมูลลง Orders
                    string query = "INSERT INTO [Orders] (Member_ID, Emp_ID, Order_Date) " +
                                   "OUTPUT INSERTED.Order_ID VALUES (@MemberID, @EmpID, GETDATE())";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberId);
                        cmd.Parameters.AddWithValue("@EmpID", CurrentUser.EmpID); // ใช้ Emp_ID ของพนักงานปัจจุบัน

                        orderId = (int)cmd.ExecuteScalar(); // ดึง Order_ID ที่เพิ่มไป
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการบันทึก Order: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return orderId;
        }

        private void buttonPlaceOrder_Click(object sender, EventArgs e)
        {
            string selectedPayment = comboPayment.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedPayment))
            {
                MessageBox.Show("กรุณาเลือกวิธีการชำระเงินก่อน!", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            //  บันทึกข้อมูลลงฐานข้อมูล
            int orderId = InsertOrders();
            if (orderId > 0)
            {
                InsertOrderDetails(orderId);

                // อัปเดตสินค้าคงเหลือในตาราง Product
                UpdateProductStock(orderId);  // เรียกใช้ฟังก์ชันลดจำนวนสินค้าคงเหลือใน Product
            }

            MessageBox.Show("ชำระเงินสำเร็จ", "สั่งซื้อสำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // พิมพ์ใบเสร็จหลังจากชำระเงิน
            PrintReceipt(orderId);

            this.Hide();
        }


        private void formPurchase_FormClosing(object sender, FormClosingEventArgs e)
        {
            storeConnection.Close();
            storeConnection.Dispose();
            memberCommand.Dispose();
            addressCommand.Dispose();
            memberAdapter.Dispose();
            addressAdapter.Dispose();
            memberTable.Dispose();
            addressTable.Dispose();
            Application.Exit();
        }

        private void InsertOrderDetails(int orderId)
        {
            try
            {
                if (uniqueCartItems == null)
                {
                    MessageBox.Show("The cart items are null.");
                    return; // หรือทำการออกจากฟังก์ชัน
                }

                int paymentId = (int)comboPayment.SelectedValue; // ดึง Payment_ID จาก comboPayment

                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();

                    foreach (var item in uniqueCartItems)
                    {
                        DataRow row = item.Value.Item1;
                        int quantity = item.Value.Item2;

                        if (row["Product_ID"] == DBNull.Value || row["Product_Price"] == DBNull.Value)
                        {
                            MessageBox.Show("Missing product data in row.");
                            continue;
                        }

                        int productId = Convert.ToInt32(row["Product_ID"]);
                        decimal price = Convert.ToDecimal(row["Product_Price"]);
                        decimal totalPrice = price * quantity;

                        string query = "INSERT INTO Order_Detail (Order_ID, Product_ID, Quantity, Total_Price, Payment_ID) " +
                                       "VALUES (@OrderID, @ProductID, @Quantity, @TotalPrice, @PaymentID)"; // เพิ่ม Payment_ID ในการบันทึก

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@OrderID", orderId);
                            cmd.Parameters.AddWithValue("@ProductID", productId);
                            cmd.Parameters.AddWithValue("@Quantity", quantity);
                            cmd.Parameters.AddWithValue("@TotalPrice", totalPrice);
                            cmd.Parameters.AddWithValue("@PaymentID", paymentId); // บันทึก Payment_ID

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการบันทึก Order Detail: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateProductStock(int orderId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();

                    // สร้างคำสั่ง SQL เพื่ออัปเดตสต็อกสินค้า
                    foreach (var item in uniqueCartItems)
                    {
                        DataRow row = item.Value.Item1;
                        int productId = Convert.ToInt32(row["Product_ID"]);
                        int quantityOrdered = item.Value.Item2;

                        // ตรวจสอบสต็อกก่อนอัปเดต
                        string checkStockQuery = "SELECT Product_Stock FROM Product WHERE Product_ID = @ProductID";
                        SqlCommand checkStockCmd = new SqlCommand(checkStockQuery, conn);
                        checkStockCmd.Parameters.AddWithValue("@ProductID", productId);

                        int currentStock = (int)checkStockCmd.ExecuteScalar();  // ดึงสต็อกสินค้าปัจจุบันจากฐานข้อมูล

                        if (currentStock >= quantityOrdered)
                        {
                            // คำนวณจำนวนสินค้าที่จะอัปเดต
                            string updateStockQuery = "UPDATE Product SET Product_Stock = Product_Stock - @Quantity WHERE Product_ID = @ProductID";
                            SqlCommand updateStockCmd = new SqlCommand(updateStockQuery, conn);
                            updateStockCmd.Parameters.AddWithValue("@Quantity", quantityOrdered); // จำนวนสินค้าที่ลูกค้าซื้อ
                            updateStockCmd.Parameters.AddWithValue("@ProductID", productId);

                            updateStockCmd.ExecuteNonQuery();  // อัปเดตสต็อกสินค้า
                        }
                        else
                        {
                            MessageBox.Show("สินค้าหมดสต็อก! " + row["Product_Name"].ToString(), "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; // หากไม่พอให้หยุดการทำงาน
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการอัปเดตสต็อกสินค้า: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadPaymentMethods()
        {
            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                conn.Open();

                // ดึงข้อมูล Payment_ID และ Payment_Method จากตาราง Payment
                string query = "SELECT Payment_ID, Payment_Method FROM Payment";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable paymentMethods = new DataTable();
                adapter.Fill(paymentMethods);

                // กำหนดให้ comboPayment แสดงค่า Payment_Name และเก็บค่า Payment_ID
                comboPayment.DataSource = paymentMethods;
                comboPayment.DisplayMember = "Payment_Method";  // แสดงชื่อการชำระเงิน (เช่น เงินสด, บัตรเครดิต)
                comboPayment.ValueMember = "Payment_ID";    // เก็บค่า Payment_ID เมื่อผู้ใช้เลือก
            }
        }
        private DataRow GetOrderDetails(int orderId)
        {
            DataTable orderTable = new DataTable();

            // ดึงข้อมูลจาก Order และ Order_Detail
            string query = "SELECT o.Order_ID, o.Order_Date, m.Member_FName, m.Member_LName, " +
                           "sa.Address AS Shipping_Address, p.Product_Name, p.Product_Price " +
                           "FROM [Orders] o " +
                           "JOIN Member m ON o.Member_ID = m.Member_ID " +
                           "JOIN Shipping_Address sa ON m.Member_ID = sa.Member_ID " +
                           "JOIN Order_Detail od ON o.Order_ID = od.Order_ID " +
                           "JOIN Product p ON od.Product_ID = p.Product_ID " +
                           "WHERE o.Order_ID = @OrderID";


            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@OrderID", orderId);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(orderTable);
            }

            if (orderTable.Rows.Count > 0)
            {
                return orderTable.Rows[0]; // คืนค่าข้อมูลใบเสร็จ
            }
            else
            {
                throw new Exception("Order not found.");
            }
        }
        private void PrintReceipt(int orderId)
        {
            // ดึงค่าช่องทางการชำระเงินจาก ComboBox
            string selectedPayment = (comboPayment.SelectedItem as DataRowView)?["Payment_Method"].ToString();

            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += (sender, e) => {
                float x = 50;
                float y = 50;
                float lineSpacing = 30;
                float tableWidth = 500;  // ความกว้างของตาราง
                float columnWidth1 = 200; // ความกว้างของคอลัมน์แรก (สินค้า)
                float columnWidth2 = 80;  // ความกว้างของคอลัมน์ที่สอง (จำนวน)
                float columnWidth3 = 80;  // ความกว้างของคอลัมน์ที่สาม (ราคา)
                float columnWidth4 = 100; // ความกว้างของคอลัมน์ที่สี่ (ราคารวม)

                // กำหนดฟอนต์
                Font headerFont = new Font("Arial", 12, FontStyle.Bold);
                Font contentFont = new Font("Arial", 12, FontStyle.Regular);

                // ข้อมูลใบเสร็จจากฐานข้อมูล
                DataRow orderRow = GetOrderDetails(orderId);

                // พิมพ์ข้อมูลใบเสร็จ
                e.Graphics.DrawString("Receipt", headerFont, Brushes.Black, x, y);
                y += lineSpacing;

                // ร้านค้าและข้อมูลใบเสร็จ
                e.Graphics.DrawString("Store Name: Toy Store", headerFont, Brushes.Black, x, y);
                y += lineSpacing;
                e.Graphics.DrawString("Invoice No: " + orderRow["Order_ID"].ToString(), contentFont, Brushes.Black, x, y);
                y += lineSpacing;
                e.Graphics.DrawString("Date: " + DateTime.Now.ToString("dd/MM/yyyy"), contentFont, Brushes.Black, x, y);
                y += lineSpacing;

                // ข้อมูลลูกค้า
                e.Graphics.DrawString("Customer: " + orderRow["Member_FName"].ToString() + " " + orderRow["Member_LName"].ToString(), contentFont, Brushes.Black, x, y);
                y += lineSpacing;
                e.Graphics.DrawString("Shipping Address: " + orderRow["Shipping_Address"].ToString(), contentFont, Brushes.Black, x, y);
                y += lineSpacing;

                // พิมพ์หัวตาราง
                e.Graphics.DrawString("Product", headerFont, Brushes.Black, x, y);
                e.Graphics.DrawString("Quantity", headerFont, Brushes.Black, x + columnWidth1, y);
                e.Graphics.DrawString("Unit Price", headerFont, Brushes.Black, x + columnWidth1 + columnWidth2, y);
                e.Graphics.DrawString("Total Price", headerFont, Brushes.Black, x + columnWidth1 + columnWidth2 + columnWidth3, y);
                y += lineSpacing;

                // วาดเส้นใต้หัวตาราง
                e.Graphics.DrawLine(Pens.Black, x, y, x + tableWidth, y);
                y += lineSpacing;

                // พิมพ์รายการสินค้า
                foreach (var item in uniqueCartItems)
                {
                    DataRow product = item.Value.Item1;
                    int quantity = item.Value.Item2;
                    decimal price = Convert.ToDecimal(product["Product_Price"]);
                    decimal totalPrice = price * quantity;

                    // พิมพ์ข้อมูลในตาราง
                    e.Graphics.DrawString(product["Product_Name"].ToString(), contentFont, Brushes.Black, x, y);
                    e.Graphics.DrawString(quantity.ToString(), contentFont, Brushes.Black, x + columnWidth1, y);
                    e.Graphics.DrawString("$" + price.ToString("F2"), contentFont, Brushes.Black, x + columnWidth1 + columnWidth2, y);
                    e.Graphics.DrawString("$" + totalPrice.ToString("F2"), contentFont, Brushes.Black, x + columnWidth1 + columnWidth2 + columnWidth3, y);
                    y += lineSpacing;
                }

                // วาดเส้นใต้รายการสินค้า
                e.Graphics.DrawLine(Pens.Black, x, y, x + tableWidth, y);
                y += lineSpacing;

                // ช่องทางชำระเงิน
                e.Graphics.DrawString("Payment Method: " + selectedPayment, contentFont, Brushes.Black, x, y);
                y += lineSpacing;

                // ราคารวม
                e.Graphics.DrawString("Total Price: $" + totalPrice.ToString("F2"), headerFont, Brushes.Black, x, y);
            };

            // แสดงพรีวิวการพิมพ์
            PrintPreviewDialog previewDialog = new PrintPreviewDialog();
            previewDialog.Document = printDoc;
            previewDialog.ShowDialog();
        }



    }
}