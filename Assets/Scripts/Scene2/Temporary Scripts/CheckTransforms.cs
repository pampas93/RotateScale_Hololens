using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTransforms : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //transform.localScale += new Vector3(0.02f,0.02f,0.02f);
        transform.Rotate(Vector3.up * Time.deltaTime * 50, Space.World);
    }
}
