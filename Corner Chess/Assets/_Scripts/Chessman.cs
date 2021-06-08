using UnityEngine;
using UnityEngine.UI;

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
                AI.S.chessmans.Add(this);
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
    //------------------
    public Cell GetFreeCellsForStep()
    {
        for (int i = cell.neighborsNumber - 1; i >= 0; i--)
            if (cell.GetNeighbor(i).isEmpty)
                return cell.GetNeighbor(i);

        return null;
    }
    //------------------------------------
    public bool CheckingOnNeighbor(Cell c)
    {
        return cell.NeighborContains(c);
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
}
