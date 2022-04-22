using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    private GameManager gameManager;
    private ShopManager shopManager;

    private void Awake()
    {
        gameManager = GameManager.instance;
        shopManager = ShopManager.instance;
    }
    private void Start()
    {
        UpdateListItems();
    }
    private void UpdateListItems()
    {
        for (int i = 0; i < shopManager.item.Length; i++)
        {
            //canvas 
            GameObject canvasItem = new GameObject();
            canvasItem.name = "canvasItem_" + i;
            Image canvasImage = canvasItem.AddComponent<Image>();
            canvasItem.AddComponent<Button>();
            canvasImage.color = new Color32(255, 255, 225, 0);
            canvasItem.GetComponent<RectTransform>().SetParent(gameObject.transform);
            canvasItem.transform.localScale = new Vector3(1f, 1f, 0f);
            // sprite
            GameObject item = new GameObject();
            Image NewImage = item.AddComponent<Image>();
            NewImage.sprite = shopManager.item[i].spriteItem;
            item.GetComponent<RectTransform>().SetParent(canvasItem.transform);
            item.GetComponent<RectTransform>().sizeDelta = new Vector2(50f, 50f);
            item.transform.localScale = new Vector3(1f, 1f, 0f);

            // text
            GameObject textItem = new GameObject();
            Text NewText = textItem.AddComponent<Text>();
            NewText.text = shopManager.item[i].name + "\n" + shopManager.item[i].price + " Coin";
            NewText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            NewText.fontSize = 20;
            NewText.alignment = TextAnchor.MiddleCenter;
            textItem.GetComponent<RectTransform>().sizeDelta = new Vector2(160f, 50f);
            textItem.GetComponent<RectTransform>().SetParent(item.transform);
            textItem.transform.localScale = new Vector3(1f, 1f, 0f);
            textItem.transform.localPosition = new Vector3(0f, -55f, 0f);

            // component Status
            ItemStatus itemStatus = canvasItem.AddComponent<ItemStatus>();
            // menambah script itemstatus untuk interaksi agar dikenalin tombol beli
            itemStatus.name = shopManager.item[i].name;
            itemStatus.value = shopManager.item[i].value;
            itemStatus.price = shopManager.item[i].price;
            itemStatus.type = shopManager.item[i].type;

            item.SetActive(true);
        }
    }
}
