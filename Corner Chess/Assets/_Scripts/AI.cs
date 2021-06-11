using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    private List<Chessman> chessmans = new List<Chessman>();    //фишки, которыми будит играть компьютер
    private List<Cell> targetCells = new List<Cell>();

    private bool whiteChoosed;  //цвет который выбрал для игры человек

    public static AI S;

    private void Awake()
    {
        if (S == null)
            S = this;

        whiteChoosed = PlayerPrefs.GetInt("white", 1) == 1;
    }

    private void Start()
    {
        targetCells = new List<Cell>(EndGame.S.GetWinCells(!whiteChoosed));
        if (!whiteChoosed)
        {
            targetCells.Reverse();
            chessmans.Reverse();
        }
    }

    public void AddAIChessmans(Chessman c)
    {
        if (c.white == !whiteChoosed)
            chessmans.Add(c);
    }

    public void Step()
    {
        List<Chessman> tempChessmansStack = new List<Chessman>(chessmans);

        while (tempChessmansStack.Count > 0)
        {
            int chessmanIndex = Random.Range(0, tempChessmansStack.Count);

            Chessman movingChessman = tempChessmansStack[chessmanIndex];

            Cell cellForStep = GetBestStep(movingChessman.GetNeighborCells());
            if (cellForStep != null)
            {
                movingChessman.transform.position = cellForStep.transform.position;
                movingChessman.MoveTo(cellForStep);

                if (targetCells[0].ChackingChessmanColor(!whiteChoosed))
                {
                    targetCells.RemoveAt(0);
                    chessmans.Remove(movingChessman);
                }
                break;
            }

            tempChessmansStack.Remove(movingChessman);

            if (tempChessmansStack.Count == 0)
            {
                Debug.Log("No free steps for AI!");
                OrderOfSteps.S.whiteMoves = !OrderOfSteps.S.whiteMoves;
            }
        }
    }

    private Cell GetBestStep(List<Cell> cellsForStep)
    {
        List<Cell> cells = new List<Cell>(cellsForStep);
        if (!whiteChoosed)
            cells.Reverse();

        int number = cells.Count;
        Cell bestCellStep = null;

        for (int i = 0; i < number; i++)
        {
            if (cells[i] != null && cells[i].isEmpty)
            {
                if (i == 0 || i == number - 1)
                    bestCellStep = cells[i];
                else if (cells[i + 1] != null && cells[i + 1].isEmpty)
                    bestCellStep = cells[Random.Range(i, i + 2)];
                else
                    bestCellStep = cells[i];
                break;
            }
        }
        return bestCellStep;
    }
}
