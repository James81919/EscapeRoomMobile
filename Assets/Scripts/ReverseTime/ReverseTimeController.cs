using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseTimeController : MonoBehaviour
{
    public AbilityTimeLeftIndicator timeLeftIndicator;
    public AbilityButton abilityButton;

    public float abilityDuration = 5f;
    public float cooldownTime = 5f;

    private TimeBody[] timeBodies;

    private bool isRewinding = false;
    private bool canRewind = true;

    private void Start()
    {
        timeBodies = FindObjectsOfType<TimeBody>();

        for (int i = 0; i < timeBodies.Length; i++)
        {
            timeBodies[i].recordTime = abilityDuration;
        }
    }

    private void Update()
    {
        if (canRewind)
        {
            if (Input.GetKeyDown(KeyCode.Return))
                StartRewind();

            if (Input.GetKeyUp(KeyCode.Return) && isRewinding)
                StopRewind();
        }
    }

    private void FixedUpdate()
    {
        if (isRewinding && canRewind)
        {
            for (int i = 0; i < timeBodies.Length; i++)
            {
                if (!timeBodies[i].Rewind())
                {
                    StopRewind();
                }
            }
        }
        else
        {
            for (int i = 0; i < timeBodies.Length; i++)
            {
                timeBodies[i].Record();
            }
        }
    }

    private void StartRewind()
    {
        isRewinding = true;

        if (timeLeftIndicator)
            timeLeftIndicator.Activate(abilityDuration);

        if (abilityButton)
            abilityButton.ActivateAbility();

        for (int i = 0; i < timeBodies.Length; i++)
        {
            timeBodies[i].OnRewindStart();
        }
    }

    private void StopRewind()
    {
        isRewinding = false;

        if (timeLeftIndicator)
            timeLeftIndicator.Deactivate();

        for (int i = 0; i < timeBodies.Length; i++)
        {
            timeBodies[i].OnRewindStop();
        }

        StartCoroutine(CooldownTimer());
    }

    private IEnumerator CooldownTimer()
    {
        canRewind = false;

        if (abilityButton)
            abilityButton.DeactivateAbility(cooldownTime);

        yield return new WaitForSeconds(cooldownTime);
        canRewind = true;
    }
}
