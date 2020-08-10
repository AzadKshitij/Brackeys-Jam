using UnityEngine;
using TMPro;


public class playerAimWapon : MonoBehaviour
{
    public Camera mainCamera;
    public Transform bulletStart;
    public float bulletSpeed;
    public GameObject crosshairs;
    public GameObject pfBullet;
    public Transform pBody;
    public Animator animator;
    public GameObject UI;
    public TextMeshProUGUI bulletsText;

    private Transform aimTransform;
    private int bullets = 60 ;

    private void Awake()
    {
        PlayerPrefs.SetString("haveRewind","true");
        pBody = pBody.GetComponent<Transform>();
        bulletsText = bulletsText.GetComponent<TextMeshProUGUI>();
        aimTransform = transform.Find("Aim");
        Cursor.visible = false;
        bulletsText.text = "60" ;

    }

    private void Update()
    {
        if(PlayerPrefs.GetString("haveRewind") == "true"){
            if (Input.GetKeyDown(KeyCode.B)){
                bullets += 20;
                PlayerPrefs.SetString("haveRewind","false");
            }
        }
        Vector3 moucePosition = mainCamera.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        if (Time.timeScale != 0)
        {
            crosshairs.transform.position = new Vector2(moucePosition.x, moucePosition.y);
            handleAim(moucePosition);
        }
        else
        {
            return;
        }
    }

    private void handleAim(Vector3 moucePosition)
    {
        Vector3 aimDirection = (moucePosition - transform.position).normalized;
        float rotationZ = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0.0f, 0.0f, rotationZ);

        if (rotationZ > 90 || rotationZ <-90)
        {
            this.gameObject.transform.localScale = new Vector3(1, -1, 1);
            pBody.localScale = new Vector3(-1, 1, 1);
            if (Input.GetMouseButtonDown(0))
            {
                if (bullets > 0){
                bulletsText.text = (bullets - 1).ToString();
                FindObjectOfType<SoundManager>().playSound("AutomaticGunSound");
                animator.SetFloat("shooting", 1);
                GameObject b = Instantiate(pfBullet, bulletStart.position, bulletStart.rotation) as GameObject;
                b.GetComponent<Rigidbody2D>().AddForce(-(bulletStart.up * bulletSpeed), ForceMode2D.Impulse);
                Destroy(b, 5.0f);
                }
            }
        }
        else
        {
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
            pBody.localScale = new Vector3(1, 1, 1);

            if (Input.GetMouseButtonDown(0))
            {
                if (bullets > 0){

                bulletsText.text = (bullets--).ToString();
                FindObjectOfType<SoundManager>().playSound("AutomaticGunSound");
                animator.SetFloat("shooting", 1);
                GameObject b = Instantiate(pfBullet, bulletStart.position, bulletStart.rotation) as GameObject;
                b.GetComponent<Rigidbody2D>().AddForce(bulletStart.up * bulletSpeed, ForceMode2D.Impulse);
                Destroy(b, 2.0f);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            animator.SetFloat("shooting", -1);
        }
    }

}
