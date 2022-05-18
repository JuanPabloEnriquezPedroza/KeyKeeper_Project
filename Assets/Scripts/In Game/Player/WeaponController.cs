using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] GameObject sword;
    [SerializeField] bool canAttack = true;
    [SerializeField] float attackCooldown = 1.0f;
    [SerializeField] AudioClip effect;
    public bool attack = false;

    void Update()
    {
        if(attack && !PauseMenu.isPaused)
        {
            if(canAttack)
            {
                SwordAttack();
            }
        }
    }

    public void SwordAttack()
    {
        canAttack = false;
        Animator anim = sword.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        AudioSource.PlayClipAtPoint(effect, transform.position);
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        attack = false;
        canAttack = true;
    }

    public void OnAttackPressed()
    {
        if(canAttack) attack = true;
    }
}

//https://youtu.be/aNZw588BQBo