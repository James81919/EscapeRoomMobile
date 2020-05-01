using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    private bool isRewinding;

    public float recordTime = 5f;

    List<PointInTime> pointsInTime;

    private void Start()
    {
        pointsInTime = new List<PointInTime>();
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
        if (isRewinding)
            Rewind();
        else
            Record();
    }

    private void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            transform.position = pointsInTime[pointsInTime.Count - 1].position;
            transform.rotation = pointsInTime[pointsInTime.Count - 1].rotation;
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }
        else
        {
            StopRewind();
        }
    }

    private void Record()
    {
        if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(0);
        }

        pointsInTime.Add(new PointInTime(transform.position, transform.rotation));
    }

    public void StartRewind()
    {
        isRewinding = true;

        if(GetComponent<Rigidbody2D>())
            GetComponent<Rigidbody2D>().isKinematic = true;
    }

    public void StopRewind()
    {
        isRewinding = false;

        if (GetComponent<Rigidbody2D>())
            GetComponent<Rigidbody2D>().isKinematic = false;
    }
}
