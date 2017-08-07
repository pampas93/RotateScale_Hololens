using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

public class ResetOption : MonoBehaviour, IInputClickHandler {


    public void OnInputClicked(InputClickedEventData eventData)
    {
        TransformMenu.instance.currentMode = TransformMenu.Mode.Reset;
        ObjectManager.instance.ResetTransform();
        
        
    }
  
}
