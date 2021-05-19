using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace The25thStudio.GridSystem
{
    class GridObject<T>
    {
        private readonly int _x;
        private readonly int _y;

        public GridObject(int x, int y)
        {
            _x = x;
            _y = y;
            Reset();
        }

        public void Reset()
        {
            Value = default(T);
            ParentX = _x;
            ParentY = _y;
            Width = 1;
            Height = 1;
        }

        public void SetValue(T value, int width, int height, int parentX, int parentY)
        {
            Value = value;
            Width = width;
            Height = height;
            ParentX = parentX;
            ParentY = ParentY;
        }
        public T Value { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        private int ParentX { get; set; }
        private int ParentY { get; set; }

        public void ParentGridObject(out int x, out int y)
        {
            x = ParentX;
            y = ParentY;
        }

        public bool IsEmpty()
        {
            return Value is null;
        }
        
        public override string ToString()
        {
            return $"Grid Object {_x}, {_y} - {IsEmpty()} - Size {Width}x{Height} - Parent {ParentX}, {ParentY} ";
        }
    }
    
}
