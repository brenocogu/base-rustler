using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public GameObject chaozao, gridHolder, gridCellobj;
    List<GameObject> spawnedSoliders;
    public LayerMask ignoredLayer;
    int enemyCount;
    int waveNumber;
    public SpawnSys spawnSys;


    public Button nextWave;


    public GameObject soldierPrefab;


    bool gridIsVisible;
    // Use this for initialization
    void Start() {
        spawnedSoliders = new List<GameObject>();

        MountGrid();
        gridIsVisible = false;
        PoolParty poolParty = gameObject.GetComponent<PoolParty>();
        spawnedSoliders = poolParty.MakeListPool(soldierPrefab, 45, new Vector3(-59, 0.24f, 0));
        ActiveSoldiers(9);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            ToggleGrid();
            //ActiveSoldiers(3);
        }


#if UNITY_EDITOR
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
#endif
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



    public void ToggleGrid() {
        gridIsVisible = !gridIsVisible;
        for (int i = 0; i < gridHolder.transform.childCount; i++) {
            gridHolder.transform.GetChild(i).SendMessage("ToggleVisibility", SendMessageOptions.DontRequireReceiver);
        }
    }

    void MountGrid() {
        for (int x = 0; x < chaozao.GetComponent<Renderer>().bounds.size.x; x += 10) {
            for (int z = 0; z < chaozao.GetComponent<Renderer>().bounds.size.z; z += 10) {
                Instantiate(gridCellobj, new Vector3(x - 6, +12, z), gridCellobj.transform.rotation, gridHolder.transform);
            }
        }
        gridHolder.transform.position = new Vector3(chaozao.GetComponent<Renderer>().bounds.min.x, 1, chaozao.GetComponent<Renderer>().bounds.min.z);
    }

    public void ActiveSoldiers(int qtt) {
        foreach (GameObject soldier in spawnedSoliders)
        {
            if(qtt <= 0) { break; }
            if (!soldier.activeSelf) {
                soldier.SetActive(true);
                qtt--;
            }
        }
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

