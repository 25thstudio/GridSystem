using System.Collections.Generic;
using UnityEngine;

namespace The25thStudio.GridSystem
{
    public class GridSystem<TGridObject>
    {
        private readonly GridObject<TGridObject>[,] _gridArray;
        private readonly Vector3 _originPosition;

        public GridSystem(int width, int height, float cellSize = 10, Vector3 originPosition = default)
        {
            Width = width;
            Height = height;
            CellSize = cellSize;
            _originPosition = originPosition;
            _gridArray = new GridObject<TGridObject>[Width, Height];

            InitializeGrid();
        }
        #region Properties

        public int Width { get; }

        public int Height { get; }

        public float CellSize { get; }

        #endregion

        #region IsEmpty methods

        public bool IsEmpty(int x, int y, int valueWidth = 1, int valueHeight = 1)
        {
            if (!IsInRange(x, y)) return false;

            for (var x1 = x; x1 < x + valueWidth; x1++)
            {
                for (var y1 = y; y1 < y + valueHeight; y1++)
                {
                    if (!IsInRange(x1, y1)) return false;
                    if (!_gridArray[x1, y1].IsEmpty()) return false;
                }
            }

            return true;
        }

        public bool IsEmpty(Vector3 worldPosition, int valueWidth = 1, int valueHeight = 1)
        {
            GetXY(worldPosition, out var x, out var y);
            return IsEmpty(x, y, valueWidth, valueHeight);
        }

        #endregion

        #region SetValue Methods

        public bool SetValue(int x, int y, TGridObject value, int valueWidth = 1, int valueHeight = 1)
        {
            if (!IsInRange(x, y)) return false;

            bool canAdd = true;
            for (var x1 = x; x1 < x + valueWidth; x1++)
            {
                for (var y1 = y; y1 < y + valueHeight; y1++)
                {
                    if (!IsInRange(x1, y1) || !_gridArray[x1, y1].IsEmpty())
                    {
                        canAdd = false;
                    }
                }
            }

            if (canAdd)
            {
                for (var x1 = x; x1 < x + valueWidth; x1++)
                {
                    for (var y1 = y; y1 < y + valueHeight; y1++)
                    {
                        _gridArray[x1, y1].SetValue(value, valueWidth, valueHeight, x, y);
                    }
                }
            }

            return canAdd;
        }

        public bool SetValue(Vector3 worldPosition, TGridObject value, int valueWidth = 1, int valueHeight = 1)
        {
            GetXY(worldPosition, out var x, out var y);
            return SetValue(x, y, value, valueWidth, valueHeight);
        }

        #endregion

        #region Remove Value methods

        public TGridObject RemoveValue(int x, int y)
        {
            if (!IsInRange(x, y)) return default(TGridObject);

            var value = _gridArray[x, y].Value;
            _gridArray[x, y].ParentGridObject(out var parentX, out var parentY);

            var parent = _gridArray[parentX, parentY];

            var maxX = parentX + parent.Width;
            var maxY = parentY + parent.Height;
            for (var x1 = parentX; x1 < maxX; x1++)
            {
                for (var y1 = parentY; y1 < maxY; y1++)
                {
                    _gridArray[x1, y1].Reset();
                }
            }

            return value;
        }

        public TGridObject RemoveValue(Vector3 worldPosition)
        {
            GetXY(worldPosition, out var x, out var y);
            return RemoveValue(x, y);
        }

        #endregion

        #region Get Value methods

        public List<TGridObject> ToList()
        {
            var list = new List<TGridObject>();

            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    var value = _gridArray[x, y].Value;
                    if (value == null) continue;
                    
                    list.Add(value);
                }
            }

            return list;
        }
        public TGridObject GetValue(int x, int y)
        {
            if (IsInRange(x, y))
            {
                return _gridArray[x, y].Value;
            }

            return default(TGridObject);
        }

        public TGridObject GetValue(Vector3 worldPosition)
        {
            GetXY(worldPosition, out var x, out var y);
            return GetValue(x, y);
        }

        #endregion

        #region Public and util methods

        public Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, y) * CellSize + _originPosition;
        }

        public Vector3 GetGridPosition(Vector3 worldPosition)
        {
            GetXY(worldPosition, out var x, out var y);
            return GetWorldPosition(x, y);
        }

        public void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            x = Mathf.FloorToInt((worldPosition - _originPosition).x / CellSize);
            y = Mathf.FloorToInt((worldPosition - _originPosition).y / CellSize);
        }

        public string PositionName(Vector3 worldPosition)
        {
            GetXY(worldPosition, out var x, out var y);
            return PositionName(x, y);
        }

        public string PositionName(int x, int y)
        {
            var name = _gridArray[x, y].ToString();
            return $"Position {x}, {y} - {name}";
        }

        #endregion


        #region Private methods

        private void InitializeGrid()
        {
            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    _gridArray[x, y] = new GridObject<TGridObject>(x, y);
                }
            }
        }

        private bool IsInRange(int x, int y)
        {
            return (x >= 0 && y >= 0 && x < Width && y < Height);
        }

        #endregion
    }
}