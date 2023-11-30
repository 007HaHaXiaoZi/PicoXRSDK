
using System;
using System.Security.Cryptography;
using System.IO;      
using System.Text;

public static class EncryptDecrypt
{
    //加解密密钥,字符串长度必须为8
    public static string key = "11111111";
    //加密
    public static string DESEnCode(string pToEncrypt)
    {
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        byte[] inputByteArray = Encoding.GetEncoding("UTF-8").GetBytes(pToEncrypt);
        try
        {
            des.Key = ASCIIEncoding.ASCII.GetBytes(key);
            des.IV = ASCIIEncoding.ASCII.GetBytes(key);
        }
        catch (Exception)
        {
            UnityEngine.Debug.Log("加密时密钥格式错误！");
            return null;
        }
        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
        try
        {
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
        }
        catch (Exception)
        {
            UnityEngine.Debug.Log("加密错误！");
            return null;
        }
        StringBuilder ret = new StringBuilder();
        foreach (byte b in ms.ToArray())
        {
            ret.AppendFormat("{0:X2}", b);
        }
        ret.ToString();
        return ret.ToString();
    }
    //解密
    public static string DESDeCode(string pToDecrypt)
    {
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
        for (int x = 0; x < pToDecrypt.Length / 2; x++)
        {
            int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
            inputByteArray[x] = (byte)i;
        }
        try
        {
            des.Key = ASCIIEncoding.ASCII.GetBytes(key);
            des.IV = ASCIIEncoding.ASCII.GetBytes(key);
        }
        catch (Exception)
        {
            UnityEngine.Debug.Log("解密时密钥格式错误！");
            return null;
        }
        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
        try
        {
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
        }
        catch (Exception)
        {
            UnityEngine.Debug.Log("解密错误");
            return null;
        }
        StringBuilder ret = new StringBuilder();
        return Encoding.Default.GetString(ms.ToArray());
    }
    private static int getNewSeed()
    {
        byte[] rndBytes = new byte[4];
        System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
        rng.GetBytes(rndBytes);
        return BitConverter.ToInt32(rndBytes, 0);
    }
    public static string GetRandomString(int len)
    {
        string s = "123456789abcdefghijklmnpqrstuvwxyzABCDEFGHIJKLMNPQRSTUVWXYZ";
        string reValue = string.Empty;
        Random rnd = new Random(getNewSeed());
        while (reValue.Length < len)
        {
            string s1 = s[rnd.Next(0, s.Length)].ToString();
            if (reValue.IndexOf(s1) == -1) reValue += s1;
        }
        return reValue;
    }
}