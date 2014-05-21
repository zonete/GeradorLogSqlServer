using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace LogSQLServer
{
    public partial class Form1 : Form
    {
        public String StringDeConexao = "Data Source=(local);Initial Catalog=Auto;User ID=admin;Password=123";

        DataTable dtTabelaColunas = new DataTable();
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtDataSource.Text = "(local)";
            txtBase.Text = "Auto";
            txtUsuario.Text = "admin";
            txtSenha.Text = "123";
            msgStatus.Text = "";

        }


        private void button1_Click(object sender, EventArgs e)
        {
            StatusProgBar.Value = 0;
            StatusProgBar.Visible = true;
            Cursor = Cursors.WaitCursor;
            msgStatus.Text = "";
            try
            {
                /*===================================================================
             * Gerar Scripts 
             *====================================================================*/
                script.Clear();
                StringBuilder aux = new StringBuilder();
                StringBuilder auxTrgCampoORiginal = new StringBuilder();
                StringBuilder auxTrgCampoORiginal1 = new StringBuilder();
                StringBuilder auxTrgCampoORiginal2 = new StringBuilder();
                StringBuilder auxTrgCampoVariavel = new StringBuilder();
                String pk;

                //Realiza o laço nos checkbox = true
                StatusProgBar.Maximum = listChk.CheckedItems.Count;
                foreach (String tabelaSelecionada in listChk.CheckedItems)
                {
                    StatusProgBar.Increment(1);
                    /* Clear Variables aux*/
                    aux.Clear();
                    auxTrgCampoORiginal.Clear();
                    auxTrgCampoORiginal1.Clear();
                    auxTrgCampoORiginal2.Clear();
                    auxTrgCampoVariavel.Clear();

                    /*Busca na DataTable  as Colunas pertencendes aquela tabelaseleciona  e salva os valores nas variaveis de apoio*/
                    foreach (DataRow row in dtTabelaColunas.Select("NOMETABELA = '" + tabelaSelecionada + "'"))
                    {
                        aux.AppendLine("       " + row["NOMECOLUNA"].ToString() + "_OLD" + retorna_tipo(row) + " NULL,");
                        aux.AppendLine("       " + row["NOMECOLUNA"].ToString() + "_NEW" + retorna_tipo(row) + " NULL,");

                        auxTrgCampoORiginal1.AppendLine("               " + row["NOMECOLUNA"].ToString() + "_OLD,");
                        auxTrgCampoORiginal2.AppendLine("               " + row["NOMECOLUNA"].ToString() + "_NEW,");

                        auxTrgCampoVariavel.AppendLine("               ,d." + row["NOMECOLUNA"].ToString() + " ");
                    }
                    auxTrgCampoORiginal.Append(auxTrgCampoORiginal1.ToString() + auxTrgCampoORiginal2.ToString());
                    auxTrgCampoVariavel.Append(auxTrgCampoVariavel.ToString().Replace(",d.", ",i."));
                    pk = Pks(tabelaSelecionada);

                    /*============================================================================================
                * START Write in script(RichTextBox)                  
                *===========================================================================================*/

                    /*============================================================================================
                * HEADER                 
                *===========================================================================================*/
                    script.AppendText("\n GO");
                    script.AppendText("\n--===========================================================");
                    script.AppendText("\n--  ORIGEM:  " + tabelaSelecionada.ToUpper());
                    script.AppendText("\n--===========================================================-");

                    /*============================================================================================
                * SCRIPT to Create table LOG_                 
                *===========================================================================================*/
                    script.AppendText("\n CREATE TABLE LOG_" + tabelaSelecionada + " ( \n ");
                    script.AppendText("       datahora datetime null, \n       operacao char(1) null, \n  ");
                    //Campos Fixos
                    script.AppendText(aux.ToString().Substring(0, aux.ToString().Length - 1) + "); \n \n");
                    // Campos das Tabelas


                    /*============================================================================================
                * SCRIPT to Create TRIGGER TRG_LOG_                 
                *===========================================================================================*/

                    script.AppendText("\n GO");
                    script.AppendText("\n-- >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>  " +
                                      "\n-- TRIGGER TRG_LOG_  " + tabelaSelecionada.ToUpper() +
                                      "\n-- <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
                    script.AppendText("\n CREATE TRIGGER TRG_LOG_" + tabelaSelecionada + " on " + tabelaSelecionada +
                                      " after INSERT,UPDATE,DELETE \n AS \n  begin \n");
                    script.AppendText("\n   declare @Type CHAR(1); " +
                                      "\n IF EXISTS (SELECT * FROM inserted )" +
                                      "\n    IF EXISTS (SELECT * FROM deleted) " +
                                      "\n           SELECT @Type = 'U'; " +
                                      "\n    ELSE " +
                                      "\n        SELECT @Type = 'I' ;" +
                                      "\n ELSE " +
                                      "\n   SELECT @Type = 'D' ;");

                    script.AppendText("\n\n-- Insert \n insert into LOG_" + tabelaSelecionada +
                                      "(\n               DATAHORA," +
                                      " \n               OPERACAO," +
                                      auxTrgCampoORiginal.ToString()
                                          .TrimEnd()
                                          .Substring(0, auxTrgCampoORiginal.ToString().TrimEnd().Length - 1) + ") " +
                                      " \n /*Value*/ \n   " +
                                      "(select \n               GETDATE(),\n               @Type \n" +
                                      auxTrgCampoVariavel.ToString() + " from INSERTED as i  full join deleted as d " +
                                      pk + ");");
                    script.AppendText("\n end; \n" +
                                      "--# # # # # # # # # # # # #  _E_N_D_  # # # # # # # # # # # # # # # # # # # #" +
                                      "\n\n");

                /*============================================================================================
                * END Write in script(RichTextBox)                  
                *===========================================================================================*/
                }
                msgStatus.Text = "Pronto";
            }
            catch (Exception erroException)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Erro:\n" + erroException.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                StatusProgBar.Visible = false;

            }

        }

        public String Pks(String pTabela)
        {
            /*================================================================================
             * Verifica as Chaves Primarias da Tabela
             *================================================================================*/
            String sql = "SELECT " +
                         " +' i.' + c.COLUMN_NAME + ' = d.' + c.COLUMN_NAME 'pk'" +
                         " FROM    INFORMATION_SCHEMA.TABLE_CONSTRAINTS pk , " +
                         " INFORMATION_SCHEMA.KEY_COLUMN_USAGE c " +
                         " WHERE   UPPER(pk.TABLE_NAME) = '" + pTabela + "' " +
                         " AND  CONSTRAINT_TYPE = 'PRIMARY KEY' " +
                         " AND     c.TABLE_NAME = pk.TABLE_NAME " +
                         " AND     c.CONSTRAINT_NAME = pk.CONSTRAINT_NAME";

            using (SqlConnection connection = new SqlConnection(StringDeConexao))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    String ret = "";
                    /*========================================================================
                     * Caso existir chave, monta o join entre as chaves
                     * =======================================================================*/
                    while (reader.Read())
                    {
                        if (ret.Length == 0)
                        {
                            ret += " on " + reader["pk"].ToString();
                        }
                        else
                        {
                            ret += " and " + reader["pk"].ToString();
                        }
                    }
                    if (ret.Trim().Length == 0)
                    {
                        ret = "";
                    }
                    return ret;
                }
            }

        }
        private String retorna_tipo(DataRow row)
        {
            /*================================================================================
             * Retorna o tipo, caso tenha que definir tamanho, ou não.
             *================================================================================*/
            switch (row["TIPO"].ToString().ToUpper())
            {
                case ("DECIMAL"):
                    {
                        //(P,S)
                        return " " + row["TIPO"].ToString() + "(" + row["TAMANHO"].ToString() + "," + row["SCALA"].ToString() + ")";
                        break;
                    }
                case "NUMBER":
                    {
                        return " " + row["TIPO"].ToString() + "(" + row["TAMANHO"].ToString() + "," + row["SCALA"].ToString() + ")";
                        break;
                    }
                case "VARCHAR":
                    {
                        return " " + row["TIPO"].ToString() + "(" + row["MAXLENGTH"].ToString() + ")";
                        break;
                    }
                case "CHAR":
                    {
                        return " " + row["TIPO"].ToString() + "(" + row["MAXLENGTH"].ToString() + ")";
                        break;
                    }
                case "NVARCHAR":
                    {
                        return " " + row["TIPO"].ToString() + "(" + row["MAXLENGTH"].ToString() + ") ";
                        break;
                    }
                case "NCHAR":
                    {
                        //(N)
                        return " " + row["TIPO"].ToString() + "(" + row["MAXLENGTH"].ToString() + ") ";
                        break;
                    }                                //sem tamamnho             
            }
            return " " + row["TIPO"].ToString() + "";
        }



        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                /*===============================================================================
                 * SALVAR SCRIPT EM ARQUIVO
                 *===============================================================================*/
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Arquivo SQL|.sql";
                sfd.ShowDialog();

                StreamWriter st = new StreamWriter(sfd.FileName.ToString());
                st.Write(script.Text);
                st.Flush();
                st.Close();
                MessageBox.Show("Salvo com Sucesso");
            }
            catch (Exception erro)
            {
                MessageBox.Show("Ocorreu um erro \n" + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void script_TextChanged(object sender, EventArgs e)
        {
            //Caso tiver script gerado Habilita botão Salvar em Arquivo
            btnSave.Enabled = (script.Text.ToString().Trim().Length > 0);
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            /*================================================================================
             * Verifica se Dados de Conexão foram preenchidos
             *================================================================================*/
            if (txtDataSource.Text.Trim().ToString().Length == 0)
            {
                MessageBox.Show("Informe a DataSource");
                txtDataSource.Focus();
                return;
            }
            if (txtBase.Text.Trim().ToString().Length == 0)
            {
                MessageBox.Show("Informe a Base");
                txtBase.Focus();
                return;
            }
            if (txtUsuario.Text.Trim().ToString().Length == 0)
            {
                MessageBox.Show("Informe a Usuário");
                txtUsuario.Focus();
                return;
            }
            if (txtSenha.Text.Trim().ToString().Length == 0)
            {
                MessageBox.Show("Informe a Senha");
                txtSenha.Focus();
                return;
            }
            dtTabelaColunas = new DataTable();


            if (btnConectar.Text.ToUpper() == "CONECTAR")
            {
                /*================================================================================
                * Conectar no BD
                *================================================================================*/
                StringDeConexao = String.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}",
                                        txtDataSource.Text,
                                        txtBase.Text,
                                        txtUsuario.Text,
                                        txtSenha.Text);
                try
                {
                    lblMarcarTodos.Text = "Marcar Todos";
                    lblMarcarTodos.Tag = "1";
                    Cursor = Cursors.WaitCursor;
                    /*================================================================================
                    * Busca todas Tabelas e Seus Campos. Só irá trazer tabelas que não tenha sido criado log ainda.
                    *================================================================================*/
                    using (SqlConnection connection = new SqlConnection(StringDeConexao))
                    {
                        String sql = "select    t.name 'NOMETABELA',    " +
                                     "          c.name 'NOMECOLUNA',    " +
                                     "          tipo.name 'TIPO',       " +
                                     "          c.precision 'TAMANHO',   " +
                                     "          c.SCALE 'SCALA',   " +
                                     "          c.max_length 'MAXLENGTH' " +
                                     "     from sys.tables t,           " +
                                     "          sys.all_columns c,      " +
                                     "          sys.types tipo          " +
                                     "    where t.object_id = c.object_id " +
                                     "      and tipo.system_type_id = c.system_type_id " +
                                     "      and ((0 = (select count(1) from sys.tables tt where (upper(tt.name) = upper('LOG_'+t.name)))) and (upper(t.name) not like 'LOG_%'))";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            SqlDataAdapter sq = new SqlDataAdapter(command);
                            sq.Fill(dtTabelaColunas);
                        }
                    }
                    /*================================================================================
                    * Adiciona os nomes da Tabelas, para que o usuário possa selecionar na listcheckbox
                    *================================================================================*/
                    listChk.Items.Clear();

                    foreach (DataRow row in dtTabelaColunas.Rows)
                    {
                        if (Pks(row["NOMETABELA"].ToString()).ToString().Trim().Length > 0)
                        {
                            if (listChk.Items.IndexOf(row["NOMETABELA"].ToString()) < 0)
                                listChk.Items.Add(row["NOMETABELA"].ToString());
                        }
                    }
                    if (listChk.Items.Count > 0)
                    {
                        btnGerar.Enabled = true;
                        listChk.Enabled = true;
                        script.Enabled = true;
                        script.Clear();
                        btnConectar.Text = "Desconectar";
                    }
                    else
                    {
                        MessageBox.Show("Todas Tabelas já possuem LOG\n", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnGerar.Enabled = false;
                        listChk.Enabled = false;
                        script.Enabled = false;
                        listChk.Items.Clear();
                        script.Clear();
                        btnConectar.Text = "Conectar";
                    }
                }
                catch (Exception erro)
                {
                    btnGerar.Enabled = false;
                    listChk.Enabled = false;
                    script.Enabled = false;
                    listChk.Items.Clear();
                    script.Clear();
                    btnConectar.Text = "Conectar";
                    MessageBox.Show("Ocorreu um erro \n" + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
            else
            {
                /*================================================================================
                * Desconecta BD
                *================================================================================*/
                btnGerar.Enabled = false;
                listChk.Enabled = false;
                listChk.Items.Clear();
                script.Enabled = false;
                script.Clear();
                btnConectar.Text = "Conectar";
                lblMarcarTodos.Text = "Marcar Todos";
                lblMarcarTodos.Tag = "1";
            }

        }

        private void label5_Click(object sender, EventArgs e)
        {
            /*================================================================================
             * Checkar todas as Tabelas 
             *================================================================================*/
            if (lblMarcarTodos.Tag == "1")
            {
                for (int i = 0; i < listChk.Items.Count; i++)
                {
                    listChk.SetItemChecked(i, true);
                }
                lblMarcarTodos.Text = "Desmarcar Todos";
                lblMarcarTodos.Tag = "0";

            }
            else
            {
                for (int i = 0; i < listChk.Items.Count; i++)
                {
                    listChk.SetItemChecked(i, false);
                }
                lblMarcarTodos.Text = "Marcar Todos";
                lblMarcarTodos.Tag = "1";
            }
        }

        private void listChk_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            /*================================================================================
             * Somente para atualizar estado do LblMArcarTodos
             *================================================================================*/
            int i = 0;
            if (e.NewValue == CheckState.Unchecked)
            {
                i = -1;
            }
            else
            {
                i = 1;

            }
            if ((listChk.CheckedItems.Count + i) == listChk.Items.Count)
            {
                lblMarcarTodos.Text = "Desmarcar Todos";
                lblMarcarTodos.Tag = "0";
            }
            else
            {
                lblMarcarTodos.Text = "Marcar Todos";
                lblMarcarTodos.Tag = "1";
            }

        }
    }
}
