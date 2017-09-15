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
        }

        private void Returning(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            Show();
            form.Hide();
            
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            form.Show();
            form.Start();
            Hide();
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
