using UnityEngine;
using TMPro;


public class PlayerUIScore : MonoBehaviour
{
    public TextMeshProUGUI playerUIscore;
    public Canvas mainCanvas;

    void Start()
    {
        
        mainCanvas = gameObject.GetComponent<Canvas>();
        
    }

    void Update()
    {
    
        playerUIscore.SetText("Score:  "+ QuestionInput.totalScore.ToString());
       
    
    }
    public void TurnScoreOff()
    {
        mainCanvas.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void TurnScoreOn()
    {
        mainCanvas.transform.GetChild(0).gameObject.SetActive(true);
    }
}


