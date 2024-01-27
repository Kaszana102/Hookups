using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void receiveDamage(IDamaging sourceDamager, string objectTag);
}
