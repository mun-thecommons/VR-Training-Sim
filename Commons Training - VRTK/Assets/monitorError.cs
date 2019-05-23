using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monitorError : MonoBehaviour
{

    
    private int childCount;
    private GameObject[] childMonitors;
    int i = 0;
    int index;
    // Start is called before the first frame update
    void Start()
    {
        
        childCount = gameObject.transform.childCount;
        childMonitors = new GameObject[childCount];
        Debug.Log("count of children is "+childCount);


        foreach (Transform child in transform)
        {
            childMonitors[i] = child.gameObject;
            i += 1;
        }
        Debug.Log(childMonitors[2]+ " is a second child object");

        index = Random.Range(0, childMonitors.Length);

        childMonitors[index].GetComponent<MonitorController>().enabled = true;
        //childMonitors[2].GetComponent<MonitorController>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {



       

    }
}
