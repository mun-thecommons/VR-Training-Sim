using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PhoneController : MonoBehaviour
{
    public bool pluggedIn = false;

    public GameObject ethernetConnector;
    public GameObject phone;
    public LineRenderer ethernetCable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!pluggedIn && ethernetCable.gameObject.activeSelf)
        {
            ethernetCable.SetPosition(0, phone.transform.localPosition);
            ethernetCable.SetPosition(1, ethernetConnector.transform.localPosition);
        }
       
        if (pluggedIn && ethernetConnector.activeSelf)
        {
            ethernetConnector.SetActive(false);
            ethernetCable.gameObject.SetActive(false);
        }
    }
}
