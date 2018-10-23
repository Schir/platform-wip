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

    private Rigidbody rb;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
		speed = 6;
		canJump = true;
		nextPosition = new Vector3(0.0f, 0.0f, 0.0f);
		jewels = 0;
		showJewelText = false;
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
        float moveVertical = Input.GetAxis ("Vertical");
		if(Input.GetKeyDown(KeyCode.Space))
		{
			jumpNow();
		}

		if(Physics.Raycast(rb.transform.position, Vector3.down, out hit, 0.5f))
		{
			canJump = true;
		}
	

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		movement.Normalize();
		nextPosition  = ((movement * speed * Time.deltaTime));
		nextPosition = PlaceFree(nextPosition);
        rb.MovePosition(rb.transform.position - nextPosition);


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

