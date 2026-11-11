using UnityEngine;

public class UFOscript : MonoBehaviour
{
    public GameObject ufoBullet;
    public int horizontalSpeed = 2;
    [SerializeField] int verticalSpeed = 2;
    public ParticleSystem explosion;
    [SerializeField] float magnitude = .5f;

    // Start is called before the first frame update
    void Start()
    {

        horizontalSpeed *= Random.Range(0, 2) * 2 - 1;
        
        InvokeRepeating(nameof(Shoot), Random.Range(2, 4), Random.Range(2, 5));
        InvokeRepeating(nameof(RandomizeMagnitude), 2, 4);
        InvokeRepeating(nameof(ChangeDirection), magnitude, magnitude);
    }

    void RandomizeMagnitude()
    {
        magnitude = Random.Range(.5f, 2);
    } 

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(horizontalSpeed * Time.deltaTime, Time.deltaTime * verticalSpeed);    
    }
    void ChangeDirection()
    {
        verticalSpeed = -verticalSpeed;
    }

    void Shoot()
    {
        transform.up = new Vector3(Random.Range(-100,100), Random.Range(-100,100)) - transform.position;
        GameObject newBullet = Instantiate(ufoBullet, transform.position + transform.up, transform.rotation); 
        transform.up = new Vector2(0,1);
        newBullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, 300));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bullet") || other.CompareTag("Drone bullet"))
        {
            Score.instance.AddScore(1000);
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
