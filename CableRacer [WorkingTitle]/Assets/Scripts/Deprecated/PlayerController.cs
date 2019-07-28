using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public GameObject HitLane;
	public GameObject HitTile;
	public GameObject NextLane;
	public GameObject NextTile;

	[SerializeField] private ProceduralGenerator _proceduralGenerator;

	[SerializeField] private TriggerDetection _triggerLeft;
	[SerializeField] private TriggerDetection _triggerRight;

	public bool isHit;
	public int CurrentTileId = 0;

	public Transform startMarker;
	public Transform endMarker;

	public GameObject CameraRig;

	public float Angle = 0;
	public float AngleSpeed = 60f;
	public int TurnSpeed;

	public float mySpeed = 0.5f;
	public float startTime;

	public bool isKurve = false;
	public float kurveSpeedMultiplyer = 2f;
	private float kurveStartTime;

	public void Start()
	{
		startMarker = _proceduralGenerator.SpawnedTiles[CurrentTileId].transform;
		endMarker = _proceduralGenerator.SpawnedTiles[CurrentTileId + 1].transform;

		startTime = Time.time;
	}
	public void Update()
	{
		// Distance moved = time * speed.
		float timer = (Time.time - startTime) * mySpeed;

		ControllerInput();

		// Set our position as a fraction of the distance between the markers.
		transform.position = Vector3.Lerp(startMarker.position, endMarker.position, timer);
		//transform.position = new Vector3(CameraRig.transform.position.x, -4.3f, CameraRig.transform.position.z);
		Vector3 relativePos = startMarker.position - endMarker.position;

	
		if (isKurve)
		{
			float timerKurve = (Time.time - kurveStartTime) * mySpeed * kurveSpeedMultiplyer;

			var newRotation = Quaternion.LookRotation(relativePos, Vector3.up).eulerAngles;
			Debug.Log("changed"+ newRotation);

			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newRotation), timerKurve);
			//transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
			transform.RotateAround(Vector3.zero, transform.forward, Angle);

			if (timerKurve >= 1)
			{
				isKurve = false;
			}
		} else
		{
			transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
			transform.RotateAround(transform.position, transform.forward, Angle);
		}





		//transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Angle);
		//transform.RotateAround(transform.position, transform.forward, Time.time * 30f);
		//Debug.Log("angle = " + Angle);



		if (timer >= 1f)
		{
			CurrentTileId++;
			startMarker = _proceduralGenerator.SpawnedTiles[CurrentTileId].transform;
			endMarker = _proceduralGenerator.SpawnedTiles[CurrentTileId + 1].transform;
			TileContainer newTile = _proceduralGenerator.SpawnedTiles[CurrentTileId].GetComponent<TileContainer>();
			if (newTile.Angle != 0f)
			{
				Debug.Log("Kurve");
				isKurve = true;
				kurveStartTime = Time.time;
			}
			startTime = Time.time;
		}

		/*
		RaycastHit hit;

		if (Physics.Raycast(transform.position,
			Vector3.down,
			out hit))
		{
			isHit = true;
			HitLane = hit.transform.gameObject;
			HitTile = HitLane.transform.parent.transform.parent.gameObject;
			CurrentTileId = HitTile.GetComponent<TileContainer>().Id;
			Debug.Log("hitlane: " + HitLane.name + "hitTile:" + HitTile.name);
		}

		if (HitLane != null)
		{
			var tileTrigger = HitTile.GetComponentInChildren<TriggerDetection>();

			if (tileTrigger.IsTriggered)
			{
				CurrentTileId++;
			}

			//transform.position = Vector3.Lerp(_proceduralGenerator.SpawnedTiles[CurrentTileId].transform.position, _proceduralGenerator.SpawnedTiles[CurrentTileId +].transform.position, Time.time);


		}
		*/

		void ControllerInput()
		{
			if (_triggerLeft.IsTriggered)
			{
				Debug.Log("left == right");
				Angle += AngleSpeed * Time.deltaTime;

			}

			if (_triggerRight.IsTriggered)
			{
				Debug.Log("right == left");
				Angle -= AngleSpeed * Time.deltaTime;

			}


			/*
			if (_triggerLeft.IsTriggered)
			{
				JumpLeft();
			}

			else
			{
				Stay();
			}

			if (_triggerRight.IsTriggered)
			{
				JumpRight();
			}

			else
			{
				Stay();
			}
			*/
		}
		/*
		void JumpLeft()
		{
			var time = Time.time;
			float jumpTimer = (Time.time - time) * 100f;

			var newAngle = Angle - TurnSpeed;

			if(newAngle < -1f)
			{
				newAngle = -1f;
			}

			Angle = Mathf.Lerp(Angle, newAngle, Time.fixedDeltaTime * 10000);
		}

		void JumpRight()
		{
			var time = Time.time;
			float jumpTimer = (Time.time - time) * 100f;

			var newAngle = Angle + TurnSpeed;

			if (newAngle > 1f)
			{
				newAngle = 1f;
			}

			Angle = Mathf.Lerp(Angle, newAngle, Time.fixedDeltaTime * 10000);
		}

		void Stay()
		{
			var oldAngle = Angle;
			Angle = oldAngle;
		}
		*/
	}
}
