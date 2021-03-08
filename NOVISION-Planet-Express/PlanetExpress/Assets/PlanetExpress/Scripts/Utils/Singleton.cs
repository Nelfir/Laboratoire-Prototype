namespace PlanetExpress.Scripts.Utils
{
using UnityEngine;

namespace Scripts.Utils.Objects
{
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        #region Fields

        /// <summary>
        /// The instance.
        /// </summary>
        private static T instance;

        #endregion

        #region Properties

        private static bool applicationisQuitting = false;

        public void OnApplicationQuit()
        {
            applicationisQuitting = true;
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();

                    if (instance == null)
                    {
                        if (applicationisQuitting)
                        {
                            Debug.LogWarning("Application is quitting, not creating an instance of this Singleton.");
                            return default;
                        }

                        GameObject obj = new GameObject
                        {
                            name = typeof(T).Name
                        };
                        instance = obj.AddComponent<T>();
                    }
                }

                return instance;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Use this for initialization.
        /// </summary>
        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;

                if (transform.parent)
                {
                    Debug.LogWarning("[Singleton] Can't make '" + typeof(T).Name + "' DontDestroyOnLoad, as it is not a root Game Object.");
                }
                else
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion
    }
}
}