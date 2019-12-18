using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbControll : MonoBehaviour
{
    public Animator probanimator;
    public string paramName;

    public bool run = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private bool needReset = false;
    // Update is called once per frame
    void Update()
    {
        if (run)
        {
            probanimator.SetBool("Paniking", true);
            needReset = true;
        }else if (needReset)
        {
            probanimator.SetBool(paramName, false);
            needReset = false;
        }
    }

}
