using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    protected float timer;
    public delegate void function();
    public function functionToRun;

    public void CountDown(float neededTime, function function){
        
        if(neededTime > timer){
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            function();
        }
    }
}
