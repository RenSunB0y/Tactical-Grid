using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_SpellHoldedScript : MonoBehaviour
{
    public GameObject pointer;

    // Start is called before the first frame update
    void Start()
    {
        pointer = GameObject.FindGameObjectWithTag("mouse");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = pointer.transform.position;
    }
}
