/***********************************************
 * 作者: ZhongGuanYao
 * 邮箱: 598115778@qq.com
 * 博客: https://blog.csdn.net/xiangxinzijiwonen
 *       https://github.com/zhongguanyao
 * 日期: 2018-10-27
 * **********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using APKINFO.Entity;
using System.IO;
using System.Xml;

namespace APKINFO.Utils
{
    /// <summary>
    /// 配置工具类
    /// </summary>
    public class ConfigUtils
    {

        #region 用户配置Assets

        /// <summary>
        /// 保存用户配置文件
        /// </summary>
        /// <param name="keystore"></param>
        public static void SaveUserConfig(List<AssetsConfig> configLst)
        {
            if (configLst == null || configLst.Count <= 0) return;

            string path = PathUtils.GetPath(Constants.DIR_CONFIG);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //配置文件路径
            path = PathUtils.JoinPath(path, Constants.FILENAME_USER_CONFIG);


            //创建XmlDocument对象
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration xmlSM = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDoc.AppendChild(xmlSM);
            XmlElement xml = xmlDoc.CreateElement("", "xml", "");
            xmlDoc.AppendChild(xml);

            XmlNode xmlNode = xmlDoc.SelectSingleNode("xml");

            XmlElement fileNode, keyNode;
            foreach (AssetsConfig cfg in configLst) {
                fileNode = xmlDoc.CreateElement("file");
                fileNode.SetAttribute("name", cfg.FileName);
                if (!string.IsNullOrEmpty(cfg.Keys))
                {
                    fileNode.SetAttribute("keys", cfg.Keys);
                }
                xmlNode.AppendChild(fileNode);
            }

            //保存好创建的XML文档
            xmlDoc.Save(path);
        }


        /// <summary>
        /// 读取用户配置
        /// </summary>
        /// <returns></returns>
        public static List<AssetsConfig> ReadUserConfig() {

            string path = PathUtils.GetPath(Constants.DIR_CONFIG);
            if (!Directory.Exists(path)) return null;

            //配置文件路径
            path = PathUtils.JoinPath(path, Constants.FILENAME_USER_CONFIG);

            if (!File.Exists(path)) return null;

            XmlDocument srcXmlDoc = new XmlDocument();
            srcXmlDoc.Load(path);
            if (srcXmlDoc.DocumentElement == null || srcXmlDoc.DocumentElement.ChildNodes.Count <= 0)
                return null;

            List<AssetsConfig> configLst = new List<AssetsConfig>();
            AssetsConfig config;
            foreach (XmlElement fileNode in srcXmlDoc.DocumentElement.ChildNodes)
            {
                if (fileNode.Name.Equals("file"))
                {
                    if (fileNode.Attributes["name"] == null) continue;
                    string name = fileNode.Attributes["name"].Value;
                    if(string.IsNullOrEmpty(name)) continue;

                    config = new AssetsConfig();
                    config.FileName = name;
                    configLst.Add(config);

                    if (fileNode.Attributes["keys"] == null) continue;
                    string keys = fileNode.Attributes["keys"].Value;
                    if (string.IsNullOrEmpty(keys)) continue;
                    config.Keys = keys;
                }
            }

            return configLst;
        }

        #endregion


        #region 签名配置

        /// <summary>
        /// 保存签名配置文件
        /// </summary>
        /// <param name="config"></param>
        public static void SaveKeystoreConfig(KeystoreConfig config)
        {
            if (config == null) return;

            string path = PathUtils.GetPath(Constants.DIR_CONFIG);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //配置文件路径
            path = PathUtils.JoinPath(path, Constants.FILENAME_KEYSTORE);


            //创建XmlDocument对象
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration xmlSM = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDoc.AppendChild(xmlSM);
            XmlElement xml = xmlDoc.CreateElement("", "xml", "");
            xmlDoc.AppendChild(xml);

            XmlNode xmlNode = xmlDoc.SelectSingleNode("xml");

            XmlElement param = xmlDoc.CreateElement("param");
            param.SetAttribute("name", "keystore");
            param.SetAttribute("value", config.KeystoreFilePath);
            xmlNode.AppendChild(param);

            param = xmlDoc.CreateElement("param");
            param.SetAttribute("name", "password");
            param.SetAttribute("value", config.Password);
            xmlNode.AppendChild(param);

            param = xmlDoc.CreateElement("param");
            param.SetAttribute("name", "aliaskey");
            param.SetAttribute("value", config.Aliaskey);
            xmlNode.AppendChild(param);

            param = xmlDoc.CreateElement("param");
            param.SetAttribute("name", "aliaspwd");
            param.SetAttribute("value", config.Aliaspwd);
            xmlNode.AppendChild(param);

            //保存好创建的XML文档
            xmlDoc.Save(path);
        }

        /// <summary>
        /// 读取签名配置文件
        /// </summary>
        /// <returns></returns>
        public static KeystoreConfig ReadKeystoreConfig()
        {
            string path = PathUtils.GetPath(Constants.DIR_CONFIG);
            if (!Directory.Exists(path))
            {
                return null;
            }
            //配置文件路径
            path = PathUtils.JoinPath(path, Constants.FILENAME_KEYSTORE);

            if (!File.Exists(path)) return null;


            XmlDocument srcXmlDoc = new XmlDocument();
            srcXmlDoc.Load(path);
            if (srcXmlDoc.DocumentElement == null || srcXmlDoc.DocumentElement.ChildNodes.Count <= 0)
                return null;

            KeystoreConfig config = new KeystoreConfig();

            foreach (XmlElement p in srcXmlDoc.DocumentElement.ChildNodes)
            {
                if (p.Name.Equals("param"))
                {
                    if (string.Equals(p.Attributes["name"].Value, "keystore"))
                    {
                        config.KeystoreFilePath = p.Attributes["value"].Value;
                    } else if (string.Equals(p.Attributes["name"].Value, "password"))
                    {
                        config.Password = p.Attributes["value"].Value;
                    } else if (string.Equals(p.Attributes["name"].Value, "aliaskey"))
                    {
                        config.Aliaskey = p.Attributes["value"].Value;
                    } else if (string.Equals(p.Attributes["name"].Value, "aliaspwd"))
                    {
                        config.Aliaspwd = p.Attributes["value"].Value;
                    }
                }
            }

            if (string.IsNullOrEmpty(config.KeystoreFilePath) || !File.Exists(config.KeystoreFilePath))
                return null;

            return config;
        }

        #endregion


    }
}
