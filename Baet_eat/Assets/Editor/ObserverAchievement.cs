using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class ObserverAchievement : AssetPostprocessor
{
    private static readonly string filePath = "Assets/Resources/";
    private static readonly string filePath2 = "Achievements/AchievementsAll";
    public static string FILR_EXTENSION = ".asset";

    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        string filename = filePath +filePath2+ FILR_EXTENSION;
        foreach (string asset in importedAssets)
        {
            if (!filename.Equals(asset))
                continue;

            CreateCS();






        }
    }
    private static void CreateCS()
    {
        AchievementsAll achievementsAll=Resources.Load<AchievementsAll>(filePath2);

        StringBuilder builder = new StringBuilder();
        builder.Clear();
        builder.Append(Application.dataPath);
        builder.Append("/Resources/AchievementEnum");
        builder.Append(".cs");

        StreamWriter sw;

        string filePass = builder.ToString();
        sw = new StreamWriter(filePass, false);
        builder.Clear();
        builder.Append("public  static class AchievementTypeEnum {");
        builder.AppendLine();


        builder.Append("public enum AchievementType {");
        builder.AppendLine();

        for (int i = 0; i < achievementsAll.achievements.Count; i++)
        {
            builder.AppendFormat("        /// <summary><see _{0}=\"{1}\"/> </summary>\\r\\n"
                , achievementsAll.achievements[i].AchievementsEnumName, achievementsAll.achievements[i].AchievementsName);
            builder.AppendLine();

            builder.AppendFormat("_{0}", achievementsAll.achievements[i].AchievementsEnumName);
            builder.Append(",");
            builder.AppendLine();

        }

        builder.Append("MAX");
        builder.AppendLine();


        builder.Append("}");

        builder.AppendLine();

        builder.Append("}");

        sw.Write(builder.ToString());

        sw.Close();
    }



}
