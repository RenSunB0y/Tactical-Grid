using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_PlayerProperties : MonoBehaviour
{
    public int PM;
    public int actualMaxPM;
    public int PV;

    private void Start()
    {
        actualMaxPM = PM;
    }
}


