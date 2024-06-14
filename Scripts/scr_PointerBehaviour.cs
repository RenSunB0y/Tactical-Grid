using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_PointerBehaviour : MonoBehaviour
{
    public Vector3 mousePos;

    public List<GameObject> casesTraversees = new List<GameObject>();
    public List<GameObject> neighboursActuels = new List<GameObject>();

    public bool listeIncrementFlag = false;
    public bool horizontal = false;
    public bool isH = false;

    public scr_PlayerProperties playerPScript;
    public scr_PlayerMovement playerMScript;
    public scr_TileBehaviour tileScript;
    public scr_SpellButton spellBScript;
    public scr_SpellManager spellManagerScript;

    public scr_TileBehaviour _tileScriptForSpell;

    public GameObject caseConcernee;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 1;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = mousePos;

        if (Input.GetKeyDown(KeyCode.C))
        {
            //ResetTiles();
            if (horizontal)
            {
                horizontal = false;
            }
            else
            {
                horizontal = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == playerMScript.actualTile)
        {
            playerMScript.deplacementTriggered = true;
        }

        if (collision.tag == "tile" && listeIncrementFlag && !spellBScript.spellOnHand)
        {
            if (casesTraversees.Count > 0)
            {
                caseConcernee = casesTraversees[casesTraversees.Count - 1];
            }
            else
            {
                caseConcernee = playerMScript.actualTile;
            }

            tileScript = caseConcernee.GetComponent<scr_TileBehaviour>();

            neighboursActuels.Clear();

            neighboursActuels.Add(tileScript.neighbourBas);
            neighboursActuels.Add(tileScript.neighbourHaut);
            neighboursActuels.Add(tileScript.neighbourGauche);
            neighboursActuels.Add(tileScript.neighbourDroit);

            if (casesTraversees.Contains(collision.gameObject) == false && neighboursActuels.Contains(collision.gameObject) && collision.gameObject != playerMScript.actualTile && !collision.gameObject.GetComponent<scr_TileBehaviour>().isMDG)
            {
                if (playerPScript.PM > 0)
                {
                    Color defaultTileColor;
                    casesTraversees.Add(collision.gameObject);
                    defaultTileColor = collision.GetComponent<scr_TileBehaviour>().sprRenderer.color;
                    collision.GetComponent<scr_TileBehaviour>().SetWalkingSprite();
                    playerPScript.PM -= 1;
                }

            }

            else if (casesTraversees.Contains(collision.gameObject) == true && collision.gameObject != playerMScript.actualTile)
            {
                
                int _index;
                _index = casesTraversees.IndexOf(collision.gameObject);
              
                int _countToIndex = (casesTraversees.Count - _index);

                for (int i = 0; i < _countToIndex - 1; i++)
                {
                    playerPScript.PM += 1;
                    casesTraversees[casesTraversees.Count - 1].GetComponent<scr_TileBehaviour>().ResetSprite();
                    casesTraversees.RemoveAt(casesTraversees.Count - 1);
                }
                caseConcernee = collision.gameObject;

            }

            else if (collision.gameObject == playerMScript.actualTile)
            {
                foreach(GameObject tile in casesTraversees)
                {
                    tile.GetComponent<scr_TileBehaviour>().ResetSprite();
                }

                casesTraversees.Clear();
                caseConcernee = playerMScript.actualTile;
                playerPScript.PM = playerPScript.actualMaxPM;
            }

         

        }
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "tile")
        {
            //// Spell Behaviour

            if (spellBScript.spellOnHand && !listeIncrementFlag)
            {
                scr_TileBehaviour _tileScriptForSpell;
                _tileScriptForSpell = collision.gameObject.GetComponent<scr_TileBehaviour>();
                _tileScriptForSpell.sprRenderer.color = Color.red;

                //si MUR DE GLACE
                if (spellManagerScript.spell == 3)
                {
                    foreach (GameObject tile in _tileScriptForSpell.neighbours)
                    {
                        scr_TileBehaviour _actualTile;
                        _actualTile = tile.GetComponent<scr_TileBehaviour>();
                        if (horizontal)
                        {
                            if (_actualTile.y == _tileScriptForSpell.y)
                            {
                                _actualTile.sprRenderer.color = Color.red;
                            }
                            else
                            {
                                _actualTile.sprRenderer.color = Color.white;
                            }
                        }

                        else
                            if (_actualTile.x == _tileScriptForSpell.x)
                        {
                            _actualTile.sprRenderer.color = Color.red;
                        }
                        else
                        {
                            _actualTile.sprRenderer.color = Color.white;
                        }
                    }
                }

                ///
                spellBScript.Activate(_tileScriptForSpell.x, _tileScriptForSpell.y);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == playerMScript.actualTile)
        {
            playerMScript.deplacementTriggered = false;
        }

        if (collision.tag == "tile")
        {

            if (spellBScript.spellOnHand && !listeIncrementFlag)
            {
                
                _tileScriptForSpell = collision.gameObject.GetComponent<scr_TileBehaviour>();
                _tileScriptForSpell.sprRenderer.color = Color.white;

                //si MUR DE GLACE
                if (spellManagerScript.spell == 3)
                {
                    ResetTiles();
                }


                    spellBScript.UnActivate();
            }
        }

    }

    void ResetTiles()
    {
        foreach (GameObject tile in _tileScriptForSpell.neighbours)
        {
            scr_TileBehaviour _actualTile;
            _actualTile = tile.GetComponent<scr_TileBehaviour>();
            _actualTile.sprRenderer.color = Color.white;
        }
    }

}
