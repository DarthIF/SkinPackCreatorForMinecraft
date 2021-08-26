using Newtonsoft.Json;
using SkinPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utils {

    public static class Files {

        public static Manifest ReadManifest(string location) {
            var json = File.ReadAllText(location);

            return JsonConvert.DeserializeObject<Manifest>(json, Converter.Settings);
        }
        public static SkinsJson ReadSkins(string location) {
            var json = File.ReadAllText(location);

            return JsonConvert.DeserializeObject<SkinsJson>(json, Converter.Settings);
        }
        public static void WriteJson(string location, object obj) {
            var json = JsonConvert.SerializeObject(obj, Converter.Settings);

            File.WriteAllText(location, json);
        }

    }

}
