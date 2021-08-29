using SkinPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Editor {

    public class ProjectBuilder : IProject {
        private static ProjectBuilder instance;

        public string Folder { get; set; }
        public string Name { get; set; }
        public string PackUUID { get; set; }
        public string PackVersion { get; set; }
        public string SkinPackUUID { get; set; }
        public string SkinPackVersion { get; set; }

        public void CreateDirectory() {
            Directory.CreateDirectory(Folder);
        }



        public static ProjectBuilder Instance => instance;

        public static void Begin() {
            instance = new ProjectBuilder();
        }
        public static void End() {
            if (instance == null) return;

            var manifest = NewManifest(instance);
            var skins = NewSkinsJson(instance);
            Project.CreateProject(instance, manifest, skins);
        }

        private static Manifest NewManifest(ProjectBuilder builder) {
            var manifest = new Manifest();

            // Criar o Manifesto
            manifest.FormatVersion = 1;
            manifest.Header = new();
            manifest.Modules = new();

            // Header
            manifest.Header.Name = builder.Name;
            manifest.Header.Uuid = builder.PackUUID;
            manifest.Header.Version = PackUtils.Version(builder.PackVersion);

            // Module
            var module = new Module();
            module.Type = PackUtils.MODULE_SKIN_PACK;
            module.Description = string.Empty;
            module.Uuid = builder.SkinPackUUID;
            module.Version = PackUtils.Version(builder.SkinPackVersion);

            manifest.Modules.Add(module);

            return manifest;
        }
        private static SkinsJson NewSkinsJson(ProjectBuilder builder) {
            var skins = new SkinsJson();

            // Criar o SkinsJson
            skins.Geometry = "skinpacks/skins.json";
            skins.Skins = new();
            skins.SerializeName = builder.Name;
            skins.LocalizationName = builder.Name;

            return skins;
        }

    }

}