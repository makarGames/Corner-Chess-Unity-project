using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Cell : MonoBehaviour
{
    private Cell northWest, north, west, northEast, southWest, east, south, southEast;
    private List<Cell> neighbors;

    private Image _image;
    private Color startColor;

    private Chessman chessman;
    public bool isEmpty { get; set; }

    private void Start()
    {
        _image = GetComponent<Image>();
        startColor = _image.color;
        neighbors = new List<Cell>() { northWest, north, west, northEast, southWest, east, south, southEast };
    }

    public bool ChackingChessmanColor(bool white)
    {
        return chessman != null && chessman.white == white;
    }

    public void SetChessman(Chessman c)
    {
        isEmpty = c == null;
        chessman = c;
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
                StartCoroutine(SmoothColorChange(_image.color - ColorStorage.freeForStepOffeset));
            else
                StartCoroutine(SmoothColorChange(startColor));
    }

    private IEnumerator SmoothColorChange(Color endColor)
    {
        float colorizeRatio = 0f;
        Color startColor = _image.color;
        while (colorizeRatio <= 1)
        {
            _image.color = Color.Lerp(startColor, endColor, colorizeRatio);
            colorizeRatio += 0.15f;
            yield return new WaitForFixedUpdate();
        }
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
