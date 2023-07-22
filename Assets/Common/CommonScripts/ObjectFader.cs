using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common.CommonScripts
{
    public class ObjectFader : MonoBehaviour
    {
        [SerializeField] private float fadeSpeed;

        private float _originalOpacity;
        private List<Material> _materials;
        private const float FadeAmount = 0.1f;

        private bool _doFade;

        private void Awake()
        {
            _materials = new List<Material>();
            foreach (var material in GetComponent<Renderer>().materials)
            {
                if (material.GetColor("_Color") == null)
                {
                    continue;
                }
                _materials.Add(material);
            }

            _originalOpacity = _materials.Last().color.a;

        }
        
        void Update()
        {
            if (_doFade)
            {
                Fade();
            }
            else
            {
                ResetFade();
            }
        }

        private void Fade()
        {
            foreach (var material in _materials)
            {
                Color currentColor = material.color;
                Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b,
                    Mathf.Lerp(currentColor.a, FadeAmount, fadeSpeed * Time.deltaTime));
                
                material.color = smoothColor;
            }
        }

        private void ResetFade()
        {
            foreach (var material in _materials)
            {
                Color currentColor = material.color;
                Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, 
                    Mathf.Lerp(currentColor.a, 1, fadeSpeed * Time.deltaTime));

                material.color = smoothColor;
            }   
        }

        public void DoFade(bool isFade)
        {
            if (isFade)
            {
                _doFade = true;
                return;
            }

            _doFade = false;
        }
        
        
    }
}
