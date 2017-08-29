using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Game_Snake.Classes;

namespace Game_Snake
{
    public partial class Form : System.Windows.Forms.Form
    {
        Snake snake;
        
        public Form()
        {
            InitializeComponent();
            snake = new Snake(pnlGameBoard.Size.Width, pnlGameBoard.Size.Height);
            timer.Start();
            snake.Lose += GameOver;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            flag_changed = false;
            if (!snake.flag_target)
                snake.target = snake.CreateTarget();
            snake.Move();
            Invalidate();

        }

        private void Form_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = Graphics.FromHwnd(pnlGameBoard.Handle);

            g.FillRectangle(Brushes.White, new Rectangle(new Point(0,0), pnlGameBoard.Size));

            foreach (Point p in snake.body)
            {
                g.FillRectangle(Brushes.Black, new Rectangle(Point.Add(p, new Size(1, 1)), Size.Subtract(snake.size, new Size(2, 2))));
            }
                //Прорисовка промежутков
                Point[] g_body = snake.body.ToArray();
                
                for (int i = 0; i < snake.body.Count; i++)
                {
                    if (i != 0)
                    {
                        snake.g_side = snake.IdenSide(g_body[i], g_body[i - 1]);
                        switch (snake.g_side)
                        {
                            case Snake.Sides.Up:
                                g.FillRectangle(Brushes.Black, new Rectangle(
                                    Point.Add(g_body[i - 1], new Size(1, 0)),
                                    new Size(snake.size.Width - 2, 1))
                                    );
                                break;
                            case Snake.Sides.Right:
                                g.FillRectangle(Brushes.Black, new Rectangle(
                                    Point.Add(g_body[i - 1], new Size(snake.size.Width - 1, 1)),
                                    new Size(1, snake.size.Height - 2))
                                    );
                                break;
                            case Snake.Sides.Down:
                                g.FillRectangle(Brushes.Black, new Rectangle(
                                    Point.Add(g_body[i - 1], new Size(1, snake.size.Height - 1)),
                                    new Size(snake.size.Width - 2, 1))
                                    );
                                break;
                            case Snake.Sides.Left:
                                g.FillRectangle(Brushes.Black, new Rectangle(
                                    Point.Add(g_body[i - 1], new Size(0, 1)),
                                    new Size(1, snake.size.Height - 2))
                                    );
                                break;
                        }
                    }
                    if (i!= snake.body.Count-1)
                    {
                        snake.g_side = snake.IdenSide(g_body[i], g_body[i + 1]);
                        switch (snake.g_side)
                        {
                            case Snake.Sides.Up:
                                g.FillRectangle(Brushes.Black, new Rectangle(
                                    Point.Add(g_body[i + 1], new Size(1, 0)),
                                    new Size(snake.size.Width - 2, 1))
                                    );
                                break;
                            case Snake.Sides.Right:
                                g.FillRectangle(Brushes.Black, new Rectangle(
                                    Point.Add(g_body[i + 1], new Size(snake.size.Width - 1, 1)),
                                    new Size(1, snake.size.Height - 2))
                                    );
                                break;
                            case Snake.Sides.Down:
                                g.FillRectangle(Brushes.Black, new Rectangle(
                                    Point.Add(g_body[i + 1], new Size(1, snake.size.Height - 1)),
                                    new Size(snake.size.Width - 2, 1))
                                    );
                                break;
                            case Snake.Sides.Left:
                                g.FillRectangle(Brushes.Black, new Rectangle(
                                    Point.Add(g_body[i + 1], new Size(0, 1)),
                                    new Size(1, snake.size.Height - 2))
                                    );
                                break;
                        }
                    }
                }
                /*
                if (snake.body.Contains(new Point(p.X,p.Y - snake.size.Height)))//Up
                {
                    g.FillRectangle(Brushes.Black, new Rectangle(
                        Point.Add(p, new Size(1, 0)), 
                        new Size(snake.size.Width - 2, 1))
                        );
                }
                if (snake.body.Contains(new Point(p.X + snake.size.Width, p.Y)))//Right
                {
                    g.FillRectangle(Brushes.Black, new Rectangle(
                        Point.Add(p,new Size(snake.size.Width - 1, 1)),
                        new Size(1, snake.size.Height - 2))
                        );
                }
                if (snake.body.Contains(new Point(p.X, p.Y + snake.size.Height)))//Down
                {
                    g.FillRectangle(Brushes.Black, new Rectangle(
                        Point.Add(p, new Size(1, snake.size.Height - 1)),
                        new Size(snake.size.Width - 2, 1))
                        );
                }
                if (snake.body.Contains(new Point(p.X - snake.size.Width, p.Y)))//Left
                {
                    g.FillRectangle(Brushes.Black, new Rectangle(
                        Point.Add(p, new Size(0, 1)),
                        new Size(1, snake.size.Height - 2))
                        );
                }*/
       

            g.FillEllipse(Brushes.Black, new Rectangle(Point.Add(snake.target, new Size(1, 1)), Size.Subtract(snake.size, new Size(2, 2))));
        }

        bool flag_changed = false;
        bool flag_stopped = false;
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (snake.flag_GameOver)
                return;
            if (e.KeyCode == Keys.Space)
            {
                if (timer.Enabled)
                {
                    timer.Stop();
                    flag_stopped = true;
                }
                else
                {
                    timer.Start();
                    flag_stopped = false;
                }
            }
            if (!flag_stopped && !flag_changed)
                switch (e.KeyCode)
                {
                    case Keys.Down:
                    case Keys.S:
                        if (snake.side == Snake.Sides.Up)
                            break;
                        snake.side = Snake.Sides.Down;
                        flag_changed = true;
                        break;
                    case Keys.Left:
                    case Keys.A:
                        if (snake.side == Snake.Sides.Right)
                            break;
                        snake.side = Snake.Sides.Left;
                        flag_changed = true;
                        break;
                    case Keys.Up:
                    case Keys.W:
                        if (snake.side == Snake.Sides.Down)
                            break;
                        snake.side = Snake.Sides.Up;
                        flag_changed = true;
                        break;
                    case Keys.Right:
                    case Keys.D:
                        if (snake.side == Snake.Sides.Left)
                            break;
                        snake.side = Snake.Sides.Right;
                        flag_changed = true;
                        break;
                }
        }

        private void GameOver(object sender, EventArgs e)
        {
            timer.Stop();
            snake.flag_GameOver = true;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            snake.Restart(pnlGameBoard.Size.Width, pnlGameBoard.Size.Height);
            timer.Start();
        }
    }
}
