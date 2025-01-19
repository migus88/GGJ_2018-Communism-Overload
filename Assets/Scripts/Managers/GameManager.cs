using migs.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : BaseBehaviour
{
    public static GameManager Instance;

    private GameState _state = GameState.Waiting;
    private int _currentCountdown = 0;

    public GameState State
    {
        get
        {
            return this._state;
        }
        set
        {
            this._state = value;
            this.onStateChanged();
        }
    }

    public KeyCode StartGameKey = KeyCode.Return;
    public int SecondsBeforeStart = 3;


    private void onStateChanged()
    {
        switch (State)
        {
            case GameState.Waiting:
                break;
            case GameState.Countdown:
                StartCoroutine(this.startCountdown());
                break;
            case GameState.Started:
            case GameState.Win:
                break;
            case GameState.Lose:
                //EventsManager.Instance.Publish(new GameStatePayload { State = State });
                break;
            default:
                break;
        }

        EventsManager.Instance.Publish(new GameStatePayload { State = State });
    }

    private IEnumerator startCountdown()
    {
        this._currentCountdown = SecondsBeforeStart;
        EventsManager.Instance.Publish(new CountdownPayload { CommandType = CountdownPayload.CountdownCommandType.Show, Value = this._currentCountdown });

        while (this._currentCountdown > 0)
        {
            yield return new WaitForSeconds(1f);
            this._currentCountdown--;

            EventsManager.Instance.Publish(new CountdownPayload { CommandType = CountdownPayload.CountdownCommandType.Update, Value = this._currentCountdown });
        }

        EventsManager.Instance.Publish(new CountdownPayload { CommandType = CountdownPayload.CountdownCommandType.Hide, Value = this._currentCountdown });
        State = GameState.Started;
    }

    private void Awake()
    {
        PlayerPrefs.SetString("Language", "english");
        Instance = this;
    }

    private void Update()
    {
        if (State == GameState.Waiting && Input.GetKeyUp(StartGameKey))
        {
            State = GameState.Countdown;
        }

        this.cheat();
    }

    private float _lastCheatEnterTime = -1;
    private int _lastCharEntered = 0;
    private readonly KeyCode[] _cheat = new KeyCode[] { KeyCode.Keypad6, KeyCode.Keypad6, KeyCode.Keypad6, KeyCode.Keypad1, KeyCode.Keypad6, KeyCode.Keypad6, KeyCode.Keypad6 };

    private void cheat()
    {
        if(Time.time - this._lastCheatEnterTime >= 0.6f)
        {
            this._lastCharEntered = 0;
        }

        if (Input.GetKeyUp(this._cheat[this._lastCharEntered]))
        {
            this._lastCheatEnterTime = Time.time;
            this._lastCharEntered++;
        }

        if(this._lastCharEntered == this._cheat.Length)
        {
            SceneManager.LoadScene((int)Scenes.Win);
        }
    }
}
