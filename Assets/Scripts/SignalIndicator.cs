using migs.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(SpriteRenderer))]
public class SignalIndicator : BaseBehaviour
{
    public Sprite GreenSignalSprite;
    public Sprite NoSignalSprite;

    private SpriteRenderer _renderer;

    private void Awake()
    {
        this._renderer = GetComponent<SpriteRenderer>();
        this._typeEvents.Add(typeof(PlayerDistancePayload), EventsManager.Instance.Subscribe<PlayerDistancePayload>(this.onSignalChanged));
    }

    private void onSignalChanged(PlayerDistancePayload obj)
    {
        this._renderer.sprite = obj.State == PlayerDistancePayload.PlayersState.InRange ? GreenSignalSprite : NoSignalSprite;
    }
}
