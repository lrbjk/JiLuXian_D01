using Common.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentViewManager : MonoBehaviour
{
    public ImageCycler RightHnadCycler;
    public ImageCycler LeftHnadCycler;
    public ImageCycler SpellCycler;
    public ImageCycler PropCycler;

    [SerializeField] private EquipmentSelector equipmentSelector;
    private  EquipmentUIFunc equipmentUIFunc;

    //左右手武器切换时对应的装备界面显示也要切换

    [SerializeField] private Image RightHandimage;
    [SerializeField] private Image LeftHandimage;
    [SerializeField] private Image Spellimage;
    [SerializeField] private Image Propimage;


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
        //只有一把武器时索引固定为0
        if(RightHnadCycler.equipmentImages.Count<2)
        {
            RightHnadCycler.currentIndex = 0;
        }

         if(RightHnadCycler.equipmentImages.Count <= 0)
         {
            Debug.Log("装备栏没有武器！");
            return;
         }
        else
        {
            RightHnadCycler.UpdateImagerDisplay();
        }

    }

    public void UpdatLeftHandView()
    {
        //Debug.Log("我是你爸爸");
        LeftHnadCycler.equipmentImages.Clear();
        //左手武器
        for (int i = 0; i < 2; i++)
        {
            if (equipmentSelector.leftHandWeaponList[i].isEquiped)
            {
                LeftHnadCycler.equipmentImages.Add(equipmentSelector.leftHandWeaponList[i].equipImage.sprite);              
            }
        }
        if (LeftHnadCycler.equipmentImages.Count < 2)
        {
            LeftHnadCycler.currentIndex = 0;
        }
        LeftHnadCycler.UpdateImagerDisplay();
    }

    public void UpdateConsumerView()
    {
        PropCycler.equipmentImages.Clear();

        for(int i = 0; i <8; i++)
        {
            if (equipmentSelector.consumerEquipmentList[i].isEquiped)
            {
                PropCycler.equipmentImages.Add(equipmentSelector.consumerEquipmentList[i].equipImage.sprite);
            }
        }
        //下标越界
        if(PropCycler.currentIndex >= PropCycler.equipmentImages.Count)
        {
            PropCycler.currentIndex = 0;
        }
        PropCycler.UpdateImagerDisplay();
    }
}
