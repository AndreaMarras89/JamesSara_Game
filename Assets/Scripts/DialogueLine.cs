using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    [SerializeField]
    string character;
    [SerializeField]
    string line;

    public DialogueLine(string ch, string li)
    {
        this.character = ch;
        this.line = li;
    }

    public string GetDialogueLine()
    {
        return this.line;
    }

    public string GetCharacter()
    {
        return this.character;
    }
}
