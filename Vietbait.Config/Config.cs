using System;
using System.IO;
using System.Reflection;
using System.Xml;
using Vietbait.Config.Properties;

namespace Vietbait.Config
{
    public class Config
    {
        #region Const

        private const string StrDefaultName = "Default Name";
        private const string StrDefaultValue = "Default Value";
        private const string StrDefaultDesc = "Default Desc";
        private const string StrDefaultSetting = "DefaultSetting";
        private const string StrItem = "Item";
        private const string StrName = "Name";
        private const string StrValue = "Value";
        private const string StrDesc = "Desc";
        private const string StrDefaultConfigFilename = "App.Config";
        private const string StrDefaultSectionName = "appSettings";
        private const string StrTrue = "True";

        #endregion

        #region Attributies

        private readonly XmlDocument _xmlDoc = new XmlDocument();
        private XmlNode _workingNode;
        private readonly ItemSettings _settings = new ItemSettings();

        #endregion

        #region Properties

        public string ConfigFileName { get; set; }
        public string SectionName { get; set; }

        #endregion

        #region Contructor

        /// <summary>
        /// Khởi tạo không truyền vào tham số.
        /// ConfigFileName = "App.Config"
        /// SectionName = "appSettings"
        /// </summary>
        public Config()
        {
            ConfigFileName = StrDefaultConfigFilename;
            SectionName = StrDefaultSectionName;
            ConstructorMethod(ConfigFileName, SectionName);
        }

        /// <summary>
        /// Khởi tạo truyền vào tham số.
        /// </summary>
        /// <param name="pConfigFileName">Tên file config</param>
        /// <param name="pConfigSectionName">Section Name</param>
        public Config(string pConfigFileName, string pConfigSectionName)
        {
            ConfigFileName = pConfigFileName;
            SectionName = pConfigSectionName;
            ConstructorMethod(ConfigFileName, SectionName);
        }

        #endregion

        #region Private Method

        private void ConstructorMethod(string pConfigFileName, string pConfigSectionName)
        {
            try
            {
                if (File.Exists(pConfigFileName))
                {
                    //Load file nếu tồn tại
                    _xmlDoc.Load(pConfigFileName);
                    XmlNodeList nodeList = _xmlDoc.GetElementsByTagName(pConfigSectionName);

                    if (nodeList.Count == 0)
                    {
                        CreateSection();
                        LoadToSettings();
                    }
                        //Nếu chỉ tìm thấy 1 Node
                    else if (nodeList.Count == 1)
                    {
                        _workingNode = nodeList[0];
                        LoadToSettings();
                    }
                        //Nếu tìm thấy nhiều hơn 1 node thì tìm node có DefaultSetting = True
                    else
                    {
                        for (int i = 0; i < nodeList.Count; i++)
                        {
                            try
                            {
                                if (nodeList[i].Attributes[0] != null)
                                {
                                    if (nodeList[i].Attributes[StrDefaultSetting].Value == StrTrue)
                                    {
                                        _workingNode = nodeList[i];
                                        LoadToSettings();
                                        break;
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                    }
                }
                else //Chưa tồn tại thì gán nội dung file.
                {
                    //Create XML 
                    File.WriteAllText(pConfigFileName, Resources.DefaultContent);
                    _xmlDoc.Load(pConfigFileName);
                    CreateSection();
                    LoadToSettings();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CreateSection()
        {
            _workingNode = CreateNewSectionNode(SectionName);
            _xmlDoc.DocumentElement.AppendChild(_workingNode);
            _xmlDoc.Save(ConfigFileName);
        }

        private void LoadToSettings()
        {
            _settings.Items.Clear();
            foreach (XmlNode childNode in _workingNode.ChildNodes)
            {
                try
                {
                    _settings.Items.Add(new Item(childNode.Attributes[StrName].Value,
                                                 childNode.Attributes[StrValue].Value,
                                                 childNode.Attributes[StrDesc].Value));
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        private int CheckName(string name)
        {
            int result = -1;
            if (string.IsNullOrEmpty(name.Trim())) return result;
            try
            {
                for (int i = 0; i < _settings.Items.Count; i++)
                {
                    if (_settings.Items[i].Name.Trim().ToUpper() == name.Trim().ToUpper())
                    {
                        result = i;
                        break;
                    }
                }
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }

        private XmlNode CreateNewNode(string name, string value, string desc)
        {
            try
            {
                XmlNode newNode = _xmlDoc.CreateNode(XmlNodeType.Element, StrItem, null);
                XmlAttribute attName = _xmlDoc.CreateAttribute(StrName);
                XmlAttribute attValue = _xmlDoc.CreateAttribute(StrValue);
                XmlAttribute attDesc = _xmlDoc.CreateAttribute(StrDesc);

                attName.Value = name;
                attValue.Value = value;
                attDesc.Value = desc;

                newNode.Attributes.Append(attName);
                newNode.Attributes.Append(attValue);
                newNode.Attributes.Append(attDesc);
                return newNode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private XmlNode CreateNewSectionNode(string pSectionName)
        {
            try
            {
                XmlNode newNode = _xmlDoc.CreateElement(pSectionName);
                XmlAttribute attDefaultSetting = _xmlDoc.CreateAttribute(StrDefaultSetting);
                attDefaultSetting.Value = StrTrue;
                newNode.Attributes.Append(attDefaultSetting);
                XmlNode childNode = CreateNewNode(StrDefaultName, StrDefaultValue, StrDefaultDesc);
                newNode.AppendChild(childNode);
                return newNode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Public Method

        [Obfuscation]
        public bool Set(string name, string value, string desc)
        {
            var newItem = new Item(name, value, desc);
            XmlNode newNode = CreateNewNode(name, value, desc);


            try
            {
                //Kiểm tra name đã tồn tại hay chưa
                int index = CheckName(name);
                bool result;
                if (index > -1)
                {
                    //Remove ở Setting
                    _settings.Items.RemoveAt(index);
                    _settings.Items.Insert(index, newItem);
                    //Replace ở Nodelist
                    _workingNode.ReplaceChild(newNode, _workingNode.ChildNodes[index]);
                    result = true;
                }
                else //Nếu chưa tồn tại thì add thêm
                {
                    //Add thêm ở Settings
                    _settings.Items.Add(newItem);

                    //Add thêm ở Nodelist
                    _workingNode.AppendChild(newNode);
                    _xmlDoc.Save(ConfigFileName);
                    result = true;
                }
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Set(string name, string value)
        {
            return Set(name, value, "");
        }

        public object Get(string name)
        {
            int index = CheckName(name);
            if (index == -1) Set(name, string.Empty);
            return index > -1 ? _settings.Items[index].Value : string.Empty;
        }

        /// <summary>
        /// Xóa toàn bộ cấu hình
        /// </summary>
        /// <returns></returns>
        public bool Clear()
        {
            try
            {
                _workingNode.RemoveAll();
                _settings.Items.Clear();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Lưu lại cấu hình
        /// </summary>
        /// <returns>True: Lưu thành công - False:Lưu không thành công</returns>
        public bool SaveConfig()
        {
            try
            {
                _xmlDoc.Save(ConfigFileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}