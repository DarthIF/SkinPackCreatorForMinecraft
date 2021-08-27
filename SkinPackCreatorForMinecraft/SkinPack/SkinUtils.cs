using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinPack {

    public static class SkinUtils {
        public static readonly string NORMAL_GEOMETRY = "geometry.humanoid.custom";
        public static readonly string SLIM_GEOMETRY = "geometry.humanoid.customSlim";
        public static readonly string FREE_TYPE = "free";
        public static readonly string PAID_TYPE = "paid";


        public static bool IsNormal(string geometry) {
            return string.Equals(geometry, NORMAL_GEOMETRY);
        }
        public static bool IsSlim(string geometry) {
            return string.Equals(geometry, SLIM_GEOMETRY);
        }

        public static bool IsFree(string type) {
            return string.Equals(type, FREE_TYPE);
        }
        public static bool IsPaid(string type) {
            return string.Equals(type, PAID_TYPE);
        }

        public static string CalcGeometry(bool normal, bool slim) {
            if (normal && !slim) return NORMAL_GEOMETRY;
            if (!normal && slim) return SLIM_GEOMETRY;

            return NORMAL_GEOMETRY;
        }
        public static string CalcType(bool free, bool paid) {
            if (free && !paid) return FREE_TYPE;
            if (!free && paid) return PAID_TYPE;

            return FREE_TYPE;
        }


    }

}