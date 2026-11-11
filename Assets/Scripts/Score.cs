using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text ScoreText;
    public int ScoreNumber;
    public static Score instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        ScoreText.text = ScoreNumber.ToString();
    }

    public void AddScore(int score){
        ScoreNumber += score;
        ScoreText.text = ScoreNumber.ToString();
    }

    public void RemoveScore(int RemoveAmount)
    {
        if(ScoreNumber >= RemoveAmount)
        {
            ScoreNumber -= RemoveAmount;
            ScoreText.text = ScoreNumber.ToString();
        }
    }
}
