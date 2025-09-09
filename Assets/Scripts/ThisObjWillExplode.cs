using UnityEngine;

public class ThisObjWillExplode : MonoBehaviour
{
    private float timer;
    public float timeToBlow;
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > timeToBlow)
        {
            Destroy(gameObject);
        }
    }
}
