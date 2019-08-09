using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SupervisorManager : MonoBehaviour
{
    public GameObject supervisorCanvas;

    // Start is called before the first frame update
    void Start()
    {
        supervisorCanvas = gameObject.transform.Find("SupervisorCanvas").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            supervisorCanvas.GetComponentInChildren<TextMeshProUGUI>().text = "Greetings, mortal";
            Debug.Log("sup greeted");
        }
    }
}
