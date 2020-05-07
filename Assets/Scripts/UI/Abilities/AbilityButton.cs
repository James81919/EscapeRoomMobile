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

    private float cooldownTime = 2f;
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
        // If not able to activate ability
        if (!canActivate)
        {
            // If cooldown is still going, count down
            if (cooldownTimeRemaining > 0)
            {
                cooldownTimeRemaining -= Time.deltaTime;
                cooldownOverlay.fillAmount = 1 - (cooldownTimeRemaining / cooldownTime);
            }
            else // If cooldown is finished...
            {
                // Set can activate to true
                canActivate = true;
            }
        }
    }

    public void SetCooldownTime(float _cooldownTime)
    {
        cooldownTime = _cooldownTime;
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
