using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class MainPlayerMove : MonoBehaviour
{
    public AudioClip doorOpen;
    public AudioClip keyPick;

    public bool hasKey = false;
    public GameObject keyFollower;
    public Sprite doorSprite;

    public GameObject winText;
    public GameObject winButton;

    public float speed;
    public float channelingSpeed;
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
    
    public GameObject tpEffectPrefab;
    public GameObject diePrefab;

    private float ctrlDisableTimer = 100f;
    public float ctrlDisableDelay;

    public bool levelX;
    public bool levelY;

    public float flipDelay = 0.2f; //time before flip happens
    private bool isFlipping;       //prevents double triggering

    public GameObject glassPanel;
    public GameObject target;
    public GameObject createdTarget;

    public float killTimer;
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
        keyFollower = GameObject.Find("FAKE KEY");
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

        //Animation
        if (animTimer > animDelay)
        {
            spriteIndex = (spriteIndex == 0) ? 1 : 0;
            animTimer = 0;
        }

        //Mirror input (with delay)
        if (controls.Player.Mirror.WasPressedThisFrame() && ctrlDisableTimer >= ctrlDisableDelay && !isFlipping)
        {
            GameObject fxMade = Instantiate(tpEffectPrefab, player.position, Quaternion.identity);
            fxMade.transform.parent = player.transform;
            //ctrlDisableTimer = 0;
            if(levelX && !levelY)
            {
                createdTarget = GameObject.Instantiate(target, new Vector3(player.position.x, -player.position.y, -1f), Quaternion.identity);
            }else if(levelY && !levelX)
            {
                createdTarget = GameObject.Instantiate(target, new Vector3(-player.position.x, player.position.y, -1f), Quaternion.identity);
            }else if(levelY && levelX)
            {
                createdTarget = GameObject.Instantiate(target, new Vector3(-player.position.x, -player.position.y, -1f), Quaternion.identity);
            }
            StartCoroutine(FlipWithDelay());
        }
        
        if(createdTarget != null)
            TargetMove();

        //Sprite logic
        if (movement.x != 0 || movement.y != 0)
            spr.sprite = sprites[spriteIndex];
        else
            spr.sprite = sprites[2 + spriteIndex];

        if ((movement.x < 0 && !flipped) || (movement.x > 0 && flipped))
            spr.flipX = true;
        else if ((movement.x > 0 && !flipped) || (movement.x < 0 && flipped))
            spr.flipX = false;


        if (hasKey)
            keyFollower.SetActive(true);
        else
            keyFollower.SetActive(false);
    }

    void TargetMove()
    {
        if(levelX && !levelY)
        {
            createdTarget.transform.position = new Vector3(player.position.x, -player.position.y, -1f);
        }else if(levelY && !levelX)
        {
            createdTarget.transform.position = new Vector3(-player.position.x, player.position.y, -1f);
        }else if(levelY && levelX)
        {
            createdTarget.transform.position = new Vector3(-player.position.x, -player.position.y, -1f);
        }
    }

    void FixedUpdate()
    {
        if (!isFlipping)
        {
            if (!flipped)
                rb.MovePosition(rb.position + (Vector2)(movement * speed * Time.fixedDeltaTime));
            else
                rb.MovePosition(rb.position - (Vector2)(movement * speed * Time.fixedDeltaTime));
        }
        else
        {
            if (!flipped)
                rb.MovePosition(rb.position + (Vector2)(movement * channelingSpeed * Time.fixedDeltaTime));
            else
                rb.MovePosition(rb.position - (Vector2)(movement * channelingSpeed * Time.fixedDeltaTime));
        }
        
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

        //KillOverlappingBoxes();

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

    public void Die()
    {
        Instantiate(diePrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("WORKING!!!!!");
        if(collision.gameObject.tag == "key" && controls.Player.Pickup.IsPressed())
        {
            Destroy(collision.gameObject);
            AudioSource.PlayClipAtPoint(keyPick, transform.position, .7f);
            hasKey = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "door" && hasKey && killTimer < killDelay)
        {
            hasKey = false;
            winText.SetActive(true);
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = doorSprite;
            AudioSource.PlayClipAtPoint(doorOpen, transform.position, .7f);
            winButton.SetActive(true);
        }
    }

    void KillOverlappingBoxes()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(
            rb.position,
            rb.GetComponent<Collider2D>().bounds.size,
            0f
        );

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("box"))
            {
                hit.GetComponent<BoxBehavior>()?.explodeBox();
            }
        }
    }

    // Disables the controls when scene unloaded
    private void OnDisable()
    {
        controls.Disable();
    }

}

