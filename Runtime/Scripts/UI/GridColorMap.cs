using System;
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

        public Dictionary<TKey, List<TType>> GetGroupedComponents<TKey, TType>(Func<Color32, GameObject, TKey> funcGroup)
        {
            var dict = new Dictionary<TKey, List<TType>>();

            foreach (var color in _dict.Keys)
            {
                foreach (var item in _dict[color])
                {
                    var type = item.GetComponent<TType>();
                    if (type is null) continue;

                    List<TType> list;
                    var group = funcGroup(color, item);
                    
                    if (dict.ContainsKey(group))
                    {
                        list = dict[group];
                    }
                    else
                    {
                        list = new List<TType>();
                        dict[group] = list;
                    }

                    list.Add(type);
                }
            }

            return dict;
        }


        public Dictionary<TKey, TType> GetGroupedComponent<TKey, TType>(Func<Color32, GameObject, TKey> funcGroup)
        {
            var dict = new Dictionary<TKey, TType>();

            foreach (var color in _dict.Keys)
            {
                foreach (var item in _dict[color])
                {
                    var type = item.GetComponent<TType>();
                    if (type is null) continue;
                    
                    var group = funcGroup(color, item);
                    dict[group] = type;
                }
            }

            return dict;
        }

    }
}