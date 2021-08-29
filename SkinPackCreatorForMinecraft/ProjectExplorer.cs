using Editor;
using SkinPack;
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

    public partial class ProjectExplorer : UserControl {
        private Action<string, Skin> onImport = null;
        private Action<int, Skin> onSelect = null;

        public ProjectExplorer() {
            InitializeComponent();
        }

        public void SetOnImport(Action<string, Skin> a) {
            this.onImport = a;
        }
        public void SetOnSelect(Action<int, Skin> a) {
            this.onSelect = a;
        }


        public void SetItems(IEnumerable<string> list) {
            listBox1.BeginUpdate();
            listBox1.Items.Clear();
            listBox1.Items.AddRange(list.ToArray());
            listBox1.EndUpdate();
        }
        public void SetItem(int index, string name) {
            listBox1.BeginUpdate();
            listBox1.Items[index] = name;
            listBox1.EndUpdate();
        }
        public void RemoveItem(int index) {
            listBox1.BeginUpdate();
            listBox1.Items.RemoveAt(index);
            listBox1.EndUpdate();
        }


        private void import(string location) {
            var skin = Project.ImportTexture(location);

            // Evento
            if (onImport != null)
                onImport.Invoke(location, skin);
        }


        private void _onClickAddSkin(object sender, EventArgs e) {
            // Adicionar no projeto
            var dialog = new OpenFileDialog();
            dialog.Title = "Select skin";
            dialog.Filter = "Skin files (*.png)|*.png";

            var result = dialog.ShowDialog();
            if (result != DialogResult.OK || string.IsNullOrWhiteSpace(dialog.FileName))
                return;

            import(dialog.FileName);
        }


        private void _OnSelectedChanged(object sender, EventArgs e) {
            var index = listBox1.SelectedIndex;
            if (index < 0) return;

            var skins = Project.Instance.ListSkins;
            var skin = skins[index];

            if (onSelect != null)
                onSelect.Invoke(index, skin);
        }

        private void _OnDropFile(object sender, DragEventArgs e) {
            var data = e.Data;
            if (!data.GetDataPresent(DataFormats.FileDrop)) return;

            var files = data.GetData(DataFormats.FileDrop) as string[];
            foreach (var file in files) {
                var ext = Path.GetExtension(file);
                if (!string.Equals(ext, ".png")) continue;

                import(file);
                break;
            }
        }

    }

}
