using migs.EventSystem;
using System.Collections;
using UnityEngine;

public class AgentEnterence : BaseBehaviour
{
    [SerializeField] private Vector3 _startingPosition;
    [SerializeField] private Vector3 _destination;
    
    
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
        // create movement from starting position to destination in 6 seconds
        var time = 0f;
        
        while (time < 6)
        {
            time += Time.deltaTime;
            this.transform.position = Vector3.Lerp(this._startingPosition, this._destination, time / 6);
            yield return null;
        }
    }
}
