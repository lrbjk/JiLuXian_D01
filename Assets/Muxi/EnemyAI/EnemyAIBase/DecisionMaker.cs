using System.Collections.Generic;
using UnityEngine;
//
namespace EnemyAIBase
{
    [System.Serializable]
    public class ActionWeight
    {
        public string actionName;
        public float baseWeight;
        public float currentWeight;
        public System.Func<BaseEnemy, float> weightModifier;
    }

    public class DecisionMaker
    {
        private Dictionary<string, ActionWeight> actions = new Dictionary<string, ActionWeight>();
        private BaseEnemy owner;


        public DecisionMaker(BaseEnemy owner)
        {
            this.owner = owner;
        }

        public string MakeDecision()
        {
            float totalWeight = 0;

            foreach (var action in actions.Values)
            {
                action.currentWeight = action.baseWeight;

                if (action.weightModifier != null)
                {
                    action.currentWeight *= action.weightModifier(owner);
                }

                totalWeight += action.currentWeight;
            }

            float random = Random.Range(0, totalWeight);
            float current = 0;

            foreach (var action in actions.Values)
            {
                current += action.currentWeight;
                if (random <= current)
                {
                    return action.actionName;
                }
            }

            return null;
        }

        //辅助方法
        public void RegisterAction(string name, float baseWeight, System.Func<BaseEnemy, float> modifier = null)
        {
            actions[name] = new ActionWeight
            {
                actionName = name,
                baseWeight = baseWeight,
                weightModifier = modifier
            };
        }

        public void UnregisterAction(string name)
        {
            actions.Remove(name);
        }

        public void ClearAllActions()
        {
            actions.Clear();
        }

        public bool HasAction(string name)
        {
            return actions.ContainsKey(name);
        }

        public void PrintAllActions()
        {
            Debug.Log($"DecisionMaker for {owner.name} has {actions.Count} actions:");
            foreach (var action in actions.Values)
            {
                Debug.Log($"- {action.actionName}: base={action.baseWeight}, current={action.currentWeight}");
            }
        }

    }
}