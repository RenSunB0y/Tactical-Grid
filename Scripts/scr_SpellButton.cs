using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_SpellButton : MonoBehaviour
{
    public bool isSurvoled;
    public bool isClicked;
    public bool spellOnHand;
    public bool sortActive;

    public GameObject spellHolded;
    public GameObject spellInstantiated;

    public scr_SpellManager spellManagerScript;
    public scr_PointerBehaviour pointerScript;

    public int tileX;
    public int tileY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isSurvoled)
        {
            isClicked = true;
        }

        if(Input.GetMouseButtonUp(0))
        {
            if (isClicked && !spellOnHand)
            {
                spellInstantiated = Instantiate(spellHolded, transform.transform.position, Quaternion.identity);
                spellOnHand = true;
                isClicked = false;
            }

            else if (isClicked)
            {
                isClicked = false;
            }
        }

        if (Input.GetMouseButtonDown(1) && spellOnHand)
        {
            Destroy(spellInstantiated);
            spellOnHand = false;

            if (sortActive)
            { UnActivate(); }
        }

        if (Input.GetMouseButtonDown(0) && sortActive)
        {
            if (pointerScript.horizontal)
            {pointerScript.isH = true;}
            else
            { pointerScript.isH = false; }

            Debug.Log("sort activé");
            UnActivate();
            spellOnHand = false;
            Destroy(spellInstantiated);
            SpellBehaviour();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("mouse"))
        {
            isSurvoled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "mouse")
        {
            isSurvoled = false;
        }
    }

    public void Activate(int activateOnX, int activateOnY)
    {
        tileX = activateOnX;
        tileY = activateOnY;
        sortActive = true;
    }

    public void UnActivate()
    {
        sortActive = false;
    }

    public void SpellBehaviour()
    {
        spellManagerScript.ActivateRightSpell(tileX, tileY);
        spellManagerScript.SelectSpell();
    }
}
