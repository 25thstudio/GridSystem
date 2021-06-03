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
            var pixelColor = map.GetPixel(x, y);
            if (pixelColor.a == 0) return;

            var prefab = _colorPrefabMap[pixelColor];
            if (prefab is null) return;

            var position = _grid.GetWorldPosition(x, y);
            var newItem = Instantiate(prefab, position, Quaternion.identity, transform);
            newItem.name = $"{prefab.name} - ({x}, {y})";
            // todo Verificar o tamanho do item e setar Widht e Height
            _grid.SetValue(x, y, newItem);

            _colorGameObjectMap.Put(pixelColor, newItem);
        }

    }
}