using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSys : MonoBehaviour {
    public GameManager gm;
    public GameObject enemyPrefab;

    int enemyNumbers;
    float timeInterval;

    void Start() {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine() {
        enemyNumbers = Random.Range(4, 15);
        timeInterval = Random.Range(0.5f, 4);
        for (int i = 0; i < enemyNumbers; i++) {
            Instantiate(enemyPrefab, transform.GetChild(Random.Range(0, transform.childCount - 1)).transform.position, transform.rotation);
            yield return new WaitForSeconds(timeInterval);
        }
        StartCoroutine(SpawnRoutine());
    }
}
