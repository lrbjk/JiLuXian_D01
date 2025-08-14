using Common.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentViewManager : MonoBehaviour
{
    [SerializeField] private ImageCycler RightHnadCycler;
    [SerializeField] private ImageCycler LeftHnadCycler;
    [SerializeField] private ImageCycler SpellCycler;
    [SerializeField] private ImageCycler PropCycler;

    [SerializeField] private EquipmentSelector equipmentSelector;
    private  EquipmentUIFunc equipmentUIFunc;

    //左右手武器切换时对应的装备界面显示也要切换

    [SerializeField] private Image RightHandimage;
    [SerializeField] private Image LeftHandimage;


    private void Start()
    {
        equipmentUIFunc = UIManager.Instance.GetUILayerManager("EquipmentUI") as EquipmentUIFunc;
        equipmentSelector = equipmentUIFunc.equipmentSelector;
        RightHnadCycler.equipmentImages.Clear();
        LeftHnadCycler.equipmentImages.Clear();
        SpellCycler.equipmentImages.Clear();
        PropCycler.equipmentImages.Clear();
    }

    /// <summary>
    /// 更新右手装备栏UI显示
    /// </summary>
    public void UpdatRightHandView()
    {
        RightHnadCycler.equipmentImages.Clear();
        //右手武器
        for (int i = 0; i < 2; i++)
        {
            if (equipmentSelector.rightHandWeaponList[i].isEquiped)
            {
                RightHnadCycler.equipmentImages.Add(equipmentSelector.rightHandWeaponList[i].equipImage.sprite);
                
            }
        }
        //装备槽全部清空时防止数组越界
        if(RightHnadCycler.equipmentImages.Count<2)
        {
            RightHnadCycler.currentIndex = 0;
        }
        RightHnadCycler.UpdateImagerDisplay();

    }

    public void UpdatLeftHandView()
    {
        RightHnadCycler.equipmentImages.Clear();
        //左手武器
        for (int i = 0; i < 2; i++)
        {
            if (equipmentSelector.leftHandWeaponList[i].isEquiped)
            {
                LeftHnadCycler.equipmentImages.Add(equipmentSelector.leftHandWeaponList[i].equipImage.sprite);
               
            }
        }
        if (RightHnadCycler.equipmentImages.Count < 2)
        {
            RightHnadCycler.currentIndex = 0;
        }
        LeftHnadCycler.UpdateImagerDisplay();
    }
}
