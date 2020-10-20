using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BubbleManager : MonoBehaviour
{
    private List<GameObject> bubbles = new List<GameObject>();

    public void AddBubble(GameObject bubble)
    {
        bubbles.Add(bubble);
    }

    public void RemoveBubble(GameObject bubble)
    {
        bubbles.Remove(bubble);
    }

    public void DestroyBubbles()
    {
        while (bubbles.Count > 0)
        {
            GameObject bubbleGameObject = bubbles.First();
            Bubble bubble = bubbleGameObject.GetComponent<Bubble>();

            bubble.DestroyBubble();
            RemoveBubble(bubbleGameObject);
        }
    }
}
