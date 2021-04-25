using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikyWheel : MonoBehaviour
{
    [SerializeField] List<Vector3> TargetPositions = new List<Vector3>();
    [SerializeField] RotateAroundSelf RotateAroundSelf = null;
    [SerializeField] float DelayBeforeMoving = 3f;
    [SerializeField] float MoveSpeed = 1f;

    bool Reverse = false;
    int Index = 0;

    // Start is called before the first frame update
    void Start()
    {
        TargetPositions.Insert(0, transform.position);

        StartCoroutine(MoveTo());
    }

    IEnumerator MoveTo() {
        yield return new WaitForSeconds(DelayBeforeMoving);

        if (Reverse) {
            Index--;
        }
        else {
            Index++;
        }

        if (Index == TargetPositions.Count - 1) {
            Reverse = true;
            RotateAroundSelf.SetRotationDirection();
        }
        else if (Index == 0) {
            Reverse = false;
            RotateAroundSelf.SetRotationDirection();
        }

        Vector3 targetPos = TargetPositions[Index];
        while (transform.position != targetPos) {

            transform.position = Vector3.MoveTowards(transform.position, targetPos, MoveSpeed * Time.deltaTime);

            yield return null;
        }

        StartCoroutine(MoveTo());
    }
}
