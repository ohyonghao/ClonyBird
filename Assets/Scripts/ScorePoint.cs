using UnityEngine;
using System.Collections;

public class ScorePoint : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D collider) {
		if(collider.tag == "Player") {
            Messenger<int>.Broadcast(GameEvent.SCORE_CHANGED, 1);
		}
	}
}
