using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.VR.WSA.Input;
using System;

public class TapRecognizer : MonoBehaviour {

    protected GestureRecognizer recognizer;

    //// Use this for initialization
    //void Start () {

    //    gestureRecognizer = new GestureRecognizer();
    //    gestureRecognizer.SetRecognizableGestures(GestureSettings.Tap);
    //    gestureRecognizer.TappedEvent += OnTappedEvent;
    //    gestureRecognizer.StartCapturingGestures();

    //    if(gestureRecognizer != null)
    //        Debug.Log(gestureRecognizer);
    //}

    private void Awake()
    {
        Debug.Log(recognizer);
        // Set up a GestureRecognizer to detect Select gestures.
        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += (source, tapCount, ray) =>
        {
            Debug.Log("Click working?");
        };
        recognizer.StartCapturingGestures();
        Debug.Log(recognizer);
    }

    //private void OnTappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    //{
    //    Debug.Log("Tapped");

    //}

    // Update is called once per frame
 //   void Update () {

 //       if (gestureRecognizer.IsCapturingGestures())
 //       {
 //           Debug.Log("Capturing gestures");
 //       }

	//}
}
