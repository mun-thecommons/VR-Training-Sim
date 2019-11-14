using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TagSort : MonoBehaviour
{

    private void Start()
    {
        foreach (string tag in UnityEditorInternal.InternalEditorUtility.tags)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
            if (objects.Length == 0)
            {
                Debug.Log("No objects with tag " + tag);
            }
        }
    }

    
}
