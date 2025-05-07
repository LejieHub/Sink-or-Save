using UnityEngine;
using TMPro;

public class FinalTreasureChest : MonoBehaviour
{
    public TreasureChest treasureChest;

    void Start()
    {
        int chest1Value = PlayerPrefs.GetInt("Chest1Value", 0);
        int chest2Value = PlayerPrefs.GetInt("Chest2Value", 0);

        int totalOriginalValue = chest1Value + chest2Value; // 总值，比如 70+50=120
        float percentage = (totalOriginalValue / 200f) * 100f; // 平均百分比，比如 60%
        int finalDisplayValue = Mathf.Clamp(Mathf.RoundToInt(percentage), 0, 100);

        treasureChest.currentValue = finalDisplayValue;

        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        // 关闭所有显示
        treasureChest.stage20.SetActive(false);
        treasureChest.stage40.SetActive(false);
        treasureChest.stage60.SetActive(false);
        treasureChest.stage80.SetActive(false);
        treasureChest.stage100.SetActive(false);

        if (treasureChest.currentValue >= 90)
        {
            treasureChest.stage100.SetActive(true);
            treasureChest.stage80.SetActive(true);
        }
        else if (treasureChest.currentValue >= 70)
            treasureChest.stage80.SetActive(true);
        else if (treasureChest.currentValue >= 50)
            treasureChest.stage60.SetActive(true);
        else if (treasureChest.currentValue >= 30)
            treasureChest.stage40.SetActive(true);
        else if (treasureChest.currentValue >= 1)
            treasureChest.stage20.SetActive(true);

        if (treasureChest.valueText != null)
        {
            treasureChest.valueText.text = treasureChest.currentValue + " / " + treasureChest.maxValue;
        }
    }
}
