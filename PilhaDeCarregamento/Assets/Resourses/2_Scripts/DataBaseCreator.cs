using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseCreator : MonoBehaviour
{
    [SerializeField] GameObject databasePrefab;
    private void Awake()
    {
        if(GameObject.FindGameObjectsWithTag("DataBase").Length < 1)
        {
            Instantiate(databasePrefab, Vector3.zero, Quaternion.identity);
        }
    }
}
