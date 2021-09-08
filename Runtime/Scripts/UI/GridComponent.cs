using System;
using UnityEngine;

namespace The25thStudio.GridSystem.UI
{
    [Serializable]
    public class GridComponent : MonoBehaviour
    {
        [Header("Size")]
        [SerializeField, Range(1, 45)] private int width = 1;
        [SerializeField, Range(1,30)] private int height = 1;

       
        [Header("Colors")]
        [SerializeField] private Color32[] mappingColors;

        public int Width => width;

        public int Height => height;
        public Color32[] MappingColors => mappingColors;

    }
}