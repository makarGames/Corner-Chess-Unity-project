using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OrderOfSteps : MonoBehaviour
{
    [SerializeField] private Text OrderOfStepsText;

    private bool soloGame;

    private string playerOneName;
    private string playerTwoName;

    private bool whiteChoosed;

    private bool _whiteMoves;
    public bool whiteMoves
    {
        get => _whiteMoves;
        set
        {
            _whiteMoves = value;

            OrderOfStepsText.text = value ? ("БЕЛЫЕ (" + playerOneName + ")") : ("ЧЁРНЫЕ (" + playerTwoName + ")");
            OrderOfStepsText.color = value ? ColorStorage.whiteChessman : ColorStorage.blackChessman;

            /*  Этот блок кода делает то же самое, что и 2 строчки выше,
                но я не уверен какой код будет читабильнее. Тот что вверху выглядит компактно и локанично, но тот что ниже кажется понятнее
            if (value)
            {
                OrderOfStepsText.text = "БЕЛЫЕ (" + playerOneName + ")";
                OrderOfStepsText.color = ColorStorage.whiteChessman;
            }
            else
            {
                OrderOfStepsText.text = "ЧЁРНЫЕ (" + playerTwoName + ")";
                OrderOfStepsText.color = ColorStorage.blackChessman;
            } */

            if (soloGame && value == !whiteChoosed)
                AI.S.Step();
        }
    }

    public static OrderOfSteps S;

    private void Awake()
    {
        if (S == null)
            S = this;

        soloGame = PlayerPrefs.GetInt("GameMode", 1) == 1;
        whiteChoosed = PlayerPrefs.GetInt("white", 1) == 1;

        if (soloGame)
            if (whiteChoosed)
            {
                playerOneName = PlayerPrefs.GetString("playerOneName", "Игрок1");
                PlayerPrefs.SetString("playerTwoName", "Компьютер");
            }
            else
            {
                playerTwoName = PlayerPrefs.GetString("playerOneName", "Игрок1");
                PlayerPrefs.SetString("playerTwoName", playerTwoName);
                PlayerPrefs.SetString("playerOneName", "Компьютер");
            }
        else
        {
            playerOneName = PlayerPrefs.GetString("playerOneName", "Игрок1");
            playerTwoName = PlayerPrefs.GetString("playerTwoName", "Игрок2");
        }
    }

    private void Start()
    {
        StartCoroutine(FirstStep());
    }

    private IEnumerator FirstStep()
    {
        yield return new WaitForEndOfFrame();
        whiteMoves = true;
    }
}
