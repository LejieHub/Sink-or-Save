using TMPro;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    public int currentValue = 0;
    public int maxValue = 100;

    [Header("Treasure Display Prefabs")]
    public GameObject stage20;
    public GameObject stage40;
    public GameObject stage60;
    public GameObject stage80;
    public GameObject stage100;

    [Header("UI Reference")]
    public TextMeshProUGUI valueText;

    private void Start()
    {
        UpdateDisplay();
    }

    public void AddTreasure(int amount)
    {
        currentValue = Mathf.Clamp(currentValue + amount, 0, maxValue);
        UpdateDisplay();
    }

    public void RemoveTreasure(int amount)
    {
        currentValue = Mathf.Clamp(currentValue - amount, 0, maxValue);
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        // 关闭所有显示
        stage20.SetActive(false);
        stage40.SetActive(false);
        stage60.SetActive(false);
        stage80.SetActive(false);
        stage100.SetActive(false);

        if (currentValue >= 90)
        {
            stage100.SetActive(true);
            stage80.SetActive(true);
        }
        else if (currentValue >= 70)
            stage80.SetActive(true);
        else if (currentValue >= 50)
            stage60.SetActive(true);
        else if (currentValue >= 30)
            stage40.SetActive(true);
        else if (currentValue >= 1)
            stage20.SetActive(true);

        if (valueText != null)
        {
            valueText.text = currentValue + " / " + maxValue;
        }
    }

    

}
