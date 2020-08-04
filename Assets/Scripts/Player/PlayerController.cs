using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;

    public float speed = 100.0f;
    public TextMeshProUGUI playerHealth;
    public GameObject GameOverWindow;

    private Rigidbody2D playerRB;
    private Vector3 moveDir;
    private bool isDashing = false;
    private int Phealth = 100;
    
    public PlayerController GetInstance()
    {
        return instance;
    }


    private void Awake()
    {
        GameOverWindow.SetActive(false);
        instance = this;
        playerRB = GetComponent<Rigidbody2D>();
        playerHealth = playerHealth.GetComponent<TextMeshProUGUI>();
        Time.timeScale = 1;
    }

    private void Update()
    {
        float moveX = 0.0f;
        float moveY = 0.0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveY = 1.0f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1.0f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            //body.transform.localScale = new Vector3(1, 1, 1);
            moveX = -1.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            //body.transform.localScale = new Vector3(-1, 1, 1);
            moveX = 1.0f;
        }

        if(Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.R))
        {
            Rewind();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDashing = true;
        }

        if (Phealth <= 0)
        {
            Cursor.visible = true;
            GameOverWindow.SetActive(true);
            Time.timeScale = 0;
        }
        playerRB.velocity = moveDir * speed * Time.deltaTime;
        moveDir = new Vector3(moveX, moveY, 0).normalized;
    }

    private void FixedUpdate()
    {
        float dashAmount = 2.0f;
        if(isDashing)
        {
            playerRB.MovePosition(transform.position + moveDir * dashAmount);
            isDashing = false;
        }
    }

    public void damage(int damage)
    {
        Phealth -= damage;
        playerHealth.text = Phealth.ToString();
        Debug.Log(Phealth);
    }

    private void Rewind()
    {
        return;
    }
}
