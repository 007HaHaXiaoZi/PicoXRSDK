using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GetTXT : MonoBehaviour
{

    public Text text;

    void Start()
    {
        GetTxt_Fun2();
    }

    /// <summary>
    /// 获取Pico下的文件（安装后的Android源生文件夹）
    /// </summary>
    void GetTxt_Fun1()
    {
        Debug.Log(Application.persistentDataPath);
        string filePath = Application.persistentDataPath + "/k.txt";//"/storage/emulated/0/Android/data/com.XX/files";
        if (File.Exists(filePath))
        {
            string[] fileData = File.ReadAllLines(filePath);
            text.text = fileData[0];
        }
    }

    /// <summary>
    /// 获取Pico下的文件，指定文件夹
    /// </summary>
    void GetTxt_Fun2()
    {
        string filePath = "/storage/emulated/0/XXX" + "/k2.txt";
        if (File.Exists(filePath))
        {
            string[] fileData = File.ReadAllLines(filePath);
            text.text = fileData[0];
        }
    }

}
