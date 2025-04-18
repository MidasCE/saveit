using System.Collections;
using UnityEngine;

public class WaterSplash : MonoBehaviour
{
    [SerializeField] private Renderer waterRenderer;
    [SerializeField] private string shaderProperty = "_Displacement";
    [SerializeField] private float splashValue = 1.3f;
    [SerializeField] private float startValue = 0.46f;
    [SerializeField] private float duration = 1.5f;

    private Coroutine _splashCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{other.name} entered the water!");
        if (waterRenderer == null) 
            return;
        if (_splashCoroutine != null)
            StopCoroutine(_splashCoroutine);

        waterRenderer.material.SetFloat(shaderProperty, splashValue);
        _splashCoroutine = StartCoroutine(SplashWaterThroughShader());
    }

    private IEnumerator SplashWaterThroughShader()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newVal = Mathf.Lerp(splashValue, startValue, elapsed / duration);
            waterRenderer.material.SetFloat(shaderProperty, newVal);
            yield return null;
        }
    }
}
