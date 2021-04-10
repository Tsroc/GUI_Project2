using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject levelEndNotification;

    public void ActivatePortal()
    {
        // Display UI
        levelEndNotification.gameObject.SetActive(true);
    }
}
