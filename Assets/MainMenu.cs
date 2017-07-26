using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    Transform camTransform;
    Vector3 pos;

    // Use this for initialization
    void Start () {

        camTransform = Camera.main.transform;

        //-0.2,-0.25,+1

      // transform.position = new Vector3(camTransform.position.x, camTransform.position.y - 0.08f, camTransform.position.z +1.0f);
	}
	
	// Update is called once per frame
	void Update () {

        camTransform = Camera.main.transform;
        pos = this.gameObject.transform.position;

        Debug.Log(pos);

        //transform.position = new Vector3(camTransform.position.x, camTransform.position.y - 0.08f, camTransform.position.z + 1f);

        transform.LookAt(Camera.main.transform);
    }
}
