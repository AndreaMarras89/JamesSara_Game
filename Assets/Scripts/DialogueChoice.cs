using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueChoice
{
    [SerializeField]
    string text;
    [SerializeField]
    int newCursorPositionInDialogue;
    [SerializeField]
    Object choiceMetadata;

    public DialogueChoice(string text, int newCursorPositionInDialogue, Object choiceMetadata)
    {
        this.text = text;
        this.newCursorPositionInDialogue = newCursorPositionInDialogue;
        this.choiceMetadata = choiceMetadata;
    }

    public string GetText()
    {
        return this.text;
    }

    public int GetNewCursorPosition()
    {
        return this.newCursorPositionInDialogue;
    }

    public Object GetChoiceMetadata()
    {
        return this.choiceMetadata;
    }
}
