using ns.ItemInfos;
using UnityEngine;

namespace ns.Weapons
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class WeaponHolderSlot : MonoBehaviour
    {
        [SerializeField]
        bool isLeftHandSlot;
        [SerializeField]
        bool isRightHandSlot;

        public Transform CreatPosTF;

        private GameObject currentWeapon;

        public bool IsLeftHandSlot { get => isLeftHandSlot; }
        public bool IsRightHandSlot { get => isRightHandSlot; }

        public void LoadWeaponModel(WeaponInfo weaponInfo, bool isLeft)
        {
            //禁用当前weapon并销毁
            UnloadWeaponAndDestory();
            if (weaponInfo == null)
            {
                //禁用当前weapon
                UnloadWeapon();
                return;
            }

            GameObject go = Instantiate(weaponInfo.ModlePrefab, CreatPosTF);
            var model = go.transform.GetChild(0);
            var pivot_l = go.transform.Find("WeaponPivot_L");
            var pivot_r = go.transform.Find("WeaponPivot_R");
            if (isLeft)
            {
                model.SetParent(pivot_l, false);
            }
            else
            {
                model.SetParent(pivot_r, false);
            }
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
            weaponInfo.ModleGO = go;
            currentWeapon = go;
        }

        public void UnloadWeaponAndDestory()
        {
            if (currentWeapon != null)
            {
                UnloadWeapon();
                Destroy(currentWeapon);
            }
        }

        public void UnloadWeapon()
        {
            if (currentWeapon != null)
                currentWeapon.SetActive(false);
        }

    }
}
