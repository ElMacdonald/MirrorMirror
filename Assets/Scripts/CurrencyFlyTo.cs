using UnityEngine;
/*
 * This class handles the behavior of currency objects flying towards a target (e.g., player or UI element).
*/
public class CurrencyFlyTo : MonoBehaviour
{
    public Transform target;
    public float animLifetime = 2.0f;
    public float animTimer = 0.0f;
    public float animDelay = 2.0f;
    public float animDelayTimer = 0.0f;
    public int val = 20;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        animDelayTimer += Time.deltaTime;
        if(animDelayTimer >= animDelay)
        {
            animTimer += Time.deltaTime;
            float t = animTimer / animLifetime;
            transform.position = Vector3.Lerp(transform.position, target.position, t);
            if(animTimer >= animLifetime)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Currency collided with: " + other.gameObject.name);
        if(other.gameObject.CompareTag("Player") && animDelayTimer > 0.4f)
        {
            Debug.Log("Picked up currency: " + val);
            CurrencyTracker ct = FindFirstObjectByType<CurrencyTracker>();
            if(ct != null)
                ct.totalCurrency += val;
            Destroy(gameObject);
        }
    }
}
