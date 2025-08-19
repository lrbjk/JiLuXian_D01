using Common.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalDescription : MonoBehaviour
{
    public Text NumberText_1;
    public Text NumberText_2;
    public Text NumberText_3;
    public Text NumberText_4;
    public Text NormalText_1;
    public Text NormalText_2;
    public Text NormalText_3;
    public Text NormalText_4;

    public Text NameText;
    public Text DescriptionText;
    public Text EffectText;
    public Text EffectTextTag;

    //4个属性值加成
    public Text EquipValue_1;
    public Text EquipValue_2;
    public Text EquipValue_3;
    public Text EquipValue_4;

    public Image DescriptionImage;
    public Sprite EmptyImage;

    [SerializeField] private EquipmentUIFunc equipmentUIFunc;
    [SerializeField] private BagList bagList;

    /// <summary>
    /// 更新描述界面的格式
    /// </summary>
    public void UpdateNormalDescription()
    {
        equipmentUIFunc = UIManager.Instance.GetUILayerManager("EquipmentUI") as EquipmentUIFunc;
        bagList = equipmentUIFunc.bagList;

        if(bagList.currentCategory == BagList.ItemCategory.None|| bagList.currentCategory == BagList.ItemCategory.Consumable ||
         bagList.currentCategory == BagList.ItemCategory.Currency || bagList.currentCategory == BagList.ItemCategory.Key || bagList.currentCategory == BagList.ItemCategory.Material)
        {
            NameText.text = "/-/";
            NormalText_1.text = "最大持有数";
            NormalText_2.text = "最大收纳数";
            NormalText_3.text = "物品类型";
            NormalText_4.text = null;

            NumberText_1.text = "-";
            NumberText_2.text = "-";
            NumberText_3.text = "-";
            NumberText_4.text = null;
            DescriptionText.text = "-";
            EffectTextTag.text = "效果";
            EffectText.text = "-";

            EquipValue_1.text = null;
            EquipValue_2.text = null;
            EquipValue_3.text = null;
            EquipValue_4.text = null;

            DescriptionImage.sprite = EmptyImage;
        }
        else if(bagList.currentCategory == BagList.ItemCategory.RightHandWeapon || bagList.currentCategory == BagList.ItemCategory.LeftHandWeapon)
        {
            NameText.text = "/-/";
            NormalText_1.text = "武器类型";
            NormalText_2.text = "攻击类型";
            NormalText_3.text = "战技";
            NormalText_4.text = "消耗";

            NumberText_1.text = "-";
            NumberText_2.text = "-";
            NumberText_3.text = "-";
            NumberText_4.text = "-";
            DescriptionText.text = "-";
            EffectTextTag.text = "攻击力";
            EffectText.text = "     物理       共振       电磁       热能";
            EquipValue_1.text = "-";
            EquipValue_2.text = "-";
            EquipValue_3.text = "-";
            EquipValue_4.text = "-";

            DescriptionImage.sprite = EmptyImage;
        }
    }
}
