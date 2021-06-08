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

    public void Step()
    {
        print("lol");
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
