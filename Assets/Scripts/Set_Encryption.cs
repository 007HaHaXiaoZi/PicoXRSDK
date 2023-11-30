using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Set_Encryption : MonoBehaviour
{
    #region

    string setAName = "A";
    string setBName = "B";
    string setKey = "11111111";

    #endregion

    public Text text_Txt;

    public Text text_SN;

    private string 内部存储文件内容="";
    private string 设备SN号 = "";



    private void OnEnable()
    {
        PXR_Enterprise.InitEnterpriseService();
        PXR_Enterprise.BindEnterpriseService(toBServiceBind);

        Invoke("Delay", 0.01f);

    }

    private void OnDestroy()
    {
        PXR_Enterprise.UnBindEnterpriseService();
    }

    private void BoolCallback(string value)
    {
        if (PXR_EnterprisePlugin.BoolCallback != null) PXR_EnterprisePlugin.BoolCallback(bool.Parse(value));
        PXR_EnterprisePlugin.BoolCallback = null;
    }

    private void IntCallback(string value)
    {
        if (PXR_EnterprisePlugin.IntCallback != null) PXR_EnterprisePlugin.IntCallback(int.Parse(value));
        PXR_EnterprisePlugin.IntCallback = null;
    }
    private void LongCallback(string value)
    {
        if (PXR_EnterprisePlugin.LongCallback != null) PXR_EnterprisePlugin.LongCallback(int.Parse(value));
        PXR_EnterprisePlugin.LongCallback = null;
    }

    private void StringCallback(string value)
    {
        if (PXR_EnterprisePlugin.StringCallback != null) PXR_EnterprisePlugin.StringCallback(value);
        PXR_EnterprisePlugin.StringCallback = null;
    }

    public void toBServiceBind(bool s)
    {
        //返回成功后在调用其他接口。
        Debug.Log("Bind success.");
    }

    public string getSN()
    {
        return PXR_Enterprise.StateGetDeviceInfo(SystemInfoEnum.EQUIPMENT_SN);
    }

    void GetTxt_Fun2()
    {
        string filePath = "/storage/emulated/0/XXX" + "/k2.txt";
        if (File.Exists(filePath))
        {
            string[] fileData = File.ReadAllLines(filePath);

            for (int i = 0; i < fileData.Length; i++)
            {
                text_Txt.text += fileData[i];
                内部存储文件内容 += fileData[i];
            }
        }
    }

    private void Delay()
    {
        GetTxt_Fun2();
        text_SN.text = getSN();
        设备SN号 = getSN();

        if (内部存储文件内容.Contains(GetCode(setAName, setBName, setKey)))
        {
            text_SN.text += "已进行此步骤";
            SceneManager.LoadScene("EntranceScene");
        }

    }

    //获取激活码
    string GetCode(string AName, string BName, string key)
    {
        EncryptDecrypt.key = key;
        return EncryptDecrypt.DESEnCode(AName + "/" + BName + "/" + 设备SN号);
    }

}
