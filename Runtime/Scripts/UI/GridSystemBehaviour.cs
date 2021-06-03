using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace The25thStudio.GridSystem.UI
{
    public class GridSystemBehaviour : MonoBehaviour
    {
        [SerializeField] private Texture2D map;
        [SerializeField] private float cellSize = 10f;
        [SerializeField] private ColorToPrefab[] prefabMap;

        private GridSystem<GameObject> _grid;

        private Dictionary<Color, GameObject> _colorMap;

        private void Awake()
        {
            _grid = new GridSystem<GameObject>(map.width, map.height, cellSize, transform.position);
        }

        private void Start()
        {
            _colorMap = prefabMap.ToDictionary(e => e.Color(), e => e.Prefab());


            for (var x = 0; x < map.width; x++)
            {
                for (var y = 0; y < map.height; y++)
                {
                    GenerateTile(x, y);
                }
            }
        }

        private void GenerateTile(int x, int y)
        {
            var pixelColor = map.GetPixel(x, y);
            if (pixelColor.a == 0) return;

            var prefab = _colorMap[pixelColor];
            if (prefab is null) return;

            var position = _grid.GetWorldPosition(x, y);
            var newItem = Instantiate(prefab, position, Quaternion.identity, transform);
            newItem.name = $"{prefab.name} - ({x}, {y})";
            _grid.SetValue(x, y, newItem);
        }

    }
}