using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScroll : MonoBehaviour
{
    public float speed;

    MeshRenderer rend;

    Material mat;

    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        mat = rend.material;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = mat.mainTextureOffset;
        offset.x += Time.deltaTime * speed;
        mat.mainTextureOffset = offset;
    }
}
