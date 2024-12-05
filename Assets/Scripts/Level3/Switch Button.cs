using System.Collections;
using System.Linq;
using UnityEngine;

public class SwitchButton : MonoBehaviour
{
    public bool active = true;
    private float cooldownDuration = 1.5f;
    [SerializeField] private GameObject[] walls;

    private void OnTriggerEnter(Collider other)
    {
        if (active)
        {
            for (int i = 0; i < walls.Count(); i++)
            {
                if (walls[i].GetComponent<Wall>().wallState.ToString() == "blue")
                {
                    walls[i].GetComponent<Wall>().wallState = Wall.State.orange;
                }
                else
                {
                    walls[i].GetComponent<Wall>().wallState = Wall.State.blue;
                }
            }
            StartCoroutine(ButtonCooldown(cooldownDuration));
        }
    }

    IEnumerator ButtonCooldown(float cooldown)
    {
        active = false;
        float time = 0f;
        float start = transform.position.y;
        float end = transform.position.y - 1;

        while (time < cooldown/2) 
        {
            float t = time / (cooldown/2);
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(start, end, t), transform.position.z);
            time += Time.deltaTime;
            yield return null;
        }

        time = 0f;

        while (time < cooldown/2)
        {
            float t = time / (cooldown / 2);
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(end, start, t), transform.position.z);
            time += Time.deltaTime;
            yield return null;
        }


        active = true;
    }
}
