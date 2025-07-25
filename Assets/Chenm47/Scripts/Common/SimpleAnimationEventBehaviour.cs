using ns.Character.Player;
using UnityEngine;


namespace Common
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class SimpleAnimationEventBehaviour : MonoBehaviour
    {
        PlayerInfo playerInfo;

        private void Start()
        {
            playerInfo = GetComponentInParent<PlayerInfo>(true);
        }

        private void OnJumpTop()
        {
            Debug.Log("到达最高");
            //标记下落
            playerInfo.IsOnTop = true;
        }
    }
}
