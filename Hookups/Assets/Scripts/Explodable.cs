using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explodable : ObjectGrabbable
{
    [SerializeField] Explosion explosion;

    protected override void Start()
    {
        base.Start();
        explosion = GameObject.Instantiate(explosion).GetComponent<Explosion>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(this.thrown && this.damageableObject.gameObject != collision.gameObject)
        {
            explosion.Explode(transform.position);
            GameObject.Destroy(this.gameObject);
        }
    }
}
