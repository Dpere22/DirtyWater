using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class OceanHealthUI : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;

        private float _t;

        private float _currHealthSlow;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            healthSlider.value = OceanHealth.GetHealthRatio();
            _currHealthSlow = OceanHealth.GetCurrentHealth();
        }

        // Update is called once per frame
        void Update()
        {
            if(!Mathf.Approximately(_currHealthSlow, OceanHealth.GetCurrentHealth())){
                _currHealthSlow = Mathf.Lerp(_currHealthSlow,OceanHealth.GetCurrentHealth(), _t);
                _t +=  0.5f * Time.deltaTime;
            }
            else
            {
                _t = 0;
            }

            healthSlider.value = _currHealthSlow / OceanHealth.GetMaxHealth();
        }
    }
}
