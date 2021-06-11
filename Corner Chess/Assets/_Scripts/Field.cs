using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Field : MonoBehaviour
{
    private int size;

    [SerializeField] private GameObject table;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private Chessman chessmanPrefab;

    private List<List<Cell>> chessboard = new List<List<Cell>>();

    private void Awake()
    {
        size = PlayerPrefs.GetInt("fieldSize", 8);
        FieldInit();
    }

    private void FieldInit()
    {
        Vector2 offset = new Vector2(-size / 2f + 0.5f, size / 2f - 0.5f);

        for (int i = 0; i < size; i++)
        {
            List<Cell> row = new List<Cell>();
            for (int j = 0; j < size; j++)
            {

                Cell tempCell = Instantiate(cellPrefab);

                tempCell.name = "Cell[" + i + "]" + "[" + j + "]";

                row.Add(tempCell);
                tempCell.transform.SetParent(transform, false);
                tempCell.transform.localPosition = new Vector3(j + offset.x, -i + offset.y, 0);

                if ((i + j) % 2 == 0)
                    tempCell.GetComponent<Image>().color = ColorStorage.blackCell;
                else
                    tempCell.GetComponent<Image>().color = ColorStorage.whiteCell;

                tempCell.isEmpty = true;

                if (i < (size - 1) / 2 && j < (size - 1) / 2 || i > size / 2 && j > size / 2)
                {
                    bool white = i < (size - 1) / 2;
                    Chessman chessman = Instantiate(chessmanPrefab);

                    chessman.transform.SetParent(table.transform, false);
                    chessman.white = white;
                    chessman.ChangeCell(tempCell);

                    EndGame.S.AddWinCells(!white, tempCell);
                    AI.S.AddAIChessmans(chessman);
                }

                GeneratePossibleMoves(j, i, tempCell, row, chessboard);
            }
            chessboard.Add(row);
        }
    }

    private void GeneratePossibleMoves(int j, int i, Cell cell, List<Cell> row, List<List<Cell>> chessboard)
    {
        if (j > 0)
        {
            Cell.SetWestEastNeighbors(row[j - 1], cell);
        }
        if (i > 0)
        {
            Cell.SetNorthSouthNeighbors(chessboard[i - 1][j], cell);
            if (j < size - 1)
            {
                Cell.SetNorthEastSouthWestNeighbors(chessboard[i - 1][j + 1], cell);
            }
        }
        if (i > 0 && j > 0)
        {
            Cell.SetNorthWestSouthEastNeighbors(chessboard[i - 1][j - 1], cell);
        }
    }
}
