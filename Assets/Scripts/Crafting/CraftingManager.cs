using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

[System.Serializable]
public class CraftingManager : MonoBehaviour
{
    [SerializeField] ItemScriptable[] abilityItems = new ItemScriptable[4];
    public List<ItemScriptable> items;
    public List<GameObject> itemsObject;
    public string[] recipes;
    private string[] recipeData;

    [SerializeField]private Transform exchangeSlot;
    [SerializeField]private GameObject itemTemplate;
    private ItemScriptable currentItem;


    void Awake(){
        recipeData = new string[recipes.Length];
        Array.Copy(recipes, recipeData, recipes.Length);
    }

    void Update(){
        if(items.Count < 3){
            Array.Copy(recipeData, recipes, recipeData.Length);
        }

        if(exchangeSlot.childCount > 0 && items.Count < 3){
            Destroy(exchangeSlot.GetChild(0).gameObject);
        }
    }

    public void SetItems(GameObject item)
    {
        itemsObject.Add(item);
        items.Add(item.GetComponent<ItemsDraggable>().item);
    }

    public void RemoveItem(GameObject item)
    {
        itemsObject.Remove(item);
        items.Remove(item.GetComponent<ItemsDraggable>().item);
    }

    public void SearchRecipe(){
        if(items.Count == 3){
            for(int i = 0; i < recipes.Length; i++){
                Array.Copy(recipeData, recipes, recipeData.Length);
                
                foreach(ItemScriptable item in items){
                    string itemName = item.name.ToLower();
                    currentItem = item;
                    int indexOfItem = recipes[i].IndexOf(itemName);

                    if(indexOfItem != -1){
                        recipes[i] = recipes[i].Remove(indexOfItem, itemName.Length);
                    }else{
                        break;
                    }
                }

                if(recipes[i].Length <= 0){
                    if(exchangeSlot.childCount != 0){
                        Destroy(exchangeSlot.GetChild(0).gameObject);
                    }
                    ShowExchange(abilityItems[i]);
                    Array.Copy(recipeData, recipes, recipeData.Length);
                    break;
                }
            }
        }
    }

    void ShowExchange(ItemScriptable abilityData){
        GameObject possibleExchange = Instantiate(itemTemplate, exchangeSlot.position, Quaternion.identity);
        possibleExchange.GetComponent<ItemsDraggable>().InitializeItem(abilityData);
        possibleExchange.transform.SetParent(exchangeSlot);
    }

    public void DestoryOffers(){
        foreach(GameObject obj in itemsObject){
            Destroy(obj);
        }
        itemsObject.Clear();
        items.Clear();
    }

}
