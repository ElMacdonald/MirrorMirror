using UnityEngine;
using UnityEngine.UI;

public class UIImageShimmer : MonoBehaviour
{
    public float speed = 5f;
    public float intensity = 0.5f; 

    private Image img;
    private Color baseColor;

    void Start()
    {
        img = GetComponent<Image>();
        baseColor = img.color;
    }

    void Update()
    {
        float t = (Mathf.Sin(Time.time * speed) + 1f) / 2f;
        float brightness = Mathf.Lerp(1f - intensity, 1f, t);

        img.color = baseColor * brightness;
    }
}
