using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class CraftingManager : MonoBehaviour
{
    public List<ItemScriptable> items;
    public string[] recipes;
    private string[] recipeData;

    [SerializeField]private Image exchangeSprite;
    private ItemScriptable currentItem;


    void Awake(){
        recipeData = new string[recipes.Length];
        Array.Copy(recipes, recipeData, recipes.Length);
    }

    void Update(){
        if(items.Count < 3){
            Array.Copy(recipeData, recipes, recipeData.Length);
        }
    }

    public void SetItems(ItemScriptable item)
    {
        items.Add(item);
    }

    public void RemoveItem(ItemScriptable item)
    {
        items.Remove(item);
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
                    exchangeSprite.sprite = currentItem.sprite;
                    break;
                }
            }
        }
    }



}
