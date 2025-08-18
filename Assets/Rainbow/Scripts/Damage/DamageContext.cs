using System;
using CharacterInfo = ns.Character.CharacterInfo;

public class DamageContext
{
    public CharacterInfo AttackerInfo;
    /// <summary>
    /// 攻击成功委托，在攻击方成功命中受击方后的回调，参数为受击方的角色信息
    /// </summary>
    public Action<CharacterInfo> AttackSucceed;
    public DamageContext(CharacterInfo attackerInfo)
    {
        AttackerInfo = attackerInfo;
    }
}
