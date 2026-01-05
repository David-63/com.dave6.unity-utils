using UnityEngine;

namespace UnityUtils
{
    public abstract class SingletonTemplate<T> : MonoBehaviour where T : SingletonTemplate<T>
    {
        [SerializeField] bool m_Persistent = true;
        public static T instance {get; protected set;}

        protected virtual void Awake()
        {
            var current = this as T;
            if (instance != null && instance != current)
            {
                Destroy(gameObject);
                return;
            }
            instance = current;

            if (m_Persistent) DontDestroyOnLoad(gameObject);
        }

        protected virtual void OnDestroy()
        {
            if (instance == this) instance = null;
        }
    }
}