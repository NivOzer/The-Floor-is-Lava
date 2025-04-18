using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] Transform shootingPoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] AudioClip shotSound;
    public LavaBar lavaBar;
    float lavaGainTimer = 0f;
    public int maxLava = 100;
    public int currentLava = 0;
    int lavaCostPerShot = 10;
    int lavaGainPerSecond = 5;
    void Start()
    {
        lavaBar.setMaxLava(maxLava);
    }

    void Update()
    {
        if (Input.GetButtonDown("Shoot") && currentLava > 0){
            Shoot();
        }
    }

    void Shoot(){
        Instantiate(bulletPrefab,shootingPoint.position,shootingPoint.rotation);
        GetComponent<AudioSource>().PlayOneShot(shotSound);
        currentLava -= lavaCostPerShot;
        lavaBar.setLava(currentLava);
    }

    public void gainLava(){
        lavaGainTimer += Time.deltaTime;
        if (lavaGainTimer > 0.3f){
            currentLava += lavaGainPerSecond;
            lavaBar.setLava(Mathf.Min(currentLava,maxLava));
            lavaGainTimer = 0f;
        }
    }
}
