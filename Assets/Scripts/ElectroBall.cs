using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroBall : MonoBehaviour
{
    public GameObject Paddle;
    public GameObject Goal;
    public float MovementSpeed = 10;

    void Start()
    {
        Paddle = GameObject.FindGameObjectWithTag("Paddle");
        Goal = GameObject.FindGameObjectWithTag("ElectroGoal");
    }
    // Update is called once per frame
    void Update()
    {

        Vector2 TheMovement = Vector3.MoveTowards(transform.position, Goal.transform.position, MovementSpeed * Time.deltaTime);
        transform.position = TheMovement;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Paddle")
        {
            ElectricSpawner.instance.ChargeBar();
            Destroy(gameObject);
        }
        if (other.tag == "ElectroGoal")
        {
            ElectricSpawner.instance.UnChargeBar();
            Destroy(gameObject);
        }
    }
}
