using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{

    public enum CollectibleType
    {
        TrashBase,
        TrashMetal,
        TrashPlastic,
        USBStick,
        ScrapPaper,
        Coin,
        Stapler
    }

    public CollectibleType collectibleType;
    public AudioClip collectAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.hasVest && other.CompareTag("Hand"))
        {
            switch (collectibleType)
            {
                case CollectibleType.TrashBase:
                case CollectibleType.TrashMetal:
                case CollectibleType.TrashPlastic:
                    GameManager.trashCollected++;
                    break;

                case CollectibleType.ScrapPaper:
                    GameManager.scrapPaperCollected++;
                    break;

                case CollectibleType.USBStick:
                    //TODO
                    break;

                case CollectibleType.Coin:
                    GameManager.coinsCollected++;
                    break;

                case CollectibleType.Stapler:
                    // TODO
                    break;
                default:
                    //TODO
                    break;

            }

            if (collectibleType != CollectibleType.USBStick) // The USB stick needs to be brought to the desk, dont destroy it
            {
                GameManager.playerAudioSource.PlayOneShot(collectAudio);
                CollectibleManager.spawnPositions.Add(transform.position); // Add the collectible's position back as a valid spawn location
                Destroy(gameObject);
            }

            
        }
    }
}
