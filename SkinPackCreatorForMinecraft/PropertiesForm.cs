using Editor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkinPackCreatorForMinecraft {

    public partial class PropertiesForm : Form {
        private Mode mode = Mode.EDIT_MANIFEST;
        private string targetFolder = null;
        private Action onProject = null;


        public PropertiesForm() {
            InitializeComponent();

        }

        public void setMode(Mode mode) {
            this.mode = mode;
        }
        public void setTargetFolder(string targetFolder) {
            this.targetFolder = targetFolder;
        }
        public void setOnProject(Action onProject) {
            this.onProject = onProject;
        }


        private void setData(IProject project) {
            project.Name = textBox_packName.Text;
            project.PackUUID = textBox_packUuid.Text;
            project.PackVersion = textBox_packVersion.Text;
            project.SkinPackUUID = textBox_skinPackUuid.Text;
            project.SkinPackVersion = textBox_skinPackVersion.Text;
        }


        private void onCreateNew() {
            ProjectBuilder.Begin();
            ProjectBuilder.Instance.Folder = targetFolder;

            // Criar novo
            textBox_packName.Text = "";
            textBox_packUuid.Text = Guid.NewGuid().ToString();
            textBox_packVersion.Text = "0.0.1";
            textBox_skinPackUuid.Text = Guid.NewGuid().ToString();
            textBox_skinPackVersion.Text = "0.0.1";
        }
        private void onCreateProject() {
            // Editar o manifesto
            var project = Project.Instance;
            textBox_packName.Text = project.Name;
            textBox_packUuid.Text = project.PackUUID;
            textBox_packVersion.Text = project.PackVersion;
            textBox_skinPackUuid.Text = project.SkinPackUUID;
            textBox_skinPackVersion.Text = project.SkinPackVersion;
        }

        private void onSaveNew() {
            var builder = ProjectBuilder.Instance;

            // Definir os dados
            setData(builder);

            // Criar a pasta do projeto
            builder.Folder = Path.Combine(builder.Folder, builder.Name);
            builder.CreateDirectory();

            // Completar
            ProjectBuilder.End();

            // Evento
            if (onProject != null)
                onProject.Invoke();
        }
        private void onSaveProject() {
            setData(Project.Instance);

            Project.SaveAll();
        }


        protected override void OnVisibleChanged(EventArgs e) {
            base.OnVisibleChanged(e);
            if (mode == Mode.NEW_MANIFEST)
                onCreateNew();
            else if (mode == Mode.EDIT_MANIFEST)
                onCreateProject();
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
            if (mode == Mode.NEW_MANIFEST)
                onSaveNew();
            else if (mode == Mode.EDIT_MANIFEST)
                onSaveProject();

            Close();
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


        public enum Mode { EDIT_MANIFEST, NEW_MANIFEST }

    }


}
