using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ChangeBackground : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Material waves;
    [SerializeField] Material courtyard;
    [SerializeField] TMP_Text backText;
    bool toogle = false;
    void Start()
    {
        XRSimpleInteractable backChange = GetComponent<XRSimpleInteractable>();
        //backChange.selectEntered.AddListener(PrintMessage);
        backChange.selectEntered.AddListener((SelectEnterEventArgs arg) => {
            if (toogle == false)
            {
                toogle = true;
                RenderSettings.skybox = courtyard;
                backText.text = "return to the beach";
            }
            else
            {
                toogle = false;
                RenderSettings.skybox = waves;
                backText.text = "return to courtyard";
            }
        
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PrintMessage(SelectEnterEventArgs arg)
    {
        Debug.Log("Hello");
        RenderSettings.skybox = courtyard;
    }
}
