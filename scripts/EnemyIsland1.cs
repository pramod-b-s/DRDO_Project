using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyIsland1 : MonoBehaviour {

    public static int HealthIsland1 = 100;

    //dodgetime = diff in PlayerControl
    public float DodgeTime = 5;
    public Rigidbody islandEnemy1, sub;

    public int r = 100, speed = 20, incrementTheta = 15;
    public float toleranceWidth = 30, radius = 20f;
    private Vector3 pos, posl, posr;
    private int flag = 0, findWay = 0, newTheta = 0, gobackflag = 0;
    private float nextTime, theta = 0, thetal, thetar, x, z, xl, xr, zl, zr, y;
    private Vector3 initPos;

    public Text RemTime;
    private int RemTimeFlag = 0;
    private float nextRemTime = 0,timeCtr = 0;
    public int goBackSpeed = 10;

    public float timeDelay1 = 80;

    private float nextTimeDownwardForce, DownwardForceFlag = 0;

    public static Vector3 DestroyedAtPosIsl1;
    public static float DestroyedAtTimeIsl1;
    private float var;

    // Use this for initialization
    void Start () {
        initPos = islandEnemy1.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Vector3.Distance(islandEnemy1.transform.position,initPos) <= 10 && RemTimeFlag == 0)
        {
            RemTimeFlag = 1;
            nextRemTime = Time.time + 1;
            timeCtr += 1;
        }
        if(Time.time >= nextRemTime && RemTimeFlag == 1)
        {
            RemTimeFlag = 0;
        }

        if (nextRemTime >= timeDelay1)
        {
            RemTime.text = "Habitation 1 destroyed !";
            Application.Quit();
            
        }
        else
        {
            RemTime.text = "Time Rem : " + (timeDelay1 - timeCtr).ToString("0.00");
        }

        if (newTheta == 0)
        {
            theta = Random.Range(0.0f, 359.0f);
            newTheta = 1;
            RemTime.text = "";
        }

        y = 0;

        if (PlayerControl.Enemy1IslandHit == 1 && flag==0)
        {
            gobackflag = 0;

            x = r * Mathf.Cos(theta * 2 * Mathf.PI / 360);
            z = r * Mathf.Sin(theta * 2 * Mathf.PI / 360);

            pos = new Vector3(x, y, z);
            Ray ray = new Ray(transform.position, pos);

            //left width measure
            thetal = theta - toleranceWidth;
            xl = r * Mathf.Cos(thetal * 2 * Mathf.PI / 360);
            zl = r * Mathf.Sin(thetal * 2 * Mathf.PI / 360);

            posl = new Vector3(xl, y, zl);
            Ray rayl = new Ray(transform.position, posl);

            //right width measure
            thetar = theta + toleranceWidth;
            xr = r * Mathf.Cos(thetar * 2 * Mathf.PI / 360);
            zr = r * Mathf.Sin(thetar * 2 * Mathf.PI / 360);

            posr = new Vector3(xr, y, zr);
            Ray rayr = new Ray(transform.position, posr);


            RaycastHit hit, hitl, hitr;

            if (Physics.Raycast(ray, out hit, radius) == false && Physics.Raycast(rayl, out hitl, radius) == false
                && Physics.Raycast(rayr, out hitr, radius) == false)
            {
                flag = 1;
                nextTime = Time.time + DodgeTime;

            }
            else
            {
                theta = (theta + incrementTheta) % 360;
            }
            //Debug.Log(PlayerControl.Enemy1IslandHit+"theta "+theta);
        }

        if (flag == 1)
        {
            if (Time.time >= nextTime)
            {
                flag = 0;
                newTheta = 0;
                islandEnemy1.velocity = Vector3.zero;
                gobackflag = 1;
                PlayerControl.Enemy1IslandHit = 0;
                //Debug.Log(PlayerControl.Enemy1IslandHit);
            }
            else
            {
                islandEnemy1.AddForce(pos * speed, ForceMode.Force);
            }
        }

        if (gobackflag == 1 && Vector3.Distance(transform.position,initPos) >= 2)
        {
            islandEnemy1.AddForce((initPos - islandEnemy1.transform.position) *goBackSpeed, ForceMode.Force);
            //Debug.Log("inside goback");
        }


        Ray raytestGoDown = new Ray(transform.position, new Vector3(0, 1, 0));
        RaycastHit hittestgd;
        Debug.DrawRay(transform.position, new Vector3(0, 1, 0), Color.red);

        if ((Mathf.Abs(sub.transform.position.y - islandEnemy1.transform.position.y) <= 2) && sub.transform.position.y >= -4
            && DownwardForceFlag == 0 && Physics.Raycast(raytestGoDown, out hittestgd, 2) == false)
        {
            islandEnemy1.AddForce(new Vector3(0, -50000, 0), ForceMode.Force);
            nextTimeDownwardForce = Time.time + 2;
            DownwardForceFlag = 1;
        }
        if (Mathf.Abs(sub.transform.position.y - islandEnemy1.transform.position.y) <= 2 && sub.transform.position.y <= -4
            && DownwardForceFlag == 0)
        {
            islandEnemy1.AddForce(new Vector3(0, 50000, 0), ForceMode.Force);
            nextTimeDownwardForce = Time.time + 2;
            DownwardForceFlag = 1;
        }
        if (DownwardForceFlag == 1 && Time.time >= nextTimeDownwardForce)
        {
            DownwardForceFlag = 0;
        }

        var = Time.time;
    }

    /*private void OnDestroy()
    {
        if (HealthIsland1 <= 0)
        {
            PlayerControl.sw.WriteLine("enemy at island 1 destroyed");
            PlayerControl.sw.WriteLine("at position" + transform.position);
            PlayerControl.sw.WriteLine("at time" + Time.time);
        }
        else
        {
            PlayerControl.sw.WriteLine("enemy at island 1 survived");
        }
    }*/

    private void OnDestroy()
    {
        DestroyedAtPosIsl1 = transform.position;
        DestroyedAtTimeIsl1 = var;
    }

}
