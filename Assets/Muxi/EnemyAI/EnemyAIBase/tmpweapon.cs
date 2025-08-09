using ns.Character.Player;
using UnityEngine;
using CharacterInfo = ns.Character.CharacterInfo;

namespace ns.ItemInfos.Weapons
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class CollderHandle : MonoBehaviour
    {
        private CharacterInfo ownerInfo;
        public PlayerAction playerAction;
        private void Start()
        {
            ownerInfo = GetComponentInParent<CharacterInfo>(true);
        }

        private void OnTriggerEnter(Collider other)
        {

            Debug.Log("ASDASD");
            if (other.CompareTag("Enemy"))
            {
                //处理与敌人的碰撞逻辑
                Debug.Log("Weapon collided with enemy: " + other.name);
                //调用敌人受击接口
                IDamage d = other.GetComponent<IDamage>();
                d.TakeDamage(new DamageContext(ownerInfo));
            }
            else if (other.CompareTag("Player"))
            {
                //玩家受击
                print("Weapon collided with player:" + other.name);
                playerAction.Damaged(10, 9, 10);
                //调用玩家受击接口
                IDamage d = other.GetComponent<IDamage>();
                d.TakeDamage(new DamageContext(ownerInfo));
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