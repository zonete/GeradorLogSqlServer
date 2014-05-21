namespace LogSQLServer
{
    partial class Form1
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
            this.listChk = new System.Windows.Forms.CheckedListBox();
            this.script = new System.Windows.Forms.RichTextBox();
            this.btnGerar = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnConectar = new System.Windows.Forms.Button();
            this.txtDataSource = new System.Windows.Forms.TextBox();
            this.txtBase = new System.Windows.Forms.TextBox();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.lblMarcarTodos = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusProgBar = new System.Windows.Forms.ToolStripProgressBar();
            this.msgStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listChk
            // 
            this.listChk.Enabled = false;
            this.listChk.FormattingEnabled = true;
            this.listChk.Location = new System.Drawing.Point(12, 79);
            this.listChk.Name = "listChk";
            this.listChk.Size = new System.Drawing.Size(244, 289);
            this.listChk.TabIndex = 0;
            this.listChk.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listChk_ItemCheck);
            // 
            // script
            // 
            this.script.Enabled = false;
            this.script.Location = new System.Drawing.Point(281, 66);
            this.script.Name = "script";
            this.script.Size = new System.Drawing.Size(554, 302);
            this.script.TabIndex = 1;
            this.script.Text = "";
            this.script.TextChanged += new System.EventHandler(this.script_TextChanged);
            // 
            // btnGerar
            // 
            this.btnGerar.Enabled = false;
            this.btnGerar.Location = new System.Drawing.Point(12, 374);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(244, 44);
            this.btnGerar.TabIndex = 2;
            this.btnGerar.Text = "Gerar";
            this.btnGerar.UseVisualStyleBackColor = true;
            this.btnGerar.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(726, 374);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(109, 44);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Salvar Arquivo";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(666, 12);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(169, 48);
            this.btnConectar.TabIndex = 4;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.UseVisualStyleBackColor = true;
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // txtDataSource
            // 
            this.txtDataSource.Location = new System.Drawing.Point(12, 27);
            this.txtDataSource.Name = "txtDataSource";
            this.txtDataSource.Size = new System.Drawing.Size(162, 20);
            this.txtDataSource.TabIndex = 5;
            // 
            // txtBase
            // 
            this.txtBase.Location = new System.Drawing.Point(180, 27);
            this.txtBase.Name = "txtBase";
            this.txtBase.Size = new System.Drawing.Size(156, 20);
            this.txtBase.TabIndex = 6;
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(342, 27);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(156, 20);
            this.txtUsuario.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Data Source (Local Servidor)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(177, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Base de Dados";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(339, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Usuário";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(501, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Senha";
            // 
            // txtSenha
            // 
            this.txtSenha.Location = new System.Drawing.Point(504, 27);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.PasswordChar = '*';
            this.txtSenha.Size = new System.Drawing.Size(156, 20);
            this.txtSenha.TabIndex = 12;
            // 
            // lblMarcarTodos
            // 
            this.lblMarcarTodos.AutoSize = true;
            this.lblMarcarTodos.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblMarcarTodos.Location = new System.Drawing.Point(12, 63);
            this.lblMarcarTodos.Name = "lblMarcarTodos";
            this.lblMarcarTodos.Size = new System.Drawing.Size(73, 13);
            this.lblMarcarTodos.TabIndex = 13;
            this.lblMarcarTodos.Tag = "1";
            this.lblMarcarTodos.Text = "Marcar Todos";
            this.lblMarcarTodos.Click += new System.EventHandler(this.label5_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msgStatus,
            this.StatusProgBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 419);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(862, 22);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusProgBar
            // 
            this.StatusProgBar.Name = "StatusProgBar";
            this.StatusProgBar.Size = new System.Drawing.Size(700, 16);
            this.StatusProgBar.Visible = false;
            // 
            // msgStatus
            // 
            this.msgStatus.Name = "msgStatus";
            this.msgStatus.Size = new System.Drawing.Size(59, 17);
            this.msgStatus.Text = "Progresso";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 441);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblMarcarTodos);
            this.Controls.Add(this.txtSenha);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.txtBase);
            this.Controls.Add(this.txtDataSource);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnGerar);
            this.Controls.Add(this.script);
            this.Controls.Add(this.listChk);
            this.Name = "Form1";
            this.Text = "Gerar Log SQL Server by Davi Zonete";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox listChk;
        private System.Windows.Forms.RichTextBox script;
        private System.Windows.Forms.Button btnGerar;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.TextBox txtDataSource;
        private System.Windows.Forms.TextBox txtBase;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.Label lblMarcarTodos;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel msgStatus;
        private System.Windows.Forms.ToolStripProgressBar StatusProgBar;
    }
}

