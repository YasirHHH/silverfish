﻿using log4net;
using Triton.Common.LogUtilities;

namespace HREngine.Bots
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    ///     The helpfunctions.
    /// </summary>

    public sealed class Helpfunctions
    {
        /// <summary>The logger for this type.</summary>
        private static readonly ILog Log = Logger.GetLoggerInstanceForType();

        public static List<T> TakeList<T>(IEnumerable<T> source, int limit)
        {
            List<T> retlist = new List<T>();
            int i = 0;

            foreach (T item in source)
            {
                retlist.Add(item);
                i++;

                if (i >= limit) break;
            }
            return retlist;
        }


        public bool runningbot = false;

        private static readonly Helpfunctions instance = new Helpfunctions();

        static Helpfunctions() { } // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit

        public static Helpfunctions Instance
        {
            get
            {
                return instance;
            }
        }

        private Helpfunctions()
        {

            //System.IO.File.WriteAllText(Settings.Instance.logpath + Settings.Instance.logfile, "");
        }

        private bool writelogg = true;

        public void loggonoff(bool onoff)
        {
            //writelogg = onoff;
        }

        public void createNewLoggfile()
        {
            //System.IO.File.WriteAllText(Settings.Instance.logpath + Settings.Instance.logfile, "");
        }

        public void logg(string s)
        {


            if (!writelogg) return;
            try
            {
                using (StreamWriter sw = File.AppendText(Settings.Instance.logpath + Settings.Instance.logfile))
                {
                    sw.WriteLine(s);
                }
            }
            catch
            {
            }
            //Console.WriteLine(s);
        }

        public DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public void ErrorLog(string s)
        {

            //this.logg(s);//dont needed anymore 
            Log.Info(s);
        }

        private string sendbuffer = "";

        public void resetBuffer()
        {
            this.sendbuffer = "";
        }

        public void writeToBuffer(string data)
        {
            this.sendbuffer += "\r\n" + data;
        }

        public void writeBufferToFile()
        {
            bool writed = true;
            this.sendbuffer += "<EoF>";
            while (writed)
            {
                try
                {
                    System.IO.File.WriteAllText(Settings.Instance.path + "crrntbrd.txt", this.sendbuffer);
                    writed = false;
                }
                catch
                {
                    writed = true;
                }
            }
            this.sendbuffer = "";
        }

        public void writeBufferToDeckFile()
        {
            bool writed = true;
            this.sendbuffer += "<EoF>";
            while (writed)
            {
                try
                {
                    System.IO.File.WriteAllText(Settings.Instance.path + "curdeck.txt", this.sendbuffer);
                    writed = false;
                }
                catch
                {
                    writed = true;
                }
            }
            this.sendbuffer = "";
        }

        public void writeBufferToActionFile()
        {
            bool writed = true;
            this.sendbuffer += "<EoF>";
            this.ErrorLog("write to action file: " + sendbuffer);
            while (writed)
            {
                try
                {
                    System.IO.File.WriteAllText(Settings.Instance.path + "actionstodo.txt", this.sendbuffer);
                    writed = false;
                }
                catch
                {
                    writed = true;
                }
            }
            this.sendbuffer = "";
        }
    }
}
