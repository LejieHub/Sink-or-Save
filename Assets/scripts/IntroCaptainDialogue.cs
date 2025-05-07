using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroCaptainDialogue : MonoBehaviour
{
    public TMP_Text dialogueText;       // 绑定 UI 上的文字文本
    public Button nextButton;           // 下一页按钮
    public TMP_Text buttonLabel;        // 按钮上的文字
    public GameObject canvasGroup;      // 整个对话 UI Canvas

    private int currentLineIndex = 0;
    private bool finished = false;

    // 教学文案，老船长口吻
    private string[] introLines =
    {
        "Ahoy, Welcome aboard, sailor! This is Sink or Save!",
        "This is a short journey. Five minutes, maybe less.",
        "Your goal is simple. Keep the treasure chest safe and stay alive.",
        "Now you have two choices.",
        "Turn right to board the ship and jump straight into chaos...",
        "Or go forward for a quick training on how to survive what lies ahead."
    };

    private string finalLine = "Ready your wits, pirate. Click again to begin your tale.";

    void Start()
    {
        // 显示第一行对话
        dialogueText.text = introLines[currentLineIndex];
        nextButton.onClick.AddListener(ShowNextLine);
    }

    void ShowNextLine()
    {
        if (!finished)
        {
            if (currentLineIndex < introLines.Length - 1)
            {
                currentLineIndex++;
                dialogueText.text = introLines[currentLineIndex];
            }
            else
            {
                dialogueText.text = finalLine;
                buttonLabel.text = "Aye!";
                finished = true;
            }
        }
        else
        {
            // 结束对话，关闭 Canvas
            canvasGroup.SetActive(false);
        }
    }
}
