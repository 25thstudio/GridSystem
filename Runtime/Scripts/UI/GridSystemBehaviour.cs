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
        [SerializeField] [Min(0)] private float cellSize = 10f;
        [SerializeField] private ColorToPrefab[] prefabMap;

        private GridSystem<GameObject> _grid;

        private Dictionary<Color, GameObject> _colorPrefabMap;
        private Dictionary<Color, List<GameObject>> _colorGameObjectMap;

        private void Awake()
        {
            _grid = new GridSystem<GameObject>(map.width, map.height, cellSize, transform.position);
        }

        private void Start()
        {
            _colorPrefabMap = prefabMap.ToDictionary(e => e.Color(), e => e.Prefab());
            _colorGameObjectMap = new Dictionary<Color, List<GameObject>>();
            for (var x = 0; x < map.width; x++)
            {
                for (var y = 0; y < map.height; y++)
                {
                    GenerateTile(x, y);
                }
            }

            CallPostConstruct();
        }

        private void CallPostConstruct()
        {
            var otherGameObjects = _grid.ToList();

            foreach (var otherGameObject in otherGameObjects)
            {
                var gridObjects = otherGameObject.GetComponents<IGridObject>();

                if (gridObjects.Length == 0) continue;
                
                foreach (var gridObject in gridObjects)
                {
                    gridObject.PostConstruct(_colorGameObjectMap);
                }
            }
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

            List<GameObject> list;
            if (!_colorGameObjectMap.ContainsKey(pixelColor))
            {
                list = new List<GameObject>();
                _colorGameObjectMap[pixelColor] = list;
            }
            else
            {
                list = _colorGameObjectMap[pixelColor];
            }
            list.Add(newItem);
        }

    }
}