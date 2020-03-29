using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Configuration;
using System.Xml;
using Microsoft.Win32;
using System.Reflection;
using System.Globalization;

namespace hw4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void text_color(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AllowFullOpen = false;
            cd.AnyColor = false;
            cd.SolidColorOnly = true;
            cd.ShowDialog();
            textBox1.ForeColor = cd.Color;
            UpdateCurrentSettings();
        }

        private void bg_color(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AllowFullOpen = false;
            cd.AnyColor = false;
            cd.SolidColorOnly = true;
            cd.ShowDialog();
            textBox1.BackColor = cd.Color;
            UpdateCurrentSettings();
        }

        private void font_style(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.ShowDialog();
            textBox1.Font = fd.Font;
            UpdateCurrentSettings();
        }

        private void save_to_config(object sender, EventArgs e)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                settings["BackGroundColor"].Value = textBox1.BackColor.Name;
                settings["TextColor"].Value = textBox1.ForeColor.Name;
                settings["TextSize"].Value = textBox1.Font.Size.ToString();
                settings["TextFont"].Value = textBox1.Font.Name;
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                MessageBox.Show("fuck");
            }
            textBox2.Text = string.Format($"Background color: {textBox1.BackColor.Name}" + Environment.NewLine +
                $"Text color: {textBox1.ForeColor.Name}" + Environment.NewLine +
                $"Text size: {textBox1.Font.SizeInPoints.ToString()}" + Environment.NewLine +
                $"Text font: {textBox1.Font.Name}");
        }

        private void load_from_config(object sender, EventArgs e)
        {
            NameValueCollection LoadFromConfig = ConfigurationManager.AppSettings;
            MessageBox.Show(LoadFromConfig.Get("BackGroundColor").ToString() + " " +
                LoadFromConfig.Get("TextColor").ToString() + " " +
                LoadFromConfig.Get("TextSize").ToString() + " " +
                LoadFromConfig.Get("TextFont").ToString());
            textBox1.BackColor = Color.FromName(LoadFromConfig["BackGroundColor"]);
            textBox1.ForeColor = Color.FromName(LoadFromConfig["TextColor"]);
            float TextSize = float.Parse(LoadFromConfig["TextSize"]);
            textBox1.Text = float.Parse(LoadFromConfig["TextSize"]).ToString();
            string TextFont = LoadFromConfig["TextFont"];
            textBox1.Font = new Font(TextFont, TextSize, GraphicsUnit.Point);
            UpdateCurrentSettings();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "Hello";
            load_from_config(sender, e);
            save_to_config(sender, e);
            UpdateRegistrSettings();
            UpdateCurrentSettings();
        }

        public void UpdateCurrentSettings()
        {
            textBox3.Text = string.Format($"Background color: {textBox1.BackColor.Name}" + Environment.NewLine +
                $"Text color: {textBox1.ForeColor.Name}" + Environment.NewLine +
                $"Text size: {textBox1.Font.SizeInPoints.ToString()}" + Environment.NewLine +
                $"Text font: {textBox1.Font.Name}");
        }

        public void UpdateRegistrSettings()
        {
            RegistryKey key = Registry.CurrentUser;
            RegistryKey subKey = key.OpenSubKey(@"Software\hw");
            try
            {
                textBox4.Text = string.Format($"Background color: {subKey.GetValue("BackGroundColor").ToString()}" + Environment.NewLine +
                $"Text color: {subKey.GetValue("TextColor").ToString()}" + Environment.NewLine +
                $"Text size: {subKey.GetValue("TextSize").ToString()}" + Environment.NewLine +
                $"Text font: {subKey.GetValue("TextFont").ToString()}");
                subKey.Close();
            }
            catch (Exception i)
            {
                textBox4.Text = i.Message;
            }
        }

        private void save_to_registr(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser;
            RegistryKey subKey = key.CreateSubKey(@"Software\hw", true);
            subKey.SetValue("BackGroundColor", textBox1.BackColor.Name);
            subKey.SetValue("TextColor", textBox1.ForeColor.Name);
            subKey.SetValue("TextSize", textBox1.Font.Size);
            subKey.SetValue("TextFont", textBox1.Font.Name);
            subKey.Close();
            UpdateRegistrSettings();
        }

        private void load_from_registr(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser;
            RegistryKey subKey = key.OpenSubKey(@"Software\hw");
            try
            {
                textBox1.BackColor = Color.FromName(subKey.GetValue("BackGroundColor").ToString());
                textBox1.ForeColor = Color.FromName(subKey.GetValue("TextColor").ToString());
                string textfont = subKey.GetValue("TextFont").ToString();
                float textsize = float.Parse(subKey.GetValue("TextSize").ToString());
                textBox1.Font = new Font(textfont, textsize, GraphicsUnit.Point);
                subKey.Close();
                UpdateRegistrSettings();
            }
            catch (Exception i)
            {
                MessageBox.Show(i.Message);
            }
        }

        private void clearing_registr(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser;
            key.DeleteSubKey(@"Software\hw");
            UpdateRegistrSettings();
        }
    }
}
