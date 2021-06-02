using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace MinecraftTexturePack
{
    class Program
    {
        public static string userName = Environment.GetEnvironmentVariable("USERNAME");

        public static string texturepathblock;
        public static string[] filesblock;

        public static string texturepathitem;
        public static string[] filesitem;

        public static string texturepatharmor;
        public static string[] filesarmor;

        public static string texturepathparticle;
        public static string[] filesparticle;

        public static Random rand;
        public static Random Guidrand;
        public static int seed;



        public static void Main()
        {
            Console.WriteLine("Enter name of the resource pack:");

            string resourceName = Console.ReadLine();
            bool ok = false;
            

            while (!ok)
            {
                try
                {
                    Console.WriteLine("Enter seed randomizer:");
                    seed = int.Parse(Console.ReadLine());
                    ok = true;
                    rand = new Random(seed);
                    Guidrand = new Random(seed);
                    Console.WriteLine(rand.Next(0, 1000));
                }
                catch
                {
                    Console.WriteLine("LA SEED DOIT ETRE UN ENTIER");
                }
            }



            if (Directory.Exists("C:\\Users\\" + userName + "\\AppData\\Roaming\\.minecraft\\resourcepacks\\" + resourceName + "\\assets\\minecraft\\textures\\block"))
            {
                texturepathblock = "C:\\Users\\" + userName + "\\AppData\\Roaming\\.minecraft\\resourcepacks\\" + resourceName + "\\assets\\minecraft\\textures\\block";
                texturepathitem = "C:\\Users\\" + userName + "\\AppData\\Roaming\\.minecraft\\resourcepacks\\" + resourceName + "\\assets\\minecraft\\textures\\item";
                texturepatharmor = "C:\\Users\\" + userName + "\\AppData\\Roaming\\.minecraft\\resourcepacks\\" + resourceName + "\\assets\\minecraft\\textures\\models\\armor";
                texturepathparticle = "C:\\Users\\" + userName + "\\AppData\\Roaming\\.minecraft\\resourcepacks\\" + resourceName + "\\assets\\minecraft\\textures\\particle";
                filesparticle = Directory.GetFiles(texturepathparticle, "*.png");
                Array.Sort(filesparticle);
                Particle();
            }
            else if(Directory.Exists("C:\\Users\\" + userName + "\\AppData\\Roaming\\.minecraft\\resourcepacks\\" + resourceName + "\\assets\\minecraft\\textures\\blocks")){
                texturepathblock = "C:\\Users\\" + userName + "\\AppData\\Roaming\\.minecraft\\resourcepacks\\" + resourceName + "\\assets\\minecraft\\textures\\blocks";
                texturepathitem = "C:\\Users\\" + userName + "\\AppData\\Roaming\\.minecraft\\resourcepacks\\" + resourceName + "\\assets\\minecraft\\textures\\items";
                texturepatharmor = "C:\\Users\\" + userName + "\\AppData\\Roaming\\.minecraft\\resourcepacks\\" + resourceName + "\\assets\\minecraft\\textures\\models\\armor";
            }
            else
            {
                Console.WriteLine("LE PROGRAMME NE SUPPORTE PAS LE RESSOURCE PACK...");
                Console.ReadLine();
                Environment.Exit(-1);
            }
            filesblock = Directory.GetFiles(texturepathblock, "*.png");
            Array.Sort(filesblock);
            filesitem = Directory.GetFiles(texturepathitem, "*.png");
            Array.Sort(filesitem);
            filesarmor = Directory.GetFiles(texturepatharmor, "*.png");
            Array.Sort(filesarmor);

            Console.WriteLine("Resource pack is: " + resourceName);
            Block();
            Item();
            Armor();
        }

        public static Guid GenerateSeededGuid()
        {
            var newGuid = new byte[16];
            Guidrand.NextBytes(newGuid);
            return new Guid(newGuid);
        }
        public static void Block()
        {
            FileToGuidBlock();
            GuidToFileBlock();
        }

        public static void Item()
        {
            FileToGuidItem();
            GuidToFileItem();
        }

        public static void Armor()
        {
            FileToGuidArmor();
            GuidToFileArmor();
        }

        public static void Particle()
        {
            FileToGuidParticle();
            GuidToFileParticle();
        }
        public static void FileToGuidBlock()
        {
            //Dictionary<string, string> textureDic = new Dictionary<string, string>();


            //create disctione
            foreach (string f in filesblock)
            {
                FileInfo infofichier = new FileInfo(f);
                string g = GenerateSeededGuid().ToString();
                string mcmetaName = string.Format("{0}.mcmeta", infofichier.FullName);
                string newMcmetaName = string.Format("{0}{1}", g, infofichier.Extension);
                string newName = string.Format("{0}{1}", g,infofichier.Extension);
                //textureDic.Add(newName, infofichier.Name);
                if (File.Exists(mcmetaName))
                {
                    Console.WriteLine("MACMETAFILE !!!");
                    Console.WriteLine(string.Format("Renome :{0}, par {1}",mcmetaName, mcmetaName.Replace(string.Format("{0}", infofichier.Name), newMcmetaName)));
                    File.Move(mcmetaName, mcmetaName.Replace(string.Format("{0}", infofichier.Name), newMcmetaName));
                    //Console.WriteLine(mcmetaName);
                    //Console.WriteLine(mcmetaName.Replace(string.Format("{0}", infofichier.Name), newMcmetaName));
                }
                File.Move(infofichier.FullName, infofichier.FullName.Replace(infofichier.Name, newName));
                Console.WriteLine(string.Format("renome le fichier : {0}, par le fichier {1}",infofichier.Name, newName));
            }
        }

        public static void GuidToFileBlock()
        {

            string[] Renamedfilesblock = Directory.GetFiles(texturepathblock, "*.png");
            List<int> used = new List<int>();
            int LimitRand = filesblock.Length;

            foreach (string f in Renamedfilesblock)
            {
                FileInfo infoFichierRenomer = new FileInfo(f);

                int newrand = rand.Next(0, LimitRand); 
                while (used.Contains(newrand))
                {
                    newrand = rand.Next(0, LimitRand);
                }

                string newNileName = filesblock[newrand];

                string Destname = newNileName;
                string ActualMcMeta = string.Format("{0}.mcmeta", infoFichierRenomer.FullName);
                string Destnamemcmeta =  string.Format("{0}.mcmeta", newNileName);

                File.Move(infoFichierRenomer.FullName, Destname);
                if (File.Exists(ActualMcMeta))
                {
                    File.Move(ActualMcMeta, Destnamemcmeta);
                }
                used.Add(newrand);
            }
        }

        public static void FileToGuidItem()
        {
            //Dictionary<string, string> textureDic = new Dictionary<string, string>();


            //create disctione
            foreach (string f in filesitem)
            {
                FileInfo infofichier = new FileInfo(f);
                string g = GenerateSeededGuid().ToString();
                string mcmetaName = string.Format("{0}.mcmeta", infofichier.FullName);
                string newMcmetaName = string.Format("{0}{1}", g, infofichier.Extension);
                string newName = string.Format("{0}{1}", g, infofichier.Extension);
                //textureDic.Add(newName, infofichier.Name);
                if (File.Exists(mcmetaName))
                {
                    Console.WriteLine("MACMETAFILE !!!");
                    Console.WriteLine(string.Format("Renome :{0}, par {1}", mcmetaName, mcmetaName.Replace(string.Format("{0}", infofichier.Name), newMcmetaName)));
                    File.Move(mcmetaName, mcmetaName.Replace(string.Format("{0}", infofichier.Name), newMcmetaName));
                    //Console.WriteLine(mcmetaName);
                    //Console.WriteLine(mcmetaName.Replace(string.Format("{0}", infofichier.Name), newMcmetaName));
                }
                File.Move(infofichier.FullName, infofichier.FullName.Replace(infofichier.Name, newName));
                Console.WriteLine(string.Format("renome le fichier : {0}, par le fichier {1}", infofichier.Name, newName));
            }
        }

        public static void GuidToFileItem()
        {

            string[] Renamedfilesitem = Directory.GetFiles(texturepathitem, "*.png");
            List<int> used = new List<int>();
            int LimitRand = filesitem.Length;

            foreach (string f in Renamedfilesitem)
            {
                FileInfo infoFichierRenomer = new FileInfo(f);

                int newrand = rand.Next(0, LimitRand);
                while (used.Contains(newrand))
                {
                    newrand = rand.Next(0, LimitRand);
                }

                string newNileName = filesitem[newrand];

                string Destname = newNileName;
                string ActualMcMeta = string.Format("{0}.mcmeta", infoFichierRenomer.FullName);
                string Destnamemcmeta = string.Format("{0}.mcmeta", newNileName);

                File.Move(infoFichierRenomer.FullName, Destname);
                if (File.Exists(ActualMcMeta))
                {
                    File.Move(ActualMcMeta, Destnamemcmeta);
                }
                used.Add(newrand);
            }
        }

        public static void FileToGuidArmor()
        {
            //Dictionary<string, string> textureDic = new Dictionary<string, string>();


            //create disctione
            foreach (string f in filesarmor)
            {
                FileInfo infofichier = new FileInfo(f);
                string g = GenerateSeededGuid().ToString();
                string mcmetaName = string.Format("{0}.mcmeta", infofichier.FullName);
                string newMcmetaName = string.Format("{0}{1}", g, infofichier.Extension);
                string newName = string.Format("{0}{1}", g, infofichier.Extension);
                //textureDic.Add(newName, infofichier.Name);
                if (File.Exists(mcmetaName))
                {
                    Console.WriteLine("MACMETAFILE !!!");
                    Console.WriteLine(string.Format("Renome :{0}, par {1}", mcmetaName, mcmetaName.Replace(string.Format("{0}", infofichier.Name), newMcmetaName)));
                    File.Move(mcmetaName, mcmetaName.Replace(string.Format("{0}", infofichier.Name), newMcmetaName));
                    //Console.WriteLine(mcmetaName);
                    //Console.WriteLine(mcmetaName.Replace(string.Format("{0}", infofichier.Name), newMcmetaName));
                }
                File.Move(infofichier.FullName, infofichier.FullName.Replace(infofichier.Name, newName));
                Console.WriteLine(string.Format("renome le fichier : {0}, par le fichier {1}", infofichier.Name, newName));
            }
        }

        public static void GuidToFileArmor()
        {

            string[] RenamedfilesArmor = Directory.GetFiles(texturepatharmor, "*.png");
            List<int> used = new List<int>();
            int LimitRand = filesarmor.Length;

            foreach (string f in RenamedfilesArmor)
            {
                FileInfo infoFichierRenomer = new FileInfo(f);

                int newrand = rand.Next(0, LimitRand);
                while (used.Contains(newrand))
                {
                    newrand = rand.Next(0, LimitRand);
                }

                string newNileName = filesarmor[newrand];

                string Destname = newNileName;
                string ActualMcMeta = string.Format("{0}.mcmeta", infoFichierRenomer.FullName);
                string Destnamemcmeta = string.Format("{0}.mcmeta", newNileName);

                File.Move(infoFichierRenomer.FullName, Destname);
                if (File.Exists(ActualMcMeta))
                {
                    File.Move(ActualMcMeta, Destnamemcmeta);
                }
                used.Add(newrand);
            }
        }

        public static void FileToGuidParticle()
        {
            //Dictionary<string, string> textureDic = new Dictionary<string, string>();


            //create disctione
            foreach (string f in filesparticle)
            {
                FileInfo infofichier = new FileInfo(f);
                string g = GenerateSeededGuid().ToString();
                string mcmetaName = string.Format("{0}.mcmeta", infofichier.FullName);
                string newMcmetaName = string.Format("{0}{1}", g, infofichier.Extension);
                string newName = string.Format("{0}{1}", g, infofichier.Extension);
                //textureDic.Add(newName, infofichier.Name);
                if (File.Exists(mcmetaName))
                {
                    Console.WriteLine("MACMETAFILE !!!");
                    Console.WriteLine(string.Format("Renome :{0}, par {1}", mcmetaName, mcmetaName.Replace(string.Format("{0}", infofichier.Name), newMcmetaName)));
                    File.Move(mcmetaName, mcmetaName.Replace(string.Format("{0}", infofichier.Name), newMcmetaName));
                    //Console.WriteLine(mcmetaName);
                    //Console.WriteLine(mcmetaName.Replace(string.Format("{0}", infofichier.Name), newMcmetaName));
                }
                File.Move(infofichier.FullName, infofichier.FullName.Replace(infofichier.Name, newName));
                Console.WriteLine(string.Format("renome le fichier : {0}, par le fichier {1}", infofichier.Name, newName));
            }
        }

        public static void GuidToFileParticle()
        {

            string[] RenamedfilesParticle = Directory.GetFiles(texturepathparticle, "*.png");
            List<int> used = new List<int>();
            int LimitRand = filesparticle.Length;

            foreach (string f in RenamedfilesParticle)
            {
                FileInfo infoFichierRenomer = new FileInfo(f);

                int newrand = rand.Next(0, LimitRand);
                while (used.Contains(newrand))
                {
                    newrand = rand.Next(0, LimitRand);
                }

                string newNileName = filesparticle[newrand];

                string Destname = newNileName;
                string ActualMcMeta = string.Format("{0}.mcmeta", infoFichierRenomer.FullName);
                string Destnamemcmeta = string.Format("{0}.mcmeta", newNileName);

                File.Move(infoFichierRenomer.FullName, Destname);
                if (File.Exists(ActualMcMeta))
                {
                    File.Move(ActualMcMeta, Destnamemcmeta);
                }
                used.Add(newrand);
            }
        }


    }
}
