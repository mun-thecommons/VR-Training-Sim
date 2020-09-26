using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Control a navigation target that shows the player where to go
/// 
/// ##Script Description
/// When a Navigation Target is instantiated at a position, a line is drawn from it to the 
/// player so that they can see where they need to go. When the player reaches the target,
/// it is destroyed
/// </summary>
public class NavigatonTarget : MonoBehaviour
{
    public GameObject lineObject; /*!< @brief The line that is drawn from the player to the navigation target  */
    private GameObject player;

    private LineRenderer navLine;

    private Vector3[] positions = new Vector3[2]; /*!< @brief array of length 2 to hold the start and end positions of the navigation line  */

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navLine = lineObject.GetComponent<LineRenderer>();
        positions[0] = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        positions[0] -= transform.position;
        positions[1] = Vector3.zero;
        navLine.SetPositions(positions);
        navLine.startWidth = 0.5f;
        navLine.endWidth = 0.5f;
    }

    /**********************************************************
     * Continuously update the position of the start of the line to the player's
     * current X and Z positions. The Y position should always be 0 so that the
     * line stays on the floor
     * 
    ***********************************************************/
    private void Update()
    {
        positions[0] = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        positions[0] -= transform.position;
        positions[1] = Vector3.zero;
        navLine.SetPositions(positions);
    }

    /**********************************************************
     * Detect when the player enters the target and then destroy it
     * 
    ***********************************************************/
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
