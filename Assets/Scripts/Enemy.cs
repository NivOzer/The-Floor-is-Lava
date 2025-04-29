using System;
using UnityEngine;

public class Enemy : MonoBehaviour , IDamagable
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

    public void TakeDamage(int amount)
    {
        health -= amount;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        DamagePopup dmgPopup = Instantiate(GameAssets.Assets.damagePopup, screenPos, Quaternion.identity, GameObject.Find("Canvas").transform).GetComponent<DamagePopup>();
        dmgPopup.Setup(amount);
        if(health <= 0){
            Die();
        }
    }

    protected void Die(){
        FindFirstObjectByType<GameManager>().EnemyDied();
        Destroy(gameObject);
    }

    public virtual void SpecialAttack(){
        
    }

}
