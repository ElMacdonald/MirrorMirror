using UnityEngine;

public class KeyFollowEnemy : MonoBehaviour
{
    public GameObject enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy != null){
            transform.position = enemy.transform.position;
            
        }
    }
}
