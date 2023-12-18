using UnityEngine;

namespace Utility
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T s_instance;

        public static T Instance => s_instance;


        protected virtual void Awake()
        {
            if (s_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                s_instance = (T)this;
            }
        }

        protected virtual void OnDestroy()
        {
            if (s_instance == this)
            {
                s_instance = null;
            }
        }
    }
}