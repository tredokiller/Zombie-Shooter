using UnityEngine;

namespace Common.CommonScripts
{
    [ExecuteInEditMode]
    public class DayTimeChanger : MonoBehaviour
    {
        [SerializeField] private Gradient directionalLightGradient;
        [SerializeField] private Gradient ambientLightGradient;

        [SerializeField, Range(1, 3600)] private float dayTimeInSeconds;
        [SerializeField, Range(0f, 1f)] private float timeProgress;

        [SerializeField] private Light directionalLight;

        private Vector3 _defaultAngles;

        private void Start()
        {
            _defaultAngles = directionalLight.transform.localEulerAngles;
        }

        private void Update()
        {
            if (Application.isPlaying)
                timeProgress += Time.deltaTime / dayTimeInSeconds;
        
            if (timeProgress > 1f)
            {
                timeProgress = 0f;
            }

            directionalLight.color = directionalLightGradient.Evaluate(timeProgress);
            RenderSettings.ambientLight = ambientLightGradient.Evaluate(timeProgress);

            directionalLight.transform.localEulerAngles =
                new Vector3(360f * timeProgress - 90f, _defaultAngles.x, _defaultAngles.z);
        }
    }
}