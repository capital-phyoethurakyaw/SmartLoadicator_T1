using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SmartLoadicator.Contracts.Views;

using Syncfusion.WinForms.Controls;

namespace SmartLoadicator.Views
{
    public partial class ShellWindow : SfForm
    {        
        public ShellWindow()
        {
            InitializeComponent();

            //Label l1 = new Label();
            //l1.Text = "FOR MENU BAR";
            
            ////l1.Dock = DockStyle.Fill;
            
            //FlowLayoutPanel FP1 = new FlowLayoutPanel();
            //FP1.BorderStyle = BorderStyle.FixedSingle;
            //FP1.Controls.Add(l1);
            //FP1.Dock = DockStyle.Fill;
            
            //tableLayoutPanel1.Controls.Add(FP1);
            //tableLayoutPanel1.SetCellPosition(FP1, new TableLayoutPanelCellPosition(0, 1));
            //tableLayoutPanel1.SetRowSpan(FP1, 2);

            //Label l2 = new Label();
            //l2.Text = "FOR ALARMS MENU";
            ////l2.Dock = DockStyle.Fill;

            //FlowLayoutPanel FP2 = new FlowLayoutPanel();
            //FP2.BorderStyle = BorderStyle.FixedSingle;
            //FP2.Controls.Add(l2);
            //FP2.Dock = DockStyle.Fill;

            //tableLayoutPanel1.Controls.Add(FP2);
            //tableLayoutPanel1.SetCellPosition(FP2, new TableLayoutPanelCellPosition(0, 0));
            //tableLayoutPanel1.SetColumnSpan(FP2, 5);

            //Label l3 = new Label();
            //l3.Text = "FO COLLASIBLE PANEL";
            ////l3.Dock = DockStyle.Fill;

            //FlowLayoutPanel FP3 = new FlowLayoutPanel();
            //FP3.BorderStyle = BorderStyle.FixedSingle;
            //FP3.Controls.Add(l3);
            //FP3.Dock = DockStyle.Fill;

            //tableLayoutPanel1.Controls.Add(FP3);
            //tableLayoutPanel1.SetCellPosition(FP3, new TableLayoutPanelCellPosition(4, 1));
            //tableLayoutPanel1.SetRowSpan(FP3, 2);

            //tbp_DataEntry.Dock = DockStyle.Fill;
            //tableLayoutPanel1.Controls.Add(tbp_DataEntry);
            //tableLayoutPanel1.SetCellPosition(tbp_DataEntry, new TableLayoutPanelCellPosition(1, 2));
            //tableLayoutPanel1.SetColumnSpan(tbp_DataEntry, 3);

            //Label l4 = new Label();
            //l4.Text = "FOR RESULT";

            //FlowLayoutPanel FP4 = new FlowLayoutPanel();
            //FP4.BorderStyle = BorderStyle.FixedSingle;
            //FP4.Controls.Add(l4);
            //FP4.Dock = DockStyle.Fill;

            //tableLayoutPanel1.Controls.Add(FP4);
            //tableLayoutPanel1.SetCellPosition(FP4, new TableLayoutPanelCellPosition(1, 1));


            //Label l5 = new Label();
            //l5.Text = "FOR 2D VIEW";

            //FlowLayoutPanel FP5 = new FlowLayoutPanel();
            //FP5.BorderStyle = BorderStyle.FixedSingle;
            //FP5.Controls.Add(l5);
            //FP5.Dock = DockStyle.Fill;

            //tableLayoutPanel1.Controls.Add(FP5);
            //tableLayoutPanel1.SetCellPosition(FP5, new TableLayoutPanelCellPosition(2, 1));

            //Label l6 = new Label();
            //l6.Text = "FOR 3D VIEW";

            //FlowLayoutPanel FP6 = new FlowLayoutPanel();
            //FP6.BorderStyle = BorderStyle.FixedSingle;
            //FP6.Controls.Add(l6);
            //FP6.Dock = DockStyle.Fill;

            //tableLayoutPanel1.Controls.Add(FP6);
            //tableLayoutPanel1.SetCellPosition(FP6, new TableLayoutPanelCellPosition(3, 1));
            ////tableLayoutPanel1.SetRowSpan(FP1, 2);
        }

        public void CloseWindow()
        {
           
        }

        //public Panel GetNavigationFrame()
        //    => this.panel1;

        public void ShowWindow()
            => Show();

	    private void ShellWindow_Resize(object sender, System.EventArgs e)
        {

            //if (this.panel1.Controls.Count > 0)
            //{
            //    var selectedControl = this.panel1.Controls[0];
            //    if (selectedControl != null)
            //    {
            //        int x = (this.panel1.Width - selectedControl.Width) / 2;
            //        int y = (this.panel1.Height - selectedControl.Height) / 2;
            //        selectedControl.Location = new System.Drawing.Point(x, y);
            //    }
            //}
        }

             private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void sfDataGrid1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
