using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] Text WavesText;

    // Start is called before the first frame update
    void Start()
    {
        GameManager gameManager = GameManager.instance;
        int normalWavesAmount = Mathf.Abs(gameManager.GetNormalCompletedWaves() - gameManager.GetHardCompletedWaves());
        int hardWavesAmount = gameManager.GetHardCompletedWaves();
        WavesText.text = $"Normal Waves: {normalWavesAmount} \n Hard waves: {hardWavesAmount}";
    }
}
