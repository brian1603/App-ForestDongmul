using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoToScene(string sceneName)
    {
        GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().Play("Scene Transition");
        StartCoroutine(GoToSceneLater(sceneName));
    }
    IEnumerator GoToSceneLater(string sceneName)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }
}
