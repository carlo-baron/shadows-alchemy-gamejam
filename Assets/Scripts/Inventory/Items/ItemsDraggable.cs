using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemsDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Image image;
    public ItemScriptable item{get; private set;}
    public Transform parentAfterDrag;

    public bool isInitialized {get; private set;}
    void Awake(){
        image = GetComponent<Image>();
    }

    public void InitializeItem(ItemScriptable item){
        if(!isInitialized){
            this.item = item;
            image.sprite = item.sprite;
        }
        isInitialized = true;
    }
    public void OnBeginDrag(PointerEventData eventData){
        print("dragging");
        image.raycastTarget = false;

        if(transform.parent.gameObject.GetComponent<Itemslots>().isCraftingSlot){
            transform.parent.gameObject.GetComponent<Itemslots>().RemoveOnCraftingSlot(item);
        }

        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData){
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData){
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
        if(transform.parent.gameObject.GetComponent<Itemslots>().isCraftingSlot){
            transform.parent.gameObject.GetComponent<Itemslots>().SetCraftingSlot(this);
        }
    }

}
