using UnityEngine;
using TMPro;

public class gameController : MonoBehaviour
{
    public GameObject[] waves;
    public TextMeshProUGUI timeBar;
    public GameObject GameOverWindow;
    public PlayerController player;
    public GameObject winScreen;
    public float time = 60 ;

    private void Awake() {
        PlayerPrefs.SetInt("enemyKilled",0);
        waves[1].SetActive(false);
        waves[2].SetActive(false);
        winScreen.SetActive(false);
    }
    void Start()
    {
        FindObjectOfType<SoundManager>().playSound("MainMusic");
        player = player.GetComponent<PlayerController>();
        timeBar = timeBar.GetComponent<TextMeshProUGUI>();   
    }

    private void Update()
    {
        if(time <= 0){
            timeBar.text = "0";
            Cursor.visible = true;
            GameOverWindow.SetActive(true);
            Time.timeScale = 0;
        }
        if(PlayerPrefs.GetString("haveRewind") == "true"){
            if (Input.GetKeyDown(KeyCode.T)){
                time += 20;
                PlayerPrefs.SetString("haveRewind","false");
            }   
        }
        if (time >0){
            time -= Time.deltaTime;
            timeBar.text = (Mathf.FloorToInt(time)).ToString();
        }

        if(PlayerPrefs.GetInt("enemyKilled") == 2)
        {
            PlayerPrefs.SetString("haveRewind","true");
            waves[1].SetActive(true);
        }
        
        if(PlayerPrefs.GetInt("enemyKilled") == 5)
        {
            PlayerPrefs.SetString("haveRewind","true");
            waves[2].SetActive(true);
        }
        if(PlayerPrefs.GetInt("enemyKilled") == 9)
        {
            Cursor.visible = true;
            Time.timeScale = 0;
            winScreen.SetActive(true);
        }


    }
    
}
