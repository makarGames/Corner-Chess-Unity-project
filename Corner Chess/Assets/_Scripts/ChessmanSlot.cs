using UnityEngine;
using UnityEngine.EventSystems;

public class ChessmanSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Chessman pointerDrag = eventData.pointerDrag.GetComponent<Chessman>();
        Cell cell = GetComponent<Cell>();

        if (pointerDrag != null && pointerDrag.CheckingOnNeighbor(cell) && cell.isEmpty)
        {
            pointerDrag.ChangeCellColor(false);
            pointerDrag.ChangeCell(cell);
        }
    }
}
