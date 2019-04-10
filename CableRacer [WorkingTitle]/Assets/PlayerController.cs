using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public GameObject HitLane;
	public GameObject HitTile;
	public GameObject NextLane;
	public GameObject NextTile;

	[SerializeField] private ProceduralGenerator _proceduralGenerator;

	public bool isHit;
	public int CurrentTileId = 0;

	public Transform startMarker;
	public Transform endMarker;

	public GameObject CameraRig;

	public float Angle = 0;

	public float mySpeed = 0.5f;
	public float startTime;

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

		ButtonInput();

		// Set our position as a fraction of the distance between the markers.
		transform.position = Vector3.Lerp(startMarker.position, endMarker.position, timer);
		//transform.position = new Vector3(CameraRig.transform.position.x, -4.3f, CameraRig.transform.position.z);
		Vector3 relativePos = startMarker.position - endMarker.position;

		var newRotation = Quaternion.LookRotation(relativePos, Vector3.up).eulerAngles;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newRotation), Time.deltaTime);
		//transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Angle);
		//transform.RotateAround(transform.position, transform.forward, Time.time * 30f);
		transform.RotateAround(transform.position, transform.forward, Angle);

		if (timer >= 1f)
		{
			CurrentTileId++;
			startMarker = _proceduralGenerator.SpawnedTiles[CurrentTileId].transform;
			endMarker = _proceduralGenerator.SpawnedTiles[CurrentTileId + 1].transform;
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

		void ButtonInput()
		{
			if (Input.GetKey(KeyCode.A))
			{
				JumpLeft();
			}

			if (Input.GetKey(KeyCode.D))
			{
				JumpRight();
			}
		}

		void JumpLeft()
		{
			var time = Time.time;
			float jumpTimer = (Time.time - time) * 2f;

			Angle = Mathf.Lerp(Angle, Angle - 5f, Time.fixedDeltaTime * 20);
		}

		void JumpRight()
		{
			var time = Time.time;
			float jumpTimer = (Time.time - time);

			Angle = Mathf.Lerp(Angle, Angle + 5f, Time.fixedDeltaTime * 20);
		}
	}
}
