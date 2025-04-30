using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;

    public float speed = 5f;
    public float sprintSpeed =  2f;
    Rigidbody rb;

    public Transform cam;

    int health;

    bool isInvincible = false;
    bool sprint = false;

    public AudioClip potionCollisionSound;
    public AudioClip coinCollisionSound;
    public AudioClip bombCollisionSound;

    private void Awake()
    {
        //Assigns rigidbody
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        health = 100;

        //Sets up UI
        GameManager.instance.UpdateData(health);
    }


    private void FixedUpdate()
    {
        //Get input from player
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        //Setting camera direction
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        //Freeze the y axis
        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;

        //Create camera relative direction with inputs
        Vector3 forwardRelative = verticalInput * camForward;
        Vector3 rightRelative = horizontalInput * camRight;

        Vector3 moveDirection = forwardRelative + rightRelative;

        //Move character
        rb.linearVelocity = new Vector3(moveDirection.x, 0, moveDirection.z) * speed;

    }

    private void OnCollisionEnter(Collision col)
    {
        //Bomb Collision
        if(col.gameObject.tag == "Bomb")
        {
            //Play bomb sound
            GameObject.FindFirstObjectByType<AudioController>().PlaySound(bombCollisionSound);
            
            Destroy(col.gameObject);

            if (isInvincible == false)
            {
                health -= 10;
                GameManager.instance.UpdateData(health);

                if(health <= 0)
                {
                    GameManager.instance.HandleGameOver();
                }
            }
        }

        //Coin Collision
        if(col.gameObject.tag == "Coin")
        {
            Destroy(col.gameObject);

            //play coin sound
            GameObject.FindFirstObjectByType<AudioController>().PlaySound(coinCollisionSound);

            GameManager.instance.coins += 1;
            GameManager.instance.UpdateData(health);
            GameManager.instance.CheckLevelWin();
        }

        //Red bottle collision
        if(col.gameObject.tag == "RedBottle")
        {
            Destroy(col.gameObject);

            GameObject.FindFirstObjectByType<AudioController>().PlaySound(potionCollisionSound);

            GameManager.instance.potions[0]++;
            GameManager.instance.UpdateData(health);
        }

        //Green bottle collision
        if (col.gameObject.tag == "GreenBottle")
        {
            Destroy(col.gameObject);

            GameObject.FindFirstObjectByType<AudioController>().PlaySound(potionCollisionSound);

            GameManager.instance.potions[1]++;
            GameManager.instance.UpdateData(health);
        }

        //Blue bottle collision
        if (col.gameObject.tag == "BlueBottle")
        {
            Destroy(col.gameObject);

            GameObject.FindFirstObjectByType<AudioController>().PlaySound(potionCollisionSound);

            GameManager.instance.potions[2]++;
            GameManager.instance.UpdateData(health);
        }
    }
    private void Update()
    {
        //Red Potion Usage
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(isInvincible == false && GameManager.instance.potions[0] > 0)
            {
                GameManager.instance.potions[0]--;

                isInvincible = true;
                Debug.Log("Invinsible On");

                //Updates UI
                GameManager.instance.UpdateData(health);

                Invoke("EndInvincibility", 10f);
            }
        }

        //Green Potion Usage
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (GameManager.instance.potions[1] > 0)
            {
                GameManager.instance.potions[1]--;

                if(health <= 90 && health > 0)
                {
                    health += 10;
                }
                if(health > 90 && health < 100)
                {
                    int healthRemainder = 100 - health;
                    health += healthRemainder;
                }

                //Updates UI
                GameManager.instance.UpdateData(health);
            }
        }

        //Blue Potion Usage
        if (Input.GetKeyDown(KeyCode.B))
        {

            if (GameManager.instance.potions[2] > 0 && sprint == false)
            {
                GameManager.instance.potions[2]--;

                //Increases speed
                speed *= sprintSpeed;
                sprint = true; 

                GameManager.instance.UpdateData(health);
                Invoke("EndSprint", 8f);
            }
        }
    }

    void EndInvincibility()
    {
        isInvincible = false;
        Debug.Log("Invinsible Off");
    }

    void EndSprint()
    {
        speed /= sprintSpeed;
        sprint = false;
    }
}
