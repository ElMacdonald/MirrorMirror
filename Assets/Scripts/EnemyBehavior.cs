using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float speed;
    public bool detected;
    public Vector3 spawnPoint;
    public bool isAlive;
    public GameObject player;
    public float animTimer;
    public float animInterval = 0.4f;

    public Sprite[] idleSprites;
    public Sprite[] angrySprites;
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        spawnPoint = transform.position;
        isAlive = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (detected)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, spawnPoint, speed * Time.deltaTime);
        }
        lineOfSight();
        handleAnims();
    }

    void handleAnims()
    {
        animTimer += Time.deltaTime;
        if (animTimer >= animInterval)
        {
            animTimer = 0f;
            if (detected)
            {
                int index = Random.Range(0, angrySprites.Length);
                spriteRenderer.sprite = angrySprites[index];
            }
            else
            {
                int index = Random.Range(0, idleSprites.Length);
                spriteRenderer.sprite = idleSprites[index];
            }
        }
    }

    // ignores own colliders to check for line of sight to player
    public void lineOfSight()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;

        RaycastHit2D[] hits = Physics2D.RaycastAll(
            transform.position,
            directionToPlayer.normalized,
            Mathf.Infinity
        );

        detected = false;

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject == gameObject)
                continue;
            if (hit.collider.gameObject == player)
                detected = true;

            break;
        }
    }

}
