using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPartLevel : MonoBehaviour
{
    private int level;

    private int partLevel;

    private GameManager gameManager;

    private AudioManager audioManager;

    private void Awake()
    {
    }

    void Start()
    {
        audioManager = AudioManager.instance;
        gameManager = GameManager.instance;
        switch (gameManager.selectedLevel)
        {
            case 0:
                level = 1;
                partLevel = 15;
                break;
            case 1:
                level = 2;
                partLevel = 6;
                break;
            case 2:
                level = 3;
                partLevel = 1;
                break;
            default:
                break;
        }
        CreatePart();
    }

    void CreatePart()
    {
        for (int i = 1; i <= partLevel; i++)
        {
            //canvas
            GameObject canvasLevel = new GameObject();
            canvasLevel.name = "canvasLevel" + i;
            Image canvasImage = canvasLevel.AddComponent<Image>();
            canvasLevel.AddComponent<Button>();
            canvasImage.color = new Color32(255, 255, 225, 255);
            canvasLevel
                .GetComponent<RectTransform>()
                .SetParent(gameObject.transform);
            canvasLevel.transform.localScale = new Vector3(1f, 1f, 0f);
            BtnScene btnScene = canvasLevel.AddComponent<BtnScene>();
            btnScene.sceneName = "Level" + level + "-" + i;

            // text
            GameObject textItem = new GameObject();
            Text NewText = textItem.AddComponent<Text>();
            NewText.text = level + "-" + i;
            NewText.font =
                Resources.GetBuiltinResource(typeof (Font), "Arial.ttf") as
                Font;
            NewText.fontSize = 20;
            NewText.color = new Color32(0, 0, 0, 255);
            NewText.alignment = TextAnchor.MiddleCenter;
            textItem.GetComponent<RectTransform>().sizeDelta =
                new Vector2(160f, 50f);
            textItem
                .GetComponent<RectTransform>()
                .SetParent(canvasLevel.transform);
            textItem.transform.localScale = new Vector3(1f, 1f, 0f);
        }
    }
}
