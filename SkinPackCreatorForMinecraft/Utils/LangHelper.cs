using SkinPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils {

    public class LangHelper : IEnumerable<string> {
        private string pack;
        private List<string> lines;

        public LangHelper(string pack) {
            this.pack = pack;
            this.lines = new();

            push("skinpack.{0}={0}", pack);
        }

        private void push(string format, params object[] args) {
            lines.Add(string.Format(format, args));
        }

        public void addSkin(Skin skin) {
            push("skin.{0}.{1}={1}", pack, skin.LocalizationName);
        }


        public IEnumerator<string> GetEnumerator() {
            return lines.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

    }

}
