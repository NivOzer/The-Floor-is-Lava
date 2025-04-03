using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Transform shootingPoint;
    [SerializeField] GameObject bulletPrefab;
    
    void Update()
    {
        if (Input.GetButtonDown("Shoot")){
            Shoot();
        }
    }

    void Shoot(){
        Instantiate(bulletPrefab,shootingPoint.position,shootingPoint.rotation);
    }
}
