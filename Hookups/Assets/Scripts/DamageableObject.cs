using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObject : MonoBehaviour
{
    [SerializeField] public int healthPoints;
    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        var grabbable = collision.gameObject.GetComponent<ObjectGrabbable>();
        receiveDamage(grabbable,collision.gameObject.tag);
    }

    public void receiveDamage(ObjectGrabbable grabbable, string tag)
    {
        if (grabbable == null || grabbable.damageableObject == null) return;
        if (tag == "Untagged" && !grabbable.damageableObject.Equals(this))
        {
            if (healthPoints > 0)
            {
                healthPoints -= grabbable.damage;
            }   
        }
    }

    public void receiveHoldingDamage(ObjectGrabbable grabbable, string tag)
    {
        if (grabbable == null || grabbable.damageableObject == null) return;
        if (tag == "Untagged" && grabbable.damageableObject.Equals(this))
        {
            if (healthPoints > 0)
            {
                healthPoints -= grabbable.damage;
            }   
        }
    }
}
