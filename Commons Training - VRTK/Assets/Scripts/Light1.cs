using UnityEngine;

public class Light1 : MonoBehaviour {

    void Update()
    {
        if (TouchDetection.station1)
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        else
            gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
}
