using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingController : MonoBehaviour
{
    [SerializeField] private PostProcessProfile postProcessProfile;

    private const float FarFocusDistance = 1.6f;
    private const float CloseFocusDistance = 0.55f;

    public void FocusDistance_SetClose()
    {
        SetDepthOfField(CloseFocusDistance);
    }
    
    public void FocusDistance_SetFar()
    {
        SetDepthOfField(FarFocusDistance);
    }

    private void SetDepthOfField(float value)
    {
        postProcessProfile.AddSettings<DepthOfField>().focusDistance.value = value;
    }
}
