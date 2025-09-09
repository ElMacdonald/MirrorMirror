using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class MainPlayerMove : MonoBehaviour
{
    public float speed;
    public bool flipped;
    private Transform player;

    private Vector3 movement;
    private float animTimer;
    public float animDelay;

    public Sprite[] sprites;
    private SpriteRenderer spr;
    private int spriteIndex;

    private PlayerControls controls;
    private Rigidbody2D rb;

    private float ctrlDisableTimer;
    public float ctrlDisableDelay;

    public bool levelX;
    public bool levelY;

    public float flipDelay = 0.2f; //time before flip happens
    private bool isFlipping;       //prevents double triggering

    public GameObject glassPanel;
    public GameObject target;

    private float killTimer;
    public float killDelay; //how long after tping you're able to kill stuff (.1f)

    void Start()
    {
        player = transform;
        spr = GetComponent<SpriteRenderer>();
        controls = new PlayerControls();
        controls.Enable();
        rb = GetComponent<Rigidbody2D>();
        glassPanel = GameObject.Find("GlassPanel");
        glassPanel.SetActive(false);
    }

    void Update()
    {
        killTimer += Time.deltaTime;
        animTimer += Time.deltaTime;
        ctrlDisableTimer += Time.deltaTime;

        if (ctrlDisableTimer >= ctrlDisableDelay)
            movement = controls.Player.Move.ReadValue<Vector2>();
        else
            movement = Vector2.zero;

        // Animation
        if (animTimer > animDelay)
        {
            spriteIndex = (spriteIndex == 0) ? 1 : 0;
            animTimer = 0;
        }

        // Mirror input (with delay)
        if (controls.Player.Mirror.WasPressedThisFrame() && ctrlDisableTimer >= ctrlDisableDelay && !isFlipping)
        {
            ctrlDisableTimer = 0;
            if(levelX && !levelY)
            {
                GameObject.Instantiate(target, new Vector3(player.position.x, -player.position.y, -1f), Quaternion.identity);
            }else if(levelY && !levelX)
            {
                GameObject.Instantiate(target, new Vector3(-player.position.x, player.position.y, -1f), Quaternion.identity);
            }else if(levelY && levelX)
            {
                GameObject.Instantiate(target, new Vector3(-player.position.x, -player.position.y, -1f), Quaternion.identity);
            }
            StartCoroutine(FlipWithDelay());
        }

        // Sprite logic
        if (movement.x != 0 || movement.y != 0)
            spr.sprite = sprites[spriteIndex];
        else
            spr.sprite = sprites[2 + spriteIndex];

        if ((movement.x < 0 && !flipped) || (movement.x > 0 && flipped))
            spr.flipX = true;
        else if ((movement.x > 0 && !flipped) || (movement.x < 0 && flipped))
            spr.flipX = false;
    }

    void FixedUpdate()
    {
        if (!flipped)
            rb.MovePosition(rb.position + (Vector2)(movement * speed * Time.fixedDeltaTime));
        else
            rb.MovePosition(rb.position - (Vector2)(movement * speed * Time.fixedDeltaTime));
    }

    IEnumerator FlipWithDelay()
    {
        
        isFlipping = true;
        yield return new WaitForSeconds(flipDelay);
        killTimer = 0f;
        if (levelX && !levelY)
            player.position = new Vector2(player.position.x, -player.position.y);
        else if (levelY && !levelX)
            player.position = new Vector2(-player.position.x, player.position.y);
        else if (levelX && levelY)
            player.position = -player.position;

        flipped = !flipped;
        

        isFlipping = false;
        glassPanel.SetActive(!glassPanel.activeSelf);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == "box" && killTimer < killDelay)
        {
            collision.gameObject.GetComponent<BoxBehavior>().explodeBox();
            Debug.Log(collision);
        }
    }
}
