using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;

namespace Utilities
{
    /// <summary>
    /// This module is responsible on getting users information from active directory
    /// Developed by Ramy El-Zawahry
    /// </summary>
    public class ActiveDirectoryManager
    {

        #region Declarations

        private DirectoryEntry m_directoryEntry = null;
        private DirectorySearcher m_directorySearcher = null;

        #endregion Declarations

        #region Constructors

        /// <summary>
        /// Initialize DirectorySearcher object
        /// </summary>
        public ActiveDirectoryManager()
        {
            m_directoryEntry = GetDirectoryEntry();
            m_directorySearcher = new DirectorySearcher(m_directoryEntry);
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Get user's mail from active directory
        /// </summary>
        /// <param name="userName">user name in active directory</param>
        /// <returns>user's mail</returns>
        public string GetUserMailByUserName(string userName)
        {
            //m_directorySearcher.Filter = "(&(objectCategory=Person) (objectClass=user)(mail=*))";
            m_directorySearcher.Filter = string.Format("(&(samAccountName={0})(objectCategory=Person) (objectClass=user)(mail=*))", userName);
            m_directorySearcher.PropertiesToLoad.Add("mail");
            SearchResult sr = m_directorySearcher.FindOne();
            return (string)sr.Properties["mail"][0];
        }

        #endregion Public Methods

        #region Private Methods

        private DirectoryEntry GetDirectoryEntry()
        {
            Domain d = Domain.GetCurrentDomain();
            DirectoryEntry _directoryEntery = d.GetDirectoryEntry();
            //DirectoryEntry _directoryEntery = new DirectoryEntry();
            //_directoryEntery.Path = "LDAP://ITI-Hermes.local/DC=ITI-Hermes,DC=local";
            _directoryEntery.AuthenticationType = AuthenticationTypes.Secure;
            return _directoryEntery;
        }

        private string GetADUserGroups(string userName)
        {
            // manipulate the string to get each group separatly
            DirectorySearcher searcher = new DirectorySearcher();
            searcher.Filter = String.Format("(cn={0})", userName);
            searcher.PropertiesToLoad.Add("memberOf");
            StringBuilder groupsList = new StringBuilder();
            SearchResult sr = searcher.FindOne();
            if (sr != null)
            {
                int groupCount = sr.Properties["memberOf"].Count;

                for (int counter = 0; counter < groupCount; counter++)
                {
                    groupsList.Append((string)sr.Properties["memberOf"][counter]);
                    groupsList.Append("|");
                }
            }
            if (groupsList.Length > 0)
            {
                groupsList.Remove(groupsList.Length - 1, 1); //remove the last '|' symbol
            }
            return groupsList.ToString();
        }

        #endregion Private Methods

    }
}
