using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    private Cell northWest, north, west, northEast, southWest, east, south, southEast;
    private List<Cell> neighbors;

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
        neighbors = new List<Cell>() { northWest, north, west, northEast, southWest, east, south, southEast };
    }

    public List<Cell> GetNeighbors()
    {
        return neighbors;
    }

    public void NeighborsChangeColor(bool activation)
    {
        foreach (Cell c in neighbors)
        {
            if (c != null && c.isEmpty)
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


    public static void SetWestEastNeighbors(Cell westCell, Cell eastCell)
    {
        westCell.east = eastCell;
        eastCell.west = westCell;
    }
    public static void SetNorthSouthNeighbors(Cell northCell, Cell southCell)
    {
        northCell.south = southCell;
        southCell.north = northCell;
    }
    public static void SetNorthWestSouthEastNeighbors(Cell northWestCell, Cell southEastCell)
    {
        northWestCell.southEast = southEastCell;
        southEastCell.northWest = northWestCell;
    }
    public static void SetNorthEastSouthWestNeighbors(Cell northEastCell, Cell southWestCell)
    {
        northEastCell.southWest = southWestCell;
        southWestCell.northEast = northEastCell;
    }


}
