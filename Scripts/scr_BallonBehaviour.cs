using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_BallonBehaviour : MonoBehaviour
{
    public GameObject startTile;
    public GameObject player;
    public GameObject actualBallonTile;
    public GameObject spriteShootButton;

    public scr_ShootButton shootButtonScript;

    public bool placed = false;
    public bool ballonHolded = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ballonHolded && placed)
        {
            transform.position = player.transform.position;
        }

        if (ballonHolded && shootButtonScript.displayed == true)
        {
            spriteShootButton.SetActive(false);
            shootButtonScript.displayed = false;
        }

        if (ballonHolded && shootButtonScript.displayed == false)
        { 
            spriteShootButton.SetActive(true);
            shootButtonScript.displayed = true;
        }
    }

    public void GetStartPos(GameObject startTilePassed)
    {
        startTile = startTilePassed;
        transform.position = startTile.transform.position;
        actualBallonTile = startTile;

        placed = true;
    }
}
