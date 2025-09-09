using UnityEngine;
using System.Collections;


//made with AI

public class CameraShaker : MonoBehaviour
{
    private static CameraShaker instance;
    private Transform camTransform;
    private Vector3 originalPos;
    private Coroutine shakeRoutine;

    void Awake()
    {
        // Singleton-style setup so you can call CameraShaker.Instance.Shake()
        if (instance == null)
        {
            instance = this;
            camTransform = Camera.main.transform;
            originalPos = camTransform.localPosition;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static CameraShaker Instance
    {
        get
        {
            if (instance == null)
            {
                // if no shaker exists, create one
                GameObject shakerObj = new GameObject("CameraShaker");
                instance = shakerObj.AddComponent<CameraShaker>();
            }
            return instance;
        }
    }

    /// <summary>
    /// Call this to shake the camera.
    /// </summary>
    /// <param name="intensity">How strong the shake is.</param>
    /// <param name="duration">How long the shake lasts.</param>
    public void Shake(float intensity, float duration)
    {
        if (shakeRoutine != null)
            StopCoroutine(shakeRoutine);

        shakeRoutine = StartCoroutine(DoShake(intensity, duration));
    }

    private IEnumerator DoShake(float intensity, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            Vector3 randomPoint = originalPos + (Vector3)Random.insideUnitCircle * intensity;
            camTransform.localPosition = randomPoint;

            elapsed += Time.deltaTime;
            yield return null;
        }

        camTransform.localPosition = originalPos;
        shakeRoutine = null;
    }
}
