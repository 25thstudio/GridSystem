using System.Collections.Generic;
using UnityEngine;

namespace The25thStudio.GridSystem.UI
{
    public interface IGridObject
    {
        void PostConstruct(Dictionary<Color, List<GameObject>> map);
    }
}