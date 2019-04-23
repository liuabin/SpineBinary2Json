using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using Spine;
using BinToJson;

namespace BIN2JSON
{
    class Program
    {
        // Change It as the Char Name.
        private static string _BoneName = "22";

        static void Main(string[] args)
        {
            Console.WriteLine("Spine Binary to Json Start.");

            TextureLoader textureLoader = new DemoLoader();
            Atlas atlas = new Atlas(_BoneName + ".atlas", textureLoader);
            AtlasAttachmentLoader attachmentLoader = new AtlasAttachmentLoader(atlas);
            SkeletonBinary skeletonBinary = new SkeletonBinary(attachmentLoader);
            SkeletonData skeletonData = skeletonBinary.ReadSkeletonData(_BoneName + ".skel");

            // Test the SkelData.
            Console.WriteLine("The spine version: \t" + skeletonData.Version);
            Console.WriteLine("The bones name: \t" + skeletonData.Name);
            Console.WriteLine("Find the Animation: \t" + skeletonData.FindAnimation("attack").Name);

            #region JSON File Create
            // Takes the skeletonData and converts it into a serializable object
            Dictionary<string, object> jsonFile = SkelDataConverter.FromSkeletonData(skeletonData);

            //convert object to json string for storing
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(jsonFile);
            File.WriteAllText(_BoneName + ".json", json);
            #endregion
            Console.WriteLine("Json File Create Successfully.");
            Console.ReadKey();
        }
    }

    // False Class
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
