using UnityEngine;

public class LeverScript : MonoBehaviour
{
    public GameObject doorToOpen;
    public Sprite usedSprite;
    public bool usable = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(usable && Input.GetKeyDown(KeyCode.F))
        {
            doorToOpen.SetActive(false);
            GetComponent<SpriteRenderer>().sprite = usedSprite;
            usable = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            usable = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            usable = false;
        }
    }
}
