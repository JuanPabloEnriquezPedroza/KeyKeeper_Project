using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementIII : MonoBehaviour
{
    int hitpoints = 3;
    public GameObject hitParticle;
    public GameObject dyingParticle;

    public Transform player;
    public LayerMask isPlayer;

    //Patroling
    public float enemy_speed = 2f;
    public float rotation_speed = 1f;
    int routine;
    float chronometer;
    public Animator movement;
    Quaternion angle;
    float degree;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    [SerializeField] GameObject sword;
    Animator attack;

    //States
    public bool playerInSightRange, playerInAttackRange;
    [SerializeField] AudioClip damageEffect;
    [SerializeField] AudioClip dyingEffect;

    public void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        attack = sword.GetComponent<Animator>();
    }

    private void Update()
    {
        //Check for sight and attack range
        if (Vector3.Distance(transform.position, player.transform.position) < 3.5f) playerInAttackRange = true;
        else playerInAttackRange = false;

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private void Patroling()
    {
        chronometer += 1 * Time.deltaTime;
        if (chronometer >= 4)
        {
            routine = Random.Range(0, 2);
            chronometer = 0;
        }
        switch (routine)
        {
            case 0:
                movement.SetBool("walk", false);
                break;
            case 1:
                degree = Random.Range(0, 360);
                angle = Quaternion.Euler(0, degree, 0);
                routine++;
                break;
            case 2:
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, rotation_speed);
                transform.Translate(Vector3.forward * enemy_speed * Time.deltaTime);
                movement.SetBool("walk", true);
                break;
        }
    }

    private void ChasePlayer()
    {
        movement.SetBool("walk", true);
        var lookPosition = player.transform.position - transform.position;
        lookPosition.y = 0;
        var rotation = Quaternion.LookRotation(lookPosition);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotation_speed * 3);
        transform.Translate(Vector3.forward * enemy_speed * 1.5f * Time.deltaTime);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        movement.SetBool("walk", false);
        if (!alreadyAttacked)
        {
            //Attack code
            attack.SetTrigger("attacking");

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage()
    {
        hitpoints--;
        if (hitpoints <= 0)
        {
            Invoke(nameof(DestroyEnemy), .0001f);
        }
        else
        {
            GameObject inst = Instantiate(hitParticle, new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z), transform.rotation);
            Destroy(inst, 1f);
            AudioSource.PlayClipAtPoint(damageEffect, transform.position);
        }
    }

    private void DestroyEnemy()
    {
        GameObject inst = Instantiate(dyingParticle, new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z), transform.rotation);
        Destroy(inst, 1f);
        AudioSource.PlayClipAtPoint(dyingEffect, transform.position);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInSightRange = true;
        }
        if (other.CompareTag("Wall") && !playerInSightRange)
        {
            Patroling();
        }
    }
}
