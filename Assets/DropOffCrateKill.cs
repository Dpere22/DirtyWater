using System;
using UnityEngine;

public class DropOffCrateKill : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("PlayerDropOffCrate")) return;
        Destroy(other.gameObject);
    }
}
