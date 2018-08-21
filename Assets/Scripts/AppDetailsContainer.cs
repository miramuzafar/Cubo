using UnityEngine;

public class AppDetailsContainer : MonoBehaviour {

	public static AppDetails LoadAppDetails = null;

	void Start()
	{
		DontDestroyOnLoad(this);
	}
}
