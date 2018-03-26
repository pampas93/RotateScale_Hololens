using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TemporaryManager : MonoBehaviour {

    public static TemporaryManager instance;

    private GameObject selectedGameObject = null;
    private GameObject previousSelectedObject = null;
    GameObject FocusedObject = null;

    UnityEngine.XR.WSA.Input.GestureRecognizer tapRecognizer;

    [SerializeField]
    bool AutoSelect = true;

    [SerializeField]
    Material boxMaterial;

    [SerializeField]
    TextMesh JustToDebug;

    [SerializeField]
    TextMesh SelectedObjectDebug;

    //private Transform currentTransform;
    //Below three vectors will hold the transform properties before changing (For reset purpose)
    private Vector3 position;
    private Vector3 scale;
    private Quaternion rotation;

    private void Awake()
    {
        instance = this;
    }

    public void ResetTransform()
    {
        Debug.Log(selectedGameObject);
        Debug.Log(position);
        if (selectedGameObject != null && position != Vector3.zero && scale != Vector3.zero)
        {
            GameObject parentObj = selectedGameObject.transform.parent.transform.gameObject;
            parentObj.transform.localScale = scale;
            parentObj.transform.localRotation = rotation;
            parentObj.transform.position = position;

            TransformMenu.instance.currentMode = TransformMenu.Mode.None;
        }
    }

    private void Start()
    {
        tapRecognizer = new UnityEngine.XR.WSA.Input.GestureRecognizer();
        tapRecognizer.TappedEvent += (source, tapCount, ray) =>
        {
            if (FocusedObject != null)
            {
                //JustToDebug.text = FocusedObject.name + " is clicked";
                //Debug.Log(FocusedObject);
                if (FocusedObject.tag != "TransformMenu")
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
        {
            SelectedObjectDebug.text = "Currently selected: " + selectedGameObject.name;

            //When SelectedGameObject is not null; we know it has a parent; So, we get the parent, getComponents of MSR and enable or disable

            GameObject parent = selectedGameObject.transform.parent.gameObject;

            var moveComponent = parent.GetComponent<MoveScript>();
            var rotateComponent = parent.GetComponent<RotateScript>();
            var scaleComponent = parent.GetComponent<ScaleScript>();

            JustToDebug.text = TransformMenu.instance.currentMode.ToString();

            switch (TransformMenu.instance.currentMode)
            {
                case TransformMenu.Mode.Move:
                    moveComponent.SetDragging(true);
                    rotateComponent.SetRotating(false);
                    scaleComponent.SetResizing(false);
                    break;
                case TransformMenu.Mode.Rotate:
                    rotateComponent.SetRotating(true);
                    moveComponent.SetDragging(false);
                    scaleComponent.SetResizing(false);
                    break;
                case TransformMenu.Mode.Scale:
                    scaleComponent.SetResizing(true);
                    moveComponent.SetDragging(false);
                    rotateComponent.SetRotating(false);
                    break;
                case TransformMenu.Mode.Reset:
                    Debug.Log("**************************Reset is enabled");
                    //TransformMenu.instance.currentMode = TransformMenu.Mode.None;
                    //Call the reset function

                    Debug.Log("Reset is Off~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    break;
                default:
                    scaleComponent.SetResizing(false);
                    moveComponent.SetDragging(false);
                    rotateComponent.SetRotating(false);
                    //Debug.Log("Nothing is enabled");
                    break;
            }
        }
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

            position = Vector3.zero;
            scale = Vector3.zero;
            rotation = Quaternion.Euler(Vector3.zero);
                

            //currentTransform = null;

            //Hide Transform menu when deselected.
            TransformMenu.instance.showMenu = false;
        }
    }

    private void CreateBox(GameObject obj)
    {
        float gap = 0.15f;
        var parentObj = new GameObject();
        parentObj.name = "Parent Temporary Object";
        parentObj.transform.position = obj.transform.position;
        obj.transform.SetParent(parentObj.transform);

        Vector3 center = obj.GetComponent<Collider>().bounds.center;
        Vector3 size = obj.GetComponent<Collider>().bounds.size;

        Debug.Log(center);
        Debug.Log(size);

        GameObject mesh = GameObject.CreatePrimitive(PrimitiveType.Cube);
        mesh.name = "Outer Box";
        mesh.transform.position = center;
        mesh.transform.localScale = new Vector3(size.x + gap, size.y + gap, size.z + gap);
        mesh.GetComponent<Renderer>().material = boxMaterial;
        Destroy(mesh.GetComponent<BoxCollider>());              //Remove Box Collider from the outer box //obstructing when click

        mesh.transform.SetParent(parentObj.transform);

        AddTransformScripts(parentObj);                         //Add the Move, Scale and Rotate Script

        position = parentObj.transform.position;
        scale = parentObj.transform.localScale;
        rotation = parentObj.transform.rotation;
        //currentTransform = parentObj.transform;

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
