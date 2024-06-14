using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_SpellManager : MonoBehaviour
{
    public int spell;
    public int lastspell;
    public float instX;
    public float instY;
    public int gridX;
    public int gridY;

    public string spellName;

    public scr_SpellButton spellBScript;
    public scr_GridCreation gridScript;
    public scr_TileBehaviour tileScript;
    public scr_PointerBehaviour pointerScript;

    public Text spellText;

    public GameObject blocDeGlace;
    public GameObject actualTileInst;

    // Start is called before the first frame update
    void Start()
    {
        SelectSpell(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectSpell()
    {
        spell = 3;

        /*spell = Random.Range(1, 6);

        if (spell == lastspell)
        {
            SelectSpell();
            return;
        }
        lastspell = spell;  */

        SetName();
    }

    public void SetName()
    {
        switch (spell)
        {
            case 1:
                spellName = "Balle Explosive";

                break;

            case 2:
                spellName = "Ankle Break";

                break;

            case 3:
                spellName = "Mur de Glace";

                break;

            case 4:
                spellName = "Teleportation";

                break;

            case 5:
                spellName = "Ballon flamboyant";

                break;

            case 6:
                spellName = "Le Bloqueur";

                break;
        }
        spellText.text = spellName;
    }

    public void ActivateRightSpell(int x, int y)
    {
        gridX = x;
        gridY = y;

        actualTileInst = gridScript.grid[gridX, gridY];
        tileScript = actualTileInst.GetComponent<scr_TileBehaviour>();

        switch (spell)
        {
            case 1:
                BalleExplosive();

                break;

            case 2:
                AnkleBreak()
;
                break;

            case 3:
                MurDeGlace();

                break;

            case 4:
                Teleportation();

                break;

            case 5:
                BallonFlamboyant();

                break;

            case 6:
                LeBloqueur();

                break;

        }
    }

    void BalleExplosive()
    {

    }

    void AnkleBreak()
    {
            
    }

    void MurDeGlace()
    {
        instX = actualTileInst.transform.position.x;
        instY = actualTileInst.transform.position.y;

        if (!tileScript.isMDG)
        {
            Instantiate(blocDeGlace, new Vector3(instX, instY, -1), Quaternion.identity);
            tileScript.isMDG = true;
        }

        foreach(GameObject tile in tileScript.neighbours)
        {
            scr_TileBehaviour ngTileScript;
            ngTileScript = tile.GetComponent<scr_TileBehaviour>();

            if (pointerScript.isH)
            {
                if (ngTileScript.y == tileScript.y && !ngTileScript.isMDG)
                {
                    Instantiate(blocDeGlace, new Vector3(tile.transform.position.x, tile.transform.position.y, -1), Quaternion.identity);
                    ngTileScript.isMDG = true;
                }
            }
            else
            {
                if (ngTileScript.x == tileScript.x && !ngTileScript.isMDG)
                {
                    Instantiate(blocDeGlace, new Vector3(tile.transform.position.x, tile.transform.position.y, -1), Quaternion.identity);
                    ngTileScript.isMDG = true;
                }
            }
        }

    }

    void Teleportation()
    {
            
    }

    void BallonFlamboyant()
    {
            
    }

    void LeBloqueur()
    {

    }

}
