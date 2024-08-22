using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public SceneTransition TheTransition;
    public AudioClip bgm;
    // Start is called before the first frame update
    void Start()
    {
        MyGameManager.Instance.PlayBGM(bgm);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
public void OnExploreClick()
    {
        TheTransition.GoToScene("spawn");
    }
public void OnExitClick()
    {
        Application.Quit();
    }

}
