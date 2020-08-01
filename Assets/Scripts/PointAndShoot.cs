using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndShoot : MonoBehaviour
{
    public GameObject crosshairs;
    public GameObject gun;
    public GameObject pfBullet;
    public float bulletSpeed = 10.0f;
    public GameObject bulletStart;

    private Vector3 target;

    private void Start()
    {
        Cursor.visible = false;   
    }

    private void Update()
    {
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,transform.position.z));
        crosshairs.transform.position = new Vector2(target.x, target.y);
        Vector3 difference = target - gun.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        if (Input.GetMouseButtonDown(0))
        {
            //Fire a shoot function
            Vector2 direction = difference / difference.magnitude;
            direction.Normalize();
            shootBullet(direction, rotationZ);
        }


    }

    private void shootBullet(Vector2 targetDir, float rotationZ)
    {
        GameObject b = Instantiate(pfBullet) as GameObject;
        b.transform.position = bulletStart.transform.position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        b.GetComponent<Rigidbody2D>().velocity = target * bulletSpeed;
        Destroy(b, 2.0f);
    }

}
