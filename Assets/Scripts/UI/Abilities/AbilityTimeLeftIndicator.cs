using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityTimeLeftIndicator : MonoBehaviour
{
    private Slider slider;

    private bool isActivated;
    private float timeRemaining;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        if (isActivated)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                slider.value = timeRemaining;
            }
            else
            {
                Deactivate();
            }
        }
    }

    public void Activate(float _abilityDuration)
    {
        gameObject.SetActive(true);

        slider.maxValue = _abilityDuration;
        slider.value = _abilityDuration;

        timeRemaining = _abilityDuration;
        isActivated = true;
    }

    public void Deactivate()
    {
        isActivated = false;
        gameObject.SetActive(false);
    }
}
