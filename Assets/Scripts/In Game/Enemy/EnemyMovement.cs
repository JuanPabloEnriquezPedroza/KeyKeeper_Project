using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] AudioClip damageEffect;
    [SerializeField] AudioClip dyingEffect;
    GameObject player;
    int hitpoints = 3;
    bool playerFound = false;

    public float enemy_speed = 2f;
    public float rotation_speed = 1f;
    
    int routine;
    float chronometer;
    //public Animator movement;
    Quaternion angle;
    float degree;

    [SerializeField] GameObject sword;
    Animator attack;
    [SerializeField] float attackCooldown = 2.0f;
    bool enemy_attacking = false;
    bool can_attack = true;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        attack = sword.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if(!playerFound)
        {
            //transform.Rotate(new Vector3(0f, 60f, 0f) * Time.deltaTime);
            //transform.position = Vector3.MoveTowards(transform.position, moveSpot.position, enemy_speed * Time.deltaTime);

            //Version 2
            //animator.SetBool("run", false);
            chronometer += 1 * Time.deltaTime;
            if(chronometer >= 4)
            {
                routine = Random.Range(0, 2);
                chronometer = 0;
            }
            switch(routine)
            {
                case 0:
                    //animator.SetBool("walk", false);
                    break;
                case 1:
                    degree = Random.Range(0, 360);
                    angle = Quaternion.Euler(0, degree, 0);
                    routine++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, rotation_speed);
                    transform.Translate(Vector3.forward * enemy_speed * Time.deltaTime);
                    //animator.SetBool("walk", true);
                    break;
            }
        }
        else
        {
            if(Vector3.Distance(transform.position, player.transform.position) > 3.5f && can_attack)
            //if(Vector3.Distance(transform.position, player.transform.position) > 5)
            {
                var lookPosition = player.transform.position - transform.position;
                lookPosition.y = 0;
                var rotation = Quaternion.LookRotation(lookPosition);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotation_speed * 3);
                //animator.SetBool("walk", false);
                //animator.SetBool("run", true);
                transform.Translate(Vector3.forward * enemy_speed * 1.5f * Time.deltaTime);

                //attack.SetBool("attack", false);

                //transform.LookAt(player.transform);
                //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, enemy_speed * Time.deltaTime);
                //GetComponent<Rigidbody>().velocity = (player.transform.position - transform.position) * enemy_speed;
            }
            else
            {
                //animator.SetBool("walk", false);
                //animator.SetBool("run", false);
                attack.SetTrigger("attacking");
                //attack.SetBool("attack", true);
                enemy_attacking = true;
                //sword.GetComponent<Collider>().SetActive(enemy_attacking);
                can_attack = false;
                //print(enemy_attacking);
                StartCoroutine(ResetAttackCooldown());
            }
        }
    }

    public void AttackFinished()
    {
        //attack.SetBool("attack", false);
        enemy_attacking = false;
    }

    IEnumerator ResetAttackCooldown()
    {
        print("Hola1");
        yield return new WaitForSeconds(attackCooldown);
        print("Hola2");
        enemy_attacking = false;
        can_attack = true;
        //AttackFinished();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerFound = true;
        }
        if(other.CompareTag("Wall"))
        {
            degree = 180;
            angle = Quaternion.Euler(0, degree, 0);
            routine = 2;
        }
    }

    public void DamageTaken()
    {
        hitpoints--;
        print(hitpoints);
        AudioSource.PlayClipAtPoint(damageEffect, transform.position);
        if (hitpoints <= 0)
        {
            AudioSource.PlayClipAtPoint(dyingEffect, transform.position);
            Destroy(gameObject);
        }
    }
}

//https://www.youtube.com/watch?v=_5pxcUykXcA