using Nevron.UI.WinForm.Controls;
using SmartLoadicator.Views;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SmartLoadicator.Views
{
    public partial class Form2 : Form
    {
        private AppDbContext context;

        public Form2()
        {
            InitializeComponent();
            context = new AppDbContext();
            context.Database.EnsureCreated();
            LoadData();
        }

        private void LoadData()
        {
            var products = context.Products.ToList();
            dataGridView1.DataSource = products;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            var product = new Product
            {
                Name = txtName.Text,
                Price = decimal.Parse(txtPrice.Text)
            };
            context.Products.Add(product);
            context.SaveChanges();
            LoadData();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                var product = context.Products.FirstOrDefault(p => p.ProductId == id);
                if (product != null)
                {
                    product.Name = txtName.Text;
                    product.Price = decimal.Parse(txtPrice.Text);
                    context.SaveChanges();
                    LoadData();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                var product = context.Products.FirstOrDefault(p => p.ProductId == id);
                if (product != null)
                {
                    context.Products.Remove(product);
                    context.SaveChanges();
                    LoadData();
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtPrice.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            }
        }
    }

}