using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [System.Serializable]
    public class DialogueNode
    {
        public string dialogueText;
        public string choice1Text;
        public string choice2Text;
        public int nextNode1;
        public int nextNode2;
    }

    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI choice1Text;
    public TextMeshProUGUI choice2Text;
    public Button choiceButton1;
    public Button choiceButton2;

    public DialogueNode[] dialogues;
    private int currentNodeIndex = 0;

    void Start()
    {
        UpdateDialogue();
        choiceButton1.onClick.AddListener(() => ChooseOption(1));
        choiceButton2.onClick.AddListener(() => ChooseOption(2));
    }

    public void ResetDialogue()
    {
        currentNodeIndex = 0; // **reset**
        UpdateDialogue(); // **update UI **
    }

    void UpdateDialogue()
    {
        if (currentNodeIndex < 0 || currentNodeIndex >= dialogues.Length)
        {
            ResetDialogue(); // **reset**
            FindObjectOfType<CanvasToggle>().OpenClose(); // close UI
            return;
        }

        DialogueNode currentNode = dialogues[currentNodeIndex];

        dialogueText.text = string.IsNullOrEmpty(currentNode.dialogueText) ? "............................." : currentNode.dialogueText;
        choice1Text.text = currentNode.choice1Text;
        choice2Text.text = currentNode.choice2Text;

        choiceButton1.gameObject.SetActive(!string.IsNullOrEmpty(currentNode.choice1Text));
        choiceButton2.gameObject.SetActive(!string.IsNullOrEmpty(currentNode.choice2Text));
    }

    void ChooseOption(int choice)
    {
        DialogueNode currentNode = dialogues[currentNodeIndex];

        int nextNode = (choice == 1) ? currentNode.nextNode1 : currentNode.nextNode2;

        if (nextNode < 0 || nextNode >= dialogues.Length)
        {
            ResetDialogue(); // **reset**
            FindObjectOfType<CanvasToggle>().OpenClose(); // close UI
            return;
        }

        currentNodeIndex = nextNode;
        UpdateDialogue();
    }
}
