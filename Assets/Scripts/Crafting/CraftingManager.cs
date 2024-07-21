using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class CraftingManager : MonoBehaviour
{
    public List<ItemScriptable> items;
    public string[] recipes;

    public void SetItems(ItemScriptable item){
        items.Add(item);
    }

    public void RemoveItem(ItemScriptable item){
        items.Remove(item);
    }

    //check if items make a certain recipe

    // public void Update(){
    //     if(items.Count > 0){
    //         string[] recipeData = recipes;
    //         for(int i = 0; i < recipes.Length; i++){
    //             foreach(ItemScriptable item in items){
    //                 item.name = item.name.ToLower();
    //                 if(recipes[i].Contains(item.name)){
    //                     recipes[i].Replace(item.name, "");
    //                     Debug.Log(recipes[i]);
    //                 }
    //             }
    //             if(recipes[i] == null){
    //                 Debug.Log("Craft");
    //             }
    //         }
    //     }
    // }
}
