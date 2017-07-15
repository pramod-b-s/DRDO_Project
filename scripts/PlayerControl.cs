using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;

public class PlayerControl : MonoBehaviour
{
    public static int health1 = 100, health2 = 100, health3 = 100, healthShip1 = 200, healthShip2 = 200;
    public static int healthHQ1 = 100, healthHQ2 = 100, healthHQ3 = 100;

    public Rigidbody sub;
    public static int health = 100;

    public int speed;
    private float ctr = 0;
    private float dx1, dy1, dz1;
    private float dx2, dy2, dz2;
    private float dx3, dy3, dz3;
    public float diff = 2;
    private float nextTime, nextTimeLight, nextTimeStatus, nextTime1, nextTime2, nextTime3;
    private float nextTimeIsland1, nextTimeIsland2, nextTimeIsland3, nextTimeShip1, nextTimeShip2;
    private int damage1 = 0, damage2 = 0, damage3 = 0, damageIsland1 = 0, damageIsland2 = 0, damageIsland3 = 0;
    private int damageShip1 = 0, damageShip2 = 0;
    private int flag = 0, f = 0, i = 0;
    private int dest1 = 0, dest2 = 0, dest3 = 0, destIsland1 = 0, destIsland2 = 0, destIsland3 = 0, destShip1 = 0, destShip2 = 0;
    private int flag1 = 0, flag2 = 0, flag3 = 0, flagIsland1 = 0, flagIsland2 = 0, flagIsland3 = 0, flagShip1 = 0, flagShip2 = 0;

    public int damageByOneShot = 40;

    public Text pos1txt, pos2txt, pos3txt;
    public Text posIsland1txt, posIsland2txt, posIsland3txt;
    public Text posShip1, posShip2;

    public Text warningtxt;
    public Text torpedo;
    public Text healthtxt;
    //public Text islandSub1,islandSub2;
    public float maxRayDist = 50, initialTime;

    public Text health1txt, health2txt, health3txt;     // health4txt, health5txt;
    public Text healthIsland1txt, healthIsland2txt, healthIsland3txt;        // healthIsland4txt, healthIsland5txt;
    public Text healthShip1txt, healthShip2txt;

    public Rigidbody e1, e2, e3, eHQ1, eHQ2, eHQ3;
    public Rigidbody ship1, ship2;

    public static int Enemy1Hit = 0, Enemy2Hit = 0, Enemy3Hit = 0, Enemy1IslandHit = 0, Enemy2IslandHit = 0, Enemy3IslandHit = 0, Enemy4IslandHit = 0;
    public static int EnemyShip1Hit = 0, EnemyShip2Hit = 0;
    //private float Enemy1HitNextTime, Enemy2HitNextTime, Enemy3HitNextTime, Enemy1IslandHitNextTime, Enemy2IslandHitNextTime;

    private int flagExtra1 = 0, flagExtra2 = 0;
    private float nxtTimeExtra1, nxtTimeExtra2;
    public AudioSource sonarClip, torpedoLaunchClip, torpedoImpactReducedVolumeClip;

    private float nextTimeShot;
    private int flagShot = 0;
    private RaycastHit hit;
    private Ray ray;
    public float TimeToHit = 2, var;

    private StreamWriter sw;

    private int hits = 0;
    private int shots = 0;

    private float nextLevelFlag, nextLevelNextTime;

    public GameObject beam;
    private LineRenderer line;

    private float rayVisibleFlag = 0, rayVisibleNextTime;

    //private DatabaseReference reference;

    void Start()
    {
        /*FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://submarinewarfaregame.firebaseio.com/");
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;*/

        pos1txt.text = "";
        pos2txt.text = "";
        pos3txt.text = "";
        posIsland1txt.text = "";
        posIsland2txt.text = "";
        posIsland3txt.text = "";
        posShip1.text = "";
        posShip2.text = "";

        initialTime = Time.time;

        healthtxt.text = "";
        warningtxt.text = "";
        torpedo.text = "";

        health1txt.text = "";
        health2txt.text = "";
        health3txt.text = "";
        healthIsland1txt.text = "";
        healthIsland2txt.text = "";
        healthIsland3txt.text = "";
        healthShip1txt.text = "";
        healthShip2txt.text = "";

        //GameObject.FindGameObjectWithTag("sub_player").GetComponent<Renderer>().enabled = false;

        //line = beam.AddComponent<LineRenderer>();

        sonarClip.Play();

        sw = new StreamWriter("datalog.txt", true);
        //Application.persistentDataPath
        line = beam.AddComponent<LineRenderer>();
    }

