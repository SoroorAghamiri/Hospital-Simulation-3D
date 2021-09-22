using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{

    Camera camera;
    public float speed = 10.0f;
    public float rotationSpeed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        camera = this.GetComponentInChildren<Camera>();
        camera.gameObject.transform.LookAt(this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //For movement
        float translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float translation2 = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        this.transform.Translate(translation2, 0, 0);

        this.transform.Translate(0, 0, translation);



        //For rotation
        if (Input.GetKey(KeyCode.Z))
        {
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.C))
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }

        //For zoom
        if (Input.GetKey(KeyCode.R) && camera.gameObject.transform.position.y > 12)
        {
            camera.gameObject.transform.Translate(0, 0, speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.F) && camera.gameObject.transform.position.y < 40)
        {
            camera.gameObject.transform.Translate(0, 0, -speed * Time.deltaTime);
        }

        //For tilting
        float angle = Vector3.Angle(camera.gameObject.transform.forward, Vector3.up);
        // Debug,Log(angle);
        if (Input.GetKey(KeyCode.T) && angle < 175)
        {
            camera.gameObject.transform.Translate(Vector3.up);
            camera.gameObject.transform.LookAt(this.transform.position);
        }
        if (Input.GetKey(KeyCode.G) && angle > 95)
        {
            camera.gameObject.transform.Translate(-Vector3.up);
            camera.gameObject.transform.LookAt(this.transform.position);
        }
    }
}
