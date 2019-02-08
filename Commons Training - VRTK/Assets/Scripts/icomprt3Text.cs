using UnityEngine;
using TMPro;

public class icomprt3Text : MonoBehaviour
{

    private TextMeshProUGUI output;

    void Start()
    {
        output = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (CheckPrinters.icomprt3Full)
        {
            output.text = "Full";
        }
        else
            output.text = "Empty";
    }
}
