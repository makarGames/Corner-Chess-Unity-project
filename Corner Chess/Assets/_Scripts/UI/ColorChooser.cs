using UnityEngine;
using UnityEngine.UI;

public class ColorChooser : MonoBehaviour
{
    [SerializeField] private Text colorChooserText;
    [SerializeField] private Button whiteButton;
    [SerializeField] private Button blackButton;

    private bool _white;
    public bool white
    {
        get => _white;
        set
        {
            _white = value;

            whiteButton.interactable = !value;
            blackButton.interactable = value;
            PlayerPrefs.SetInt("white", value ? 1 : 0);
        }
    }

    public static ColorChooser S;

    private void Awake()
    {
        if (S == null)
            S = this;

        whiteButton.GetComponent<Image>().color = ColorStorage.whiteChessman;
        blackButton.GetComponent<Image>().color = ColorStorage.blackChessman;
        whiteButton.onClick.AddListener(() => white = true);
        blackButton.onClick.AddListener(() => white = false);
    }

    public void GameModeChanging(bool soloGame)
    {
        colorChooserText.text = soloGame ? "Выберете цвет" : "Игрок1\t\t\t\t\t\t\t Игрок2";
        EnablingButtons(soloGame);
        if (soloGame) white = true;
    }

    public void EnablingButtons(bool state)
    {
        whiteButton.enabled = state;
        blackButton.enabled = state;
    }
}
