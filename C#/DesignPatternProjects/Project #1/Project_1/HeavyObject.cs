using System;

namespace Assi1
{
    public class HeavyObject
    {
        public HeavyObject(float width = 1000f, float length = 1000f, float height = 1000f, float density = 1f) {
            Width = width;
            Length = length;
            Height = height;
            Density = density;
        }

        public float Width;
        public float Length;
        public float Height;

        public float Density;
        
        // Volume = Width * Height * Length
        public float Volume {
            get { return Width * Height * Length; }
            set { }
        }

        // Mass = Volume * Density
        public float Mass {
            get { return Volume * Density; }
            set { }
        }

        public void Print() {
            Console.WriteLine("HeavyObject: (" + Width + "x" + Length + "x" + Height + "), density = " + Density + ", mass = " + Mass);
        }
    }
}
