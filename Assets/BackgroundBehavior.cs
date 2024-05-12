using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehavior : MonoBehaviour
{
    public GameObject blockPrefab;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            Debug.Log(mousePos.x);
            Debug.Log(mousePos.y);

            //float xPos = Random.Range(20f, 360f);
            float xPos = Mathf.Clamp(mousePos.x, -1.5f, 1.5f);

            var newBlock = Instantiate(blockPrefab, new Vector3(xPos, 4.5f, 0), Quaternion.identity);
            newBlock.GetComponent<BlockBehavior>().BlockSize = Random.Range(1, 3);
        }
    }


}
