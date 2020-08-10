using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        { 
            collision.gameObject.GetComponent<EnemyController>().damage(1);
            
            //EnemyController.GetInstance().damage(1);
            Destroy(this.gameObject);
        }
        
        if (collision.tag == "Enemy3")
        {
            collision.gameObject.GetComponent<EnemyController1>().damage(1);
            Destroy(this.gameObject);
        }
        Destroy(this.gameObject);
    }
}
