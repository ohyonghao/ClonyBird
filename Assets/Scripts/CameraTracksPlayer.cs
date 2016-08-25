using UnityEngine;
using System.Collections;

public class CameraTracksPlayer : MonoBehaviour {

	Transform playerTransform;

    [SerializeField]
    private GameObject player;
	float offsetX;

	// Use this for initialization
	void Start () {
		playerTransform = player.transform;

		offsetX = transform.position.x - playerTransform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		if(playerTransform != null) {
			Vector3 pos = transform.position;
			pos.x = playerTransform.position.x + offsetX;
			transform.position = pos;
		}
	}
}
