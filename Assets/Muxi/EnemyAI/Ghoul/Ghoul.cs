namespace EnemyAIBase
{
    public class Ghoul : BaseEnemy
    {
        //自己的独属字段
        protected override void InitializeAI()
        {
            throw new System.NotImplementedException();
        }

        protected override void RegisterGoals()
        {

        }

        public override void TransitionToCombat()
        {
            base.TransitionToCombat();

        }
    }
}