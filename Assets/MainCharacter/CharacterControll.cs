using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CharacterControll : MonoBehaviour
{
    public Animator animator;
    public Animation shootright;
    public float movementSpeed = 3.5f;
    public float shootCastTime = 1.0f;
    public float shootCoolDown = 2.5f;
    public Rigidbody2D arrow;
    public float arrowSpeed = 500.0f;
    public int coins = 0;
    public int health = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    private float isShootingTime = 0.0f;
    private float shootCooldownTime = 0.0f;
    private int lastDir = 0;
    private bool lastMoveState = false;

    private bool hasBlueKey = false;
    private bool hasYellowKey = false;
    private bool hasRedKey = false;
    private bool hasGreenKey = false;

    void FixedUpdate()
    {
        if(health <= 0)
        {
            SceneManager.LoadScene("Menu");
        }
        if (isShootingTime < 0.01f)
        {
            animator.SetBool("shoot", false);
            DirectionalMovement();
        }
        else
        {
            isShootingTime -= Time.fixedDeltaTime;
        }
        if (shootCooldownTime < 0.01f )
        {
            if (Input.GetKeyDown("f"))
            {
                isShootingTime = shootCastTime;
                shootCooldownTime = shootCoolDown;
                animator.SetBool("shoot", true);
                Vector3 arrowRotation = new Vector3(0, 0,0);
                switch(lastDir){
                    case 0: arrowRotation = new Vector3(0, 0, 0); break;
                    case 1: arrowRotation = new Vector3(0, 0, 90); break;
                    case 2: arrowRotation = new Vector3(0, 0, 180); break;
                    case 3: arrowRotation = new Vector3(0, 0, -90); break;
                }
                GameObject newArrow = Instantiate(arrow.gameObject, this.transform.position, Quaternion.Euler(arrowRotation));
                print(arrowRotation);
                newArrow.GetComponent<Rigidbody2D>().AddForce(newArrow.transform.up* arrowSpeed );

            }
        }
        else
        {
            shootCooldownTime -= Time.deltaTime;
        }


    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Coin")
        {
            coins++;
            Destroy(col.gameObject);
        }
        if(col.tag == "Prob")
        {
            col.GetComponent<ProbControll>().run = true;
        }
        if(col.tag == "Mine")
        {
            col.GetComponent<Mine>().run = true;
            col.GetComponent<Mine>().entity = "Player";
        }
        if(col.tag == "Key")
        {
            switch (col.name)
            {
                case "GreenKey":this.hasGreenKey = true;break;
                case "BlueKey": this.hasBlueKey = true; break;
                case "RedKey": this.hasRedKey = true; break;
                case "YellowKey": this.hasYellowKey = true; break;
            }
            Destroy(col.gameObject);
        }
        if (col.tag == "Door")
        {
            bool hasKey = false;
            switch (col.name)
            {
                case "BlueDoor": hasKey = this.hasBlueKey; break;
                case "GreenDoor": hasKey = this.hasGreenKey; break;
                case "RedDoor": hasKey = this.hasRedKey; break;
                case "YellowDoor": hasKey = this.hasYellowKey; break;
            }
            if(hasKey)
                Destroy(col.gameObject);
        }
        if (col.tag == "BombKey")
        {
            if(!col.GetComponent<SwitchBomb>().done)
                col.GetComponent<SwitchBomb>().press();
        }
        if (col.tag == "Chest")
        {
            if (hasYellowKey)
                col.GetComponent<ChestControll>().Open();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {

        if (col.tag == "Prob")
        {
            col.GetComponent<ProbControll>().run = false;
        }
        if (col.tag == "Mine")
        {
            col.GetComponent<Mine>().run = false;
            col.GetComponent<Mine>().entity = "";
        }
    }

    public void DirectionalMovement()
    {
        bool[] dirs = { Input.GetKey("w"), Input.GetKey("a"), Input.GetKey("s"), Input.GetKey("d") };

        Vector2 moveVec = new Vector2(0, 0);
        bool isMoving = false;
        for(int i = 0; i < dirs.Length; i++)
        {
            if (dirs[i])
            {
                if(lastMoveState == false || (lastDir == i && lastMoveState==true))
                {
                    switch (i)
                    {
                        case 0: moveVec = new Vector2(0, 1);break;
                        case 1: moveVec = new Vector2(-1, 0); break;
                        case 2: moveVec = new Vector2(0, -1); break;
                        case 3: moveVec = new Vector2(1, 0); break;
                    }
                    lastDir = i;
                    isMoving = true;
                    break;
                }
            }
        }
        if (!isMoving)
        {
            lastMoveState = false;
            animator.SetInteger("moveDirection", lastDir);
            animator.SetBool("isMoving", false);
            return;
        }
        if (animator.GetInteger("moveDirection") != lastDir)
        {
            lastMoveState = false;
            animator.SetBool("isMoving", false);
            animator.SetInteger("moveDirection", lastDir);
            return;
        }
        lastMoveState = true;
        this.transform.Translate(moveVec * movementSpeed * Time.deltaTime);
        animator.SetBool("isMoving", true);


    }
}
