using System;
using System.Collections.Generic;
using UnityEngine;
using static BagList;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance;
    
    private SelectableImage currentlySelected;
    
    [SerializeField] private List<SelectableImage> selectedObjects = new List<SelectableImage>();
    [SerializeField] private int currentSelectionIndex = 0;

    public InfiniteScroll InfiniteScroll;

    //与背包连接接口
    [SerializeField] private BagList bagList;

    //冷却时间
    public float minInterval = 0.5f;
    private float lastCallTime;
    // 在SelectionManager中
    public static event Action<ItemCategory> OnCategoryChanged;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        currentSelectionIndex = 0;
    }
    
    void Start()
    {
        if (InfiniteScroll.endInitialization)
        //将所有已有的和新生成的物体加入列表之中
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                selectedObjects.Add(child.GetComponent<SelectableImage>());
            }
        }
    }
    
    public void SelectImage(SelectableImage newSelection)
    {

        if (currentlySelected == newSelection)
        {
            currentlySelected.SetSelected(false);
            currentlySelected = null;
            return;
        }
        
        if (currentlySelected != null)
        {
            currentlySelected.SetSelected(false);
        }
        
        currentlySelected = newSelection;
        currentlySelected.SetSelected(true);
        currentSelectionIndex = selectedObjects.IndexOf(currentlySelected);

        //切换时增加背包物品分类判断
        bagList.currentCategory = currentSelectionIndex switch
        {
            6 or 16 => ItemCategory.Consumable,
            7 or 17 => ItemCategory.Material,
            8 or 18 => ItemCategory.Currency,
            9 or 19 => ItemCategory.HeadEquipment,
            10 or 0 or 20 => ItemCategory.BodyEquipment,
            11 or 21 or 1 => ItemCategory.KernelEquipment,
            12 or 2 => ItemCategory.Spell,
            13 or 3 => ItemCategory.Key,
            14 or 4 => ItemCategory.RightHandWeapon,
            15 or 5 => ItemCategory.LeftHandWeapon,
            _ => ItemCategory.None
        };

        bagList.InitializeSlotPrefabReferences();
    }

     public void moveRightOnEdge()
     {
        if (Time.time - lastCallTime < minInterval)
        {
            Debug.Log("方法调用太频繁，被限制");
            return;
        }

        if (currentSelectionIndex >= 10 && (InfiniteScroll.content_rtf.localPosition.x <= -2965))
        {
            currentSelectionIndex -= 9;
        }
        else
        {
            currentSelectionIndex += 1;
        }

        if (currentSelectionIndex > 0)
        {
            SelectionManager.Instance.SelectImage(selectedObjects[currentSelectionIndex]);
        }

        lastCallTime = Time.time;
    }

    public void moveLeftOnEdge()
    {
        if (Time.time - lastCallTime < minInterval)
        {
            Debug.Log("方法调用太频繁，被限制");
            return;
        }

        if (currentSelectionIndex <= 7 && (InfiniteScroll.content_rtf.localPosition.x >= -265))
        {
            currentSelectionIndex += 9;
        }
        else
        {
            currentSelectionIndex -= 1;
        }

        if (currentSelectionIndex > 0)
        {
            SelectionManager.Instance.SelectImage(selectedObjects[currentSelectionIndex]);
        }

        lastCallTime = Time.time;
    }

}