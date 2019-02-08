using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPrinters : MonoBehaviour {

    public static bool icomprt1Full;
    public static bool icomprt2Full;
    public static bool icomprt3Full;

    private float time;
    private float wait = 1f;

    private int min = 1;
    private int sec = 0;

    void Update()
    {
        if (icomprt1Full || icomprt2Full || icomprt3Full)
        {
            time += Time.deltaTime;
            if (time > wait)
            {
                sec += 1;

                if (sec > 59)
                {
                    min += 1;
                    sec = 0;
                }

                time = 0f;
            }

            if ((min % Random.Range(2, 5)) == 0)
            {
                icomprt1Full = false;
            }
            if ((min % Random.Range(2, 5)) == 0)
            {
                icomprt2Full = false;
            }
            if ((min % Random.Range(2, 5)) == 0)
            {
                icomprt3Full = false;
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "icomprt1")
        {
            icomprt1Full = true;
            GetComponent<Renderer>().enabled = false;
        }
       if (col.gameObject.name == "icomprt2")
        {
            icomprt2Full = true;
            GetComponent<Renderer>().enabled = false;
        }
       if (col.gameObject.name == "icomprt3")
        {
            icomprt3Full = true;
            GetComponent<Renderer>().enabled = false;
        }
    }
}
