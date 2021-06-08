using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Field : MonoBehaviour
{
    private readonly int width = 8, height = 8;

    [SerializeField] private Canvas canvas;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private Chessman chessmanPrefab;

    private List<List<Cell>> chessboard = new List<List<Cell>>();

    private void Awake()
    {
        FieldInit();

        //chessboard[3][3].colorize();
    }

    private void FieldInit()
    {
        Vector2 offset = new Vector2(-height / 2 + 0.5f, width / 2 - 0.5f);

        for (int i = 0; i < height; i++)
        {
            List<Cell> row = new List<Cell>();
            for (int j = 0; j < width; j++)
            {

                Cell tempCell = Instantiate(cellPrefab);
                tempCell.name = "Cell[" + i + "]" + "[" + j + "]";
                row.Add(tempCell);
                tempCell.transform.SetParent(transform, false);

                tempCell.transform.localPosition = new Vector3(j + offset.x, -i + offset.y, 0);

                if ((i + j) % 2 == 0)
                    tempCell.GetComponent<Image>().color = Color.grey;

                tempCell.isEmpty = true;



                //------------------------------------------
                if (i < 3 && j < 3 || i > 4 && j > 4)
                {
                    Chessman chessman = Instantiate(chessmanPrefab);
                    chessman.transform.SetParent(canvas.transform, false);
                    chessman.white = i < 3;
                    chessman.ChangeCell(tempCell);
                }
                //--------------------------
                if (j > 0)
                    tempCell.AddNeighbor(row[j - 1]);
                if (i > 0)
                {
                    tempCell.AddNeighbor(chessboard[i - 1][j]);
                    if (j < width - 1) tempCell.AddNeighbor(chessboard[i - 1][j + 1]);
                }
                if (i > 0 && j > 0)
                    tempCell.AddNeighbor(chessboard[i - 1][j - 1]);
            }
            chessboard.Add(row);
        }
    }

}
