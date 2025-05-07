using UnityEngine;

public class SkullPickup : MonoBehaviour
{
    public SkullCollection skullCollection; // ** ÷∂Ø∏≥÷µ**

    public void CollectSkull()
    {
        if (skullCollection != null)
        {
            Debug.Log("pick");
            skullCollection.CollectSkull(gameObject);
        }
        else
        {
            Debug.LogError("SkullCollection not assigned! Please set it in the Inspector.");
        }
    }
}
