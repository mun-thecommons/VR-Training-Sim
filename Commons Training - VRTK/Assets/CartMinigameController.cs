using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartMinigameController : MonoBehaviour
{

    public GameObject reamHolder;
    public GameObject paperPrefab;
    public GameObject paperBoxes;

    public AudioClip successClip;
    private AudioSource cartAudio;

    // Where to start placing the paper reams
    private Vector3 offset;

    void Start()
    {
        cartAudio = GetComponent<AudioSource>();
        offset = new Vector3(-14.7f, 0.03f, -8.0f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cart") && paperBoxes.activeSelf)
        {

            paperBoxes.SetActive(false);

            GameObject paper = Instantiate(paperPrefab, offset, paperPrefab.transform.rotation) as GameObject;

            // put the paper reams in the reamholder game object to keep the hierarchy clean
            paper.transform.parent = reamHolder.transform;

            cartAudio.PlayOneShot(successClip);

        }
    }
}
