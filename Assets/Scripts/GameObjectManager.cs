using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectManager : MonoBehaviour {

    public static GameObjectManager instance;
    private GameObject selectedGameObject = null;
    private GameObject previousSelectedObject = null;

    [SerializeField]
    Material boxMaterial;

    private void Awake()
    {
        instance = this;
    }

    public void SetGameObject(GameObject obj)
    {
        previousSelectedObject = selectedGameObject;        //Set the previously selected object to previousSelectedObject

        selectedGameObject = obj;                           //Set the current object selected to selectedGameObject
        Debug.Log("Currently Selected object is " + selectedGameObject);
        Debug.Log("Previously selected object was " + previousSelectedObject);

        RemoveBox(previousSelectedObject);

        if(selectedGameObject != previousSelectedObject)
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
        }
    }

    private void CreateBox(GameObject obj)
    {
        float gap = 0.25f;
        var parentObj = new GameObject();
        parentObj.transform.position = obj.transform.position;
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

    }
}
