using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonColorSync : MonoBehaviour
{
    private Button button;
    private List<Graphic> targets;
    private Color tmp_color;
    public List<Graphic> exclusions;

    public bool AddColor = true;
    private Dictionary<string, Color> TargetList = new Dictionary<string, Color>();
    private Dictionary<string, Color> DefColorList = new Dictionary<string, Color>();

    void Start()
    {
        button = GetComponent<Button>();
    }

    void Update()
    {
        if (tmp_color != button.targetGraphic.canvasRenderer.GetColor())
        {
            tmp_color = button.targetGraphic.canvasRenderer.GetColor();
            targets = GetComponentsInChildren<Graphic>().Where(c => c.gameObject != gameObject && !exclusions.Contains(c)).ToList();
            if (AddColor)
            {
                foreach(Graphic t in targets){
                    string k = t.GetInstanceID().ToString();
                    if (!TargetList.ContainsKey(k))
                    {
                        TargetList.Add(k, t.color);
                    }
                    if (!DefColorList.ContainsKey(k))
                    {
                        DefColorList.Add(k, button.colors.normalColor);
                    }
                    Color c = TargetList[k];
                    if (tmp_color != DefColorList[k])
                    {
                        c = c + (tmp_color - DefColorList[k]);
                    }
                    t.color = c;
                }
            }
            else
            {
                targets.ForEach(t => t.color = tmp_color);
            }
            //DebugLogger.Log("[ButtonColorSync]"+gameObject.name+" "+targets.Count);
        }
    }

    //public void reset()
    //{
    //    button = GetComponent<Button>();
    //    targets = GetComponentsInChildren<Graphic>().Where(c => c.gameObject != gameObject).ToList();
    //}

    public void manualSet()
    {
        Start();
        Update();
    }
}