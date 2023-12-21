using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainBehavior : MonoBehaviour
{
    private float speed = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Rotate(0f, 90f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + speed * 1);

        if (gameObject.transform.position.z > 200)
        {
            Destroy(gameObject);
        }
    }
}
