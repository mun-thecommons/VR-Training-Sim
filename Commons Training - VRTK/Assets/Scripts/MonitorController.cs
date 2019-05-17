using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorController : MonoBehaviour
{
    public Material screenMaterial;
    private Material brokenScreenMat;
    private Material brokenScreenRef;
    private Material[] materialsArray;

    Color original;
    // Start is called before the first frame update
    void Start()
    {
        original = screenMaterial.color;
        brokenScreenMat = new Material(screenMaterial);
        brokenScreenMat.CopyPropertiesFromMaterial(screenMaterial);
        brokenScreenMat.color = Color.blue;
        materialsArray = new Material[GetComponent<Renderer>().materials.Length];
        materialsArray = GetComponent<Renderer>().materials;
        Debug.Log(GetComponent<Renderer>().materials.Length);
        materialsArray[1] = brokenScreenMat;
        GetComponent<Renderer>().materials = materialsArray;
        brokenScreenRef = GetComponent<Renderer>().materials[1];
        StartCoroutine(FlashColour());
    }

    IEnumerator FlashColour()
    {
        while (true)
        {
            brokenScreenRef.color = original;
            yield return new WaitForSeconds(0.5f);
            brokenScreenRef.color = Color.blue;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
