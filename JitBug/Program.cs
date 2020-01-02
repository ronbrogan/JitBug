using System;
using System.Linq;
using System.Numerics;

namespace JitBug
{
    class Program
    {
        static void Main()
        {
            // Failure
            Console.WriteLine("Replacing array element with new struct, position is (erroneously) zero");
            {
                var bug = Bug.Create();
                bug.MutateBroken();

                Console.WriteLine(bug.Vertices[0].Position);
            }

            // Works
            Console.WriteLine("Replacing array element with new struct, data matches provided value");
            {
                var bug = Bug.Create();
                bug.MutateWorks();

                Console.WriteLine(bug.Vertices[0].Position);
            }

            Console.ReadLine();
        }
    }

    public class Bug
    {
        public static Bug Create()
        {
            return new Bug
            {
                Vertices = Enumerable.Range(1, 100).Select(i => new Vertex(new Vector3(i), Vector2.One)).ToArray()
            };
        }

        public Vertex[] Vertices { get; set; }

        public void MutateBroken()
        {
            for (var i = 0; i < Vertices.Length; i++)
            {
                var vert = Vertices[i];

                Vertices[i] = new Vertex(vert.Position, vert.TexCoords);
            }
        }

        public void MutateWorks()
        {
            for (var i = 0; i < Vertices.Length; i++)
            {
                var vert = Vertices[i];

                Vertices[i] = new Vertex(vert.Position, new Vector2(-1));
            }
        }
    }

    public struct Vertex
    {
        public Vector3 Position;
        public Vector2 TexCoords;

        public Vertex(Vector3 pos, Vector2 tex)
        {
            Position = pos;
            TexCoords = tex;
        }
    }
}
