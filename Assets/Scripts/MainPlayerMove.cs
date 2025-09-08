using UnityEngine;

public class MainPlayerMove : MonoBehaviour
{
    public float speed;
    private bool flipped;
    private Transform center;
    private Transform player;

    private Vector2 movement;
    private float horizontal;
    private float vertical;
    private float animTimer;
    public float animDelay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = transform;
        center = player.parent;
    }

    // Update is called once per frame
    void Update()
    {
        animTimer += Time.deltaTime;

    }
}
