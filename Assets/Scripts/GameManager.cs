using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] TextMeshProUGUI waveText;
    int normalWavesCompleted = 0;
    [SerializeField] GameObject Shop;
    int hardWavesCompleted = 0;
    [SerializeField] TextMeshProUGUI PauseText;
    public int GetHardCompletedWaves()
    {
        return hardWavesCompleted;
    }

    int CurrentNormalWave
    {
        get { return normalWavesCompleted + 1; }
        set { normalWavesCompleted = value; }
    }

    public int GetNormalCompletedWaves()
    {
        return normalWavesCompleted;
    }

    private void Awake()
    {
        instance = this;
    }
    public int GetAsteroidsAmount()
    {
        return AsteroidsSpawner.instance.AsteroidsHolder.GetComponent<Transform>().childCount;
    }
    
    int GetUFOAmount()
    {
        return UFOSpawnerScript.instance.UFOHolder.GetComponent<Transform>().childCount;
    }

    bool waveBuilt = true;
    public void HideShop()
    {
        Shop.SetActive(false);
    }

    void PauseGame(bool isPaused) 
    {
        AudioListener.pause = isPaused;
        Player.instance.enabled = !isPaused;
        PauseText.enabled = isPaused;
    }

    void CheckForPause()
    {
        bool pauseButtonPressed = Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape);

        if (pauseButtonPressed && !PauseText.enabled)
        {
            Time.timeScale = 0;
            PauseGame(true);
        }
        else if (pauseButtonPressed && PauseText.enabled)
        {
            Time.timeScale = 1;
            PauseGame(false);
        }
    }

    private void Update()
    {
        CheckForPause();
        bool waveCompleted = waveBuilt && GetAsteroidsAmount() == 0 && GetUFOAmount() == 0;
        if (waveCompleted && !Shop.activeSelf)
        {
            normalWavesCompleted++;
            Shop.SetActive(true);

            if (isNextWaveUFO)
            {
                isNextWaveUFO = false;
                hardWavesCompleted++;
            }
            waveBuilt = false;
        }

        waveText.text = "Wave " + CurrentNormalWave;
    }

    IEnumerator LoadUFOLevel()
    {
        for(int i = 0; i < 15; i++)
        {
            UFOSpawnerScript.instance.CreateUfoAtRandomPos();
            yield return new WaitForSeconds(1);
        }
        waveBuilt = true;
    }

    void ResetAsteroids()
    {
        AsteroidsSpawner.instance.ResetAsteroids();
        waveBuilt = true;
    }

    public void CreateUFOWave()
    {
        StartCoroutine(LoadUFOLevel());
    }

    public bool isNextWaveUFO;

    public void LoadNextWave()
    {
        if(isNextWaveUFO)
        {
            StartCoroutine(LoadUFOLevel());
        }
        else
        {
            Invoke(nameof(ResetAsteroids), 1);
        }
    }

    public void DisableLastImage(Image[] Images)
    {
        Image[] allEnabledImages = Images.Where(Image => Image.enabled == true).ToArray();
        Image lastImage = allEnabledImages.Last();
        lastImage.enabled = false;
    }
    public Image[] GetDisabledImages(Image[] Images)
    {
        return Images.Where(Image => Image.enabled == false).ToArray();
    }
    public void EnableFirstImage(Image[] Images)
    {
        Image firstImage = GetDisabledImages(Images).First();
        firstImage.enabled = true;   
    }
    public Image[] GetEnabledImages(Image[] Images)
    {
        return Images.Where(Image => Image.enabled == true).ToArray();

    }
}
