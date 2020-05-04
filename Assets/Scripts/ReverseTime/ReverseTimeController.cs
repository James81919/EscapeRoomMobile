using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseTimeController : MonoBehaviour
{
    public float recordTime = 5f;
    public float cooldownTime = 5f;

    private TimeBody[] timeBodies;

    private bool isRewinding = false;
    private bool canRewind = true;

    private void Start()
    {
        timeBodies = FindObjectsOfType<TimeBody>();

        for (int i = 0; i < timeBodies.Length; i++)
        {
            timeBodies[i].recordTime = recordTime;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            StartRewind();

        if (Input.GetKeyUp(KeyCode.Return))
            StopRewind();
    }

    private void FixedUpdate()
    {
        if (isRewinding && canRewind)
        {
            for (int i = 0; i < timeBodies.Length; i++)
            {
                timeBodies[i].Rewind();
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

        for (int i = 0; i < timeBodies.Length; i++)
        {
            timeBodies[i].OnRewindStart();
        }
    }

    private void StopRewind()
    {
        isRewinding = false;

        for (int i = 0; i < timeBodies.Length; i++)
        {
            timeBodies[i].OnRewindStop();
        }

        StartCoroutine(CooldownTimer());
    }

    private IEnumerator CooldownTimer()
    {
        canRewind = false;
        yield return new WaitForSeconds(cooldownTime);
        canRewind = true;
    }
}
