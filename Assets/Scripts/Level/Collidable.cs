using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    public float damage = 1.0f;
    private BoxCollider2D boxCollider;
    public ContactFilter2D filter;
    public Collider2D[] hits = new Collider2D[10];
    protected virtual void Start() 
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        //Collidable work
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            OnCollide(hits[i]);

            //The array is not cleaned up, so we do it ourself
            hits[i] = null;
        }
    }
    protected virtual void OnCollide(Collider2D coll)
    {
        Debug.Log(coll.name);
        //Pumpkin.pumpHp -= damage;
    }

}
