using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objectsPrefabs;


    private List<GameObject> pooledObjects = new List<GameObject>();


    public GameObject GetObject(string type)
    {
        //first fetch for a obj in the pool
        foreach (GameObject obj in pooledObjects)
        {
            if (obj.name == type && !obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        //if there's no new object we create a new one
        for (int i = 0; i < objectsPrefabs.Length; i++)
        {
            if (objectsPrefabs[i].name == type)
            {
                GameObject newObject = Instantiate(objectsPrefabs[i]);
                pooledObjects.Add(newObject);
                newObject.name = type;
                return newObject;
            }
        }

        return null;
    }

    public void ReleaseObject(GameObject gameObject)
    {

        gameObject.SetActive(false);
    }

}
