using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroCaptainDialogue : MonoBehaviour
{
    public TMP_Text dialogueText;       // �� UI �ϵ������ı�
    public Button nextButton;           // ��һҳ��ť
    public TMP_Text buttonLabel;        // ��ť�ϵ�����
    public GameObject canvasGroup;      // �����Ի� UI Canvas

    private int currentLineIndex = 0;
    private bool finished = false;

    // ��ѧ�İ����ϴ�������
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
        // ��ʾ��һ�жԻ�
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
            // �����Ի����ر� Canvas
            canvasGroup.SetActive(false);
        }
    }
}
