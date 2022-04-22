using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemStatus : MonoBehaviour
{
    public int value;

    public int price;

    public string type;

    public string name;

    Button btn;

    private ShopManager shopManager;

    private AudioManager audioManager;

    private void Awake()
    {
        shopManager = ShopManager.instance;
        audioManager = AudioManager.instance;
    }

    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener (Clicked);
    }

    public void Clicked()
    {
        audioManager.PlaySound("Click");
        GameObject ui = GameObject.FindWithTag("UISHop");
        ui.GetComponent<UIShop>().StoreItem(gameObject);
    }
}
