using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

namespace Common.CommonScripts
{
    public static class Transition
    {
        private const Ease DefaultEase = Ease.InBack;
        public delegate void TransitionFinished();
        
        public static void TransitionTo(RectTransform transform, Vector3 toPosition,
            float duration , TransitionFinished callback = null)
        {
            transform.DOMove(toPosition, duration).SetEase(DefaultEase).OnComplete(() => InvokeCallback(callback));
        }


        public static void TransitionFromTo(RectTransform transform , Vector3 fromPosition, Vector3 toPosition, float duration, 
            TransitionFinished callback= null)
        {
            transform.position = fromPosition;
            transform.DOMove(toPosition, duration).SetEase(DefaultEase).OnComplete(() => InvokeCallback(callback));
        }

        private static void InvokeCallback([CanBeNull] TransitionFinished callback)
        {
            if (callback != null)
            {
                callback.Invoke();
            }
        }
    }
}