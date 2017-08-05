using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.VR.WSA.Input;

public class ObjectManager : MonoBehaviour {

    public static ObjectManager instance;

    private GameObject selectedGameObject = null;
    private GameObject previousSelectedObject = null;
    GameObject FocusedObject = null;

    GestureRecognizer tapRecognizer;

    [SerializeField]
    bool AutoSelect = true;

    [SerializeField]
    Material boxMaterial;

    [SerializeField]
    TextMesh JustToDebug;

    [SerializeField]
    TextMesh SelectedObjectDebug;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        tapRecognizer = new GestureRecognizer();
        tapRecognizer.TappedEvent += (source, tapCount, ray) =>
        {
            if(FocusedObject != null)
            {
                JustToDebug.text = FocusedObject.name + " is clicked";
                //Debug.Log(FocusedObject);
                if(FocusedObject.tag != "TransformMenu")
                {
                    SetSelectedGameObject(FocusedObject);
                }       
            }

        };

        if (AutoSelect)
        {
            tapRecognizer.StartCapturingGestures();
        }

    }

    private void Update()
    {
        
        GameObject oldFocusObject = FocusedObject;

        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            FocusedObject = hitInfo.collider.gameObject;
        }
        else
        {
            FocusedObject = null;
        }

        // If the focused object changed this frame, start detecting fresh gestures again.
        if (FocusedObject != oldFocusObject)
        {
            tapRecognizer.CancelGestures();
            tapRecognizer.StartCapturingGestures();
        }

        if (selectedGameObject != null)
            SelectedObjectDebug.text = "Currently selected: " + selectedGameObject.name;
        else
            SelectedObjectDebug.text = "Currently selected: None";
    }

    public void SetSelectedGameObject(GameObject obj)
    {
        previousSelectedObject = selectedGameObject;        //Set the previously selected object to previousSelectedObject

        selectedGameObject = obj;                           //Set the current object selected to selectedGameObject
        Debug.Log("Currently Selected object is " + selectedGameObject);
        Debug.Log("Previously selected object was " + previousSelectedObject);

        RemoveBox(previousSelectedObject);

        if (selectedGameObject != previousSelectedObject)
        {
            CreateBox(selectedGameObject);
        }
        else
        {
            //RemoveBox(selectedGameObject);
            selectedGameObject = null;
            previousSelectedObject = null;
        }

    }

    private void RemoveBox(GameObject obj)
    {
        if (obj != null)
        {
            GameObject parentObj = obj.transform.parent.gameObject;
            obj.transform.parent = null;
            Destroy(parentObj);

            //Hide Transform menu when deselected.
            TransformMenu.instance.showMenu = false;
        }
    }

    private void CreateBox(GameObject obj)
    {
        float gap = 0.15f;
        var parentObj = new GameObject();
        parentObj.name = "Parent Temporary Object";
        obj.transform.SetParent(parentObj.transform);

        Vector3 center = obj.GetComponent<Renderer>().bounds.center;
        Vector3 size = obj.GetComponent<Renderer>().bounds.size;

        GameObject mesh = GameObject.CreatePrimitive(PrimitiveType.Cube);
        mesh.name = "Outer Box";
        mesh.transform.position = center;
        mesh.transform.localScale = new Vector3(size.x + gap, size.y + gap, size.z + gap);
        mesh.GetComponent<Renderer>().material = boxMaterial;
        Destroy(mesh.GetComponent<BoxCollider>());              //Remove Box Collider from the outer box //obstructing when click

        mesh.transform.SetParent(parentObj.transform);

        AddTransformScripts(parentObj);                         //Add the Move, Scale and Rotate Script

        //Show Transform menu when object selected
        TransformMenu.instance.showMenu = true;
    }

    private void AddTransformScripts(GameObject parentObj)
    {
        parentObj.AddComponent<ScaleScript>();
        parentObj.AddComponent<RotateScript>();
        parentObj.AddComponent<MoveScript>();
    }
}
