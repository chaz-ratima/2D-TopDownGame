using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DialogueManager : MonoBehaviour
{
    PlayerInput playerInput;
    [SerializeField] GameObject playerInputMovement;
    public TextMeshProUGUI textDisplay;
    public GameObject canvas;

    private bool interact;
    private bool nearNPC;
    private bool isTalking;
    private bool isAbleToInteract;

    public string[] lines;
    private int index;
    public float typingSpeed;

    private void Start()
    {
        playerInput = playerInputMovement.GetComponent<PlayerInput>();
    }

    void Update()
    {
        interact = Input.GetKeyDown(KeyCode.Space);

        // Intial interaction to start dialogue, bring up canvas and deny movement
        if (!isTalking && interact && nearNPC)
        {
            canvas.SetActive(true);
            StartDialogue();
            playerInput.NoMoving();
            isTalking = true;
        }

        // if text is finished displaying, interaction key is available
        if (textDisplay.text == lines[index])
        {
            isAbleToInteract = true;
        }

        // beings next sentence if able to
        if (isTalking == true && isAbleToInteract == true && interact)
        {
            isAbleToInteract = false;
            NextSentence();
        }
    }

    public void StartDialogue()
    {
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        // foreach loop that displays the next letter in index until the word is displayed for that index
        // types in typingSpeed
        foreach (char letter in lines[index].ToCharArray()) {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            nearNPC = true;
        } else nearNPC = false;
    }

    public void DialogueEnd()
    {
        // brings up canvas, allows movement
        playerInput.YesMoving();
        canvas.SetActive(false);
        isTalking = false;
    }
}
