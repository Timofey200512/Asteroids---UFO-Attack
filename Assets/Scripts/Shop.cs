using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Shop : Timer
{
    public static Shop Instance;

    [SerializeField] private Image[] jumpImages;

    [SerializeField] Image[] dronesImages;

    public enum ItemPrices
    {
        jump = 2000,
        drones = 4000,
        UFOLevel = 10000
    }

    private void Awake()
    {
        Instance = this; 
    }

    GameManager gameManager;
    private void Start()
    {
        gameObject.SetActive(false);
        gameManager = GameManager.instance;
    }

    public Image[] GetJumpImages()
    {
        return jumpImages;
    }

    public Image[] GetDronesImages()
    {
        return dronesImages;
    }

    public void SetNextUFOWave()
    {
        if (Score.instance.ScoreNumber >= (int)ItemPrices.UFOLevel && !gameManager.isNextWaveUFO)
        {
            Score.instance.RemoveScore((int)ItemPrices.UFOLevel);
            gameManager.isNextWaveUFO = true;
        }
    }

    public void AddJump()
    {
        if (Score.instance.ScoreNumber >= (int)ItemPrices.jump && gameManager.GetDisabledImages(jumpImages).Count() > 0)
        {
            Score.instance.RemoveScore((int)ItemPrices.jump);
            gameManager.EnableFirstImage(jumpImages);
        }
    }

    public void AddDrone()
    {
        if (Score.instance.ScoreNumber >= (int)ItemPrices.drones && gameManager.GetDisabledImages(dronesImages).Count() > 0)
        {
            Score.instance.RemoveScore((int)ItemPrices.drones);
            gameManager.EnableFirstImage(dronesImages);
        }
    }

}
