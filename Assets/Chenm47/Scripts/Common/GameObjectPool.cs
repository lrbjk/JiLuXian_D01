using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    /*
     所有频繁创建、销毁的游戏对象应该使用对象池
    1.创建：public GameObject CreateGameObject(string key, GameObject prefab, Vector3 pos, Quaternion rot)
        回收：public void CollectObjectDelay(GameObject go, float delay = 0)
    2.需要通过对象池创建的物体，如需每次创建时执行某些逻辑，请继承IResetable

     */
    /// <summary>
    /// 可重置
    /// </summary>
    public interface IResetable
    {
        void OnReset();
    }

    /// <summary>
    /// 描述：对象池
    /// </summary>
    public class GameObjectPool : MonoSingleton<GameObjectPool>
    {
        private Dictionary<string, List<GameObject>> cache;
        protected override void Init()
        {
            base.Init();
            cache = new Dictionary<string, List<GameObject>>();
        }

        /// <summary>
        /// 从对象池创建一个对象
        /// </summary>
        /// <param name="key">物体类别</param>
        /// <param name="prefab">预制体</param>
        /// <param name="pos"></param>
        /// <param name="rot"></param>
        /// <returns></returns>
        public GameObject CreateGameObject(string key, GameObject prefab, Vector3 pos, Quaternion rot)
        {
            GameObject go = FindUsableGameObject(key);

            if (go == null)
            {
                go = AddGameObject(key, prefab, pos, rot);
            }

            UseGameObject(pos, rot, go);

            return go;
        }

        private static void UseGameObject(Vector3 pos, Quaternion rot, GameObject go)
        {
            go.transform.position = pos;
            go.transform.rotation = rot;
            go.SetActive(true);
            foreach (var item in go.GetComponents<IResetable>())
            {
                item.OnReset();
            }
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="go">对象</param>
        /// <param name="delay">延迟时间，默认为0</param>
        public void CollectObjectDelay(GameObject go, float delay = 0)
        {
            StartCoroutine(CollectObject(go, delay));
        }

        private IEnumerator CollectObject(GameObject go, float delay)
        {
            yield return new WaitForSeconds(delay);
            go.SetActive(false);
        }

        private GameObject AddGameObject(string key, GameObject prefab, Vector3 pos, Quaternion rot)
        {
            GameObject go = Instantiate(prefab, pos, rot);
            if (!cache.ContainsKey(key))
                cache.Add(key, new List<GameObject>());
            cache[key].Add(go);
            return go;
        }

        private GameObject FindUsableGameObject(string key)
        {
            if (cache.ContainsKey(key))
                return cache[key].Find(g => !g.activeInHierarchy);
            return null;
        }

        /// <summary>
        /// 清空某类游戏对象缓存
        /// </summary>
        /// <param name="key"></param>
        public void Clear(string key)
        {
            if (cache.ContainsKey(key))
            {
                foreach (GameObject go in cache[key])
                {
                    Destroy(go);
                }
                cache.Remove(key);
            }
        }
        /// <summary>
        /// 清空全部物体
        /// </summary>
        public void ClearAll()
        {
            foreach (string key in new List<string>(cache.Keys))
            {
                Clear(key);
            }
            cache.Clear();
        }

    }
}
