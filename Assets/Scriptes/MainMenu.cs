using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject panelSlider;
    [SerializeField] private Slider _slider;
    private AsyncOperation _async;
    private void Start()
    {
        PlayerPrefs.SetInt("SettingGame",0);
    }

    public void SceneGame(int i)
    {
        PlayerPrefs.SetInt("SettingGame",i);
        StartCoroutine(slider());
    }

    IEnumerator slider()
    {
        panelSlider.SetActive(true);
        _async = SceneManager.LoadSceneAsync(1);
        _async.allowSceneActivation = false;
        
        while (_async.isDone == false)
        {
            _slider.value = _async.progress;
            if (_async.progress == 0.9f)
            {
                _slider.value = 1f;
                _async.allowSceneActivation = true;
            }

            yield return null;
        }
        
    }

    public void Exit()
    {
        Application.Quit();
    }
}
