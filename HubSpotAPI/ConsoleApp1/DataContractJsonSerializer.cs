using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;


namespace WebAPIClientHubspot {
    internal class DataContractJsonSerializer {
        private Type type;

        public DataContractJsonSerializer(Type type) {
            this.type = type;
        }

        internal List<Repo> ReadObject(Stream stream) {
            throw new NotImplementedException();
        }

        [DataContract(Name = "repo")]
        public class Repository { }

    }


}