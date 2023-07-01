using DG.Tweening;
using UnityEngine;

namespace Common.CommonScripts
{
    public static class Transition
    {
        private const Ease DefaultEase = Ease.InElastic;

        public static void TransitionTo(RectTransform transform, Vector3 toPosition,
            float duration)
        {
            transform.DOMove(toPosition, duration).SetEase(DefaultEase);
        }


        public static void TransitionFromTo(RectTransform transform , Vector3 fromPosition, Vector3 toPosition, float duration)
        {
            transform.position = fromPosition;
            transform.DOMove(toPosition, duration).SetEase(DefaultEase);
        }
    }
}