using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BtnReviewStore : MonoBehaviour
{
    Button btn;

    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener (Click);
    }

    // Update is called once per frame
    void Click()
    {
        Application.OpenURL("market://details?id=" + Application.productName);
    }
}
