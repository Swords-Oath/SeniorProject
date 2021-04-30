using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadfill : MonoBehaviour
{
    private float fill = 0;
    private float time;
    private Image Object;
    private bool runningClockWise = true;
    private bool runningCounterWise = false;

    private void Start()
    {
        Object = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(runningClockWise == true)
        {
            if (fill > 0)
            {
                if (time >= 0.05f)
                {
                    fill -= 0.01f;
                    Object.fillAmount = fill;
                    time = 0;
                }
            }
            else
            {
                runningClockWise = false;
                Object.fillClockwise = false;
                runningCounterWise = true;
                time = 0;


            }

        }
        else if(runningCounterWise == true)
        {
            if (fill < 1)
            {
                if (time >= 0.05f)
                {
                    fill += 0.01f;
                    Object.fillAmount = fill;
                    Object.fillClockwise = false;
                    time = 0;
                }
            }
            else
            {
                runningClockWise = true;
                Object.fillClockwise = true;
                runningCounterWise = false;
                time = 0;



            }

        }
    }
}
