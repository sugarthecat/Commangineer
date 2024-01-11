using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.Units
{
    internal class Class1 : TexturedObject
    {

        Point position;
        Point goal = new Point(15,20);
        List<Point> path = new List<Point>();
        Pathfinding pathing;
        float maxMove = 1;
        float move = 1;
        float maxRescan = 20;
        float rescan =0;

        public Class1(Pathfinding pathing)
        {
            position = new Point(0, 0);
            this.pathing = pathing;
        }
        public Point Size
        {
            get
            {
                return new Point(1, 1);
            }
        }
        public Point Position
        {
            get
            {
                return position;
            }
        }

        public void update(float deltaTime)
        {
            if(rescan <= 0)
            {
                rescan = maxRescan;
                path = pathing.find(position, goal);
                Debug.WriteLine("finding");
                Debug.WriteLine(path[0].X + " , " + path[0].Y + " ahhh");
            }
            else
            {
                rescan -= deltaTime;
            }
            if (move <= 0)
            {
                move = maxMove;
                if(path.Count > 0)
                {
                    Point temp = Vector2.Normalize(path[0].ToVector2() - position.ToVector2()).ToPoint();
                    Debug.WriteLine(temp.X + " , " + temp.Y + "temp it up in here");
                    moveX(temp.X);
                    moveY(temp.Y);
                    Debug.WriteLine(position.X + " , " + position.Y);
                    path.RemoveAt(0);
                }
            }
            else
            {
                move -= deltaTime;
            }
        }

        public Texture2D GetTexture()
        {
            return Assets.GetTexture("wood");
        }

        public void moveX(int val)
        {
            position.X += val;
        }
        public void moveY(int val)
        {
            position.Y += val;
        }

        public Point GetSize()
        {
            return Size;
        }

        public Point GetPosition()
        {
            return Position;
        }
    }
}
