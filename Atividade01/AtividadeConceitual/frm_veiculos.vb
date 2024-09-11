Public Class frm_veiculos

    Private Sub img_foto_Click(sender As Object, e As EventArgs) Handles img_foto.Click
        Try
            With OpenFileDialog1
                .Title = "SELECIONE UMA FOTO" 'Título da janela
                .InitialDirectory = Application.StartupPath & "\imagem\"
                .ShowDialog()
                diretorio = .FileName
                img_foto.Load(diretorio)
            End With
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub
    Private Sub frm_veiculos_Load(sender As Object, e As EventArgs) Handles Me.Load
        conecta_banco()
        carregar_dados()
        carregar_campos()
        carregar_campos2()
        carregar_campos3()
        carregar_campos4()
    End Sub

    Private Sub btn_gravar_Click(sender As Object, e As EventArgs) Handles btn_gravar.Click
        Try
            SQL = "select * from tb_cadastrov where N_CHASSI='" & txt_chassi.Text & "'"
            rs = db.Execute(SQL)

            If rs.EOF = True Then 'Se não existir o cpf na tabela
                SQL = "insert into tb_cadastrov (N_CHASSI,MONTADORA,NOME_VEICULO,ANO_FABRICACAO,COR,PLACA,VALOR_MERCADO,FOTO) " &
                          "values ('" & txt_chassi.Text & "', '" & cmb_montadora.SelectedItem & "', " &
                          "'" & txt_nome.Text & "', '" & cmb_ano.SelectedItem & "', " &
                          "'" & cmb_cor.SelectedItem & "', '" & txt_placa.Text & "', " &
                          "'" & txt_valor.Text & "', '" & diretorio & "')"
                rs = db.Execute(UCase(SQL))
                MsgBox("Registro enviado com sucesso!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AVISO")

            Else
                If diretorio = "" Then
                    diretorio = rs.Fields(8).Value
                End If
                SQL = "update tb_cadastrov set MONTADORA='" & cmb_montadora.SelectedItem & "', " &
                      "NOME_VEICULO='" & txt_nome.Text & "', ANO_FABRICACAO='" & cmb_ano.SelectedItem & "', " &
                      "COR='" & cmb_cor.SelectedItem & "', PLACA='" & txt_placa.Text & "', " &
                      "VALOR_MERCADO='" & txt_valor.Text & "', " &
                      "FOTO='" & diretorio & "' where N_CHASSI ='" & txt_chassi.Text & "'"

                rs = db.Execute(UCase(SQL))
                MsgBox("Registro alterado com sucesso!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AVISO")

            End If
            carregar_dados()
            limpar_cadastro()
        Catch ex As Exception
            MsgBox($"Erro ao gravar! + {ex}", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "AVISO")
        End Try
        carregar_campos2()
        carregar_campos3()
        carregar_campos4()
    End Sub


    Private Sub txt_chassi_LostFocus(sender As Object, e As EventArgs) Handles txt_chassi.LostFocus
        Try
            SQL = "select * from tb_cadastrov where N_CHASSI='" & txt_chassi.Text & "'"
            rs = db.Execute(SQL)
            If rs.EOF = False Then 'Se existir o chassi na tabela
                txt_chassi.Text = rs.Fields(1).Value
                cmb_montadora.SelectedItem = rs.Fields(2).Value
                txt_nome.Text = rs.Fields(3).Value
                cmb_ano.SelectedItem = rs.Fields(4).Value
                cmb_cor.SelectedItem = rs.Fields(5).Value
                txt_placa.Text = rs.Fields(6).Value
                txt_valor.Text = rs.Fields(7).Value
                img_foto.Load(rs.Fields(8).Value)
            Else
                txt_nome.Focus()
            End If
        Catch ex As Exception
            MsgBox("Erro ao consultar!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ATENÇÃO")
        End Try
    End Sub

    Private Sub txt_chassi_DoubleClick(sender As Object, e As EventArgs) Handles txt_chassi.DoubleClick
        limpar_cadastro()
        carregar_campos2()
        carregar_campos3()
        carregar_campos4()
    End Sub

    Private Sub txt_busca_TextChanged(sender As Object, e As EventArgs) Handles txt_busca.TextChanged

        Try
            SQL = "select * from tb_cadastrov where " & cmb_campo.Text & " like '" & txt_busca.Text & "%'"
            rs = db.Execute(SQL)
            cont = 0
            With dgv_dados
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

    Private Sub dgv_dados_Click(sender As Object, e As EventArgs) Handles dgv_dados.Click
        Try
            With dgv_dados
                If .CurrentRow.Cells(5).Selected = True Then
                    aux_N_CHASSI = .CurrentRow.Cells(1).Value
                    SQL = "select * from tb_cadastrov where N_CHASSI ='" & aux_N_CHASSI & "'"
                    rs = db.Execute(SQL)
                    If rs.EOF = False Then
                        txt_chassi.Text = rs.Fields(1).Value
                        cmb_montadora.SelectedItem = rs.Fields(2).Value
                        txt_nome.Text = rs.Fields(3).Value
                        cmb_ano.SelectedItem = rs.Fields(4).Value
                        cmb_cor.SelectedItem = rs.Fields(5).Value
                        txt_placa.Text = rs.Fields(6).Value
                        txt_valor.Text = rs.Fields(7).Value
                        img_foto.Load(rs.Fields(8).Value)
                    End If
                ElseIf .CurrentRow.Cells(6).Selected = True Then
                    aux_N_CHASSI = .CurrentRow.Cells(1).Value
                    SQL = "select * from tb_cadastrov where N_CHASSI='" & aux_N_CHASSI & "'"
                    rs = db.Execute(SQL)
                    If rs.EOF = False Then
                        resp = MsgBox("Deseja realmente excluir o" + vbNewLine &
                                    "N_CHASSI: " & aux_N_CHASSI & "?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "ATENÇÃO")
                        If resp = MsgBoxResult.Yes Then
                            SQL = "delete from tb_cadastrov where N_CHASSI='" & aux_N_CHASSI & "'"
                            rs = db.Execute(SQL)
                        End If
                        carregar_dados()
                    End If
                Else
                    Exit Sub
                End If
            End With
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub


End Class
