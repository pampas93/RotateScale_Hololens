using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

public class InsertPrefab : MonoBehaviour, IInputClickHandler
{

    public GameObject Prefab;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Prefab.SetActive(true);
    }

    
}
