using UnityEngine;

public class CrabActivator : MonoBehaviour
{
    public CrabAI crabAI;

    public void ActivateCrab()
    {
        if (crabAI != null)
        {
            crabAI.enabled = true;
            Debug.Log("CrabAI component enabled!");
        }
    }
}
