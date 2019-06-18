using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//public class TextWriter : MonoBehaviour
//{
   

//    public static TextWriter Instance;
//    private void Awake()
//    {
//        if(Instance == null)
//        {
//            Instance = this;
//        }
//    }

//    public void AddLineToDocument(string line)
//    {
//        string path = "Documentation.txt";

//        //Write some text to the test.txt file
//        StreamWriter writer = new StreamWriter(path, true);
//        writer.WriteLine(line);
//        writer.Close();

//        //Re-import the file to update the reference in the editor
//        TextAsset asset = Resources.Load("Documentation") as TextAsset;
//    }

//}
