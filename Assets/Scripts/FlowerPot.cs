using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerPot : MonoBehaviour
{
    private Color originalColor;
    public bool mouseHover = false;
    
    void Start()
    {
        originalColor = GameObject.GetComponent<Renderer>().material.color;
    }
    
    void OnMouseOver()
    {
        //change color
        mouseHover = true;
    }

    void OnMouseExit()
    {
        //revert back
        mouseHover = false;
    }
}
