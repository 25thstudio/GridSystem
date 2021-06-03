using UnityEngine;

namespace The25thStudio.GridSystem.UI
{
    [System.Serializable]
    public class ColorToPrefab
    {
        [SerializeField] private Color color;
        [SerializeField] private GameObject prefab;

        public Color Color()
        {
            return color;
        }

        public GameObject Prefab()
        {
            return prefab;
        }


    }
}