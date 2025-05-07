using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class WoodPlankSpawner : MonoBehaviour
{
    public GameObject woodPlankPrefab;
    public Transform spawnPoint;

    public void SpawnPlank(SelectEnterEventArgs args)
    {
        if (woodPlankPrefab == null || args == null) return;

        if (args.interactorObject is XRBaseInteractor interactor)
        {
            GameObject newPlank = Instantiate(woodPlankPrefab, spawnPoint.position, spawnPoint.rotation);

            if (newPlank.TryGetComponent(out IXRSelectInteractable interactable))
            {
                interactor.interactionManager.SelectEnter(
                    (IXRSelectInteractor)interactor,
                    interactable
                );
            }
        }
    }
}
