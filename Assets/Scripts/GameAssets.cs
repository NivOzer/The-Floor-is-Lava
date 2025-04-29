using TMPro;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets Assets { get; private set; }

    void Awake()
    {
        if (Assets != null && Assets!=this){
            Destroy(gameObject);
            return;
        }
        Assets = this;
    }

    [Header("Prefabs")]
    public DamagePopup damagePopup;
}
