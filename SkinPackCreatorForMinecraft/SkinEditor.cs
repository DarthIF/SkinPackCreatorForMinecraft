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
using Utils;

namespace SkinPackCreatorForMinecraft {

    public partial class SkinEditor : UserControl {
        private Skin skin;
        private string newImage = null;

        private Action<Skin> onChanged = null;
        private Action<Skin> onDeleted = null;


        public SkinEditor() {
            InitializeComponent();
        }

        public void SetSkin(Skin skin) {
            this.skin = skin;
            this.newImage = null;
            this.Visible = true;

            UpdateUI();
        }

        public void SetOnChanged(Action<Skin> a) {
            this.onChanged = a;
        }
        public void SetOnDeleted(Action<Skin> a) {
            this.onDeleted = a;
        }


        private void UpdateUI() {
            if (skin == null) return;

            // Nome da Skin
            textBox_name.Text = skin.LocalizationName;

            // Geometria 
            var isNormal = PackUtils.IsNormal(skin.Geometry);
            radioButton_normal.Checked = isNormal;
            radioButton_slim.Checked = !isNormal;

            // Tipo
            var isFree = PackUtils.IsFree(skin.Type);
            radioButton_free.Checked = isFree;
            radioButton_paid.Checked = !isFree;

            // Skin
            pictureBox1.Image = Project.LoadImage(skin);
        }
        private void setImage(string location) {
            pictureBox1.Image = Image.FromFile(location);

            // Salvar alterações
            this.newImage = location;
        }


        private void _onSaveClick(object sender, EventArgs e) {
            if (skin == null) return;

            // Novos valores
            skin.LocalizationName = textBox_name.Text;
            skin.Geometry = PackUtils.CalcGeometry(radioButton_normal.Checked, radioButton_slim.Checked);
            skin.Type = PackUtils.CalcType(radioButton_free.Checked, radioButton_paid.Checked);

            // Alteração na skin
            if (newImage != null) {
                Project.ReplaceTexture(newImage, skin);
                newImage = null;
            }

            // Aplicar
            Project.SaveSkins();

            // Evento
            if (onChanged != null)
                onChanged.Invoke(skin);
        }

        private void _onCloseClick(object sender, EventArgs e) {
            // Esconder
            this.Visible = false;
        }


        private void _onDeleteClick(object sender, EventArgs e) {
            // Fechar a imagem
            var image = pictureBox1.Image;
            if (image != null)
                image.Dispose();

            // Evento
            if (onDeleted != null)
                onDeleted.Invoke(skin);

            // Apagar o arquivo
            Project.DeleteSkin(skin);

            // Esconder
            this.Visible = false;
        }

        private void _onPickImage(object sender, EventArgs e) {
            if (skin == null) return;

            var dialog = new OpenFileDialog();
            dialog.Title = "Select skin";
            dialog.Filter = "Skin files (*.png)|*.png";

            var result = dialog.ShowDialog();
            if (result != DialogResult.OK || string.IsNullOrWhiteSpace(dialog.FileName))
                return;

            setImage(dialog.FileName);
        }

        private void _onDropFile(object sender, DragEventArgs e) {
            var data = e.Data;
            if (!data.GetDataPresent(DataFormats.FileDrop)) return;

            var files = data.GetData(DataFormats.FileDrop) as string[];
            foreach (var file in files) {
                var ext = Path.GetExtension(file);
                if (!string.Equals(ext, ".png")) continue;

                setImage(file);
                break;
            }

        }

    }

}
