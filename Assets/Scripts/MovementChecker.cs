using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementChecker : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 previousPosition;
    private Vector3 velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        previousPosition = rb.position;
    }

    void FixedUpdate()
    {
        Vector3 currentPosition = rb.position;
        velocity = (currentPosition - previousPosition) / Time.fixedDeltaTime;
        previousPosition = currentPosition;
    }

    public Vector3 PredictFuturePosition(float time)
    {
        return rb.position + velocity * time;
    }
}
