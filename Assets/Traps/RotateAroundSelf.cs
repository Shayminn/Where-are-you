using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundSelf : MonoBehaviour
{
    [SerializeField] float DegreePerSecond = 1;
    [SerializeField] bool Clockwise = true;

    int sign = 1;

    void Start() {
        SetRotationDirection(Clockwise);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(sign * Vector3.forward * DegreePerSecond * Time.fixedDeltaTime);
    }

    public void SetRotationDirection(bool Clockwise) {
        if (Clockwise) {
            sign = -1;
        }
        else {
            sign = 1;
        }
    }

    public void SetRotationDirection() {
        SetRotationDirection(!Clockwise);
    }
}
