using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;


public class MemoryScript : MonoBehaviour
{
	public static int health1 = 100, health2 = 100, health3 = 100;
	public static int healthHQ1 = 100, healthHQ2 = 100, healthHQ3 = 100;

	public Rigidbody sub;
	public int time = 600;

	public int speed = 500;
	public float diff = 2;

	public Text healthtxt;
	public float maxRayDist = 50, initialTime;

	public GameObject[] obj;
	private int[] status,dest;
	private float[] timeDest;

	public AudioSource sonarClip, torpedoLaunchClip, torpedoImpactReducedVolumeClip;
	public float TimeToHit = 2, var;

	public GameObject beam;

	private float ctr = 0;
	private float dx1, dy1, dz1;
	private float dx2, dy2, dz2;
	private float dx3, dy3, dz3;

	private float nextTime, nextTimeLight, nextTimeStatus, nextTime1, nextTime2, nextTime3;
	private float nextTimeIsland1, nextTimeIsland2, nextTimeIsland3, nextTimeShip1, nextTimeShip2;

	private int flag = 0,i;

	private float nextTimeShot;
	private int flagShot = 0;
	private RaycastHit hit;
	private Ray ray;

	private StreamWriter sw;

	private int hits = 0;
	private int shots = 0,recordFlag=0,counter=0;

	private float nextLevelFlag, nextLevelNextTime,nextLevelRecord;

	private LineRenderer line;

	private float rayVisibleFlag = 0, rayVisibleNextTime;
	private int ctrlFlag = 0, winFlag = 0, disorientflag = 0,doneFlag=0,misses=0;

	private Vector3[] positions;


	void Start()
	{
		initialTime = Time.time;

		healthtxt.text = "";

		status = new int[10];
		dest = new int[10];
		timeDest = new float[10];

		//obj = new GameObject[10];

		positions = new Vector3[1500];

		sonarClip.Play();

		sw = new StreamWriter("datalogMemory.txt", true);
		//Application.persistentDataPath
		line = beam.AddComponent<LineRenderer>();
		line.enabled = false;

		for (i = 0; i < 10; i++) {
			dest [i] = 0;
		}

		winFlag = 1;
	}

