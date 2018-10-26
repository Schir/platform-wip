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
	private float elapsedTime;
	private float tempX;
	private float tempY;
	private float tempZ;
	private float xtarg;
	private float ytarg;
	private float ztarg;
	private float dx;
	private float dy;
	private float dz;
    private Rigidbody rb;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
		speed = 12;
		canJump = true;
		nextPosition = new Vector3(0.0f, 0.0f, 0.0f);
		jewels = 0;
		showJewelText = false;
		lastForwardMove = new Vector3(1.0f, 1.0f, 1.0f);
		elapsedTime = 0.0f;
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
	bool placeFree(Vector3 check)
	{

		if(rb.transform.position - check == gameObject.transform.position)
		{
			return false;
		}
		else
		{
			return true;
		}
		/* RaycastHit hitWall;
		if(Physics.Raycast(rb.transform.position, check, out hitWall, check.magnitude))
		{
			if(hitWall.collider.tag == "wall")
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		else
		{
			return true;
		}*/
	}
	/* float moveX(float x, float y, float z, float speed)
	{
		xtarg = x - speed;
		if(!placeFree(new Vector3(xtarg, y, z)))
		{
			return xtarg;
		}
		else
		{
			return 0;
		}
	}
	float moveY(float x, float y, float z, float speed)
	{
		ytarg = y + speed;
		if(placeFree(new Vector3(x, ytarg, z)))
		{
			return ytarg;
		}
		else
		{
			return 0;
		}
	}
	float moveZ(float x, float y, float z, float speed)
	{
		ztarg = z - speed;
		if(!placeFree(new Vector3(x, y, ztarg)))
		{
			return ztarg;
		}
		else
		{
			return 0;
		}
	}*/

    void Update ()
    {
		RaycastHit hit;
        float moveHorizontal = Input.GetAxis ("Horizontal");
		dx = moveHorizontal;
		if(moveHorizontal > 0.06f || moveHorizontal < -0.06f)
		{
			lastForwardMove = new Vector3(moveHorizontal, 0.0f, 0.0f);
			lastForwardMove.Normalize();
		}
        float moveVertical = Input.GetAxis ("Vertical");
		dz = moveVertical;
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
		//for(int i = 0; i < 6; i++)
		//	{
				if(placeFree(movement))
				{
				rb.transform.position = rb.transform.position - movement * Time.deltaTime * (speed / 12);
				}
				else
				{
					//
				}
		//	}
		
			//nextPosition = PlaceFree(nextPosition);
      
	}
	void attackPlease(Vector3 direction)
	{
		RaycastHit hit2;
		if (Physics.BoxCast(rb.transform.position /* direction*/, new Vector3 (1.0f, 0.5f, 1.0f), direction, out hit2, new Quaternion(0,0,0,0), 80.0f))//(Physics.Raycast(rb.transform.position, /* rb.transform.position +*/ (direction * 10), out hit2, 80.0f))
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
		screenPos = Camera.main.WorldToScreenPoint(transform.position) + (Vector3.up*3);
		TextArea = new Rect(screenPos.x-100, screenPos.y-100, screenPos.x+100, screenPos.y + 50);
		if(textTimerCurrent > 0)
		{
			if(showJewelText)
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
	void OnCollision(Collision col)
	{
		if(col.gameObject.tag == "wall")
		{
			rb.MovePosition(rb.transform.position + (Vector3.up * 8));
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

/*
Note to self:
rewrite the movement code. The update function should probably do something like:
movePlayer(position, speed)

nextMove(Vector3 check, speed)
{
	float x = check.x;
	float y = check.y
	float z = check.z
	for(int i = 0; i < 6; i++)
	{
		if(placeFree())
	}

}
fuck it. I'll go check what I wrote in the Hero Core experiment and see if I can translate that into 3D. 

okay here's what I might've done


Vector3 moveX(float x, float y, float z, float speed)
{
	xtarg = x + speed;
	if(place_free(new Vector3(xtarg, y, z)))
	{
		return new Vector3(xtarg, y, z);
	}
}
Vector3 moveY(float x, float y, float z, float speed)
{
	ytarg = y + speed;
	if(place_free(new Vector3(x, ytarg, z)))
	{
		return new Vector3(x, ytarg, z);
	}
}
Vector3 moveZ(float x, float y, float z, float speed)
{
	Ztarg = y + speed;
	if(place_free(new Vector3(x, ytarg, z)))
	{
		return new Vector3(x, ytarg, z);
	}
}

void Update()
{
	for(var i = 0; i < 6; i++)
	{
		if(place_free(x+(dx/6), y))
		{
			moveX(x, y, (dx/6));
		}
		if(place_free(x, y+(dy / 6)))
		{
			moveY(x, y, dy/6);
		}
}



 */