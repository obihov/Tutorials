using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_InheritanceOverrideSealed
{
    
    internal class InheritanceExample
    {
        public static void Run()
        {
            Square square = new Square(5);
            Rectangle square2 = new Square(5);
            Console.WriteLine(square.Area);
            Console.WriteLine(square2.Area);

            square.Height = 30;
            var Height = square.Height;
        }
    }

    public class Rectangle
    {
        public virtual int Height { get; set; }
        public virtual int Width { get; set; }

        public int Area
        {
            get { return Height * Width; }
        }

        public Rectangle(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
    }

    public class Square : Rectangle
    {
        public override int Height { get => base.Height; set => base.Height = value; }
        public override int Width { get => base.Width; set { base.Width = value; } }

        public Square(int size) : base(size, size)
        {

        }
    }
}
