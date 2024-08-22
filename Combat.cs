using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Combat : MonoBehaviour
{
    public List<GameObject> EnemySpriteList;
    public List<GameObject> HeroSpriteList;
    public int CurrentTurn = 0;
    public GameObject box;
    public int EnemyHP;
    public int InitialEnemyHP;
    public Image EnemyHPBarImage;
    public Text EnemyBarHP;
    public List<int> AllyHPList;
    public List<int> InitialAllyHP;
    public List<int> AllyMPList;
    public List<int> InitialAllyMP;
    public List<int> EnemyHPList;
    public List<Image> ImgAllyHPList;
    public List<Image> ImgAllyMPList;
    public List<int> EnemyDMGList;
    public int EnemyNum;
    public int CurrentTabIndex;
    public Text AttackButton;
    public Text SpecialButton;
    public Image AllyBarMP;
    public int TargetNum;
    public int StageIndex;
    public GameScript TheGameScript;
    public List<int> AllyDMGList;
    public GameObject DmgText;
    public GameObject EnemyPosition;
    public GameObject AllyPosition;
    public SceneTransition TheTransition;
    public AudioClip HitSound;
    public AudioClip AllyDieSound;
    public AudioClip HealSound;
    public AudioClip ShieldSound;



    // Start is called before the first frame update
    void Start()
    {
        //SetEnemy(0);
        //SetHero(0);
        //SetTurn(CurrentTurn);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetEnemy(int index)
    {
        EnemyNum = index;
        print(string.Format("EnemyNum {0}/{1}" ,index, StageIndex));
        foreach (Transform item in AllyPosition.transform)
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in EnemyPosition.transform)
        {
            Destroy(item.gameObject);
        }
        ImgAllyMPList[2].transform.parent.parent.gameObject.SetActive(MyGameManager.Instance.IsFriendUnlocked);
        AllyHPList = new List<int>() {150, 130, 130}; //hp of allies
        AllyDMGList = new List<int>() { 40, 30, 20 }; //atk dmg of allies
        InitialAllyHP = new List<int>() { 150, 130, 130 }; //initial hp
        AllyMPList = new List<int>() { 200, 170, 180 };//mp allies
        InitialAllyMP = new List<int>() { 200, 170, 180 };//initial mp
        if (StageIndex == 0) //for enemies on stage 0
        {
            EnemyDMGList = new List<int>() { 40, 50, 70 };
            EnemyHPList = new List<int>() { 125, 90, 310 };
        }
        else if (StageIndex == 1) //new stats for new enemies on stage 1
        {
            EnemyDMGList = new List<int>() { 45, 50, 74 };
            EnemyHPList = new List<int>() { 125, 90, 310 };
        }
        else if (StageIndex == 2) //new stats for new enemies on stage 1
        {
            EnemyDMGList = new List<int>() { 45, 50, 120,74 };
            EnemyHPList = new List<int>() { 126, 91,101 ,311 };
            print("set stage index 2 hp 3 -> " + EnemyHPList[3]);
        }
        else if (StageIndex == 3) //new stats for new enemies on stage 1
        {
            EnemyDMGList = new List<int>() { 45, 50, 120, 74 };
            EnemyHPList = new List<int>() { 125, 90, 100, 310 };
        }


        else if (StageIndex == 4) //new stats for new enemies on stage 1
        {
            EnemyDMGList = new List<int>() { 45, 50, 120, 74 };
            EnemyHPList = new List<int>() { 125, 90, 100, 310 };
        }
        else if (StageIndex == 5) //new stats for new enemies on stage 1
        {
            EnemyDMGList = new List<int>() { 45, 50, 120, 74 };
            EnemyHPList = new List<int>() { 125, 90, 100, 310 };
        }
        else if (StageIndex == 6) //new stats for new enemies on stage 1
        {
            EnemyDMGList = new List<int>() { 45, 50, 120, 74 };
            EnemyHPList = new List<int>() { 125, 90, 100, 310 };
        }
        else if (StageIndex == 7) //new stats for new enemies on stage 1
        {
            EnemyDMGList = new List<int>() { 45, 50, 120, 74 };
            EnemyHPList = new List<int>() { 125, 90, 100, 310 };
        }

        EnemyHPBarImage.fillAmount = 1;
        foreach (var item in ImgAllyHPList)
        {
            item.fillAmount = 1;
        }
        for (int i = 0; i < EnemyDMGList.Count; i++)
        {
            EnemySpriteList[i].SetActive(i == EnemyNum);

        }
        
        EnemyHP = EnemyHPList[index]; //Choosing hp depending on what enemy it is

        InitialEnemyHP = EnemyHP;
        EnemyBarHP.text = EnemyHP.ToString() + " / " + InitialEnemyHP.ToString();
        SetHero(0);
        CurrentTurn = 0;
        SetTurn(0);

    }
    public void SetHero(int index)
    {
        HeroSpriteList[0].SetActive(index == 0);
        HeroSpriteList[1].SetActive(index == 1);
        HeroSpriteList[2].SetActive(index == 2);
        //HeroSpriteList[0].SetActive(false);
        //HeroSpriteList[1].SetActive(false);
        //HeroSpriteList[2].SetActive(false);
        //if (index == 0)
        //{
        //    HeroSpriteList[0].SetActive(true);

        //} else if (index == 1)
        //{
        //    HeroSpriteList[1].SetActive(true);

        //}
        //else if (index == 2)
        //{
        //    HeroSpriteList[2].SetActive(true);
        //}
    }
    public void SetTurn(int index)
    {
        if (DeathCheck() )
        {
            this.gameObject.SetActive(false);
            // Go to game over scene
            TheTransition.GoToScene("GameOver");
        }
        if (index == 2 && !MyGameManager.Instance.IsFriendUnlocked) //friend doesnt appear if not saved
        {
            CurrentTurn += 1;
            SetTurn(CurrentTurn);
            return;
        }

        if (index == 3)
        {
            StartCoroutine(EnemySchedule());
        }
        else
        {
            if (AllyHPList[index] <= 0)
            {
                CurrentTurn += 1; 
                SetTurn(CurrentTurn);
                return;
            } 
            box.SetActive(true);
            SetHero(index);
            OnTabClick(0);
        }
    }
    IEnumerator EnemySchedule()
    {
        box.SetActive(false);
        EnemySpriteList[EnemyNum].GetComponent<SPUM_Prefabs>().PlayAnimation("2_Attack_Normal");
        Transform root = EnemySpriteList[EnemyNum].transform.Find("UnitRoot");
        if (root == null)
        {
            root = EnemySpriteList[EnemyNum].transform.Find("HorseRoot");
        }
        root.GetComponent<Animator>().SetTrigger("Attack");

        int maxtarget = 2;
        if (MyGameManager.Instance.IsFriendUnlocked)
        {
            maxtarget = 3;
        }
        int target = UnityEngine.Random.Range(0, maxtarget);
        int loopCounter = 0;
        while (loopCounter < 1000)
        {
            target = UnityEngine.Random.Range(0, maxtarget);
            if (AllyHPList[target] > 0)
            {
                break;
            }
            loopCounter++;
        }
        SetHero(target);
        MyGameManager.Instance.PlaySound(HitSound);
        yield return new WaitForSeconds(0.5f); //wait for 0.5s until next turn

        if (TargetNum == 1 || TargetNum == 2)
        {
            target = TargetNum;
        }
        TargetNum = 0;
        int dmg = EnemyDMGList[EnemyNum];
        AllyHPList[target] -= dmg;
        if (AllyHPList[target] <= 0)
        {
            MyGameManager.Instance.PlaySound(AllyDieSound);

        }

        CreateDmgText(AllyPosition, dmg, false);
        ImgAllyHPList[target].fillAmount = 1f * AllyHPList[target] / InitialAllyHP[target];
        CurrentTurn = 0;
        SetTurn(CurrentTurn);
    }
    public void CreateDmgText(GameObject target, int dmg, bool isCrit)
    {
        GameObject obj = Instantiate(DmgText, target.transform);
        obj.transform.position = target.transform.position;
        obj.GetComponent<Text>().text = dmg.ToString();
        if (isCrit)
        {
            obj.transform.Find("Image").gameObject.SetActive(true);
            obj.GetComponent<Text>().color = new Color(248/255f, 103/255f, 11/255f);
        }
    }

    public void OnAttackClick() //skill 1 button
    {
        StartCoroutine(AttackSchedule());
       
    }
    IEnumerator AttackSchedule() //animation schedule
    {
        if (CurrentTabIndex == 0) //Attack
        {
            if (CurrentTurn == 2)
            {
                HeroSpriteList[CurrentTurn].transform.Find("UnitRoot").GetComponent<Animator>().SetTrigger("Attack");

            }
            else
            {
                HeroSpriteList[CurrentTurn].GetComponent<SPUM_Prefabs>().PlayAnimation("2_Attack_Normal");
            }
            MyGameManager.Instance.PlaySound(HitSound);
            yield return new WaitForSeconds(0.5f); //wait for 0.5s until next turn
            int dmg;
            bool isCrit = false;
            if (Random.Range(0,100) < 15)
            {
                dmg = AllyDMGList[CurrentTurn] * 2;
                isCrit = true;
            }
            else
            {
                dmg = AllyDMGList[CurrentTurn];
            }
            EnemyHP -= dmg;
            CreateDmgText(EnemyPosition, dmg, isCrit);
            EnemyHPBarImage.fillAmount = 1f * EnemyHP / InitialEnemyHP;
            EnemyBarHP.text = EnemyHP.ToString() + " / " + InitialEnemyHP.ToString();
            if (EnemyHP <= 0)
            {
                GoToNextStage();
            }
            CurrentTurn += 1;
            SetTurn(CurrentTurn);
        }
        else if (CurrentTabIndex == 1) //Heal
        {
            if (AllyMPList[CurrentTurn] >= 50) // if ally's mp greater than or equal to 50
            {
                if (CurrentTurn == 2)
                {
                    HeroSpriteList[CurrentTurn].transform.Find("UnitRoot").GetComponent<Animator>().SetFloat("NormalState", 2);
                    HeroSpriteList[CurrentTurn].transform.Find("UnitRoot").GetComponent<Animator>().SetTrigger("Attack");

                }
                else
                {
                    HeroSpriteList[CurrentTurn].GetComponent<SPUM_Prefabs>().PlayAnimation("2_Attack_Magic");
                }
                MyGameManager.Instance.PlaySound(HealSound);

                yield return new WaitForSeconds(0.5f); //wait for 0.5s until next turn
                AllyMPList[CurrentTurn] -= 50;
                ImgAllyMPList[CurrentTurn].fillAmount = 1f * AllyMPList[CurrentTurn] / InitialAllyMP[CurrentTurn];
                AllyHPList[CurrentTurn] += (int)(0.3f * InitialAllyHP[CurrentTurn]);
                ImgAllyHPList[CurrentTurn].fillAmount = 1f * AllyHPList[CurrentTurn] / InitialAllyHP[CurrentTurn];
                if (AllyHPList[CurrentTurn] > InitialAllyHP[CurrentTurn])
                {
                    AllyHPList[CurrentTurn] = InitialAllyHP[CurrentTurn];
                }
                CurrentTurn += 1;
                SetTurn(CurrentTurn);
            }
            else
            {
                print("Not Enough MP!");
            }
            
        }
    }
    public void GoToNextStage() //going to next stage
    {
        bool isboss = false;
        if (MyGameManager.Instance.StageNumber == 0)
        {
            isboss = true;
        } else
        {
            if (EnemyNum == EnemySpriteList.Count-1)
            {
                isboss = true;
            }
            else
            {
                //this.gameObject.SetActive(false);
                TheGameScript.EndBattle();
            }
        }
        if (isboss) //if boss killed go to next stage
        {
            //this.gameObject.SetActive(false);
            if (MyGameManager.Instance.StageNumber == 1) // HERE: CHANGE STAGE NUMBER DEPENDING ON WHAT STAGE IT IS RIGHT BEFORE THE ENDING!
            {

                TheTransition.GoToScene("Ending");
                return;
            }
            TheTransition.GoToScene("Stage " + (MyGameManager.Instance.StageNumber + 1));
        }
        else //if non-boss entity killed just close combat pop up
        {
            //this.gameObject.SetActive(false);
        }
        
    }
    

    public void OnSpecialClick()
    {
        StartCoroutine(SpecialSchedule()) ;
    }
    IEnumerator SpecialSchedule()
    {

        if (CurrentTabIndex == 0 && AllyMPList[CurrentTurn] >= 50) //Special Atk
        {
            if (CurrentTurn == 2)
            {
                HeroSpriteList[CurrentTurn].transform.Find("UnitRoot").GetComponent<Animator>().SetFloat("NormalState", 1);
                HeroSpriteList[CurrentTurn].transform.Find("UnitRoot").GetComponent<Animator>().SetTrigger("Attack");

            }
            else
            {
                HeroSpriteList[CurrentTurn].GetComponent<SPUM_Prefabs>().PlayAnimation("2_Attack_Bow");
            }
            MyGameManager.Instance.PlaySound(HitSound);
            yield return new WaitForSeconds(1.5f); //wait for 1.5s until next turn
            int dmg = (int)(AllyDMGList[CurrentTurn] * 1.3f);
            EnemyHP -= dmg;
            CreateDmgText(EnemyPosition, dmg, false);
            AllyMPList[CurrentTurn] -= 50;
            ImgAllyMPList[CurrentTurn].fillAmount = 1f * AllyMPList[CurrentTurn] / InitialAllyMP[CurrentTurn];
            EnemyHPBarImage.fillAmount = 1f * EnemyHP / InitialEnemyHP;
            EnemyBarHP.text = EnemyHP.ToString() + " / " + InitialEnemyHP.ToString();
            if (EnemyHP <= 0)
            {
                GoToNextStage();
            }
            CurrentTurn += 1;
            SetTurn(CurrentTurn);
        }
        else if (CurrentTabIndex == 1 && AllyMPList[CurrentTurn] >= 40) //Shield
        {
            if (CurrentTurn == 2)
            {
                HeroSpriteList[CurrentTurn].transform.Find("UnitRoot").GetComponent<Animator>().SetFloat("NormalState", 2);
                HeroSpriteList[CurrentTurn].transform.Find("UnitRoot").GetComponent<Animator>().SetTrigger("Attack");

            }
            else
            {
                HeroSpriteList[CurrentTurn].GetComponent<SPUM_Prefabs>().PlayAnimation("5_Skill_Bow");
            }
            MyGameManager.Instance.PlaySound(ShieldSound);

            yield return new WaitForSeconds(1); //wait for 1s until next turn
            AllyMPList[CurrentTurn] -= 40;
            ImgAllyMPList[CurrentTurn].fillAmount = 1f * AllyMPList[CurrentTurn] / InitialAllyMP[CurrentTurn];
            TargetNum = Random.Range(1, 3);
            CurrentTurn += 1;
            SetTurn(CurrentTurn);
        }
    }

    public bool DeathCheck()
    {
        bool result = true;
        for (int i = 0; i < AllyHPList.Count; i++)
        {
            if (!MyGameManager.Instance.IsFriendUnlocked && i == 2)
            {
                continue;
            }

            if (AllyHPList[i] > 0)
            {
                result = false;
                break;
            }
            //if (MyGameManager.Instance.IsFriendUnlocked)
            //{
            //    maxdeath = 3;
            //}
            //if (maxdeath == 3)
            //{
            //    if (AllyHPList[i] > 0)
            //    {
            //        result = false;
            //        break;
            //    }
            //}
            //else if (maxdeath == 2)
            //{
            //    if (AllyHPList[0] >= 0 && AllyHPList[1] >= 0)
            //    {
            //        result = false;
            //        break;
            //    }
            //}

        }
        //print("Not dead");
        return result;
        //if (AllyHPList[0] <= 0 && AllyHPList[1] <= 0 && AllyHPList[2] <= 0)
        //{
        //    return true;
        //} else
        //{
        //    return false;
        //}
    }
    public void OnTabClick(int index)
    {
        CurrentTabIndex = index;
        if (index == 0)
        {
            AttackButton.text = "Attack";
            SpecialButton.text = "Special";
            SpecialButton.transform.parent.gameObject.SetActive(true);
        }
        else if (index == 1)
        {
            if (CurrentTurn == 0)
            {
                SpecialButton.transform.parent.gameObject.SetActive(true);
            }
            else
            {
                SpecialButton.transform.parent.gameObject.SetActive(false);
            }
            AttackButton.text = "Heal";
            SpecialButton.text = "Shield";
        }
    }
}
