using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGoddesSpriteController : MonoBehaviour
{
    [SerializeField] Image img;
    int _index = -1;
    void Update()
    {
        GoddessSkinClass goddessSkinClass = GameData.mGoddesSkin[GameData.GoddessViewIndex];
        if (_index != GameData.GoddessViewIndex && SkinSelectManager.Instance != null)
        {
            img.sprite = SkinSelectManager.Instance.GetGoddesSprite(GameData.GoddessViewIndex);
            _index = GameData.GoddessViewIndex;
        }
    }

    private void OnEnable()
    {
        Update();
    }
}
