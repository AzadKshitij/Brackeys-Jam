using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAimWapon : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform bulletStart;
    [SerializeField] private float bulletSpeed;
    private Transform aimTransform;
    public GameObject crosshairs;
    public GameObject pfBullet;

    public Animator animator;


    private void Awake()
    {
        aimTransform = transform.Find("Aim");
        Cursor.visible = false;
    }

    private void Update()
    {
        handleAim();
    }

    private void handleAim()
    {
        Vector3 moucePosition = mainCamera.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        crosshairs.transform.position = new Vector2(moucePosition.x, moucePosition.y);
        Vector3 aimDirection = (moucePosition - transform.position).normalized;
        float rotationZ = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0.0f, 0.0f, rotationZ);

        if (rotationZ > 90 || rotationZ <-90)
        {
            this.gameObject.transform.localScale = new Vector3(1, -1, 1);
            if (Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<SoundManager>().playSound("AutomaticGunSound");
                animator.SetFloat("shooting", 1);
                Debug.Log("First");
                GameObject b = Instantiate(pfBullet, bulletStart.position, bulletStart.rotation) as GameObject;
                b.GetComponent<Rigidbody2D>().AddForce(-(bulletStart.up * bulletSpeed), ForceMode2D.Impulse);
                Destroy(b, 5.0f);
            }
        }
        else
        {
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
            if (Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<SoundManager>().playSound("AutomaticGunSound");
                animator.SetFloat("shooting", 1);
                GameObject b = Instantiate(pfBullet, bulletStart.position, bulletStart.rotation) as GameObject;
                b.GetComponent<Rigidbody2D>().AddForce(bulletStart.up * bulletSpeed, ForceMode2D.Impulse);
                Destroy(b, 2.0f);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            animator.SetFloat("shooting", -1);
        }
    }

}
