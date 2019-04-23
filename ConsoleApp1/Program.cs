using System;
using System.Collections.Generic;
using Spine;
using BinToJson;
using System.IO;
using System.Web.Script.Serialization;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");

            TextureLoader textureLoader = new DemoLoader();
            Atlas atlas = new Atlas("22.atlas",textureLoader);
            AtlasAttachmentLoader attachmentLoader = new AtlasAttachmentLoader(atlas);
            SkeletonJson skeletonJson = new SkeletonJson(attachmentLoader);
            SkeletonBinary skeletonBinary = new SkeletonBinary(attachmentLoader);
            //SkeletonData skeletonData = json.readSkeletonData("mySkeleton.json");
            SkeletonData skeletonData = skeletonBinary.ReadSkeletonData("22.skel");
            Console.WriteLine(skeletonData.Version);
            Console.WriteLine(skeletonData.Name);
            Console.WriteLine(skeletonData.FindAnimation("attack").Name);

            #region JSON
            //Takes the skeletonData and converts it into a serializable object
            Dictionary<string, object> jsonFile = SkelDataConverter.FromSkeletonData(skeletonData);

            //convert object to json string for storing
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(jsonFile);


            //Output file to same directory as input with "name 1", does not allow overwrites
            //string preExtension = fileName.Substring(0, fileName.LastIndexOf('.'));
            //int addNum = 1;
            //string fullerName = preExtension;
            //while (File.Exists(fullerName + ".json"))
            //{
            //    fullerName = preExtension + " " + addNum;
            //    addNum++;
            //}
            File.WriteAllText("22.json", json);
            #endregion
        }
    }

    class DemoLoader : TextureLoader
    {
        public void Load(AtlasPage page, string path)
        {
            //throw new NotImplementedException();
            return;
        }

        public void Unload(object texture)
        {
            //throw new NotImplementedException();
            return;
        }
    }
}
