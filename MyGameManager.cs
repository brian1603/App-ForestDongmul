using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MyGameManager : MonoBehaviour
{
    public int StageNumber;
    public bool IsFriendUnlocked;

    private static MyGameManager _instance;
    public static MyGameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType(typeof(MyGameManager)) as MyGameManager;
                if (!_instance)
                {
                    GameObject container = new GameObject();
                    _instance = container.AddComponent(typeof(MyGameManager)) as MyGameManager;
                    container.AddComponent<AudioSource>();
                    DontDestroyOnLoad(_instance);
                }
            }
            return _instance;
        }
    }
    public void PlaySound(AudioClip clip)
    {
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();
    }
    public void PlayBGM(AudioClip clip)
    {
        if (transform.Find("bgm") == null)
        {
            GameObject obj = new GameObject();
            obj.transform.parent = this.transform;
            obj.name = "bgm";
            obj.AddComponent<AudioSource>();
        }
        transform.Find("bgm").GetComponent<AudioSource>().clip = clip;
        transform.Find("bgm").GetComponent<AudioSource>().Play();
    }
}

