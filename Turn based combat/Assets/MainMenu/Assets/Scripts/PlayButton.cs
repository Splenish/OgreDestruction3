using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayButton : MonoBehaviour {

    public void StartGame() {
        SceneManager.LoadScene("world");
    }
	
}
