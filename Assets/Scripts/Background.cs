using UnityEngine;

public class Background : MonoBehaviour
{
    public GameManager GameManager;

    private void Start()
    {
        BoxCollider2D backgroundCollider = gameObject.GetComponent<BoxCollider2D>();
        backgroundCollider.size = new Vector2(Screen.width, Screen.height);
    }

    private void OnMouseDown()
    {
        GameManager.ResetBonusBar();
    }
}
