using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSombra : MonoBehaviour
{
    SpriteRenderer myrenderer;
    private Shader mymaterial;
    public Color mycolor;
    
    // Start is called before the first frame update
    void Start()
    {
        myrenderer = GetComponent<SpriteRenderer>();
        mymaterial = Shader.Find("GUI/Text Shader");
    }

    // Update is called once per frame
    void Update()
    {
        ColorSprite();
    }

    void ColorSprite()
    {
        myrenderer.material.shader = mymaterial;   
        myrenderer.color = mycolor;
        
    }

    public void Finish()
    {
        gameObject.SetActive(false);
    }
}
