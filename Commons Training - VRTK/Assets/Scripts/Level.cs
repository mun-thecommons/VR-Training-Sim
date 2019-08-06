using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level : MonoBehaviour
{
    public GameObject player;
    private Vector3 startPosition;

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
        startPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        countDownText.text = "Go to CS Desk: " + timer;
        if(timer <= 0f && !MasterController.vestCollected)
        {
            countDownText.text = "Out of time";
            player.transform.position = startPosition;
            timer = resetTimer;
        }
    }
}
