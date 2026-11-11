using UnityEngine;

public class Drone : Timer
{
    [SerializeField] float AngularVelocity = 130;
    Rigidbody2D rigbody2d;
    [SerializeField] GameObject Bullet;
    [SerializeField] float BulletFrequency = .3f; 
    
    void Start()
    {
        rigbody2d = GetComponent<Rigidbody2D>();
        Invoke(nameof(SelfDestroy), 7);
    }
    void SelfDestroy()
    {
        Destroy(gameObject);
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(Bullet, transform.position + transform.up, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, 1000));
        GetComponent<AudioSource>().Play();
    }

    private void Update()
    {
        rigbody2d.rotation += AngularVelocity * Time.deltaTime;
        CountDown(BulletFrequency, Shoot);
    }
}
