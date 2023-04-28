namespace SanyaCountryView
{
    partial class FormBuilding
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelPrice = new System.Windows.Forms.Label();
            this.labelSquare = new System.Windows.Forms.Label();
            this.textBoxPrice = new System.Windows.Forms.TextBox();
            this.textBoxSquare = new System.Windows.Forms.TextBox();
            this.labelDateCreated = new System.Windows.Forms.Label();
            this.textBoxDateCreated = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 23);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(62, 15);
            this.labelName.TabIndex = 4;
            this.labelName.Text = "Название:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(99, 20);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(303, 23);
            this.textBoxName.TabIndex = 5;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(246, 206);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(327, 206);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelPrice
            // 
            this.labelPrice.AutoSize = true;
            this.labelPrice.Location = new System.Drawing.Point(12, 72);
            this.labelPrice.Name = "labelPrice";
            this.labelPrice.Size = new System.Drawing.Size(38, 15);
            this.labelPrice.TabIndex = 8;
            this.labelPrice.Text = "Цена:";
            // 
            // labelSquare
            // 
            this.labelSquare.AutoSize = true;
            this.labelSquare.Location = new System.Drawing.Point(12, 121);
            this.labelSquare.Name = "labelSquare";
            this.labelSquare.Size = new System.Drawing.Size(62, 15);
            this.labelSquare.TabIndex = 9;
            this.labelSquare.Text = "Площадь:";
            // 
            // textBoxPrice
            // 
            this.textBoxPrice.Location = new System.Drawing.Point(99, 72);
            this.textBoxPrice.Name = "textBoxPrice";
            this.textBoxPrice.Size = new System.Drawing.Size(303, 23);
            this.textBoxPrice.TabIndex = 10;
            // 
            // textBoxSquare
            // 
            this.textBoxSquare.Location = new System.Drawing.Point(99, 118);
            this.textBoxSquare.Name = "textBoxSquare";
            this.textBoxSquare.Size = new System.Drawing.Size(303, 23);
            this.textBoxSquare.TabIndex = 11;
            // 
            // labelDateCreated
            // 
            this.labelDateCreated.AutoSize = true;
            this.labelDateCreated.Location = new System.Drawing.Point(12, 169);
            this.labelDateCreated.Name = "labelDateCreated";
            this.labelDateCreated.Size = new System.Drawing.Size(88, 15);
            this.labelDateCreated.TabIndex = 12;
            this.labelDateCreated.Text = "Дата создания:";
            // 
            // textBoxDateCreated
            // 
            this.textBoxDateCreated.Location = new System.Drawing.Point(106, 166);
            this.textBoxDateCreated.Name = "textBoxDateCreated";
            this.textBoxDateCreated.ReadOnly = true;
            this.textBoxDateCreated.Size = new System.Drawing.Size(296, 23);
            this.textBoxDateCreated.TabIndex = 13;
            // 
            // FormBuilding
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 241);
            this.Controls.Add(this.textBoxDateCreated);
            this.Controls.Add(this.labelDateCreated);
            this.Controls.Add(this.textBoxSquare);
            this.Controls.Add(this.textBoxPrice);
            this.Controls.Add(this.labelSquare);
            this.Controls.Add(this.labelPrice);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelName);
            this.Name = "FormBuilding";
            this.Text = "Строение";
            this.Load += new System.EventHandler(this.FormBuilding_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label labelName;
        private TextBox textBoxName;
        private Button buttonSave;
        private Button buttonCancel;
        private Label labelPrice;
        private Label labelSquare;
        private TextBox textBoxPrice;
        private TextBox textBoxSquare;
        private Label labelDateCreated;
        private TextBox textBoxDateCreated;
    }
}