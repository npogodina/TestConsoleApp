using System;

namespace Coding.Exercise
{
    // In mathematics, the Cartesian Product of sets A and B is defined as the set of all ordered pairs (x, y)
    // such that x belongs to A and y belongs to B.
    // For example, if A = {1, 2} and B = {3, 4, 5},
    // then the Cartesian Product of A and B is {(1, 3), (1, 4), (1, 5), (2, 3), (2, 4), (2, 5)}
    //
    // You are given an example of an inheritance hierarchy which results in Cartesian-product duplication:
    // Set A = shapes = Triangle, Square
    // Set B = drawing type = Vector, Raster
    // Cartesian Product: Vector Triangle, Vector Square, Raster Triangle, Raster Square

    public interface IRenderer
    {
        string WhatToRenderAs { get; }
    }

    public class VectorRenderer : IRenderer
    {
        public string WhatToRenderAs => "lines";
    }

    public class RasterRenderer : IRenderer
    {
        public string WhatToRenderAs => "pixels";
    }

    public abstract class Shape
    {
        public string Name { get; set; }

        protected IRenderer _renderer;

        public Shape(IRenderer renderer)
        {
            _renderer = renderer;
        }
    }

    public class Triangle : Shape
    {
        public Triangle(IRenderer renderer) : base(renderer)
        {
            Name = "Triangle";
        }

        public override string ToString()
        {
            return $"Drawing {Name} as {_renderer.WhatToRenderAs}";
        }
    }

    public class Square : Shape
    {
        public Square(IRenderer renderer) : base(renderer)
        {
            Name = "Square";
        }

        public override string ToString()
        {
            return $"Drawing {Name} as {_renderer.WhatToRenderAs}";
        }
    }
}
