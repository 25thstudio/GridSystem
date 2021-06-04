using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace The25thStudio.GridSystem.UI
{
    public class GridSystemBehaviour : MonoBehaviour
    {
        [SerializeField] private Texture2D map;
        [SerializeField] [Min(0)] private float cellSize = 10f;
        [SerializeField] private ColorToPrefab[] prefabMap;
        [SerializeField] private UnityEvent<GridColorMap> postConstructEvent;
        
        private GridSystem<GameObject> _grid;

        private Dictionary<Color, GameObject> _colorPrefabMap;
        private GridColorMap _colorGameObjectMap;

        private void Awake()
        {
            _grid = new GridSystem<GameObject>(map.width, map.height, cellSize, transform.position);
        }

        private void Start()
        {
            _colorPrefabMap = prefabMap.ToDictionary(e => e.Color(), e => e.Prefab());
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

        private void GenerateTile(int x, int y)
        {
            if (!TryGetPrefab(x, y, out var pixelColor, out var prefab)) return;
            
            var position = _grid.GetWorldPosition(x, y);
            var newItem = Instantiate(prefab, position, Quaternion.identity, transform);
            newItem.name = $"{prefab.name} - ({x}, {y})";
            
            var gridSize = GridSize(newItem);
            _grid.SetValue(x, y, newItem, gridSize.Width, gridSize.Height);

            _colorGameObjectMap.Put(pixelColor, newItem);

        }
        
        private bool TryGetPrefab(int x, int y, out Color pixelColor, out GameObject prefab)
        {
            prefab = default;
            pixelColor = map.GetPixel(x, y);
            
            return pixelColor.a != 0 && _colorPrefabMap.TryGetValue(pixelColor, out prefab);
        }

        private IGridSize GridSize(GameObject newItem)
        {
            return newItem.GetComponent<IGridSize>() ?? new DefaultGridSize();
        }

    }
}