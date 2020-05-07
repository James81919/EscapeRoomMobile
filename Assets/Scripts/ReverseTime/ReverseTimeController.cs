using System.Collections;
using System.Collections.Generic;
using System.Xml;
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
        // Get all time bodies in the scene
        timeBodies = FindObjectsOfType<TimeBody>();

        // Set the ability duration for each time body in the scene
        for (int i = 0; i < timeBodies.Length; i++)
        {
            timeBodies[i].recordTime = abilityDuration;
        }
    }

    private void Update()
    {
        // If able to rewind...
        if (canRewind)
        {
            // If the rewind button is pressed, start rewinding
            if (Input.GetKeyDown(KeyCode.Return))
                StartRewind();

            // If the rewind button is released, stop rewinding
            if (Input.GetKeyUp(KeyCode.Return) && isRewinding)
                StopRewind();
        }
    }

    private void FixedUpdate()
    {
        // If able is rewinding and can rewind..
        if (isRewinding && canRewind)
        {
            // Rewind all time bodies in scene
            for (int i = 0; i < timeBodies.Length; i++)
            {
                // If cannot rewind time body, stop rewinding
                if (!timeBodies[i].Rewind())
                    StopRewind();
            }
        }
        else
        {

            // Record each time body in scene
            for (int i = 0; i < timeBodies.Length; i++)
            {
                timeBodies[i].Record();
            }
        }
    }

    private void StartRewind()
    {
        // Set is rewinding bool to true
        isRewinding = true;

        // Activate time left indicator
        if (timeLeftIndicator)
            timeLeftIndicator.Activate(abilityDuration);

        // Set ability button to activated ability state
        if (abilityButton)
            abilityButton.ActivateAbility();

        // Call OnRewindStart() for each time body in scene
        for (int i = 0; i < timeBodies.Length; i++)
        {
            timeBodies[i].OnRewindStart();
        }
    }

    private void StopRewind()
    {
        // Set is rewinding bool to false
        isRewinding = false;

        // Deactivate time left indicator
        if (timeLeftIndicator)
            timeLeftIndicator.Deactivate();

        // Call OnRewindStop() for each time body in scene
        for (int i = 0; i < timeBodies.Length; i++)
        {
            timeBodies[i].OnRewindStop();
        }

        // Start cooldown timer
        StartCoroutine(CooldownTimer());
    }

    private IEnumerator CooldownTimer()
    {
        // Set ability rewind to false
        canRewind = false;

        // Set ability button to deactivated ability state
        if (abilityButton)
            abilityButton.DeactivateAbility(cooldownTime);
        
        // Wait for cooldown time
        yield return new WaitForSeconds(cooldownTime);

        // Set ability to rewind to true
        canRewind = true;
    }

    public void AbilityButton_Pressed()
    {
        if (canRewind)
        {
            if (isRewinding)
            {
                StopRewind();
            }
            else
            {
                StartRewind();
            }    
        }
    }
}
