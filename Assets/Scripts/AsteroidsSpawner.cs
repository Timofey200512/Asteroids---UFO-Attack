using System.Collections.Generic;
using UnityEngine;

public class AsteroidsSpawner : Timer
{
    public static AsteroidsSpawner instance;
    List<Vector2> NoSpawnZoneCordinates = new List<Vector2>();
    protected List<Vector2> allCordinates = new List<Vector2>();
    Camera cam;
    public GameObject asteroid;
    public List<Sprite> AsteroidsDrawings = new List<Sprite>();
    GameObject asteroidObject;
    [SerializeField] int asteroidsAmount = 1;
    [SerializeField] private GameObject _asteroidsHolder;
    public GameObject AsteroidsHolder { get { return _asteroidsHolder; } }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        
        cam = Camera.main;

        for(int y = -1; y < 2; y++){
            for(int x = -1; x < 2; x++){
                NoSpawnZoneCordinates.Add(new Vector2(x, y));
            }
        }


        Vector2 LeftBottom = new Vector2(Mathf.Round(cam.ScreenToWorldPoint(new Vector2(0,0)).x), Mathf.Round(cam.ScreenToWorldPoint(new Vector2(0,0)).y));

        for(int a = 0; a < 2 * cam.orthographicSize; a++){
            for(int i = 0; i < cam.aspect * 2 * cam.orthographicSize; i++){
                    allCordinates.Add(new Vector2(i + LeftBottom.x, a + LeftBottom.y));
            }
        }


        foreach (Vector2 i in NoSpawnZoneCordinates){
            allCordinates.Remove(i);
        }

        MakeNumberOfAsteroids(asteroidsAmount);
        asteroidsAmount += 2;
    }
    
    public void ResetAsteroidsForAsteroidScript(){
        CountDown(2, ResetAsteroids);
    }
    public void ResetAsteroids()
    {
        MakeNumberOfAsteroids(asteroidsAmount);
        asteroidsAmount += 2;
    }

    protected virtual void MakeNumberOfAsteroids(int amount){

        for(int i = 0; i < amount; i++){
            MakeAnAsteroid(asteroid, 60, allCordinates[Random.Range(0, allCordinates.Count - 1)], new Vector2(1.5f, 1.5f));
        }
    }

    public void MakeAnAsteroid(GameObject asteroid, int force, Vector2 pos, Vector2 scale)
    {
        asteroidObject = Instantiate(asteroid, pos, transform.rotation);
        asteroidObject.GetComponent<Transform>().localScale = scale;
        asteroidObject.GetComponent<SpriteRenderer>().sprite = AsteroidsDrawings[Random.Range(0,3)];
        asteroidObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-force, force), Random.Range(-force, force)));
        asteroidObject.transform.SetParent(AsteroidsHolder.transform);
    }

}
