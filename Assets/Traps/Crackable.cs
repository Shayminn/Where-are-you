using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crackable : MonoBehaviour {
    [SerializeField] GameObject CrackedVersion = null;

    public void Crack() {

        // 3D Objs
        Vector3 targetPos = transform.position;
        targetPos.z -= 0.1f;

        GameObject obj = Instantiate(CrackedVersion, targetPos, CrackedVersion.transform.rotation);
        obj.transform.localScale = new Vector3(transform.localScale.x * 2.5f, transform.localScale.z, transform.localScale.y * 2.5f);

        Destroy(obj, 1f);
    }

}
