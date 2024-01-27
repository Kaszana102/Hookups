using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObject : MonoBehaviour, IDamageable
{
    [SerializeField] public int healthPoints;
    private readonly string DAMAGE_TAG = "DamageObject";

    public void receiveDamage(IDamaging sourceDamager, string objectTag)
    {
        if (sourceDamager == null || sourceDamager.thrower() == null) return;
        if (objectTag == DAMAGE_TAG && !sourceDamager.thrower().Equals(this))
        {
            if (healthPoints > 0)
            {
                healthPoints -= sourceDamager.howMany();
            }
        }
    }

    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        var damaging = collision.gameObject.GetComponent<IDamaging>();
        receiveDamage(damaging,collision.gameObject.tag);
    }
}
