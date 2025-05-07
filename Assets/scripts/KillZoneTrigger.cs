using UnityEngine;

public class KillZoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Destroy(other.gameObject);
        }
    }
}
