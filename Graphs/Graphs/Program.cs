using System;
using System.Collections.Generic;

namespace Graphs
{
    class Program
    {
        static void Main(string[] args)
        {
            Animal whatever = new Animal(Hi);
            whatever.Speak("HI");

            Graph<string> graph = new Graph<string>();

            Vertex<string> A = new Vertex<string>("A");
            Vertex<string> B = new Vertex<string>("B");
            Vertex<string> C = new Vertex<string>("C");
            Vertex<string> D = new Vertex<string>("D");


            graph.AddVertex(A);
            graph.AddVertex(B);
            graph.AddVertex(C);
            graph.AddVertex(D);

            graph.AddEdgeProMax(A, B);
            graph.AddEdgeProMax(A, C);
            graph.AddEdgeProMax(B, D);
            graph.AddEdgeProMax(C, D);
            //This works

            graph.AddEdgeProMax(B, C);
            //This breaks

            var fast =graph.SSSP(A, D);
            ;

        }

        class Animal
        {
            Func<string, string> speak;

            public Animal(Func<string, string> speak)
            {
                this.speak = speak;
            }

            public void Speak(string message)
            {
                //Console.WriteLine("Hi");
                Console.WriteLine(speak(message));
            }
        }
        static string Hi(string message) 
        {
            return message;
        }
    }
}
