using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform CameraTarget;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetTargetLater());
    }
    IEnumerator SetTargetLater()
    {
        yield return new WaitForSeconds(0.5f);
        CameraTarget = LevelManager.Instance.Players[0].transform;
    }
    // Update is called once per frame
    void Update()
    {

        if (CameraTarget != null)
        {
            transform.position = new Vector3(CameraTarget.position.x, CameraTarget.position.y, transform.position.z);
        }
    }
}
