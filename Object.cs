using System;

namespace SpaceWarProject
{
    public class Object
    {
        public double spawnX { get; set; }
        public double spawnY { get; set; }
        public double Width { get; protected set; }
        public double Height { get; protected set; }

        public Object(double x, double y, double width, double height)
        {
            spawnX = x;
            spawnY = y;
            Width = width;
            Height = height;
        }
    }
}
