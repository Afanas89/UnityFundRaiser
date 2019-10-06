using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TimerController : MonoBehaviour
{
    public Text timerInfo;
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timerInfo.text = "Осталось "+GameEngine.GetTime()+" сек" ;
    }
}
