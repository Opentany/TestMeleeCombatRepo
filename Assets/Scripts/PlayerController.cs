using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action<string> attackEvent;
    public Animator animator;
    public float force;
    public float movementSpeed;
    public float rotationSpeed;
    public Vector3 mouse;
    public float specialCooldown;
    public float baseDamage;
    public AttackStats attackStats;
    Vector3 mouseOld;
    int backward = 1;
    float lastTimeSpecial;


    // Update is called once per frame
    private void Start()
    {
        mouse = Input.mousePosition;
        mouseOld = mouse;
        lastTimeSpecial -= specialCooldown;
    }
    void Update()
    {
        mouseOld = mouse;
        mouse = Input.mousePosition;
        if (Input.GetKey(KeyCode.W))
        {
            Movement(transform.forward,false);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Movement(transform.forward * -1f, true);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Movement(transform.right * -1f, false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Movement(transform.right, false);
        }
        if (mouse.x < mouseOld.x)
        {
            Rotate();
        }
        if (mouse.x > mouseOld.x)
        {
            Rotate();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            PerformAttack();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            PerformStrongAttack();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            PerformSpecialAttack();
        }
    }

    void Movement(Vector3 direction, bool back)
    {
        if (back){ backward = -1; }
        else { backward = 1; }
        transform.position += direction * Time.deltaTime * movementSpeed;
        backward = 1;
    }

    void Rotate()
    {
        transform.Rotate(new Vector3(0, (mouse.x - mouseOld.x) * backward, 0) * Time.deltaTime * rotationSpeed);
    }

    private void PerformSpecialAttack()
    {
        var tryTime = Time.time;
        if (tryTime>lastTimeSpecial+specialCooldown)
        {
            Attack(GlobalConst.special);
            lastTimeSpecial = tryTime;
        }
        
    }

    private void PerformStrongAttack()
    {
        Attack(GlobalConst.strong);
    }

    private void PerformAttack()
    {
        Attack(GlobalConst.swing);
    }

    private void Attack(string type)
    {
        animator.SetTrigger(type);
        attackStats.SetDamage(type, baseDamage);
        attackEvent?.Invoke(type);
    }
}
