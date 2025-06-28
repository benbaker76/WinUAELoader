// Copyright (c) 2008, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Data;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Collections;
using System.IO;
using System.Xml;
using ADOX;

namespace WinUAELoader
{
    class GameBaseNode : IComparable<GameBaseNode>
    {
        public string Name = null;
        public string FileName = null;
        public string ScrnshotFilename = null;
        public bool UseMouse = true;
        public PalNTSCType PalNTSC = PalNTSCType.PAL;
        public int NumDisks = 0;
        public string Genre = null;
        public string Year = null;
        public string Publisher = null;
        public int PlayersFrom = -1;
        public int PlayersTo = -1;
        public string Comment = null;
        public string[] UAEConfig = null;

        public GameBaseNode(string fileName)
        {
            FileName = fileName;
        }

        public GameBaseNode()
        {
        }

        public GameBaseNode(string name, string fileName, bool useMouse, PalNTSCType palNTSC, int numDisks, string[] uaeConfig)
        {
            Name = name;
            FileName = fileName;
            UseMouse = useMouse;
            PalNTSC = palNTSC;
            NumDisks = numDisks;
            UAEConfig = uaeConfig;
        }

        public int CompareTo(GameBaseNode other)
        {
            return String.Compare(this.Name, other.Name);
        }
    }

    class WHDLoadNode : IComparable<WHDLoadNode>
    {
        public int GA_Id = -1;
        public string Name = null;
        public string FileName = null;
        public string ScrnshotFilename = null;
        public bool UseMouse = true;
        public PalNTSCType PalNTSC = PalNTSCType.PAL;
        public string Cd = null;
        public string WHDLoad = null;
        public string[] UAEConfig = null;

        public WHDLoadNode()
        {
        }

        public int CompareTo(WHDLoadNode other)
        {
            return String.Compare(this.Name, other.Name);
        }
    }

    class SPSNode : IComparable<SPSNode>
    {
        public int GA_Id = -1;
        public string Name = null;
        public string FileName = null;
        public string ScrnshotFilename = null;
        public bool UseMouse = true;
        public PalNTSCType PalNTSC = PalNTSCType.PAL;
        public int NumDisks = 0;
        public string[] UAEConfig = null;

        public SPSNode()
        {
        }

        public int CompareTo(SPSNode other)
        {
            return String.Compare(this.Name, other.Name);
        }
    }

    class GBDatabase
    {
        public enum JetVersion : int
        {
            Jet10 = 1,
            Jet11 = 2,
            Jet20 = 3,
            Jet3x = 4, // Access 97
            Jet4x = 5 // Access 2000
        }

        public string GameBaseVersion = "0.0";
        public string DemoBaseVersion = "0.0";

        public Dictionary<int, GameBaseNode> GameBaseHash = null;
        public Dictionary<int, GameBaseNode> DemoBaseHash = null;

        public List<GameBaseNode> GameBaseArray = null;
        public List<WHDLoadNode> WHDLoadArray = null;
        public List<SPSNode> SPSArray = null;
        public List<GameBaseNode> DemoBaseArray = null;

        public int GameBaseTotal = 0;
        public int WHDLoadTotal = 0;
        public int SPSTotal = 0;
        public int DemoBaseTotal = 0;
        
        public GBDatabase()
        {
            GameBaseHash = new Dictionary<int, GameBaseNode>();
            DemoBaseHash = new Dictionary<int, GameBaseNode>();

            GameBaseArray = new List<GameBaseNode>();
            WHDLoadArray = new List<WHDLoadNode>();
            SPSArray = new List<SPSNode>();
            DemoBaseArray = new List<GameBaseNode>();
        }

        private void GetDatabaseVersion(string databasePath, ref string version)
        {
            OleDbConnection databaseConnection = null;
            OleDbDataAdapter dataAdapter = null;
            DataSet dataSet = null;
            DataTable dataTable = null;

            if (!File.Exists(databasePath))
                return;

            string connString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + databasePath;

            try
            {
                databaseConnection = new OleDbConnection(connString);

                string sqlCommand = String.Format("SELECT * FROM Config");

                dataAdapter = new OleDbDataAdapter(sqlCommand, connString);
                databaseConnection.Open();

                dataSet = new DataSet();

                dataAdapter.Fill(dataSet);

                dataTable = dataSet.Tables[0];

                if (dataTable.Rows.Count > 0)
                    version = String.Format("{0}.{1}", dataTable.Rows[0]["MajorVersion"].ToString(), dataTable.Rows[0]["MinorVersion"].ToString());
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("GetVersion", "GBDatabase", ex.Message, ex.StackTrace);
            }
            finally
            {
                if (dataTable != null)
                    dataTable.Dispose();
                if (dataSet != null)
                    dataSet.Dispose();
                if (dataAdapter != null)
                    dataAdapter.Dispose();
                if (databaseConnection != null)
                {
                    databaseConnection.Close();
                    databaseConnection.Dispose();
                }
            }
        }

        public string GetGameYear(OleDbConnection databaseConnection, int YE_Id)
        {
            OleDbDataAdapter dataAdapter = null;
            DataSet dataSet = null;
            DataTable dataTable = null;
            string year = null;

            try
            {
                string sqlCommand = "SELECT * FROM Years WHERE YE_Id = " + YE_Id.ToString();

                dataAdapter = new OleDbDataAdapter(sqlCommand, databaseConnection.ConnectionString);

                dataSet = new DataSet();

                dataAdapter.Fill(dataSet);

                dataTable = dataSet.Tables[0];

                if (dataTable.Rows.Count > 0)
                    year = dataTable.Rows[0]["Year"].ToString();
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("GetGameYear", "GBDatabase", ex.Message, ex.StackTrace);
            }
            finally
            {
                if (dataTable != null)
                    dataTable.Dispose();
                if (dataSet != null)
                    dataSet.Dispose();
                if (dataAdapter != null)
                    dataAdapter.Dispose();
            }

            return year;
        }

        public string GetGameGenre(OleDbConnection databaseConnection, int GE_Id)
        {
            OleDbDataAdapter dataAdapter = null;
            DataSet dataSet = null;
            DataTable dataTable = null;
            string genre = null;

            try
            {
                string sqlCommand = "SELECT * FROM Genres WHERE GE_Id = " + GE_Id.ToString();

                dataAdapter = new OleDbDataAdapter(sqlCommand, databaseConnection.ConnectionString);

                dataSet = new DataSet();

                dataAdapter.Fill(dataSet);

                dataTable = dataSet.Tables[0];

                if (dataTable.Rows.Count > 0)
                    genre = dataTable.Rows[0]["Genre"].ToString();
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("GetGameGenre", "GBDatabase", ex.Message, ex.StackTrace);
            }
            finally
            {
                if (dataTable != null)
                    dataTable.Dispose();
                if (dataSet != null)
                    dataSet.Dispose();
                if (dataAdapter != null)
                    dataAdapter.Dispose();
            }

            return genre;
        }

