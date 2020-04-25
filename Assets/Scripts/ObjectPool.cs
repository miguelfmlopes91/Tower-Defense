using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objectsPrefabs;


    public GameObject GetObject(string type)
    {
        for (int i = 0; i < objectsPrefabs.Length; i++)
        {
            if (objectsPrefabs[i].name == type)
            {
                GameObject newObject = Instantiate(objectsPrefabs[i]);
                newObject.name = type;
                return newObject;
            }
        }

        return null;
    }

}
