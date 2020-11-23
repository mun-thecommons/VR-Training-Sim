using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ##Script Description
/// The script restricts the position of a Game Object in ranges of x and z, which keeping the y coordinate constant
/// </summary>
public class PositionLimits : MonoBehaviour
{
    private Quaternion startRotation;
    private Vector3 startPosition;
    private float startY;
    public float minX;
    public float minZ;
    private float maxX;
    private float maxZ;
    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.rotation;
        startPosition = transform.localPosition;

        startY = transform.localPosition.y;

        maxX = transform.localPosition.x;
        maxZ = transform.localPosition.z;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if      (transform.localPosition.z > 0.4f) { transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0.4f); }
        else if (transform.localPosition.z <  0)    { transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0); }

        if (transform.localPosition.x != startPosition.x || transform.localPosition.y != startPosition.y) { transform.localPosition = new Vector3(startPosition.x, startPosition.y, transform.localPosition.z); } // Only change x and y
        if (transform.rotation != startRotation) { transform.rotation = startRotation; }
        /*if(transform.localPosition.x <= minX)
        {
            transform.localPosition = new Vector3(minX, transform.localPosition.y, transform.localPosition.z);
        }
        if (transform.localPosition.z <= minZ)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, minZ);
        }


        if (transform.localPosition.x > maxX)
        {
            transform.localPosition = new Vector3(maxX, transform.localPosition.y, transform.localPosition.z);
        }
        if (transform.localPosition.z > maxZ)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, maxZ);
        }*/
    }
}
