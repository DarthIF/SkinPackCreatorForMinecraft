using SkinPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils {

    public static class PackUtils {
        public static string DEFAULT_VERSION => "0.0.1";
        public static string GEOMETRY_NORMAL => "geometry.humanoid.custom";
        public static string GEOMETRY_SLIM => "geometry.humanoid.customSlim";
        public static string TYPE_FREE => "free";
        public static string TYPE_PAID => "paid";
        public static string MODULE_SKIN_PACK => "skin_pack";
        

        public static bool IsNormal(string geometry) {
            return string.Equals(geometry, GEOMETRY_NORMAL);
        }
        public static bool IsSlim(string geometry) {
            return string.Equals(geometry, GEOMETRY_SLIM);
        }

        public static bool IsFree(string type) {
            return string.Equals(type, TYPE_FREE);
        }
        public static bool IsPaid(string type) {
            return string.Equals(type, TYPE_PAID);
        }

        public static string CalcGeometry(bool normal, bool slim) {
            if (normal && !slim) return GEOMETRY_NORMAL;
            if (!normal && slim) return GEOMETRY_SLIM;

            return GEOMETRY_NORMAL;
        }
        public static string CalcType(bool free, bool paid) {
            if (free && !paid) return TYPE_FREE;
            if (!free && paid) return TYPE_PAID;

            return TYPE_FREE;
        }


        public static bool IsSkinModule(Module module) {
            return string.Equals(module.Type, MODULE_SKIN_PACK);
        }

        public static Module CreateModule(List<long> version = null) {
            var module = new Module();
            module.Type = MODULE_SKIN_PACK;
            module.Description = "";
            module.Uuid = RandomUUID();
            module.Version = version != null ? version : Version(DEFAULT_VERSION);

            return module;
        }
        public static void CreateModuleFor(List<Module> modules) {
            modules.Add(CreateModule());
        }

        public static string RandomUUID() {
            return Guid.NewGuid().ToString();
        }

        public static List<long> Version(string version, List<long> list) {
            var codes = version.Split('.', 3);

            // Encher de pontos 
            foreach (var code in codes) {
                if (list.Count == 3) break;

                int nun;
                if (int.TryParse(code, out nun))
                    list.Add(nun);

            }

            // Consertar a lista
            while (list.Count != 3) {
                // Menos de 3
                if (list.Count < 3) {
                    list.Add(0);
                    continue;
                }

                // Mais de 3
                if (list.Count > 3) {
                    list.RemoveAt(list.Count - 1);
                    continue;
                }
            }

            return list;
        }
        public static List<long> Version(string version) {
            return Version(version, new());
        }

    }

}