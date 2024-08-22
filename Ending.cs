using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public AudioClip bgm;
    public SceneTransition TheTransition;

    // Start is called before the first frame update
    void Start()
    {
        MyGameManager.Instance.PlayBGM(bgm);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnBackToTitle()
    {
        TheTransition.GoToScene("Title"); 
    }
}
