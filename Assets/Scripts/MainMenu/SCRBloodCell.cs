using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRBloodCell : MonoBehaviour
{
    public BloodSpawner blood_spawner;
    public GameObject game_area;

    public float speed;

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        transform.position += transform.up * (Time.deltaTime * speed);
        float distance = Vector3.Distance(transform.position, game_area.transform.position);

        if (distance > blood_spawner.death_circle_radius)
        {
            RemoveCell();

        }
    }
    void RemoveCell()
    {
        blood_spawner.blood_count -= 1;
        Destroy(gameObject);
        
    }
}
