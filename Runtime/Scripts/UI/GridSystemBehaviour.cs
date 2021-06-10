using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace The25thStudio.GridSystem.UI
{
    public class GridSystemBehaviour : MonoBehaviour
    {
        [SerializeField] private Texture2D map;
        [SerializeField] [Min(0)] private float cellSize = 10f;
        [SerializeField] private GridComponent[] gridComponents;
        [SerializeField] private UnityEvent<GridColorMap> postConstructEvent;

        private GridSystem<GameObject> _grid;

        private Dictionary<Color32, GridComponent> _colorGridComponentMap;
        private GridColorMap _colorGameObjectMap;

        private void Start()
        {
            if (map is null) return;

            Load(map);
        }

        public void Load(Texture2D pMap)
        {
            RemoveAllChildren();
            map = pMap;
            _grid = new GridSystem<GameObject>(map.width, map.height, cellSize, transform.position);

            CreateColorGridComponentMap();

            _colorGameObjectMap = new GridColorMap();
            for (var x = 0; x < map.width; x++)
            {
                for (var y = 0; y < map.height; y++)
                {
                    GenerateTile(x, y);
                }
            }

            postConstructEvent.Invoke(_colorGameObjectMap);
        }

        private void RemoveAllChildren()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        private void CreateColorGridComponentMap()
        {
            _colorGridComponentMap = new Dictionary<Color32, GridComponent>();
            foreach (var gridComponent in gridComponents)
            {
                foreach (var color in gridComponent.MappingColors)
                {
                    _colorGridComponentMap.Add(Opaque(color), gridComponent);
                }
            }
        }

        private static Color32 Opaque(Color32 color)
        {
            return new Color32(color.r, color.g, color.b, 1);
        }

        private void GenerateTile(int x, int y)
        {
            if (!_grid.IsEmpty(x, y)) return;

            if (!TryGetPrefab(x, y, out var pixelColor, out var prefab)) return;

            if (!CanBuild(x, y, pixelColor, prefab)) return;

            var position = _grid.GetWorldPosition(x, y);
            var newItem = Instantiate(prefab, position, Quaternion.identity, transform);

            if (newItem == null) return;

            newItem.name = $"{prefab.name} - ({x}, {y})";
            var go = newItem.gameObject;
            _grid.SetValue(x, y, go, newItem.Width, newItem.Height);

            _colorGameObjectMap.Put(pixelColor, go);
        }

        private bool CanBuild(int x, int y, Color32 pixelColor, GridComponent prefab)
        {
            if (!_grid.IsEmpty(x, y)) return false;

            if (prefab.Width == 1 && prefab.Height == 1) return true;

            if (prefab.Width > 1)
            {
                for (var x1 = x + 1; x1 < (x + prefab.Width); x1++)
                {
                    var nextColor = Opaque(map.GetPixel(x1, y));
                    if (!nextColor.Equals(Opaque(pixelColor))) return false;
                }
            }

            if (prefab.Height > 1)
            {
                for (var y1 = y + 1; y1 < (y + prefab.Height); y1++)
                {
                    var nextColor = Opaque(map.GetPixel(x, y1));
                    if (!nextColor.Equals(Opaque(pixelColor))) return false;
                }
            }


            return true;
        }

        private bool TryGetPrefab(int x, int y, out Color32 pixelColor, out GridComponent gridComponent)
        {
            gridComponent = default;
            pixelColor = map.GetPixel(x, y);

            var opaquePixelColorNoAlpha = Opaque(pixelColor);

            return pixelColor.a != 0 && _colorGridComponentMap.TryGetValue(opaquePixelColorNoAlpha, out gridComponent);
        }
    }
}