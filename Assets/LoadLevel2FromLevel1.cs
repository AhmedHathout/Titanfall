using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel2FromLevel1 : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<CallTitan>().LoadLevel2();
    }
}
