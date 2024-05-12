using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehavior : MonoBehaviour
{
    public GameObject blockPrefab;

    
    public bool UntouchedBlock;

    public int BlockSize
    {
        get
        {
            return _blockSize;
        }
        set
        {
            SetBlockSize(value);
        }
    }


    GameManager gameManager;
    Rigidbody2D _rb2d;
    int _blockSize = 0;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        //_blockSize = Random.Range(1, 3);
        var rotationAngle = Random.Range(-25, 25);

        _rb2d = GetComponent<Rigidbody2D>();
        var impulse = (rotationAngle * Mathf.Deg2Rad) * _rb2d.inertia;
        _rb2d.AddTorque(impulse, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetBlockSize(int size = 1)
    {
        _blockSize = size;
        //Debug.Log("SetBlockSize");
        //Debug.Log(size);
        float sideLength = size * 0.2f + 0.2f;
        transform.localScale = new Vector3(sideLength, sideLength, 1);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision);
        //Debug.Log(collision.collider.gameObject);
        var bb = collision.collider.gameObject.GetComponent<BlockBehavior>();
        if (bb != null)
        {
            //Debug.Log(bb.BlockSize);
            if (bb.BlockSize == BlockSize)
            {
                bb.gameObject.SetActive(false);
                var newX = (bb.gameObject.transform.position.x + gameObject.transform.position.x) / 2;
                var newY = (bb.gameObject.transform.position.y + gameObject.transform.position.y) / 2;
                var newBlock = Instantiate(blockPrefab, new Vector3(newX, newY, 0), Quaternion.identity);
                //Debug.Log("setting new block");
                newBlock.GetComponent<BlockBehavior>().BlockSize = BlockSize + 1;
                Destroy(bb.gameObject);
                Destroy(gameObject);
            }
        }

        if (UntouchedBlock)
        {
            UntouchedBlock = false;
            gameManager.SendMessage("BlockLanded");
        }
    }
}
