using migs.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Text))]
public class CountdownLabel : BaseBehaviour
{

    private Text _text;

    private void Awake()
    {
        this._text = GetComponent<Text>();

        this._typeEvents.Add(typeof(CountdownPayload), EventsManager.Instance.Subscribe<CountdownPayload>(this.onCountdownChange));

        gameObject.SetActive(false);
    }

    private void onCountdownChange(CountdownPayload obj)
    {
        if (obj.CommandType == CountdownPayload.CountdownCommandType.Show && !gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            this._text.text = obj.Value.ToString();
        }
        else if (obj.CommandType == CountdownPayload.CountdownCommandType.Hide && gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else if (obj.CommandType == CountdownPayload.CountdownCommandType.Update && gameObject.activeSelf)
        {
            this._text.text = obj.Value.ToString();
        }
    }
}