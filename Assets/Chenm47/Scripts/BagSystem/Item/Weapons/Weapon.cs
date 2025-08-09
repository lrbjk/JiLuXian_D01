using ns.Character;
using ns.ItemInfos;
using ns.Value;

namespace ns.Weapons
{
    /// <summary>
    /// 描述：
    /// </summary>
    public abstract class Weapon
    {
        protected static WeaponLVHandler lVHandler;//处理武器等级加成
        protected static WeaponCharacterPropertyHandler WPCPHandler;//处理角色属性加成
        public WeaponInfo WInfo;

        public abstract float GetFinalPhysicalATK();

        public abstract float GetSpecialATK(ResistanceType resistanceType);

        public abstract ResistanceType[] GetAllSpecialResistanceTypes();

    }
}
