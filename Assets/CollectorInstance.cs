using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CollectorInstance : MonoBehaviour
{
    // Start is called before the first frame update
    public Object[] FILTERS_;
    public Object[] RENDERERS_;
    public int num_objs_ = 1;
    public GameObject Thing;

    void Start()
    {
        FILTERS_ = Resources.FindObjectsOfTypeAll(typeof(UnityEngine.Mesh));
        RENDERERS_ = GetAssetsOfType(typeof(Material), "mat");
        int i = 0;
        while (i < num_objs_)
        {
            var thing = Instantiate(Thing);
            //thing.GetComponent<Rigidbody>().AddForce(Random.insideUnitCircle.normalized * Random.Range(1.0f, 10.0f));
            i++;
        }
    }
    public static Object[] GetAssetsOfType(System.Type type, string fileExtension)
    {
        List<Object> tempObjects = new List<Object>();
        DirectoryInfo directory = new DirectoryInfo(Application.dataPath);
        FileInfo[] goFileInfo = directory.GetFiles("*" + fileExtension, SearchOption.AllDirectories);

        int i = 0; int goFileInfoLength = goFileInfo.Length;
        FileInfo tempGoFileInfo; string tempFilePath;
        Object tempGO;
        for (; i < goFileInfoLength; i++)
        {
            tempGoFileInfo = goFileInfo[i];
            if (tempGoFileInfo == null)
                continue;

            tempFilePath = tempGoFileInfo.FullName;
            tempFilePath = tempFilePath.Replace(@"\", "/").Replace(Application.dataPath, "Assets");

            Debug.Log(tempFilePath + "\n" + Application.dataPath);

            tempGO = AssetDatabase.LoadAssetAtPath(tempFilePath, typeof(Object)) as Object;
            if (tempGO == null)
            {
                Debug.LogWarning("Skipping Null");
                continue;
            }

            tempObjects.Add(tempGO);
        }

        return tempObjects.ToArray();
    }
    void Update()
    {

    }
}
