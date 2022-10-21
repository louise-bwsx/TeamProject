using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BMI : MonoBehaviour
{
    [SerializeField] private TMP_InputField weightInput;
    [SerializeField] private TMP_InputField heightInput;
    [SerializeField] private Button button;

    private void Start()
    {
        button.onClick.AddListener(Calculate);
    }

    private void Calculate()
    {
        float weight = float.Parse(weightInput.text);
        float height = float.Parse(heightInput.text);
        float heightMeter = height / 100;

        Debug.Log(weight / (heightMeter * heightMeter));
    }
}