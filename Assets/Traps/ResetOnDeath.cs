using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResetOnDeath : MonoBehaviour
{
    protected Vector3 StartingPosition;

    protected void Start() {
        StartingPosition = transform.position;    
    }

    public abstract void StartTrap();

    public abstract void ResetTrap();
}
