using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

public class zAxisRotate : MonoBehaviour,IInputClickHandler {

    //public bool selected = false;
    private Shader x;
    private Shader defaultShader;
    private HandRotate rotateScript;


    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (rotateScript.rotateAxis != 'Z')
        {
            Debug.Log("Z switched on");
            rotateScript.rotateAxis = 'Z';
            this.GetComponent<Renderer>().material.shader = x;
        }
        else
        {
            Debug.Log("Z switched off");
            rotateScript.rotateAxis = ' ';
            this.GetComponent<Renderer>().material.shader = defaultShader;
        }

    }

    // Use this for initialization
    void Start()
    {

        defaultShader = this.GetComponent<Renderer>().material.shader;
        x = Shader.Find("Legacy Shaders/Self-Illumin/Bumped Diffuse");
        GameObject grandParent = this.gameObject.transform.parent.gameObject.transform.parent.gameObject;
        //Debug.Log(grandParent);
        rotateScript = grandParent.GetComponent<HandRotate>();

    }

    // Update is called once per frame
    void Update()
    {
        if (rotateScript.rotateAxis == 'Z')
        {
            this.GetComponent<Renderer>().material.shader = x;
        }
        else
        {
            this.GetComponent<Renderer>().material.shader = defaultShader;
        }

    }
}
