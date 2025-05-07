using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    public GameObject targetObject;

    public void ActivateObject()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(true);
            Debug.Log("Object activated: " + targetObject.name);
        }
    }
}
