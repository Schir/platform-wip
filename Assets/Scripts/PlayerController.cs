using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private float speed;
	private bool canJump;
	private Vector3 jump = new Vector3 (0.0f, 300, 0.0f);
	private Vector3 nextPosition;
	public int jewels;
	public bool showJewelText;
	private double textTimerMax = 400;
	public double textTimerCurrent = 0;
	private Vector3 screenPos;
	private Rect TextArea = new Rect(0, 0, 400, 80);
	private Vector3 lastForwardMove;

    private Rigidbody rb;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
		speed = 6;
		canJump = true;
		nextPosition = new Vector3(0.0f, 0.0f, 0.0f);
		jewels = 0;
		showJewelText = false;
		lastForwardMove = new Vector3(1.0f, 1.0f, 1.0f);
    }
	void jumpNow()
	{
		if(canJump)
		{
			rb.AddForce(jump);
			canJump = false;
		}
		else
		{
			//do nothing
		}
	}
	Vector3 PlaceFree(Vector3 check)
	{
		if(rb.transform.position - check == gameObject.transform.position)
		{
			return Vector3.zero;
		}
		else
		{
			return check;
		}
	}


    void Update ()
    {
		RaycastHit hit;
        float moveHorizontal = Input.GetAxis ("Horizontal");
		if(moveHorizontal > 0.02f || moveHorizontal < 0.02f);
		{
			lastForwardMove = new Vector3(moveHorizontal, 0.0f, 0.0f);
			lastForwardMove.Normalize();
		}
        float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		movement.Normalize();
		/*if(movement != new Vector3(0.0f, 0.0f, 0.0f))
		{
			lastForwardMove = movement;
		}*/
		if(Input.GetKeyDown(KeyCode.Space))
		{
			jumpNow();
		}

		if(Physics.Raycast(rb.transform.position, Vector3.down, out hit, 0.5f))
		{
			canJump = true;
		}
		if(Input.GetKeyDown(KeyCode.Z))
		{
			attackPlease(rb.transform.position - (lastForwardMove));
		}
	
		nextPosition  = (movement * speed * Time.deltaTime);
		nextPosition = PlaceFree(nextPosition);
        rb.MovePosition(rb.transform.position - nextPosition);

    }
	void attackPlease(Vector3 direction)
	{
		RaycastHit hit2;
		if (Physics.BoxCast(/*rb.transform.position*/ direction, new Vector3 (0.5f, 0.5f, 0.5f), direction, out hit2, new Quaternion(0,0,0,0), 80.0f))//(Physics.Raycast(rb.transform.position, /* rb.transform.position +*/ (direction * 10), out hit2, 80.0f))
		{
			if(hit2.collider.tag == "enemy")
			{
				if(hit2.rigidbody)
				{
					hit2.rigidbody.AddForce(direction * 20);
				}
			}
		}
		
	}

	void OnGUI()
	{
		if(showJewelText)
		{
			screenPos = Camera.main.WorldToScreenPoint(transform.position) + (Vector3.up*3);
			TextArea = new Rect(screenPos.x-100, screenPos.y-100, screenPos.x+100, screenPos.y + 50);
			if(textTimerCurrent > 0)
			{
				GUI.Label(TextArea, "Boy, I sure can't wait to sell this jewel\nat the playground so the kids can\n       smoke hard nicotine!");
				textTimerCurrent -= 1;
			}
			else
			{
				showJewelText = false;
			}
		}

	}


	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "item" || col.gameObject.tag == "Jewel")
		{
			jewels++;
			showJewelText = true;
			textTimerCurrent = textTimerMax;
			Destroy(col.gameObject);
		}
		if(col.gameObject.tag == "water")
		{
			jump = new Vector3 (0.0f, 60, 0.0f);
		}
		if(col.gameObject.tag != "water")
		{
			jump = new Vector3 (0.0f, 300, 0.0f);
		}
	}

}
/* 
public class Attack : Collider
{
	private int timer;
	void Start()
	{
		timer = 60;
		isTrigger = true;

	}
	void Update()
	{
		if(timer > 0)
		{
			timer --;
		}
		else
		{
			Destroy(this);
		}
	}
	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "enemy")
		{
			Destroy(col.gameObject);
		}
		else
		{

		}
	}
}*/