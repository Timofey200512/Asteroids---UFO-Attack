using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(SelfDestroy), 2);
    }

    void SelfDestroy(){
        Destroy(gameObject);
    }
}
