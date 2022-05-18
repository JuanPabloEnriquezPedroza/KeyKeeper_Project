using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject prefabEnemy;
    public int totalEnemies = 3;

    void Start()
    {
        for(int i = 0; i < totalEnemies; i++)
        {
            Instantiate(prefabEnemy, transform.position, Quaternion.identity, transform);
        }
    }
}
