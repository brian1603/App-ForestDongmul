using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLater : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyText());
    }
     
IEnumerator DestroyText()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
