using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public List<Chessman> chessmans = new List<Chessman>();
    public List<Cell> targetCells = new List<Cell>();

    public static AI S;

    private void Awake()
    {
        if (S == null)
            S = this;
    }

    private void Start()
    {
        targetCells = new List<Cell>(EndGame.S.GetWhiteWinCells());
    }

    public void Step()
    {
        List<Chessman> tempChessmansStack = new List<Chessman>(chessmans);

        while (tempChessmansStack.Count > 0)
        {
            int chessmanIndex = Random.Range(0, tempChessmansStack.Count);

            Chessman movingChessmans = tempChessmansStack[chessmanIndex];
            Cell cellForStep = movingChessmans.GetFreeCellsForStep();

            if (cellForStep != null)
            {
                OrderOfSteps.S.whiteMoves = !OrderOfSteps.S.whiteMoves;
                movingChessmans.transform.position = cellForStep.transform.position;
                movingChessmans.ChangeCell(cellForStep);
                EndGame.S.CheckingEndGame();

                /* if (targetCells[targetCells.Count - 1] == cellForStep)
                {
                    print("LOL");
                    targetCells.Remove(cellForStep);
                    chessmans.Remove(movingChessmans);
                } */
                break;
            }
            tempChessmansStack.Remove(movingChessmans);
            if (tempChessmansStack.Count == 0)
            {
                Debug.Log("No free steps for AI!");
                OrderOfSteps.S.whiteMoves = !OrderOfSteps.S.whiteMoves;
            }
        }
    }
}
