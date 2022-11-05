using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Bar : MonoBehaviour
{
    [SerializeField] protected Slider Slider;
    
    private Coroutine _changeValueRoutine;
    private float _changingRate = 1f;

    protected void OnValueChange(float value, float maxValue)
    {
        if (_changeValueRoutine != null)
        {
            StopCoroutine(_changeValueRoutine);
        }

        _changeValueRoutine = StartCoroutine(ChangeValue(value, maxValue));
    }

    private IEnumerator ChangeValue(float value, float maxValue)
    {
        float normalizeHealth = value / maxValue;

        while (Slider.value != normalizeHealth)
        {
            Slider.value = Mathf.MoveTowards(Slider.value, normalizeHealth, _changingRate * Time.deltaTime);
            yield return null;
        }
    }
}