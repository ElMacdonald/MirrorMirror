using UnityEngine;
using UnityEngine.InputSystem;


//TODO CONVERT BITS OF CODE INTO FUNCTIONS
public class MainPlayerMove : MonoBehaviour
{
    public float speed;
    public bool flipped;
    private Transform center;
    private Transform player;

    private Vector3 movement;
    private float horizontal;
    private float vertical;
    private float animTimer;
    public float animDelay;

    public Sprite[] sprites;
    public SpriteRenderer spr;
    public int spriteIndex;

    public PlayerControls controls;
    private Rigidbody2D rb;

    private float ctrlDisableTimer;
    public float ctrlDisableDelay;
    void Start()
    {
        player = transform;
        center = player.parent;
        spr = GetComponent<SpriteRenderer>();
        controls = new PlayerControls();
        controls.Enable();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        animTimer += Time.deltaTime;
        ctrlDisableTimer += Time.deltaTime;

        if (ctrlDisableTimer < ctrlDisableDelay)
            controls.Disable();
        else
            controls.Enable();

        if (animTimer > animDelay)
        {
            if (spriteIndex == 0)
            {
                spriteIndex = 1;
            }
            else
            {
                spriteIndex = 0;
            }
            animTimer = 0;
        }

        if (controls.Player.Mirror.WasPressedThisFrame())
        {
            flipped = !flipped;
            player.position = player.position * -1;
            ctrlDisableTimer = 0;
        }


        movement = controls.Player.Move.ReadValue<Vector2>();
        if (flipped == false)
            player.position += speed * Time.deltaTime * movement;
        else
            player.position += speed * Time.deltaTime * -movement;

        if (movement.x != 0 || movement.y != 0)
            spr.sprite = sprites[spriteIndex];
        else
            spr.sprite = sprites[2 + spriteIndex];

        if ((movement.x < 0 && flipped == false) || (movement.x > 0 && flipped))
            spr.flipX = true;
        else if ((movement.x > 0 && flipped == false) || (movement.x < 0 && flipped))
            spr.flipX = false;


    }
}
