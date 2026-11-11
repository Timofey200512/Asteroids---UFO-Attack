using UnityEngine;

public class UFOSpawnerScript : Timer
{
    public GameObject ufo;
    Vector2 CamLeftButtom;
    Vector2 CamRightTop;
    Camera cam;
    public static UFOSpawnerScript instance;
    [SerializeField] private GameObject _UFOHolder;
    public GameObject UFOHolder { get { return _UFOHolder; } }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        
        CamLeftButtom = cam.ScreenToWorldPoint(new Vector2(0,0));
        CamRightTop = cam.ScreenToWorldPoint(new Vector2(cam.pixelWidth, cam.pixelHeight));
    }

    // Update is called once per frame
    void Update()
    {
        bool shopIsVisible = Shop.Instance.gameObject.activeSelf;

        if(!shopIsVisible)
            CountDown(Random.Range(30, 40), CreateUfoAtRandomPos);
    }
    public void CreateUfoAtRandomPos()
    {
        GameObject UFO = Instantiate(ufo, new Vector2(Random.Range(CamLeftButtom.x, CamRightTop.x), Random.Range(CamLeftButtom.y, CamRightTop.y)), transform.rotation);
        UFO.transform.SetParent(UFOHolder.transform);
    }
}
