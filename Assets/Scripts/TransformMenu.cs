using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformMenu : MonoBehaviour {

    public string trans = "";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        switch (trans)
        {
            case "Move":    //Enable HandDraggable and disable rest
                break;
            case "Scale":   //Enable HandResize and disable rest
                break;
            case "Rotate":  //Enable HandRotate and disable rest
                break;
            default:    //Disable all three

                break;
        }
		
	}
}
