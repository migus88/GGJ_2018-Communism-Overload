using System.Collections;
using System.Collections.Generic;
using migs.EventSystem;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TranslationSupport : BaseBehaviour
{
    public string Category;
    public string Key;

    private Text _txt;

    private void Awake()
    {
        this._txt = GetComponent<Text>();
        this._typeEvents.Add(typeof(TranslationPayload), EventsManager.Instance.Subscribe<TranslationPayload>(o => { Translate(); }));
    }

    // Use this for initialization
    void Start()
    {
        Translate();
    }

    public void Translate()
    {
        var translation = TranslationManager.Instance.GetText(Category, Key);
        this._txt.text = translation == string.Empty ? this._txt.text : translation;
    }
}
