using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    private Chessman chessman;
    private List<Cell> neighbors = new List<Cell>();
    private Color startColor;

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
                GetComponent<Image>().color = Color.black;
            else
                GetComponent<Image>().color = startColor;
    }
}
