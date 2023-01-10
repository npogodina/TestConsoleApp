using System;

namespace Coding.Exercise
{
    public class Square
    {
        public int Side;
    }

    public interface IRectangle
    {
        int Width { get; }
        int Height { get; }
    }

    public static class ExtensionMethods
    {
        public static int Area(this IRectangle rc)
        {
            return rc.Width * rc.Height;
        }
    }

    // Try to define a SquareToRectangleAdapter  that adapts the Square  to the IRectangle  interface.
    public class SquareToRectangleAdapter : IRectangle
    {
        public int Width { get; }
        public int Height { get; }

        // Takes a Square and turns it into IRectangle as an instace of SquareToRectangleAdapter
        public SquareToRectangleAdapter(Square square)
        {
            this.Width = this.Height = square.Side;
        }
    }
}