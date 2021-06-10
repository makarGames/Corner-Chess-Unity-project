using UnityEngine;
using UnityEngine.UI;

public class FieldSizeSlider : MonoBehaviour
{
    [SerializeField] private Text fieldSizeText;
    [SerializeField] private Slider slider;

    private int _size;
    private int size
    {
        get => _size;
        set
        {
            _size = value;
            fieldSizeText.text = "ВЫБЕРЕТЕ РАЗМЕР ПОЛЯ: " + value + "x" + value;
            PlayerPrefs.SetInt("fieldSize", value);
        }
    }

    private void Start()
    {
        slider.onValueChanged.AddListener((v) => size = Mathf.RoundToInt(v));
        slider.value = 8;
    }
}
