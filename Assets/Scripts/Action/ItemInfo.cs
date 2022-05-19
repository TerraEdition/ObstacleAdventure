using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    private GameManager gameManager;

    private ShopManager shopManager;

    [SerializeField]
    private GameObject itemShopPrefab;

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
            int _i = i;
            GameObject canvasItem =
                (GameObject)
                Instantiate(itemShopPrefab,
                new Vector3(0, 0, 0),
                Quaternion.identity);
            canvasItem.transform.GetChild(0).GetComponent<Image>().sprite =
                shopManager.item[i].spriteItem;
            canvasItem.transform.GetChild(1).GetComponent<Text>().text =
                shopManager.item[i].price.ToString() +
                " Coin (" +
                shopManager.item[i].name.Split(" ")[1] +
                ")";

            // component Status
            ItemStatus itemStatus = canvasItem.AddComponent<ItemStatus>();

            // menambah script itemstatus untuk interaksi agar dikenalin tombol beli
            itemStatus.name = shopManager.item[i].name;
            itemStatus.value = shopManager.item[i].value;
            itemStatus.price = shopManager.item[i].price;
            itemStatus.type = shopManager.item[i].type;
            canvasItem.transform.SetParent(gameObject.transform);
            canvasItem.transform.localScale = new Vector3(1, 1, 0);
        }
    }
}
