using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSys : MonoBehaviour {
    public Transform[] transformPoints;
    public GameObject enemyPrefab;

    public IEnumerator SpawnRoutine(int iterationCount) {
        for (int i = iterationCount; i > 0; i--) { //improved for loop
            int spawnChance = Random.Range(0, 11);
            switch(spawnChance) {
                default:
                    Instantiate(enemyPrefab, transformPoints[Random.Range(0, transformPoints.Length)], true);
                    yield return new WaitForSeconds(Random.Range(0.25f, 5.5f));
                    continue;
                case 9:
                    Instantiate(enemyPrefab, transformPoints[Random.Range(0, transformPoints.Length)], true);
                    Instantiate(enemyPrefab, transformPoints[Random.Range(0, transformPoints.Length)], true);
                    yield return new WaitForSeconds(Random.Range(0.25f, 5.5f));
                    continue;
                case 10:
                    Instantiate(enemyPrefab, transformPoints[Random.Range(0, transformPoints.Length)], true);
                    Instantiate(enemyPrefab, transformPoints[Random.Range(0, transformPoints.Length)], true);
                    Instantiate(enemyPrefab, transformPoints[Random.Range(0, transformPoints.Length)], true);
                    Instantiate(enemyPrefab, transformPoints[Random.Range(0, transformPoints.Length)], true);
                    yield return new WaitForSeconds(Random.Range(0.25f, 5.5f));
                    continue;
            }
        }
    }
}