        public string GetGamePublisher(OleDbConnection databaseConnection, int PU_Id)
        {
            OleDbDataAdapter dataAdapter = null;
            DataSet dataSet = null;
            DataTable dataTable = null;
            string publisher = null;

            try
            {
                string sqlCommand = "SELECT * FROM Publishers WHERE PU_Id = " + PU_Id.ToString();

                dataAdapter = new OleDbDataAdapter(sqlCommand, databaseConnection.ConnectionString);

                dataSet = new DataSet();

                dataAdapter.Fill(dataSet);

                dataTable = dataSet.Tables[0];

                if (dataTable.Rows.Count > 0)
                    publisher = dataTable.Rows[0]["Publisher"].ToString();
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("GetGamePublisher", "GBDatabase", ex.Message, ex.StackTrace);
            }
            finally
            {
                if (dataTable != null)
                    dataTable.Dispose();
                if (dataSet != null)
                    dataSet.Dispose();
                if (dataAdapter != null)
                    dataAdapter.Dispose();
            }

            return publisher;
        }

        public void CreateGameExDatabase(string databasePath, JetVersion version)
        {
            Catalog dataCatalog;
            Table dataTable;

            dataCatalog = new Catalog();
            dataTable = new Table();

            try
            {
                if (File.Exists(databasePath))
                    File.Delete(databasePath);

                string connString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + databasePath + String.Format(";Jet OLEDB:Engine Type={0};", (int)version);

                /* Microsoft.Office.Interop.Access.Application app = new Microsoft.Office.Interop.Access.Application();

                app.NewCurrentDatabase(databasePath);
                app.CloseCurrentDatabase(); */

                dataCatalog.Create(connString);
                //dataCatalog.let_ActiveConnection(connString);

                dataTable.Name = "GameData";

                dataTable.Columns.Append("ID", DataTypeEnum.adInteger, 0);
                dataTable.Keys.Append("PrimaryKey", KeyTypeEnum.adKeyPrimary, "ID", null, null);
                dataTable.Columns["ID"].ParentCatalog = dataCatalog;
                dataTable.Columns["ID"].Properties["Autoincrement"].Value = true;

                dataTable.Columns.Append("GoodName", DataTypeEnum.adVarWChar, 255);
                dataTable.Columns["GoodName"].ParentCatalog = dataCatalog;
                dataTable.Columns["GoodName"].Properties["Nullable"].Value = true;
                
                dataTable.Columns.Append("NoIntro", DataTypeEnum.adVarWChar, 255);
                dataTable.Columns["NoIntro"].ParentCatalog = dataCatalog;
                dataTable.Columns["NoIntro"].Properties["Nullable"].Value = true;

                dataTable.Columns.Append("TOSEC", DataTypeEnum.adVarWChar, 255);
                dataTable.Columns["TOSEC"].ParentCatalog = dataCatalog;
                dataTable.Columns["TOSEC"].Properties["Nullable"].Value = true;

                dataTable.Columns.Append("Name", DataTypeEnum.adVarWChar, 255);
                dataTable.Columns["Name"].ParentCatalog = dataCatalog;
                dataTable.Columns["Name"].Properties["Nullable"].Value = true;

                dataTable.Columns.Append("Publisher", DataTypeEnum.adLongVarWChar, 0);
                dataTable.Columns["Publisher"].ParentCatalog = dataCatalog;
                dataTable.Columns["Publisher"].Properties["Nullable"].Value = true;

                dataTable.Columns.Append("Date", DataTypeEnum.adDate, 0);
                dataTable.Columns["Date"].ParentCatalog = dataCatalog;
                dataTable.Columns["Date"].Properties["Nullable"].Value = true;

                dataTable.Columns.Append("Category", DataTypeEnum.adVarWChar, 50);
                dataTable.Columns["Category"].ParentCatalog = dataCatalog;
                dataTable.Columns["Category"].Properties["Nullable"].Value = true;

                dataTable.Columns.Append("NumPlayers", DataTypeEnum.adVarWChar, 255);
                dataTable.Columns["NumPlayers"].ParentCatalog = dataCatalog;
                dataTable.Columns["NumPlayers"].Properties["Nullable"].Value = true;

                dataTable.Columns.Append("Description", DataTypeEnum.adLongVarWChar, 0);
                dataTable.Columns["Description"].ParentCatalog = dataCatalog;
                dataTable.Columns["Description"].Properties["Nullable"].Value = true;

                dataCatalog.Tables.Append(dataTable);

                dataTable = new Table();

                dataTable.Name = "Configuration";

                dataTable.Columns.Append("Setting", DataTypeEnum.adVarWChar, 255);
                //dataTable.Columns.Append("Value", DataTypeEnum.adVarWChar, 255);
                dataTable.Columns.Append("Value", DataTypeEnum.adLongVarWChar, 0);
                dataTable.Columns["Value"].ParentCatalog = dataCatalog;
                dataTable.Columns["Value"].Properties["Nullable"].Value = true;

                dataCatalog.Tables.Append(dataTable);

                dataCatalog.ActiveConnection = null;
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("CreateGameExDatabase", "GXDatabase", ex.Message, ex.StackTrace);
            }
            finally
            {
                dataTable = null;
                dataCatalog = null;
            }

            // SetDatabaseInfo(databasePath);
        }

        public void ClearGameExDatabase(string databasePath)
        {
            OleDbConnection databaseConnection = null;
            OleDbDataAdapter dataAdapter = null;
            DataSet dataSet = null;
            DataTable dataTable = null;

            if (!File.Exists(databasePath))
                return;

            string connString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + databasePath;

            try
            {
                databaseConnection = new OleDbConnection(connString);
                dataAdapter = new OleDbDataAdapter();

                dataAdapter.DeleteCommand = new OleDbCommand("DELETE * FROM GameData", databaseConnection);

                databaseConnection.Open();

                dataAdapter.DeleteCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("ClearGameExDatabase", "GBDatabase", ex.Message, ex.StackTrace);
            }
            finally
            {
                if (dataTable != null)
                    dataTable.Dispose();
                if (dataSet != null)
                    dataSet.Dispose();
                if (dataAdapter != null)
                    dataAdapter.Dispose();
                if (databaseConnection != null)
                {
                    databaseConnection.Close();
                    databaseConnection.Dispose();
                }
            }
        }

        private string GetBiographyStr()
        {
            try
            {
                return File.ReadAllText(Settings.File.Biography);
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("GetBiographyStr", "GBDatabase", ex.Message, ex.StackTrace);
            }

            return null;
        }

        private string GetNumPlayers(int playersFrom, int playersTo)
        {
            string playersString = null;

            if (playersFrom == -1 || playersTo == -1)
                return null;

            if(playersTo > 1)
                playersString = " Players";
            else
                playersString = " Player";

            if (playersFrom == playersTo)
                return playersFrom + playersString;

            return String.Format("{0}-{1}{2}", playersFrom, playersTo, playersString);
        }

