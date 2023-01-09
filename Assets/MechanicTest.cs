using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicTest : MonoBehaviour
{
    [SerializeField] GameObject pf_soundSource;

    GameObject soundSource;
    float size;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            soundSource = Instantiate(pf_soundSource, pos, Quaternion.identity);
            size = 0;
        }

        if (Input.GetMouseButton(1))
        {
            size += Time.deltaTime * 4f;
            soundSource.transform.localScale = size * Vector3.one;
        }

        if (Input.GetMouseButtonUp(1))
        {
            Destroy(soundSource);
        }
    }
}
