using Newtonsoft.Json;
using System.Collections.Generic;

namespace ConsoleApp1.Models {
    public class Repo {
        public string name;
    }
    public class Firstname {
        public string value { get; set; }
    }

    public class Lastname {
        public string value { get; set; }
    }
    

    public class Properties {
        public Firstname firstname { get; set; }
        public Lastname lastname { get; set; }
    }

    public class Contact{
        public Properties properties { get; set; }
        public int vid { get; set; }

        public bool IsDuplicateOf(Contact o2) {
            var o1 = this;
            if (o1.IsAnonymous() || o2.IsAnonymous()) {
                return false;
            }
            return o1.properties?.firstname?.value == o2.properties?.firstname?.value
                && o1.properties?.lastname?.value == o2.properties?.lastname?.value
                && o1.vid != o2.vid;
        }

        public bool IsAnonymous() {
            return properties.firstname == null && properties.lastname == null;
        }

        public override string ToString() {
            return $"{properties.firstname?.value} {properties.lastname?.value} ({vid})";
        }
    }

    public class RootObject {
        public IEnumerable<Contact> contacts { get; set; }
        [JsonProperty("has-more")]
        public bool HasMore { get; set; }
        [JsonProperty("vid-offset")]
        public int VidOffset { get; set; }
    }

}
