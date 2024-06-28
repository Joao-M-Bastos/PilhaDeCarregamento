using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame3
    void Update()
    {
        Time.timeScale = 10;

        if (Input.GetKeyDown(KeyCode.D))
        {
            Time.timeScale += 1;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Time.timeScale -= 1;
        }
    }
}
