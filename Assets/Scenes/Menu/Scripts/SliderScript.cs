using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
   private Slider slider;

   private void Awake()
   {
      slider = GetComponent<Slider>();
   }

   public void SetSliderValue(float value)
   {
      slider.value = value;
   }
}
