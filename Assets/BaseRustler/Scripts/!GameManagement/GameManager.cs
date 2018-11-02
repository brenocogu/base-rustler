using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public GameObject chaozao, gridHolder, gridCellobj;
    List<GameObject> spawnedSoliders; 
    public float gold;
    public LayerMask ignoredLayer;
    int enemyCount;
    int waveNumber;
    public SpawnSys spawnSys;

    public Text goldTxt;

    public Button nextWave;


    public GameObject soldierPrefab;

    float goldTime, matTime, foodTime;
    int mattMax, foodMax;
    bool gridIsVisible;
    // Use this for initialization
    void Start () {
        spawnedSoliders = new List<GameObject>();

        goldTime = 2;
        matTime = 10;
        foodTime = 20;

        gold = 550;

        

        MountGrid();
        gridIsVisible = false;

        InstantiateSoldiers(9);
    }
	
	// Update is called once per frame
	void Update () {
        goldTxt.text = "" + gold;
        if (Input.GetKeyDown(KeyCode.Z)) {
            ToggleGrid();
        }


        //DEV MOUSE CONTROLLER
        if (Input.GetMouseButtonUp(0)) {
            RaycastHit hit = new RaycastHit();
            Ray clickRay = Camera.main.ScreenPointToRay((Vector2)Input.mousePosition);
            if (Physics.Raycast(clickRay, out hit, Mathf.Infinity, ignoredLayer))
            {
                if (hit.collider.gameObject.tag == "GridCell" && gridIsVisible)
                {
                    if (Input.GetKey(KeyCode.X))
                    {
                        hit.collider.gameObject.SendMessage("DesselectCell", SendMessageOptions.DontRequireReceiver);
                    }
                    else
                    {
                        hit.collider.gameObject.SendMessage("SelectCell", spawnedSoliders, SendMessageOptions.DontRequireReceiver);
                    }
                }
                if (hit.collider.gameObject.tag == "Castle")
                {
                    hit.collider.gameObject.SendMessage("SelectCastle", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
        //END DEV
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if ((touch.deltaTime < 0.2f && touch.phase == TouchPhase.Ended))
            { //Characterize an click in a mobile device OR for develop options, record the MouseButton0
                RaycastHit hit = new RaycastHit();
                Ray clickRay = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(clickRay, out hit, Mathf.Infinity, ignoredLayer))
                {
                    if (hit.collider.gameObject.tag == "GridCell" && gridIsVisible)
                    {
                        if (Input.GetKey(KeyCode.X))
                        {
                            hit.collider.gameObject.SendMessage("DesselectCell", SendMessageOptions.DontRequireReceiver);
                        }
                        else
                        {
                            hit.collider.gameObject.SendMessage("SelectCell", spawnedSoliders, SendMessageOptions.DontRequireReceiver);
                        }
                    }
                    if (hit.collider.gameObject.tag == "Castle")
                    {
                        hit.collider.gameObject.SendMessage("SelectCastle", SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
            else if ((touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary))
            {//Drag Movement
                
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

    public void InstantiateSoldiers(int qtt) {
        if (gold > 400)
        {
            gold -= 400;
            for (int i = 0; i < qtt; i++)
            {
                spawnedSoliders.Add(Instantiate(soldierPrefab, new Vector3(-29.9f, 0, 0), transform.rotation));
            }
        }
    }

    void AddGold() {
        gold += 250;
    }

    public void IncreaseEnemyCount() {
        enemyCount++;
        if (nextWave.gameObject.activeSelf) {
            nextWave.gameObject.SetActive(false);
        }
    }

    public void DecreaseEnemyCount(){
        enemyCount--;
        if (enemyCount == 0) {
            nextWave.gameObject.SetActive(true);
        }
    }

    public void CallWave() {
        StartCoroutine(spawnSys.SpawnRoutine(waveNumber + 3));
        waveNumber++;
    }
}

