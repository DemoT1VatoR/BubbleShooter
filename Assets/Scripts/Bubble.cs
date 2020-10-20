using UnityEngine;

public class Bubble : MonoBehaviour
{
    public TransformType TransformType;
    public BubbleType BubbleType;

    private float _transformSpeed;
    private float _rotationSpeed;

    private BubbleManager _bubbleManager;
    private GameManager _gameManager;
    private Transform _bubbleImage;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private void Start()
    {
        _transformSpeed = Random.Range(2f, 5f);
        _rotationSpeed = Random.Range(50f, 200f);

        GameObject gameManagerObject = GameObject.Find("GameManager");
        _gameManager = gameManagerObject.GetComponent<GameManager>();

        GameObject bubbleManagerObject = GameObject.Find("BubbleManager");
        _bubbleManager = bubbleManagerObject.GetComponent<BubbleManager>();

        _bubbleImage = transform.GetChild(0);
        _spriteRenderer = _bubbleImage.GetComponent<SpriteRenderer>();
        _animator = _bubbleImage.GetComponent<Animator>();

        if(BubbleType == BubbleType.Bonus)
        {
            _spriteRenderer.color = new Color(255, 255, 0, 150);
        }
    }

    private void Update()
    {
        if (TransformType == TransformType.TransformLeft)
        {
            gameObject.transform.Translate(_transformSpeed * Time.deltaTime, 0, 0);
            _bubbleImage.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
        }
        else
        {
            gameObject.transform.Translate(-(_transformSpeed * Time.deltaTime), 0, 0);
            _bubbleImage.Rotate(0, 0, -(_rotationSpeed * Time.deltaTime));
        }
    }

    private void OnMouseDown()
    {
        if (BubbleType == BubbleType.Bonus)
        {
            _gameManager.AddTime();
        }
        else
        {
            _gameManager.AddScore();
        }

        DestroyBubble();
    }

    public void DestroyBubble()
    {
        _animator.Play("Die");

        _bubbleManager.RemoveBubble(gameObject);
        Destroy(gameObject, _animator.runtimeAnimatorController.animationClips[1].length);
    }
}
