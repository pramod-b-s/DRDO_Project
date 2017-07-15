using UnityEngine;
using System.Collections;

public class FlyCamScript : MonoBehaviour
{
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    private float yaw = 0f;
    private float pitch = 0f;
    public int speed = 1000;
    private int rot=0,f=0;

    float cameraDistanceMax = 20f;
    float cameraDistanceMin = 5f;
    float cameraDistance = 10f;
    float scrollSpeed = 0.5f;
    float fov,initFOV,minfov=15f,maxfov=90f;
    public float sensitivity;
    private int flag = 0;
    //public Camera cam;

    public GameObject ch1, ch2, ch3, ch4, periscope;

    private void Start()
    {
        initFOV = Camera.main.fieldOfView;
        //cam = GetComponent<Camera>();
        
        GameObject.FindGameObjectWithTag("sub_player").GetComponent<Renderer>().enabled = false;

        ch1.GetComponent<Renderer>().enabled = false;
        ch2.GetComponent<Renderer>().enabled = false;
        ch3.GetComponent<Renderer>().enabled = false;
        ch4.GetComponent<Renderer>().enabled = false;
        
    }
    

    private void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0f);

        //JOYSTICK
        //if (Input.GetButton("Fire2"))

        //PC
        //if ()

        if (Input.GetKey(KeyCode.RightShift))
        {
            /*if (flag == 0)
            {
                Camera.main.transform.position += Camera.main.transform.forward * 50;
                periscope.GetComponent<Renderer>().enabled = false;
                flag = 1;
            }*/

            /*fov = Camera.main.fieldOfView;
            fov *= sensitivity;
            fov = Mathf.Clamp(fov, maxfov, minfov);
            Camera.main.fieldOfView = fov;
            //f = 1;
            GameObject.FindGameObjectWithTag("sub_player").GetComponent<Renderer>().enabled = true;*/

            //ch1.GetComponent<Renderer>().enabled = true;
            //ch2.GetComponent<Renderer>().enabled = true;
            //ch3.GetComponent<Renderer>().enabled = true;
            //ch4.GetComponent<Renderer>().enabled = true;
        }

        //JOYSTICK
        //else

        //PC
        //if (Input.GetKey(KeyCode.O))

        else
        {
            /*if (flag == 1)
            {
                Camera.main.transform.position -= Camera.main.transform.forward * 50;
                periscope.GetComponent<Renderer>().enabled = true;
                flag = 0;
            }*/
            //Camera.main.fieldOfView = initFOV;
            //GameObject.FindGameObjectWithTag("sub_player").GetComponent<Renderer>().enabled = false;

            ch1.GetComponent<Renderer>().enabled = false;
            ch2.GetComponent<Renderer>().enabled = false;
            ch3.GetComponent<Renderer>().enabled = false;
            ch4.GetComponent<Renderer>().enabled = false;

        }


        /*if (Input.GetKey(KeyCode.LeftArrow))
        {
            rot--;
            Vector3 torque = new Vector3(0.0f, -rot, 0.0f);

            transform.rotation.Set(0.0f, -rot, 0.0f, 1f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rot++;
            Vector3 torque = new Vector3(0.0f, rot, 0.0f);

            transform.rotation.Set(0.0f, rot, 0.0f, 1f);
        }*/

        /*cameraDistance += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        cameraDistance = Mathf.Clamp(cameraDistance, cameraDistanceMin, cameraDistanceMax);*/

    }

}

/*
 
		WASD/Arrows:    Movement
		          Q:    Climb
		          E:    Drop
                      Shift:    Move faster
                    Control:    Move slower
                        End:    Toggle cursor locking to screen (you can also press Ctrl+P to toggle play mode on and off).
	

public float cameraSensitivity = 90;
public float climbSpeed = 4;
public float normalMoveSpeed = 10;
public float slowMoveFactor = 0.25f;
public float fastMoveFactor = 3;

private float rotationX = 0.0f;
private float rotationY = 0.0f;
public Rigidbody rb;

void Start()
{
    Screen.lockCursor = true;
    rb = GetComponent<Rigidbody>();
}

void Update()
{
    Vector3 displacement = new Vector3(0, 0, 1.0f);

    rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
    rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
    rotationY = Mathf.Clamp(rotationY, -90, 90);

    transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
    transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

    transform.position = rb.position + displacement;


    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
    {
        transform.position += transform.forward * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
        transform.position += transform.right * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
    }
    else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
    {
        transform.position += transform.forward * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
        transform.position += transform.right * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
    }
    else
    {
        transform.position += transform.forward * normalMoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
        transform.position += transform.right * normalMoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
    }


    if (Input.GetKey(KeyCode.Q)) { transform.position += transform.up * climbSpeed * Time.deltaTime; }
    if (Input.GetKey(KeyCode.E)) { transform.position -= transform.up * climbSpeed * Time.deltaTime; }

    if (Input.GetKeyDown(KeyCode.End))
    {
        Screen.lockCursor = (Screen.lockCursor == false) ? true : false;
    }
}*/
