using UnityEngine;
using UnityEngine.UI;

public class Naming : MonoBehaviour
{
    [SerializeField] private InputField playerOne;
    [SerializeField] private InputField playerTwo;

    private string _playerOneName;
    private string playerOneName
    {
        get => _playerOneName;
        set
        {
            _playerOneName = value;
            PlayerPrefs.SetString("playerOneName", value);
        }
    }

    private string _playerTwoName;
    private string playerTwoName
    {
        get => playerTwoName;
        set
        {
            _playerTwoName = value;
            PlayerPrefs.SetString("playerTwoName", value);
        }
    }


    private void Start()
    {
        playerOneName = "Игрок1";
        playerTwoName = "Игрок2";

        playerOne.onValueChanged.AddListener((v) => playerOneName = v);
        playerTwo.onValueChanged.AddListener((v) => playerTwoName = v);
    }
}
