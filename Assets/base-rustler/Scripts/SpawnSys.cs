using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSys : MonoBehaviour {
    public Transform[] transformPoints;
    public GameObject enemyPrefab;
    List<GameObject> spawnedEnemys;
    PoolParty pool;

    void Start() {
        pool = GetComponent<PoolParty>();
        spawnedEnemys = pool.MakeListPool(enemyPrefab, 430, Vector3.zero);
    }

    public IEnumerator SpawnRoutine(int iterationCount) {
        for (int i = iterationCount; i > 0; i--) { //improved for loop
            int spawnChance = Random.Range(0, 11);
            switch(spawnChance) {
                default:
                    StartCoroutine(ActiveEnemy(1, transformPoints[Random.Range(0, transformPoints.Length)].position));
                    yield return new WaitForSeconds(Random.Range(0.25f, 5.5f));
                    continue;
                case 9:
                    StartCoroutine(ActiveEnemy(2, transformPoints[Random.Range(0, transformPoints.Length)].position));
                    yield return new WaitForSeconds(Random.Range(0.25f, 5.5f));
                    continue;
                case 10:
                    StartCoroutine(ActiveEnemy(4, transformPoints[Random.Range(0, transformPoints.Length)].position));
                    yield return new WaitForSeconds(Random.Range(0.25f, 5.5f));
                    continue;
            }
        }
    }

    public IEnumerator ActiveEnemy(int qtt, Vector3 positions)
    {
        foreach (GameObject soldier in spawnedEnemys)
        {
            if (qtt <= 0) { break; }
            if (!soldier.activeSelf)
            {
                soldier.transform.position = positions;
                soldier.SetActive(true);
                qtt--;
                yield return new WaitForSeconds(0.25f);
            }
        }
    }

}
