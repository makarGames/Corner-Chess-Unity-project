using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Chessman : MonoBehaviour
{
    private Cell cell;

    private bool _white;
    public bool white
    {
        get => _white;
        set
        {
            _white = value;
            if (value)
            {
                GetComponent<Image>().color = ColorStorage.whiteChessman;
                GetComponent<RectTransform>().rotation = Quaternion.Euler(0f, 0f, -180f);
            }
            else
            {
                GetComponent<Image>().color = ColorStorage.blackChessman;
            }
        }
    }

    public void ChangeCellColor(bool activation)
    {
        cell.NeighborsChangeColor(activation);
    }

    public List<Cell> GetNeighborCells()
    {
        return cell.GetNeighbors();
    }

    public void ChangeCell(Cell c)
    {
        GetComponent<DragDrop>().isDropped = true;

        if (cell != null)
        {
            cell.isEmpty = true;
            cell.chessman = null;
            ChangeCellColor(false);
        }

        cell = c;
        cell.isEmpty = false;
        cell.chessman = this;
        transform.position = cell.transform.position;
    }

    public void MoveTo(Cell newCell)
    {
        this.ChangeCell(newCell);
        EndGame.S.CheckingEndGame();
        OrderOfSteps.S.whiteMoves = !OrderOfSteps.S.whiteMoves;
    }
}
