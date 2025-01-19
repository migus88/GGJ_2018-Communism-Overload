using migs.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameMusicSpawner : BaseBehaviour
{
    public AudioSource Audio;

    private void Awake()
    {
        this._typeEvents.Add(typeof(GameStatePayload), EventsManager.Instance.Subscribe<GameStatePayload>(this.onGameStateChanged));
    }

    private void onGameStateChanged(GameStatePayload obj)
    {
        if(obj.State == GameState.Countdown)
        {
            Audio.Play();
        }
    }
}
