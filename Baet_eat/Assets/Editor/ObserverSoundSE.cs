using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class ObserverSoundSE : AssetPostprocessor
{
    private static readonly string filePath = "Assets/Resources/";
    private static readonly string filePath2 = "InGame/SoundSEObjectAll";
    public static string FILR_EXTENSION = ".asset";

    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        string filename = filePath + filePath2 + FILR_EXTENSION;
        foreach (string asset in importedAssets)
        {
            if (!filename.Equals(asset))
                continue;

            CreateCS();






        }
    }
    private static void CreateCS()
    {
        SoundSEObjectlAll achievementsAll = Resources.Load<SoundSEObjectlAll>(filePath2);

        StringBuilder builder = new StringBuilder();
        builder.Clear();
        builder.Append(Application.dataPath);
        builder.Append("/Resources/Enum/SoundSEEnum");
        builder.Append(".cs");

        StreamWriter sw;

        string filePass = builder.ToString();
        sw = new StreamWriter(filePass, false);
        builder.Clear();
        builder.Append("public  static class SoundSEEnum {");
        builder.AppendLine();


        builder.Append("public enum SoundSEType {");
        builder.AppendLine();

        for (int i = 0; i < achievementsAll.notesMaterials.Count; i++)
        {
            builder.AppendFormat("        /// <summary><see _{0}=\"{1}\"/> </summary>\\r\\n"
                , achievementsAll.notesMaterials[i].typeName, achievementsAll.notesMaterials[i].typeNameExplanation);
            builder.AppendLine();

            builder.AppendFormat("_{0}", achievementsAll.notesMaterials[i].typeName);
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