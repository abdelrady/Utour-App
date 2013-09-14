using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.DataContracts.DTOs
{
    /// <summary>
    /// DTO For The Tourist Calss
    /// 
    /// </summary>
    abstract class TouristInfo
    {

        public string UserName { get; set; }
        public string Password { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public bool? Gender { get; set; }
        public string Nationality { get; set; }
        public string Email { get; set; }
        public string Preferred_Language { get; set; }

    }
}
