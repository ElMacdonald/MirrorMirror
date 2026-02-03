using UnityEngine;

public class DeathAnimDoer : MonoBehaviour
{
    public Sprite[] deathSprites;
    public float startSpriteTimer;
    public float startSpriteDelay;
    public float animTimer;
    public float animDelay;
    public SpriteRenderer spriteRenderer;
    public int curFrame;
    public GameOverScreen gOver;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startSpriteTimer = 0f;
        gOver = FindObjectOfType<GameOverScreen>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleAnims();
    }

    void HandleAnims()
    {
        startSpriteTimer += Time.deltaTime;
        if (startSpriteTimer >= startSpriteDelay)
        {
            animTimer += Time.deltaTime;
            if (animTimer >= animDelay)
            {
                animTimer = 0f;
                if (curFrame < deathSprites.Length)
                {
                    spriteRenderer.sprite = deathSprites[curFrame];
                    curFrame++;
                }
                else
                {
                    gOver.EnableGameOver();
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
