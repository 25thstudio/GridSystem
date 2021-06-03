using System.Collections.Generic;
using UnityEngine;

namespace The25thStudio.GridSystem.UI
{
    public class PostConstructObject
    {
        public PostConstructObject(Vector2 position, GameObject gameObject, List<GameObject> otherGameObjects)
        {
            Position = position;
            GameObject = gameObject;
            OtherGameObjects = otherGameObjects;
        }

        public GameObject GameObject { get; }

        public Vector2 Position { get; }

        public List<GameObject> OtherGameObjects { get; }
    }
}