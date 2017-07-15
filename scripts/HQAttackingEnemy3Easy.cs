using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HQAttackingEnemy3Easy : MonoBehaviour
{

    //dodgetime = diff in PlayerControl
    public float DodgeTime = 5;
    public Rigidbody HQEnemy, sub;

    public int r = 100, DodgeSpeed = 15, incrementTheta = 15;
    public float toleranceWidth = 30, radius = 20f;
    private Vector3 pos, posl, posr;
    private int flag = 0, findWay = 0, newTheta = 0, gobackflag = 0;
    private float nextTime, theta = 0, thetal, thetar, x, z, xl, xr, zl, zr, y;
    private Vector3 initPos;

    public Text RemTime;
    private int RemTimeFlag = 0;
    private float nextRemTime = 0, timeCtr = 0;
    public int goBackSpeed = 10;

    public float timeDelay = 800;

    private float nextTimeDownwardForce, DownwardForceFlag = 0;

    public static Vector3 DestroyedAtPosIsl;
    public static float DestroyedAtTimeIsl;
    private float var;
    public float downwardSpeed = 5000, downwardTime = 2;

    // Use this for initialization
    void Start()
    {
        initPos = HQEnemy.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(HQEnemy.transform.position, initPos) <= 10 && RemTimeFlag == 0)
        {
            RemTimeFlag = 1;
            nextRemTime = Time.time + 1;
            timeCtr += 1;
        }
        if (Time.time >= nextRemTime && RemTimeFlag == 1)
        {
            RemTimeFlag = 0;
        }

        if (nextRemTime >= timeDelay)
        {
            RemTime.text = "HQ destroyed !";
            Application.LoadLevel(3);
            //Application.Quit();
        }
        else
        {
            RemTime.text = "Time Rem : " + (timeDelay - timeCtr).ToString("0.00");
        }

        if (newTheta == 0)
        {
            theta = Random.Range(0.0f, 359.0f);
            newTheta = 1;
            RemTime.text = "";
        }

        y = 0;

        if (PlayerControlEasy.Enemy3IslandHit == 1 && flag == 0)
        {
            gobackflag = 0;

            x = r * Mathf.Cos(theta * 2 * Mathf.PI / 360);
            z = r * Mathf.Sin(theta * 2 * Mathf.PI / 360);

            pos = new Vector3(x, y, z);
            Ray ray = new Ray(transform.position, pos);
            Debug.DrawRay(transform.position, pos, Color.red);

            //left width measure
            thetal = theta - toleranceWidth;
            xl = r * Mathf.Cos(thetal * 2 * Mathf.PI / 360);
            zl = r * Mathf.Sin(thetal * 2 * Mathf.PI / 360);

            posl = new Vector3(xl, y, zl);
            Ray rayl = new Ray(transform.position, posl);
            Debug.DrawRay(transform.position, posl, Color.red);

            //right width measure
            thetar = theta + toleranceWidth;
            xr = r * Mathf.Cos(thetar * 2 * Mathf.PI / 360);
            zr = r * Mathf.Sin(thetar * 2 * Mathf.PI / 360);

            posr = new Vector3(xr, y, zr);
            Ray rayr = new Ray(transform.position, posr);
            Debug.DrawRay(transform.position, posr, Color.red);

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
                HQEnemy.velocity = Vector3.zero;
                gobackflag = 1;
                PlayerControlEasy.Enemy3IslandHit = 0;
                //Debug.Log(PlayerControl.Enemy1IslandHit);
            }
            else
            {
                HQEnemy.AddForce(pos * DodgeSpeed, ForceMode.Force);
            }
        }

        if (gobackflag == 1 && Vector3.Distance(transform.position, initPos) >= 2)
        {
            HQEnemy.AddForce((initPos - HQEnemy.transform.position) * goBackSpeed, ForceMode.Force);
            //Debug.Log("inside goback");
        }


        Ray raytestGoDown = new Ray(transform.position, new Vector3(0, -1, 0));
        RaycastHit hittestgd;
        Debug.DrawRay(transform.position, new Vector3(0, -1, 0), Color.red);

        Ray raytestGoUp = new Ray(transform.position, new Vector3(0, 1, 0));
        RaycastHit hittestgu;
        Debug.DrawRay(transform.position, new Vector3(0, 1, 0), Color.red);


        if ((Mathf.Abs(sub.transform.position.y - HQEnemy.transform.position.y) <= 2) && sub.transform.position.y >= -4
            && DownwardForceFlag == 0 && Physics.Raycast(raytestGoDown, out hittestgd, 2) == false)
        {
            HQEnemy.AddForce(new Vector3(0, -downwardSpeed, 0), ForceMode.Force);
            nextTimeDownwardForce = Time.time + downwardTime;
            DownwardForceFlag = 1;
        }
        if (Mathf.Abs(sub.transform.position.y - HQEnemy.transform.position.y) <= 2 && sub.transform.position.y <= -4
            && DownwardForceFlag == 0 && Physics.Raycast(raytestGoUp, out hittestgu, 2) == false)
        {
            HQEnemy.AddForce(new Vector3(0, downwardSpeed, 0), ForceMode.Force);
            nextTimeDownwardForce = Time.time + downwardTime;
            DownwardForceFlag = 1;
        }
        if (DownwardForceFlag == 1 && Time.time >= nextTimeDownwardForce)
        {
            DownwardForceFlag = 0;
            HQEnemy.velocity = Vector3.zero;
        }

        var = Time.time;
    }


    private void OnDestroy()
    {
        DestroyedAtPosIsl = transform.position;
        DestroyedAtTimeIsl = var;
    }
}