	private void FixedUpdate()
	{

		if (recordFlag == 0) {
			positions [counter] = GameObject.FindGameObjectWithTag("sub_player").transform.position;
			counter++;
			recordFlag = 1;

			nextLevelRecord = Time.time + 0.5f;
		}

		if (Time.time >= nextLevelRecord) {
			recordFlag = 0;
		}

		Vector3 bwd = new Vector3(0f, 0f, -1.0f);
		Vector3 fwd = new Vector3(0f, 0f, 1.0f);
		Vector3 right = new Vector3(1.0f, 0f, 0f);
		Vector3 left = new Vector3(-1.0f, 0f, 0f);
		Vector3 down = new Vector3(0f, -1.0f, 0f);
		Vector3 up = new Vector3(0f, 1.0f, 0f);

		Debug.DrawLine(transform.position, transform.position + Camera.main.transform.forward * maxRayDist, Color.red);

		healthtxt.text = "Time Left : " + (time-Time.time).ToString();

		if (time-Time.time <= 0)
		{
			Destroy(GameObject.FindGameObjectWithTag("sub_player"));
			healthtxt.text = "";
			nextLevelFlag = 1;
			nextLevelNextTime = Time.time + 5;
			Application.LoadLevel(2);
		}

		if (Time.time >= nextTimeShot && flagShot == 1)
		{
			flagShot = 0;

			if (Physics.Raycast(ray, out hit, maxRayDist))
			{
				if (hit.collider.tag == ("distraction"))
				{
					misses++;

					torpedoImpactReducedVolumeClip.Play();
				}


				if (hit.collider.tag == ("obj0") && dest[0]==0)
				{
					hits++;
					positions [0] = obj [0].transform.position;
					timeDest [0] = Time.time;

					torpedoImpactReducedVolumeClip.Play();
					Destroy(GameObject.FindGameObjectWithTag("obj0"));
					dest [0] = 1;
				}
				if (hit.collider.tag == ("obj1") && dest[1]==0)
				{
					hits++;
					positions [1] = obj [1].transform.position;
					timeDest [1] = Time.time;

					torpedoImpactReducedVolumeClip.Play();
					Destroy(GameObject.FindGameObjectWithTag("obj1"));
					dest [1] = 1;
				}
				if (hit.collider.tag == ("obj2") && dest[2]==0)
				{
					hits++;
					positions [2] = obj [2].transform.position;
					timeDest [2] = Time.time;

					torpedoImpactReducedVolumeClip.Play();
					Destroy(GameObject.FindGameObjectWithTag("obj2"));
					dest [2] = 1;
				}
				if (hit.collider.tag == ("obj3") && dest[3]==0)
				{
					hits++;
					positions [3] = obj [3].transform.position;
					timeDest [3] = Time.time;

					torpedoImpactReducedVolumeClip.Play();
					Destroy(GameObject.FindGameObjectWithTag("obj3"));
					dest [3] = 1;
				}
				if (hit.collider.tag == ("obj4") && dest[4]==0)
				{
					hits++;
					positions [4] = obj [4].transform.position;
					timeDest [4] = Time.time;

					torpedoImpactReducedVolumeClip.Play();
					Destroy(GameObject.FindGameObjectWithTag("obj4"));
					dest [4] = 1;
				}
				if (hit.collider.tag == ("obj5") && dest[5]==0)
				{
					hits++;
					positions [5] = obj [5].transform.position;
					timeDest [5] = Time.time;

					torpedoImpactReducedVolumeClip.Play();
					Destroy(GameObject.FindGameObjectWithTag("obj5"));
					dest [5] = 1;
				}
				if (hit.collider.tag == ("obj6") && dest[6]==0)
				{
					hits++;
					positions [6] = obj [6].transform.position;
					timeDest [6] = Time.time;

					torpedoImpactReducedVolumeClip.Play();
					Destroy(GameObject.FindGameObjectWithTag("obj6"));
					dest [6] = 1;
				}
				if (hit.collider.tag == ("obj7") && dest[7]==0)
				{
					hits++;
					positions [7] = obj [7].transform.position;
					timeDest [7] = Time.time;

					torpedoImpactReducedVolumeClip.Play();
					Destroy(GameObject.FindGameObjectWithTag("obj7"));
					dest [7] = 1;
				}
				if (hit.collider.tag == ("obj8") && dest[8]==0)
				{
					hits++;
					positions [8] = obj [8].transform.position;
					timeDest [8] = Time.time;

					torpedoImpactReducedVolumeClip.Play();
					Destroy(GameObject.FindGameObjectWithTag("obj8"));
					dest [8] = 1;
				}
				if (hit.collider.tag == ("obj9") && dest[9]==0)
				{
					hits++;
					positions [9] = obj [9].transform.position;
					timeDest [9] = Time.time;

					torpedoImpactReducedVolumeClip.Play();
					Destroy(GameObject.FindGameObjectWithTag("obj9"));
					dest [9] = 1;
				}

			}

			nextTime = Time.time + diff;
		}

		for (i = 0; i < 10; i++) {
			winFlag = winFlag & dest [i];
			if (i == 9) {
				doneFlag = 1;
			}
		}

		if (doneFlag == 1 && winFlag == 1) {
			doneFlag = 0;
			healthtxt.text = "You Win !!";
			Application.LoadLevel(4);
		}

		if(Input.GetMouseButtonDown(0) && flag == 0)
		{
			//healthtxt.text = "";

			shots++;

			torpedoLaunchClip.Play();

			flag = 1;
			ray = new Ray(transform.position, Camera.main.transform.forward * maxRayDist);

			nextTimeShot = Time.time + diff;

			rayVisibleNextTime = Time.time + 0.5f;
			rayVisibleFlag = 1;

			line.SetPosition(0, sub.transform.position);
			line.SetPosition(1, Camera.main.transform.forward * maxRayDist + sub.transform.position);
			Material sample = new Material(Shader.Find("Unlit/Texture"));

			flagShot = 1;

			line.material = sample;
			line.SetColors(Color.black, Color.black);
			line.SetWidth(0.2f, 0.2f);
			line.enabled = true;

		}

		if (sub.position.y >= 1)
		{
			sub.velocity = Vector3.zero;
			sub.MovePosition(new Vector3(sub.transform.position.x, 0.5f, sub.transform.position.z));
		}

		if (sub.position.y >= 0)
		{
			for (i = 0; i < 10; i++) {
				if (dest[i] == 0) {
					obj[i].GetComponent<MeshRenderer> ().enabled = false;
				}
			}
		}
		else
		{
			for (i = 0; i < 10; i++) {
				if (dest[i] == 0) {
					obj[i].GetComponent<MeshRenderer> ().enabled = true;
				}
			}
		}


		if (Input.GetKey(KeyCode.UpArrow) && ctrlFlag == 0)
		{
			sub.AddForce(Camera.main.transform.forward.normalized * speed, ForceMode.Force);
		}
		if (Input.GetKey(KeyCode.DownArrow) && ctrlFlag == 0)
		{
			sub.AddForce(-Camera.main.transform.forward.normalized * speed, ForceMode.Force);
		}

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			sub.AddForce(Quaternion.Euler(0f, -90f, 0f) * Camera.main.transform.forward.normalized * speed, ForceMode.Force);
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			sub.AddForce(Quaternion.Euler(0f, 90f, 0f) * Camera.main.transform.forward.normalized * speed, ForceMode.Force);
		}

