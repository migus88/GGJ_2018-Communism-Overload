using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public GameObject Credits;

    private AudioSource _source;
    private GameObject _inst;
    private void Start()
    {
        this._source = GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        SceneManager.LoadScene((int)Scenes.Intro);
    }

    public void ShowCredits()
    {
       this._inst = Instantiate(Credits, transform.parent);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void PlaySound()
    {
        this._source.time = 0;
        this._source.Play();
    }

}
