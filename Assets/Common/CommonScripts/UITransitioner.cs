using UnityEngine;
namespace Common.CommonScripts
{
    public class UITransitioner : MonoBehaviour
    {
        [SerializeField] private RectTransform fromPosition;
        [SerializeField] private RectTransform toPosition;

        [SerializeField] private RectTransform transitionObject;
        
        [SerializeField , Range(0, 5)] private float duration;
        

        public void PlayTransitionFromTo()
        {
            Transition.TransitionFromTo(transitionObject , fromPosition.position, toPosition.position , duration);
        }

        public void PlayTransitionToFrom()
        {
            Transition.TransitionFromTo(transitionObject , toPosition.position, fromPosition.position , duration);
        }
    }
}