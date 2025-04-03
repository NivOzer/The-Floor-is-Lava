using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 2f;
    [SerializeField] protected int health = 3;
    

    // Update is called once per frame
    protected virtual void Update()
    {
        Move();
    }
    public virtual void Move(){

    }

    public virtual void TakeDamage(int amount){
        health -= amount;
        if(health <= 0){
            Die();
        }
    }

    protected void Die(){
        Destroy(gameObject);
    }

}
