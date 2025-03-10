using UnityEngine;

namespace Utilities
{
    /// <summary>
    /// Generic Class for MonoBehaviour Singleton - Will be used by Services.
    /// <summary>
    
    public class GenericMonoSingleton<T> : MonoBehaviour where T : GenericMonoSingleton<T>
    {
        protected static T instance;
        public static T Instance { get { return instance; } }

        protected virtual void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
            }
            else
            {
                instance = (T)this;
            }
        }
    }

}