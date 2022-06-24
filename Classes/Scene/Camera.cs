using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Classes.Utility;

namespace Classes.Scene
{
    public class Camera
    {
        private Point position;
        private Vector direction;
        private double[,] screen;

        public Camera(Point position,Vector direction, int high, int width)
        {
            this.position = position;
            this.direction = direction.Normalize();
            screen = new double[high, width];
        }

        public void RefreshScreen()
        {
            Array.Clear(screen, 0, screen.Length);
        }

        public double[,] Screen
        {
            get
            {
                return screen;
            }
        }
        public Vector Direction
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
            }
        }
        
        public Point Position
        {
            get
            {
                return position;
            }
        }
    }
}
