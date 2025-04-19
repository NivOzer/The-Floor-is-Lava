using UnityEngine;

public class EvilWizardX : Enemy
{
    [SerializeField] Animator animator;
    [SerializeField] Transform shootingPoint;
    [SerializeField] GameObject lavaBallPrefab;
    [SerializeField] private float specialAttackCooldown = 4f;
    private float specialAttackTimer = 0f;
    int dmgFromPlayer = 20;

    void Update()
    {
        HandleSpecialAttack();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            TakeDamage(dmgFromPlayer);
            Debug.Log("Took 20 Damage");
        }
    }

    private void HandleSpecialAttack(){
        specialAttackTimer += Time.deltaTime;
        if (specialAttackTimer >= specialAttackCooldown){
            SpecialAttack();
            specialAttackTimer = 0f;
        }
    }
    public override void SpecialAttack()
    {
        // base.SpecialAttack();
        for (int i = 0; i < Random.Range(3,5); i++){
            animator.SetBool("Attack",true);
            Instantiate(lavaBallPrefab,shootingPoint.position,lavaBallPrefab.transform.rotation);
        }
    }

}
