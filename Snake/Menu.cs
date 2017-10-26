using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Game_Snake;
using System.Threading;

namespace Menu_Snake
{
    public partial class MenuForm : System.Windows.Forms.Form
    {
        Game_Snake.GameForm form = new Game_Snake.GameForm();

        public MenuForm()
        {
            InitializeComponent();
            form.BackToMenu += Returning;
            VelChooseBox.SelectedIndex = 0;
        }

        private void Returning(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            if (form.IsAccessible)
            {
                form.Close();
            }
            Show();
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            int v;
            if (VelChooseBox.SelectedItem != null)
            {
                switch (VelChooseBox.SelectedItem.ToString())
                {
                    case "1":
                        form.Start(5);
                        break;
                    case "2":
                        form.Start(4);
                        break;
                    case "3":
                        form.Start(3);
                        break;
                    case "4":
                        form.Start(2);
                        break;
                    case "5":
                        form.Start(1);
                        break;
                }
                form.Show();
                Hide();
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
