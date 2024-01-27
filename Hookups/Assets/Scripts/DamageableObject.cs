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
        if (grabbable == null || grabbable.DamObj == null) return;
        var tag = collision.gameObject.tag;
        if (tag == "DamageObject" && !grabbable.DamObj.Equals(this))
        {
            
            if (healthPoints > 0)
            {
                healthPoints -= 1;
            }
            
        }
    }
}
