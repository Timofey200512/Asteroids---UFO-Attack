using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : EdgeTeleport
{
    float mediumAsteroidSize = .8f;
    float smallAsteroidSize = .3f;
    AudioSource audioSource;
    public ParticleSystem explosion;

    void Start(){
        audioSource = GetComponent<AudioSource>();
    }
    public AudioClip[] explosionSounds;
    void OnTriggerEnter2D(Collider2D other){
        bool BulletHIT = other.CompareTag("Bullet") || other.CompareTag("Drone bullet");

        if(BulletHIT){

            if(transform.localScale.x == 1.5f){         
                playRandomSoundAndMakeExplosion();
                createASmallerAsteroid(other, mediumAsteroidSize);
                Score.instance.AddScore(20);
            }
            
            else if(transform.localScale.x == .8f){
                playRandomSoundAndMakeExplosion();
                createASmallerAsteroid(other, smallAsteroidSize);
                Score.instance.AddScore(50);
            }

            else{
                playRandomSoundAndMakeExplosion();
                Score.instance.AddScore(100);

                Destroy(gameObject);
                Destroy(other.gameObject);
                
            }
            
        }
    }
    void playRandomSoundAndMakeExplosion()
    {
        audioSource.clip = explosionSounds[Random.Range(0,2)];
        AudioSource.PlayClipAtPoint(explosionSounds[Random.Range(0,2)], transform.position);  
        Instantiate(explosion, transform.position, transform.rotation);

    }
    void createASmallerAsteroid(Collider2D other, float size)
    {
        AsteroidsSpawner.instance.MakeAnAsteroid(gameObject, 100, transform.position, new Vector2(size, size));
        AsteroidsSpawner.instance.MakeAnAsteroid(gameObject, 100, transform.position, new Vector2(size, size));
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
