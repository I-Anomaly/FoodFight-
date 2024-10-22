using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{        
    public Transform grabPoint;  // Reference to the grab point on the player
    public float grabRange = 2f; // Distance the player can grab food
    private GameObject grabbedFood; // Currently held food
    public float throwForce = 10f; 

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))  // Press 'E' to grab food
        {
            if (grabbedFood == null)  // If not holding food, try to grab it
            {
                TryGrabFood();
            }
            else  // If already holding food, drop or throw it
            {
                ThrowFood();
            }
        }
    }


    void TryGrabFood()
{
    Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, grabRange);
    foreach (var hitCollider in hitColliders)
    {
        if (hitCollider.CompareTag("Food"))
        {
            grabbedFood = hitCollider.gameObject;
            grabbedFood.transform.position = grabPoint.position;
            grabbedFood.transform.parent = grabPoint;

            Rigidbody2D foodRb = grabbedFood.GetComponent<Rigidbody2D>();
            Collider2D foodCol = grabbedFood.GetComponent<Collider2D>();
            if (foodRb != null)
            {
                foodRb.isKinematic = true; 
                foodCol.
                foodRb.velocity = Vector2.zero; // Ensure no residual forces
            }

            Rigidbody2D playerRb = GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }

            break;
        }
    }
}

void ThrowFood()
{
    if (grabbedFood != null)
    {
        Rigidbody2D foodRb = grabbedFood.GetComponent<Rigidbody2D>();
        if (foodRb != null)
        {
            foodRb.isKinematic = false;
        }
        grabbedFood.transform.parent = null;
        Vector2 throwDirection = new Vector2(transform.localScale.x, 0).normalized;
        foodRb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
        grabbedFood = null;



}

}

}
