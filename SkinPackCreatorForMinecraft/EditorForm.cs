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

        public EditorForm() {
            InitializeComponent();
        }

        public void newSkin(string path) {
            var skin = Project.NewSkin(path);

            skinEditor1.SetSkin(skin);
        }



        private void _OpenProperties(object sender, EventArgs e) {
            if (!Project.IsOpen()) return;

            var form = new PropertiesForm();
            form.setMode(PropertiesForm.Mode.EDIT_MANIFEST);
            form.ShowDialog(this);
        }


        private void _NewProject(object sender, EventArgs e) {
            var dialog = new FolderBrowserDialog();
            var result = dialog.ShowDialog();
            if (result != DialogResult.OK || string.IsNullOrWhiteSpace(dialog.SelectedPath))
                return;

            var form = new PropertiesForm();
            form.setMode(PropertiesForm.Mode.NEW_MANIFEST);
            form.setTargetFolder(dialog.SelectedPath);
            form.ShowDialog(this);
        }

        private void _OpenProject(object sender, EventArgs e) {
            var dialog = new FolderBrowserDialog();
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

            MessageBox.Show("Opened: " + folder);
        }

        private void _ExportProject(object sender, EventArgs e) {
            if (!Project.IsOpen()) return;

            var dialog = new SaveFileDialog();
            dialog.Title = "Save as...";
            dialog.Filter = "Resource pack|*.mcpack|Zip pack|*.zip";
            dialog.FileName = Project.Instance.Name;

            var result = dialog.ShowDialog();
            if (result != DialogResult.OK || string.IsNullOrWhiteSpace(dialog.FileName))
                return;

            // Export
            Project.Export(dialog.FileName);

            MessageBox.Show("Saved as: " + dialog.FileName);
        }



        private void _onClickAddSkin(object sender, EventArgs e) {
            var dialog = new OpenFileDialog();
            dialog.Title = "Select skin";
            dialog.Filter = "Skin files (*.png)|*.png";

            var result = dialog.ShowDialog();
            if (result != DialogResult.OK || string.IsNullOrWhiteSpace(dialog.FileName))
                return;

            newSkin(dialog.FileName);
        }

        private void _onDropSkin(object sender, DragEventArgs e) {
            var data = e.Data;
            if (!data.GetDataPresent(DataFormats.FileDrop)) return;

            var files = data.GetData(DataFormats.FileDrop) as string[];
            foreach (var file in files) {
                var ext = Path.GetExtension(file);
                if (!string.Equals(ext, ".png")) continue;

                newSkin(file);
                break;
            }
        }


    }

}