using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


public class DropObjectOnDestroy : MonoBehaviour {
    public List<GameObject> obj;


    void OnDestroy() {
        for (int i = 0; i < obj.Count; i++) {
            var tmpO = Instantiate(obj[i]);
        }
    }

}
