using UnityEngine;
using System.Collections;

public class PointTimer : MonoBehaviour {

    const float _timelapsed = 2f;
    float nextpointloss;
    void Start() {
        nextpointloss = Time.time + _timelapsed;
    }
    void Update() {
        if (nextpointloss < Time.time) {
            nextpointloss = Time.time + _timelapsed;
            Messenger<int>.Broadcast(GameEvent.SCORE_CHANGED, -1);
        }
    }
}
