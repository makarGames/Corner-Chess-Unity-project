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

    private List<Chessman> whiteChessmans = new List<Chessman>();
    private List<Chessman> blackChessmans = new List<Chessman>();

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

    public void AddChessmans(bool forWhite, Chessman chessman)
    {
        if (forWhite)
            whiteChessmans.Add(chessman);
        else
            blackChessmans.Add(chessman);
    }

    public void AddWinCells(bool forWhite, Cell cell)
    {
        if (forWhite)
            whiteWinCells.Add(cell);
        else
            blackWinCells.Add(cell);
    }

    public void CheckingEndGame()
    {
        if (CheckFullnessCells(whiteWinCells, whiteChessmans))
            WhiteWins(true);
        else if (CheckFullnessCells(blackWinCells, blackChessmans))
            WhiteWins(false);
    }

    private bool CheckFullnessCells(List<Cell> cells, List<Chessman> chessmans)
    {
        bool fullness = true;
        foreach (Cell cell in cells)
        {
            if (!chessmans.Contains(cell.chessman))
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
