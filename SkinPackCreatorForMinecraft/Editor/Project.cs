using SkinPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Editor {

    public class Project {
        private static Project instance;

        public string folder;
        public Manifest manifest;
        public SkinsJson skins;

        public Project(string folder) {
            this.folder = folder;
            this.manifest = Files.ReadManifest(fileManifest(folder));
            this.skins = Files.ReadSkins(fileSkins(folder));
        }

        public Project(string folder, Manifest manifest, SkinsJson skins) {
            this.folder = folder;
            this.manifest = manifest;
            this.skins = skins;

            // Salvar
            Save(folder, manifest, skins);
        }


        public string Name {
            get => manifest.Header.Name;
            set {
                manifest.Header.Name = value;
                skins.SerializeName = value;
                skins.LocalizationName = value;
            }
        }
        public string PackUUID {
            get => manifest.Header.Uuid;
            set => manifest.Header.Uuid = value;
        }
        public string PackVersion {
            get => string.Join('.', manifest.Header.Version);
        }
        public string SkinPackUUID {
            get {
                var first = manifest.Modules.First();
                return first.Uuid;
            }
        }
        public string SkinPackVersion {
            get {
                var first = manifest.Modules.First();
                return string.Join('.', first.Version);
            }
        }




        public static Project Instance => instance;

        public static bool IsProjectFolder(string folder) {
            return File.Exists(fileManifest(folder))
                && File.Exists(fileSkins(folder));
        }
        public static bool IsOpen() {
            return instance != null;
        }

        public static bool Open(string folder) {
            instance = new Project(folder);
            return true;
        }

        public static bool Save() {
            if (instance == null) return false;

            Save(instance.folder, instance.manifest, instance.skins);

            return true;
        }
        private static void Save(string folder, Manifest manifest, SkinsJson skins) {
            Files.WriteJson(fileManifest(folder), manifest);
            Files.WriteJson(fileSkins(folder), skins);

        }

        public static bool Export(string targetFile) {
            // Referencia:
            // https://docs.microsoft.com/pt-br/dotnet/api/system.io.compression.zipfile.createfromdirectory?view=net-5.0

            if (instance == null) return false;

            // Salvar dados pendentes
            Save();

            // Criar um arquivo zip
            var folder = instance.folder;
            ZipFile.CreateFromDirectory(folder, targetFile);

            return true;
        }

        public static bool NewProject(string folder, string packName) {
            var manifest = new Manifest();
            var skins = new SkinsJson();

            // Criar o Manifesto
            manifest.FormatVersion = 1;
            manifest.Header = new();
            manifest.Modules = new();

            manifest.Header.Name = packName;
            manifest.Header.Uuid = NewUUID();
            manifest.Header.Version = new();
            FillVersion("1.0.0", manifest.Header.Version);
            manifest.Modules.Add(NewModule());

            // Criar o SkinsJson
            skins.Geometry = "skinpacks/skins.json";
            skins.Skins = new();
            skins.SerializeName = packName;
            skins.LocalizationName = packName;

            instance = new Project(folder, manifest, skins);
            return true;
        }

        private static void FillVersion(string version, List<long> list) {
            var codes = version.Split('.', 3);

            int count = 0;
            foreach (var code in codes) {
                if (count == 3) break;

                int nun;
                if (int.TryParse(code, out nun)) {
                    list.Add(nun);
                    count++;
                }
            }

            if (count == 3) return;

            while (count < 3) {
                list.Add(0);
                count++;
            }
        }
        private static Module NewModule() {
            var module = new Module();
            module.Type = "skin_pack";
            module.Uuid = NewUUID();
            module.Version = new();
            FillVersion("1.0.0", module.Version);

            return module;
        }
        private static string NewUUID() {
            return Guid.NewGuid().ToString();
        }

        // Nomes dos arquivos
        private static string fileManifest(string path) {
            return $"{path}\\manifest.json";
        }
        private static string fileSkins(string path) {
            return $"{path}\\skins.json";
        }

    }

}