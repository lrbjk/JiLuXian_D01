using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public interface IDamage
{
   /// <summary>
   /// ¹¥»÷·½·¨
   /// </summary>
   /// <param name="damageContext"></param>
    public void TakeDamage(DamageContext damageContext);
}