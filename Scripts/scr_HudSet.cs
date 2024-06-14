using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_HudSet : MonoBehaviour
{
	public GameObject startTile;
	public GameObject tileVisited;
	public GameObject previousTile;

	public SpriteRenderer sprRenderer;

	public int mRange;
	public static int mRangeActuel = 0;
	public int neighborNumber = 0;
	public static int lowestNeighborNumber;

	public scr_PlayerProperties playerPScript;
	public scr_PlayerMovement playerMScript;
	public scr_TileBehaviour tileScript;
	public scr_TileBehaviour startingTileScript;

	public List<GameObject> overallHUD = new List<GameObject>();
	public List<GameObject> actualVisits = new List<GameObject>();

	public bool checkFlag = false;

    private void Start()
    {
		playerMScript = GameObject.FindGameObjectWithTag("Player").GetComponent<scr_PlayerMovement>();
		playerPScript = GameObject.FindGameObjectWithTag("Player").GetComponent<scr_PlayerProperties>();
		mRange = playerPScript.actualMaxPM;

		sprRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
		startTile = playerMScript.actualTile;
		mRange = playerPScript.actualMaxPM;
	}

	public void CheckingForHUD(int pmRange)
	{
		tileScript = this.GetComponentInParent<scr_TileBehaviour>();

		if(checkFlag)
        {
			return;
        }
		tileScript.isHUD = true;

		//sprRenderer.color = Color.black;

		if (pmRange > 0)
		{
			foreach (GameObject tile in tileScript.neighbours)
            {
				scr_TileBehaviour acTileScript;
				scr_HudSet acHUDScript;
				acTileScript = tile.GetComponent<scr_TileBehaviour>();
				acHUDScript = tile.GetComponent<scr_HudSet>();

				if (!acTileScript.isMDG)
                {
					acHUDScript.CheckingForHUD(pmRange - 1);
                }
            }

			foreach( GameObject tile in playerMScript.allTiles)
            {
				scr_HudSet hudScriptAllTiles = tile.GetComponent<scr_HudSet>();
				hudScriptAllTiles.checkFlag = false;
            }
        }

	}
}
