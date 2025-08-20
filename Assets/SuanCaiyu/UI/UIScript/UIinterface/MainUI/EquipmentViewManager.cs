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

    private EquipmentBagUIList equipmentBagUIList;

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

  
    public void UpdatRightHandView()
    {
        //更新主界面装备显示和item列表
        RightHnadCycler.equipmentImages.Clear();
        equipmentSelector.rightHandItems.Clear();
        for (int i = 0; i < 2; i++)
        {
            if (equipmentSelector.rightHandWeaponList[i].isEquiped)
            {
                RightHnadCycler.equipmentImages.Add(equipmentSelector.rightHandWeaponList[i].equipImage.sprite);
                equipmentSelector.rightHandItems.Add(equipmentSelector.rightHandWeaponList[i].EquipItem);
            }
        }

        if(RightHnadCycler.equipmentImages.Count<2)
        {
            RightHnadCycler.currentIndex = 0;
        }

         if(RightHnadCycler.equipmentImages.Count <= 0)
         {
            return;
         }
        else
        {
            RightHnadCycler.UpdateImagerDisplay();
        }

    }

    public void UpdatLeftHandView()
    {

        LeftHnadCycler.equipmentImages.Clear();
        equipmentSelector.leftHandItems.Clear();

        for (int i = 0; i < 2; i++)
        {
            if (equipmentSelector.leftHandWeaponList[i].isEquiped)
            {
                LeftHnadCycler.equipmentImages.Add(equipmentSelector.leftHandWeaponList[i].equipImage.sprite);
                equipmentSelector.leftHandItems.Add(equipmentSelector.leftHandWeaponList[i].EquipItem);
            }
        }
        if (LeftHnadCycler.equipmentImages.Count < 2)
        {
            LeftHnadCycler.currentIndex = 0;
        }
        if (LeftHnadCycler.equipmentImages.Count <= 0)
        {
            return;
        }
        else
        {
            LeftHnadCycler.UpdateImagerDisplay();
         }
        
    }

    public void UpdateConsumerView()
    {
        PropCycler.equipmentImages.Clear();
        equipmentSelector.consumEquipItems.Clear();

        for (int i = 0; i < 8; i++)
        {
            if (equipmentSelector.consumerEquipmentList[i].isEquiped)
            {
                PropCycler.equipmentImages.Add(equipmentSelector.consumerEquipmentList[i].equipImage.sprite);
                equipmentSelector.consumEquipItems.Add(equipmentSelector.consumerEquipmentList[i].EquipItem);
            }
        }
        //�±�Խ��
        if(PropCycler.currentIndex >= PropCycler.equipmentImages.Count)
        {
            PropCycler.currentIndex = 0;
        }
        PropCycler.UpdateImagerDisplay();
    }
}
