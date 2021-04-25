using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAir : MonoBehaviour
{
    public LayerMask LayerMask;

    public float distance = 0.25f;
    public bool InAir = false;

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, distance, LayerMask);

        if (hit.collider != null) {
            InAir = false;
        }
        else {
            InAir = true;
        }
    }
}
