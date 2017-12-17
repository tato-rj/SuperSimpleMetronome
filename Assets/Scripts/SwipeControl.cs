using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeControl : MonoBehaviour {

	public Camera screen;
	public Text speedLabel;
	public Slider speed;
	public AudioSource click;
	public GameObject muteIcon;
	public GameObject subdivisionLabel;

	private Text subdivisionText;
	private DataManager dataManager;
	private float initSpeed;
	private Touch initTouch;
	private bool moved;
	private float limit = 20f;

	void Awake()
	{
		dataManager = new DataManager();
		speed.value = initSpeed = (int)dataManager.Load();
		speedLabel.text = initSpeed.ToString ();
		screen.backgroundColor = NewColor (speed.value);
		subdivisionText = subdivisionLabel.GetComponentInChildren<Text> ();
	}

	void Update()
	{
		foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Began) {
				initTouch = touch;
				moved = false;
			} else if (touch.phase == TouchPhase.Moved) {
				float xMoved = initTouch.position.x - touch.position.x;
				float yMoved = initTouch.position.y - touch.position.y;
				float distance = Mathf.Sqrt ((xMoved * xMoved) + (yMoved * yMoved));
				bool swipedLeft = Mathf.Abs (xMoved) > Mathf.Abs (yMoved);

				if (distance > limit) {
					if (swipedLeft && xMoved > 0) {
						//SWIPE LEFT
					} else if (swipedLeft && xMoved < 0) {
						//SWIPE RIGHT
					} else if (!swipedLeft && yMoved < 0) {
						//SWIPE UP
						UpdateSpeed (distance);
					} else if (!swipedLeft && yMoved > 0) {
						//SWIPE DOWN
						UpdateSpeed (-distance);
					}
					moved = true;
				}
			} else if (touch.phase == TouchPhase.Ended) {
				if (touch.tapCount == 2) {
					SubdivideBeats ();
				}

				initSpeed = speed.value;
				dataManager.Save (speed.value);
			}
		}

	}

	void UpdateSpeed(float increment)
	{
		speed.value = Mathf.RoundToInt (initSpeed + (increment/8));
		speedLabel.text = speed.value.ToString();
		screen.backgroundColor = NewColor (speed.value);		
	}

	protected Color NewColor(float input)
	{
		float colorValue = (input - 40) / 180;
		return new Color(
			colorValue,
			colorValue/2,
			238f);	
	}

	protected void SubdivideBeats()
	{
		int count = Metronome.instance.subdivision;
		subdivisionText.text = count.ToString ()+"x";

		if (count < 3) {
			Metronome.instance.subdivision += 1;
			subdivisionLabel.SetActive (true);
		} else {
			Metronome.instance.subdivision = 1;
			subdivisionLabel.SetActive (false);
		}
	}
}
