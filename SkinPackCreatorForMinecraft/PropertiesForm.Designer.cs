
namespace SkinPackCreatorForMinecraft {
    partial class PropertiesForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.button1 = new System.Windows.Forms.Button();
            this.textBox_packName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_packUuid = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_packVersion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_skinPackUuid = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_skinPackVersion = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(297, 310);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this._onClickSave);
            // 
            // textBox_packName
            // 
            this.textBox_packName.Location = new System.Drawing.Point(13, 31);
            this.textBox_packName.Name = "textBox_packName";
            this.textBox_packName.Size = new System.Drawing.Size(359, 23);
            this.textBox_packName.TabIndex = 1;
            this.textBox_packName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._onKeyPressName);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Pack name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Pack UUID";
            // 
            // textBox_packUuid
            // 
            this.textBox_packUuid.Enabled = false;
            this.textBox_packUuid.Location = new System.Drawing.Point(13, 80);
            this.textBox_packUuid.Name = "textBox_packUuid";
            this.textBox_packUuid.Size = new System.Drawing.Size(278, 23);
            this.textBox_packUuid.TabIndex = 4;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(297, 80);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "New UUID";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this._onClickPackUUID);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "* Version";
            // 
            // textBox_packVersion
            // 
            this.textBox_packVersion.Location = new System.Drawing.Point(13, 129);
            this.textBox_packVersion.MaxLength = 16;
            this.textBox_packVersion.Name = "textBox_packVersion";
            this.textBox_packVersion.Size = new System.Drawing.Size(359, 23);
            this.textBox_packVersion.TabIndex = 7;
            this.textBox_packVersion.Text = "0.0.1";
            this.textBox_packVersion.WordWrap = false;
            this.textBox_packVersion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._onKeyPressVersion);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 314);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(260, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "* Versions must have the following pattern: x.x.x";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 159);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Skin pack UUID";
            // 
            // textBox_skinPackUuid
            // 
            this.textBox_skinPackUuid.Enabled = false;
            this.textBox_skinPackUuid.Location = new System.Drawing.Point(13, 178);
            this.textBox_skinPackUuid.Name = "textBox_skinPackUuid";
            this.textBox_skinPackUuid.Size = new System.Drawing.Size(278, 23);
            this.textBox_skinPackUuid.TabIndex = 10;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(297, 178);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 11;
            this.button3.Text = "New UUID";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this._onClickSkinPackUUID);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 208);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 15);
            this.label6.TabIndex = 12;
            this.label6.Text = "* Skin pack Version";
            // 
            // textBox_skinPackVersion
            // 
            this.textBox_skinPackVersion.Location = new System.Drawing.Point(13, 227);
            this.textBox_skinPackVersion.Name = "textBox_skinPackVersion";
            this.textBox_skinPackVersion.Size = new System.Drawing.Size(359, 23);
            this.textBox_skinPackVersion.TabIndex = 13;
            this.textBox_skinPackVersion.Text = "0.0.1";
            this.textBox_skinPackVersion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._onKeyPressVersion);
            // 
            // PropertiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 341);
            this.Controls.Add(this.textBox_skinPackVersion);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox_skinPackUuid);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_packVersion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox_packUuid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_packName);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PropertiesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manifest properties";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox_packName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_packUuid;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_packVersion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_skinPackUuid;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_skinPackVersion;
    }
}