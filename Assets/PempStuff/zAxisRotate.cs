using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

public class zAxisRotate : MonoBehaviour,IInputClickHandler {

    bool selected = false;
    private Shader x;
    private Shader defaultShader;
    private HandRotate rotateScript;


    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (!selected)
        {
            this.GetComponent<Renderer>().material.shader = x;
            selected = true;
            Debug.Log("Z is selected");
            rotateScript.rotateAxis = 'Z';
        }
        else
        {
            this.GetComponent<Renderer>().material.shader = defaultShader;
            selected = false;
            Debug.Log("Z is deselected");
            rotateScript.rotateAxis = ' ';
        }
        
    }

    // Use this for initialization
    void Start()
    {

        defaultShader = this.GetComponent<Renderer>().material.shader;
        x = Shader.Find("Legacy Shaders/Bumped Diffuse");
        GameObject grandParent = this.gameObject.transform.parent.gameObject.transform.parent.gameObject;
        //Debug.Log(grandParent);
        rotateScript = grandParent.GetComponent<HandRotate>();

    }

    // Update is called once per frame
    void Update()
    {


    }
}
