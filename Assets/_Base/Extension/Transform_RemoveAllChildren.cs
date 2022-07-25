using UnityEngine;

namespace Base
{
    public static class Transform_RemoveAllChildren
    {
        public static void RemoveAllChildren(this Transform transform)
        {
            foreach(Transform child in transform)
            {
                Object.Destroy(child.gameObject);
            }
        }
    }
}
