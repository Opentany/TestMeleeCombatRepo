using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public PlayerController playerController;
    public Transform pointToTarget;
    public float movementSpeed;
    public float distance;
    public float range;
    public float agro;
    public Animator anim;
    public int chanceToDodge;
    public float specialCooldown;
    public float baseDamage;
    public AttackStats attackStats;


    bool attacking;
    bool dodging;
    Coroutine attack;
    Coroutine dodge;
    float speed;
    float lastSpecial;

    private void Start()
    {
        playerController.attackEvent += DodgeAction;
        lastSpecial -= specialCooldown;
    }

    private void DodgeAction(string typeOfAttack)
    {
        var random = UnityEngine.Random.Range(1, 101);
        if (typeOfAttack == GlobalConst.strong && distance < range && random+10 <= chanceToDodge)
        {
            PerformDodge(0.5f);
        }
        else if (distance<range && random<= chanceToDodge)
        {
            PerformDodge(0.15f);
        }
    }

    private void PerformDodge(float delay)
    {
        dodging = true;
        if (dodge != null)
        {
            StopCoroutine(dodge);
        }
        dodge = StartCoroutine(DodgeCoroutine(delay));
    }

    IEnumerator DodgeCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        do
        {
            transform.position = Vector3.MoveTowards(transform.position, pointToTarget.position, speed * -2 * Time.deltaTime);
            yield return null;
        } while (distance<range+2);
        dodging = false;
    }

    private void Update()
    {
        
        distance = Vector3.Distance(transform.position, pointToTarget.position);
        if (distance > agro)
        {
            speed = movementSpeed / 2f;
        }
        else
        {
            speed = movementSpeed;
        }
        transform.LookAt(pointToTarget);
        if (distance > range &&!dodging)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointToTarget.position, speed * Time.deltaTime);
        }
        else if (!attacking&!dodging)
        {
            PerformAttack();
        }
    }

    private void PerformAttack()
    {
        string type = RandomAttack();
        attacking = true;
        if (attack != null)
        {
            StopCoroutine(attack);
        }
        attack = StartCoroutine(AttackCoroutine(type));
    }
    private string RandomAttack()
    {
        int random;
        var tryTime = Time.time;
        if (tryTime > lastSpecial + specialCooldown)
        {
            random = UnityEngine.Random.Range(1, 4);
        }
        else
        {
            random = UnityEngine.Random.Range(1, 3);
        }
        switch (random)
        {
            case 1: return GlobalConst.swing;
            case 2: return GlobalConst.strong;
            case 3:
                {
                    lastSpecial = tryTime;
                    return GlobalConst.special;
                }
        }
        return GlobalConst.swing;
    }

    IEnumerator AttackCoroutine(string type)
    {
        anim.SetTrigger(type);
        attackStats.SetDamage(type, baseDamage);
        yield return new WaitForSeconds(0.9f);
        attacking = false;
    }
}