    private void FixedUpdate()
    {
        /*if (Input.GetKey(KeyCode.B))
        {
            //line.SetVertexCount(2);
            line.SetPosition(0, sub.transform.position);
            line.SetPosition(1, ship2.transform.position);
            //line.SetColors(Color.red, Color.red);
            line.enabled = true;
        }
        if (Input.GetKey(KeyCode.N))
        {
            line.enabled = false;
        }*/

        if (health1 <= 0)
        {
            health1txt.text = "!! :/ :/ :/ !!";
        }
        else
        {
            health1txt.text = "Enemy 1 : " + health1;
        }
        if (health2 <= 0)
        {
            health2txt.text = "!! :/ :/ :/ !!";
        }
        else
        {
            health2txt.text = "Enemy 2 : " + health2;
        }
        if (health3 <= 0)
        {
            health3txt.text = "!! :/ :/ :/ !!";
        }
        else
        {
            health3txt.text = "Enemy 3 : " + health3;
        }

        if (healthHQ1 >= 0)
        {

            healthIsland1txt.text = "Island Enemy 1 : " + healthHQ1;
        }
        else
        {
            healthIsland1txt.text = "!! :/ :/ :/ !!";
        }
        if (healthHQ2 >= 0)
        {

            healthIsland2txt.text = "Island Enemy 2 : " + healthHQ2;
        }
        else
        {
            healthIsland2txt.text = "!! :/ :/ :/ !!";
        }
        if (healthHQ3 >= 0)
        {

            healthIsland3txt.text = "Island Enemy 3 : " + healthHQ3;
        }
        else
        {
            healthIsland3txt.text = "!! :/ :/ :/ !!";
        }

        if (healthShip1 >= 0)
        {

            healthShip1txt.text = "Enemy Ship 1 : " + healthShip1;
        }
        else
        {
            healthShip1txt.text = "!! :/ :/ :/ !!";
        }
        if (healthShip2 >= 0)
        {

            healthShip2txt.text = "Enemy Ship 2 : " + healthShip2;
        }
        else
        {
            healthShip2txt.text = "!! :/ :/ :/ !!";
        }

        //view.transform.position = transform.position + Camera.main.transform.forward*10;

        Vector3 bwd = new Vector3(0f, 0f, -1.0f);
        Vector3 fwd = new Vector3(0f, 0f, 1.0f);
        Vector3 right = new Vector3(1.0f, 0f, 0f);
        Vector3 left = new Vector3(-1.0f, 0f, 0f);
        Vector3 down = new Vector3(0f, -1.0f, 0f);
        Vector3 up = new Vector3(0f, 1.0f, 0f);

        Debug.DrawLine(transform.position, transform.position + Camera.main.transform.forward * maxRayDist, Color.red);

        healthtxt.text = "Health : " + health.ToString();

        if (health <= 0)
        {
            Destroy(GameObject.FindGameObjectWithTag("sub_player"));
            healthtxt.text = "!! :/ :/ :/ !!";
            nextLevelFlag = 1;
            nextLevelNextTime = Time.time + 5;
        }

        //Debug.Log(health);

        if (Time.time >= nextTime1 && flag1 == 1)
        {
            torpedoImpactReducedVolumeClip.Play();
            flag1 = 0;
        }
        if (Time.time >= nextTime2 && flag2 == 1)
        {
            torpedoImpactReducedVolumeClip.Play();
            flag2 = 0;
        }
        if (Time.time >= nextTime3 && flag3 == 1)
        {
            torpedoImpactReducedVolumeClip.Play();
            flag3 = 0;
        }
        if (Time.time >= nextTime3 && flag3 == 1)
        {
            torpedoImpactReducedVolumeClip.Play();
            flag3 = 0;
        }

        if (Time.time >= nextTimeIsland1 && flagIsland1 == 1)
        {
            torpedoImpactReducedVolumeClip.Play();
            flagIsland1 = 0;
        }
        if (Time.time >= nextTimeIsland2 && flagIsland2 == 1)
        {
            torpedoImpactReducedVolumeClip.Play();
            flagIsland2 = 0;
        }
        if (Time.time >= nextTimeIsland3 && flagIsland3 == 1)
        {
            torpedoImpactReducedVolumeClip.Play();
            flagIsland3 = 0;
        }

        if (Time.time >= nextTimeShip1 && flagShip1 == 1)
        {
            torpedoImpactReducedVolumeClip.Play();
            flagShip1 = 0;
        }
        if (Time.time >= nextTimeShip2 && flagShip2 == 1)
        {
            torpedoImpactReducedVolumeClip.Play();
            flagShip2 = 0;
        }


        if (Time.time >= nextTimeShot && flagShot == 1)
        {
            flagShot = 0;

            if (Physics.Raycast(ray, out hit, maxRayDist))
            {
                hits++;

                if (hit.collider.tag == "sub_enemy_1" && dest1 == 0)
                {
                    if (flag1 == 0)
                    {
                        warningtxt.text = "Torpedo Shot Submarine 1 !";

                        Enemy1Hit = 1;
                        flag1 = 1;
                        nextTime1 = Time.time + diff;
                        health1 -= damageByOneShot;
                        if (health1 <= 0)
                        {
                            Destroy(GameObject.FindGameObjectWithTag("sub_enemy_1"));
                            dest1 = 1;
                            warningtxt.text = "Submarine 1 Destroyed !";
                        }
                    }
                }

                if (hit.collider.tag == "sub_enemy_2" && dest2 == 0)
                {
                    if (flag2 == 0)
                    {
                        warningtxt.text = "Torpedo Shot Submarine 2 !";

                        Enemy2Hit = 1;
                        flag2 = 1;
                        nextTime2 = Time.time + diff;
                        health2 -= damageByOneShot;
                        if (health2 <= 0)
                        {
                            Destroy(GameObject.FindGameObjectWithTag("sub_enemy_2"));
                            dest2 = 1;
                            warningtxt.text = "Submarine 2 Destroyed !";
                        }
                    }
                }

                if (hit.collider.tag == "sub_enemy_3" && dest3 == 0)
                {
                    if (flag3 == 0)
                    {
                        warningtxt.text = "Torpedo Shot Submarine 3 !";

                        Enemy3Hit = 1;
                        flag3 = 1;
                        nextTime3 = Time.time + diff;
                        health3 -= damageByOneShot;
                        if (health3 <= 0)
                        {
                            Destroy(GameObject.FindGameObjectWithTag("sub_enemy_3"));
                            dest3 = 1;
                            warningtxt.text = "Submarine 3 Destroyed !";
                        }
                    }
                }


                if (hit.collider.tag == "sub_enemy_island_1" && destIsland1 == 0)
                {
                    if (flagIsland1 == 0)
                    {
                        warningtxt.text = "Torpedo Shot HQ Submarine 1 !";
                        nextTimeIsland1 = Time.time + diff;

                        //dodge1();

                        Enemy1IslandHit = 1;
                        flagIsland1 = 1;
                        healthHQ1 -= damageByOneShot;
                        if (healthHQ1 <= 0)
                        {
                            Destroy(GameObject.FindGameObjectWithTag("sub_enemy_island_1"));
                            destIsland1 = 1;
                            warningtxt.text = "HQ Submarine 1 Destroyed !";
                        }
                    }
                }

                if (hit.collider.tag == "sub_enemy_island_2" && destIsland2 == 0)
                {
                    if (flagIsland2 == 0)
                    {
                        warningtxt.text = "Torpedo Shot HQ Submarine 2 !";
                        nextTimeIsland2 = Time.time + diff;

                        Enemy2IslandHit = 1;
                        flagIsland2 = 1;
                        healthHQ2 -= damageByOneShot;
                        if (healthHQ2 <= 0)
                        {
                            Destroy(GameObject.FindGameObjectWithTag("sub_enemy_island_2"));
                            destIsland2 = 1;
                            warningtxt.text = "HQ Submarine 2 Destroyed !";
                        }
                    }
                }

                if (hit.collider.tag == "sub_enemy_island_3" && destIsland3 == 0)
                {
                    if (flagIsland3 == 0)
                    {
                        warningtxt.text = "Torpedo Shot HQ Submarine 3 !";
                        nextTimeIsland3 = Time.time + diff;

                        Enemy3IslandHit = 1;
                        flagIsland3 = 1;
                        healthHQ2 -= damageByOneShot;
                        if (healthHQ3 <= 0)
                        {
                            Destroy(GameObject.FindGameObjectWithTag("sub_enemy_island_3"));
                            destIsland3 = 1;
                            warningtxt.text = "HQ Submarine 3 Destroyed !";
                        }
                    }
                }

                if (hit.collider.tag == "ship1" && destShip1 == 0)
                {
                    if (flagShip1 == 0)
                    {
                        warningtxt.text = "Torpedo Shot Ship 1 !";

                        EnemyShip1Hit = 1;
                        flagShip1 = 1;
                        nextTimeShip1 = Time.time + diff;
                        healthShip1 -= damageByOneShot;
                        if (healthShip1 <= 0)
                        {
                            Destroy(GameObject.FindGameObjectWithTag("ship1"));
                            destShip1 = 1;
                            warningtxt.text = "Ship 1 Destroyed !";
                        }
                    }
                }

                if (hit.collider.tag == "ship2" && destShip2 == 0)
                {
                    if (flagShip2 == 0)
                    {
                        warningtxt.text = "Torpedo Shot Ship 2 !";

                        EnemyShip2Hit = 1;
                        flagShip2 = 1;
                        nextTimeShip2 = Time.time + diff;
                        healthShip2 -= damageByOneShot;
                        if (healthShip2 <= 0)
                        {
                            Destroy(GameObject.FindGameObjectWithTag("ship2"));
                            destShip2 = 1;
                            warningtxt.text = "Ship 2 Destroyed !";
                        }
                    }
                }

            }

            if (flag1 == 0 && flag2 == 0 && flag3 == 0 && flagIsland1 == 0 && flagIsland2 == 0 && flagIsland3 == 0
                    && flagShip1 == 0 && flagShip2 == 0)
            {
                warningtxt.text = "No Collision of Torpedo with Enemy !";
            }

            nextTime = Time.time + diff;
        }

        //FOR JOYSTICK
        /*float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        sub.AddForce(new Vector3(h, 0, v) * speed, ForceMode.Force);*/

        //FOR JOYSTICK
        //if (Input.GetButton("Fire3") && flag==0){

        //FOR DESKTOP
        //if (Input.GetKey(KeyCode.Space) && flag==0)

        if (Input.GetKey(KeyCode.Space) && flag == 0)
        {
            shots++;

            torpedoLaunchClip.Play();

            flag = 1;
            ray = new Ray(transform.position, Camera.main.transform.forward * maxRayDist);

            flagShot = 1;

            //Debug.DrawLine(transform.position, transform.position + Vector3.right * maxRayDist, Color.red);

            nextTimeShot = Time.time + diff;

            torpedo.text = "Torpedo shot Successfully !";
            nextTimeStatus = Time.time + TimeToHit;

            rayVisibleNextTime = Time.time + 0.5f;
            rayVisibleFlag = 1;

            //Vector3 vec = Camera.main.transform.position * 50 - sub.transform.position;
            line.SetPosition(0, sub.transform.position);
            line.SetPosition(1, Camera.main.transform.forward * maxRayDist + sub.transform.position);
            Material sample = new Material(Shader.Find("Unlit/Texture"));
            sample.SetColor("color",Color.black);// = Color.black;
            line.material = sample;
            line.SetColors(Color.black, Color.black);
            line.SetWidth(0.2f, 0.2f);
            line.enabled = true;

        }

        if (dest1 == 1 && dest2 == 1 && dest3 == 1 && destIsland1 == 1 && destIsland2 == 1 && destIsland3 == 1 && destShip1 == 1 && destShip2 == 1)
        {
            warningtxt.text = "You Win !!";
            Application.Quit();
        }

        if (Time.time >= nextTime)
        {
            warningtxt.text = "";
        }
        if (Time.time >= nextTimeStatus)
        {
            torpedo.text = "";
            flag = 0;
        }

        //FOR DESKTOP GAME
        if (Input.GetKey(KeyCode.UpArrow))
        {
            sub.AddForce(fwd * speed, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            sub.AddForce(bwd * speed, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            sub.AddForce(left * speed, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            sub.AddForce(right * speed, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.S))
        {
            sub.AddForce(down * speed, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (sub.position.y >= 0)
            {
                ;
            }
            else
            {
                sub.AddForce(up * speed, ForceMode.Force);
            }
        }

        //FOR JOYSTICK MOBILE GAME
        /*if (Input.GetButton("Jump"))
        {
            sub.AddForce(down * speed, ForceMode.Force);  
        }
        if (Input.GetButton("Fire1"))
        {
            if (sub.position.y >= 1)
            {
                ;
            }
            else
            {
                sub.AddForce(up * speed, ForceMode.Force);
            }
        }*/


        if (dest1 == 0)
        {

            pos1txt.text = "Sub 1 : " + (GameObject.FindGameObjectWithTag("sub_player").transform.position -
                e1.transform.position).ToString() + " Dist : " +
                Vector3.Distance(GameObject.FindGameObjectWithTag("sub_player").transform.position,
                e1.transform.position).ToString();
        }


        if (dest2 == 0)
        {

            pos2txt.text = "Sub 2 : " + (GameObject.FindGameObjectWithTag("sub_player").transform.position -
                e2.transform.position).ToString() + " Dist : " +
                Vector3.Distance(GameObject.FindGameObjectWithTag("sub_player").transform.position,
                e2.transform.position).ToString();

        }

        if (dest3 == 0)
        {

            pos3txt.text = "Sub 3 : " + (GameObject.FindGameObjectWithTag("sub_player").transform.position -
                e3.transform.position).ToString() + " Dist : " +
                Vector3.Distance(GameObject.FindGameObjectWithTag("sub_player").transform.position,
                e3.transform.position).ToString();

        }

        if (destIsland1 == 0)
        {
            posIsland1txt.text = "Sub HQ 1 : " + (GameObject.FindGameObjectWithTag("sub_player").transform.position -
                eHQ1.transform.position).ToString() + " Dist : " +
                Vector3.Distance(GameObject.FindGameObjectWithTag("sub_player").transform.position,
                eHQ1.transform.position).ToString();
        }

        if (destIsland2 == 0)
        {
            posIsland2txt.text = "Sub HQ 2 : " + (GameObject.FindGameObjectWithTag("sub_player").transform.position -
                eHQ2.transform.position).ToString() + " Dist : " +
                Vector3.Distance(GameObject.FindGameObjectWithTag("sub_player").transform.position,
                eHQ2.transform.position).ToString();
        }

        if (destIsland3 == 0)
        {
            posIsland3txt.text = "Sub HQ 3 : " + (GameObject.FindGameObjectWithTag("sub_player").transform.position -
                eHQ3.transform.position).ToString() + " Dist : " +
                Vector3.Distance(GameObject.FindGameObjectWithTag("sub_player").transform.position,
                eHQ3.transform.position).ToString();
        }

        if (destShip1 == 0)
        {
            posShip1.text = "Ship 1 : " + (GameObject.FindGameObjectWithTag("sub_player").transform.position -
                ship1.transform.position).ToString() + " Dist : " +
                Vector3.Distance(GameObject.FindGameObjectWithTag("sub_player").transform.position,
                ship1.transform.position).ToString();
        }

        if (destShip2 == 0)
        {
            posShip2.text = "Ship 2 : " + (GameObject.FindGameObjectWithTag("sub_player").transform.position -
                ship2.transform.position).ToString() + " Dist : " +
                Vector3.Distance(GameObject.FindGameObjectWithTag("sub_player").transform.position,
                ship2.transform.position).ToString();
        }

        //FOR DIFFICULT LEVEL

        var = Time.time;


        if (nextLevelFlag == 1 && Time.time >= nextLevelNextTime)
        {
            Application.LoadLevel(2);
        }

        if (rayVisibleFlag == 1 && Time.time >= rayVisibleNextTime)
        {
            rayVisibleFlag = 0;
            line.enabled = false;
        }

    }


    void OnCollisionEnter(Collision col)
    {
        ctr += 1.0f;
        if (ctr <= 3.0f)
        {
            warningtxt.text = "Collision ! Damage = " + (ctr / 3.0f * 100.0f).ToString("0.00") + "%";

            if (ctr == 3.0f)
            {
                warningtxt.text = "Submarine Critical ! Be very careful !";
            }
        }
        else
        {
            warningtxt.text = "Busted ! Submarine destroyed !";
            Application.Quit();
        }
    }

    private void Update()
    {

    }

    private void OnDestroy()
    {
        //firebase
        //reference.Child("users").Child("accuracy").SetValueAsync("testing");

        using (sw)
        {
            sw.WriteLine("hits = " + hits);
            sw.WriteLine("shots fired = " + shots);
            sw.WriteLine("accuracy = " + hits + "/" + shots);

            if (health <= 0)
            {
                sw.WriteLine("player destroyed ");
                sw.WriteLine("at position " + transform.position);
                sw.WriteLine("at time " + var);
            }
            else
            {
                sw.WriteLine("player survived ");
                sw.WriteLine("time taken " + var);
            }

            if (health1 <= 0)
            {
                sw.WriteLine("Submarine 1 destroyed ");
                sw.WriteLine("at position " + AggressiveEnemy1.DestroyedAtPos);
                sw.WriteLine("at time " + AggressiveEnemy1.DestroyedAtTime);
            }
            else
            {
                sw.WriteLine("Submarine 1 survived");
            }

            if (health2 <= 0)
            {
                sw.WriteLine("Submarine 2 destroyed ");
                sw.WriteLine("at position " + AggressiveEnemy2.DestroyedAtPos);
                sw.WriteLine("at time " + AggressiveEnemy2.DestroyedAtTime);
            }
            else
            {
                sw.WriteLine("Submarine 2 survived");
            }

            if (health3 <= 0)
            {
                sw.WriteLine("Submarine 3 destroyed ");
                sw.WriteLine("at position " + AggressiveEnemy3.DestroyedAtPos);
                sw.WriteLine("at time " + AggressiveEnemy3.DestroyedAtTime);
            }
            else
            {
                sw.WriteLine("Submarine 3 survived");
            }


            if (healthHQ1 <= 0)
            {
                sw.WriteLine("Submarine at HQ 1 destroyed ");
                sw.WriteLine("at position " + HQAttackingEnemy1.DestroyedAtPosIsl);
                sw.WriteLine("at time " + HQAttackingEnemy1.DestroyedAtPosIsl);
            }
            else
            {
                sw.WriteLine("Submarine at HQ 1 destroyed ");
            }

            if (healthHQ2 <= 0)
            {
                sw.WriteLine("Submarine at HQ 2 destroyed ");
                sw.WriteLine("at position " + HQAttackingEnemy2.DestroyedAtPosIsl);
                sw.WriteLine("at time " + HQAttackingEnemy2.DestroyedAtPosIsl);
            }
            else
            {
                sw.WriteLine("Submarine at HQ 2 destroyed ");
            }

            if (healthHQ3 <= 0)
            {
                sw.WriteLine("Submarine at HQ 3 destroyed ");
                sw.WriteLine("at position " + HQAttackingEnemy3.DestroyedAtPosIsl);
                sw.WriteLine("at time " + HQAttackingEnemy3.DestroyedAtPosIsl);
            }
            else
            {
                sw.WriteLine("Submarine at HQ 3 destroyed ");
            }

            /*if (healthShip1 <= 0)
            {
                sw.WriteLine("Ship 1 destroyed ");
                sw.WriteLine("at position " + ShipScript1.DestroyedAtPosShip);
                sw.WriteLine("at time " + ShipScript1.DestroyedAtTimeShip);
            }
            else
            {
                sw.WriteLine("Ship 1 survived");
            }

            if (healthShip2 <= 0)
            {
                sw.WriteLine("Ship 2 destroyed ");
                sw.WriteLine("at position " + ShipScript2.DestroyedAtPosShip);
                sw.WriteLine("at time " + ShipScript2.DestroyedAtTimeShip);
            }
            else
            {
                sw.WriteLine("Ship 2 survived");
            }*/

        }
    }
}





/*
 

        //USING E1,E2,E3 DOESNT GIVE DISTANCE BETWEEN ENEMY AND PLAYER
        if (dest1 == 0)
        {

            pos1txt.text = "Sub 1 : " + (GameObject.FindGameObjectWithTag("sub_player").transform.position -
                GameObject.FindGameObjectWithTag("sub_enemy_1").transform.position).ToString() + " Dist : " +
                Vector3.Distance(GameObject.FindGameObjectWithTag("sub_player").transform.position,
                GameObject.FindGameObjectWithTag("sub_enemy_1").transform.position).ToString();
        }


        if (dest2 == 0)
        {

            pos2txt.text = "Sub 2 : " + (GameObject.FindGameObjectWithTag("sub_player").transform.position -
                GameObject.FindGameObjectWithTag("sub_enemy_2").transform.position).ToString() + " Dist : " +
                Vector3.Distance(GameObject.FindGameObjectWithTag("sub_player").transform.position,
                GameObject.FindGameObjectWithTag("sub_enemy_2").transform.position).ToString();

        }

        if (dest3 == 0)
        {

            pos3txt.text = "Sub 3 : " + (GameObject.FindGameObjectWithTag("sub_player").transform.position -
                GameObject.FindGameObjectWithTag("sub_enemy_3").transform.position).ToString() + " Dist : " +
                Vector3.Distance(GameObject.FindGameObjectWithTag("sub_player").transform.position,
                GameObject.FindGameObjectWithTag("sub_enemy_3").transform.position).ToString();

        }

        if (destIsland1 == 0)
        {
            posIsland1txt.text = "Sub HQ 1 : " + (GameObject.FindGameObjectWithTag("sub_player").transform.position -
                GameObject.FindGameObjectWithTag("sub_enemy_island_1").transform.position).ToString() + " Dist : " +
                Vector3.Distance(GameObject.FindGameObjectWithTag("sub_player").transform.position,
                GameObject.FindGameObjectWithTag("sub_enemy_island_1").transform.position).ToString();
        }

        if (destIsland2 == 0)
        {
            posIsland2txt.text = "Sub HQ 2 : " + (GameObject.FindGameObjectWithTag("sub_player").transform.position -
                GameObject.FindGameObjectWithTag("sub_enemy_island_2").transform.position).ToString() + " Dist : " +
                Vector3.Distance(GameObject.FindGameObjectWithTag("sub_player").transform.position,
                GameObject.FindGameObjectWithTag("sub_enemy_island_2").transform.position).ToString();
        }

        if (destIsland3 == 0)
        {
            posIsland3txt.text = "Sub HQ 3 : " + (GameObject.FindGameObjectWithTag("sub_player").transform.position -
                GameObject.FindGameObjectWithTag("sub_enemy_island_3").transform.position).ToString() + " Dist : " +
                Vector3.Distance(GameObject.FindGameObjectWithTag("sub_player").transform.position,
                GameObject.FindGameObjectWithTag("sub_enemy_island_3").transform.position).ToString();
        }

        if (destShip1 == 0)
        {
            posShip1.text = "Ship 1 : " + (GameObject.FindGameObjectWithTag("sub_player").transform.position -
                GameObject.FindGameObjectWithTag("ship1").transform.position).ToString() + " Dist : " +
                Vector3.Distance(GameObject.FindGameObjectWithTag("sub_player").transform.position,
                GameObject.FindGameObjectWithTag("ship1").transform.position).ToString();
        }

        if (destShip2 == 0)
        {
            posShip1.text = "Ship 2 : " + (GameObject.FindGameObjectWithTag("sub_player").transform.position -
                GameObject.FindGameObjectWithTag("ship2").transform.position).ToString() + " Dist : " +
                Vector3.Distance(GameObject.FindGameObjectWithTag("sub_player").transform.position,
                GameObject.FindGameObjectWithTag("ship2").transform.position).ToString();
        }*/



/*
 
    if (Input.GetKey(KeyCode.Space) && flag==0){

            torpedoLaunchClip.Play();

            flag = 1;
            Ray ray = new Ray(transform.position, Camera.main.transform.forward * maxRayDist);
            RaycastHit hit;

            //Debug.DrawLine(transform.position, transform.position + Vector3.right * maxRayDist, Color.red);
            if (Physics.Raycast(ray, out hit, maxRayDist))
            {
                

                if (hit.collider.tag == "sub_enemy_1" && dest1==0)
                {                   
                    if(flag1==0)
                    {
                        warningtxt.text = "Torpedo Shot Enemy 1 !";

                        Enemy1Hit = 1;
                        flag1 = 1;
                        nextTime1 = Time.time + diff;
                        Enemy1.health1 -= damageByOneShot;
                        if (Enemy1.health1 <= 0)
                        {
                            Destroy(GameObject.FindGameObjectWithTag("sub_enemy_1"));
                            dest1 = 1;
                            warningtxt.text = "Enemy 1 Destroyed !";
                        }
                    }
                }

                if (hit.collider.tag == "sub_enemy_2" && dest2==0)
                {
                    if (flag2 == 0)
                    {
                        warningtxt.text = "Torpedo Shot Enemy 2 !";

                        Enemy2Hit = 1;
                        flag2 = 1;
                        nextTime2 = Time.time + diff;
                        Enemy2.health2 -= damageByOneShot;
                        if (Enemy2.health2 <= 0)
                        {
                            Destroy(GameObject.FindGameObjectWithTag("sub_enemy_2"));
                            dest2 = 1;
                            warningtxt.text = "Enemy 2 Destroyed !";
                        }
                    }
                }

                if (hit.collider.tag == "sub_enemy_3" && dest3==0)
                {
                    if (flag3 == 0)
                    {
                        warningtxt.text = "Torpedo Shot Enemy 3 !";

                        Enemy3Hit = 1;
                        flag3 = 1;
                        nextTime3 = Time.time + diff;
                        Enemy3.health3 -= damageByOneShot;
                        if (Enemy3.health3 <= 0)
                        {
                            Destroy(GameObject.FindGameObjectWithTag("sub_enemy_3"));
                            dest3 = 1;
                            warningtxt.text = "Enemy 3 Destroyed !";
                        }
                    }
                }

                if (hit.collider.tag == "sub_enemy_island_1" && destIsland1==0)
                {
                    if (flagIsland1 == 0)
                    {
                        warningtxt.text = "Torpedo Shot Island Enemy 1 !";
                        nextTimeIsland1 = Time.time + diff;

                        //dodge1();

                        Enemy1IslandHit = 1;
                        flagIsland1 = 1;
                        EnemyIsland1.HealthIsland1 -= damageByOneShot;
                        if (EnemyIsland1.HealthIsland1 <= 0)
                        {
                            Destroy(GameObject.FindGameObjectWithTag("sub_enemy_island_1"));
                            destIsland1 = 1;
                            warningtxt.text = "Island Enemy 1 Destroyed !";
                        }
                    }
                }

                if (hit.collider.tag == "sub_enemy_island_2" && destIsland2==0)
                {
                    if (flagIsland2 == 0)
                    {
                        warningtxt.text = "Torpedo Shot Island Enemy 2 !";
                        nextTimeIsland2 = Time.time + diff;

                        Enemy2IslandHit = 1;
                        flagIsland2 = 1;
                        EnemyIsland2.HealthIsland2 -= damageByOneShot;
                        if (EnemyIsland2.HealthIsland2 <= 0)
                        {
                            Destroy(GameObject.FindGameObjectWithTag("sub_enemy_island_2"));
                            destIsland2 = 1;
                            warningtxt.text = "Island Enemy 2 Destroyed !";
                        }
                    }
                }

                if (flag1 == 0 && flag2 == 0 && flag3 == 0 && flagIsland1 == 0 && flagIsland2 == 0)
                {
                    warningtxt.text = "No Collision of Torpedo with Enemy !";
                }

                nextTime = Time.time + diff;
            }
            
            torpedo.text = "Torpedo shot Successfully !";
            nextTimeStatus = Time.time + diff;
           
        }


     */


