using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
    public GameObject CombatPopUp;
    private List<string> DialogueList = new List<string>()
    {
        "1Stop!",
        "0What?",
        "1We shouldn't venture too far into the forest..",
        "0why not?",
        "1My mom told me that those who came here never came back.",
        "0...",
        "1Hey, wait up! Don't leave me here alone!",
        "1Wait... That's a-",
        "0human.",
        "1Oh no.",
        "0nah cuh we win these"
    };
    public GameObject Dialogue;
    public Text LabelText;
    public int DialogueIndex;
    public Image Portrait;
    public List<Sprite> PortraitList;
    public Transform Sheep;
    public Transform BattlePostion;
    public Transform MainCamera;
    public Animator BunnyAnimator;
    public Animator SheepAnimator;
    public GameObject BattleTransition;
    public AudioClip BattleBGM;

    // Start is called before the first frame update
    void Start()
    { 
        ShowNextDiag();
        StartCoroutine(ShowDialogue());
    }
    public void BunnyWalk()
    {
        BunnyAnimator.Play("1_Run");
    }
    public void BunnyIdle()
    {
        BunnyAnimator.Play("0_idle");
    }
    public void SheepWalk()
    {
        SheepAnimator.Play("1_Run");
    }
    public void SheepIdle()
    {
        SheepAnimator.Play("0_idle");
    }

    IEnumerator ShowDialogue()
    {
        yield return new WaitForSeconds(6.2f);
        Dialogue.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Vector3 scale = Sheep.localScale;
        scale.x *= -1;
        Sheep.localScale = scale;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnDiagClick()
    {
        if (DialogueIndex == 5 && Dialogue.activeSelf) //when first convo is over
        {
            Dialogue.SetActive(false);
            GetComponent<Animator>().Play("Cutscene 1-2"); //plays next cutscene
            Vector3 scale = Sheep.localScale;
            scale.x *= -1;
            Sheep.localScale = scale;

        }
        else if (DialogueIndex == 6)
        {
            Dialogue.SetActive(false);
            GetComponent<Animator>().Play("Cutscene 1-3"); //plays next cutscene
        }
        else if (DialogueIndex == 10)
        {
            Dialogue.SetActive(false);
            GoToBattle();
            return;
        }
        DialogueIndex += 1;
        ShowNextDiag();
    }
    public void GoToBattle()
    {

        BattleTransition.GetComponent<Animator>().Play("OpenAndClose");
        StartCoroutine(GoToBattleLater());
    }

    public void HeyWaitUp()
    {
        Dialogue.SetActive(true);
    }

    public void ShowNextDiag() //shows next dialogue
    {
        string text = DialogueList[DialogueIndex];
        string indexText = text.Substring(0, 1); //identifies speaker
        int index = int.Parse(indexText);
        text = text.Substring(1, text.Length-1); //separates text
        Portrait.sprite = PortraitList[index];
        LabelText.text = text;

    }
    IEnumerator GoToBattleLater()
    {
        yield return new WaitForSeconds(1);
        GetComponent<Animator>().Play("Cutscene 1-4"); //plays next cutscene
        MainCamera.position = BattlePostion.position;
        CombatPopUp.SetActive(true);
        CombatPopUp.GetComponent<Combat>().SetEnemy(0);
        MyGameManager.Instance.PlayBGM(BattleBGM);
    }
}
