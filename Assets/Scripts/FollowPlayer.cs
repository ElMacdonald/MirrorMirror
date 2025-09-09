using UnityEngine;
//Made with AI

public class FollowPlayer : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform target;       // Who to follow
    public Vector3 offset = new Vector3(-1f, 0.5f, 0f); // position behind & above target
    public float followSpeed = 5f; // how quickly it follows

    [Header("Hover Settings")]
    public float hoverAmplitude = 0.2f; // how high/low it bounces
    public float hoverFrequency = 2f;   // how fast it bounces

    private Vector3 baseOffset;

    void Start()
    {
        baseOffset = offset;
    }

    void Update()
    {
        if (target == null) return;

        // Smooth follow toward target position
        Vector3 desiredPosition = target.position + baseOffset;

        // Add hovering (sin wave on Y)
        float hover = Mathf.Sin(Time.time * hoverFrequency) * hoverAmplitude;
        desiredPosition.y += hover;

        // Smooth movement
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
    }
}
