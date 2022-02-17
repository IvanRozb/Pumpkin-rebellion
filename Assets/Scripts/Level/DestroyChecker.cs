using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyChecker : MonoBehaviour
{
    public ParticleSystem ps;
    public float duration, childCount;
    private void Update()
    {
        if (ps.time > duration /*&& ps.transform.parent.childCount == childCount*/)
        {
            Destroy(ps.gameObject);
            Destroy(this.gameObject);
        }
    }
}
