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
    float touchDelta;

    public GameObject nextWave;


    public GameObject soldierPrefab;

    int lastClicked;

    public Image remmoval, openn;


    bool gridIsVisible, isRemoval;
    // Use this for initialization
    void Start() {
        lastClicked = 0;
        spawnedSoliders = new List<GameObject>();

        MountGrid();
        gridIsVisible = false;
        PoolParty poolParty = gameObject.GetComponent<PoolParty>();
        spawnedSoliders = poolParty.MakeListPool(soldierPrefab, 45, new Vector3(-59, 0.24f, 0));
        ActiveSoldiers(9);
    }

    // Update is called once per frame
    void Update() {

#if UNITY_EDITOR
        if (gridIsVisible)
        {
            if (Input.GetMouseButtonUp(0) && !Input.GetKey(KeyCode.LeftControl))
            {
                RaycastHit hit = new RaycastHit();
                Ray clickRay = Camera.main.ScreenPointToRay((Vector2)Input.mousePosition);
                if (Physics.Raycast(clickRay, out hit, Mathf.Infinity, ignoredLayer))
                {
                    if (hit.collider.gameObject.tag == "EditorOnly") {
                        Debug.Log("AAAAA");
                    }
                    if (hit.collider.gameObject.tag == "GridCell")
                    {
                        if (isRemoval)
                        {
                            hit.collider.gameObject.SendMessage("DesselectCell", SendMessageOptions.DontRequireReceiver);
                        }
                        else
                        {
                            hit.collider.gameObject.SendMessage("SelectCell", spawnedSoliders, SendMessageOptions.DontRequireReceiver);
                        }
                    }
                }
            }
        }
#endif
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                touchDelta = Time.time;
            }
            if (touch.phase == TouchPhase.Ended)
            { //Characterize an click in a mobile device OR for develop options, record the MouseButton0
                touchDelta = Time.time - touchDelta;
                if (touchDelta < 0.2f)
                {
                    RaycastHit hit = new RaycastHit();
                    Ray clickRay = Camera.main.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(clickRay, out hit, Mathf.Infinity, ignoredLayer))
                    {
                        if (hit.collider.gameObject.tag == "GridCell" && gridIsVisible)
                        {
                            if (isRemoval)
                            {
                                hit.collider.gameObject.SendMessage("DesselectCell", SendMessageOptions.DontRequireReceiver);
                            }
                            else
                            {
                                hit.collider.gameObject.SendMessage("SelectCell", spawnedSoliders, SendMessageOptions.DontRequireReceiver);
                            }
                        }
                    }
                }
            }
        }
    }

    public void ToggleGrid(int clickType) {
        if (lastClicked != clickType)
        {
            isRemoval = (clickType == 1) ? false : (clickType == 2) ? true : false;
            if (!gridIsVisible) {
                for (int i = 0; i < gridHolder.transform.childCount; i++)
                {
                    gridHolder.transform.GetChild(i).SendMessage("ToggleVisibility", SendMessageOptions.DontRequireReceiver);
                }
            }
            switch (clickType) {
                case 1:
                    openn.color = new Color(0,0,0);
                    remmoval.color = new Color(1, 1, 1);
                    break;
                case 2:
                    remmoval.color = new Color(0, 0, 0);
                    openn.color = new Color(1, 1, 1);
                    break;
            }
            lastClicked = clickType;
            StartCoroutine(WaitClick());
        }
        else {
            gridIsVisible = false;
            for (int i = 0; i < gridHolder.transform.childCount; i++)
            {
                gridHolder.transform.GetChild(i).SendMessage("ToggleVisibility", SendMessageOptions.DontRequireReceiver);
            }
            lastClicked = 0;
            remmoval.color = new Color(1, 1, 1);
            openn.color = new Color(1, 1, 1);
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
        if (nextWave.activeSelf) {
            nextWave.SetActive(false);
        }
    }

    public void DecreaseEnemyCount(){
        enemyCount--;
        if (enemyCount == 0 && !nextWave.activeSelf) {
            nextWave.SetActive(true);
        }
    }

    public void CallWave() {
        StartCoroutine(spawnSys.SpawnRoutine(waveNumber + 3));
        waveNumber++;
    }

    IEnumerator WaitClick() {
        //shit happens and I don't know what to do to fix this one. When you click on a GUI button, it select an cell that's behind it. 
        //My solution: put an enumerator to handle a time between click limits. I'm felling bad with what I've done.
        gridIsVisible = false;
        yield return new WaitForSeconds(0.1f);
        gridIsVisible = true;
    }
}

