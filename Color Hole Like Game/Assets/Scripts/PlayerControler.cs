using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControler : MonoBehaviour
{
    //for keyboard play
	public float scale;
	public float speed;
	public float radius;

	public Transform visuals;
	public Transform directionRing;

	public SphereCollider detector;
	public BoxCollider[] colliders;
	public List<Rigidbody> victims;
	
	public float maxDragDistance = 40f;
	private float curDragDistance;
	private Vector3 moveDirection;
	private Vector3 dragStartPos;

	private void Awake()
	{
		GameManager.gameManager.playerControler = this;
	}

	private void Start()
	{
		SetScale();
	}

	private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
		if (Input.GetMouseButtonDown(0))
		{
			dragStartPos = mousePos;
            return;
		}
		else if (Input.GetMouseButton(0))
		{
			curDragDistance = (mousePos - dragStartPos).magnitude;

			if (curDragDistance > maxDragDistance)
			{
				dragStartPos = mousePos - moveDirection * maxDragDistance;
				curDragDistance = (mousePos - dragStartPos).magnitude;
			}

            moveDirection = (mousePos - dragStartPos).normalized;
			var direction = new Vector3(moveDirection.x, 0, moveDirection.y);
			transform.position += direction * speed * curDragDistance * Time.deltaTime;
			directionRing.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(new Vector3(90, 0, 0));
		}

		var nearbyObjects = Physics.OverlapSphere(transform.position, radius);
		foreach(var nearbyObject in nearbyObjects)
		{
			if (nearbyObject.gameObject.layer == 9)
			{
				return;
			}

			Rigidbody nearbyObjectRb = nearbyObject.GetComponentInParent<Rigidbody>();
			if (!nearbyObjectRb || victims.Contains(nearbyObjectRb))
			{
				continue;
			}
			else
			{
				victims.Add(nearbyObjectRb);
				nearbyObject.gameObject.layer = 10;
				nearbyObjectRb.isKinematic = false;
			}
		}
	}
    

	private void FixedUpdate()
	{
        if (!GameManager.gameManager.started || GameManager.gameManager.gameOver)
        {
            return;
        }

        foreach (var victim in victims)
		{
			victim.AddForce(Vector3.down * scale * GameManager.gameManager.gravity * Time.fixedDeltaTime, ForceMode.VelocityChange);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 10) // Victims fall into the hole
		{
            GameManager.gameManager.menuUIManager.AddScore(1);
		}

	}

	private void OnTriggerExit(Collider other)
	{
		if (other.transform.position.y < 0) // Victims falls through the hole
		{
			other.gameObject.SetActive(false);

			Rigidbody victimRb = other.GetComponent<Rigidbody>();
			if (victimRb)
			{
				victims.Remove(victimRb);
			}
		}
	}

	public void SetScale()
	{
		visuals.localScale = new Vector3(scale, scale, scale);
		visuals.localPosition = new Vector3(0, -scale / 2f - 0.49f, 0);
		detector.center = new Vector3(0, -1f - scale / 2f, 0);
		detector.radius = scale / 2f;		

		foreach (var coll in colliders)
		{
			var direction = coll.center.normalized * 50.025f;
			coll.center = direction * (1 + scale / 100f);
		}

	}

	
}