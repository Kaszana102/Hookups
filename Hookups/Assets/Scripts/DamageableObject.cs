using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageableObject : MonoBehaviour
{
    [SerializeField] public int healthPoints;
    [SerializeField] private Image healthBar;
    public Healthbar healthbar;
    private int maxHealth = 10;


    private void Start()
    {
        healthbar.SetMaxHealth(maxHealth);
    }
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
                //healthBar.fillAmount = healthPoints / maxHealth;
                healthbar.SetHealth(healthPoints);
            }   
        }
    }

    public void receiveDamage(int damage)
    {        
        if (healthPoints > 0)
        {
            healthPoints -= damage;
            //healthBar.fillAmount = healthPoints / maxHealth;
            healthbar.SetHealth(healthPoints);
        }       
    }

    public void receiveHoldingDamage(int damage)
    {
        
        if (healthPoints > 0)
        {
            healthPoints -= damage;
            //healthBar.fillAmount = healthPoints / maxHealth;
            healthbar.SetHealth(healthPoints);
        }   
        
    }
}
