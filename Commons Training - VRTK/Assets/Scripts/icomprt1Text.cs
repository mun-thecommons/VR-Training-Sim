using UnityEngine;
using TMPro;

public class icomprt1Text : MonoBehaviour
{

    private TextMeshProUGUI output;

    void Start()
    {
        output = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (CheckPrinters.icomprt1Full)
        {
            output.text = "Full";
        }
        else
            output.text = "Empty";
    }
}
