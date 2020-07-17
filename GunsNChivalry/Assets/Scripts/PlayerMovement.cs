using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 0f;
    private Vector2 moveVec;
    private int currentRoom = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 inputVec = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVec = inputVec.normalized * moveSpeed;

        transform.up = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
    }

    private void FixedUpdate()
    {
        rb.position += moveVec * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Room"))
        {
            int index = RoomsTemplates.instance.roomsSpawned.IndexOf(c.gameObject);

            if (index != currentRoom)
            {
                currentRoom = index;
                Camera.main.transform.position = new Vector3(c.transform.position.x, c.transform.position.y , -10f);
            }
        }
    }
}
