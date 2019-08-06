using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level : MonoBehaviour
{
    private float timer = 15f;
    public GameObject countDown;
    private TextMeshProUGUI countDownText;

    // Start is called before the first frame update
    void Start()
    {
        countDownText = countDown.GetComponent<TextMeshProUGUI>();
        countDownText.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        countDownText.text = "Go To Desk: " + timer;
        if(timer <= 0f)
        {
            countDownText.text = "Out of time";
            if(timer <= 3f)
            {
                countDownText.enabled = false;
            }
        }
    }
}
