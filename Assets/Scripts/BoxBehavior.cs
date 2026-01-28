using UnityEngine;

public class BoxBehavior : MonoBehaviour
{
    private CameraShaker cs;

    public GameObject debrisPrefab;   //prefab of debris (ParticleSystem or sprite chunks)
    public GameObject currencyPrefab; //prefab of currency to spawn
    public int debrisCount = 10;      //how many pieces to spawn
    public float debrisForce = 5f;    //explosion force
    public int currencyAmount = 5;

    public AudioClip breakSound;      //sound to play on destruction
    public float soundVolume = 1f;

    private void Start()
    {
        cs = GameObject.Find("Camera Shaker").GetComponent<CameraShaker>();
    }

    public void explodeBox()
    {
        //Screen shake
        cs.Shake(.3f, .3f);

        //Play break sound
        if (breakSound != null)
        {
            AudioSource.PlayClipAtPoint(breakSound, transform.position, soundVolume);
        }

        //Spawn debris
        if (debrisPrefab != null)
        {
            for (int i = 0; i < debrisCount; i++)
            {
                GameObject debris = Instantiate(
                    debrisPrefab,
                    transform.position,
                    Quaternion.identity
                );

                //Give each piece a random push
                Rigidbody2D rb = debris.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 dir = Random.insideUnitCircle.normalized;
                    rb.AddForce(dir * debrisForce, ForceMode2D.Impulse);
                    rb.AddTorque(Random.Range(-200f, 200f));
                }

                //Auto destroy debris after a short time
                Destroy(debris, 5f);
            }
            for (int i = 0; i < currencyAmount; i++)
            {
                Instantiate(
                    currencyPrefab,
                    transform.position,
                    Quaternion.identity
                );
                Rigidbody2D rb = debrisPrefab.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 dir = Random.insideUnitCircle.normalized;
                    rb.AddForce(dir * debrisForce, ForceMode2D.Impulse);
                    rb.AddTorque(Random.Range(-200f, 200f));
                }
            }
        }

        // Destroy box
        Destroy(gameObject);
    }
}
