using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderControll : MonoBehaviour
{
    public GameObject player;
    public GameObject deathSmoke;
    public float viewDist = 20.0f;
    public float followingSpeed = 2.0f;
    public Animator animation;
    public int damage = 20;
    public float attackCooldownTime = 5.0f;
    public float attackRange = 0.45f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private float attacktimer = 0.0f;
    void Update()
    {
        if (attacktimer < 0.1f)
        {
            if(Vector2.Distance(player.transform.position, this.transform.position) < attackRange)
            {
                animation.SetBool("isWalking", false);
                player.GetComponent<CharacterControll>().health -= damage;
                attacktimer = attackCooldownTime;
            }
            else if (Vector2.Distance(player.transform.position, this.transform.position) < viewDist)
            {


                Vector3 targ = player.transform.position;
                targ.z = 0f;

                Vector3 objectPos = transform.position;
                targ.x = targ.x - objectPos.x;
                targ.y = targ.y - objectPos.y;

                float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

                RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position);

                if (hit.collider == null || hit.collider.tag == "Player" || hit.collider.tag == "Spider" || hit.collider.tag == "Floor")
                {

                    this.transform.Translate(-transform.right * followingSpeed * Time.fixedDeltaTime);
                    animation.SetBool("isWalking", true);

                }
                else
                {
                    animation.SetBool("isWalking", false);
                }
            }

        }
        else
        {
            attacktimer -= Time.fixedDeltaTime;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Arrow")
        {
            GameObject smoke = Instantiate(deathSmoke, this.transform.position, this.transform.rotation);
            Destroy(smoke, 0.5f);
            Destroy(col.gameObject);
            Destroy(this.gameObject);

        }
    }
}
