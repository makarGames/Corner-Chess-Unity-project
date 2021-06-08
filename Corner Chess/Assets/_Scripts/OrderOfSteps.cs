using UnityEngine;
using UnityEngine.UI;

public class OrderOfSteps : MonoBehaviour
{
    [SerializeField] private Text OrderOfStepsText;

    private bool _whiteMoves;
    public bool whiteMoves
    {
        get => _whiteMoves;
        set
        {
            _whiteMoves = value;
            if (value)
            {

                OrderOfStepsText.text = "БЕЛЫЕ";
                OrderOfStepsText.color = ColorStorage.whiteChessman;
                AI.S.Step();
            }
            else
            {
                OrderOfStepsText.text = "ЧЁРНЫЕ";
                OrderOfStepsText.color = ColorStorage.blackChessman;
            }
        }
    }

    public static OrderOfSteps S;
    private void Awake()
    {
        if (S == null)
            S = this;

        whiteMoves = true;
    }
}
