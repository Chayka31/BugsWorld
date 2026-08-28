using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera mainCamera; // Камера, из которой пускается луч
    public GameObject telo;
    private Juk_controller controller;
    private bool block_movement;
    private Rigidbody rb;

    public bool PlayerControlled;
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        telo = transform.GetChild(0).gameObject;
        controller = GetComponent<Juk_controller>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerControlled) 
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // Проверяем пересечение луча с коллайдерами
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.layer != 9) 
                {
                    Vector3 direction = hit.point - transform.position;
                    float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg; // Вычисляем угол поворота по оси Y

                    // Поворачиваем объект с заданной скоростью
                    float currentAngle = telo.transform.eulerAngles.y; // Текущий угол поворота по оси Y
                    float targetAngle = Mathf.MoveTowardsAngle(currentAngle, angle, controller._RotationSpeed * Time.deltaTime);
                    if (!block_movement)
                    {
                        telo.transform.rotation = Quaternion.Euler(0f, targetAngle, 0f); // Поворачиваем объект только по оси Y
                        transform.position = Vector3.MoveTowards(transform.position, new Vector3(hit.point.x, transform.position.y, hit.point.z), controller._Speed * Time.deltaTime);
                    }
                }
            }
        }
    }
    
    public void ThrowingAfterHit(Vector3 vec_coll) 
    {
        block_movement = true;
        Vector3 dir_throw = (transform.position - vec_coll).normalized;
        rb.AddForce(dir_throw * 15f, ForceMode.Impulse);
        Invoke("OffBlockMovement", 0.3f);
    }

    public void OffBlockMovement() 
    {
        block_movement = false;
        if (rb != null) 
        {
            rb.velocity = Vector3.zero;
        }
    }
}
