using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextPagination : MonoBehaviour
{
    public TMP_Text storyText; // 绑定 UI Text 组件
    public Button nextButton; // 绑定 Next 按钮
    public TMP_Text buttonText; // 按钮上的文字
    public GameObject canvas; // 绑定整个 Canvas，在最后隐藏

    private int currentIndex = 0; // 当前文本索引
    private bool finalTextShown = false; // 是否已经显示最终文本

    // 你的文本内容
    private string[] storyLines =
    {
        "The storm wrecked your ship… You survived.",
        "This island feels… off.",
        "They say pirate treasure lies buried here.",
        "A giant mushroom? Is there a goblin inside?",
        "A tent, a skull-topped spear… Who was here before you?"
    };

    private string finalLine = "Let’s survive… or perish trying."; // 最终旁白

    void Start()
    {
        storyText.text = storyLines[currentIndex]; // 显示第一行文本
        nextButton.onClick.AddListener(ShowNextText); // 绑定按钮点击事件
    }

    void ShowNextText()
    {
        if (!finalTextShown) // 还未进入最终文本
        {
            if (currentIndex < storyLines.Length - 1)
            {
                currentIndex++;
                storyText.text = storyLines[currentIndex]; // 更新文本
            }
            else
            {
                // 显示最终文本
                storyText.text = finalLine;
                buttonText.text = "Oh no!"; // 改变按钮文本
                finalTextShown = true;
            }
        }
        else
        {
            // 隐藏 Canvas
            canvas.SetActive(false);
        }
    }
}
