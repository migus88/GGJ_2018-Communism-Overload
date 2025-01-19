using migs.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class VirusSliderElement : BaseBehaviour
{
    public RectTransform LoadingSlider;
    public int LoadingCellsAmount = 20;
    public GameObject CellPrefab;
    public Text InterruptedText;

    private void Awake()
    {
        this._typeEvents.Add(typeof(VirusProgressPayload), EventsManager.Instance.Subscribe<VirusProgressPayload>(this.onVirusProgress));
    }

    private void onVirusProgress(VirusProgressPayload obj)
    {
        if (obj.CommandType != VirusProgressPayload.UploadCommandType.Progress)
            return;

        var displayedCells = Mathf.FloorToInt(LoadingCellsAmount * obj.Progress);

        if(LoadingSlider.childCount > 0 && displayedCells == 0)
        {
            StartCoroutine(this.transmissionInterrupted());
        }

        if (LoadingSlider.childCount > displayedCells)
        {
            for (int i = 0; i < LoadingSlider.childCount; i++)
            {
                Destroy(LoadingSlider.GetChild(i).gameObject); //TODO: rework this shit
            }
        }

        if (LoadingSlider.childCount < displayedCells)
        {
            while (LoadingSlider.childCount < displayedCells)
            {
                Instantiate(CellPrefab, LoadingSlider);
            }
        }
    }

    private IEnumerator transmissionInterrupted()
    {
        InterruptedText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        
        InterruptedText.gameObject.SetActive(false);
    }
}
