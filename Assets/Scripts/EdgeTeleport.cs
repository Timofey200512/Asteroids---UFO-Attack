using UnityEngine;

public class EdgeTeleport : Timer
{
    float screenXcordinates;
    float screenYcordinates;

    // Update is called once per frame
    void Update()
    {
        screenXcordinates = Camera.main.WorldToViewportPoint(transform.position).x;
        screenYcordinates = Camera.main.WorldToViewportPoint(transform.position).y;

        if(screenXcordinates > 1.1)
            transform.position = new Vector2(-transform.position.x + .5f, transform.position.y);
        
        if(screenXcordinates < -0.1)
            transform.position = new Vector2(-transform.position.x - .5f, transform.position.y);

        if(screenYcordinates > 1.1)
            transform.position = new Vector2(transform.position.x, -transform.position.y + .5f);
        
        if(screenYcordinates < -0.1)
            transform.position = new Vector2(transform.position.x, -transform.position.y - .5f);

        bool outOfScreen = screenXcordinates > 1 || screenYcordinates > 1 || screenXcordinates < 0 || screenYcordinates < 0;

        if(outOfScreen)
        {
            CountDown(6, MoveToCenter);
        }
        else
        {
            timer = 0; 
        }

    }

    void MoveToCenter()
    {
        Rigidbody2D ridBody2d = GetComponent<Rigidbody2D>();
        ridBody2d.bodyType = RigidbodyType2D.Static;
        ridBody2d.bodyType = RigidbodyType2D.Dynamic;
        ridBody2d.AddForce(-transform.position * 7);
    }

}
