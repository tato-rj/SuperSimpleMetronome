using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class DataManager {

	public void Save (float input) {

		BinaryFormatter binaryFormatter = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/metronome_speed.dat");
		UserSpeed data = new UserSpeed ();

		data.speed = input;

		binaryFormatter.Serialize (file, data);
		file.Close ();
	}

	public float Load () {

		if (File.Exists (Application.persistentDataPath + "/metronome_speed.dat")) {

			BinaryFormatter binaryFormatter = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/metronome_speed.dat", FileMode.Open);
			UserSpeed data = (UserSpeed)binaryFormatter.Deserialize (file);
			file.Close ();

			return data.speed;

		} else {
			return 80f;
		}
	}
}


[Serializable]
class UserSpeed {

	public float speed;

}