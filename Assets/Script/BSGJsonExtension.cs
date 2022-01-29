using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class BSGJsonExtension
{
    //使用方法
    //读取
    //readStudentData = BSGJsonExtension.BSGReadJson<StudentData>(flie, creatStudentData);
    //creatStudentData 没有json文件时候写入的初始值,也可以不传,生成空白的json文件
    //存储  实现原理是:新生成json覆盖老的json   
    //BSGJsonExtension.BSGSaveJson<StudentData>(flie, readStudentData);

    #region Application.persistentDataPath 
    /// <summary>
    /// 读取Json Application.persistentDataPath
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="file">路径</param>
    /// <param name="defaultInfo">无Json初始值</param>
    /// <param name="fileNotFoundException">无Json事件</param> 
    /// <returns></returns>
    public static T BSGReadPersistentJson<T>(string file, T defaultInfo = default(T), System.Action fileNotFoundException = null)
    {
        /*
          WWW www = new WWW("jar:file://" + Application.dataPath + "!/assets/"+dataName);
          WWW www = new WWW("file://"+Application.dataPath + "/Raw/"+dataName);
         */
        var url = Application.persistentDataPath + "/" + file;
        Debug.Log("读取路径" + url);
        if (File.Exists(url))
        {
            string sr = null;
            try
            {
                sr = File.ReadAllText(url);
            }
            catch (System.Exception e)
            {
                Debug.LogError("读取出错" + e.Message);
                return default(T);
            }
            Debug.Log("读取Json成功");
            //转换成Json格式      
            T tempT = JsonUtility.FromJson<T>(sr.ToString());
            Debug.Log(sr);
            return tempT;
        }
        else
        {
            Debug.Log("Json文件不存在");
            if (fileNotFoundException != null)
            {
                fileNotFoundException.Invoke();
            }
            T s = System.Activator.CreateInstance<T>();
            s = defaultInfo;
            //创建json文件
            BSGCreatPersistentJson<T>(file, s);
            return s;
        }
    }


    public static bool BSGSavePersistentJson<T>(string file, T info)
    {
        var url = Application.persistentDataPath + "/" + file;
        Debug.Log("开始保存Json" + url);
        FileInfo fileInfo = new FileInfo(url);
        StreamWriter sw = fileInfo.CreateText();
        string json = JsonUtility.ToJson(info, true);
        try
        {
            sw.WriteLine(json.ToString());
        }
        catch (System.Exception e)
        {

            Debug.Log("保存Json失败" + e.Message);
            sw.Close();
            return false;
        }
        Debug.Log("保存Json成功");
        sw.Close();
        return true;
    }

    //创建json文件
    private static T BSGCreatPersistentJson<T>(string file, T defaultInfo)
    {
        var url = Application.persistentDataPath + "/" + file;
        Debug.Log("开始创建Json" + url);
        FileInfo fileInfo = new FileInfo(url);
        StreamWriter sw = fileInfo.CreateText();
        string json = JsonUtility.ToJson(defaultInfo, true);
        try
        {
            sw.WriteLine(json.ToString());
        }
        catch (System.Exception e)
        {

            Debug.Log("创建Json失败" + e.Message);
            sw.Close();
            return defaultInfo;
        }
        Debug.Log("创建Json成功" + json);
        sw.Close();
        return defaultInfo;
    }
    #endregion

    #region Application.dataPath 
    /// <summary>
    /// 读取Json Application.persistentDataPath
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="file">路径</param>
    /// <param name="defaultInfo">无Json初始值</param>
    /// <param name="fileNotFoundException">无Json事件</param> 
    /// <returns></returns>
    public static T BSGReadDataPathJson<T>(string file, T defaultInfo = default(T), System.Action fileNotFoundException = null)
    {
        /*
          WWW www = new WWW("jar:file://" + Application.dataPath + "!/assets/"+dataName);
          WWW www = new WWW("file://"+Application.dataPath + "/Raw/"+dataName);
         */
        var url = Application.dataPath + "/Resources/Jsons" + file;
        Debug.Log("读取路径" + url);
        if (File.Exists(url))
        {
            string sr = null;
            try
            {
                sr = File.ReadAllText(url);
            }
            catch (System.Exception e)
            {
                Debug.LogError("读取出错" + e.Message);
                return default(T);
            }
            Debug.Log("读取Json成功");
            //转换成Json格式      
            T tempT = JsonUtility.FromJson<T>(sr.ToString());
            Debug.Log(sr);
            return tempT;
        }
        else
        {
            Debug.Log("Json文件不存在");
            if (fileNotFoundException != null)
            {
                fileNotFoundException.Invoke();
            }
            T s = System.Activator.CreateInstance<T>();
            s = defaultInfo;
            //创建json文件
            BSGCreatDataPathJson<T>(file, s);
            return s;
        }
    }


    public static bool BSGSaveDataPathJson<T>(string file, T info)
    {
        var url = Application.dataPath + "/Resources/Jsons" + file;
        Debug.Log("开始保存Json" + url);
        FileInfo fileInfo = new FileInfo(url);
        StreamWriter sw = fileInfo.CreateText();
        string json = JsonUtility.ToJson(info, true);
        try
        {
            sw.WriteLine(json.ToString());
        }
        catch (System.Exception e)
        {

            Debug.Log("保存Json失败" + e.Message);
            sw.Close();
            return false;
        }
        Debug.Log("保存Json成功");
        sw.Close();
        return true;
    }

    //创建json文件
    private static T BSGCreatDataPathJson<T>(string file, T defaultInfo)
    {
        var url = Application.dataPath + "/Resources/Jsons/" + file; 
        Debug.Log("开始创建Json" + url);
        FileInfo fileInfo = new FileInfo(url);
        StreamWriter sw = fileInfo.CreateText();
        string json = JsonUtility.ToJson(defaultInfo, true);
        try
        {
            sw.WriteLine(json.ToString());
        }
        catch (System.Exception e)
        {

            Debug.Log("创建Json失败" + e.Message);
            sw.Close();
            return defaultInfo;
        }
        Debug.Log("创建Json成功" + json);
        sw.Close();
        return defaultInfo;
    }
    #endregion

}


