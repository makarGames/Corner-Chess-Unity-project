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

        ChackingForStep(pointerDrag, cell);
    }

    private void ChackingForStep(Chessman chessman, Cell cell)
    {
        if (chessman != null && cell.isEmpty && chessman.GetNeighborCells().Contains(cell))
        {
            chessman.MoveTo(cell);
        }
    }
}
