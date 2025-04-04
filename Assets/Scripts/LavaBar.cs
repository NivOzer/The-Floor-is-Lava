using UnityEngine;
using UnityEngine.UI;

public class LavaBar : MonoBehaviour
{
    public Slider slider;
    public void setMaxLava(int lava){
        slider.maxValue = lava;
    }

    public void setLava(int lava){
        slider.value = lava;
    }
}
