using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Service.MyDebug
{
    public enum DebugVerbocity
    {
        Exception, Warning, Informational
    }

    /// <summary>
    /// A simple class that allows toggling debugging to files/console on and off
    /// </summary>
    public class MyDebugger
    {
        #region fields
        public event EventHandler<MessageLoggedEventArgs> MessageLoggedEvent;

        private bool _shouldWriteToFile = true;
        private bool _shouldWriteToConsole = true;
        private DebugVerbocity _selectedVerbocity = DebugVerbocity.Informational;
        private static MyDebugger _INSTANCE = null;
        #endregion

        #region properties
        // The singleton instance
        public static MyDebugger Instance
        {
            get
            {
                if (_INSTANCE == null)
                {
                    _INSTANCE = new MyDebugger();
                }

                return _INSTANCE;
            }
        }
        // Should the debugger write new messages to file
        public bool ShouldWriteToFile
        {
            get
            {
                return _shouldWriteToFile;
            }
            set
            {
                if (_shouldWriteToFile != value)
                {
                    _shouldWriteToFile = value;
                }
            }
        }

        // Should the debugger write new messages to console
        public bool ShouldWriteToConsole
        {
            get
            {
                return _shouldWriteToConsole;
            }
            set
            {
                if (_shouldWriteToConsole != value)
                {
                    _shouldWriteToConsole = value;
                }
            }
        }

        public DebugVerbocity SelectedVerbocity
        {
            get
            {
                return _selectedVerbocity;
            } set
            {
                if (_selectedVerbocity != value)
                {
                    _selectedVerbocity = value;
                }
            }
        }
        #endregion

        #region constructor
        private MyDebugger()
        {

        }
        #endregion

        #region method

        /// <summary>
        /// Create a new entry in the logs
        /// </summary>
        /// <param name="message">The message to log (exception or string object)</param>
        /// <param name="verbocity">The level of verbocity this message is mapped to</param>
        public void LogMessage(object message, DebugVerbocity verbocity)
        {
            try
            {
                if (verbocity <= _selectedVerbocity)
                {
                    string logEntry = BuildLogEntry(message, verbocity);
                    if (_shouldWriteToFile)
                        WriteToFile(logEntry);
                    if (_shouldWriteToConsole)
                        Console.WriteLine(logEntry);

                    // raise event so listeners can do what they like with the log entry
                    OnMessageLogged(logEntry);
                }
            } catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }

        private void WriteToFile(string logEntry)
        {
            string fileName = "LogEntry-" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

            if (!File.Exists(fileName))
            {
                File.Create(fileName);
            }

            using (StreamWriter sw = File.AppendText(fileName))
            {
                sw.WriteLine(logEntry);
            }
        }

        /// <summary>
        /// Add a date and symbol to the message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="verbocity"></param>
        /// <returns></returns>
        private string BuildLogEntry(object message, DebugVerbocity verbocity)
        {
            string information = "";
            string datetime = DateTime.Now.ToString("MMM dd yyyy hh:mm:ss");
            string severitySymbol = "";

            if (message is Exception)
            {
                Exception exceptionEntry = (Exception)message;
                information = exceptionEntry.Message;
            }
            else if (message is string)
            {
                information = (string)message;
            }

            switch (verbocity)
            {
                case (DebugVerbocity.Informational):
                    {
                        severitySymbol = "+";
                        break;
                    }
                case (DebugVerbocity.Warning):
                    {
                        severitySymbol = "!";
                        break;
                    }
                case (DebugVerbocity.Exception):
                    {
                        severitySymbol = "-";
                        break;
                    }
            }
            return "["+severitySymbol+"][" + datetime + "]" + information;
        }

        public void OnMessageLogged(string logEntry)
        {
            if (MessageLoggedEvent != null) MessageLoggedEvent(this, new MessageLoggedEventArgs()
            {
                LogEntry = logEntry
            });
        }

        #endregion
    }

    public class MessageLoggedEventArgs
    {
        public string LogEntry;
    }
}
