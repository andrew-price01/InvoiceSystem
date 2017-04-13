using Group_Project;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace GroupProject
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : Window
    {
        #region Class Level Variables

        /// <summary>
        /// Variable to store info received from database
        /// </summary>
        DataAccess db;

        #endregion
        public Search()
        {
            InitializeComponent();
            fillComboBox();
            fillDataGridView();
        }

        /// <summary>
        /// Method to fill each ComboBox with data
        /// </summary>
        public void fillComboBox()
        {
            try
            {
                db = new DataAccess();

                int iRet = 0; //Number of return values

                DataSet ds = new DataSet(); //Holds the return values

                ds = db.ExecuteSQLStatement("SELECT InvoiceNum, TotalCharge FROM Invoices", ref iRet);

                cb_InvoiceNum.Items.Add("All");
                cb_InvoiceDate.Items.Add("All");
                cb_TotalCharge.Items.Add("All");

                for (int i = 0; i < iRet; i++)
                {
                    cb_InvoiceNum.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                }

                for (int i = 0; i < iRet; i++)
                {
                    ds = db.ExecuteSQLStatement("SELECT DISTINCT FORMAT(InvoiceDate, 'm/d/yyyy') FROM Invoices", ref iRet);
                    cb_InvoiceDate.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                }

                for (int i = 0; i < iRet; i++)
                {
                    ds = db.ExecuteSQLStatement("SELECT DISTINCT TotalCharge FROM Invoices", ref iRet);
                    cb_TotalCharge.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                }

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Method to fill DataGridView
        /// </summary>
        public void fillDataGridView()
        {
            try
            {
                string sSQL;  //Holds an SQL statement

                db = new DataAccess();

                DataTable dt = new DataTable("Invoice");

                sSQL = "SELECT InvoiceNum, FORMAT(InvoiceDate, 'mm/dd/yyyy') AS InvoiceDate, TotalCharge  FROM Invoices";

                dt = db.ExecuteSQLStatement2(sSQL);
                dataGridView1.ItemsSource = dt.DefaultView;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Filter DataGridView, Invoice Number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void cb_InvoiceNum_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            string value = cb_InvoiceNum.SelectedItem.ToString();
            try
            {
                db = new DataAccess();
                DataTable dt = new DataTable("Invoice");
                DataSet ds = new DataSet();

                if (value == "All")
                {
                    dt = db.ExecuteSQLStatement2("SELECT InvoiceNum, FORMAT(InvoiceDate, 'm/dd/yyyy') AS InvoiceDate, TotalCharge FROM Invoices");
                    dataGridView1.ItemsSource = dt.DefaultView;
                }
                else
                {
                    dt = db.ExecuteSQLStatement2("SELECT InvoiceNum, FORMAT(InvoiceDate, 'm/dd/yyyy') AS InvoiceDate, TotalCharge FROM Invoices WHERE InvoiceNum LIKE " + value);
                    dataGridView1.ItemsSource = dt.DefaultView;
                }
               
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Filter DataGridView, Invoice Date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_InvoiceDate_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            string value = cb_InvoiceDate.SelectedItem.ToString();
            try
            {
                db = new DataAccess();

                DataTable dt = new DataTable("Invoice");

                DataSet ds = new DataSet();

                if (value == "All")
                {
                    dt = db.ExecuteSQLStatement2("SELECT InvoiceNum, FORMAT(InvoiceDate, 'm/dd/yyyy') AS InvoiceDate, TotalCharge FROM Invoices");
                    dataGridView1.ItemsSource = dt.DefaultView;
                }
                else
                {
                    dt = db.ExecuteSQLStatement2("SELECT InvoiceNum, FORMAT(InvoiceDate, 'm/dd/yyyy') AS InvoiceDate, TotalCharge FROM Invoices WHERE InvoiceDate LIKE '%" + value + "%'");
                    dataGridView1.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// Filter DataGridView, Total Charge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void cb_TotalCharge_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            string value = cb_TotalCharge.SelectedItem.ToString();
            try
            {
                db = new DataAccess();

                DataTable dt = new DataTable("Invoice");

                DataSet ds = new DataSet();

                if (value == "All")
                {
                    dt = db.ExecuteSQLStatement2("SELECT InvoiceNum, FORMAT(InvoiceDate, 'm/dd/yyyy') AS InvoiceDate, TotalCharge FROM Invoices");
                    dataGridView1.ItemsSource = dt.DefaultView;
                }
                else
                {
                    dt = db.ExecuteSQLStatement2("SELECT InvoiceNum, FORMAT(InvoiceDate, 'm/dd/yyyy') AS InvoiceDate, TotalCharge FROM Invoices WHERE TotalCharge LIKE " + value);
                    dataGridView1.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        private void bt_SelectInvoice_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow MainWindow1 = new MainWindow();

            if (dataGridView1.SelectedItems.Count > 0)
            {

                DataRowView drv = (DataRowView)dataGridView1.SelectedItem;
                String result = (drv["InvoiceNum"]).ToString();

                MainWindow1.Show();
                MainWindow1.editSelectedInvoice(result);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select an invoice to edit.", "Message");
            }
            
        }

    }
}
