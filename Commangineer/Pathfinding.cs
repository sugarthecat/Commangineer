using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Commangineer
{
    /// <summary>
    /// A class used to represent a check
    /// </summary>
    public class checks
    {
        private float d; // A float value used in determining the order of checks
        private Point p; // The point of the check

        public checks(float d, Point p)
        {
            this.d = d;
            this.p = p;
        }

        /// <summary>
        /// Reworked equals function to work better with the custom checks object
        /// </summary>
        /// <param name="obj">The object to check if is equal to</param>
        /// <returns>If the other object is the same as the current object</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return ((checks)obj).p == this.p;
        }

        /// <summary>
        /// Gets the float weight of the check
        /// </summary>
        /// <returns>The float weight</returns>
        public float getFloat()
        {
            return d;
        }

        /// <summary>
        /// Gets the point of the check
        /// </summary>
        /// <returns>The check's point</returns>
        public Point getPoint()
        {
            return (p);
        }
    }

    /// <summary>
    /// A static class used to find a path between points
    /// </summary>
    public static class Pathfinding
    {
        private static Tile[,] map;
        private static bool[,] blockedPath;
        private static float[,] pathStart;
        private static float[,] pathGoal;
        private static float[,] pathTotal;
        private static bool[,] search;
        private static Point[,] enter;
        private static List<checks> check = new List<checks>();

        private static Point pos;
        private static Point goal;

        private static Point unitSize;

        /// <summary>
        /// Finds the best path between two points
        /// </summary>
        /// <param name="origin">The starting point</param>
        /// <param name="destination">The ending point</param>
        /// <returns>A list of points to follow</returns>
        public static List<Point> FindPath(Point origin, Point destination)
        {
            unitSize = new Point(1,1);
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
        /// <summary>
        /// Finds the best path between two points
        /// </summary>
        /// <param name="origin">The starting point</param>
        /// <param name="destination">The ending point</param>
        /// <param name="size">The size of whatever we are finding a path for</param>
        /// <returns>A list of points to follow</returns>
        public static List<Point> FindPath(Point origin, Point destination, Point size)
        {
            unitSize = size;
            resetVars();
            List<Point> path = new List<Point>();
            Point track;
            bool goalReach = false;
            pos = origin;
            goal = destination;
            if (blockedPath[goal.X, goal.Y])
            {
                goal.X--;
                if (blockedPath[goal.X,goal.Y])
                {
                    return new List<Point>();
                }
            }
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

        /// <summary>
        /// Resets variables used in order to prevent any errors
        /// </summary>
        private static void resetVars()
        {
            int width = Commangineer.Level.GetTileWidth();
            int height = Commangineer.Level.GetTileHeight();

            map = Commangineer.Level.GetTileMap(); ;

            pathStart = new float[width, height];
            pathGoal = new float[width, height];
            pathTotal = new float[width, height];
            search = new bool[width, height];
            enter = new Point[width, height];
            blockedPath = new bool[width, height];
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    pathStart[i, j] = 0;
                    pathGoal[i, j] = 0;
                    pathTotal[i, j] = 0;
                    search[i, j] = false;
                    enter[i, j] = new Point(0, 0);
                    if (map[i, j].IsSolid)
                    {
                        for (int i2 = 0; i2 < unitSize.X; i2++)
                        {
                            for (int j2 = 0; j2 < unitSize.Y; j2++)
                            {
                                if(i-i2 >= 0 && j-j2 >= 0)
                                {
                                    blockedPath[i - i2, j - j2] = true;
                                }
                            }
                        }
                    }
                }
            }
            check.Clear();
        }

        /// <summary>
        /// Handles math regarding to searching for a best path
        /// </summary>
        /// <param name="p">The point to search a path for</param>
        private static void addSearch(Point p)
        {
            if (p.X != 0)
            {
                if (!blockedPath[p.X - 1, p.Y])
                {
                    if (pathTotal[p.X - 1, p.Y] == 0 || pathStart[p.X - 1, p.Y] > pathStart[p.X, p.Y] + 1)
                    {
                        pathStart[p.X - 1, p.Y] = pathStart[p.X, p.Y] + 1;
                        pathGoal[p.X - 1, p.Y] = Vector2.Distance(map[p.X - 1, p.Y].Position, map[goal.X, goal.Y].Position);
                        pathTotal[p.X - 1, p.Y] = pathGoal[p.X - 1, p.Y] + pathStart[p.X - 1, p.Y];
                        enter[p.X - 1, p.Y] = p;
                    }

                    if (!search[p.X - 1, p.Y])
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
                if (!blockedPath[p.X, p.Y - 1])
                {
                    if (pathTotal[p.X, p.Y - 1] == 0 || pathStart[p.X, p.Y - 1] > pathStart[p.X, p.Y] + 1)
                    {
                        pathStart[p.X, p.Y - 1] = pathStart[p.X, p.Y] + 1;
                        pathGoal[p.X, p.Y - 1] = Vector2.Distance(map[p.X, p.Y - 1].Position, map[goal.X, goal.Y].Position);
                        pathTotal[p.X, p.Y - 1] = pathGoal[p.X, p.Y - 1] + pathStart[p.X, p.Y - 1];
                        enter[p.X, p.Y - 1] = p;
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
                if (!blockedPath[p.X + 1, p.Y])
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
                if (!blockedPath[p.X, p.Y + 1])
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