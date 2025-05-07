using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class HoverScaleEffect : MonoBehaviour
{
    private Vector3 originalScale;
    public float scaleFactor = 1.2f;
    private bool isHovered = false;

    private void Awake()
    {
        originalScale = transform.localScale;
        var interactable = GetComponent<XRBaseInteractable>();

        interactable.hoverEntered.AddListener(_ =>
        {
            isHovered = true;
            transform.localScale = originalScale * scaleFactor;
        });

        interactable.hoverExited.AddListener(_ =>
        {
            if (!interactable.isSelected) 
            {
                isHovered = false;
                transform.localScale = originalScale;
            }
        });
    }
}
