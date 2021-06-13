using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Graphs
{
    class Graph<T> where T : IComparable
    {
        public List<Vertex<T>> Vertices;
        public Graph()
        {
            //This Does nothing
            Vertices = new List<Vertex<T>>();
        }
        public void AddVertex(Vertex<T> ver)
        {
            Vertices.Add(ver);
        }
        public void Remove(Vertex<T> Ver)
        {
            for (int i = 0; i < Ver.Others.Count; i++)
            {
                int s = Ver.Others[i].Others.IndexOf(Ver);
                Ver.Others[i].Others[s] = null;
            }
            Vertices.Remove(Ver);
        }
        public void AddEdgeProMax(Vertex<T> Ver, Vertex<T> Ber)
        {
            Ver.Others.Add(Ber);
            Ber.Others.Add(Ver);
        }
        public void RemoveEdgeProMini(Vertex<T> Ver, Vertex<T> Bers)
        {
            int s = Ver.Others.IndexOf(Bers);
            int d = Bers.Others.IndexOf(Ver);
            Ver.Others[d] = null;
            Bers.Others[s] = null;
        }
        public Vertex<T> Seach(T ver)
        {
            for (int i = 0; i < Vertices.Count; i++)
            {
                if (Vertices[i].val.CompareTo(ver) == 0)
                {
                    return Vertices[i];
                }
            }
            return null;
        }
        public List<Vertex<T>> DepthFirstTravesral(Vertex<T> StartingNode)
        {
            List<Vertex<T>> d = new List<Vertex<T>>();
            DFT(StartingNode, new List<Vertex<T>>(), d);
            return d;
        }
        public void DFT(Vertex<T> Current, List<Vertex<T>> BeenThere, List<Vertex<T>> Content)
        {
            BeenThere.Add(Current);
            Content.Add(Current);
            for (int i = 0; i < Current.Others.Count; i++)
            {
                if (!BeenThere.Contains(Current.Others[i]))
                {
                    DFT(Current.Others[i], BeenThere, Content);
                }
            }
            // #     # 
            //    -
            // ________
        }
        public List<Vertex<T>> BreathFirst(Vertex<T> StaringNode)
        {
            List<Vertex<T>> BeenThereRecordedThat = new List<Vertex<T>>();
            Queue<Vertex<T>> NodesToCheck = new Queue<Vertex<T>>();
            NodesToCheck.Enqueue(StaringNode);
            while (NodesToCheck.Count > 0)
            {
                Vertex<T> Current = NodesToCheck.Dequeue();
                BeenThereRecordedThat.Add(Current);
                for (int i = 0; i < Current.Others.Count; i++)
                {
                    if (!BeenThereRecordedThat.Contains(Current.Others[i]))
                    {
                        NodesToCheck.Enqueue(Current.Others[i]);
                    }
                }
            }
            return BeenThereRecordedThat;
        }

        //This is nice and great but doesn't guarentee shortest path
        //1.) Generate all possible paths to the end and keep track of the total weight per path
        //2.) Do a traversal and keep track of the total distance away from start per node
        public List<T> SSSP(Vertex<T> Start, Vertex<T> End)
        {
            Dictionary<List<T>, int> AllPaths = new Dictionary<List<T>, int>();
            SSSPHeper(Start, End, new List<Vertex<T>>(), AllPaths);
            int e = 0;
            List<T> List = new List<T>();
            foreach(KeyValuePair<List<T>, int> kvp in AllPaths)
            {
                if (e == 0)
                {
                    List = kvp.Key;
                    e = kvp.Value;
                }
                else if (kvp.Value < e)
                {
                    List = kvp.Key;
                    e = kvp.Value;
                }
            }
            //FIlter through and find which path had the lowest total cost

            return List;
        }
        #region Recursion way
        void SSSPHeper(Vertex<T> Start, Vertex<T> End, List<Vertex<T>> Path, Dictionary<List<T>, int> Done)
        {
            List<Vertex<T>> vertices = new List<Vertex<T>>(Path);
            vertices.Add(Start);

            if (Start == End)
            {
                var selected = vertices.Select(GetT).ToList();
                Done.Add(selected, selected.Count);

                return;
            }

            //Create a new list with the same stuff as path then add start to that new path
          
            for (int i = 0; i < Start.Others.Count; i++)
            {
                if (!vertices.Contains(Start.Others[i]))
                {
                    SSSPHeper(Start.Others[i], End, vertices, Done);
                }
            }
        }
        #endregion 

        public T GetT(Vertex<T> V)
        {
            return V.val;
        }

        // *    *
        //  ____
        //top hat clan
        //_________
        //|  *  * |
        //        |   | 
        //     __|_______|__       
        //|  _  _ |
        //|  |   ||
        //|    >  | 
        //|   __  |
        //|       |
    }
}
