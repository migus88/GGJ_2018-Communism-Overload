using migs.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PageManager : BaseBehaviour
{

    // Use this for initialization
    void Start()
    {
        this._typeEvents.Add(typeof(GameStatePayload), EventsManager.Instance.Subscribe<GameStatePayload>(this.onGameStateChanged));
    }

    private void onGameStateChanged(GameStatePayload obj)
    {
        if(obj.State == GameState.Win)
        {
            StartCoroutine(this.loadScene(Scenes.Win));
        }
        else if(obj.State == GameState.Lose)
        {
            StartCoroutine(this.loadScene(Scenes.Lose));
        }
    }

    private IEnumerator loadScene(Scenes index)
    {
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene((int)index);
    }
}
