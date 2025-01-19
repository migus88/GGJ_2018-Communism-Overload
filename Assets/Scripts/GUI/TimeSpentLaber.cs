using migs.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TimeSpentLaber : BaseBehaviour
{
    private Text _text;
    private float _time = 0;
    private bool _isRunning = false;

    private void Start()
    {
        this._text = GetComponent<Text>();

        this._typeEvents.Add(typeof(GameStatePayload), EventsManager.Instance.Subscribe<GameStatePayload>(this.onGameStateChanged));
    }

    private void Update()
    {
        this._text.text = string.Format(TranslationManager.Instance.GetText("Game", "TimeSpent"), "00:00");

        if (!this._isRunning)
            return;

        var timer = Time.time - this._time;
        var timeStr = string.Format("{0}:{1:00}", (int)timer / 60, (int)timer % 60);

        this._text.text = string.Format(TranslationManager.Instance.GetText("Game", "TimeSpent"), timeStr); // $"Time spent: {timeStr}";
    }

    private void onGameStateChanged(GameStatePayload obj)
    {
        if(obj.State == GameState.Started)
        {
            this._time = Time.time;
            this._isRunning = true;
        }
        else if(obj.State == GameState.Lose || obj.State == GameState.Win)
        {
            this._isRunning = false;
            var timer = Time.time - this._time;
            var timeStr = string.Format("{0}:{1:00}", (int)timer / 60, (int)timer % 60);
            PlayerPrefs.SetString("TimeSpent", timeStr);
        }

    }
}
