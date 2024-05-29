using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_2.Service
{
    internal class UnionFind
    {
        private int[] parent;
        private int[] rank;

        public UnionFind(int n)
        {
            parent = new int[n];
            rank = new int[n];
            for (int i = 0; i < n; i++)
            {
                parent[i] = i;
                rank[i] = 0;
            }
        }

        public int Find(int x)
        {
            if (parent[x] != x)
                parent[x] = Find(parent[x]);
            return parent[x];
        }

        public void Union(int x, int y)
        {
            int rootX = Find(x);
            int rootY = Find(y);

            if (rootX != rootY)
            {
                if (rank[rootX] < rank[rootY])
                    parent[rootX] = rootY;
                else if (rank[rootX] > rank[rootY])
                    parent[rootY] = rootX;
                else
                {
                    parent[rootY] = rootX;
                    rank[rootX]++;
                }
            }
        }

        public bool Connected(int x, int y)
        {
            return Find(x) == Find(y);
        }
    }
}
