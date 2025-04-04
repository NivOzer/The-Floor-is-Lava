using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Transform shootingPoint;
    [SerializeField] GameObject bulletPrefab;
    public LavaBar lavaBar;
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
        currentLava -= lavaCostPerShot;
        lavaBar.setLava(currentLava);
    }

    public void gainLava(){
        currentLava += lavaGainPerSecond;
        lavaBar.setLava(currentLava);
    }
}
