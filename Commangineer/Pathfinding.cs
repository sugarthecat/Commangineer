﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return ((checks)obj).p == this.p;
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
    internal static class Pathfinding
    {
        static Tile[,] map;
        static float[,] pathStart;
        static float[,] pathGoal;
        static float[,] pathTotal;
        static bool[,] search;
        static Point[,] enter;
        static List<checks> check = new List<checks>();

        static Point pos;
        static Point goal;




        public static List<Point> find(Point origin, Point destination)
        {
            resetVars();
            List<Point> path = new List<Point>();
            Point track;
            bool goalReach = false;
            pos = origin;
            goal = destination;

            if (pos != goal)
            {
                pathStart[pos.X, pos.Y] = 0;
                pathGoal[pos.X, pos.Y] = Vector2.Distance(map[pos.X, pos.Y].Position, map[goal.X, goal.Y].Position);
                pathTotal[pos.X, pos.Y] = Vector2.Distance(map[pos.X, pos.Y].Position, map[goal.X, goal.Y].Position);
                search[pos.X, pos.Y] = true;
                enter[pos.X, pos.Y] = pos;

                addSearch(pos);
                while (!goalReach && check.Count > 0)
                {
                    check = check.OrderBy(o => o.getFloat()).ToList();

                    addSearch(check[0].getPoint());

                    if (check[0].getPoint() == goal)
                    {
                        goalReach = true;
                    }

                    check.RemoveAt(0);
                }

                if (goalReach)
                {
                    track = goal;
                    while (track != pos)
                    {
                        path.Add(track);
                        track = enter[track.X, track.Y];
                    }
                    path.Reverse();
                }
            }
            

            return path;

        }

        static void resetVars()
        {
            int width = Commangineer.Level.GetTileWidth();
            int height = Commangineer.Level.GetTileHeight();

            map = Commangineer.Level.GetTileMap(); ;

            pathStart = new float[width, height];
            pathGoal = new float[width, height];
            pathTotal = new float[width, height];
            search = new bool[width, height];
            enter = new Point[width, height];
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    pathStart[i, j] = 0;
                    pathGoal[i, j] = 0;
                    pathTotal[i, j] = 0;
                    search[i, j] = false;
                    enter[i, j] = new Point(0,0);
                }
            }
            check.Clear();
        }

        static void addSearch(Point p)
        {
           if(p.X != 0)
            {
                if (map[p.X -1, p.Y].IsPath)
                {
                    if (pathTotal[p.X - 1, p.Y] == 0 || pathStart[p.X -1, p.Y] > pathStart[p.X,p.Y] + 1)
                    {
                        pathStart[p.X - 1, p.Y] = pathStart[p.X,p.Y] + 1;
                        pathGoal[p.X- 1, p.Y] = Vector2.Distance(map[p.X - 1, p.Y].Position, map[goal.X, goal.Y].Position);
                        pathTotal[p.X - 1, p.Y] = pathGoal[p.X - 1, p.Y] + pathStart[p.X - 1, p.Y];
                        enter[p.X - 1, p.Y] = p;
                    }

                    if (!search[p.X-1, p.Y])
                    {
                        checks tempCheck = new checks(pathTotal[p.X - 1, p.Y], new Point(p.X - 1, p.Y));
                        if (!check.Contains(tempCheck))
                        {
                            check.Add(tempCheck);
                        }
                        else
                        {
                            check[check.IndexOf(tempCheck)] = tempCheck;
                        }
                    }
                }
            }

            if (p.Y != 0)
            {
                if (map[p.X, p.Y - 1].IsPath)
                {
                    if (pathTotal[p.X, p.Y - 1] == 0 || pathStart[p.X, p.Y - 1] > pathStart[p.X, p.Y] + 1)
                    {
                        pathStart[p.X, p.Y - 1] = pathStart[p.X, p.Y] + 1;
                        pathGoal[p.X, p.Y - 1] = Vector2.Distance(map[p.X, p.Y - 1].Position, map[goal.X, goal.Y].Position);
                        pathTotal[p.X, p.Y - 1] = pathGoal[p.X, p.Y - 1] + pathStart[p.X, p.Y - 1];
                        enter[p.X, p.Y-1] = p;
                    }

                    if (!search[p.X, p.Y - 1])
                    {
                        checks tempCheck = new checks(pathTotal[p.X, p.Y - 1], new Point(p.X, p.Y - 1));
                        if (!check.Contains(tempCheck))
                        {
                            check.Add(tempCheck);
                        }
                        else
                        {
                            check[check.IndexOf(tempCheck)] = tempCheck;
                        }
                    }
                }
            }

            if (p.X != map.GetLength(0) - 1)
            {
                if (map[p.X + 1, p.Y].IsPath)
                {
                    if (pathTotal[p.X + 1, p.Y] == 0 || pathStart[p.X + 1, p.Y] > pathStart[p.X, p.Y] + 1)
                    {
                        pathStart[p.X + 1, p.Y] = pathStart[p.X, p.Y] + 1;
                        pathGoal[p.X + 1, p.Y] = Vector2.Distance(map[p.X + 1, p.Y].Position, map[goal.X, goal.Y].Position);
                        pathTotal[p.X + 1, p.Y] = pathGoal[p.X + 1, p.Y] + pathStart[p.X + 1, p.Y];
                        enter[p.X + 1, p.Y] = p;
                    }

                    if (!search[p.X + 1, p.Y])
                    {
                        checks tempCheck = new checks(pathTotal[p.X + 1, p.Y], new Point(p.X + 1, p.Y));
                        if (!check.Contains(tempCheck))
                        {
                            check.Add(tempCheck);
                        }
                        else
                        {
                            check[check.IndexOf(tempCheck)] = tempCheck;
                        }
                    }
                }
            }

            if (p.Y != map.GetLength(1) - 1)
            {
                if (map[p.X, p.Y + 1].IsPath)
                {
                    if (pathTotal[p.X, p.Y + 1] == 0 || pathStart[p.X, p.Y + 1] > pathStart[p.X, p.Y] + 1)
                    {
                        pathStart[p.X, p.Y + 1] = pathStart[p.X, p.Y] + 1;
                        pathGoal[p.X, p.Y + 1] = Vector2.Distance(map[p.X, p.Y + 1].Position, map[goal.X, goal.Y].Position);
                        pathTotal[p.X, p.Y + 1] = pathGoal[p.X, p.Y + 1] + pathStart[p.X, p.Y + 1];
                        enter[p.X, p.Y + 1] = p;
                    }

                    if (!search[p.X, p.Y + 1])
                    {
                        checks tempCheck = new checks(pathTotal[p.X, p.Y + 1], new Point(p.X, p.Y + 1));
                        if (!check.Contains(tempCheck))
                        {
                            check.Add(tempCheck);
                        }
                        else
                        {
                            check[check.IndexOf(tempCheck)] = tempCheck;
                        }
                        
                    }
                }
            }

            search[p.X, p.Y] = true;
        }

    }
}
