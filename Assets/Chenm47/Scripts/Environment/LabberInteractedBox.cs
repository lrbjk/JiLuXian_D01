using ns.Character.Player;
using UnityEngine;


namespace ns.Enviroment
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class LabberInteractedBox : MonoBehaviour
    {
        [SerializeField]
        private bool IsUpBox;
        private static PlayerInfo info;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (info == null)
                    info = other.GetComponent<PlayerInfo>();
                info.ClimbPosTF = transform.Find("ClimbPosition");
                if (IsUpBox)
                {
                    info.IsInUpClimbBox = true;
                    info.IsInDownClimbBox = false;
                }
                else
                {
                    info.IsInUpClimbBox = false;
                    info.IsInDownClimbBox = true;
                }
                Debug.Log("Player has interacted with the box.");
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (info == null)
                    info = other.GetComponent<PlayerInfo>();
                info.IsInUpClimbBox = false;
                info.IsInDownClimbBox = false;
            }
        }
    }
}
