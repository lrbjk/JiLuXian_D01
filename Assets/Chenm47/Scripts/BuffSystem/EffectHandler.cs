using UnityEngine;
using CharacterInfo = ns.Character.CharacterInfo;

/*

*/
namespace ns.BuffSystem
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class EffectHandler : MonoBehaviour
    {
        private CharacterInfo info;
        private StatusEffectManager statusManager;

        private void Awake()
        {
            info = GetComponent<CharacterInfo>();
            statusManager = GetComponent<StatusEffectManager>();
        }

        public void ApplyHeal(int amount)
        {
            info.ChangeHP(amount);
        }
        /// <summary>
        /// 应用各种持续效果
        /// </summary>
        /// <param name="effect"></param>
        public void ApplyStatus(StatusEffect effect)
        {
            statusManager.AddEffect(effect);
        }

        //public void CastProjectile(GameObject projectilePrefab, Transform spawnPoint, float speed)
        //{
        //    var proj = GameObject.Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
        //    proj.GetComponent<Rigidbody>().velocity = spawnPoint.forward * speed;
        //}
    }
}
