﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorController : MonoBehaviour
{
    public Material screenMaterial;
    public bool errorState = false;

    private bool flashing = false;
    private Material brokenScreenMat;
    private Material brokenScreenRef;
    private Material[] materialsArray;
    private Color original;

    void Start()
    {        
        original = screenMaterial.color;
        brokenScreenMat = new Material(screenMaterial);
        brokenScreenMat.CopyPropertiesFromMaterial(screenMaterial);
        brokenScreenMat.color = Color.blue;       
    }

    private void Update()
    {
        if (errorState && !flashing)
        {
            materialsArray = new Material[GetComponent<Renderer>().materials.Length];
            materialsArray = GetComponent<Renderer>().materials;
            materialsArray[1] = brokenScreenMat;
            GetComponent<Renderer>().materials = materialsArray;
            brokenScreenRef = GetComponent<Renderer>().materials[1];
            StartCoroutine(FlashColour());
            flashing = true;
        }
    }

    IEnumerator FlashColour()
    {
        while (errorState)
        {
            brokenScreenRef.color = original;
            yield return new WaitForSeconds(0.5f);
            brokenScreenRef.color = Color.blue;
            yield return new WaitForSeconds(0.5f);
        }
        brokenScreenRef.color = original;
        flashing = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Hand"))
        {
            if (errorState)
            {
                errorState = false;
                MasterController.ScoreModify(0, 1, 0, true, false);
                if(Level.level == 3)
                {
                    Level.level3Monitor = true;
                }
            }
        }
    }
}
