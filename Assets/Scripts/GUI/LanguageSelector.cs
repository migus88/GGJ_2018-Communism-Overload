using System.Collections;
using System.Collections.Generic;
using migs.EventSystem;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LanguageSelector : MonoBehaviour
{
    public Sprite EnglishSprite;
    public Sprite RussianSprite;

    private Image _image;

    private void Awake()
    {
        this._image = GetComponent<Image>();
        SetImage();
    }

    public void SetImage()
    {
        var currentLanguage = PlayerPrefs.GetString("Language", "english");

        if (currentLanguage == "english")
        {
            this._image.sprite = RussianSprite;
        }
        else if (currentLanguage == "russian")
        {
            this._image.sprite = EnglishSprite;
        }
    }

    public void ChangeLanguage()
    {
        var currentLanguage = PlayerPrefs.GetString("Language", "english");

        if (currentLanguage == "english")
        {
            TranslationManager.Instance.SetLanguage("russian");
        }
        else if (currentLanguage == "russian")
        {
            TranslationManager.Instance.SetLanguage("english");
        }

        SetImage();
        EventsManager.Instance.Publish(new TranslationPayload());
    }
}
