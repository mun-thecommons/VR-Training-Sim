using UnityEngine;

public class Light1 : MonoBehaviour {

    void Update()
    {
        if (RoundsCard.isRound1Done)
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        else
            gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
}
