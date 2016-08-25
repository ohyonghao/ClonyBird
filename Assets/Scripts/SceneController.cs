using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {

    int numBGPanels = 6;

	public float pipeMax = 0.8430938f;
	public float pipeMin = -0.003243029f;
    public float xOffset = 1.2f;
    public GameObject prefab;
    
	void Start() {
        for (int i = 0 ; i < numBGPanels; i++)
        {
            Instantiate(prefab, new Vector3(1.2f*i+xOffset, 0f, 0f), Quaternion.identity);
        }
		GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");
        foreach(GameObject pipe in pipes) {
			Vector3 pos = pipe.transform.position;
			pos.y = Random.Range(pipeMin, pipeMax);
			pipe.transform.position = pos;
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		float widthOfBGObject = ((BoxCollider2D)collider).size.x;

		Vector3 pos = collider.transform.position;

		pos.x += widthOfBGObject * numBGPanels;

		if(collider.tag == "Pipe") {
			pos.y = Random.Range(pipeMin, pipeMax);
		}

		collider.transform.position = pos;

	}
}
