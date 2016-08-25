using UnityEngine;
using System.Collections;

public class GameMenuPopup : MonoBehaviour {

    bool clicksEnabled;
    float originalTimeScale;
    [SerializeField] GameObject disableClicks;
    void Start() {
        originalTimeScale = 0f;
        clicksEnabled = false;
    }
    public void Open() {
        originalTimeScale = Time.timeScale;
        Time.timeScale = 0;
        Debug.Log("Time Stopped in GameMenuPopup");

        clicksEnabled = disableClicks.activeSelf;
        gameObject.SetActive(true);
        disableClicks.SetActive(true);
    }
    public void Close() {
        gameObject.SetActive(false);
        if(!clicksEnabled)
            disableClicks.SetActive(false);
        Time.timeScale = originalTimeScale;
        Debug.Log("Time is " + originalTimeScale + " in GameMenuPopup");
    }
    public void QuitGame() {
        Application.Quit();
    }
}
