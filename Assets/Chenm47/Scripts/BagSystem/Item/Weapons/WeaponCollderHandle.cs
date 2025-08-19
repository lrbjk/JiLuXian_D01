using System;
using UnityEngine;
using CharacterInfo = ns.Character.CharacterInfo;

namespace ns.Weapons
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class WeaponCollderHandle : MonoBehaviour
    {
        private CharacterInfo attackerInfo;

        private void Start()
        {
            attackerInfo = GetComponentInParent<CharacterInfo>(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                //处理与敌人的碰撞逻辑
                Debug.Log("Weapon collided with enemy: " + other.name);
                //调用敌人受击接口
                IDamage d = other.GetComponent<IDamage>();
                var content = new DamageContext(attackerInfo);
                content.AttackSucceed = new Action<CharacterInfo>(AddPlayerTransitionValue);
                d.TakeDamage(content);
            }
            else if (other.CompareTag("Player"))
            {
                //玩家受击
                print("Weapon collided with player:" + other.name);
                //调用玩家受击接口
                IDamage d = other.GetComponent<IDamage>();
                d.TakeDamage(new DamageContext(attackerInfo));
            }
        }

        private void AddPlayerTransitionValue(CharacterInfo info)
        {
            //直接增加0.05f，从简计算，目前只有玩家有转换值加成
            attackerInfo.UpdateTransitionValue(Mathf.CeilToInt(attackerInfo.GetTransitionCeil() * 0.05f));
        }

        public void SetCollider(bool enable)
        {
            //启用或禁用碰撞体
            GetComponent<Collider>().enabled = enable;
            Debug.Log("Collider " + (enable ? "enabled" : "disabled"));
        }

    }
}
