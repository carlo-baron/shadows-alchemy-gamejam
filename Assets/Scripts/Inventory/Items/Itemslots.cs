using UnityEngine;
using UnityEngine.EventSystems;

public class Itemslots : MonoBehaviour, IDropHandler
{
    public ItemScriptable itemScriptable { get; private set; }
    private CraftingManager craftingManager;
    public bool isCraftingSlot;
    public bool isHotBar;

    public void OnDrop(PointerEventData eventData){
        if(transform.childCount == 0){
            ItemsDraggable item = eventData.pointerDrag.GetComponent<ItemsDraggable>();
            if(!isHotBar){
                item.parentAfterDrag = transform;
            }
            if(isCraftingSlot){
                craftingManager = FindObjectOfType<CraftingManager>();
            }
        }
    }

   public void SetCraftingSlot(GameObject child){
        // itemScriptable = child.item;
        craftingManager.SetItems(child);
        craftingManager.SearchRecipe();
    }

    public void RemoveOnCraftingSlot(GameObject child){
        craftingManager.RemoveItem(child);
    }
}