using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KP_lab1
{
    [SoapInclude(typeof(Point3D))]
    [XmlInclude(typeof(Point3D))]
    [Serializable]
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public static Random rnd = new Random();

        public virtual double Metric()
        { 
            return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
        }

        public override string ToString()
        {
            return string.Format($"({X}, {Y})");
        }
        public Point()
        {
            X = rnd.Next(10);
            Y = rnd.Next(10);
        }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        private int CompareTo(object obj)
        {
            Point? p = (Point)obj;
            return (int)(Metric() - p.Metric());
        }
    }
}
