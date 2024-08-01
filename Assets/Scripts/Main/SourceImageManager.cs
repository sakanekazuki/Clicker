using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SourceImageManager : MonoBehaviour
{
    [SerializeField] string Label = "";
    [SerializeField] List<Sprite> sprites;
    [SerializeField] Image image;

    private void Reset()
    {
        image = GetComponent<Image>();
    }
    public void SetSprite(int index = 0)
    {
        if (index >= sprites.Count)
        {
            index = 0;
        }
        image.sprite = sprites[index];
    }
    public void RemoveSprite(int index = 0)
    {
        image.sprite = null;
    }
}
