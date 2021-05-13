// Programs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadfill : MonoBehaviour
{
    // Variables for time it takes to complete one load and fill amount
    private float fill = 0;
    private float time;

    // Image loading
    private Image Object;

    // Direction it's loading in
    private bool runningClockWise = true;
    private bool runningCounterWise = false;

    private void Start()
    {
        // Finds Loading Image
        Object = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        // Adds to the time spent loading
        time += Time.deltaTime;

        if(runningClockWise == true)
        {
            if (fill > 0)
            {
                // if time is less than one rotation of time continue loading current direction
                if (time >= 0.05f)
                {
                    fill -= 0.01f;
                    Object.fillAmount = fill;
                    time = 0;
                }
            }
            else
            {
                // else reset values and move opposite direction

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
                // if time is less than one rotation of time continue loading current direction
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
                // else reset values and move opposite direction

                runningClockWise = true;
                Object.fillClockwise = true;
                runningCounterWise = false;
                time = 0;



            }

        }
    }
}
