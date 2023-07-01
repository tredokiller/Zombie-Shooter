using UnityEngine;

namespace Common.CommonScripts
{
    public static class RendererTools
    {
        public static Vector3 GetRendererSize(Renderer renderer)
        {
            Bounds bounds = renderer.bounds;
            
            Vector3 rendererSize = bounds.size;

            return rendererSize;
        }
        
        public static Vector3 GetRandomPositionInRenderer(Renderer renderer)
        {
            var rendererSize = GetRendererSize(renderer);

            float randomX = Random.Range(-rendererSize.x / 2f, rendererSize.x / 2f);
            float randomY = Random.Range(-rendererSize.y / 2f, rendererSize.y / 2f);
            float randomZ = Random.Range(-rendererSize.z / 2f, rendererSize.z / 2f);
            Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);
            return randomPosition;
        }
    }
}