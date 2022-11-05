using UnityEngine;
using TMPro;

public class HealthValueDisplay : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _currentHealthText;
    [SerializeField] private TMP_Text _maxHealthText;

    private void OnEnable()
    {
        _player.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float currentHealth, float maxHealth)
    {
        _currentHealthText.text = Mathf.Ceil(currentHealth).ToString();
        _maxHealthText.text = "/" + Mathf.Ceil(maxHealth).ToString();
    }
}