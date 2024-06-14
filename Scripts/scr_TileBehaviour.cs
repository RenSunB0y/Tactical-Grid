using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_TileBehaviour : MonoBehaviour
{
    public SpriteRenderer sprRenderer;

    public scr_GridCreation gridScript;
    public scr_TileBehaviour neighbourTileScript;
    public scr_TileBehaviour actualNeighborTileScript;
    public scr_BallonBehaviour ballonScript;

    public GameObject tileInstanciee;
    public GameObject ballon;

    public GameObject tileSprite;
    public GameObject tileHUD;
    public GameObject tileWalking;

    public GameObject neighbourDroit;
    public GameObject neighbourGauche;
    public GameObject neighbourHaut;
    public GameObject neighbourBas;

    public static int rangePM;
    public static int rangeDFS;

    public List<GameObject> neighbours = new List<GameObject>();
    public List<GameObject> shortestPath = new List<GameObject>();

    public int x;
    public int y;

    public static bool stopFlag = false;
    public static bool gridCreated = false;
    public bool neighboursAdded = false;
    public bool isHUD = false;
    public bool blackFlag = false;
    public bool isMDG = false;

    public bool isHudCheck = false;

    public int poids;

    private void Start()
    {
        ballon = GameObject.FindGameObjectWithTag("ballon");
        sprRenderer = this.GetComponentInChildren<SpriteRenderer>();
        ballonScript = ballon.GetComponent<scr_BallonBehaviour>();


        if (x == 4 && y == 4)
        {
            ballonScript.GetStartPos(this.gameObject);
        }

        if (x == 3 && y ==0)
        {
            tileInstanciee = Instantiate(gridScript.tileButGauche, new Vector3(transform.position.x + .3f, transform.position.y + .52f, 1.5f), Quaternion.identity);
            neighbourHaut = tileInstanciee;
            GetParentAndTag();
        }

        if (x == 4 && y == 0)
        {
            tileInstanciee = Instantiate(gridScript.tileButMilieu, new Vector3(transform.position.x + .3f, transform.position.y + .52f, 0.5f), Quaternion.identity);
            neighbourHaut = tileInstanciee;
            GetParentAndTag();
        }

        if (x == 5 && y == 0)
        {
            tileInstanciee = Instantiate(gridScript.tileButDroit, new Vector3(transform.position.x + .3f, transform.position.y + .52f, 0), Quaternion.identity);
            neighbourHaut = tileInstanciee;
            GetParentAndTag();
        }

        ///

        if (x == 3 && y == 8)
        {
            tileInstanciee = Instantiate(gridScript.tileButGauche, new Vector3(transform.position.x - .3f, transform.position.y - .52f, -.5f), Quaternion.identity);
            neighbourBas= tileInstanciee;
            GetParentAndTag();
        }

        if (x == 4 && y == 8)
        {
            tileInstanciee = Instantiate(gridScript.tileButMilieu, new Vector3(transform.position.x - .3f, transform.position.y - .52f, -2f), Quaternion.identity);
            neighbourBas = tileInstanciee;
            GetParentAndTag();
            
        }

        if (x == 5 && y == 8)
        {
            tileInstanciee = Instantiate(gridScript.tileButDroit, new Vector3(transform.position.x - .3f, transform.position.y - .52f, -2.5f), Quaternion.identity);
            neighbourBas = tileInstanciee;
            GetParentAndTag();
        }

    }

    private void GetParentAndTag()
    {
        tileInstanciee.transform.parent = gridScript.gameObject.transform;
        tileInstanciee.tag = "but";
    }

    private void Update()
    {
        if (gridCreated && !neighboursAdded)
        {
            
            GetNeighbourhood();
        }

        if (isHUD && !isHudCheck)
        {
            SetHUDSprite();
        }
    }

    public void SetTriggerTrue()
    {
        gridCreated = true;
    }

    public void GetNeighbourhood()
    {

        if (y < 8)
        {
            neighbourBas = gridScript.grid[x, y + 1];
            neighbours.Add(neighbourBas);
        }

        if (y > 0)
        {
            neighbourHaut = gridScript.grid[x, y - 1];
            neighbours.Add(neighbourHaut);
        }

        if (x > 0)
        {
            neighbourGauche = gridScript.grid[x - 1, y];
            neighbours.Add(neighbourGauche);
        }

        if (x < 8)
        {
            neighbourDroit = gridScript.grid[x + 1, y];
            neighbours.Add(neighbourDroit);
        }

        neighboursAdded = true;
    }

    public void SetHUDSprite()
    {
        tileSprite.SetActive(false);
        tileHUD.SetActive(true);
        tileWalking.SetActive(false);

        isHudCheck = true;
    }

    public void ResetSprite()
    {
        tileSprite.SetActive(true);
        tileHUD.SetActive(false);
        tileWalking.SetActive(false);

        isHudCheck = false;
    }

    public void SetWalkingSprite()
    {
        tileSprite.SetActive(false);
        tileHUD.SetActive(false);
        tileWalking.SetActive(true);
    }

}