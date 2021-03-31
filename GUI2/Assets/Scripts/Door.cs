using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject door;

    public void OpenDoor()
    {
        door.GetComponent<SpriteRenderer>().enabled = false;
        door.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void CloseDoor()
    {
        door.GetComponent<SpriteRenderer>().enabled = true;
        door.GetComponent<BoxCollider2D>().enabled = true;
    }

}