        private void GetFieldArray(GameBaseNode gamebaseNode, string fileName, out string[] fieldArray, out string[] valueArray)
        {
            List<string> FieldArray = new List<string>();
            List<string> ValueArray = new List<string>();

            FieldArray.Add("GoodName");
            ValueArray.Add(Convert.StrToDBStr(fileName));

            FieldArray.Add("Name");
            ValueArray.Add(Convert.StrToDBStr(gamebaseNode.Name));

            FieldArray.Add("Publisher");
            ValueArray.Add(Convert.StrToDBStr(gamebaseNode.Publisher));

            FieldArray.Add("[Date]");
            ValueArray.Add(Convert.GetDateYear(gamebaseNode.Year));

            FieldArray.Add("Category");
            ValueArray.Add(Convert.StrToDBStr(gamebaseNode.Genre));

            FieldArray.Add("NumPlayers");
            ValueArray.Add(Convert.StrToDBStr(GetNumPlayers(gamebaseNode.PlayersFrom, gamebaseNode.PlayersTo)));

            FieldArray.Add("Description");
            ValueArray.Add(Convert.StrToDBStr(gamebaseNode.Comment));

            fieldArray = FieldArray.ToArray();
            valueArray = ValueArray.ToArray();
        }

        private void InsertGameExConfigData(OleDbDataAdapter dataAdapter, string name, string version, int numGames)
        {
            dataAdapter.InsertCommand.CommandText = String.Format("INSERT INTO Configuration (Setting, [Value]) VALUES ('Name',{0})", Convert.StrToDBStr(String.Format("Commodore Amiga ({0})", name)));
            dataAdapter.InsertCommand.ExecuteNonQuery();

            dataAdapter.InsertCommand.CommandText = String.Format("INSERT INTO Configuration (Setting, [Value]) VALUES ('System',{0})", Convert.StrToDBStr("PC"));
            dataAdapter.InsertCommand.ExecuteNonQuery();

            dataAdapter.InsertCommand.CommandText = String.Format("INSERT INTO Configuration (Setting, [Value]) VALUES ('Creator',{0})", Convert.StrToDBStr("HeadKaze"));
            dataAdapter.InsertCommand.ExecuteNonQuery();

            dataAdapter.InsertCommand.CommandText = String.Format("INSERT INTO Configuration (Setting, [Value]) VALUES ('Author',{0})", Convert.StrToDBStr("HeadKaze"));
            dataAdapter.InsertCommand.ExecuteNonQuery();

            dataAdapter.InsertCommand.CommandText = String.Format("INSERT INTO Configuration (Setting, [Value]) VALUES ('Version',{0})", Convert.StrToDBStr(version));
            dataAdapter.InsertCommand.ExecuteNonQuery();

            dataAdapter.InsertCommand.CommandText = String.Format("INSERT INTO Configuration (Setting, [Value]) VALUES ('Date',{0})", Convert.StrToDBStr(DateTime.Now.ToString("MM/dd/yyyy")));
            dataAdapter.InsertCommand.ExecuteNonQuery();

            dataAdapter.InsertCommand.CommandText = String.Format("INSERT INTO Configuration (Setting, [Value]) VALUES ('NumGames',{0})", numGames);
            dataAdapter.InsertCommand.ExecuteNonQuery();

            dataAdapter.InsertCommand.CommandText = String.Format("INSERT INTO Configuration (Setting, [Value]) VALUES ('Database',{0})", Convert.StrToDBStr(String.Format("[PC] Commodore Amiga ({0}).mdb", name)));
            dataAdapter.InsertCommand.ExecuteNonQuery();

            dataAdapter.InsertCommand.CommandText = String.Format("INSERT INTO Configuration (Setting, [Value]) VALUES ('GoodTool',{0})", Convert.StrToDBStr("GameBase Amiga"));
            dataAdapter.InsertCommand.ExecuteNonQuery();

            dataAdapter.InsertCommand.CommandText = String.Format("INSERT INTO Configuration (Setting, [Value]) VALUES ('NoIntro',{0})", Convert.StrToDBStr(null));
            dataAdapter.InsertCommand.ExecuteNonQuery();

            dataAdapter.InsertCommand.CommandText = String.Format("INSERT INTO Configuration (Setting, [Value]) VALUES ('TOSEC',{0})", Convert.StrToDBStr(null));
            dataAdapter.InsertCommand.ExecuteNonQuery();

            dataAdapter.InsertCommand.CommandText = String.Format("INSERT INTO Configuration (Setting, [Value]) VALUES ('Biography',{0})", Convert.StrToDBStr(GetBiographyStr()));
            dataAdapter.InsertCommand.ExecuteNonQuery();
        }

