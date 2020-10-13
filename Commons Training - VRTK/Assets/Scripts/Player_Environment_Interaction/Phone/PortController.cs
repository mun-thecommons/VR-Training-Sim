using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortController : MonoBehaviour
{

    public PhoneController attachedPhone;
    public GameObject portConnector;

    // Start is called before the first frame update
    void Start()
    {
        portConnector.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EthernetConnector"))
        {
            attachedPhone.pluggedIn = true;
            portConnector.SetActive(true);
        }
    }
}
