using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPhoneBook.Classes
{
    public class General
    {
        public static string ApplicationTitle => $"{Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyTitleAttribute>().Title} v{Assembly.GetExecutingAssembly().GetName().Version}";
        public static string PhoneBookPath => ConfigurationManager.AppSettings["PhoneBookPath"];
        public static DialogResult ShowMessage(string message, ShowMessageType showMessageType)
        {
            MessageBoxButtons messageBoxButtons;
            MessageBoxIcon messageBoxIcon;

            switch (showMessageType)
            {
                case ShowMessageType.Error:
                    messageBoxButtons = MessageBoxButtons.OK;
                    messageBoxIcon = MessageBoxIcon.Error;
                    message = "ERROR: " + message;
                    Log.Error(message);                    
                    break;
                case ShowMessageType.Confirmation:
                    messageBoxButtons = MessageBoxButtons.YesNo;
                    messageBoxIcon = MessageBoxIcon.Question;
                    break;
                default:
                    messageBoxButtons = MessageBoxButtons.OK;
                    messageBoxIcon = MessageBoxIcon.Information;
                    break;
            }

            return MessageBox.Show(message, $@"{ApplicationTitle} {DateTime.Now:dd MMM yyyy HH:mm:ss}", messageBoxButtons, messageBoxIcon);
        }
        public static Logger Log => LogManager.GetCurrentClassLogger();
        public enum FormAction
        {
            Add,
            Modify,
            Delete
        }
        public enum ShowMessageType
        {
            Information,
            Error,
            Confirmation
        }
    }
}
