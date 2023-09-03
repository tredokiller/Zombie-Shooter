using System;
using UnityEngine;
namespace Common.CommonScripts
{
    public class UITransitioner : MonoBehaviour
    {
        [SerializeField] private RectTransform fromPosition;
        [SerializeField] private RectTransform toPosition;

        [SerializeField] private RectTransform transitionObject;
        
        [SerializeField , Range(0, 5)] private float duration;


        public Action OnTransitionFromToCompleted;
        public Action OnTransitionToFromCompleted;
        
        public void PlayTransitionFromTo(Transition.TransitionFinished callback = null)
        {
            Transition.TransitionFromTo(transitionObject , fromPosition.position, toPosition.position , duration, callback);
        }

        public void PlayTransitionToFrom(Transition.TransitionFinished callback = null)
        {
            Transition.TransitionFromTo(transitionObject , toPosition.position, fromPosition.position , duration , callback);
        }
    }
}