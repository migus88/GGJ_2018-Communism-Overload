using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using migs.EventSystem;

[RequireComponent(typeof(Slider))]
public class VirusUploadSlider : BaseBehaviour
{
    private Slider _slider;

    private void Awake()
    {
        this._slider = GetComponent<Slider>();
        this._typeEvents.Add(typeof(VirusProgressPayload), EventsManager.Instance.Subscribe<VirusProgressPayload>(this.onVirusProgress));
    }

    private void onVirusProgress(VirusProgressPayload obj)
    {
        this._slider.value = obj.Progress;
    }
}