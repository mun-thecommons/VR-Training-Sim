using UnityEngine;
using TMPro;

public class icomprt2Text : MonoBehaviour
{

    private TextMeshProUGUI output;

    void Start()
    {
        output = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (CheckPrinters.icomprt2Full)
        {
            output.text = "Full";
        }
        else
            output.text = "Empty";
    }
}
