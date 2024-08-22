using System.Collections;
using System.Collections.Generic;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject CombatCamera;
    public GameObject CombatPopUp;
    public int StageIndex;
    public int CurrentEnemyIndex;
    public List<GameObject> EnemyList;
    public float HeroScale = 2;
    public GameObject DeerCage;
    public GameObject Deer;
    public AudioClip bgm;
    public List<string> DeerEscapeDiag = new List<string>()
    {
        "2...",
        "1We would appreciate it if you expressed your gratitude. We nearly died trying to save you!",
        "2(Mutters in a low tone)",
        "0?",
        "1?",
        "2Us deers... We express our gratitude with actions, not words.",
        "0Are you skilled at combat?",
        "2I am nearly unsurmountable.",
        "1Doesn't seem like it by the looks of your cage...",
        "0We are making our way into the unexplored depths of the FOREST. If you really want to express your thanks...",
        "2I swear on the name of my ancestors, I will assist you to the very end.",
        "0[DEER HAS JOINED YOUR TEAM!]"
    };
    public enum AnimationState
    {
        idle,
        run
    }
    public AnimationState TheState = AnimationState.idle;
    public DialogueScript TheDialogue;
    public Animator BattleTransition;
    public AudioClip EnemyEncounterSound;
    public SceneTransition TheTransition;
    public AudioClip BattleBGM;

    // Start is called before the first frame update
    void Start()
    {
        MyGameManager.Instance.StageNumber = StageIndex;
        if (StageIndex > 0)
        {
            StartCoroutine(ScaleUpHero());
        }
        MyGameManager.Instance.PlayBGM(bgm);
        TheTransition.GetComponent<Animator>().Play("OpenTransition");
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            if (TheState == AnimationState.idle)
            {
                LevelManager.Instance.Players[0].transform.Find("KoalaModel").Find("model").Find("UnitRoot").GetComponent<Animator>().Play("1_Run");
                TheState = AnimationState.run;
            }
        }
        else if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            if (TheState == AnimationState.run)
            {
                LevelManager.Instance.Players[0].transform.Find("KoalaModel").Find("model").Find("UnitRoot").GetComponent<Animator>().Play("0_idle");
                TheState = AnimationState.idle;
            }
        }
    }
    public void StartBattle(int enemyIndex)
    {
        BattleTransition.Play("OpenAndClose");
        StartCoroutine(StartBattleLater(enemyIndex));
    }
    IEnumerator StartBattleLater(int enemyIndex)
    {
        MyGameManager.Instance.PlaySound(EnemyEncounterSound);

        yield return new WaitForSeconds(1);
        CurrentEnemyIndex = enemyIndex;
        MainCamera.SetActive(false);
        CombatCamera.SetActive(true);
        CombatCamera.GetComponent<AudioListener>().enabled = true;
        CombatPopUp.SetActive(true);
        Combat combat = CombatPopUp.GetComponent<Combat>();
        combat.StageIndex = StageIndex;
        combat.SetEnemy(enemyIndex);
        MyGameManager.Instance.PlayBGM(BattleBGM);
    }

    IEnumerator ScaleUpHero()
    {
        yield return new WaitForSeconds(0.1f);
        LevelManager.Instance.Players[0].transform.localScale = new Vector3(HeroScale, HeroScale, 2);
    }

    public void EndBattle()
    {
        BattleTransition.Play("OpenAndClose");
        StartCoroutine(CloseCombatLater());
    }
    IEnumerator CloseCombatLater()
    {

        yield return new WaitForSeconds(1);
        MainCamera.SetActive(true);
        CombatCamera.SetActive(false);
        CombatPopUp.SetActive(false);
        EnemyList[CurrentEnemyIndex].SetActive(false);

        if (StageIndex == 1 && CurrentEnemyIndex == 1)
        {
            MyGameManager.Instance.IsFriendUnlocked = true;
            DeerCage.SetActive(false);
            StartCoroutine(TalkDeerLater());
        }
        MyGameManager.Instance.PlayBGM(bgm);
    }

    IEnumerator TalkDeerLater()
    {
        yield return new WaitForSeconds(1);
        TheDialogue.ShowDialogue(DeerEscapeDiag, OnDeerDiagEnd);

    }
    public void OnDeerDiagEnd()
    {
        TheDialogue.gameObject.SetActive(false);
        Deer.SetActive(false);
        //this.gameObject.SetActive(false);

        print("4");
    }

}
