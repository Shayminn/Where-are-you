using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAir : MonoBehaviour
{
    public LayerMask LayerMask;

    public float Distance = 0.3f;
    public float LeftRight = 0.1f;
    public bool InAir = false;

    // Update is called once per frame
    void Update()
    {
        Vector3 currTrans = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, Distance, LayerMask);

        currTrans.x -= LeftRight;
        RaycastHit2D hitLeft = Physics2D.Raycast(currTrans, -Vector2.up, Distance, LayerMask);

        currTrans.x += LeftRight * 2;
        RaycastHit2D hitRight = Physics2D.Raycast(currTrans, -Vector2.up, Distance, LayerMask);

        if (hit.collider != null ||
            hitLeft.collider != null ||
            hitRight.collider != null) {
            InAir = false;
        }
        else {
            InAir = true;
        }
    }
}
