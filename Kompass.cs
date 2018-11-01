using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kompass : MonoBehaviour {

	public Text headingAccuracyText, magneticHeadingText, trueHeadingText, timeStamp, spider;
	public string insect;
	public void Awake()
	{
		StartCoroutine(InitializeLocation());
	}
	public IEnumerator InitializeLocation()
	{
		// First, check if user has location service enabled
		if (!Input.location.isEnabledByUser)
		{
			Debug.Log("location disabled by user");
			insect = "location disabled by user";
			yield break;
		}
		// enable compass
		Input.compass.enabled = true;
		// start the location service
		Debug.Log("start location service");
		insect = "start location service";
		Input.location.Start(10, 0.01f);
		// Wait until service initializes
		int maxSecondsToWaitForLocation = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxSecondsToWaitForLocation > 0)
		{
			yield return new WaitForSeconds(1);
			maxSecondsToWaitForLocation--;
		}
		
		// Service didn't initialize in 20 seconds
		if (maxSecondsToWaitForLocation < 1)
		{
			Debug.Log("location service timeout");
			insect = "location service timeout";
			yield break;
		}
		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed)
		{
			Debug.Log("unable to determine device location");
			insect = "unable to determine device location";
			yield break;
		}
		Debug.Log("location service loaded");
		insect = "location service loaded";
		yield break;
	}
	
	// Update is called once per frame
	void Update () {
		headingAccuracyText.text = "Accuracy of heading reading in degrees.: " + Input.compass.headingAccuracy.ToString();
		magneticHeadingText.text = "The heading in degrees relative to the magnetic North Pole.: " + Input.compass.magneticHeading;
		trueHeadingText.text = "The heading in degrees relative to the geographic North Pole. " + Input.compass.trueHeading;
		timeStamp.text = Input.compass.timestamp.ToString();
		//insect = Input.compass.enabled.ToString();
		spider.text = insect;
		//Debug.Log(Input.compass.enabled);
	}
}
