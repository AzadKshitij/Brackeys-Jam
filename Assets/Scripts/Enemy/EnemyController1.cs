using UnityEngine;

public class EnemyController1 : MonoBehaviour
{
    private static EnemyController1 instance;

    public float speed;
    public float stoppingDistance;
    public float retreteDistance;

    [SerializeField] private Animator animator;

    public Transform player;


    //Health
    public int healt = 40;

    private void Awake()
    {
        player = player.GetComponent<Transform>();
        animator = animator.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
       // Debug.LogError("Kshitij");
        
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            animator.SetBool("isFollowing", true);
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreteDistance)
        {
            animator.SetBool("isFollowing", false);
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, player.position) < retreteDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }
        if(-gameObject.transform.position.x + player.position.x  < 1.2f && -gameObject.transform.position.y + player.position.y < 1.2f)
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", true);
        }

        if (healt <= 0)
        {
            PlayerPrefs.SetInt("enemyKilled",PlayerPrefs.GetInt("enemyKilled")+1);
            Destroy(gameObject);
            Debug.Log(PlayerPrefs.GetInt("enemyKilled"));
        }

        if (player.position.x < this.gameObject.transform.position.x)
        {
            this.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }
        if (player.position.x > this.gameObject.transform.position.x)
        {
            this.gameObject.transform.localScale = new Vector3(-0.5f, 0.5f, 1);
        }
    }

    public void damage(int amount)
    {
        FindObjectOfType<SoundManager>().playSound("Hurt");
        healt -= amount;
    }
}
