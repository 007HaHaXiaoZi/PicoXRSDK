using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class GetSN : MonoBehaviour
{

    public Text _text;

    private void OnEnable()
    {
        PXR_Enterprise.InitEnterpriseService();
        PXR_Enterprise.BindEnterpriseService(toBServiceBind);
    }

    private void Start()
    {
        Invoke("Delay",0.1f);
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

    private void Delay()
    {
        _text.text = getSN();
    }
}
