using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    public Color activatedColor;
    private Color normalColor;

    [System.NonSerialized]
    public bool canActivate;
    [System.NonSerialized]
    public bool isActivated;

    private float cooldownTime;
    private float cooldownTimeRemaining;

    private Image cooldownOverlay;

    private void Start()
    {
        cooldownOverlay = transform.Find("Overlay").GetComponent<Image>();
        normalColor = cooldownOverlay.color;

        canActivate = true;
        isActivated = false;
    }

    private void Update()
    {
        if (!canActivate)
        {
            if (cooldownTimeRemaining > 0)
            {
                cooldownTimeRemaining -= Time.deltaTime;
                cooldownOverlay.fillAmount = 1 - (cooldownTimeRemaining / cooldownTime);
            }
            else
            {
                canActivate = true;
            }
        }
    }

    public void ActivateAbility()
    {
        if (canActivate)
        {
            isActivated = true;

            cooldownOverlay.color = activatedColor;
        }
    }

    public void DeactivateAbility(float _cooldownTime)
    {
        canActivate = false;
        isActivated = false;

        cooldownOverlay.fillAmount = 0;

        cooldownTime = _cooldownTime;
        cooldownTimeRemaining = cooldownTime;

        cooldownOverlay.color = normalColor;
    }
}
