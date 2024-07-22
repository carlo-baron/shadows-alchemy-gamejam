using UnityEngine;
using UnityEngine.EventSystems;

public class Itemslots : MonoBehaviour, IDropHandler
{
    public ItemScriptable itemScriptable { get; private set; }
    private CraftingManager craftingManager;
    public bool isCraftingSlot;

    public void OnDrop(PointerEventData eventData){
        if(transform.childCount == 0){
            ItemsDraggable item = eventData.pointerDrag.GetComponent<ItemsDraggable>();
            item.parentAfterDrag = transform;
            if(isCraftingSlot){
                craftingManager = FindObjectOfType<CraftingManager>();
            }
        }
    }

   public void SetCraftingSlot(ItemsDraggable child){
        itemScriptable = child.item;
        craftingManager.SetItems(itemScriptable);
        craftingManager.SearchRecipe();
    }

    public void RemoveOnCraftingSlot(ItemScriptable item){
        craftingManager.RemoveItem(item);
    }
}