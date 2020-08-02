using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 10;

    public void damageEnemy()
    {
        health -= 1;
        Debug.Log(health);
    }
}
