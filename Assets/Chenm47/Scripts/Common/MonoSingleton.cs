using UnityEngine;

namespace Common
{
    /// <summary>
    /// 描述：单例模式
    /// </summary>
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T instance;
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
                }
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        protected void Awake()
        {
            if (instance == null)
            {
                Instance = this as T;
                Init();
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Init()
        {
            //初始化
        }
    }
}
