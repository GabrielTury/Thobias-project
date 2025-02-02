﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class SimpleFollow : MonoBehaviour
{
    public static SimpleFollow instance;
    public int life = 10;
    [SerializeField] float circleRadius,speed;
    [SerializeField] LayerMask layerMask;
    public GameObject target;
    Rigidbody2D rdb;
    Animator anima;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        rdb = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
    }
    /// <summary>
    /// Checa o se o player esta dentro do range
    /// </summary>
    void CheckRange()
    {
        Collider2D inRange = Physics2D.OverlapCircle(transform.position, circleRadius, layerMask);
        if(inRange != null)
        {
            target = inRange.gameObject;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position + new Vector3(0,1f,0),speed);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        CheckRange();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }

    public void OnParticleCollision()
    {
        life--;
            if(life <= 0)
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Ativa a animação de dar dano no player.
    /// </summary>
    public void HitAnimation()
    {
        anima.SetTrigger("Hit");
    }
}
