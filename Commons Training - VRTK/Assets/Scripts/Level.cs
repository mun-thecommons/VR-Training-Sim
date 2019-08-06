using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level : MonoBehaviour
{
    public GameObject player;
    public Transform startPosition;

    //Level Completion Booleans
    private bool level1Completed = false;
    private bool level2Completed = false;
    private bool level3Completed = false;
    private bool level4Completed = false;
    private bool level5Completed = false;

    //Timer 
    private float timer = 15f;
    private float resetTimer = 15f;

    //Count Down Timer Canvas
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
        countDownText.text = "Go to CS Desk: " + Mathf.Round(timer*100)/100.0 ;
        if(timer <= 0f && !MasterController.vestCollected)
        {
            countDownText.text = "Out of time";
            if(timer <= -3f)
            {
                player.transform.position = startPosition.transform.position;
                timer = resetTimer;
            }
            
        }
       
    }
}
