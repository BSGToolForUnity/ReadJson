using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random; 

public class TestJson : MonoBehaviour
{
    string flie = "jsontest.json";

    public StudentData creatStudentData;
    public StudentData readStudentData;
    public InputField inputField;
    // Use this for initialization
    void Start()
    {
        InitStudentData();
    }

    public void BtnRead()
    {
        //有个小遗憾   修改readData 时候 会同时修改 creatStudent  
        flie = inputField.text;
        readStudentData = BSGJsonExtension.BSGReadJson<StudentData>(flie, creatStudentData);
    }
    public void BtnSet()
    {
        for (int i = 0; i < readStudentData.classes.Length; i++)
        {
            readStudentData.classes[i].className = "修改班级" + i;
        }
        BSGJsonExtension.BSGSaveJson<StudentData>(flie, readStudentData);
    }
    /// <summary>
    /// 初始化Data
    /// </summary>
    public void InitStudentData()
    {
        if (creatStudentData.classes.Length == 0)
        {
            Debug.Log("初始化Data");
            creatStudentData.classes = new Class[3];
            for (int i = 0; i < creatStudentData.classes.Length; i++)
            {
                creatStudentData.classes[i].className = "班级" + i;
                creatStudentData.classes[i].studentInfos = new StudentInfo[10];//每个班级10个人
                for (int j = 0; j < creatStudentData.classes[i].studentInfos.Length; j++)
                {
                    creatStudentData.classes[i].studentInfos[j].studentName = "张" + j;
                    creatStudentData.classes[i].studentInfos[j].scoreInfos = new ScoreInfo[5];
                    for (int k = 0; k < creatStudentData.classes[i].studentInfos[j].scoreInfos.Length; k++)
                    {
                        creatStudentData.classes[i].studentInfos[j].scoreInfos[k].number = k;
                        creatStudentData.classes[i].studentInfos[j].scoreInfos[k].Score1 = Random.Range(0, 100);
                        creatStudentData.classes[i].studentInfos[j].scoreInfos[k].Score2 = Random.Range(0, 100);
                        creatStudentData.classes[i].studentInfos[j].scoreInfos[k].Score3 = Random.Range(0, 100);
                    }
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {

    }

}

/// <summary>
/// 学生数据 
/// </summary>
[System.Serializable]
public struct StudentData //学生信息 
{
    public Class[] classes; //多个班级
}
/// <summary>
/// 班级
/// </summary>
[System.Serializable]
public struct Class
{
    public string className; //班级名
    public Vector2 classPos; //班级位置 
    public StudentInfo[] studentInfos;  //学生信息
}
/// <summary>
/// 学生信息
/// </summary>
[System.Serializable]
public struct StudentInfo
{
    public string studentName;  //学生名
    public ScoreInfo[] scoreInfos; //学生多次分数 
}
/// <summary>
/// 分数
/// </summary>
[System.Serializable]
public struct ScoreInfo
{
    public int number;//次数  
    public int Score1;//分数1
    public int Score2;//分数2  
    public int Score3;//分数3 
}

