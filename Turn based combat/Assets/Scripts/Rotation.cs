using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {

    public int rotateSpeed = 40;

	void FixedUpdate () {
        this.transform.RotateAround(gameObject.transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
        
	}
}
