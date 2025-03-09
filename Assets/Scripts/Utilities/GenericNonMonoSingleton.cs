using UnityEngine;

namespace Utilities
{
    // Generic Class for Non MonoBehaviour Singleton class. 
    public class GenericNonMonoSingleton<T> where T : GenericNonMonoSingleton<T>
    {
        private static T instance;
        public static T Instance { get { return instance; } }

        public GenericNonMonoSingleton()
        {
            if (instance == null)
            {
                instance = (T)this;
            }
            else
            {
                Debug.Log(instance + " is Trying to create another instance");
            }
        }
    }

}