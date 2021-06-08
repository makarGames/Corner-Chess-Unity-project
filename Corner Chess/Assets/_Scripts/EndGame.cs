using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private List<Cell> whiteWinCells = new List<Cell>();
    private List<Cell> blackWinCells = new List<Cell>();

    public static EndGame S;
    private void Awake()
    {
        if (S == null)
            S = this;
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
        int cellCounter = 0;

        foreach (Cell c in whiteWinCells)
            if (c.chessman == null || !c.chessman.white)
            {
                cellCounter = 0;
                break;
            }
            else
                cellCounter++;

        if (cellCounter == whiteWinCells.Count)
        {
            WhiteWins(true);
            return;
        }

        foreach (Cell c in blackWinCells)
            if (c.chessman == null || c.chessman.white)
            {
                cellCounter = 0;
                break;
            }
            else
                cellCounter++;
        if (cellCounter == blackWinCells.Count)
            WhiteWins(false);
    }

    private void WhiteWins(bool win)
    {
        if (win)
            print("WHITE WINS!");
        else
            print("BLACK WINS!");
    }
}
