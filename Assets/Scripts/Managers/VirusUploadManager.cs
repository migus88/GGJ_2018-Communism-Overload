using migs.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VirusUploadManager : BaseBehaviour
{
    public PlayerDistancePayload.PlayersState PlayerState = PlayerDistancePayload.PlayersState.FarAway;
    [Range(0, 0.2f)]
    public float UploadSpeed = 0.1f;
    [Range(0, 1f)]
    public float CurrentProgress = 0;
    public float MaxTimeFarAway = 3f;
    public float MaximumDistanceBetweenPlayers = 3f;
    public PlayerController Player1;
    public PlayerController Player2;


    private float _farAwayFrom = -1;

    private void Awake()
    {
        this._typeEvents.Add(typeof(GameStatePayload), EventsManager.Instance.Subscribe<GameStatePayload>(this.onGameStateChanged));
    }

    private void onGameStateChanged(GameStatePayload obj)
    {
        if(obj.State == GameState.Lose || obj.State == GameState.Win)
        {
            PlayerPrefs.SetFloat("Progress", CurrentProgress);
        }
    }

    private void Update()
    {
        if (GameManager.Instance.State == GameState.Started)
        {
            if (Mathf.Abs(Player1.transform.position.x - Player2.transform.position.x) > MaximumDistanceBetweenPlayers)
            {
                PlayerState = PlayerDistancePayload.PlayersState.FarAway;
            }
            else
            {
                PlayerState = PlayerDistancePayload.PlayersState.InRange;
            }
        }

        if (PlayerState == PlayerDistancePayload.PlayersState.InRange)
        {
            this._farAwayFrom = -1;
            CurrentProgress += UploadSpeed * Time.deltaTime;
            CurrentProgress = CurrentProgress > 1f ? 1f : CurrentProgress;
        }
        else if (PlayerState == PlayerDistancePayload.PlayersState.FarAway)
        {
            if (this._farAwayFrom == -1)
                this._farAwayFrom = Time.time;

            if (Time.time - this._farAwayFrom >= MaxTimeFarAway)
            {
                CurrentProgress = 0;
            }
        }

        EventsManager.Instance.Publish(new VirusProgressPayload { CommandType = VirusProgressPayload.UploadCommandType.Progress, Progress = CurrentProgress });
        EventsManager.Instance.Publish(new PlayerDistancePayload { State = PlayerState });

        if(CurrentProgress == 1f)
        {
            GameManager.Instance.State = GameState.Win;
        }
    }

    /*private void onPlayerDistanceState(PlayerDistancePayload obj)
    {
        PlayerState = obj.State;
    }*/
}