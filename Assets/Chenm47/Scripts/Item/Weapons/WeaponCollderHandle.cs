using UnityEngine;

namespace ns.Item.Weapons
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class WeaponCollderHandle : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                //处理与敌人的碰撞逻辑
                Debug.Log("Weapon collided with enemy: " + other.name);
                //敌人受伤
                print("Enemy hit: " + other.name);
            }
            else if (other.CompareTag("Player"))
            {
                //玩家受伤
                print("Player hit: " + other.name);
            }
        }

        public void SetCollider(bool enable)
        {
            //启用或禁用碰撞体
            GetComponent<Collider>().enabled = enable;
            Debug.Log("Collider " + (enable ? "enabled" : "disabled"));
        }

    }
}
