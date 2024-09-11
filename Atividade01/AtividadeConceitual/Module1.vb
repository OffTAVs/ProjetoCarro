Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Module Module1
    Public diretorio, SQL, aux_N_CHASSI, resp As String
    Public db As New ADODB.Connection
    Public rs As New ADODB.Recordset
    Public cont, aux_idcliente As Integer

    Sub conecta_banco()
        'String de Conexão ADO SQL-SERVER
        Try
            db = CreateObject("ADODB.Connection")
            db.Open("Provider=SQLOLEDB;Data Source=LAB5-12;Initial Catalog=veiculos;trusted_connection=yes;")
            MsgBox("Conexão OK", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AVISO")
        Catch ex As Exception
            MsgBox("Erro ao Conectar!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "AVISO")
        End Try
    End Sub

    Sub limpar_cadastro()
        Try
            ' Usando o 'Me' para acessar o formulário atual ou substitua por uma referência ao formulário correto
            With frm_veiculos
                ' Limpar os campos de texto
                .txt_chassi.Clear()
                .txt_nome.Clear()
                .txt_placa.Clear()
                .txt_valor.Clear()

                ' Limpar os itens dos ComboBox
                .cmb_montadora.Items.Clear()
                .cmb_ano.Items.Clear()
                .cmb_cor.Items.Clear()
                .img_foto.Load(Application.StartupPath & "\imagem\novafoto.png")
                ' Focar o controle 'txt_chassi'
                .txt_chassi.Focus()
            End With
        Catch ex As Exception
            ' Exibir detalhes do erro
            MsgBox("Erro ao limpar cadastro: " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "AVISO")
        End Try
    End Sub

    Sub carregar_dados()
        Try
            SQL = "select * from tb_cadastrov order by NOME_VEICULO asc"
            rs = db.Execute(SQL)
            cont = 0
            With frm_veiculos.dgv_dados
                .Rows.Clear()
                Do While rs.EOF = False
                    cont = cont + 1
                    .Rows.Add(cont, rs.Fields(1).Value, rs.Fields(3).Value, rs.Fields(2).Value, rs.Fields(4).Value, Nothing, Nothing)
                    rs.MoveNext()
                Loop
            End With

        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Sub carregar_campos()
        Try
            With frm_veiculos.cmb_campo.Items
                .Add("N_CHASSI")
                .Add("NOME_VEICULO")
            End With
            frm_veiculos.cmb_campo.SelectedIndex = 1
        Catch ex As Exception

        End Try
    End Sub

    Sub carregar_campos2()
        Try
            With frm_veiculos.cmb_montadora.Items
                .Add("CHEVROLET")
                .Add("FORD")
                .Add("TOYOTA")
                .Add("NISSAN")
                .Add("HONDA")
                .Add("VOLVO")
                .Add("BMW")
                .Add("MERCEDES")
                frm_veiculos.cmb_montadora.SelectedIndex = 0
            End With
        Catch ex As Exception

        End Try
    End Sub

    Sub carregar_campos3()
        Try
            With frm_veiculos.cmb_cor.Items
                .Add("PRATA")
                .Add("PRETO")
                .Add("CINZA")
                .Add("VERMELHO")
                .Add("AZUL")
                frm_veiculos.cmb_cor.SelectedIndex = 0
            End With
        Catch ex As Exception

        End Try
    End Sub

    Sub carregar_campos4()
        Try
            ' Limpa os itens existentes na ComboBox antes de adicionar novos
            frm_veiculos.cmb_ano.Items.Clear()

            ' Adiciona anos à ComboBox de 1950 a 2024
            For ano As Integer = 1950 To 2024
                frm_veiculos.cmb_ano.Items.Add(ano.ToString())
            Next

            ' Define a opção padrão se desejar (opcional)
            If frm_veiculos.cmb_ano.Items.Count > 0 Then
                frm_veiculos.cmb_ano.SelectedIndex = 0 ' Seleciona o primeiro item (1950)
            End If
        Catch ex As Exception
            ' Trate a exceção aqui, se necessário
            MessageBox.Show("Erro ao carregar anos na ComboBox.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Module
