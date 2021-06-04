using UnityEngine;

namespace The25thStudio.GridSystem.UI
{
    [System.Serializable]
    public class ColorToPrefab
    {
        [SerializeField] private Color32 color;
        [SerializeField] private GameObject prefab;

        public Color32 Color()
        {
            return color;
        }

        public GameObject Prefab()
        {
            return prefab;
        }


    }
}