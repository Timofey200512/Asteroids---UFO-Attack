using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections;

public class Lifes : MonoBehaviour
{
    [SerializeField] Image[] LifeImages;
    public void RemoveLife()
    {
        GameManager.instance.DisableLastImage(LifeImages);
    }

    public void LoadGameOverSceneIfOutOfLives()
    {
        StartCoroutine(LoadGameOverSceneCoroutine());
    }

    IEnumerator LoadGameOverSceneCoroutine()
    {
        int EnabledImagesCount = GameManager.instance.GetEnabledImages(LifeImages).Count();

        if (EnabledImagesCount == 0)
        {
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("Game Over");
        }

    }
}
