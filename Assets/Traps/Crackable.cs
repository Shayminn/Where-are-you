using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crackable : MonoBehaviour {
    [SerializeField] GameObject CrackedVersion = null;

    List<MeshRenderer> MeshRenderers = new List<MeshRenderer>();

    public void Crack() {

        // 3D Objs
        Vector3 targetPos = transform.position;
        targetPos.z -= 0.1f;

        GameObject obj = Instantiate(CrackedVersion, targetPos, CrackedVersion.transform.rotation);
        obj.transform.localScale = transform.localScale;

        Destroy(obj, 1f);
    }

}
