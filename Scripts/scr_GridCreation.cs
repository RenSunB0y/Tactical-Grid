using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_GridCreation : MonoBehaviour
{
    public GameObject tile;

    public GameObject actualTile;
    public GameObject tileButGauche;
    public GameObject tileButMilieu;
    public GameObject tileButDroit;


    public scr_TileBehaviour tileScript;

    [SerializeField] public int colonnesMax;
    [SerializeField] public int lignesMax;

    public float x;
    public float y;
    public float baseX;
    public float baseY;

    public GameObject[,] grid;

    // Start is called before the first frame update
    void Start()
    {
        grid = new GameObject[colonnesMax, lignesMax];
        x = -1.772f;
        y = 2.872f;

        CreationDeLaGrille();

        scr_TileBehaviour.gridCreated = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreationDeLaGrille()
    {
        for (int colonnes = 0; colonnes < colonnesMax; colonnes ++)
        {
            baseX = x;
            baseY = y;
            for (int lignes = 0; lignes < lignesMax; lignes ++)
            {   
                if (lignes == 0 && colonnes > 0)
                {
                    x = baseX;
                    y = baseY;
                }

                actualTile = Instantiate(tile, new Vector3(x, y, 0), Quaternion.identity);
                actualTile.transform.parent = this.transform;
                actualTile.name = "[" + colonnes.ToString() + ";" + lignes.ToString() + "]";
                actualTile.tag = "tile";

                grid[colonnes, lignes] = actualTile;
                
                tileScript = actualTile.GetComponent<scr_TileBehaviour>();
                tileScript.gridScript = this;
                tileScript.x = colonnes;
                tileScript.y = lignes;

                x -= .3f * 1.2f;
                y -= .52f * 1.2f;
                
                if (lignes == 8)
                {
                    x = baseX;
                    y = baseY;
                }
            }
            x += .75f *1.2f;
            y += -.2f *1.2f;
        }
    }
}