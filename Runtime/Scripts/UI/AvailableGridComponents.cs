using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace The25thStudio.GridSystem.UI
{
    [CreateAssetMenu(fileName = "Available GridComponents", menuName = "The 25th Studio/Grid System/Available Components", order = 0)]
    public class AvailableGridComponents : ScriptableObject, IEnumerable<GridComponent>
    {
        [SerializeField] private List<GridComponent> gridComponents;

        
        public IEnumerator<GridComponent> GetEnumerator()
        {
            return gridComponents.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}