using UnityEngine;
using UnityEngine.UI;

public class OceanHealthUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;

    private float t;

    private float currHealthSlow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthSlider.value = OceanHealth.GetHealthRatio();
        currHealthSlow = OceanHealth.GetCurrentHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Mathf.Approximately(currHealthSlow, OceanHealth.GetCurrentHealth())){
            currHealthSlow = Mathf.Lerp(currHealthSlow,OceanHealth.GetCurrentHealth(), t);
            t +=  0.5f * Time.deltaTime;
        }
        else
        {
            t = 0;
        }

        healthSlider.value = currHealthSlow / OceanHealth.GetMaxHealth();
    }
}
