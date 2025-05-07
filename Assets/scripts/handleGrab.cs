using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class HandleGrab : XRGrabInteractable
{
    public Transform handler;
    private Rigidbody rb;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        EndGrab();
    }

    private void EndGrab()
    {
        // 清空自身刚体的速度
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // 同步位置、旋转并清空 handler 的刚体速度
        if (handler != null)
        {
            transform.position = handler.position;
            transform.rotation = handler.rotation;
            transform.localScale = Vector3.one;

            if (handler.TryGetComponent(out Rigidbody rbhandler))
            {
                rbhandler.linearVelocity = Vector3.zero;
                rbhandler.angularVelocity = Vector3.zero;
            }
        }
    }

    private void Update()
    {
        if (handler != null && isSelected && interactorsSelecting.Count > 0)
        {
            if (Vector3.Distance(handler.position, transform.position) > 0.4f)
            {
                var interactor = interactorsSelecting[0];

                if (interactor is IXRSelectInteractor selectInteractor && interactionManager != null)
                {
                    interactionManager.SelectExit(selectInteractor, this);
                }
            }
        }
    }
}
