using UnityEngine;
[ExecuteInEditMode]
public class DayTimeChanger : MonoBehaviour
{
    [SerializeField] private Gradient _directionalLightGradient;
    [SerializeField] private Gradient _ambientLightGradient;

    [SerializeField, Range(1, 3600)] private float _dayTimeInSeconds;
    [SerializeField, Range(0f, 1f)] private float _timeProgress;

    [SerializeField] private Light _directionalLight;

    private Vector3 _defaultAngles;
    void Start() => _defaultAngles = _directionalLight.transform.localEulerAngles;

    private void Update()
    {
        if (Application.isPlaying)
            _timeProgress += Time.deltaTime / _dayTimeInSeconds;
        
        if (_timeProgress > 1f)
        {
            _timeProgress = 0f;
        }

        _directionalLight.color = _directionalLightGradient.Evaluate(_timeProgress);
        RenderSettings.ambientLight = _ambientLightGradient.Evaluate(_timeProgress);

        _directionalLight.transform.localEulerAngles =
            new Vector3(360f * _timeProgress - 90f, _defaultAngles.x, _defaultAngles.z);
    }
}
