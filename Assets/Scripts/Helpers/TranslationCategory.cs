using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class TranslationCategory
{
    public string Name;
    public TranslationText[] Texts;
}

[Serializable]
public class TranslationText
{
    public string Key;
    public string Value;
}
