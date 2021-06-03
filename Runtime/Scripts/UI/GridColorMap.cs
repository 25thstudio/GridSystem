using System.Collections.Generic;
using UnityEngine;

namespace The25thStudio.GridSystem.UI
{
    public class GridColorMap
    {
        private readonly Dictionary<Color, List<GameObject>> _dict;

        public GridColorMap()
        {
            _dict = new Dictionary<Color, List<GameObject>>();
        }
        
        internal void Put(Color color, GameObject gameObject)
        {
            List<GameObject> list;
            if (!_dict.ContainsKey(color))
            {
                list = new List<GameObject>();
                _dict[color] = list;
            }
            else
            {
                list = this[color];
            }
            list.Add(gameObject);

        }

        public IEnumerable<Color> Colors => _dict.Keys;

        public List<GameObject> this[Color color] => _dict.ContainsKey(color) ? _dict[color] : default;
    }
}