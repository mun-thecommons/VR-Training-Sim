using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static TextMeshProUGUI staplerCount;
    public static TextMeshProUGUI coinCount;
    private bool inInventory = false;

    // Start is called before the first frame update
    private void Start()
    {
        gameObject.GetComponent<Canvas>().enabled = false;
        staplerCount = GameObject.FindGameObjectWithTag("StaplerCount").GetComponentInChildren<TextMeshProUGUI>();
        coinCount = GameObject.FindGameObjectWithTag("CoinCount").GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.RThumbstick))
        {
            if (!inInventory && !MasterController.inMenu)
            {
                gameObject.GetComponent<Canvas>().enabled = true;
                staplerCount.SetText("Number of Staplers: " +MasterController.staplers);
                coinCount.SetText("Number of Coins: " + MasterController.coins);
            }
            else if(gameObject.GetComponent<Canvas>().enabled)
            {
                gameObject.GetComponent<Canvas>().enabled = false;
            }
            inInventory = !inInventory;
        }
    }
}
