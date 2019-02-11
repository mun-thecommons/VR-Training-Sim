using UnityEngine;
using TMPro;


public class PlayerUIScore : MonoBehaviour
{
    public TextMeshProUGUI playerUIscore;
    public Canvas mainCanvas;

    void Start()
    {
        //playerUIscore = gameObject.GetComponent<TextMeshProUGUI>();
        // TurnScoreOn();
        mainCanvas = gameObject.GetComponent<Canvas>();
        
    }

    void Update()
    {

       
        playerUIscore.SetText("Score:  "+ QuestionInput.totalScore.ToString());
        Debug.Log("being updated");
       // mainCanvas.transform.GetChild(0).gameObject.SetActive(false);



        //


    }
    public void TurnScoreOff()
    {
        //GameObject.Find("totalScore").SetActive(false);
        //gameObject.transform.FindChild("totalScore")().gameObject()
        //  GameObject.FindGameObjectWithTag("PlayerUIScore").SetActive(false);
        mainCanvas.transform.GetChild(0).gameObject.SetActive(false);

    }

    public void TurnScoreOn()
    {
        // GameObject.Find("totalScore").SetActive(true);
        // GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(false);
        // GameObject.FindGameObjectWithTag("PlayerUIScore").SetActive(true);
        Debug.Log("What up?");
        mainCanvas.transform.GetChild(0).gameObject.SetActive(true);
    }
}

/**
 * 
 *  IEnumerator HidePlayerScore()
    {
        private PlayerUIScore playerUIScore;
    playerUIScore = GameObject.Find("Canvas").GetComponent<PlayerUIScore>();
        yield return new WaitForSeconds(3f);
        playerUIScore.TurnScoreOff();
        _isScoreShowing = false;

    }
 * 
 * 
 * 
 * **/
