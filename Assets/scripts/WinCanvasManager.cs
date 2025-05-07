using UnityEngine;
using TMPro;
using System.Collections;

public class WinCanvasManager : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    public FinalTreasureChest finalChest;

    void Start()
    {
        StartCoroutine(DelayedShowResult());
    }

    IEnumerator DelayedShowResult()
    {
        yield return null; // 等1帧，保证FinalTreasureChest更新完

        if (resultText == null || finalChest == null)
        {
            Debug.LogError("Result Text or FinalChest not assigned!");
            yield break;
        }

        int value = finalChest.treasureChest.currentValue; // 这时候才是真正的数值！

        string title = "";

        if (value >= 90)
            title = "Legendary Pirate";
        else if (value >= 70)
            title = "Master Buccaneer";
        else if (value >= 50)
            title = "Seasoned Sailor";
        else if (value >= 30)
            title = "Survivor at Sea";
        else
            title = "Lost Soul";

        resultText.text = $"<size=120%><b>Congratulations, Captain!</b></size>\n" +
                          $"Title: <b><color=#00BFFF>{title}</color></b></size>\n\n" +
                          $"You have survived the storm and protected your loot.\n" +
                          $"Go take a look at your final treasure!";
    }
}
