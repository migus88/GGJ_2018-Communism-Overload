using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour {

    private AudioSource _audio;

	// Use this for initialization
	void Start () {
        this._audio = GetComponent<AudioSource>();
	}
	
    public void PlaySound()
    {
        this._audio.time = 0;
        this._audio.Play();
    }
}
