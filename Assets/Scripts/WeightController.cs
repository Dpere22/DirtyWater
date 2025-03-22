using Events;
using UnityEngine;
using UnityEngine.UI;

public class WeightController : MonoBehaviour
{
    [SerializeField] Slider weightSlider;
    [SerializeField] private Image fill;
    private float t;

    private float sliderVal;

    private float currWeightSlow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        if(!Mathf.Approximately(currWeightSlow, GameEventsManager.Instance.PlayerManager.CurrentWeight)){
            currWeightSlow = Mathf.Lerp(currWeightSlow,GameEventsManager.Instance.PlayerManager.CurrentWeight, t);
            t +=  0.5f * Time.deltaTime;
        }
        else
        {
            t = 0;
        }
        sliderVal = currWeightSlow / GameEventsManager.Instance.PlayerManager.MaxWeight;

        fill.color = sliderVal > 0.75 ? Color.red : Color.green;
        
        weightSlider.value = sliderVal;
    }
}
