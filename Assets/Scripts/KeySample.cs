using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class KeySample : MonoBehaviour {
	[SerializeField]
	Text debugtext;
	[SerializeField]
	Text sensortext;
	[SerializeField]
	Text vibrationtext;

	public GameObject cube1;
	public GameObject gyrodebugcube;
	Quaternion initialAttitude;

	// Use this for initialization
	void Start () {
		//Activate Gyro sensor
		Input.gyro.enabled=true;
		//Avoid Sleep
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

	}

	// Update is called once per frame
	void Update () {
		DownKeyCheck ();

		//Gyro Acceleration
		//Display gyro and acceleration values
		sensortext.text ="Gyro"+"\n"+ "x:" + Input.gyro.attitude.eulerAngles.x.ToString ("0.0") + ", y:" + Input.gyro.attitude.eulerAngles.y.ToString ("0.0") + ", z:" + Input.gyro.attitude.eulerAngles.z.ToString ("0.0")+"\n";
		sensortext.text+="\n"+"Accel"+"\n"+"x:" +Input.acceleration.x.ToString ("0.0")+", y:" +Input.acceleration.y.ToString ("0.0")+", z:" +Input.acceleration.z.ToString ("0.0");

		//apply gyro value to gyrodebugcube transform
		gyrodebugcube.transform.rotation =Input.gyro.attitude;
		gyrodebugcube.transform.rotation*=Quaternion.Euler(-90f,0f,0f);
		gyrodebugcube.transform.rotation = new Quaternion (gyrodebugcube.transform.rotation.x,gyrodebugcube.transform.rotation.z,gyrodebugcube.transform.rotation.y,gyrodebugcube.transform.rotation.w);

	}
	void DownKeyCheck(){
		if (Input.anyKeyDown) {
			foreach (KeyCode code in Enum.GetValues(typeof(KeyCode))) {
				if (Input.GetKeyDown (code)) {
					Debug.Log (code);

					//show which key is pushed
					debugtext.text = code.ToString ();

					//activate vibration(So far it doesn't work)
					StartVibrate ();

					//apply random torque to  cube1
					cube1.GetComponent<ConstantForce> ().torque = new Vector3 (UnityEngine.Random.Range(-100,100),UnityEngine.Random.Range(-100,100),UnityEngine.Random.Range(-100,100));

					if (code.ToString () == "Menu") {
						//One-Finger Swipe: RightArrow,LeftArrow,UpArrow,DownArrow
						//Two-Finger Swipe BackSpace,Delete
						//One-Finger Hold: Menu
						//Two-Finger Tap: Escape
						// Not sure about One-Finger Tap...

						// do something here
					}

					break;
				}
			}
		}

	}

	public void StartVibrate(){

		if (SystemInfo.supportsVibration) {
			vibrationtext.text = "Vibrating";
			Handheld.Vibrate ();

		} else {
			vibrationtext.text = "Not available";
		}

	}
}
