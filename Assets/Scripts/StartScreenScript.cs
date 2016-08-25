using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class StartScreenScript : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        showStartScreen();
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale==0 && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !EventSystem.current.IsPointerOverGameObject()) {
			Time.timeScale = 1;
            Debug.Log("Time Started in StartScreenScript");
			GetComponent<SpriteRenderer>().enabled = false;

		}
	}

    public void showStartScreen( )
    {
        GetComponent<SpriteRenderer>().enabled = true;
        Time.timeScale = 0;
        Debug.Log("Time Stopped in StartScreenScript");
    }
}
