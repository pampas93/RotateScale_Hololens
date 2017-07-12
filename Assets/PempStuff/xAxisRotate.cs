using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

public class xAxisRotate : MonoBehaviour,IInputClickHandler {

    //public bool selected = false;
    private Shader x;
    private Shader defaultShader;
    private HandRotate rotateScript;


    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("Click function");
        if (rotateScript.rotateAxis != 'X')
        {
            Debug.Log("X switched on");
            rotateScript.rotateAxis = 'X';
            this.GetComponent<Renderer>().material.shader = x;
        }
        else
        {
            Debug.Log("X switched off");
            rotateScript.rotateAxis = ' ';
            this.GetComponent<Renderer>().material.shader = defaultShader;
        }
        //throw new NotImplementedException();
    }

    // Use this for initialization
    void Start () {

        defaultShader = this.GetComponent<Renderer>().material.shader;
        x = Shader.Find("Legacy Shaders/Bumped Diffuse");
        GameObject grandParent = this.gameObject.transform.parent.gameObject.transform.parent.gameObject;
        //Debug.Log(grandParent);
        rotateScript = grandParent.GetComponent<HandRotate>();

    }
	
	// Update is called once per frame
	void Update () {

        if(rotateScript.rotateAxis == 'X')
        {
            this.GetComponent<Renderer>().material.shader = x;
        }
        else
        {
            this.GetComponent<Renderer>().material.shader = defaultShader;
        }


    }
}
