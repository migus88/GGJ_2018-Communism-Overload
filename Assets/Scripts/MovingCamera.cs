using migs.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : BaseBehaviour
{
    public float Speed = 2.5f;

    private bool _isStarted = false;

    private void Awake()
    {
        this._typeEvents.Add(typeof(GameStatePayload), EventsManager.Instance.Subscribe<GameStatePayload>(this.onGameStateChanged));
    }

    void Update()
    {
        if (this._isStarted)
            transform.position = transform.position + Vector3.right * Speed * Time.deltaTime;
    }

    private void onGameStateChanged(GameStatePayload obj)
    {
        this._isStarted = obj.State == GameState.Started;
    }
}

