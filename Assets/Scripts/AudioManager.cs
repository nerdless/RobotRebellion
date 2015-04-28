using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public AudioClip[] soundEffects;


	void Start () 
    {
        audio.clip = soundEffects[0];
        audio.Play();
	}

    void Normal()
    {
        if (audio.clip != soundEffects[0])
        {
            audio.clip = soundEffects[0];
            audio.Play();
        }
    }

    void Chasing()
    {
        //audio.Stop();
        if (audio.clip != soundEffects[1])
        {
            audio.clip = soundEffects[1];
            audio.Play();
        }
        //
    }

    void Restarding()
    {
        if (audio.clip != soundEffects[1])
        {
            audio.clip = soundEffects[1];
            audio.Play();
        }
        //audio.Play();
    }
}
