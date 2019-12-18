using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{

    public bool run = false;
    public GameObject explosion;
    public string entity;
    public bool explodeOnHuman = false;
    public float explodTime = 0.5f;
    public int damage = 20;
    public float pushPower = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private bool done = false;
    void Update()
    {
        if (explodeOnHuman == false || entity == "Player") {
            if (run && !done)
            {
                explode();
            }
        }
    }
    public void explode()
    {
        done = true;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<CharacterControll>().health -= damage;
        GameObject exp = Instantiate(explosion, this.transform.position, explosion.transform.rotation);
        Destroy(exp, explodTime);
        Destroy(this.gameObject, explodTime);
    }
    public void noDamageExplode()
    {
        done = true;
        GameObject exp = Instantiate(explosion, this.transform.position, explosion.transform.rotation);
        Destroy(exp, explodTime);
        Destroy(this.gameObject, explodTime);
    }
}
