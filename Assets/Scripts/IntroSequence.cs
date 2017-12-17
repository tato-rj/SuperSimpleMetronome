using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSequence : MonoBehaviour {

	void Start()
	{
		StartCoroutine (ShowTitle());
	}

	IEnumerator ShowTitle()
	{
		foreach (Transform child in gameObject.transform) {
			child.GetComponent<Animation>().Play();
			yield return new WaitForSeconds (0.5f);
		}
		yield return new WaitForSeconds (0.5f);
		GetComponent<Animation> ().Play ();
		yield return new WaitForSeconds (0.5f);
		SceneManager.LoadScene ("main");

	}

}
