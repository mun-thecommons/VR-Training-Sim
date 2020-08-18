using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///#Header 1
///*Brief* **Summary**
///This is a class
/// </summary>
public class Documentation_Trial : MonoBehaviour
{
    void Start()
    {
        
    }

  
    void Update()
    {

    }
    /************************************************
    *#Header 1
    * *This* **is** a function
     @param a
     @note b
     @warning c
     ***********************************************/
    public int func()
    {
        int a = 1;
        int b = 2;
        int c = a + b;
        return c;
    }
}
