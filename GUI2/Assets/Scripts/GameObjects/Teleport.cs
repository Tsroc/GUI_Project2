using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] Transform spawnPos;

    public Vector3 GetTeleportPos()
    {
        return spawnPos.position;
    }
 }
