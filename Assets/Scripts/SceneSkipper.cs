using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSkipper : MonoBehaviour
{
    public Scenes SceneID = Scenes.Game;
    public KeyCode SkipButton = KeyCode.Space;

    private bool _isNeedToStop = false;
    private Text _text;

    private void Start()
    {
        this._text = GetComponent<Text>();

        if (this._text != null)
            StartCoroutine(this.fade());
    }

    private IEnumerator fade()
    {
        this._text.text = string.Format(TranslationManager.Instance.GetText("Global", "PressKeyToSkip"), SkipButton);// $"Press {SkipButton} to skip...";
        bool isFadingIn = false;

        while (!this._isNeedToStop)
        {
            if (isFadingIn)
            {
                this._text.color = new Color(this._text.color.r, this._text.color.g, this._text.color.b, this._text.color.a + 1f * Time.deltaTime);
                if (this._text.color.a >= 1f)
                    isFadingIn = false;
            }
            else
            {
                this._text.color = new Color(this._text.color.r, this._text.color.g, this._text.color.b, this._text.color.a - 1f * Time.deltaTime);
                if (this._text.color.a <= 0f)
                    isFadingIn = true;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private void OnDestroy()
    {
        this._isNeedToStop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(SkipButton))
        {
            SceneManager.LoadScene((int)SceneID);
        }
    }
}
