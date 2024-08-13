namespace CapaPresentacion
{
    partial class Demo
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnAdd = new Button();
            btnGet = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            txtSearch = new TextBox();
            btnSearch = new Button();
            dataGridCliente = new DataGridView();
            txtNombre = new TextBox();
            txtEdad = new TextBox();
            txtDireccion = new TextBox();
            txtCorreoElectronico = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            txtID = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridCliente).BeginInit();
            SuspendLayout();
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(53, 22);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(94, 29);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnGet
            // 
            btnGet.Location = new Point(53, 62);
            btnGet.Name = "btnGet";
            btnGet.Size = new Size(94, 29);
            btnGet.TabIndex = 1;
            btnGet.Text = "Get";
            btnGet.UseVisualStyleBackColor = true;
            btnGet.Click += btnGet_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(165, 23);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(94, 29);
            btnUpdate.TabIndex = 2;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(165, 62);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(94, 29);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(473, 78);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(373, 27);
            txtSearch.TabIndex = 4;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(612, 31);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(94, 29);
            btnSearch.TabIndex = 5;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // dataGridCliente
            // 
            dataGridCliente.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridCliente.Location = new Point(12, 183);
            dataGridCliente.Name = "dataGridCliente";
            dataGridCliente.RowHeadersWidth = 51;
            dataGridCliente.Size = new Size(848, 366);
            dataGridCliente.TabIndex = 6;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(14, 145);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(155, 27);
            txtNombre.TabIndex = 7;
            // 
            // txtEdad
            // 
            txtEdad.Location = new Point(186, 145);
            txtEdad.Name = "txtEdad";
            txtEdad.Size = new Size(73, 27);
            txtEdad.TabIndex = 8;
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(274, 145);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(236, 27);
            txtDireccion.TabIndex = 9;
            // 
            // txtCorreoElectronico
            // 
            txtCorreoElectronico.Location = new Point(532, 145);
            txtCorreoElectronico.Name = "txtCorreoElectronico";
            txtCorreoElectronico.Size = new Size(314, 27);
            txtCorreoElectronico.TabIndex = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.Control;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.Location = new Point(53, 108);
            label1.Name = "label1";
            label1.Size = new Size(51, 20);
            label1.TabIndex = 11;
            label1.Text = "Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.Control;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.Location = new Point(196, 108);
            label2.Name = "label2";
            label2.Size = new Size(37, 20);
            label2.TabIndex = 12;
            label2.Text = "Age";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.Control;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.Location = new Point(359, 108);
            label3.Name = "label3";
            label3.Size = new Size(66, 20);
            label3.TabIndex = 13;
            label3.Text = "Address";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.Control;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.Location = new Point(629, 108);
            label4.Name = "label4";
            label4.Size = new Size(47, 20);
            label4.TabIndex = 14;
            label4.Text = "Email";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = SystemColors.Control;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label5.Location = new Point(274, 31);
            label5.Name = "label5";
            label5.Size = new Size(132, 20);
            label5.TabIndex = 15;
            label5.Text = "ID Update/Delete";
            // 
            // txtID
            // 
            txtID.BackColor = SystemColors.ActiveCaption;
            txtID.Location = new Point(305, 58);
            txtID.Name = "txtID";
            txtID.Size = new Size(51, 27);
            txtID.TabIndex = 16;
            // 
            // Demo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(881, 575);
            Controls.Add(txtID);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtCorreoElectronico);
            Controls.Add(txtDireccion);
            Controls.Add(txtEdad);
            Controls.Add(txtNombre);
            Controls.Add(dataGridCliente);
            Controls.Add(btnSearch);
            Controls.Add(txtSearch);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnGet);
            Controls.Add(btnAdd);
            Name = "Demo";
            Text = "Capa de Presentacion";
            ((System.ComponentModel.ISupportInitialize)dataGridCliente).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnAdd;
        private Button btnGet;
        private Button btnUpdate;
        private Button btnDelete;
        private TextBox txtSearch;
        private Button btnSearch;
        private DataGridView dataGridCliente;
        private TextBox txtNombre;
        private TextBox txtEdad;
        private TextBox txtDireccion;
        private TextBox txtCorreoElectronico;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox txtID;
    }
}
