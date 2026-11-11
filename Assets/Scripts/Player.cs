using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Player : Timer
{
    Rigidbody2D Rigidbody2D;
    public GameObject bullet;
    public int rotationSpeed = 120;
    public float thrustSpeed;
    public ParticleSystem explosionVisual;
    public AudioClip explosionSound;
    public Animator animator;
    private bool vurnable;
    GameObject lifeManager;
    Animator anim;
    public static Player instance;
    [SerializeField] GameObject Tutorial;

    [SerializeField] bool isAutoShootingOn;
    private void Awake()
    {
        instance = this; 
    }

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        lifeManager = GameObject.Find("life manager");
        anim = GetComponent<Animator>();
        InvokeRepeating(nameof(AutoShooting), 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!vurnable)
        {
            CountDown(anim.GetCurrentAnimatorStateInfo(0).length, () => vurnable = true);
        }

        Rigidbody2D.rotation -= Time.deltaTime * rotationSpeed * Input.GetAxis("Horizontal");

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Shoot();
            Tutorial?.SetActive(false);
        }

        CheckForTeleportation();

    }
    void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, transform.position + transform.up, transform.rotation);
        newBullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, 1000));
        GetComponent<AudioSource>().Play();
    }

    void AutoShooting()
    {
        if (isAutoShootingOn && AsteroidsSpawner.instance.AsteroidsHolder.transform.childCount > 0)
        {
            Vector2 targetPos = (Vector2)AsteroidsSpawner.instance.AsteroidsHolder.transform?.GetChild(0).position;
            Vector2 diff = targetPos - (Vector2)transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
            Shoot();
        }
    }

    void CheckForTeleportation()
    {
        int maxImagesAmount = 3;
        Image[] jumpImages = Shop.Instance.GetJumpImages();
        int disabledJumpImagesAmount = GameManager.instance.GetDisabledImages(jumpImages).Count();

        if(disabledJumpImagesAmount < maxImagesAmount && Input.GetKeyDown(KeyCode.N))
        {
            Vector2 minCameraCordinates = Camera.main.ScreenToWorldPoint(Vector2.zero);
            Vector2 maxCameraCordinates = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

            Vector2 randomPos;

            void RandomizePos()
            {
                randomPos = new Vector2(UnityEngine.Random.Range(minCameraCordinates.x, maxCameraCordinates.x),
                    UnityEngine.Random.Range(minCameraCordinates.y, maxCameraCordinates.y));
            }

            Collider2D GetColliderAtPos(Vector2 position)
            {
                return Physics2D.OverlapBox(position, GetComponent<BoxCollider2D>().size, Rigidbody2D.rotation);
            }

            RandomizePos();

            while (GetColliderAtPos(randomPos) != null)
            {
                RandomizePos();
            }

            GameManager.instance.DisableLastImage(jumpImages);
            transform.position = randomPos;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            Rigidbody2D.AddRelativeForce(new Vector2(0, thrustSpeed));
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void BecomeUnvurnable()
    {
        vurnable = false;
        anim.Play("Invulnerable animation", 0);
    }

    void Respawn()
    {
        transform.position = Vector2.zero;
        gameObject.SetActive(true);
        transform.rotation = Quaternion.identity;
        BecomeUnvurnable();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(vurnable && !collision.CompareTag("Drone bullet") && !collision.CompareTag("Drone"))
        {
            Lifes lifesScript = lifeManager.GetComponent<Lifes>();
            lifesScript.RemoveLife();
            lifesScript.LoadGameOverSceneIfOutOfLives();

            Instantiate(explosionVisual, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);

            Invoke(nameof(Respawn), 3);
            gameObject.SetActive(false);
        }
    }
}
