using migs.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AgentEnterence : BaseBehaviour
{
    private void Start()
    {
        this._typeEvents.Add(typeof(GameStatePayload), EventsManager.Instance.Subscribe<GameStatePayload>(this.onGameStateChanged));
    }

    private void onGameStateChanged(GameStatePayload obj)
    {
        if(obj.State == GameState.Countdown)
        {
            StartCoroutine(this.animateEntrence());
        }
    }

    private IEnumerator animateEntrence()
    {
        for (int i = 0; i < 6; i++)
        {
            for (float y = 0; y < 1f; y += Time.deltaTime)
            {
                transform.position = new Vector3(transform.position.x + 1f * Time.deltaTime, transform.position.y);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
