using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

public class TransformMenu : MonoBehaviour, IInputClickHandler {

    public static TransformMenu instance;

    [HideInInspector]
    public enum Mode { Move, Scale, Rotate, Reset, None };

    [HideInInspector]
    public Mode currentMode = Mode.None;

    [HideInInspector]
    public bool showMenu = false;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        //Keep a track to show Menu or not
        if (showMenu)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }

        

        ////Checking th mode of transform
        //switch (currentMode)
        //{
        //    case Mode.Move:
        //        Debug.Log("Move is enabled");
        //        break;
        //    case Mode.Rotate:
        //        Debug.Log("Rotate is enabled");
        //        break;
        //    case Mode.Scale:
        //        Debug.Log("Scale is enabled");
        //        break;
        //    case Mode.Reset:
        //        Debug.Log("**************************Reset is enabled");
        //        currentMode = Mode.None;
        //        //Call the reset function
        //        Debug.Log("Reset is Off~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        //        break;
        //    default:
        //        //Debug.Log("Nothing is enabled");
        //        break;
        //}
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        showMenu = !showMenu;

    }

}
