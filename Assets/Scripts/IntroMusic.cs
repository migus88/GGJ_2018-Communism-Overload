using migs.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]
public class IntroMusic : BaseBehaviour
{
    private AudioSource _audio;

    private void Awake()
    {
        var existing = GameObject.FindObjectsOfType<IntroMusic>();

        if(existing != null)
        {
            foreach (var item in existing)
            {
                if (item != this)
                    Destroy(item.gameObject);
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        this._audio = GetComponent<AudioSource>();
        this._typeEvents.Add(typeof(GameStatePayload), EventsManager.Instance.Subscribe<GameStatePayload>(this.onGameStateChanged));
    }

    private void onGameStateChanged(GameStatePayload obj)
    {
        if(obj.State == GameState.Countdown)
        {
            this._audio.Stop();
            Destroy(gameObject);
        }
    }
}
