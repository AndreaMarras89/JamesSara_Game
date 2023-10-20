using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    public static ConversationManager instance;
    public DialogueLine[] currentDialogue = null;
    public DialogueMultipleChoice[] currentChoices = null;
    public int dialogueCursor = 0;
    public bool dialogueIndexIsANumber = false;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        currentDialogue = null;
        currentChoices = null;
        dialogueCursor = 0;

    }

    
    void Update()
    {
        if(currentDialogue != null && currentDialogue.Length > 0 && Input.GetMouseButtonDown(0))
        {
            if (dialogueCursor >= currentDialogue.Length && UiFade.instance.dialogBoxOpen)
            {
                // dialogo finito, chiudo tutto e tanti saluti al cazzo
                currentDialogue = null;
                currentChoices = null;
                dialogueCursor = 0;
                UiFade.instance.CloseDialogBox();
                //Debug.Log("Ho chiuso la fottuta dialogue box");
            }
            else if (!UiFade.instance.dialogBoxOpen)
            {
                // se il dialogo è appena iniziato, apro pure la dialogue box
                UiFade.instance.OpenDialogBox();
            }else if(UiFade.instance.dialogBoxOpen && int.TryParse(currentDialogue[dialogueCursor].GetCharacter(), out var index))
            {
                Debug.Log(index);
                if(index > currentDialogue.Length)
                {
                    Debug.Log("Sto qua dentro");
                    currentDialogue = null;
                    currentChoices = null;
                    dialogueCursor = 0;
                    UiFade.instance.CloseDialogBox();
                }
                
            }
            
            // GESTIONE DEL DIALOGO
            // Se il campo character è un numero intero, allora è una linea "di skip": serve per skippare all'indice indicato nel campo character
            // Se il campo character inizia per # ed è seguito da un numero intero, allora è una linea di scelta multipla: rimanda alla lista delle scelte multiple all'indice indicato dopo il #
            // Altrimenti è una semplice linea di dialogo che viene mostrata nella dialogue box
            if(currentDialogue != null)
            {
                if (int.TryParse(currentDialogue[dialogueCursor].GetCharacter(), out var skipIndex))
                {
                    // skip to skipIndex in current dialogue
                    dialogueCursor = skipIndex;
                    dialogueIndexIsANumber = true;
                }
                if (dialogueCursor < currentDialogue.Length && currentDialogue[dialogueCursor].GetCharacter()[0] == '#' && int.TryParse(currentDialogue[dialogueCursor].GetCharacter().Substring(1), out var multipleChoiceIndex))
                {
                    // go to multipleChoiceIndex in multiple choices array
                    DialogueMultipleChoice mutipleChoiceToShow = currentChoices[multipleChoiceIndex];
                    // --- succedono cose ---
                    // alla fine scelgo, qui ho messo un hardcoding provvisorio
                    DialogueChoice chose = mutipleChoiceToShow.GetChoiceList()[0];
                    UiFade.instance.corpo.text = chose.GetText();
                    // -----
                    dialogueCursor = chose.GetNewCursorPosition();
                }
                else
                {
                    if (dialogueCursor < currentDialogue.Length)
                    {
                        UiFade.instance.corpo.text = currentDialogue[dialogueCursor].GetCharacter() + ": " + currentDialogue[dialogueCursor].GetDialogueLine();
                        dialogueCursor++;
                    }
                    
                }
            }
            
        }
    }
}
