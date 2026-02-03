using UnityEngine;

public class TPAnimEffect : MonoBehaviour
{
    public float lifeDuration;
    public float animDelay;
    public float animTimer;
    public float lifeTimer;
    public Sprite[] animSprites;
    public SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleAnims();
        lifeTimer += Time.deltaTime;
        if(lifeTimer >= lifeDuration)
        {
            Destroy(this.gameObject);
        }
    }

    void HandleAnims()
    {
        animTimer += Time.deltaTime;
        if (animTimer >= animDelay)
        {
            animTimer = 0f;
            if (animSprites.Length > 0)
            {
                spriteRenderer.sprite = animSprites[0];
                Sprite[] newArray = new Sprite[animSprites.Length - 1];
                for (int i = 1; i < animSprites.Length; i++)
                {
                    newArray[i - 1] = animSprites[i];
                }
                animSprites = newArray;
            }
        }
    }
}
