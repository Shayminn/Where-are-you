using UnityEngine;

public abstract class ResetOnDeath : MonoBehaviour {
    protected Vector3 StartingPosition;
    public bool Started = false;
    public bool NeedsReset = true;

    protected void Start() {
        StartingPosition = transform.position;
    }

    public abstract void StartTrap();

    public abstract void ResetTrap();
}
