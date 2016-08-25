using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsPopup : MonoBehaviour {

    float originalTimeScale;
    [SerializeField] Slider speedSlider;
    [SerializeField] Text speedText;
    [SerializeField] Slider maxSpeedSlider;
    [SerializeField] Text maxSpeedText;
    [SerializeField] GameObject disableClicks;

    bool clicksEnabled;
    void Start() {
        originalTimeScale = 0f;
        maxSpeedSlider.maxValue = 30f;
        maxSpeedSlider.value = PlayerPrefs.GetFloat("maxspeed", 3f);
        speedSlider.maxValue = maxSpeedSlider.value;
        speedSlider.value = PlayerPrefs.GetFloat("speed", 1f);
        
    }
	
	public void Open() {
        
        originalTimeScale = Time.timeScale;
        Time.timeScale = 0;
        Debug.Log("Time Stopped in SettingsPopup");

        clicksEnabled = disableClicks.activeSelf;
        disableClicks.SetActive(true);
        gameObject.SetActive(true);
    }
    public void Close() {
        gameObject.SetActive(false);
        if(!clicksEnabled)
            disableClicks.SetActive(false);
        Time.timeScale = originalTimeScale;
        Debug.Log("Time is " + originalTimeScale + " in SettingsPopup");
    }
    public void OnSpeedValueChanged(float speed) {
        Debug.Log("Speed: " + speed);
        Messenger<float>.Broadcast(GameEvent.SPEED_CHANGED, speed);
        speedText.text = "" + speed;
    }
    public void OnMaxSpeedValueChanged(float speed) {
        Debug.Log("Max Speed: " + speed);
        Messenger<float>.Broadcast(GameEvent.MAX_SPEED_CHANGED, speed);
        maxSpeedText.text = "" + speed;
    }
}
