using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        transform.Rotate(this.gameObject.transform.rotation.x, this.gameObject.transform.rotation.y + 1, this.gameObject.transform.rotation.z);
    }
}
