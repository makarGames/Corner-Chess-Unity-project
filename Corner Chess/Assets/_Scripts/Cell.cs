using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    private List<Cell> neighbors = new List<Cell>();
    private Color startColor;

    public int neighborsNumber
    {
        get => neighbors.Count;
    }

    public Chessman chessman;
    public bool isEmpty { get; set; }

    private void Start()
    {
        startColor = GetComponent<Image>().color;
    }

    public void AddNeighbor(Cell cell)
    {
        if (!neighbors.Contains(cell))
        {
            neighbors.Add(cell);
            cell.AddNeighbor(this);
        }
    }
    //------------------------------------
    public Cell GetNeighbor(int index)
    {
        return neighbors[index];
    }
    //-----------------------------------
    public bool NeighborContains(Cell cell)
    {
        return neighbors.Contains(cell);
    }

    public void NeighborsChangeColor(bool activation)
    {
        foreach (Cell c in neighbors)
        {
            c.Colorize(activation);
        }
    }

    private void Colorize(bool drag)
    {
        if (isEmpty)
            if (drag)
                GetComponent<Image>().color -= ColorStorage.freeForStepOffeset;
            else
                GetComponent<Image>().color = startColor;
    }
}
