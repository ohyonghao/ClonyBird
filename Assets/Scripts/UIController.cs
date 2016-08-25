using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {

    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject player;
    [SerializeField] private SettingsPopup settingsPopup;
    [SerializeField] private GameMenuPopup gameMenuPopup;
    [SerializeField] private BirdMovement bird;

    int score = 0;
    int highScore = 0;

    public void AddPoint(int point) {
        if (bird.dead)
            return;

        score += point;
        Debug.Log("Point: " + point);
        Debug.Log("Score: " + score);
        if (score < 0)
            score = 0;

        if (score > highScore) {
            highScore = score;
        }
    }
    
    void Start() {
        score = 0;
        highScore = PlayerPrefs.GetInt("highScore", 0);
        settingsPopup.Close();
        gameMenuPopup.Close();
    }
    void Awake() {
        Messenger<int>.AddListener(GameEvent.SCORE_CHANGED, AddPoint);
    }
    void OnDestroy()
    {
        PlayerPrefs.SetInt("highScore", highScore);
        Messenger<int>.RemoveListener(GameEvent.SCORE_CHANGED, AddPoint);
    }

    void Update() {
        scoreText.text = "Score: " + score + "\nHigh Score: " + highScore;
        
        if (Input.GetKeyDown(KeyCode.Escape)) {
            gameMenuPopup.Open();
        }
    }

    public void OnOpenSettings() {
        settingsPopup.Open();
    }

}
