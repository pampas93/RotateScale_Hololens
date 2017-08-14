using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

public class MoveOption : MonoBehaviour, IInputClickHandler {

    private bool isSelected = false;
    private Material defaultMat;

    [SerializeField]
    Material selectedMaterial;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (!isSelected)
        {
            TransformMenu.instance.currentMode = TransformMenu.Mode.Move;
            isSelected = true;
        }
        else
        {
            TransformMenu.instance.currentMode = TransformMenu.Mode.None;
            isSelected = false;
        }
    }

    private void Awake()
    {
        defaultMat = this.gameObject.GetComponent<Renderer>().material;
    }

    private void Update()
    {
        TransformMenu.Mode temp = TransformMenu.instance.currentMode;
        if (temp == TransformMenu.Mode.Move)
        {
            this.gameObject.GetComponent<Renderer>().material = selectedMaterial;
            isSelected = true;
        }
        else
        {
            this.gameObject.GetComponent<Renderer>().material = defaultMat;
            isSelected = false;
        }
    }

}
