using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour {

    public GameObject audioClip;

    public static HealthPickUp Instance;

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        //audioClip = GameObject.Find("jatajamat");
        //audioClip.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
            //audioClip.SetActive(false);
            //audioClip.SetActive(true);
            Instantiate(audioClip);
			float fullHP = PlayerPrefs.GetFloat ("Hero1BaseHP");
			PlayerPrefs.SetFloat ("Hero1HP", fullHP);
			fullHP = PlayerPrefs.GetFloat ("Hero2BaseHP");
			PlayerPrefs.SetFloat ("Hero2HP", fullHP);
            Destroy(this.gameObject);
            Debug.Log("HealthPickUp" + " " + fullHP);
		}
	}
}
