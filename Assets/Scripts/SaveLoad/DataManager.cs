using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{    public static DataManager Instance { get; private set; }

    //クラスの参照
    public Save saveClass;
    public Read readClass;

    public static bool isInit = false;

    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        Read();

        //Save();

        //Read();

        isInit = true;
    }

    public void Read()
    {
        //読み込む
        readClass.enabled = true;
        //Debug.Log("読み込みがおわりました");
    }

    public void Save()
    {
        //セーブする
        saveClass.enabled = true;
        //Debug.Log("セーブができました");
        //C:\Users\User(ユーザーネーム)\AppData\LocalLow\DefaultCompany（プロジェクトカンパニー）\SampleForQiita(プロジェクトネーム)
    }


    public void Delete()
    {
        //セーブファイルのパスを設定
        string SaveFilePath = Application.persistentDataPath + "/" + SaveLoadKey.SaveFileName;

        if (File.Exists(SaveFilePath))
        {
            File.Delete(SaveFilePath);
            Debug.Log($"Delete {SaveFilePath}");
        }
    }
}