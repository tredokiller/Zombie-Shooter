using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
   private Slider _slider;

   private void Awake()
   {
      _slider = GetComponent<Slider>();
   }

   public void SetSliderValue(float value)
   {
      _slider.value = value;
   }
}
