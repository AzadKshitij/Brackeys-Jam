﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kulhadi : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().damage(2);
        }
    }
}
