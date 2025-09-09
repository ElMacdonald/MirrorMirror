using UnityEngine;
using UnityEngine.UI;

public class UIImageShimmer : MonoBehaviour
{
    public float speed = 5f;       // how fast it shimmers
    public float intensity = 0.5f; // how strong the shimmer is

    private Image img;
    private Color baseColor;

    void Start()
    {
        img = GetComponent<Image>();
        baseColor = img.color;
    }

    void Update()
    {
        // Oscillates between 0–1
        float t = (Mathf.Sin(Time.time * speed) + 1f) / 2f;

        // Blend brightness
        float brightness = Mathf.Lerp(1f - intensity, 1f, t);

        img.color = baseColor * brightness;
    }
}
