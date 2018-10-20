using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBehaviour : MonoBehaviour {
    bool isAssign;
    SpriteRenderer spr;
    public Color assignColor;
    Color oldColor;
    Vector3 min, max;
    List<GameObject> assingnedSoldiers;
    public GameObject visionRange;


    List<GameObject> enemysInRange;

    void Start() {
        assingnedSoldiers = new List<GameObject>();
        enemysInRange = new List<GameObject>();
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
            assingnedSoldiers.RemoveRange(0, assingnedSoldiers.Count);
        }
    }

    public void AssignSoldier(int iterations, List<GameObject> soldiers) { //TODO peso dos soldados: cada grid tem X espaços, e soldados tem pesos diferentes. e.g: Cavalaria tem peso 3
        for(int i = 0; i < iterations; i++) { 
            foreach (GameObject soldier in soldiers) {
                if (soldier.GetComponent<SoldierBrain>() != null && !soldier.GetComponent<SoldierBrain>().assignedToarea) {
                    soldier.GetComponent<SoldierBrain>().AssignArea(max, min);
                    assingnedSoldiers.Add(soldier);
                    break;
                }
            }
        }
    }

    void AddEnemys(GameObject enemy) {
        if (assingnedSoldiers != null && enemy != null)
        {
            enemysInRange.Add(enemy);
            foreach (GameObject soldier in assingnedSoldiers)
            {
                if (enemy == null) {
                    break;
                }
                if (soldier.GetComponent<SoldierBrain>().enemyTarget == null) //TODO peso dos inimigos. E.g: Inimigo peso 1 = 1 soldado, inimigo peso 3 = os 3 soldados
                {
                    soldier.GetComponent<SoldierBrain>().GetEnemy(enemy);
                    break;
                }
            }
            if (enemy != null)
            {
                enemy.GetComponent<EnemyBehaviour>().cell.Add(this);
            }
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemysInRange.Remove(enemy);
        enemy.GetComponent<EnemyBehaviour>().cell = null;
        //if (enemy.GetComponent<EnemyBehaviour>().hp <= 0) {
          //  GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().SendMessage("AddGold", SendMessageOptions.DontRequireReceiver);
        //}
        /*foreach (GameObject soldier in assingnedSoldiers)
        {
            if (soldier.GetComponent<SoldierBrain>().enemyTarget != null && soldier.GetComponent<SoldierBrain>().enemyTarget == enemy)
            {
                soldier.GetComponent<SoldierBrain>().RemoveEnemy();
                if (enemysInRange.Count > 0) {
                    soldier.GetComponent<SoldierBrain>().GetEnemy(enemysInRange[enemysInRange.Count-1]);
                }
            }
        }*/
    }
}