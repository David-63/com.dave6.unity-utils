using UnityEngine;

namespace UnityUtils
{
    public static class GameObjectExtensions
    {
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component 
        {
            T component = gameObject.GetComponent<T>();
            return component != null ? component : gameObject.AddComponent<T>();
        }

        public static bool TryGetComponentInChildren<T>(this Component self, out T component, bool includeInactive = false) where T : Component
        {
            if (self.TryGetComponent<T>(out component)) return true;

            foreach (Transform child in self.transform)
            {
                if (!includeInactive && !child.gameObject.activeInHierarchy) continue;

                if (child.TryGetComponentInChildren(out component, includeInactive)) return true;
            }

            component = null;
            return false;
        }
    }
}
