using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [SerializeField] private Text congratulation;
    [SerializeField] private Image fade;
    private Animation fader;

    private string winer;
    private string playerOneName;
    private string playerTwoName;

    private List<Cell> whiteWinCells = new List<Cell>();
    private List<Cell> blackWinCells = new List<Cell>();

    public static EndGame S;
    private void Awake()
    {
        if (S == null)
            S = this;

        fader = fade.GetComponent<Animation>();
    }

    private void Start()
    {
        playerOneName = PlayerPrefs.GetString("playerOneName", "Игрок1");
        playerTwoName = PlayerPrefs.GetString("playerTwoName", "Игрок2");
    }

    public List<Cell> GetWinCells(bool white) => white ? whiteWinCells : blackWinCells;

    public void AddWinCells(bool forWhite, Cell cell)
    {
        if (forWhite)
            whiteWinCells.Add(cell);
        else
            blackWinCells.Add(cell);
    }

    public void CheckingEndGame()
    {
        if (CheckFullnessCells(whiteWinCells, true))
            WhiteWins(true);
        else if (CheckFullnessCells(blackWinCells, false))
            WhiteWins(false);
    }

    private bool CheckFullnessCells(List<Cell> cells, bool white)
    {
        bool fullness = true;
        foreach (Cell cell in cells)
        {
            if (!cell.ChackingChessmanColor(white))
            {
                fullness = false;
                break;
            }
        }

        return fullness;
    }

    private void WhiteWins(bool win)
    {
        winer = win ? playerOneName : playerTwoName;
        fader.Play("FadeOut");
        StartCoroutine(WinPanel(win));
    }

    private IEnumerator WinPanel(bool win)
    {
        fade.GetComponent<CanvasGroup>().blocksRaycasts = true;
        yield return new WaitUntil(() => fade.color.a == 1);
        congratulation.gameObject.SetActive(true);

        congratulation.text = winer + " победил!";
    }
}
