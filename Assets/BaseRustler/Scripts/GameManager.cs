using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public GameObject chaozao, gridHolder, gridCellobj;
    List<GameObject> spawnedSoliders; //TODO deixar private e o sistema ira executar o Assign;
    public float gold, matterials, food;
    int goldLvl, matLvl, foodLvl;
    public LayerMask ignoredLayer;

    public Text goldTxt, soldierTXT, costText;


    public GameObject soldierPrefab;

    float goldTime, matTime, foodTime;
    float goldCost, matCost, foodCost;
    int mattMax, foodMax;
    bool gridIsVisible;
    // Use this for initialization
    void Start () {
        spawnedSoliders = new List<GameObject>();
        //DontDestroyOnLoad(gameObject);
        //if (GameObject.FindGameObjectsWithTag("GameController").Length > 1) {
        //    Destroy(GameObject.FindGameObjectsWithTag("GameController")[1]);
       // }
        goldLvl = foodLvl = matLvl = 1;
        goldTime = 2;
        matTime = 10;
        foodTime = 20;

        gold = 550;
        matterials = 50;
        food = 200;

        mattMax = 75 + 50 * matLvl;
        foodMax = 100 + 50 * foodLvl;

        StartCoroutine(GoldGain());
        StartCoroutine(FoodGain());
        StartCoroutine(MatterialGain());

        //Initialize the Levels
        UpgradeAttribute("gold");
        UpgradeAttribute("matterials");
        UpgradeAttribute("food");
        MountGrid();
        gridIsVisible = false;

        InstantiateSoldiers(9);
    }
	
	// Update is called once per frame
	void Update () {
        goldTxt.text = "" + gold;
        soldierTXT.text = "" + spawnedSoliders.Count;
		costText.text = "" + (400 + _goldFormula(spawnedSoliders.Count));
        if (Input.GetKeyDown(KeyCode.Z)) {
            ToggleGrid();
        }

        if (Input.GetKeyDown(KeyCode.L)) {
            UpgradeAttribute("gold");
        }

        if (Input.touchCount == 1 || Input.GetMouseButton(0)) {
            RaycastHit hit = new RaycastHit();
            Ray clickRay = Camera.main.ScreenPointToRay( (Input.touchCount > 0) ? Input.GetTouch(0).position : (Vector2)Input.mousePosition);
            if (Physics.Raycast(clickRay, out hit,Mathf.Infinity, ignoredLayer)) {
                if (hit.collider.gameObject.tag == "GridCell" && gridIsVisible) {
                    if (Input.GetKey(KeyCode.X))
                    {
                        hit.collider.gameObject.SendMessage("DesselectCell", SendMessageOptions.DontRequireReceiver);
                    }
                    else {
                        hit.collider.gameObject.SendMessage("SelectCell", spawnedSoliders, SendMessageOptions.DontRequireReceiver);
                    }
                    }
                if (hit.collider.gameObject.tag == "Castle") {
                    hit.collider.gameObject.SendMessage("SelectCastle", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
	}

    

    void ToggleGrid() {
        gridIsVisible = !gridIsVisible;
        for (int i = 0; i < gridHolder.transform.childCount; i++) {
            gridHolder.transform.GetChild(i).SendMessage("ToggleVisibility", SendMessageOptions.DontRequireReceiver);
        }
    }

    void MountGrid() {
        for (int x = 0; x < chaozao.GetComponent<Renderer>().bounds.size.x; x+=10) {
            for(int z = 0; z < chaozao.GetComponent<Renderer>().bounds.size.z; z+=10){
                Instantiate(gridCellobj, new Vector3(x - 6, +12,z), gridCellobj.transform.rotation, gridHolder.transform);
            }
        }
        gridHolder.transform.position = new Vector3(chaozao.GetComponent<Renderer>().bounds.min.x, 1, chaozao.GetComponent<Renderer>().bounds.min.z);
    }

    public float ReturnAttribute(string attributeName)
    {
        switch (attributeName)
        {
            case "gold":
                return gold;
            case "matterials":
                return matterials;
            case "food":
                return food;
            default:
                return 0;
        }
    }

    public void UpgradeAttribute(string attributeName)
    {
        switch (attributeName)
        {
            case "gold":
                if(gold >= goldCost) {
                    gold -= goldCost;
                    goldLvl++;
                    goldCost = 120 * _goldFormula(goldLvl) * goldLvl;
                }
                break;
            case "matterials":
                if (gold >= matCost) {
                    gold -= matCost;
                    matLvl++;
                    matCost = 180 * _goldFormula(goldLvl) * matLvl;
                }
                break;
            case "food":
                if (gold >= foodCost) {
                    gold -= foodCost;
                    foodLvl++;
                    foodCost = 100 * _goldFormula(goldLvl) * foodLvl;
                }
                break;
            default:
                break;
        }
    }
    
    //EQUAÇÃO PARA GANHO DE GOLD POR TEMPO BASEADO NO NÍVEL => y = (11.75988f + (0.002387479f - 11.75988f)/(1 + (x/3.315969f)^1.186491f)) * VALOR DE GAIN ;
    //EQUAÇÃO PARA UPGRADE COST DE GOLD BASEADO NO NÍVEL => y = (120 * p.s.) * gLvl;
    IEnumerator GoldGain() {
        yield return new WaitForSeconds(goldTime);
        gold += _goldFormula(goldLvl);
        StartCoroutine(GoldGain());
    }

    IEnumerator FoodGain() {
        yield return new WaitForSeconds(foodTime);
        if (food < foodMax) {
            food += 1 /*TODO SUBSTITUIR PELO NÚMERO DE FARMS*/ * 3;
        }
        StartCoroutine(FoodGain());
    }

    IEnumerator MatterialGain() {
        yield return new WaitForSeconds(matTime);
        if (matterials < mattMax) {
            matterials += matLvl;
        }
        StartCoroutine(MatterialGain());
    }


    static float _goldFormula(float lvl) {
        return Mathf.Floor((11.75988f + (0.002387479f - 11.75988f) / (1 + lvl)) * 1.5f);
    }

    public void InstantiateSoldiers(int qtt) {
        if (gold > 400 + _goldFormula(spawnedSoliders.Count))
        {
            gold -= 400 + _goldFormula(spawnedSoliders.Count);
            for (int i = 0; i < qtt; i++)
            {
                spawnedSoliders.Add(Instantiate(soldierPrefab, new Vector3(-29.9f, 0, 0), transform.rotation));
            }
        }
    }

    void AddGold() {
        gold += 250;
    }
}

