using UnityEngine;

namespace The25thStudio.GridSystem
{
    public class GridSystem<TGridObject>
    {
        private readonly TGridObject[,] _gridArray;
        private readonly Vector3 _originPosition;

        public GridSystem(int width, int height, float cellSize = 10, Vector3 originPosition = default)
        {
            Width = width;
            Height = height;
            CellSize = cellSize;
            _originPosition = originPosition;
            _gridArray = new TGridObject[Width, Height];
        }

        public void SetValue(int x, int y, TGridObject value)
        {
            if (x >= 0 && y >= 0 && x < Width && y < Height)
            {
                _gridArray[x, y] = value;
            }
        }

        public void SetValue(Vector3 worldPosition, TGridObject value)
        {
            GetXY(worldPosition, out var x, out var y);
            SetValue(x, y, value);
        }

        public TGridObject GetValue(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < Width && y < Height)
            {
                return _gridArray[x, y];
            }

            return default(TGridObject);
        }

        public TGridObject GetValue(Vector3 worldPosition)
        {
            GetXY(worldPosition, out var x, out var y);
            return GetValue(x, y);
        }

        public Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, y) * CellSize + _originPosition;
        }


        #region Getters

        public int Width { get; }

        public int Height { get; }

        public float CellSize { get; }

        #endregion

        #region Private Methods

        private void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            x = Mathf.FloorToInt((worldPosition - _originPosition).x / CellSize);
            y = Mathf.FloorToInt((worldPosition - _originPosition).y / CellSize);
        }

        #endregion
    }
}