		if (Input.GetMouseButton(1))
		{
			ctrlFlag = 1;
			if (Input.GetKey(KeyCode.DownArrow))
			{
				sub.AddForce(down * speed, ForceMode.Force);
			}

			if (Input.GetKey(KeyCode.UpArrow))
			{
				if (sub.position.y >= 1)
				{
					sub.velocity = Vector3.zero;
				}
				else
				{
					sub.AddForce(up * speed, ForceMode.Force);
				}
			}
		}
		else
		{
			ctrlFlag = 0;
		}

		if (Time.time >= nextTimeShot && flag==1) {
			flag = 0;
		}

		var = Time.time;


		if (rayVisibleFlag == 1 && Time.time >= rayVisibleNextTime)
		{
			rayVisibleFlag = 0;
			line.enabled = false;
		}

		if (sub.transform.eulerAngles.x != 90)
		{
			Destroy(sub);
			healthtxt.text = "!! :/ :/ :/ !!";
			Application.LoadLevel(3);
			nextLevelFlag = 1;
			nextLevelNextTime = Time.time + 5;
			disorientflag = 1;
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
			sw.WriteLine("wrong hits = " + misses);
			sw.WriteLine("Health remaining : " + time);
			sw.WriteLine("");

			for (i = 0; i < counter; i++) {
				sw.WriteLine(positions[counter]);
			}
			sw.WriteLine("");

			if (time-var <= 0)
			{
				sw.WriteLine("player destroyed ");
				if (disorientflag == 1)
				{
					sw.WriteLine("because of collision with obstacles resulting in disorientation");
				}
				if (disorientflag == 0)
				{
					sw.WriteLine("because of time-out");
				}
				sw.WriteLine("at position " + transform.position);
				sw.WriteLine("at time " + var);
			}
			else
			{
				sw.WriteLine("player survived ");
				sw.WriteLine("time taken " + var);
			}

			for(i=0;i<10;i++){
				if(dest[i]==1){
					sw.WriteLine("object " +i+" destroyed ");
					sw.WriteLine("at position " + positions[i]);
					sw.WriteLine("at time " + timeDest[i]);
				}
				else
				{
					sw.WriteLine("object " +i+" survived");
				}
			}
			
		}
	}
}



