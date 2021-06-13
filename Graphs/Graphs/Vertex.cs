using System;
using System.Collections.Generic;
using System.Text;

namespace Graphs
{
    class Vertex<T>
    {
        #region Don't Open
        // |    | -why did you open the region
        //  ____
        //
        #endregion
        public T val;
        public List<Vertex<T>> Others;
        public Vertex(T vall)
        {
            val = vall;
            Others = new List<Vertex<T>>();
        }
    }
}
