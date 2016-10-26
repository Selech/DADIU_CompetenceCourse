using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public static AudioManager Instance;
    public AudioSource roll;
    public AudioSource collision;
    public AudioSource powerup;

	// Use this for initialization
	void Awake () {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
	}

    void Update()
    {
        
    }

    public void ToggleRollSound(bool play)
    {
        if (play)
            roll.Play();
        else
            roll.Pause();
    }

    public void PlayCollisionSound()
    {
        collision.Play();
    }

    public void PlayPowerupSound()
    {
        powerup.Play();
    }

    public void SetRollPitch(float speed)
    {
        roll.pitch = 0.8f * speed + 0.3f;
        roll.volume = 0.4f * speed + 0.1f;
    }
}
