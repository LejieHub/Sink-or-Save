using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class AutoReleaseWhenTooFar : MonoBehaviour
{
    private XRGrabInteractable grab;
    private IXRSelectInteractor selectingInteractor;

    [SerializeField]
    private float maxGrabDistance = 0.5f;

    private void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();

        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        selectingInteractor = args.interactorObject;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        selectingInteractor = null;
    }

    private void Update()
    {
        if (selectingInteractor != null && grab.isSelected)
        {
            float distance = Vector3.Distance(selectingInteractor.transform.position, transform.position);

            if (distance > maxGrabDistance && grab.interactionManager != null)
            {
                grab.interactionManager.SelectExit(selectingInteractor, grab);
            }
        }
    }

}
