using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateReferencePoints : MonoBehaviour {

    [SerializeField]
    Material MeshMaterial;

	// Use this for initialization
	void Start () {

        Vector3 center = this.gameObject.GetComponent<Renderer>().bounds.center;
        Vector3 size = this.gameObject.GetComponent<Renderer>().bounds.size;
        Vector3 max = this.gameObject.GetComponent<Renderer>().bounds.max;

        Debug.Log(max);

        GameObject mesh = GameObject.CreatePrimitive(PrimitiveType.Cube);
        mesh.transform.position = center;
        mesh.transform.localScale = size;

        mesh.GetComponent<Renderer>().material = MeshMaterial;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
