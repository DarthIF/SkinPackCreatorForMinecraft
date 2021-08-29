using SkinPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Editor {

    public class Project : IProject {
        public static readonly string MANIFEST_FILE = "manifest.json";
        public static readonly string SKINS_FILE = "skins.json";
        public static readonly string TEXTS_DIRECTORY = "texts";
        public static readonly string LANG_FILE = "en_US.lang";
        private static Project instance;

        public string folder;
        public Manifest manifest;
        public SkinsJson skins;

        private string textsFolder;
        private string langFile;


        public Project(string folder) {
            this.folder = folder;
            this.manifest = Files.ReadManifest(pathManifest(folder));
            this.skins = Files.ReadSkins(pathSkins(folder));

            initialize();
        }
        public Project(string folder, Manifest manifest, SkinsJson skins) {
            this.folder = folder;
            this.manifest = manifest;
            this.skins = skins;

            // Salvar
            Save(folder, manifest);
            Save(folder, skins);

            initialize();
        }
        private void initialize() {
            this.textsFolder = Path.Combine(folder, TEXTS_DIRECTORY);
            this.langFile = Path.Combine(textsFolder, LANG_FILE);

            // Verificar a pasta "texts"
            if (!Directory.Exists(textsFolder))
                Directory.CreateDirectory(textsFolder);
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
            set => manifest.Header.Version = PackUtils.Version(value);
        }
        public string SkinPackUUID {
            get => SkinModule.Uuid;
            set => SkinModule.Uuid = value;
        }
        public string SkinPackVersion {
            get => string.Join('.', SkinModule.Version);
            set => SkinModule.Version = PackUtils.Version(value);
        }

        public Module SkinModule {
            get {
                Module skin = null;
                foreach (var module in manifest.Modules) {
                    if (!PackUtils.IsSkinModule(module)) continue;

                    skin = module;
                    break;
                }

                // Criar um modulo de skin
                if (skin == null) {
                    var version = manifest.Header.Version;
                    skin = PackUtils.CreateModule(version);

                    manifest.Modules.Add(skin);

                    Save(folder, manifest);
                }

                return skin;
            }
        }


        public List<Skin> ListSkins => skins.Skins;



        public static Project Instance => instance;

        public static bool IsOpen => instance != null;
        public static bool IsProjectFolder(string folder) {
            return File.Exists(pathManifest(folder))
                && File.Exists(pathSkins(folder));
        }

        public static bool Open(string folder) {
            instance = new Project(folder);
            return true;
        }

        // Salvar
        public static bool SaveAll() {
            if (instance == null) return false;

            Save(instance.folder, instance.manifest);
            Save(instance.folder, instance.skins);

            return true;
        }
        public static bool SaveManifest() {
            if (instance == null) return false;

            Save(instance.folder, instance.manifest);

            return true;
        }
        public static bool SaveSkins() {
            if (instance == null) return false;

            // Salvar
            Save(instance.folder, instance.skins);

            // Criar um Lang
            var lang = new LangHelper(instance.Name);
            foreach (var skin in instance.ListSkins) {
                lang.addSkin(skin);
            }

            // Salvar o lang
            Files.WriteLang(instance.langFile, lang);

            return true;
        }
        private static void Save(string folder, Manifest manifest) {
            if (manifest == null) return;

            // Salvar o .json
            Files.WriteJson(pathManifest(folder), manifest);
        }
        private static void Save(string folder, SkinsJson skins) {
            if (skins == null) return;

            // Salvar o .json
            Files.WriteJson(pathSkins(folder), skins);
        }

        // .mcpack
        public static bool Export(string targetFile) {
            // Referencia:
            // https://docs.microsoft.com/pt-br/dotnet/api/system.io.compression.zipfile.createfromdirectory?view=net-5.0

            if (instance == null) return false;

            // Salvar dados pendentes
            SaveAll();

            // Criar um arquivo zip
            var folder = instance.folder;
            ZipFile.CreateFromDirectory(folder, targetFile);

            return true;
        }
        public static bool Import(string importFile) {
            var rootDir = Path.GetDirectoryName(importFile);
            var nameDir = Path.GetFileNameWithoutExtension(importFile);
            var location = Path.Combine(rootDir, nameDir);

            // Extrair o .mcpack para uma pasta e abri-la
            ZipFile.ExtractToDirectory(importFile, location);

            return Open(location);
        }


        public static Project CreateProject(string packName = "") {
            var manifest = new Manifest();
            var skins = new SkinsJson();

            // Criar o Manifesto
            manifest.FormatVersion = 1;
            manifest.Header = new();
            manifest.Modules = new();

            manifest.Header.Name = packName;
            manifest.Header.Uuid = PackUtils.RandomUUID();
            manifest.Header.Version = new();
            PackUtils.Version(PackUtils.DEFAULT_VERSION, manifest.Header.Version);
            PackUtils.CreateModuleFor(manifest.Modules);

            // Criar o SkinsJson
            skins.Geometry = "skinpacks/skins.json";
            skins.Skins = new();
            skins.SerializeName = packName;
            skins.LocalizationName = packName;

            return new Project("", manifest, skins);
        }
        public static void SaveNewProject(Project project) {
            instance = project;

            SaveAll();
        }

        // Criar um projeto
        public static void CreateProject(ProjectBuilder builder, Manifest manifest, SkinsJson skins) {
            instance = new Project(builder.Folder, manifest, skins);

        }



        // Skins
        public static void ReplaceTexture(string soucePath, Skin skin) {
            if (instance == null) return;

            var texture = Path.Combine(instance.folder, skin.Texture);
            File.Delete(texture);
            File.Copy(soucePath, texture);
        }
        public static Skin ImportTexture(string path) {
            if (instance == null) return null;

            var fileName = Path.GetFileName(path);
            var fileNameOnly = Path.GetFileNameWithoutExtension(path);

            // Criar a skin
            var skin = new Skin();
            skin.LocalizationName = fileNameOnly;
            skin.Geometry = PackUtils.GEOMETRY_NORMAL;
            skin.Texture = fileName;
            skin.Type = PackUtils.TYPE_FREE;

            // Adicionar
            instance.skins.Skins.Add(skin);

            // Copiar
            var targetName = Path.Combine(instance.folder, fileName);
            File.Copy(path, targetName);

            // Salvar
            SaveSkins();

            return skin;
        }

        public static bool DeleteSkin(Skin skin) {
            if (instance == null) return false;

            // Remover
            instance.skins.Skins.Remove(skin);
            SaveSkins();

            // Apagar
            var targetFile = Path.Combine(instance.folder, skin.Texture);

            bool deleted = false;
            while (deleted == false) {
                try {
                    File.Delete(targetFile);
                    deleted = true;
                } catch (IOException) {
                    deleted = false;
                }
            }


            return true;
        }

        public static Image LoadImage(Skin skin) {
            if (instance == null) return null;

            var texture = Path.Combine(instance.folder, skin.Texture);
            return Image.FromFile(texture);
        }


        // Nomes dos arquivos
        private static string pathManifest(string path) {
            return Path.Combine(path, MANIFEST_FILE);
        }
        private static string pathSkins(string path) {
            return Path.Combine(path, SKINS_FILE);
        }

    }

    public interface IProject {

        string Name { get; set; }
        string PackUUID { get; set; }
        string PackVersion { get; set; }
        string SkinPackUUID { get; set; }
        string SkinPackVersion { get; set; }

    }

}