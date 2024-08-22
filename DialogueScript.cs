using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    public Text LabelText;
    public int DialogueIndex;
    public Image Portrait;
    public List<Sprite> PortraitList;
    public List<string> TextList;
    public UnityAction TheCallBack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowDialogue(List<string> textList , UnityAction callback)
    {
        gameObject.SetActive(true);
        TextList = textList;
        TheCallBack = callback;
        ShowNextDiag();
    }
    public void ShowNextDiag() //shows next dialogue
    {
        string text = TextList[DialogueIndex];
        string indexText = text.Substring(0, 1); //identifies speaker
        int index = int.Parse(indexText);
        text = text.Substring(1, text.Length - 1); //separates text
        Portrait.sprite = PortraitList[index];
        LabelText.text = text;

    }
    public void OnDiagClick() //dialogue
    {

        DialogueIndex += 1;
        if (DialogueIndex >= TextList.Count)
        {
            TheCallBack.Invoke();
            return;
        }
        ShowNextDiag();

    }
}
