using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBomb : MonoBehaviour
{
    public GameObject[] connectedMines;
    public bool done = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void press()
    {
        this.GetComponent<Animator>().SetBool("Pressed", true);
        for (int i = 0; i < connectedMines.Length; i++)
        {
            if(connectedMines[i] != null)
                connectedMines[i].GetComponent<Mine>().noDamageExplode();
        }
        done = true;
    }
}
