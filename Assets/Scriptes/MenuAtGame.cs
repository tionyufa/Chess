using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuAtGame : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Button _button_1;
    [SerializeField] private TextMeshProUGUI _button_1_text;
    [SerializeField] private Button _button_2;
    [SerializeField] private TextMeshProUGUI _button_2_text;
    [SerializeField] private Button _button_3;
    [SerializeField] private TextMeshProUGUI _button_3_text;
    [SerializeField] private TextMeshProUGUI _text;


    public void ReplayGame()
    {
        GameSettingReplay();
    }

    private void GameSettingReplay()
    {
        _button_2.gameObject.SetActive(true); // включаем если вызывается в главное меню
        _text.text = "Выберите новый режим игры";
        _button_1_text.text = "Шаги в любую сторону";
        _button_1.onClick.AddListener(() => PlayerPrefSetting(0));
        _button_2_text.text = "Шаги по вертикали и горизонтали";
        _button_2.onClick.AddListener(() => PlayerPrefSetting(1));
        _button_3_text.text = "Шаги по диагонали";
        _button_3.onClick.AddListener(() => PlayerPrefSetting(2));
        panel.SetActive(true);
        
    }
    private void PlayerPrefSetting(int i)
    {
        PlayerPrefs.SetInt("SettingGame",i);
        SceneManager.LoadSceneAsync(1);
    }
    
    public void MainMenu()
    {
        Menu();
    }

    private void Menu()
    {
        _button_2.gameObject.SetActive(false);
        _text.text = "Вы действительно хотите выйти в главное меню?";
        _button_1_text.text = "Да";
        _button_1.onClick.AddListener(() => SceneManager.LoadSceneAsync(0));
        _button_3_text.text = "Нет";
        _button_3.onClick.AddListener(() => panel.SetActive(false));
        panel.SetActive(true);
    }

    
    
}
