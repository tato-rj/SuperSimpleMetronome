using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Metronome : MonoBehaviour {

	public static Metronome instance;

	public Camera screen;
	public GameObject input;
	public GameObject slider;
	public bool metronomeOn;
	public int subdivision = 1;

	private Animation pulse;
	private AudioSource click;
	private Dictionary<string, int> range;

	void Awake()
	{
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
		} else {
			instance = this;
		}

		metronomeOn = false;
		pulse = input.GetComponent<Animation> ();
		click = GetComponent<AudioSource> ();
		range = new Dictionary<string, int> ();
		range ["min"] = 40;
		range ["max"] = 220;
		slider.GetComponent<Slider>().minValue = range ["min"];
		slider.GetComponent<Slider>().maxValue = range ["max"];
	}

	void Start()
	{
		StartCoroutine (Clicks ());
	}

	IEnumerator Clicks()
	{
		yield return new WaitForSeconds (0.5f);
		pulse.Play ();

		while (true) {
			float interval = 60/slider.GetComponent<Slider>().value;

			click.Play ();

			for (int i = 1; i < subdivision; i++) {
				yield return new WaitForSeconds (interval/subdivision);
				click.Play ();				
			}
		
			yield return new WaitForSeconds (interval/subdivision);

			pulse.Play ();
		}
	}

}
