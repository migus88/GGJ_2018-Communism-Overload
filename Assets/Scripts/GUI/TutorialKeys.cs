using migs.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TutorialKeys : BaseBehaviour
{

    // Use this for initialization
    void Start()
    {
        this._typeEvents.Add(typeof(GameStatePayload), EventsManager.Instance.Subscribe<GameStatePayload>(this.onGameStateChanged));
    }

    private void onGameStateChanged(GameStatePayload obj)
    {
        if(obj.State == GameState.Countdown)
        {
            gameObject.SetActive(false);
        }
    }
}
