using Editor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkinPackCreatorForMinecraft {

    public partial class PropertiesForm : Form {
        public enum Mode { EDIT_MANIFEST, NEW_MANIFEST }
        private Mode mode = Mode.EDIT_MANIFEST;
        private string targetFolder = null;


        public PropertiesForm() {
            InitializeComponent();
        }

        public void setMode(Mode mode) {
            this.mode = mode;
        }

        public void setTargetFolder(string targetFolder) {
            this.targetFolder = targetFolder;
        }


        private void _onCreate(object sender, LayoutEventArgs e) {
            if (mode != Mode.EDIT_MANIFEST) return;

            var project = Project.Instance;
            textBox_packName.Text = project.Name;
            textBox_packUuid.Text = project.PackUUID;
            textBox_packVersion.Text = project.PackVersion;
            textBox_skinPackUuid.Text = project.SkinPackUUID;
            textBox_skinPackVersion.Text = project.SkinPackVersion;
        }


        private void _onKeyPressName(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar)
                && !char.IsLetterOrDigit(e.KeyChar)
                && !char.IsWhiteSpace(e.KeyChar)
                && (e.KeyChar != '-')
                && (e.KeyChar != '_')) {
                e.Handled = true;
            }
        }

        private void _onKeyPressVersion(object sender, KeyPressEventArgs e) {
            IgnoreNonNumbers(e);

        }

        private void _onClickPackUUID(object sender, EventArgs e) {
            textBox_packUuid.Text = Guid.NewGuid().ToString();
        }

        private void _onClickSkinPackUUID(object sender, EventArgs e) {
            textBox_skinPackUuid.Text = Guid.NewGuid().ToString();
        }

        private void _onClickSave(object sender, EventArgs e) {

        }



        private static bool IgnoreNonNumbers(KeyPressEventArgs e) {
            // Ignorar qualquer tecla que não seja numeros controles ou pontos
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && (e.KeyChar != '.')) {
                e.Handled = true;
                return true;
            }

            return false;
        }


    }


}
