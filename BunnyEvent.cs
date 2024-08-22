using System.Collections;
using System.Collections.Generic;
using MoreMountains.TopDownEngine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BunnyEvent : MonoBehaviour
{
    public GameObject Dialogue;
    private List<string> DialogueList = new List<string>()
    {
        "1Are you sure about this?",
        "0Of course I am.",
        "1Dude, I'm not too sure about this...",
        "0Even if you won't go, I'm still going to go.",
        "1Fine, fine.. ",
        "0Let's go, the FOREST awaits us.",
        "1Promise me this: we're not going to venture too far out.",
        "0...",
        "1Is that a yes?",
        "0Sure."
    };
    public Text LabelText;
    public int DialogueIndex;
    public Image Portrait;
    public List<Sprite> PortraitList;
    public SceneTransition TheTransition;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Dialogue.SetActive(true);
        ShowNextDiag();
        LevelManager.Instance.Players[0].GetComponent<CharacterMovement>().enabled = false;
    }
    public void ShowNextDiag() //shows next dialogue
    {
        string text = DialogueList[DialogueIndex];
        string indexText = text.Substring(0, 1); //identifies speaker
        int index = int.Parse(indexText);
        text = text.Substring(1, text.Length - 1); //separates text
        Portrait.sprite = PortraitList[index];
        LabelText.text = text;

    }
    public void OnDiagClick()
    {
        if (DialogueIndex == 9 && Dialogue.activeSelf) //when first convo is over
        {
            Dialogue.SetActive(false);
            TheTransition.GoToScene("Stage 0");

        }
        DialogueIndex += 1;
        ShowNextDiag();
    }
}
