using System.Data.Sql;
using System.Data;
namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DataSet ds = new DataSet();
        DataTable dt1 = ds.Tables[0];
        string ln = dt1.Rows[0][1].ToString();
        private void updateButton_Click(object sender, EventArgs e)
        {

        }
    }
}
