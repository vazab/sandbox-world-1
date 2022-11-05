using UnityEngine;
using TMPro;

public class SpeedometerDisplay : MonoBehaviour
{
    [SerializeField] private Speedometer _speedometer;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _speedometer.MaxSpeedChanged += OnMaxSpeedChanged;
    }

    private void OnDisable()
    {
        _speedometer.MaxSpeedChanged -= OnMaxSpeedChanged;
    }

    private void OnMaxSpeedChanged(float maxSpeed)
    {
        _text.text = string.Format("{0:N2}", maxSpeed);
    }
}