using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

public class yAxisRotate : MonoBehaviour, IInputClickHandler {

    //public bool selected = false;
    private Shader x;
    private Shader defaultShader;
    private HandRotate rotateScript;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("Y axis clicked");
        if(rotateScript.rotateAxis != 'Y')
        {
            Debug.Log("Y switched on");
            rotateScript.rotateAxis = 'Y';
            this.GetComponent<Renderer>().material.shader = x;
        }
        else
        {
            Debug.Log("Y switched off");
            rotateScript.rotateAxis = ' ';
            this.GetComponent<Renderer>().material.shader = defaultShader;
        }
        //if (!selected)
        //{
        //    //this.GetComponent<Renderer>().material.shader = x;
        //    //selected = true;
        //    //Debug.Log("Y is selected");
        //    rotateScript.rotateAxis = 'Y';
        //}
        //else
        //{
        //    //this.GetComponent<Renderer>().material.shader = defaultShader;
        //    //selected = false;
        //    //Debug.Log("Y is deselected");
        //    rotateScript.rotateAxis = ' ';
        //}
        
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
        if (rotateScript.rotateAxis == 'Y')
        {
            this.GetComponent<Renderer>().material.shader = x;
        }
        else
        {
            this.GetComponent<Renderer>().material.shader = defaultShader;
        }
    }
}
