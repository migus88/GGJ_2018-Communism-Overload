﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TranslationManager
{
    public static TranslationManager Instance { get; } = new TranslationManager();

    private TranslationManager()
    {
        this.loadLanguage();
    }

    public TranslationLanguage Language { get; set; }

    public void SetLanguage(string lng)
    {
        PlayerPrefs.SetString("Language", lng);
        this.loadLanguage();
    }

    private void loadLanguage()
    {
        var lng = PlayerPrefs.GetString("Language", "english");

        var txt = Resources.Load<TextAsset>($"Translations/{lng}");

        if (txt == null)
        {
            Debug.Log("Can't find a translation");
            return;
        }

        Language = JsonUtility.FromJson<TranslationLanguage>(txt.text);
    }

    public string GetText(string category, string key)
    {
        var cat = Language.Categories.FirstOrDefault(c => c.Name == category);

        if (cat == null)
            return string.Empty;

        var txt = cat.Texts.FirstOrDefault(t => t.Key == key);

        return txt == null ? string.Empty : txt.Value;
    }
}
