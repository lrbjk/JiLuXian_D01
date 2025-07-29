namespace ns.Character
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class EnemyInfo : CharacterInfo
    {
        public override float GetBaseMovtionPoise()
        {
            //throw new System.NotImplementedException();
            return 10;
        }

        public override float GetBaseReducedPoise()
        {
            throw new System.NotImplementedException();
        }

        public override int GetDEF()
        {
            //throw new System.NotImplementedException();//具体实现
            return 10;
        }

        public override int GetResistance(ResistanceType resistanceType)
        {
            //throw new System.NotImplementedException();
            return 10;
        }

        public override float GetWeaponExecutionCoefficient()
        {
            return 0;//现阶段敌人无法处决
        }

        public override float GetWeaponPhysicalATK()
        {
            throw new System.NotImplementedException();//直接使用动作配置获得的攻击数值
        }
    }
}
