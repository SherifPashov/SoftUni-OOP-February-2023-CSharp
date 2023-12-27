using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace _01.ClassBoxData
{
    public class Box
    {
        private const string PropartyExseptionMessage = "{0} cannot be zero or negative.";

        public double length;
        public double width;
        public double height;
        public Box(double length, double width, double height)
        {
            Length = length;
            Width = width;
            Height = height;
        }

        public double Length 
        {
            get => length;
            
            set 
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(PropartyExseptionMessage, nameof(Length)));
                }
                length = value;
            } 
        }
        public double Width
        {
            get => width;

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(PropartyExseptionMessage, nameof(Width)));
                }
                width = value;
            }
        }
        public double Height 
        { 
            get => height;

            set 
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(PropartyExseptionMessage, nameof(Height)));
                }
                height = value;
            }
        }
        // Volume = lwh
        //Lateral Surface Area = 2lh + 2wh
        //Surface Area = 2lw + 2lh + 2wh
        public double SurfaceArea() => 2 * Length * Width + LateralSurfaceArea();
        public double LateralSurfaceArea() => 2 * Length * Height + 2 * Width * Height;

        public double Volume() => Length * Width * Height;



    }
}
