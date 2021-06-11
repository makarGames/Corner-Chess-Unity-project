using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class Chessman : MonoBehaviour
{
    private Animation _animation;
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

    private void Awake()
    {
        _animation = GetComponent<Animation>();
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
            cell.SetChessman(null);
            ChangeCellColor(false);
        }

        cell = c;
        cell.SetChessman(this);
        StartCoroutine(MovingChess(cell.transform.position));
    }

    private IEnumerator MovingChess(Vector3 endPosiotion)
    {
        float moveRatio = 0f;
        Vector3 startPosition = transform.position;
        while (moveRatio <= 1)
        {
            transform.position = Vector3.Lerp(startPosition, endPosiotion, moveRatio);
            moveRatio += 1f / 6f;
            yield return new WaitForFixedUpdate();
        }
        _animation.Play("Placed");
    }

    public void MoveTo(Cell newCell)
    {
        this.ChangeCell(newCell);
        EndGame.S.CheckingEndGame();
        OrderOfSteps.S.whiteMoves = !OrderOfSteps.S.whiteMoves;
    }
}
