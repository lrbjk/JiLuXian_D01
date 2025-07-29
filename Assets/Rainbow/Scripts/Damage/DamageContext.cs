using CharacterInfo = ns.Character.CharacterInfo;

public class DamageContext
{
    public CharacterInfo AttackerInfo;

    public DamageContext(CharacterInfo attackerInfo)
    {
        AttackerInfo = attackerInfo;
    }
}
