using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{

    public static string SCENE_NAME_MANAGE = "ManagerScene";
    public static string SCENE_NAME_TITLE = "TitleCanvas";
    public static string SCENE_NAME_MAIN = "MainCanvas";
    public static string[] scene_list = new string[] { SCENE_NAME_TITLE, SCENE_NAME_MAIN };

    string OpenSceneName = "";
    private List<string> LoadedScene = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        SetOpenSceneName();
    }

    private void SetOpenSceneName()
    {
        if (OpenSceneName == "")
        {
            int cnt = 0;
            foreach (string name in scene_list)
            {
                Scene scene = SceneManager.GetSceneByName(name);
                if (scene.IsValid())
                {
                    OpenSceneName = name;
                    if (!LoadedScene.Contains(name)) LoadedScene.Add(name);
                    cnt++;
                }
            }
            //�Ώ�Scene��1���J���ĂȂ��ꍇ�^�C�g�����J��
            if (cnt == 0)
            {
                //ChangeScene(SCENE_NAME_TITLE, null, false);
                ChangeScene(SCENE_NAME_MAIN, null, false);//�^�C�g����ʂ���Ȃ���
                
            }
        }
    }
    /// <summary>
    /// Scene�؂�ւ�����
    /// </summary>
    public void ChangeScene(string SceneName = "", Action OnComplete = null, bool TransEffect = true)
    {
        StartCoroutine(ChangeSceneCol(SceneName, () => { }, TransEffect));
    }
    IEnumerator ChangeSceneCol(string SceneName = "", Action OnComplete = null, bool TransEffect = true)
    {
        bool wait = true;

        if (TransEffect)
        {
            //Transition�؂�ւ�
            wait = true;
            TransitionRuleController.Instance.On(() =>
            {
                wait = false;
            });
            while (wait)
            {
                yield return null;
            }
        }





        //Manage�ȊO�͔j��
        foreach (string name in scene_list)
        {
            if (name == SCENE_NAME_MANAGE) continue;

            Scene unload_scene = SceneManager.GetSceneByName(name);
            if (unload_scene.IsValid())
            {
                yield return SceneManager.UnloadSceneAsync(name);
                LoadedScene.Remove(name);
            }
        }

        //�J��
        OpenSceneName = SceneName;
        Debug.Log($"SceneName {SceneName}");
        if (!LoadedScene.Contains(OpenSceneName))
        {
            yield return SceneManager.LoadSceneAsync(OpenSceneName, LoadSceneMode.Additive);
            LoadedScene.Add(OpenSceneName);
        }
        Scene scene = SceneManager.GetSceneByName(OpenSceneName); //�ꉞ�`�F�b�N
        while (!scene.IsValid() && !scene.isLoaded)
        {
            yield return null;
        }


        //������
        PlayOnInit();


        if (TransEffect)
        {
            //Transition�؂�ւ�
            wait = true;
            TransitionRuleController.Instance.Off(() =>
            {
                wait = false;
            });
            while (wait)
            {
                yield return null;
            }
        }


        OnComplete?.Invoke();

        yield break;
    }

    //OnInit�����s
    private void PlayOnInit()
    {
        Type componentType = Type.GetType(OpenSceneName);
        Component targetComponent = GameObject.Find(OpenSceneName).GetComponent(OpenSceneName);
        MethodInfo method = componentType.GetMethod("OnInit");
        method.Invoke(targetComponent, null);
    }

}
