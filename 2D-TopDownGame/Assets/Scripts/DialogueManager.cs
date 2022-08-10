using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DialogueManager : MonoBehaviour
{
    OldPlayerInput playerInput;
    [SerializeField] GameObject playerInputMovement;
    public TextMeshProUGUI textDisplay;
    public GameObject canvas;

    private bool interact;
    private bool interact2;
    private bool nearNPC;
    private bool isTalking;
    private bool isAbleToInteract;
    private bool skip;
    private bool canSkip;
    public string[] lines;
    private int index;
    public float typingSpeed;

    private void Start()
    {
        playerInput = playerInputMovement.GetComponent<OldPlayerInput>();
    }

    void Update()
    {
        interact = Input.GetKeyDown(KeyCode.Space);
        interact2 = Input.GetKeyDown(KeyCode.B);

        InitialInteraction();
        ContinueKey();
        AvaliableSentence();
        TextSkip();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            nearNPC = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            nearNPC = false;
        }
    }

    public void StartDialogue()
    {
        StartCoroutine(TypeLine());
    }

    public void SkipDialogue()
    {
        StartCoroutine(SkipLine());
    }

    IEnumerator TypeLine()
    {
        // foreach loop that displays the next letter in index until the word is displayed for that index
        // types in typingSpeed
        foreach (char letter in lines[index].ToCharArray()){
            textDisplay.text += letter;
            if (!skip)
                yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator SkipLine()
    {
        yield return new WaitForSeconds(1);
        canSkip = true;
    }

    public void NextSentence()
    {
        // checks where in index and if it can either go to next index or end
        if(index < lines.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartDialogue();
        } else
        {
            textDisplay.text = "";
            DialogueEnd();
        }
    }

    public void DialogueEnd()
    {
        // brings up canvas, allows movement
        playerInput.YesMoving();
        canvas.SetActive(false);
        isTalking = false;
        canSkip = false;
        skip = false;
    }

    #region DialogueInteractionFunctions
    public void InitialInteraction()
    {
        if (!isTalking)
        {
            // Intial interaction to start dialogue, bring up canvas and deny movement
            if (nearNPC && interact)
            {
                canvas.SetActive(true);
                StartDialogue();
                playerInput.NoMoving();
                isTalking = true;
                canSkip = true;
                skip = false;
            }
        }
    }

    public void ContinueKey()
    {
        if (isTalking){
            // if text is finished displaying, interaction key is available
            if (textDisplay.text == lines[index]){
                isAbleToInteract = true;
                skip = false;
                canSkip=false;
            }
        }
    }

    public void AvaliableSentence(){
        if (isTalking){
            // brings next sentence if able to
            if (interact && isAbleToInteract){
                isAbleToInteract = false;
                NextSentence();
                SkipDialogue();
            }
        }
    }

    public void TextSkip()
    {
        if (isTalking){
            if(canSkip && interact) {
                // Fills text in before loop is finished
                skip = true;
            }
        }

    }
    #endregion
}
