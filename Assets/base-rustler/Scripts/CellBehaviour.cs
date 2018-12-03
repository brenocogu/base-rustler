using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBehaviour : MonoBehaviour {
    bool isAssign;
    SpriteRenderer spr;
    public Color assignColor;
    Color oldColor;
    Vector3 min, max;
    LinkedList<GameObject> assingnedSoldiers;
    public GameObject visionRange;


    LinkedList<GameObject> enemysInRange;

    void Start() {
        assingnedSoldiers = new LinkedList<GameObject>();
        enemysInRange = new LinkedList<GameObject>();
        spr = GetComponent<SpriteRenderer>();
        oldColor = spr.color;
        min = gameObject.GetComponent<Renderer>().bounds.min;
        max = gameObject.GetComponent<Renderer>().bounds.max;
        visionRange.SetActive(false);
        ToggleVisibility();
    }

    void ToggleVisibility() {
        spr.enabled = !spr.enabled;
        if (isAssign)
        {
            if (spr.enabled)
            {
                visionRange.GetComponent<Renderer>().enabled = true;
            }
            else
            {
                visionRange.GetComponent<Renderer>().enabled = false;
            }
        }
    }

    void SelectCell(List<GameObject> soldiers){
        if (!isAssign){
            AssignSoldier(3, soldiers);
            if (assingnedSoldiers.Count > 0)
            {
                isAssign = true;
                spr.color = assignColor;
                visionRange.SetActive(true);
            }
        }
    }

    void DesselectCell() {
        if (isAssign) {
            isAssign = false;
            spr.color = oldColor;
            visionRange.SetActive(false);
            foreach (GameObject soldier in assingnedSoldiers) {
                soldier.GetComponent<SoldierBrain>().UnassignSoldier();
            }
            assingnedSoldiers.Clear();
        }
    }

    public void AssignSoldier(int iterations, List<GameObject> soldiers) { //TODO peso dos soldados: cada grid tem X espaços, e soldados tem pesos diferentes. e.g: Cavalaria tem peso 3
        for(int i = 0; i < iterations; i++) { 
            foreach (GameObject soldier in soldiers) {
                if (soldier.GetComponent<SoldierBrain>() != null && !soldier.GetComponent<SoldierBrain>().assignedToarea && soldier.activeSelf) {
                    soldier.GetComponent<SoldierBrain>().AssignArea(max, min);
                    soldier.GetComponent<SoldierBrain>().cellHead = gameObject.GetComponent<CellBehaviour>();
                    assingnedSoldiers.AddFirst(soldier);
                    break;
                }
            }
        }
    }

    void AddEnemys(GameObject enemy) {
        if (assingnedSoldiers != null && enemy != null)
        {
            enemysInRange.AddLast(enemy);
            if (enemy != null)
            {
                //enemy.GetComponent<EnemyBehaviour>().cell.Add(this);
            }
            AssignEnemy();
            

        }
    }

    public void SetFree() {
        AssignEnemy();
    }

    public void AssignEnemy() {
        //GameObject[] enemyREF = enemysInRange.ToArray();
        if (enemysInRange.Count >= 1) {
            foreach (GameObject soldier in assingnedSoldiers)
            {
                if (enemysInRange.Count < 1)
                {
                    break;
                }
                if (soldier.GetComponent<SoldierBrain>().enemyTarget == null) //TODO peso dos inimigos. E.g: Inimigo peso 1 = 1 soldado, inimigo peso 3 = os 3 soldados
                {
                    if (enemysInRange.Count < 1)
                    {
                        break;
                    }
                    if (enemysInRange.Last.Value.activeSelf)
                    {
                        soldier.GetComponent<SoldierBrain>().GetEnemy(enemysInRange.Last.Value);
                    }
                    break;
                }
                if (enemysInRange.Count < 1)
                {
                    break;
                }
            }
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemysInRange.Remove(enemy);
        enemy.GetComponent<EnemyBehaviour>().cell = null;
    }

    public void GetAssign(List<GameObject> games) {
        if (isAssign)
        {
            DesselectCell();
        }
        else {
            SelectCell(games);
        }
    }
}