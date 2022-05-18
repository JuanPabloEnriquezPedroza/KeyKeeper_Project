using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    [SerializeField] WeaponController wc;
    public GameObject hitParticle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Skeleton(Clone)" && wc.attack)
        {
            wc.attack = false;
            other.GetComponent<EnemyMovementIII>().TakeDamage();
        }
    }
}
