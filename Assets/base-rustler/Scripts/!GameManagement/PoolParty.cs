using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolParty : MonoBehaviour {

    public List<GameObject> MakeListPool(GameObject obj, int qtt, Vector3 position) {
        List<GameObject> inPool = new List<GameObject>();
        for (int i = qtt; i > 0; i--) {
            //improved for Loop
            GameObject objeto = (GameObject) Instantiate(obj, position, obj.transform.rotation);
            objeto.SetActive(false);
            inPool.Add(objeto);
        }
        return inPool;
    }
}
