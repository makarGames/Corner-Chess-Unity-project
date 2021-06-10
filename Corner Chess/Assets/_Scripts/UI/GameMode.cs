using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{
    [SerializeField] private Button soloGameButton;
    [SerializeField] private Button twoPlayersButton;

    [SerializeField] private Text inputFieldPlayerOne;
    [SerializeField] private GameObject inputFieldPlayerTwo;

    private bool _soloGame;
    private bool soloGame
    {
        get => _soloGame;
        set
        {
            _soloGame = value;

            soloGameButton.interactable = !value;
            twoPlayersButton.interactable = value;

            ColorChooser.S.GameModeChanging(value);

            inputFieldPlayerOne.text = value ? "Введите своё имя..." : "Введите имя Игрок1";

            inputFieldPlayerTwo.SetActive(!value);

            PlayerPrefs.SetInt("GameMode", value ? 1 : 0);

        }
    }

    private void Start()
    {
        soloGame = false;
        soloGameButton.onClick.AddListener(() => soloGame = true);
        twoPlayersButton.onClick.AddListener(() => soloGame = false);
    }
}
