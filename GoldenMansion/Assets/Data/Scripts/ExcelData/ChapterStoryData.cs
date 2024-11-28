using System.IO;
using System.Collections.Generic;

namespace ExcelData
{
    public class ChapterStoryData : IDataSheet
    {
        public class Item
        {
            public int storyID;
            public int languageID;
            public string CHN;
            public int selectionOneID;
            public string selectionOneText;
            public int selectionTwoID;
            public string selectionTwotTXT;
        }

        private static ChapterStoryData s_Instance;
        private static ChapterStoryData Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = new ChapterStoryData();
                    s_Instance.Init();
                    DataService.RegisterSheet(s_Instance);
                }
                return s_Instance;
            }
        }

        public static Item GetItem(int key)
        {
            Instance.m_Items.TryGetValue(key, out Item foundItem);
            #if UNITY_EDITOR
            if (foundItem == null)
            {
                UnityEngine.Debug.LogWarningFormat("{0} do not contains item of key '{1}'.", Instance.sheetName, key);
            }
            #endif
            return foundItem;
        }

        public static IEnumerable<KeyValuePair<int, Item>> GetDict()
        {
            return Instance.m_Items;
        }

        private Dictionary<int, Item> m_Items = new Dictionary<int, Item>();

        public string sheetName => "ChapterStoryData";

        private void Init()
        {
            byte[] bytes = DataService.GetSheetBytes(sheetName);
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                using(BinaryReader reader = new BinaryReader(ms))
                {
                    reader.ReadString(); //sheetName

                    //Read header
                    SheetHeader sheetHeader = new SheetHeader();
                    sheetHeader.ReadFrom(reader);
                    List<SheetHeader.Item> headerItems = sheetHeader.items;

                    int columns = headerItems.Count;
                    int rows = reader.ReadInt32();

                    //Get Item indices
                    int storyIDIndex = sheetHeader.IndexOf("storyID", "int");
                    int languageIDIndex = sheetHeader.IndexOf("languageID", "int");
                    int CHNIndex = sheetHeader.IndexOf("CHN", "string");
                    int selectionOneIDIndex = sheetHeader.IndexOf("selectionOneID", "int");
                    int selectionOneTextIndex = sheetHeader.IndexOf("selectionOneText", "string");
                    int selectionTwoIDIndex = sheetHeader.IndexOf("selectionTwoID", "int");
                    int selectionTwotTXTIndex = sheetHeader.IndexOf("selectionTwotTXT", "string");

                    #if UNITY_EDITOR
                    bool promptMismatchColumns = false;
                    #endif
                    for (int i = 0; i < rows; ++i)
                    {
                        Item newItem = new Item();
                        for (int j = 0; j < columns; ++j)
                        {
                            SheetHeader.Item headerItem = headerItems[j];

                            if (j == storyIDIndex)
                            {
                                newItem.storyID = reader.ReadInt32();
                            }
                            else if (j == languageIDIndex)
                            {
                                newItem.languageID = reader.ReadInt32();
                            }
                            else if (j == CHNIndex)
                            {
                                newItem.CHN = reader.ReadString();
                            }
                            else if (j == selectionOneIDIndex)
                            {
                                newItem.selectionOneID = reader.ReadInt32();
                            }
                            else if (j == selectionOneTextIndex)
                            {
                                newItem.selectionOneText = reader.ReadString();
                            }
                            else if (j == selectionTwoIDIndex)
                            {
                                newItem.selectionTwoID = reader.ReadInt32();
                            }
                            else if (j == selectionTwotTXTIndex)
                            {
                                newItem.selectionTwotTXT = reader.ReadString();
                            }
                            else
                            {
                                DataService.ReadAndDrop(reader, headerItem.valType);
                                #if UNITY_EDITOR
                                if (!promptMismatchColumns)
                                {
                                    UnityEngine.Debug.LogWarningFormat("Data sheet '{0}' find mismatch columns for '{1}({2})'.", sheetName, headerItem.name, headerItem.valType);
                                    promptMismatchColumns = true;
                                }
                                #endif
                            }
                        }
                        m_Items.Add(newItem.storyID, newItem);
                    }
                }
            }
            
        }

    }
}
    