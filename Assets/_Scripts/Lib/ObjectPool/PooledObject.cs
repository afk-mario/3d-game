using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PooledGameObjectExtensions {
    public static void ReturnToPool (this GameObject gameObject) {

        var PooledObject = gameObject.GetComponent<PooledObject> ();

        if (PooledObject == null) {
            Debug.LogError ($"Cannot return {gameObject} to object pool because it was not created from one");
            return;
        }

        PooledObject.owner.ReturnObject (gameObject);
    }
}

public class PooledObject : MonoBehaviour {
    public ObjectPool owner;
}