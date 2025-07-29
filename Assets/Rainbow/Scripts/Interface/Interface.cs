using ns.Value;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public interface IDamage
{
   /// <summary>
   /// 受击方法
   /// </summary>
   /// <param name="damageContext"></param>
    public void TakeDamage(DamageContext damageContext)
    {
        //是否无敌
        //计算伤害
        //血量扣除
        //是否死亡
        //计算韧性
        //基础韧性扣除
        //是否虚弱
        //是否处于霸体帧
        //累积动作韧性
        //是否打断动作
        //受击动画
    }
}