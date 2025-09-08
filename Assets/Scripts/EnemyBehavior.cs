using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private Transform enemy;
    private int health;
    public float speed;
    private float detectionTime;
    private float detectTimer;
    public bool mirrorEnemy;
    public MainPlayerMove player;
    private bool targetMirrored;
    void Start()
    {
        
    }

    void Update()
    {
        targetMirrored = player.flipped;
        if (mirrorEnemy == targetMirrored)
        {
            //enemy.lookAt(player.tr)
        }
    }
}
