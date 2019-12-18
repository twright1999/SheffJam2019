using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject brokenArrow;
    public float maxOfsetArrow = 1.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Arrow")
        {
            if(Mathf.Abs(col.transform.rotation.z) < 0.2f)
            {
                Vector3 initPos =new Vector3(col.transform.position.x, col.transform.position.y + Random.Range(0.2f, maxOfsetArrow),0) ;
                Instantiate(brokenArrow, initPos, col.transform.rotation);

            }
            Destroy(col.gameObject);
        }
    }
}
