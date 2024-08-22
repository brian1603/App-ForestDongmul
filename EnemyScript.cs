using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int EnemyIndex;
    public GameScript TheGameScript;
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
        StartBattle();
    }
    public void StartBattle()
    {
        TheGameScript.StartBattle(EnemyIndex);
    }
}
