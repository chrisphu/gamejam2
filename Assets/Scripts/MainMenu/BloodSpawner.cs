using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSpawner : MonoBehaviour
{
    public GameObject game_area;
    public GameObject death_area;
    public GameObject blood_prefab;

    public int blood_count = 0;
    public int blood_limit = 50;
    public int blood_per_frame = 1;

    public float spawn_circle_radius = 10.0f;
    public float death_circle_radius = 10.0f;

    public float fastest_speed = 75.0f;
    public float slowest_speed = 50.0f;

    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        MaintainPopulation();
    }

    void MaintainPopulation()
    {
        if (blood_count < blood_limit)
        {
            for(int i = 0; i < blood_per_frame; i++)
            {
                Vector3 position=GetRandomStartPosition();
                SCRBloodCell new_cell = AddCell(position);
            }
        }
    }

    Vector3 GetRandomStartPosition()
    {
        Vector3 position = Random.insideUnitCircle;
        position += game_area.transform.position;
        return position;
    }

    Vector3 GetRandomEndPosition()
    {
        Vector3 position = Random.insideUnitCircle;
        position += death_area.transform.position;
        return position;
    }

    SCRBloodCell AddCell(Vector3 position)
    {
        blood_count += 1;
        GameObject new_cell = Instantiate(
            blood_prefab,
            position,
            Quaternion.FromToRotation(Vector3.up, GetRandomEndPosition()),
            gameObject.transform
        );

        SCRBloodCell cell_script = new_cell.GetComponent<SCRBloodCell>();
        cell_script.blood_spawner = this;
        cell_script.game_area = game_area;
        cell_script.death_area = death_area;
        cell_script.speed=Random.Range(slowest_speed,fastest_speed);

        return cell_script;

    }
}
