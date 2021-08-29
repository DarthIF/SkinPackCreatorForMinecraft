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
using Utils;

namespace SkinPackCreatorForMinecraft {

    public partial class EditorForm : Form {
        private static readonly string FILTER = "Minecraft Resource Pack|*.mcpack|Zip file|*.zip";


        public EditorForm() {
            InitializeComponent();

            // Project Explorer
            projectExplorer1.SetOnImport((location, skin) => {
                skinEditor1.SetSkin(skin);
                UpdateProjectSkins();
            });
            projectExplorer1.SetOnSelect((index, skin) => {
                skinEditor1.SetSkin(skin);
            });

            skinEditor1.SetOnChanged(skin => {
                var skins = Project.Instance.ListSkins;
                var index = skins.IndexOf(skin);

                projectExplorer1.SetItem(index, skin.LocalizationName);
            });
            skinEditor1.SetOnDeleted(skin => {
                var skins = Project.Instance.ListSkins;
                var index = skins.IndexOf(skin);

                projectExplorer1.RemoveItem(index);
            });
        }


        public void UpdateProjectSkins() {
            if (!Project.IsOpen) return;

            var skins = Project.Instance.ListSkins;
            var names = skins.Select(skin => skin.LocalizationName);

            projectExplorer1.SetItems(names);
        }


        // Eventos
        private void onProjectOpen() {
            // Visibilidade
            projectExplorer1.Visible = true;

            // Mostrar as skins
            UpdateProjectSkins();
        }


        // Menus --------------------------------
        private void _OnMenuOpenFolder(object sender, EventArgs e) {
            var dialog = new FolderBrowserDialog();
            dialog.Description = "Select a project folder...";

            var result = dialog.ShowDialog();
            if (result != DialogResult.OK || string.IsNullOrWhiteSpace(dialog.SelectedPath))
                return;

            var folder = dialog.SelectedPath;
            if (!Project.IsProjectFolder(folder)) {
                MessageBox.Show("Invalid folder");
                return;
            }

            // Open
            Project.Open(folder);
            onProjectOpen();

            // Mensagem
            MessageBox.Show("Opened: \n" + folder);
        }

        private void _OnMenuOpenPackFile(object sender, EventArgs e) {
            var dialog = new OpenFileDialog();
            dialog.Title = "Select a file...";
            dialog.Filter = FILTER;

            var result = dialog.ShowDialog();
            if (result != DialogResult.OK || string.IsNullOrWhiteSpace(dialog.FileName))
                return;

            var file = dialog.FileName;
            Project.Import(file);
            onProjectOpen();

            MessageBox.Show("Opened: \n" + file);
        }

        private void _OnMenuNewProject(object sender, EventArgs e) {
            var dialog = new FolderBrowserDialog();
            dialog.Description = "Select project folder location";

            var result = dialog.ShowDialog();
            if (result != DialogResult.OK || string.IsNullOrWhiteSpace(dialog.SelectedPath))
                return;

            var form = new PropertiesForm();
            form.setMode(PropertiesForm.Mode.NEW_MANIFEST);
            form.setTargetFolder(dialog.SelectedPath);
            form.setOnProject(onProjectOpen);
            form.ShowDialog(this);
        }

        private void _OnMenuManifestEdit(object sender, EventArgs e) {
            if (!Project.IsOpen) {
                MessageBox.Show("Open a project first!");
                return;
            }

            var form = new PropertiesForm();
            form.setMode(PropertiesForm.Mode.EDIT_MANIFEST);
            form.ShowDialog(this);
        }

        private void _OnMenuExportMcpack(object sender, EventArgs e) {
            Export(2);
        }

        private void _OnMenuExportZip(object sender, EventArgs e) {
            Export(1);
        }


        // Função de exportar
        private void Export(int filterIndex) {
            if (!Project.IsOpen) return;

            var dialog = new SaveFileDialog();
            dialog.Title = "Save as...";
            dialog.Filter = FILTER;
            dialog.FilterIndex = filterIndex;
            dialog.FileName = Project.Instance.Name;

            var result = dialog.ShowDialog();
            if (result != DialogResult.OK || string.IsNullOrWhiteSpace(dialog.FileName))
                return;

            // Export
            Project.Export(dialog.FileName);

            // Mensagem
            MessageBox.Show("Saved: \n" + dialog.FileName);
        }

    }

}