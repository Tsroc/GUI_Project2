using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Door Trigger"))
        {
            collision.gameObject.GetComponent<Door>().OpenDoor();
        }
        else if (collision.tag == "Instruction")
        {
            collision.gameObject.GetComponent<Instruction>().Activate();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.tag == "Door Trigger"))
        {
            collision.gameObject.GetComponent<Door>().CloseDoor();
        }
    }
}
