using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuPosition : MonoBehaviour {


    private GameObject parentObj;
    private Renderer r;

    void Start () {

        parentObj = this.transform.parent.gameObject;
        r = parentObj.GetComponent<Renderer>();
        //Debug.Log(parentObj);
    }
	
	void Update () {

        Vector3 menuPosition;

        var xx = r.bounds.extents.x / 2;

        float t = parentObj.transform.localScale.y / 2;
        Debug.Log(t);
        //menuPosition = new Vector3(r.bounds.center.x + xx, r.bounds.center.y, r.bounds.center.z);
        menuPosition = new Vector3(parentObj.transform.position.x, parentObj.transform.position.y + t + 0.2f, parentObj.transform.position.z);

        //Debug.Log(menuPosition);
        transform.position = menuPosition;

        transform.LookAt(Camera.main.transform);

    }
}
