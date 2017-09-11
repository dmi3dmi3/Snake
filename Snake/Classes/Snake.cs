using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Snake.Classes
{
    class Snake
    {
        public Queue<Point> body;
        public Point target;
        public bool flag_target;
        public Point head;
        public Size size;
        public bool flag_GameOver;
        public event EventHandler Lose;

        public enum Sides { Up, Right, Down, Left}
        public Sides side;
        public Sides g_side;

        public Snake(int x, int y)
        {
            body = new Queue<Point>();
            side = Sides.Left;
            size = new Size(x / 30, y / 20);
            head = new Point(14*size.Width, 9*size.Height);
            body.Enqueue(new Point(head.X + size.Width + size.Width, head.Y));
            body.Enqueue(new Point(head.X + size.Width, head.Y));
            body.Enqueue(head);
            target = CreateTarget();
            flag_GameOver = false;
        }

        public void Move()
        {
            Point point = head;

            switch (side)
            {
                case Sides.Up:
                    point.Offset(0, - size.Height);
                    break;
                case Sides.Right:
                    point.Offset(size.Width, 0);
                    break;
                case Sides.Down:
                    point.Offset(0, size.Height);
                    break;
                case Sides.Left:
                    point.Offset(-size.Width, 0);
                    break;
            }
            if (body.Contains(point))
            {
                Lose(this, new EventArgs());
                return;
            }
            if (!(point == target))
            {
                body.Dequeue();
            }
            else
            {
                flag_target = false;
            }
            
            body.Enqueue(point);
            head = point;

        }

        public Point CreateTarget()
        {
            Point point;
            do
            {
                int num1 = MyRandom.RandomInt(30);
                int num2 = MyRandom.RandomInt(20);
                point = new Point(num1*size.Width, num2*size.Height);
            } while (body.Contains(point));
            flag_target = true;

            return point;
        }

        public void Restart(int x, int y)
        {
            body = new Queue<Point>();
            side = Sides.Left;
            size = new Size(x / 30, y / 20);
            head = new Point(14*size.Width, 9*size.Height);
            body.Enqueue(new Point(head.X + size.Width + size.Width, head.Y));
            body.Enqueue(new Point(head.X + size.Width, head.Y));
            body.Enqueue(head);
            target = CreateTarget();
            flag_GameOver = false;
        }

        public Sides IdenSide(Point a, Point b)
        {
            if (a.X - b.X == 0 && a.Y - b.Y == -size.Height)
                return Sides.Up;
            if (a.X - b.X == size.Width && a.Y - b.Y == 0)
                return Sides.Right;
            if (a.X - b.X == 0 && a.Y - b.Y == size.Height)
                return Sides.Down;
            return Sides.Left;
        }
    }
}
