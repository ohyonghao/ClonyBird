using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class BirdMovement : MonoBehaviour {
    
	public float flapSpeed    = 100f;
	float forwardSpeed = 1f;
    float maxSpeed = 3f;
    const float realMaxSpeed = 20f; // Cannot exceed this

	bool didFlap = false;

	Animator animator;

	public bool dead = false;
	float deathCooldown;

	public bool godMode = false;
    public GameObject startScreen;

	// Use this for initialization
	void Start () {
		animator = transform.GetComponentInChildren<Animator>();

		if(animator == null) {
			Debug.LogError("Didn't find animator!");
        }
        forwardSpeed = PlayerPrefs.GetFloat("speed", forwardSpeed);
        maxSpeed = PlayerPrefs.GetFloat("maxspeed", maxSpeed);
    }

	// Do Graphic & Input updates here
	void Update() {
		if(dead) {
			deathCooldown -= Time.deltaTime;

			if(deathCooldown <= 0)
            {
                if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !EventSystem.current.IsPointerOverGameObject() ) {
                    PlayerPrefs.SetFloat("speed", forwardSpeed);
                    PlayerPrefs.SetFloat("maxspeed", maxSpeed);
					SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single );
				}
			}
		}
		else {
			if(Time.timeScale != 0 && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !EventSystem.current.IsPointerOverGameObject()) {
				didFlap = true;
			}
		}
	}

	
	// Do physics engine updates here
	void FixedUpdate () {

		if(dead)
			return;

		GetComponent<Rigidbody2D>().AddForce( Vector2.right * forwardSpeed );

		if(didFlap) {
			GetComponent<Rigidbody2D>().AddForce( Vector2.up * flapSpeed );
			animator.SetTrigger("DoFlap");

			didFlap = false;
		}

		if(GetComponent<Rigidbody2D>().velocity.y > 0) {
			transform.rotation = Quaternion.Euler(0, 0, 0);
		}
		else {
			float angle = Mathf.Lerp (0, -90, (-GetComponent<Rigidbody2D>().velocity.y / maxSpeed) );
			transform.rotation = Quaternion.Euler(0, 0, angle);
		}
	}

    void OnCollisionEnter2D(Collision2D collision) {
        if (godMode)
            return;

        if (collision.gameObject.tag == "Ground" ) {
            animator.SetTrigger("Death");
            dead = true;
            deathCooldown = 0.5f;
        }
	}
    void Awake() {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, setForwardSpeed);
        Messenger<float>.AddListener(GameEvent.MAX_SPEED_CHANGED, setMaxSpeed);
    }
    void OnDestroy() {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, setForwardSpeed);
        Messenger<float>.RemoveListener(GameEvent.MAX_SPEED_CHANGED, setMaxSpeed);
        PlayerPrefs.SetFloat("speed", forwardSpeed);
        PlayerPrefs.SetFloat("maxspeed", maxSpeed);
    }
    // Change speed settings
    public void setForwardSpeed(float speed) {
        if( speed > maxSpeed) {
            forwardSpeed = maxSpeed;
        }else {
            forwardSpeed = speed;
        }
    }
    public void setMaxSpeed(float speed) {
        if( speed > realMaxSpeed) {
            maxSpeed = realMaxSpeed;
        }else {
            maxSpeed = speed;
        }

        if(forwardSpeed > maxSpeed) {
            forwardSpeed = maxSpeed;
        }
    }
}
