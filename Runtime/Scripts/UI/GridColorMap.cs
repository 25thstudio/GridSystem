using System.Collections.Generic;
using UnityEngine;

namespace The25thStudio.GridSystem.UI
{
    public class GridColorMap
    {
        private readonly Dictionary<Color32, List<GameObject>> _dict;

        public GridColorMap()
        {
            _dict = new Dictionary<Color32, List<GameObject>>();
        }
        
        internal void Put(Color32 color, GameObject gameObject)
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

        public IEnumerable<Color32> Colors => _dict.Keys;

        public List<GameObject> this[Color32 color] => _dict.ContainsKey(color) ? _dict[color] : default;
    }
}