using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_PlayerMovement : MonoBehaviour
{
    public GameObject actualTile;
    public GameObject pointer;
    public GameObject ballon;

    public scr_GridCreation gridScript;
    public scr_PointerBehaviour pointerScript;
    public scr_PlayerProperties playerScript;
    public scr_HudSet tileScript;
    public scr_SpellButton spellBScript;
    public scr_BallonBehaviour ballonScript;

    public Vector3 pointerPos;
    public Vector3 playerPos;

    public bool playerPositioned = false;
    public bool deplacementTriggered = false;
    public bool HUDSet = false;

    public List<GameObject> path = new List<GameObject>();
    
    public GameObject[] allTiles;

    // Start is called before the first frame update
    void Start()
    {
        ballon = GameObject.FindGameObjectWithTag("ballon");
        ballonScript = ballon.GetComponent<scr_BallonBehaviour>();

        gridScript = GameObject.FindGameObjectWithTag("manager").GetComponent<scr_GridCreation>();
        playerScript = this.gameObject.GetComponent<scr_PlayerProperties>();
        pointerScript = pointer.GetComponent<scr_PointerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (scr_TileBehaviour.gridCreated)
        {
            allTiles = GameObject.FindGameObjectsWithTag("tile");
        }

            //positionner le joueur

        if (scr_TileBehaviour.gridCreated && playerPositioned == false)
        {
            GetFirstPos();
        }

        transform.position = new Vector3 (actualTile.transform.position.x, actualTile.transform.position.y, -2)  ;

        //

        playerPos = transform.position;
        pointerPos = pointer.transform.position;


        if (Input.GetMouseButtonDown(0) && deplacementTriggered)
        {
            pointerScript.listeIncrementFlag = true;

            if (!HUDSet && !spellBScript.spellOnHand)
            { 
                SetHUD();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            pointerScript.listeIncrementFlag = false;

            if (pointerScript.casesTraversees.Count > 0)
            {
                actualTile = pointerScript.casesTraversees[pointerScript.casesTraversees.Count - 1];

                playerScript.PM = playerScript.actualMaxPM;

                pointerScript.casesTraversees.Clear();

                
            }
            for (int i = 0; i < allTiles.Length; i++)
            {
                scr_TileBehaviour tileI;
                scr_HudSet hudScript;
                hudScript = allTiles[i].GetComponent<scr_HudSet>();
                tileI = allTiles[i].GetComponent<scr_TileBehaviour>();
                tileI.ResetSprite();
                tileI.isHUD = false;
                tileI.sprRenderer.color = Color.white;
                hudScript.checkFlag = false;
            }
            HUDSet = false;

            scr_HudSet.mRangeActuel = 0;



        }

        //verifier si le ballon est touché

        if (actualTile == ballonScript.actualBallonTile && playerPositioned)
        {
            ballonScript.ballonHolded = true;
            ballonScript.player = this.gameObject;
        }
    }

    void GetFirstPos()
    {
        actualTile = gridScript.grid[Random.Range(0, 8), Random.Range(0, 8)];
        playerPositioned = true;
    }

    void SetHUD()
    {
        tileScript = actualTile.GetComponent<scr_HudSet>();
        tileScript.CheckingForHUD(playerScript.actualMaxPM);
        
        HUDSet = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "mouse")
        {
            pointerScript.playerPScript = this.gameObject.GetComponent<scr_PlayerProperties>();
            pointerScript.playerMScript = this;
            deplacementTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "mouse")
        {
            deplacementTriggered = false;
        }
    }
    
}
