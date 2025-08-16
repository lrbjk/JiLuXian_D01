using ns.Value;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

*/
namespace ns.Character.Player
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class PlayerBullet : MonoBehaviour
    {
        private float speed;
        private CharacterInfo attackerInfo;
        public void Init(Vector3 dir, float speed, CharacterInfo attackerInfo)
        {
            transform.rotation = Quaternion.LookRotation(dir);
            this.speed = speed;
            this.attackerInfo = attackerInfo;
            this.gameObject.SetActive(true);
        }

        private void Update()
        {
            //以一定速度向前方飞去....
            transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        }

        private void OnTriggerEnter(Collider other)
        {
            //判断敌人造成伤害、或进入虚弱状态(暂未实现)
            if (other.CompareTag("Enemy"))
            {
                //造成伤害
                var enemyInfo = other.gameObject.GetComponent<CharacterInfo>();
                float damage = DamageCalculator.CalculateDamage(attackerInfo, enemyInfo);
                IDamage d = other.gameObject.GetComponent<IDamage>();
                DamageContext damageContext = new DamageContext(attackerInfo);
                d.TakeDamage(damageContext);
                //销毁
                Destroy(this.gameObject);
            }
            else if (other.gameObject.layer == LayerMask.GetMask("Default"))
            {
                //销毁
                Destroy(this.gameObject);
            }//暂时这样
        }

    }
}
