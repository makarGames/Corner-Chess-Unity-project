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
                GetComponent<Image>().color = Color.red;
            else
                GetComponent<Image>().color = Color.black;
        }
    }

    public void ChangeCellColor(bool activation)
    {
        cell.NeighborsChangeColor(activation);
    }

    public bool CheckingOnNeighbor(Cell c)
    {
        return cell.NeighborContains(c);
    }

    public void ChangeCell(Cell c)
    {
        GetComponent<DragDrop>().isDropped = true;

        if (cell != null)
            cell.isEmpty = true;

        cell = c;
        cell.isEmpty = false;
        transform.position = cell.transform.position;
    }
}
