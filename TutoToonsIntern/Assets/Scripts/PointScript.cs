using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class PointScript : MonoBehaviour
{
    private int index;
    private GameObject engine;
    private EngineScript scEngine;
    private GameObject blue;
    private GameObject magenta;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        TextMeshPro txt = GetComponentInChildren<TextMeshPro>();
        index = Int32.Parse(txt.text);
        magenta = transform.Find("button_magenta").gameObject;
        blue = transform.Find("button_blue").gameObject;
        engine = GameObject.Find("GameEngine");
        scEngine = engine.GetComponent<EngineScript>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (scEngine.PressPointIndex == index)
        {
            animator.SetTrigger("Clicked");
     
            scEngine.DrawLine();
            scEngine.PressPointIndex++;
            
        }
    }
}
