using UnityEngine;

namespace UnityUtils
{
    public abstract class SingletonTemplate<T> : MonoBehaviour where T : SingletonTemplate<T>
    {
        [SerializeField] bool _Persistent = false;
        public static T Instance {get; protected set;}

        protected virtual void Awake()
        {
            var current = this as T;
            if (Instance != null && Instance != current)
            {
                Destroy(gameObject);
                return;
            }
            Instance = current;

            if (_Persistent) DontDestroyOnLoad(gameObject);
        }

        protected virtual void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }
    }
}