        public void AddGameExGameBaseDatabase(string databasePath)
        {
            OleDbConnection databaseConnection = null;
            OleDbDataAdapter dataAdapter = null;
            DataSet dataSet = null;
            DataTable dataTable = null;

            CreateGameExDatabase(databasePath, JetVersion.Jet4x);

            string connString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + databasePath;

            try
            {
                databaseConnection = new OleDbConnection(connString);
                dataAdapter = new OleDbDataAdapter();

                dataAdapter.InsertCommand = new OleDbCommand();

                dataAdapter.InsertCommand.Connection = databaseConnection;

                databaseConnection.Open();

                InsertGameExConfigData(dataAdapter, "GameBase", GameBaseVersion, GameBaseTotal);

                foreach (GameBaseNode gamebaseNode in GameBaseArray)
                {
                    if (String.IsNullOrEmpty(gamebaseNode.FileName))
                        continue;

                    string[] fieldArray = null;
                    string[] valueArray = null;
                    string fileName = Path.GetFileNameWithoutExtension(gamebaseNode.FileName);

                    GetFieldArray(gamebaseNode, fileName, out fieldArray, out valueArray);

                    dataAdapter.InsertCommand.CommandText = String.Format("INSERT INTO GameData ({0}) VALUES ({1})", Convert.StrArrayToStr(fieldArray), Convert.StrArrayToStr(valueArray));
                    dataAdapter.InsertCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("AddGameExGameBaseDatabase", "GXDatabase", ex.Message, ex.StackTrace);
            }
            finally
            {
                if (dataTable != null)
                    dataTable.Dispose();
                if (dataSet != null)
                    dataSet.Dispose();
                if (dataAdapter != null)
                    dataAdapter.Dispose();
                if (databaseConnection != null)
                {
                    databaseConnection.Close();
                    databaseConnection.Dispose();
                }
            }
        }

        public void AddGameExWHDLoadDatabase(string databasePath)
        {
            OleDbConnection databaseConnection = null;
            OleDbDataAdapter dataAdapter = null;
            DataSet dataSet = null;
            DataTable dataTable = null;

            CreateGameExDatabase(databasePath, JetVersion.Jet4x);

            string connString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + databasePath;

            try
            {
                databaseConnection = new OleDbConnection(connString);
                dataAdapter = new OleDbDataAdapter();

                dataAdapter.InsertCommand = new OleDbCommand();

                dataAdapter.InsertCommand.Connection = databaseConnection;

                databaseConnection.Open();

                InsertGameExConfigData(dataAdapter, "WHDLoad", GameBaseVersion, WHDLoadTotal);

                foreach (WHDLoadNode WHDLoadNode in WHDLoadArray)
                {
                    GameBaseNode gamebaseNode = null;

                    if (GameBaseHash.TryGetValue(WHDLoadNode.GA_Id, out gamebaseNode))
                    {
                        if (String.IsNullOrEmpty(WHDLoadNode.FileName))
                            continue;

                        string[] fieldArray = null;
                        string[] valueArray = null;
                        string fileName = Path.GetFileNameWithoutExtension(WHDLoadNode.FileName);

                        GetFieldArray(gamebaseNode, fileName, out fieldArray, out valueArray);

                        dataAdapter.InsertCommand.CommandText = String.Format("INSERT INTO GameData ({0}) VALUES ({1})", Convert.StrArrayToStr(fieldArray), Convert.StrArrayToStr(valueArray));
                        dataAdapter.InsertCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("AddGameExWHDLoadDatabase", "GXDatabase", ex.Message, ex.StackTrace);
            }
            finally
            {
                if (dataTable != null)
                    dataTable.Dispose();
                if (dataSet != null)
                    dataSet.Dispose();
                if (dataAdapter != null)
                    dataAdapter.Dispose();
                if (databaseConnection != null)
                {
                    databaseConnection.Close();
                    databaseConnection.Dispose();
                }
            }
        }

        public void AddGameExSPSDatabase(string databasePath)
        {
            OleDbConnection databaseConnection = null;
            OleDbDataAdapter dataAdapter = null;
            DataSet dataSet = null;
            DataTable dataTable = null;

            CreateGameExDatabase(databasePath, JetVersion.Jet4x);

            string connString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + databasePath;

            try
            {
                databaseConnection = new OleDbConnection(connString);
                dataAdapter = new OleDbDataAdapter();

                dataAdapter.InsertCommand = new OleDbCommand();

                dataAdapter.InsertCommand.Connection = databaseConnection;

                databaseConnection.Open();

                InsertGameExConfigData(dataAdapter, "SPS", GameBaseVersion, SPSTotal);

                foreach (SPSNode spsNode in SPSArray)
                {
                    GameBaseNode gamebaseNode = null;

                    if (GameBaseHash.TryGetValue(spsNode.GA_Id, out gamebaseNode))
                    {
                        if (String.IsNullOrEmpty(spsNode.FileName))
                            continue;

                        string[] fieldArray = null;
                        string[] valueArray = null;
                        string fileName = Path.GetFileNameWithoutExtension(spsNode.FileName);

                        GetFieldArray(gamebaseNode, fileName, out fieldArray, out valueArray);

                        dataAdapter.InsertCommand.CommandText = String.Format("INSERT INTO GameData ({0}) VALUES ({1})", Convert.StrArrayToStr(fieldArray), Convert.StrArrayToStr(valueArray));
                        dataAdapter.InsertCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("AddGameExSPSDatabase", "GXDatabase", ex.Message, ex.StackTrace);
            }
            finally
            {
                if (dataTable != null)
                    dataTable.Dispose();
                if (dataSet != null)
                    dataSet.Dispose();
                if (dataAdapter != null)
                    dataAdapter.Dispose();
                if (databaseConnection != null)
                {
                    databaseConnection.Close();
                    databaseConnection.Dispose();
                }
            }
        }

        public void AddGameExDemoBaseDatabase(string databasePath)
        {
            OleDbConnection databaseConnection = null;
            OleDbDataAdapter dataAdapter = null;
            DataSet dataSet = null;
            DataTable dataTable = null;

            CreateGameExDatabase(databasePath, JetVersion.Jet4x);

            string connString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + databasePath;

            try
            {
                databaseConnection = new OleDbConnection(connString);
                dataAdapter = new OleDbDataAdapter();

                dataAdapter.InsertCommand = new OleDbCommand();

                dataAdapter.InsertCommand.Connection = databaseConnection;

                databaseConnection.Open();

                InsertGameExConfigData(dataAdapter, "DemoBase", DemoBaseVersion, DemoBaseTotal);

                foreach (GameBaseNode gamebaseNode in DemoBaseArray)
                {
                    if (String.IsNullOrEmpty(gamebaseNode.FileName))
                        continue;

                    string[] fieldArray = null;
                    string[] valueArray = null;
                    string fileName = Path.GetFileNameWithoutExtension(gamebaseNode.FileName);

                    GetFieldArray(gamebaseNode, fileName, out fieldArray, out valueArray);

                    dataAdapter.InsertCommand.CommandText = String.Format("INSERT INTO GameData ({0}) VALUES ({1})", Convert.StrArrayToStr(fieldArray), Convert.StrArrayToStr(valueArray));
                    dataAdapter.InsertCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("AddGameExDemoBaseDatabase", "GXDatabase", ex.Message, ex.StackTrace);
            }
            finally
            {
                if (dataTable != null)
                    dataTable.Dispose();
                if (dataSet != null)
                    dataSet.Dispose();
                if (dataAdapter != null)
                    dataAdapter.Dispose();
                if (databaseConnection != null)
                {
                    databaseConnection.Close();
                    databaseConnection.Dispose();
                }
            }
        }

        public void GetGameBaseGameList(string databasePath, ref Dictionary<int, GameBaseNode> gameBaseHash, ref List<GameBaseNode> gameBaseArray, ref int total, ref string version)
        {
            OleDbConnection databaseConnection = null;
            OleDbDataAdapter dataAdapter = null;
            DataSet dataSet = null;
            DataTable dataTable = null;

            if (!File.Exists(databasePath))
                return;

            gameBaseArray.Clear();
            gameBaseHash.Clear();
            total = 0;

            GetDatabaseVersion(databasePath, ref version);

            string connString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + databasePath;

            try
            {
                databaseConnection = new OleDbConnection(connString);

                string sqlCommand = "SELECT * FROM Games";

                dataAdapter = new OleDbDataAdapter(sqlCommand, connString);
                databaseConnection.Open();

                dataSet = new DataSet();

                dataAdapter.Fill(dataSet);

                dataTable = dataSet.Tables[0];

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    int GA_Id = Convert.StrToInt(dataTable.Rows[i]["GA_Id"].ToString());
                    //string FileName = Path.GetFileName(dataTable.Rows[i]["FileName"].ToString());
                    string FileName = dataTable.Rows[i]["FileName"].ToString();

                    bool PlayersSim = Convert.StrToBool(dataTable.Rows[i]["PlayersSim"].ToString());
                    int Control = Convert.StrToInt(dataTable.Rows[i]["Control"].ToString());
                    bool UseMouse = true;
                   
                    if(PlayersSim && (Control == 0 || Control == 1))
                        UseMouse = false;

                    GameBaseNode gameBase = new GameBaseNode(dataTable.Rows[i]["Name"].ToString(), FileName, UseMouse, (PalNTSCType)Convert.StrToInt(dataTable.Rows[i]["V_PalNTSC"].ToString()), Convert.StrToInt(dataTable.Rows[i]["V_Length"].ToString()), dataTable.Rows[i]["Gemus"].ToString().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));

                    int GE_Id = Convert.StrToInt(dataTable.Rows[i]["GE_Id"].ToString());
                    int YE_Id = Convert.StrToInt(dataTable.Rows[i]["YE_Id"].ToString());
                    int PU_Id = Convert.StrToInt(dataTable.Rows[i]["PU_Id"].ToString());

                    gameBase.Genre = GetGameGenre(databaseConnection, GE_Id);
                    gameBase.Year = GetGameYear(databaseConnection, YE_Id);
                    gameBase.Publisher = GetGamePublisher(databaseConnection, PU_Id);
                    gameBase.PlayersFrom = Convert.StrToInt(dataTable.Rows[i]["PlayersFrom"].ToString());
                    gameBase.PlayersTo = Convert.StrToInt(dataTable.Rows[i]["PlayersTo"].ToString());
                    gameBase.Comment = dataTable.Rows[i]["Comment"].ToString();
                    gameBase.ScrnshotFilename = dataTable.Rows[i]["ScrnshotFilename"].ToString();

                    gameBaseHash.Add(GA_Id, gameBase);
                    gameBaseArray.Add(gameBase);
                    total++;

                    System.Windows.Forms.Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("GetGameBaseGameList", "GBDatabase", ex.Message, ex.StackTrace);
            }
            finally
            {
                if (dataTable != null)
                    dataTable.Dispose();
                if (dataSet != null)
                    dataSet.Dispose();
                if (dataAdapter != null)
                    dataAdapter.Dispose();
                if (databaseConnection != null)
                {
                    databaseConnection.Close();
                    databaseConnection.Dispose();
                }
            }
        }

        public void FixDemoBaseData()
        {
            try
            {
                foreach (GameBaseNode gameBaseNode in DemoBaseArray)
                {
                    if (gameBaseNode.FileName != String.Empty)
                    {
                        if (gameBaseNode.Publisher == "(Unknown)")
                            gameBaseNode.Publisher = Path.GetDirectoryName(gameBaseNode.FileName);

                        if (gameBaseNode.Genre == "[uncategorized]")
                            gameBaseNode.Genre = String.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("FixDemoBaseData", "GBDatabase", ex.Message, ex.StackTrace);
            }
        }

        public void GetWHDLoadGameList(string databasePath)
        {
            OleDbConnection databaseConnection = null;
            OleDbDataAdapter dataAdapter = null;
            DataSet dataSet = null;
            DataTable dataTable = null;

            if (!File.Exists(databasePath))
                return;

            WHDLoadArray.Clear();
            WHDLoadTotal = 0;

            string connString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + databasePath;

            try
            {
                databaseConnection = new OleDbConnection(connString);

                string sqlCommand = "SELECT * FROM Extras WHERE Name = 'WHDLoad'";

                dataAdapter = new OleDbDataAdapter(sqlCommand, connString);
                databaseConnection.Open();

                dataSet = new DataSet();

                dataAdapter.Fill(dataSet);

                dataTable = dataSet.Tables[0];

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    GameBaseNode gamebaseNode = null;
                    WHDLoadNode WHDLoadNode = new WHDLoadNode();

                    WHDLoadNode.GA_Id =  Convert.StrToInt(dataTable.Rows[i]["GA_Id"].ToString());

                    if (GameBaseHash.TryGetValue(WHDLoadNode.GA_Id, out gamebaseNode))
                    {
                        WHDLoadNode.Name = gamebaseNode.Name;
                        WHDLoadNode.ScrnshotFilename = gamebaseNode.ScrnshotFilename;
                        WHDLoadNode.UseMouse = gamebaseNode.UseMouse;
                        WHDLoadNode.PalNTSC = gamebaseNode.PalNTSC;
                    }

                    //WHDLoadNode.FileName = Path.GetFileName(dataTable.Rows[i]["Path"].ToString());
                    WHDLoadNode.FileName = dataTable.Rows[i]["Path"].ToString();

                    string data = null;
                    string[] dataSplit = null;
                    List<string> uaeConfig = new List<string>();

                    data = dataTable.Rows[i]["Data"].ToString();
                    dataSplit = data.Split(new string[] { "|~|" }, StringSplitOptions.None);

                    if (dataSplit.Length == 9)
                    {
                        data = dataSplit[7];
                        dataSplit = data.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                        WHDLoadNode.Cd = dataSplit[0].Replace("cd=", "");
                        WHDLoadNode.WHDLoad = dataSplit[1].Replace("whdload=", "");

                        for (int j = 2; j < dataSplit.Length; j++)
                            uaeConfig.Add(dataSplit[j]);

                        WHDLoadNode.UAEConfig = uaeConfig.ToArray();

                        WHDLoadArray.Add(WHDLoadNode);
                        WHDLoadTotal++;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("GetWHDLoadGameList", "GBDatabase", ex.Message, ex.StackTrace);
            }
            finally
            {
                if (dataTable != null)
                    dataTable.Dispose();
                if (dataSet != null)
                    dataSet.Dispose();
                if (dataAdapter != null)
                    dataAdapter.Dispose();
                if (databaseConnection != null)
                {
                    databaseConnection.Close();
                    databaseConnection.Dispose();
                }
            }
        }

        public void GetSPSGameList(string databasePath)
        {
            OleDbConnection databaseConnection = null;
            OleDbDataAdapter dataAdapter = null;
            DataSet dataSet = null;
            DataTable dataTable = null;

            if (!File.Exists(databasePath))
                return;

            SPSArray.Clear();
            SPSTotal = 0;

            string connString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + databasePath;

            try
            {
                databaseConnection = new OleDbConnection(connString);

                string sqlCommand = "SELECT * FROM Extras WHERE Name = 'SPS'";

                dataAdapter = new OleDbDataAdapter(sqlCommand, connString);
                databaseConnection.Open();

                dataSet = new DataSet();

                dataAdapter.Fill(dataSet);

                dataTable = dataSet.Tables[0];

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    GameBaseNode gamebaseNode = null;
                    SPSNode spsNode = new SPSNode();

                    spsNode.GA_Id = Convert.StrToInt(dataTable.Rows[i]["GA_Id"].ToString());

                    if (GameBaseHash.TryGetValue(spsNode.GA_Id, out gamebaseNode))
                    {
                        spsNode.Name = gamebaseNode.Name;
                        spsNode.ScrnshotFilename = gamebaseNode.ScrnshotFilename;
                        spsNode.UseMouse = gamebaseNode.UseMouse;
                        spsNode.PalNTSC = gamebaseNode.PalNTSC;
                        spsNode.NumDisks = gamebaseNode.NumDisks;
                    }

                    //spsNode.FileName = Path.GetFileName(dataTable.Rows[i]["Path"].ToString());
                    spsNode.FileName = dataTable.Rows[i]["Path"].ToString();

                    string data = null;
                    string[] dataSplit = null;

                    data = dataTable.Rows[i]["Data"].ToString();
                    dataSplit = data.Split(new string[] { "|~|" },StringSplitOptions.None);

                    if (dataSplit.Length == 9)
                    {
                        data = dataSplit[7];

                        spsNode.UAEConfig = data.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    }

                    SPSArray.Add(spsNode);
                    SPSTotal++;
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("GetGameList", "GBDatabase", ex.Message, ex.StackTrace);
            }
            finally
            {
                if (dataTable != null)
                    dataTable.Dispose();
                if (dataSet != null)
                    dataSet.Dispose();
                if (dataAdapter != null)
                    dataAdapter.Dispose();
                if (databaseConnection != null)
                {
                    databaseConnection.Close();
                    databaseConnection.Dispose();
                }
            }
        }

        public void GetGameBaseVersionXml(string fileName, string databaseName, ref string version)
        {
            XmlTextReader reader = null;

            if (!File.Exists(fileName))
                return;

            try
            {
                reader = new XmlTextReader(fileName);

                do
                {
                    if (!reader.Read())
                        break;
                } while (reader.Name != databaseName);

                if (reader.HasAttributes)
                {
                    while (reader.MoveToNextAttribute())
                    {
                        switch (reader.Name.ToLower())
                        {
                            case "version":
                                version = reader.Value;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("GetGameBaseVersionXml", "MameXml", ex.Message, ex.StackTrace);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader = null;
                }
            }
        }

        private class XmlAttrib
        {
            public string Name = null;
            public string Value = null;

            public XmlAttrib(string name, string value)
            {
                Name = name;
                Value = value;
            }
        }

        private GameBaseNode ProcessGameBaseAttribs(List<XmlAttrib> xmlAttribArray)
        {
            GameBaseNode gamebaseNode = new GameBaseNode();

            try
            {
                foreach (XmlAttrib xmlAttrib in xmlAttribArray)
                {
                    switch (xmlAttrib.Name.ToLower())
                    {
                        case "name":
                            gamebaseNode.Name = xmlAttrib.Value;
                            break;
                        case "filename":
                            gamebaseNode.FileName = xmlAttrib.Value;
                            break;
                        case "scrnshotfilename":
                            gamebaseNode.ScrnshotFilename = xmlAttrib.Value;
                            break;
                        case "usemouse":
                            gamebaseNode.UseMouse = Convert.StrToBool(xmlAttrib.Value);
                            break;
                        case "palntsc":
                            gamebaseNode.PalNTSC = (PalNTSCType)Convert.StrToInt(xmlAttrib.Value);
                            break;
                        case "numdisks":
                            gamebaseNode.NumDisks = Convert.StrToInt(xmlAttrib.Value);
                            break;
                        case "uaeconfig":
                            gamebaseNode.UAEConfig = xmlAttrib.Value.Split(new char[] { '|' });
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("ProcessGameBaseAttribs", "GBDatabase", ex.Message, ex.StackTrace);
            }

            return gamebaseNode;
        }

        private WHDLoadNode ProcessWHDLoadAttribs(List<XmlAttrib> xmlAttribArray)
        {
            WHDLoadNode whdLoadNode = new WHDLoadNode();

            try
            {
                foreach (XmlAttrib xmlAttrib in xmlAttribArray)
                {
                    switch (xmlAttrib.Name.ToLower())
                    {
                        case "name":
                            whdLoadNode.Name = xmlAttrib.Value;
                            break;
                        case "filename":
                            whdLoadNode.FileName = xmlAttrib.Value;
                            break;
                        case "scrnshotfilename":
                            whdLoadNode.ScrnshotFilename = xmlAttrib.Value;
                            break;
                        case "usemouse":
                            whdLoadNode.UseMouse = Convert.StrToBool(xmlAttrib.Value);
                            break;
                        case "palntsc":
                            whdLoadNode.PalNTSC = (PalNTSCType)Convert.StrToInt(xmlAttrib.Value);
                            break;
                        case "cd":
                            whdLoadNode.Cd = xmlAttrib.Value;
                            break;
                        case "whdload":
                            whdLoadNode.WHDLoad = xmlAttrib.Value;
                            break;
                        case "uaeconfig":
                            whdLoadNode.UAEConfig = xmlAttrib.Value.Split(new char[] { '|' });
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("ProcessWHDLoadAttribs", "GBDatabase", ex.Message, ex.StackTrace);
            }

            return whdLoadNode;
        }

        private SPSNode ProcessSPSAttribs(List<XmlAttrib> xmlAttribArray)
        {
            SPSNode spsNode = new SPSNode();

            try
            {
                foreach (XmlAttrib xmlAttrib in xmlAttribArray)
                {
                    switch (xmlAttrib.Name.ToLower())
                    {
                        case "name":
                            spsNode.Name = xmlAttrib.Value;
                            break;
                        case "filename":
                            spsNode.FileName = xmlAttrib.Value;
                            break;
                        case "scrnshotfilename":
                            spsNode.ScrnshotFilename = xmlAttrib.Value;
                            break;
                        case "usemouse":
                            spsNode.UseMouse = Convert.StrToBool(xmlAttrib.Value);
                            break;
                        case "palntsc":
                            spsNode.PalNTSC = (PalNTSCType)Convert.StrToInt(xmlAttrib.Value);
                            break;
                        case "numdisks":
                            spsNode.NumDisks = Convert.StrToInt(xmlAttrib.Value);
                            break;
                        case "uaeconfig":
                            spsNode.UAEConfig = xmlAttrib.Value.Split(new char[] { '|' });
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("ProcessSPSAttribs", "GBDatabase", ex.Message, ex.StackTrace);
            }

            return spsNode;
        }

        public void ReadGameBaseXml(string fileName, List<GameBaseNode> gameBaseArray)
        {
            XmlTextReader reader = null;

            gameBaseArray.Clear();

            try
            {
                reader = new XmlTextReader(fileName);

                reader.Read();

                while (reader.Read())
                {
                    if (reader.LocalName == "Game")
                    {
                        if (reader.HasAttributes)
                        {
                            List<XmlAttrib> xmlAttribArray = new List<XmlAttrib>();

                            while (reader.MoveToNextAttribute())
                                xmlAttribArray.Add(new XmlAttrib(reader.Name, reader.Value));

                            GameBaseNode gamebaseNode = ProcessGameBaseAttribs(xmlAttribArray);

                            gameBaseArray.Add(gamebaseNode);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                LogFile.WriteEntry("ReadGameBaseXml", "MameXml", ex.Message, ex.StackTrace);
            }
        }

        public void ReadWHDLoadXml()
        {
            XmlTextReader reader = null;

            WHDLoadArray.Clear();

            try
            {
                reader = new XmlTextReader(Settings.File.WHDLoadXml);

                reader.Read();

                while (reader.Read())
                {
                    if (reader.LocalName == "Game")
                    {
                        if (reader.HasAttributes)
                        {
                            List<XmlAttrib> xmlAttribArray = new List<XmlAttrib>();

                            while (reader.MoveToNextAttribute())
                                xmlAttribArray.Add(new XmlAttrib(reader.Name, reader.Value));

                            WHDLoadNode whdLoadNode = ProcessWHDLoadAttribs(xmlAttribArray);

                            WHDLoadArray.Add(whdLoadNode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("ReadWHDLoadXml", "MameXml", ex.Message, ex.StackTrace);
            }
        }

        public void ReadSPSXml()
        {
            XmlTextReader reader = null;

            SPSArray.Clear();

            try
            {
                reader = new XmlTextReader(Settings.File.SPSXml);

                reader.Read();

                while (reader.Read())
                {
                    if (reader.LocalName == "Game")
                    {
                        if (reader.HasAttributes)
                        {
                            List<XmlAttrib> xmlAttribArray = new List<XmlAttrib>();

                            while (reader.MoveToNextAttribute())
                                xmlAttribArray.Add(new XmlAttrib(reader.Name, reader.Value));

                            SPSNode spsNode = ProcessSPSAttribs(xmlAttribArray);

                            SPSArray.Add(spsNode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("ReadSPSXml", "GBDatabase", ex.Message, ex.StackTrace);
            }
        }

        public bool TryReadGameBaseXml(string xmlFileName, string fileName, string romFolder, List<GameBaseNode> GameBaseArray, out GameBaseNode GameBaseNode)
        {
            GameBaseNode = null;

            try
            {
                foreach (GameBaseNode gameBaseNode in GameBaseArray)
                {
                    if (Path.GetFileName(fileName) == Path.GetFileName(gameBaseNode.FileName))
                    {
                        string newFileName = gameBaseNode.FileName;

                        if (!File.Exists(Path.Combine(romFolder, newFileName)))
                            if (!File.Exists(Path.Combine(romFolder, newFileName = Path.GetFileName(newFileName))))
                                continue;

                        gameBaseNode.FileName = newFileName;

                        GameBaseNode = gameBaseNode;

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("TryReadGameBaseXml", "GBDatabase", ex.Message, ex.StackTrace);
            }

            return false;
        }

        public bool TryReadWHDLoadXml(string xmlFileName, string fileName, out WHDLoadNode WHDLoadNode)
        {
            WHDLoadNode = null;

            try
            {
                foreach (WHDLoadNode whdloadNode in WHDLoadArray)
                {
                    if (Path.GetFileName(fileName) == Path.GetFileName(whdloadNode.FileName))
                    {
                        string newFileName = whdloadNode.FileName;

                        if (!File.Exists(Path.Combine(Settings.Folder.WHDLoadROMs, newFileName)))
                            if (!File.Exists(Path.Combine(Settings.Folder.WHDLoadROMs, newFileName = Path.GetFileName(newFileName))))
                                continue;

                        whdloadNode.FileName = newFileName;

                        WHDLoadNode = whdloadNode;

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("TryReadWHDLoadXml", "GBDatabase", ex.Message, ex.StackTrace);
            }

            return false;
        }

        public bool TryReadSPSXml(string xmlFileName, string fileName, out SPSNode SPSNode)
        {
            SPSNode = null;

            try
            {
                foreach (SPSNode spsNode in SPSArray)
                {
                    if (Path.GetFileName(fileName) == Path.GetFileName(spsNode.FileName))
                    {
                        string newFileName = spsNode.FileName;

                        if (!File.Exists(Path.Combine(Settings.Folder.SPSROMs, newFileName)))
                            if (!File.Exists(Path.Combine(Settings.Folder.SPSROMs, newFileName = Path.GetFileName(newFileName))))
                                continue;

                        spsNode.FileName = newFileName;

                        SPSNode = spsNode;

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("TryReadSPSXml", "GBDatabase", ex.Message, ex.StackTrace);
            } 

            return false;
        }

        public void WriteGameBaseMapFile(string mapFileName, string gameBaseFolder, List<GameBaseNode> gameBaseArray)
        {
            try
            {
                List<string> mapArray = new List<string>();

                for (int i = 0; i < gameBaseArray.Count; i++)
                {
                    string fileName = gameBaseArray[i].FileName;

                    if (fileName != String.Empty)
                    {
                        if(Settings.GameEx.RemoveMissingGames)
                            if (!File.Exists(Path.Combine(gameBaseFolder, fileName)))
                                if (!File.Exists(Path.Combine(gameBaseFolder, fileName = Path.GetFileName(fileName))))
                                    continue;

                        if (Settings.GameEx.FilterGames)
                        {
                            bool found = false;
                            string[] filterList = Settings.GameEx.FilterString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                            for (int j = 0; j < filterList.Length; j++)
                            {
                                if (fileName.Contains(filterList[j]))
                                {
                                    found = true;
                                    break;
                                }
                            }

                            if (found)
                                continue;
                        }

                        mapArray.Add(String.Format("{0}|{1}", Path.GetFileName(fileName), gameBaseArray[i].Name));
                    }
                }

                File.WriteAllLines(mapFileName, mapArray.ToArray());
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("WriteGameBaseMapFile", "GBDatabase", ex.Message, ex.StackTrace);
            }
        }

        public void WriteWHDLoadMapFile(string mapFileName)
        {
            try
            {
                List<string> mapArray = new List<string>();

                for (int i = 0; i < WHDLoadArray.Count; i++)
                {
                    string fileName = WHDLoadArray[i].FileName;

                    if (Settings.GameEx.RemoveMissingGames)
                        if (!File.Exists(Path.Combine(Settings.Folder.WHDLoadROMs, fileName)))
                            if (!File.Exists(Path.Combine(Settings.Folder.WHDLoadROMs, fileName = Path.GetFileName(fileName))))
                                continue;

                    if (Settings.GameEx.FilterGames)
                    {
                        bool found = false;
                        string[] filterList = Settings.GameEx.FilterString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        for (int j = 0; j < filterList.Length; j++)
                        {
                            if (fileName.Contains(filterList[j]))
                            {
                                found = true;
                                break;
                            }
                        }

                        if (found)
                            continue;
                    }

                    mapArray.Add(String.Format("{0}|{1}", Path.GetFileName(fileName), WHDLoadArray[i].Name));
                }

                File.WriteAllLines(mapFileName, mapArray.ToArray());
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("WriteWHDLoadMapFile", "GBDatabase", ex.Message, ex.StackTrace);
            }
        }

        public void WriteSPSMapFile(string mapFileName)
        {
            try
            {
                List<string> mapArray = new List<string>();

                for (int i = 0; i < SPSArray.Count; i++)
                {
                    string fileName = SPSArray[i].FileName;

                    if (Settings.GameEx.RemoveMissingGames)
                        if (!File.Exists(Path.Combine(Settings.Folder.SPSROMs, fileName)))
                            if (!File.Exists(Path.Combine(Settings.Folder.SPSROMs, fileName = Path.GetFileName(fileName))))
                                continue;

                    if (Settings.GameEx.FilterGames)
                    {
                        bool found = false;
                        string[] filterList = Settings.GameEx.FilterString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        for (int j = 0; j < filterList.Length; j++)
                        {
                            if (fileName.Contains(filterList[j]))
                            {
                                found = true;
                                break;
                            }
                        }

                        if (found)
                            continue;
                    }

                    mapArray.Add(String.Format("{0}|{1}", Path.GetFileName(fileName), SPSArray[i].Name));
                }

                File.WriteAllLines(mapFileName, mapArray.ToArray());
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("WriteSPSLoadMapFile", "GBDatabase", ex.Message, ex.StackTrace);
            }
        }

        public void WriteGameBaseXml(string fileName)
        {
            XmlTextWriter writer = null;

            try
            {
                writer = new XmlTextWriter(fileName, null);

                writer.Formatting = Formatting.Indented;

                writer.WriteStartDocument();

                writer.WriteStartElement("GameBase");

                writer.WriteAttributeString("Version", GameBaseVersion);
                writer.WriteAttributeString("NumGames", GameBaseTotal.ToString());

                foreach (GameBaseNode gamebaseNode in GameBaseArray)
                {
                    writer.WriteStartElement("Game");
                    writer.WriteAttributeString("Name", gamebaseNode.Name);
                    writer.WriteAttributeString("FileName", gamebaseNode.FileName);
                    writer.WriteAttributeString("ScrnshotFilename", gamebaseNode.ScrnshotFilename);
                    writer.WriteAttributeString("UseMouse", gamebaseNode.UseMouse.ToString());
                    writer.WriteAttributeString("PalNTSC", ((int)gamebaseNode.PalNTSC).ToString());
                    writer.WriteAttributeString("NumDisks", gamebaseNode.NumDisks.ToString());
                    writer.WriteAttributeString("UAEConfig", String.Join("|", gamebaseNode.UAEConfig));
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();

                writer.WriteEndDocument();
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("WriteGameBaseXml", "GBDatabase", ex.Message, ex.StackTrace);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Flush();
                    writer.Close();
                }
            }
        }

        public void WriteWHDLoadXml(string fileName)
        {
            XmlTextWriter writer = null;

            try
            {
                writer = new XmlTextWriter(fileName, null);

                writer.Formatting = Formatting.Indented;

                writer.WriteStartDocument();

                writer.WriteStartElement("WHDLoad");

                writer.WriteAttributeString("Version", GameBaseVersion);
                writer.WriteAttributeString("NumGames", WHDLoadTotal.ToString());

                foreach (WHDLoadNode whdloadNode in WHDLoadArray)
                {
                    writer.WriteStartElement("Game");
                    writer.WriteAttributeString("Name", whdloadNode.Name);
                    writer.WriteAttributeString("FileName", whdloadNode.FileName);
                    writer.WriteAttributeString("ScrnshotFilename", whdloadNode.ScrnshotFilename);
                    writer.WriteAttributeString("UseMouse", whdloadNode.UseMouse.ToString());
                    writer.WriteAttributeString("PalNTSC", ((int)whdloadNode.PalNTSC).ToString());
                    writer.WriteAttributeString("Cd", whdloadNode.Cd);
                    writer.WriteAttributeString("WHDLoad", whdloadNode.WHDLoad);
                    writer.WriteAttributeString("UAEConfig", String.Join("|", whdloadNode.UAEConfig));
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();

                writer.WriteEndDocument();
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("WriteWHDLoadXml", "GBDatabase", ex.Message, ex.StackTrace);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Flush();
                    writer.Close();
                }
            }
        }

        public void WriteSPSXml(string fileName)
        {
            XmlTextWriter writer = null;

            try
            {
                writer = new XmlTextWriter(fileName, null);

                writer.Formatting = Formatting.Indented;

                writer.WriteStartDocument();

                writer.WriteStartElement("SPS");

                writer.WriteAttributeString("Version", GameBaseVersion);
                writer.WriteAttributeString("NumGames", SPSTotal.ToString());

                foreach (SPSNode spsNode in SPSArray)
                {
                    writer.WriteStartElement("Game");
                    writer.WriteAttributeString("Name", spsNode.Name);
                    writer.WriteAttributeString("FileName", spsNode.FileName);
                    writer.WriteAttributeString("ScrnshotFilename", spsNode.ScrnshotFilename);
                    writer.WriteAttributeString("UseMouse", spsNode.UseMouse.ToString());
                    writer.WriteAttributeString("PalNTSC", ((int)spsNode.PalNTSC).ToString());
                    writer.WriteAttributeString("NumDisks", spsNode.NumDisks.ToString());
                    writer.WriteAttributeString("UAEConfig", String.Join("|", spsNode.UAEConfig));
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();

                writer.WriteEndDocument();
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("WriteSPSXml", "GBDatabase", ex.Message, ex.StackTrace);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Flush();
                    writer.Close();
                }
            }
        }

        public void WriteDemoBaseXml(string fileName)
        {
            XmlTextWriter writer = null;

            try
            {
                writer = new XmlTextWriter(fileName, null);

                writer.Formatting = Formatting.Indented;

                writer.WriteStartDocument();

                writer.WriteStartElement("DemoBase");

                writer.WriteAttributeString("Version", DemoBaseVersion);
                writer.WriteAttributeString("NumGames", DemoBaseTotal.ToString());

                foreach (GameBaseNode gamebaseNode in DemoBaseArray)
                {
                    writer.WriteStartElement("Game");
                    writer.WriteAttributeString("Name", gamebaseNode.Name);
                    writer.WriteAttributeString("FileName", gamebaseNode.FileName);
                    writer.WriteAttributeString("ScrnshotFilename", gamebaseNode.ScrnshotFilename);
                    writer.WriteAttributeString("UseMouse", gamebaseNode.UseMouse.ToString());
                    writer.WriteAttributeString("PalNTSC", ((int)gamebaseNode.PalNTSC).ToString());
                    writer.WriteAttributeString("NumDisks", gamebaseNode.NumDisks.ToString());
                    writer.WriteAttributeString("UAEConfig", String.Join("|", gamebaseNode.UAEConfig));
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();

                writer.WriteEndDocument();
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("WriteDemoBaseXml", "GBDatabase", ex.Message, ex.StackTrace);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Flush();
                    writer.Close();
                }
            }
        }

        public void WriteUserStartup(string fileName, string cd, string whdLoad)
        {
            try
            {
                List<string> userStartup = new List<string>();

                userStartup.Add("cd " + cd);
                userStartup.Add("whdload " + whdLoad);

                File.WriteAllLines(fileName, userStartup.ToArray());
            }
            catch(Exception ex)
            {
                LogFile.WriteEntry("WriteUserStartup", "GBDatabase", ex.Message, ex.StackTrace);
            }
        }

        public void WriteUAEConfig(string fileName, string[] UAEArray)
        {
            try
            {
                string[] uaeDefault = File.ReadAllLines(fileName);
                List<string> uaeOutput = new List<string>();

                //uaeOutput.AddRange(uaeDefault);

                for (int i = 0; i < uaeDefault.Length; i++)
                    if (!uaeDefault[i].StartsWith("filesystem"))
                        uaeOutput.Add(uaeDefault[i]);

                for (int i=0; i<UAEArray.Length; i++)
                {
                    bool found = false;

                    string[] uaeSplit1 = UAEArray[i].Split(new char[] { '=' });

                    if (uaeSplit1.Length == 2)
                    {
                        if (!uaeSplit1[0].StartsWith("filesystem"))
                        {
                            for (int j = 0; j < uaeOutput.Count; j++)
                            {
                                string[] uaeSplit2 = uaeOutput[j].Split(new char[] { '=' });

                                if (uaeSplit2.Length == 2)
                                {
                                    if (uaeSplit1[0] == "cpu_type" && uaeSplit2[0] == "cpu_model")
                                        uaeOutput[j] = UAEArray[i];

                                    if (uaeSplit1[0] == uaeSplit2[0])
                                    {
                                        uaeOutput[j] = UAEArray[i];
                                        found = true;
                                    }
                                }
                            }
                        }
                    }

                    if(!found)
                        uaeOutput.Add(UAEArray[i]);
                }

                File.WriteAllLines(fileName, uaeOutput.ToArray());
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("WriteUAEConfig", "GBDatabase", ex.Message, ex.StackTrace);
            }
        }
    }
}
