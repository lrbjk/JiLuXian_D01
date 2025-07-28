using AI.FSM.Framework;
using ns.Item.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

*/
namespace ns.Character
{
    /// <summary>
    /// 描述：
    /// </summary>
    public interface IGetCurrentAtkWeapon
    {
        public Weapon GetCurrentAtkWeapon();
    }
}
