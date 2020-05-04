using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    [System.NonSerialized]
    public float recordTime;

    List<PointInTime> pointsInTime;

    private void Start()
    {
        pointsInTime = new List<PointInTime>();
    }

    public void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            transform.position = pointsInTime[pointsInTime.Count - 1].position;
            transform.rotation = pointsInTime[pointsInTime.Count - 1].rotation;
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }
        else
        {
            OnRewindStop();
        }
    }

    public void Record()
    {
        if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(0);
        }

        pointsInTime.Add(new PointInTime(transform.position, transform.rotation));
    }

    public void OnRewindStart()
    {
        if(GetComponent<Rigidbody2D>())
            GetComponent<Rigidbody2D>().isKinematic = true;
    }

    public void OnRewindStop()
    {
        if (GetComponent<Rigidbody2D>())
            GetComponent<Rigidbody2D>().isKinematic = false;
    }
}
