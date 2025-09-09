using UnityEngine;

public class KeyScript : MonoBehaviour
{
    private PlayerControls controls;
    private void Start()
    {
        controls = new PlayerControls();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && controls.Player.Pickup.WasPressedThisFrame())
        {
            collision.gameObject.GetComponent<MainPlayerMove>().hasKey = true;
            Destroy(gameObject);
        }
    }
}
