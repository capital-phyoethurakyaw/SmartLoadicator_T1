using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Syncfusion
using Syncfusion.WinForms.Controls;

namespace SmartLoadicator.Views
{
    public partial class Main : SfForm
    {
        private int borderSize = 2;
        private Size formSize;

        public Main()
        {
            InitializeComponent();
            this.Padding = new Padding(borderSize);
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            AdjustForm();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            formSize = this.ClientSize;
        }

        private void AdjustForm()
        {
            switch (this.WindowState)
            {
                case FormWindowState.Maximized: //Maximized form (After)
                    this.Padding = new Padding(8, 8, 8, 0);
                    break;
                case FormWindowState.Normal: //Restored form (After)
                    if (this.Padding.Top != borderSize)
                        this.Padding = new Padding(borderSize);
                    break;
            }
        }

        private void CollapseMenu(int menuFlag) // 1 = Main Menu, 2 = Data Entry Meny, 3 = Result Menu
        {
            int origin = this.pnl_MainMenu.Width;

            if(menuFlag == 1)
            {
                //Collapse menu
                if (this.pnl_MainMenu.Width > 90)
                {
                    pnl_MainMenu.Width = 90;
                    pnl_MainMenuHeader.Width = 90;
                    splitContainer1.SplitterDistance = 90;
                    Btn_MainMenu.Dock = DockStyle.Top;
                    foreach (Button menuButton in pnl_MainMenu.Controls.OfType<Button>())
                    {
                        menuButton.Text = "";
                        menuButton.ImageAlign = ContentAlignment.MiddleCenter;
                        menuButton.Padding = new Padding(0);
                    }
                }
                else
                {   //Expand menu
                    pnl_MainMenu.Width = 169;
                    pnl_MainMenuHeader.Width = 169;
                    splitContainer1.SplitterDistance = 169;
                    Btn_MainMenu.Dock = DockStyle.Right;
                    foreach (Button menuButton in pnl_MainMenu.Controls.OfType<Button>())
                    {
                        if (menuButton.Tag == null) continue;
                        menuButton.Text = "   " + menuButton.Tag.ToString(); 
                        menuButton.ImageAlign = ContentAlignment.MiddleLeft;
                        menuButton.Padding = new Padding(10, 0, 0, 0);
                    }
                }
            }
            else if(menuFlag == 2)
            {
                // Collapse menu
                if (this.pnl_DataEntry.Width > 100)
                {
                    pnl_DataEntry.Width = 100;
                    pnl_DataEntryHeader.Width = 100;
                    splitContainer2.SplitterDistance = 100;
                    Btn_DataEntryMenu.Dock = DockStyle.Top;
                    foreach (Button menuButton in pnl_DataEntry.Controls.OfType<Button>())
                    {
                        menuButton.Text = "";
                        menuButton.ImageAlign = ContentAlignment.MiddleCenter;
                        menuButton.Padding = new Padding(0);
                    }
                }
                else
                {   //Expand menu
                    pnl_DataEntry.Width = 169;
                    pnl_MainMenuHeader.Width = 169;
                    splitContainer2.SplitterDistance = 169;
                    Btn_DataEntryMenu.Dock = DockStyle.Right;
                    foreach (Button menuButton in pnl_DataEntry.Controls.OfType<Button>())
                    {
                        if (menuButton.Tag == null) continue;
                        menuButton.Text = "   " + menuButton.Tag.ToString();
                        menuButton.ImageAlign = ContentAlignment.MiddleLeft;
                        menuButton.Padding = new Padding(10, 0, 0, 0);
                    }
                }
            }
            else if(menuFlag == 3)
            {
                // Collapse menu
                if (this.pnl_ResultMenu.Width > 60)
                {
                    pnl_ResultMenu.Width = 59;
                    pnl_ResultHeader.Width = 59;
                    splitContainer3.SplitterDistance = 59;
                    Btn_ResultMenu.Dock = DockStyle.Top;
                    foreach (Button menuButton in pnl_ResultMenu.Controls.OfType<Button>())
                    {
                        menuButton.Text = "";
                        menuButton.ImageAlign = ContentAlignment.MiddleCenter;
                        menuButton.Padding = new Padding(0);
                    }
                }
                else
                {   //Expand menu
                    pnl_ResultMenu.Width = 111;
                    pnl_ResultHeader.Width = 111;
                    splitContainer3.SplitterDistance = 111;
                    Btn_ResultMenu.Dock = DockStyle.Right;
                    foreach (Button menuButton in pnl_ResultMenu.Controls.OfType<Button>())
                    {
                        if (menuButton.Tag == null) continue;
                        menuButton.Text = "   " + menuButton.Tag.ToString(); 
                        menuButton.ImageAlign = ContentAlignment.MiddleLeft;
                        menuButton.Padding = new Padding(10, 0, 0, 0);
                    }
                }
            }
            
        }

        private void Main_MinimumSizeChanged(object sender, EventArgs e)
        {
            formSize = this.ClientSize;
            this.WindowState = FormWindowState.Minimized;
        }

        private void Main_MaximumSizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                formSize = this.ClientSize;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.Size = formSize;
            }
        }

        private void Btn_MainMenu_Click(object sender, EventArgs e)
        {
            CollapseMenu(1);
        }

        private void Btn_ResultMenu_Click(object sender, EventArgs e)
        {
            CollapseMenu(3);
        }

        private void Btn_DataEntryMenu_Click(object sender, EventArgs e)
        {
            CollapseMenu(2);
        }
    }
}
