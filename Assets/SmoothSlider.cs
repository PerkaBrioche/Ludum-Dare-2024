using System;
using UnityEngine;
using UnityEngine.UI;

public class SmoothSlider : MonoBehaviour
{
    private Slider slider; 
    public float smoothSpeed = 0.05f;

    private float currentValue = 0f;
    private float targetValue = 0f;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    { }
    void Update()
    {
        targetValue = InkManager.Instance.Ink;
        currentValue = Mathf.Lerp(currentValue, targetValue, smoothSpeed * Time.deltaTime);
        slider.value = currentValue;
    }

    public void NewValue(int MaxValue, int minValue)
    {
        slider.minValue = minValue;
        slider.maxValue = MaxValue;
        slider.value = 0;
    }
}