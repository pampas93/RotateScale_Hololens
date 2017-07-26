/////////////////////////////////
///  Abhijit Srikanth         ///
///  TransformOptions.cs      ///
///  github.com/pampas93      ///
/////////////////////////////////

/* Main Function:
 *  - Select and deselect transform option (given in option menu)
 *  - Change material when clicked
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

public class TransformOptions : MonoBehaviour, IInputClickHandler
{

    private TransformMenu menuScript;
    private Material defaultMat;

    public string option = "";
    public Material selectedMat;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        //Debug.Log(option + "is clicked");
        if (menuScript.trans != option)
        {
            //Select and change material to blue
            menuScript.trans = option;
            gameObject.GetComponent<Renderer>().material = selectedMat;
        }
        else
        {
            //Unselect and change material back to grey
            menuScript.trans = "";
            gameObject.GetComponent<Renderer>().material = defaultMat;
        }

        
    }

    // Use this for initialization
    void Start()
    {
        defaultMat = gameObject.GetComponent<Renderer>().material;
        menuScript = gameObject.transform.parent.gameObject.GetComponent<TransformMenu>();
        //Debug.Log(menuScript);
    }

}
