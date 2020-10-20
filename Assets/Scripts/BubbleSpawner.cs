using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public TransformType TransformType;
    public GameObject BubblePrefab;
    public BubbleManager BubbleManager;
    public GameManager GameManager;

    private BoxCollider2D _boxCollider;

    private float _elapsedTime;
    private float _secondsBetweenSpawn;
    private bool _enableSpawner;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (!_enableSpawner)
        {
            return;
        }

        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _secondsBetweenSpawn)
        {
            _elapsedTime = 0;
            _secondsBetweenSpawn = Random.Range(0.1f, 2f);

            SpawnBubble(BubbleType.Standart);
        }
    }

    private void OnTriggerEnter2D(Collider2D colliderObject)
    {
        Bubble bubble = colliderObject.gameObject.GetComponent<Bubble>();

        if (bubble.TransformType != TransformType)
        {
            BubbleManager.RemoveBubble(colliderObject.gameObject);
            Destroy(colliderObject.gameObject);

            GameManager.RemoveScore();
        }
    }

    public void EnableSpawner()
    {
        _enableSpawner = true;
    }

    public void DisableSpawner()
    {
        _enableSpawner = false;
    }

    public void SpawnBubble(BubbleType bubbleType)
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, Random.Range(-(_boxCollider.size.y / 2), _boxCollider.size.y / 2), 0f);

        GameObject bubbleGameObject = Instantiate(BubblePrefab, spawnPosition, Quaternion.identity);

        Bubble bubble = bubbleGameObject.GetComponent<Bubble>();
        bubble.TransformType = TransformType;
        bubble.BubbleType = bubbleType;

        BubbleManager.AddBubble(bubbleGameObject);
    }
}
