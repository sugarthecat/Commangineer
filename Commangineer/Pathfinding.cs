using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Commangineer
{
    class checks
    {
        float d;
        Point p;

        public checks(float d, Point p)
        {
            this.d = d;
            this.p = p;
        }

        public float getFloat()
        {
            return d;
        }

        public Point getPoint()
        {
            return (p);
        }
    }
    internal class Pathfinding
    {
        Tile[,] map;
        float[,] pathStart;
        float[,] pathGoal;
        float[,] pathTotal;
        bool[,] search;
        Point[,] enter;
        List<checks> check = new List<checks>();

        Point pos;
        Point goal;


        public Pathfinding(Tile[,] m)
        {
            int width = m.GetLength(0);
            int height = m.GetLength(1);


            map = m;


            pathStart = new float[width,height];
            pathGoal = new float[width,height];
            pathTotal = new float[width,height];
            search = new bool[width,height];
            enter = new Point[width,height];
        }

        public void find(Point p, Point g)
        {
            bool goalReach = false;
            pos = p;
            goal = g;

            pathStart[pos.X, pos.Y] = 0;
            pathGoal[pos.X, pos.Y] = Vector2.Distance(map[pos.X, pos.Y].GetPosition().ToVector2(), map[goal.X, goal.Y].GetPosition().ToVector2());
            pathTotal[pos.X, pos.Y] = Vector2.Distance(map[pos.X, pos.Y].GetPosition().ToVector2(), map[goal.X, goal.Y].GetPosition().ToVector2());
            search[pos.X,pos.Y] = true;
            enter[pos.X,pos.Y] = pos;

            addSearch(pos);

            while(!goalReach && check.Count > 0)
            {
                check = check.OrderBy(o=>o.getFloat()).ToList();

                addSearch(check[0].getPoint());

                check.RemoveAt(0);
            }

        }

        void addSearch(Point p)
        {
           if(p.X != 0)
            {
                if (map[p.X -1, p.Y].getIsPath())
                {
                    if (pathTotal[p.X - 1, p.Y] == 0 || pathStart[p.X -1, p.Y] > pathStart[p.X,p.Y] + 1)
                    {
                        pathStart[p.X - 1, p.Y] = pathStart[p.X,p.Y] + 1;
                        pathGoal[p.X- 1, p.Y] = Vector2.Distance(map[pos.X - 1, pos.Y].GetPosition().ToVector2(), map[goal.X, goal.Y].GetPosition().ToVector2());
                        pathTotal[p.X - 1, p.Y] = pathGoal[p.X - 1, p.Y] + pathStart[p.X - 1, p.Y];
                        enter[p.X - 1, p.Y] = p;
                    }

                    if (!search[p.X-1, p.Y])
                    {
                        check.Add(new checks(pathTotal[p.X - 1, p.Y], new Point(p.X-1, p.Y))) ;
                    }
                }
            }

            if (p.Y != 0)
            {
                if (map[p.X, p.Y - 1].getIsPath())
                {
                    if (pathTotal[p.X, p.Y - 1] == 0 || pathStart[p.X, p.Y - 1] > pathStart[p.X, p.Y] + 1)
                    {
                        pathStart[p.X, p.Y - 1] = pathStart[p.X, p.Y] + 1;
                        pathGoal[p.X, p.Y - 1] = Vector2.Distance(map[pos.X, pos.Y - 1].GetPosition().ToVector2(), map[goal.X, goal.Y].GetPosition().ToVector2());
                        pathTotal[p.X, p.Y - 1] = pathGoal[p.X, p.Y - 1] + pathStart[p.X, p.Y - 1];
                        enter[p.X, p.Y-1] = p;
                    }

                    if (!search[p.X , p.Y - 1])
                    {
                        check.Add(new checks(pathTotal[p.X, p.Y - 1], new Point(p.X, p.Y - 1)));
                    }
                }
            }

            if (p.X != map.GetLength(0) - 1)
            {
                if (map[p.X + 1, p.Y].getIsPath())
                {
                    if (pathTotal[p.X + 1, p.Y] == 0 || pathStart[p.X + 1, p.Y] > pathStart[p.X, p.Y] + 1)
                    {
                        pathStart[p.X + 1, p.Y] = pathStart[p.X, p.Y] + 1;
                        pathGoal[p.X + 1, p.Y] = Vector2.Distance(map[pos.X + 1, pos.Y].GetPosition().ToVector2(), map[goal.X, goal.Y].GetPosition().ToVector2());
                        pathTotal[p.X + 1, p.Y] = pathGoal[p.X + 1, p.Y] + pathStart[p.X + 1, p.Y];
                        enter[p.X + 1, p.Y] = p;
                    }

                    if (!search[p.X + 1, p.Y])
                    {
                        check.Add(new checks(pathTotal[p.X - 1, p.Y], new Point(p.X - 1, p.Y)));
                    }
                }
            }

            if (p.Y != map.GetLength(1) - 1)
            {
                if (map[p.X, p.Y + 1].getIsPath())
                {
                    if (pathTotal[p.X, p.Y + 1] == 0 || pathStart[p.X, p.Y + 1] > pathStart[p.X, p.Y] + 1)
                    {
                        pathStart[p.X, p.Y + 1] = pathStart[p.X, p.Y] + 1;
                        pathGoal[p.X, p.Y + 1] = Vector2.Distance(map[pos.X, pos.Y + 1].GetPosition().ToVector2(), map[goal.X, goal.Y].GetPosition().ToVector2());
                        pathTotal[p.X, p.Y + 1] = pathGoal[p.X, p.Y + 1] + pathStart[p.X, p.Y + 1];
                        enter[p.X, p.Y + 1] = p;
                    }

                    if (!search[p.X, p.Y + 1])
                    {
                        check.Add(new checks(pathTotal[p.X, p.Y - 1], new Point(p.X, p.Y - 1)));
                    }
                }
            }
        }

    }
}
