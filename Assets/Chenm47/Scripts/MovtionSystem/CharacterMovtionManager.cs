using System.Collections.Generic;
using UnityEngine;

namespace ns.Movtion
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class CharacterMovtionManager : MonoBehaviour
    {
        public MovtionInfo[] Movtions;

        public IEnumerable<MovtionInfo> GetAllMovtionInfos()
        {
            foreach (var movtion in Movtions)
            {
                yield return movtion;
            }
        }

        public MovtionInfo GetMovtionInfo(int movtionID)
        {
            foreach (var movtion in Movtions)
            {
                if (movtion.MovtionID == movtionID)
                {
                    return movtion;
                }
            }
            Debug.LogWarning($"Movtion with ID {movtionID} not found.");
            return null;
        }
    }
}
