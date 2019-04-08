using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStats:MonoBehaviour
{
    public float strongMod;
    public float specialMod;

    [HideInInspector]
    public float damage;


    public void SetDamage(string attackType, float damage)
    {
        switch(attackType)
        {
            case GlobalConst.special:
                this.damage = damage * specialMod;
                break;
            case GlobalConst.strong:
                this.damage = damage * strongMod;
                break;
            case GlobalConst.swing:
                this.damage = damage;
                break;
        }
    }
}
