using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasBase : MonoBehaviour
{
    private CanvasManager _canvasManager;
    public CanvasManager canvasManager { 
        get {
            if (_canvasManager == null)
            {
                GameObject obj = GameObject.Find("CanvasManager");
                if (obj != null)
                    _canvasManager = obj.GetComponent<CanvasManager>();
            }
            return _canvasManager;
        }
    }

    private LoadingManager _loadingManager;
    public LoadingManager loadingManager
    {
        get
        {
            if (_loadingManager == null)
            {
                GameObject obj = GameObject.Find("LoadingManager");
                if (obj != null)
                    _loadingManager = obj.GetComponent<LoadingManager>();
            }
            return _loadingManager;
        }
    }

    public virtual void Awake()
    {
        Application.targetFrameRate = 60;

        if (canvasManager == null)
        {
            //Manageがない場合無理やり追加
            SceneManager.LoadScene(CanvasManager.SCENE_NAME_MANAGE, LoadSceneMode.Additive);

            StartCoroutine(corInitSaveData(() => {
                OnInit();
            }));

        }
    }
    public virtual void Start()
    {
    }

    IEnumerator corInitSaveData(Action OnComplete)
    {
        bool wait = true;
        while (wait)
        {
            //Debug.Log("corInitSaveData stay");
            if (DataManager.Instance != null && DataManager.isInit)
                break;

            yield return null;
        }

        //Debug.Log("corInitSaveData finish");

        OnComplete?.Invoke();

        yield break;
    }

    public virtual void OnInit()
    {
        //Debug.Log($"CanvasBase OnInit");
    }

    public virtual void OnView()
    {

    }

    public virtual void OnExit()
    {

    }

    public virtual void OnRemove()
    {

    }


    public virtual void ChangeScene(string name)
    {
        canvasManager.ChangeScene(name);
    }
}
