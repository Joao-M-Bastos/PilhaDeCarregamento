using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TremGenerator : MonoBehaviour
{
    [SerializeField] GameObject trem;
    [SerializeField] float trainTimeInterval;

    float currentCooldown;
    // Start is called before the first frame update
    void Start()
    {
        float[] values = DataBase.GetInstance().GetManegerValues();

        trainTimeInterval = values[3];
    }

    // Update is called once per frame
    void Update()
    {
        currentCooldown -= Time.deltaTime;
        if(currentCooldown < 0)
        {
            Instantiate(trem, transform.position, transform.rotation);
            currentCooldown = trainTimeInterval;
        }
    }
}
