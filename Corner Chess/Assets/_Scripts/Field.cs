using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode
{
    kingLike,
    straightJumps,
    checkersLike
}

public class Field : MonoBehaviour
{
    private readonly int size = 8;

    [SerializeField] private Canvas canvas;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private Chessman chessmanPrefab;

    private GameMode gameMode;

    private List<List<Cell>> chessboard = new List<List<Cell>>();

    private void Awake()
    {
        FieldInit();
        gameMode = GameMode.straightJumps;
        /* GameModeFromString(PlayerPrefs.GetString("gameModeName", "straightJumps")); */
    }

    private GameMode GameModeFromString(string gameModeName)
    {
        switch (gameModeName)
        {
            case "kingLike":
                return GameMode.kingLike;
            case "straightJumps":
                return GameMode.straightJumps;
            case "checkersLike":
                return GameMode.checkersLike;
        }
        Debug.Log("ERROR! Default  is kingLike game mode");
        return GameMode.kingLike;
    }

    private void FieldInit()
    {
        Vector2 offset = new Vector2(-size / 2 + 0.5f, size / 2 - 0.5f);

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
                    Chessman chessman = Instantiate(chessmanPrefab);
                    chessman.transform.SetParent(canvas.transform, false);
                    chessman.white = i < (size - 1) / 2;
                    EndGame.S.AddWinCells(i > (size - 1) / 2, tempCell);
                    chessman.ChangeCell(tempCell);
                }

                GeneratePossibleMoves(GameMode.straightJumps, j, i, tempCell, row, chessboard);
            }
            chessboard.Add(row);
        }
    }

    private void GeneratePossibleMoves(GameMode mode, int j, int i, Cell cell, List<Cell> row, List<List<Cell>> chessboard)
    {
        switch (mode)
        {
            case GameMode.kingLike:
                {
                    if (j > 0)
                        cell.AddNeighbor(row[j - 1]);
                    if (i > 0)
                    {
                        cell.AddNeighbor(chessboard[i - 1][j]);
                        if (j < size - 1)
                            cell.AddNeighbor(chessboard[i - 1][j + 1]);
                    }
                    if (i > 0 && j > 0)
                        cell.AddNeighbor(chessboard[i - 1][j - 1]);
                    return;
                }
            case GameMode.straightJumps:
                {
                    if (j > 1)
                        cell.AddNeighbor(row[j - 2]);
                    if (i > 1)
                        cell.AddNeighbor(chessboard[i - 2][j]);
                    return;
                }
            case GameMode.checkersLike:
                {
                    if (j > 1 && i > 1)
                        cell.AddNeighbor(chessboard[i - 2][j - 2]);
                    if (i > 1 && j + 2 < chessboard[0].Count)
                        cell.AddNeighbor(chessboard[i - 2][j + 2]);
                    return;
                }
        }
    }
}
