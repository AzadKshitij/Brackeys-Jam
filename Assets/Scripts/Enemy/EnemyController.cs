using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private static EnemyController instance;

    public float speed;
    public float stoppingDistance;
    public float retreteDistance;

    private float timeBWShots;
    public float startTimeBWShots;

    [SerializeField] private Animator animator;

    public GameObject projectile;
    public Transform projectileStart;
    public Transform player;


    //Health
    public int healt = 40;

    public static EnemyController GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
        player = player.GetComponent<Transform>();
        animator = animator.GetComponent<Animator>();
        projectileStart = projectileStart.GetComponent<Transform>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        timeBWShots = startTimeBWShots;
        
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log(PlayerPrefs.GetInt("enemyKilled"));

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

        if (timeBWShots <= 0)
        {
            Instantiate(projectile, projectileStart.position , Quaternion.identity);
            timeBWShots = startTimeBWShots;
        }
        else
        {
            timeBWShots -= Time.deltaTime;
        }
        if (healt <= 0)
        {
            PlayerPrefs.SetInt("enemyKilled",PlayerPrefs.GetInt("enemyKilled")+1);
            Destroy(gameObject);
            Debug.Log(PlayerPrefs.GetInt("enemyKilled"));
        }
        if (player.position.x < this.gameObject.transform.position.x)
        {
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        if (player.position.x > this.gameObject.transform.position.x)
        {
            this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void damage(int amount)
    {
        FindObjectOfType<SoundManager>().playSound("Hurt");
        healt -= amount;
    }
}
