using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @kurtdekker - head nod yes sensor
//
// To use:
//	Put this on a mouse-controlled FPS camera and it can detect:
//		- up/down "head nods yes."
//	Optionally give it an audio to play
//
public class HeadNodYesSensor : MonoBehaviour
{
	public AudioSource AudioYes;

	// how many up-downs until we say "you nodded"
	const float NodCountRequired = 6;

	// how much up/down constitutes half of a nod
	const float NodAngularRequirement = 3;

	// each phase of "nod" only latches for this long
	const float NodTimingRequirement = 0.75f;

	// track if we think you are nodding
	float NodInProgress;

	float LastSignificantNodAngle;
	int LastDigitalNod;
	int NodCount;

	void UpdateNodYes()
	{
		// time out
		if (NodInProgress > 0)
		{

			NodInProgress -= Time.deltaTime;
			if (NodInProgress <= 0)
			{
				//Debug.Log( "Timed out - NOD YES");
				NodCount = 0;
				LastDigitalNod = 0;
			}
		}

		// how far up/down is your head?
		float forwardY = transform.forward.y;
		float angle = Mathf.Asin( forwardY) * Mathf.Rad2Deg;

		// quantize and study this nod
		int nod = 0;		// neutral, +1 is up, -1 is down
		if (angle < LastSignificantNodAngle - NodAngularRequirement)
		{
			//Debug.Log( "Down");
			nod = -1;
			LastSignificantNodAngle = angle;
		}
		else
		{
			if (angle > LastSignificantNodAngle + NodAngularRequirement)
			{
				//Debug.Log( "Up");
				nod = +1;
				LastSignificantNodAngle = angle;
			}
		}

		// we've gone up / down enough?
		if (nod != 0)
		{
			// and it was in a different direction than before
			if (nod != LastDigitalNod)
			{
				LastDigitalNod = nod;

				NodCount++;

				// reset timing, we think you might still be nodding
				NodInProgress = NodTimingRequirement;

				if (NodCount >= NodCountRequired)
				{
					NodCount = 0;

					GameManager.instance.isNodding = true;
					Invoke("ResetNod", 1.25f);
				}
			}
		}
	}

	void Update ()
	{
		UpdateNodYes();
	}

	void ResetNod()
	{
		GameManager.instance.isNodding = false;
	}
}