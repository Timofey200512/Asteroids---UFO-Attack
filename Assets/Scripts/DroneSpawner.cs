using System.Linq;
using UnityEngine;

public class DroneSpawner : MonoBehaviour
{
    [SerializeField] GameObject Drone;
    private void Update()
    {
        int DisabledDronesImages = GameManager.instance.GetDisabledImages(Shop.Instance.GetDronesImages()).Count();

        if(Input.GetKeyDown(KeyCode.M) && DisabledDronesImages < 3)
        {
            GameManager.instance.DisableLastImage(Shop.Instance.GetDronesImages());
            Vector2 minCameraCordinates = Camera.main.ScreenToWorldPoint(Vector2.zero);
            Vector2 maxCameraCordinates = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

            Vector2 randomPos = new Vector2(Random.Range(minCameraCordinates.x, maxCameraCordinates.x),
                Random.Range(minCameraCordinates.y, maxCameraCordinates.y));

            Instantiate(Drone, randomPos, Quaternion.identity);

        }
    }
}
