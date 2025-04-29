using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    TextMeshProUGUI dmgText;
    void Awake()
    {
        dmgText = GetComponentInChildren<TextMeshProUGUI>();   
    }

    void Update()
    {
        float moveSpeedY = 30f;
        transform.position += new Vector3(0,moveSpeedY) * Time.deltaTime;    
    }

    public void Setup(int damageAmount){
        dmgText.text = damageAmount.ToString();
        Destroy(gameObject,0.6f);
    }
}
