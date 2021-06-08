using UnityEngine;
using UnityEngine.EventSystems;

public class ChessmanSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Chessman pointerDrag = eventData.pointerDrag.GetComponent<Chessman>();
        Cell cell = GetComponent<Cell>();

        if (pointerDrag.white != OrderOfSteps.S.whiteMoves)
            return;

        if (pointerDrag != null && pointerDrag.CheckingOnNeighbor(cell) && cell.isEmpty)
        {


            pointerDrag.ChangeCell(cell);
            EndGame.S.CheckingEndGame();
            OrderOfSteps.S.whiteMoves = !OrderOfSteps.S.whiteMoves;
        }
    }
}
