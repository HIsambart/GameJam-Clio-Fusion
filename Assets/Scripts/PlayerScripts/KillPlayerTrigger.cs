using UnityEngine;

namespace PlayerScripts
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
