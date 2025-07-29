using UnityEngine;


    /// <summary>
    /// 描述：单例模式
    /// </summary>
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T instance;
        private bool isBeforeAwake = false;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>(true);
                    //如果没找到
                    if (instance == null)
                    {
                        //创建一个游戏对象挂载这个脚本
                        instance = new GameObject("Singleton of" + typeof(T)).AddComponent<T>();
                    }
                    else
                    {
                        instance.Init();
                    }
                    instance.isBeforeAwake = true;
                }
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        protected virtual void Awake()
        {
            if (instance != null && (!isBeforeAwake))
            {
                Destroy(gameObject); //如果已经存在实例，则销毁当前对象
                return;
            }
            else
            {
                if (instance == null)
                {
                    Instance = this as T;
                    Init();
                }
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Init()
        {
            //初始化
            //DontDestroyOnLoad(gameObject);
        }
    }

