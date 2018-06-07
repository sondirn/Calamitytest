
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using MonoGame.Extended;

namespace Calamity_War
{
    public class QuadTree
    {
        private const int MAX_OBJECTS = 20;
        private const int MAX_LEVELS = 10;

        private int level;
        private List<Physics> actors;
        private RectangleF bounds;
        private QuadTree[] nodes;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="level"></param>
        /// <param name="bounds"></param>
        public QuadTree(int level, RectangleF bounds)
        {
            this.level = level;
            actors = new List<Physics>();
            this.bounds = bounds;
            nodes = new QuadTree[4];
        }
        /// <summary>
        /// clears the quadtree
        /// </summary>
        public void Clear()
        {
            actors.Clear();

            for(int i = 0; i < nodes.Length; i++)
            {
                if(nodes[i] != null)
                {
                    nodes[i].Clear();
                    nodes[i] = null;
                }
            }
        }
        /// <summary>
        /// Splits nodes into 4 sub nodes
        /// </summary>
        private void split()
        {
            float subWidth = (bounds.Width / 2);
            float subHeight = (bounds.Height / 2);
            float x = bounds.X;
            float y = bounds.Y;


            nodes[0] = new QuadTree(level + 1, new RectangleF(x + subWidth, y, subWidth, subHeight));
            nodes[1] = new QuadTree(level + 1, new RectangleF(x, y, subWidth, subHeight));
            nodes[2] = new QuadTree(level + 1, new RectangleF(x, y + subHeight, subWidth, subHeight));
            nodes[3] = new QuadTree(level + 1, new RectangleF(x + subWidth, y + subHeight, subWidth, subHeight));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        private int getIndex(Physics rect)
        {
            int index = -1;
            double verticalMidpoint = bounds.X + (bounds.Width / 2);
            double horizontalMidpoint = bounds.Y + (bounds.Height / 2);

            // if Object fit in top quadrants
            bool topQuadrant = (rect.Data.Bounds.Y < horizontalMidpoint && (rect.Data.Bounds.Y + rect.Data.Bounds.Height) < horizontalMidpoint);
            // if Object in bottomQuadrants
            bool bottomQuadrant = (rect.Data.Bounds.Y > horizontalMidpoint);

            

            //if object in left quadrants
            if(rect.Data.Bounds.X < verticalMidpoint && rect.Data.Bounds.X + rect.Data.Bounds.Width < verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 1;
                }else if (bottomQuadrant)
                {
                    index = 2;
                }
            }
            //if object in right quadrants
            else if(rect.Data.Bounds.X > verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 0;
                }else if (bottomQuadrant)
                {
                    index = 3;
                }
            }
            return index;
        }

        public void Insert(Physics rect)
        {
            if(nodes[0] != null)
            {
                int index = getIndex(rect);

                if(index != -1)
                {
                    nodes[index].Insert(rect);

                    return;
                }
            }

            actors.Add(rect);

            if(actors.Count > MAX_OBJECTS && level < MAX_LEVELS)
            {
                if(nodes[0] == null)
                {
                    split();
                }

                int i = 0;
                while (i < actors.Count)
                {
                    int index = getIndex(actors[i]);
                    if (index != -1)
                    {
                        nodes[index].Insert(actors[i]);
                        actors.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }

        public List<Physics> Retrieve(List<Physics> returnedActors, Physics rect)
        {
            int index = getIndex(rect);
            if(index != -1 && nodes[0] != null)
            {
                nodes[index].Retrieve(returnedActors, rect);
            }

            returnedActors.AddRange(actors);

            return returnedActors;
        }
    }
}
