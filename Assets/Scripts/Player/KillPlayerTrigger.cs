using System;
using UnityEngine;

namespace Player
{
    public class KillPlayerTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<Player>().Respawn();
                Debug.Log("Respawned player");
            }
        }
    }
}
