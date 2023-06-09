﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Daemon
{
    public class FileManager
    {
        private string fileName = "database_secret.txt";
        public string programFolder = AppDomain.CurrentDomain.BaseDirectory + "\\secret";
        public string separator = "@@@";
        public FileManager() { }
       


        public void SaveSnapshot(BackupConfiguration config, Snapshot snapshot)
        {
            string filename = "Snapshot_" + config.configId.ToString();
            string directoryname = "Snapshots";
            string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, directoryname);
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, directoryname, filename);
            Directory.CreateDirectory(directoryPath);
            File.Create(filePath).Close();

            StreamWriter streamWriter = new StreamWriter(filePath, false);
            streamWriter.Write(snapshot.Data[0] + ";" + snapshot.Data[1]);
            streamWriter.Close();
        }

        public Snapshot ReadSnapshot(BackupConfiguration config)
        {
            Snapshot snapshot;
            string filename = "Snapshot_" + config.configId.ToString();
            string directoryname = "Snapshots";


            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, directoryname, filename);

            StreamReader streamReader = new StreamReader(filePath);
            string line = streamReader.ReadLine();

            streamReader.Close();

            string[] data = line.Split(';');
            snapshot = new Snapshot(data);


            return snapshot;
        }

        public bool CheckIDFile()
        {
            string filePath = Path.Combine(programFolder, fileName);
            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\secret") && File.Exists(filePath))
            {
                return true;

            }
            else
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\secret");
                File.Create(programFolder + "\\" + fileName).Close();
                return false;
            }
        }
        public int GetStationID()
        {
            int id = int.Parse(File.ReadAllText(programFolder + "\\" + fileName).ToString());
            return id;
        }

        public void SaveID(string id)
        {
            string filePath = Path.Combine(programFolder, fileName);
            File.WriteAllText(filePath, id);
        }

        public string getID()
        {
            string ID = string.Empty;
            string filePath = Path.Combine(programFolder, fileName);

            StreamReader reader = new StreamReader(filePath);
            ID = reader.ReadToEnd();
            reader.Close();
            return ID;
        }

        public void saveConfigs(string json)
        {
            string filePath = Path.Combine(programFolder + "\\" + "configJson");
            JsonEncryption guard = new JsonEncryption();

            string protectedJson = guard.Encrypt(json);
            File.WriteAllText(filePath, protectedJson);
        }

        public string getConfigs()
        {
            string filePath = Path.Combine(programFolder + "\\" + "configJson");
            JsonEncryption guard = new JsonEncryption();
            string encryptedJson = File.ReadAllText(filePath);

            string decryptedJson = guard.Decrypt(encryptedJson);
            return decryptedJson;
        }
        public void saveKey(byte key)
        {
            string filePath = Path.Combine(programFolder + "\\" + "key");

        }

        public void saveReports(string path, string content)
        {
            File.AppendAllText(path, content);
        }

        public string getReports(string path)
        {
            string reports = File.ReadAllText(path);
            return reports;
        }


    }
}
