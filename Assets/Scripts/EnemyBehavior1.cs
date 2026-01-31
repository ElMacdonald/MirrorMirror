using UnityEngine;

public class EnemyBehavior1 : MonoBehaviour
{
    public float speed;
    public bool detected;
    public Vector3 spawnPoint;
    public bool isAlive;
    public GameObject player;
    public float animTimer;
    public float animInterval = 0.4f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        spawnPoint = transform.position;
        isAlive = true;
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
            
        }
    }

    public void lineOfSight(){
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, Mathf.Infinity);
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Player")){
            detected = true;
        } else {
            detected = false;
        }
    }
}
