using UnityEngine;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class DataAccessLayer {
	[DllImport("__Internal")]
	private static extern void SyncFiles();

	[DllImport("__Internal")]
	private static extern void WindowAlert(string message);

	// Use this for initialization
	public static void Save(AppDetails appDetails)
	{
		string dataPath = string.Format("{0}/AppDetails.dat",Application.persistentDataPath);
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream fileStream;

		try
		{
			if (File.Exists(dataPath))
			{
				File.WriteAllText(dataPath, string.Empty);
            	fileStream = File.Open(dataPath, FileMode.Open);
			}
			else
			{
				fileStream = File.Create(dataPath);
			}

			binaryFormatter.Serialize(fileStream, appDetails);
        	fileStream.Close();

			if (Application.platform == RuntimePlatform.WebGLPlayer)
        	{
            	SyncFiles();
        	}

		}
		catch (Exception e)
    	{
        	PlatformSafeMessage("Failed to Save: " + e.Message);
    	}
	}

	public static AppDetails Load()
	{
		AppDetails appDetails = null;
		string dataPath = string.Format("[{0}/AppDetails.dat", Application.persistentDataPath);

		try
		{
			if(File.Exists(dataPath))
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				FileStream fileStream = File.Open(dataPath,FileMode.Open);

				appDetails = (AppDetails)binaryFormatter.Deserialize(fileStream);
				fileStream.Close();
			}
		}
		catch (Exception e)
    	{
        	PlatformSafeMessage("Failed to Load: " + e.Message);
    	}

		return appDetails;

	}
	private static void PlatformSafeMessage(string message)
	{
    	if (Application.platform == RuntimePlatform.WebGLPlayer)
    	{
        	WindowAlert(message);
    	}
    	else
    	{
        	Debug.Log(message);
    	}
	}
}
