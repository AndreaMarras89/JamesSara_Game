using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueMultipleChoice
{
    [SerializeField]
    DialogueChoice[] choiceList;

    public DialogueMultipleChoice(DialogueChoice[] choiceList)
    {
        this.choiceList = choiceList;
    }

    public DialogueChoice[] GetChoiceList()
    {
        return this.choiceList;
    }
}
