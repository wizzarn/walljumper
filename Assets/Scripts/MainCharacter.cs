using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour {


	private float oldGravityScale = 0;
	Vector2 movementVelocity = new Vector2(1.5f,2);
	Animator anim;
	public LayerMask obstacleMask;
	public AudioHandler audioHandler;
	public GameManager gameManager;
	Rigidbody2D rigidBody;
	bool movingAnim = false;
	float invinsibleTime = 3;
	float tmrInvinsibleTime=0;
	public int currentLife = 0;
	public int maxLife = 3;
	public bool isHurt = false;

	public enum CurrentStatus{
		Idle,
		Moving,
		Dead,
		Pause
	}
	public CurrentStatus currentStatus;
	public enum CurrentSide{
		DEFAULT,
		LEFT,
		RIGHT,
		UP,
		DOWN
	}
	public enum Actions{
		GoLeft,
		GoRight,
		GoUp,
		GoDown
	}
	Actions currentAction;
	void Start () {
		currentLife = maxLife;
		rigidBody = this.gameObject.GetComponent<Rigidbody2D> ();
		oldGravityScale = this.GetComponent<Rigidbody2D> ().gravityScale;
		anim = this.GetComponent<Animator> ();
	}
	public List<GameObject>livesIcons = new List<GameObject>();
	void Update () {
		if (currentStatus == CurrentStatus.Pause || currentStatus == CurrentStatus.Dead)
			return;
		Inputs();
		UpdateRayCasts ();
		UpdateAnimations ();
		InvinsibleTime ();
	}
	void InvinsibleTime(){
		if (!isHurt)
			return;
		tmrInvinsibleTime += Time.deltaTime;
		if (tmrInvinsibleTime > invinsibleTime) {
			tmrInvinsibleTime = 0;
			currentStatus = (rigidBody.velocity.x != 0 || rigidBody.velocity.y != 0) ? currentStatus = CurrentStatus.Moving : currentStatus = CurrentStatus.Idle;
			isHurt = false;
		}
			
	}
	void AnimationsLogic(){
		movingAnim = rigidBody.velocity.x != 0 || rigidBody.velocity.y != 0 ? true : false;
	}
	void UpdateAnimations(){
		AnimationsLogic ();
		anim.SetBool ("moving",movingAnim);
	}
	void MainActions(Actions action){
		switch (action){
			case Actions.GoLeft:
			rigidBody.velocity = new Vector2 (-movementVelocity.x,0);
			break;
			case Actions.GoRight:
			rigidBody.velocity = new Vector2 (movementVelocity.x,0);
			break;
			case Actions.GoUp:
			rigidBody.velocity = new Vector2 (0,movementVelocity.y);
			break;
			case Actions.GoDown:
			rigidBody.velocity = new Vector2 (0,-movementVelocity.y);
			break;
			default:
			break;
		}
		currentAction = action;
	}
	bool CanBeHurt(){
		if (currentStatus == CurrentStatus.Pause || currentStatus == CurrentStatus.Dead || isHurt)
			return false;
		return true;
	}
	public void GetHurt(int damage){
		if (!CanBeHurt ())
			return;
		isHurt = true;
		currentLife -= damage;
		if (currentLife < 0)
			currentLife = 0;
		
		foreach(GameObject liveIcon in livesIcons)
			liveIcon.SetActive (false);
		for (int i = 0; i < currentLife; i++)
			livesIcons [i].SetActive (true);
		
		if (currentLife <= 0) {
			currentStatus = CurrentStatus.Dead;
			currentLife = 0;
			SetGameOver ();
		}
	}
	void SetGameOver(){
		gameManager.SetGameOver ();
	}
	void Inputs(){
		//Go left
		if (Input.GetKeyUp(KeyCode.A)){
			MainActions (Actions.GoLeft);
		}
		//Go Right
		if (Input.GetKeyUp(KeyCode.D)){
			MainActions (Actions.GoRight);
		}
		// Go Up
		if (Input.GetKeyDown(KeyCode.W)){
			MainActions (Actions.GoUp);
		}
		// Go Down
		if (Input.GetKeyDown(KeyCode.S)){
			MainActions (Actions.GoDown);
		}
		currentStatus = CurrentStatus.Moving;

	}
	void UpdateRayCasts(){

		// LEFT
		float ray = 0.2f;
		float lastInputX = -1;
		float bodyRayDistanceLeft = lastInputX * -ray;
		float bodyRayDistanceRight = lastInputX * ray;

		// UP
		float bodyRayDistanceUp = lastInputX * ray;

		Vector3 transformPositionLeft = this.transform.position;
		transformPositionLeft.x -= .4f;

		Vector3 transformPositionRight = this.transform.position;
		transformPositionRight.x += .4f;

		Vector3 transformPositionUp = this.transform.position;
		transformPositionUp.y += .4f;

		RaycastHit2D bodyRayInfoRight = Physics2D.Raycast (transformPositionRight, Vector2.right * lastInputX, Mathf.Abs (bodyRayDistanceRight), obstacleMask);
		RaycastHit2D bodyRayInfoLeft = Physics2D.Raycast (transformPositionLeft, Vector2.right * lastInputX, Mathf.Abs (bodyRayDistanceLeft), obstacleMask);

		RaycastHit2D headRayInfo = Physics2D.Raycast (transformPositionUp, Vector2.up, ray, obstacleMask); // valor de distancia fue puesto al tanteo

		if (bodyRayInfoLeft.collider != null) {
			Debug.DrawRay (transformPositionLeft, new Vector3 (bodyRayDistanceLeft, 0, 0), Color.blue);
		} else {
			Debug.DrawRay (transformPositionLeft, new Vector3 (bodyRayDistanceLeft, 0, 0), Color.red);
		}

		if (bodyRayInfoRight.collider != null) {
			Debug.DrawRay (transformPositionRight, new Vector3 (bodyRayDistanceRight, 0, 0), Color.blue);
		} else {
			Debug.DrawRay (transformPositionRight, new Vector3 (bodyRayDistanceRight, 0, 0), Color.red);
		}

		if (headRayInfo.collider != null) {
			Debug.DrawRay (transformPositionUp, new Vector3 (0, bodyRayDistanceUp, 0), Color.blue);
			Vector3 currentVelocity = this.GetComponent<Rigidbody2D> ().velocity;
			if (currentVelocity.y > 0) {
				currentVelocity.y = 0;
				this.GetComponent<Rigidbody2D> ().velocity = currentVelocity;
			}
		} else {
			Debug.DrawRay (transformPositionUp, new Vector3 (0, bodyRayDistanceUp, 0), Color.red);
		}


	}
	void OnTriggerStay2D(Collider2D other){
	}

	void OnTriggerEnter2D(Collider2D other){
	}
	void OnTriggerExit2D(Collider2D other){
		
	}
}
