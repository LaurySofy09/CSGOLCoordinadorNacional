Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization
Imports FluentFTP

Public Class SeguimientoProyectoCoordinadorNacional
    Inherits System.Web.UI.Page
    Public notifyModal As String = ""
    'Public ValorIDProyecto As String = ""
    'Public ValorIDRTRD As String = ""
    Dim ObjInterfacesJP As New InterfacesJP
    Dim ObjInterzGeneral As New InterfazJF
    Dim lws As New InterfazGeneral
    Dim objInterRF As New InterfazRF
    Dim dt As New DataTable
    Dim dtMensaje As New DataTable
    Dim dtEmpty As New DataTable
    Dim ObjInterLSD As New InterfazLSD
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim IdUsuario As String = Session("IdentificadorUsuario")

        If Not IsPostBack Then

            ucBusqueda.InicializarControl(ucBusqueda.Pantalla.SeguimientoProyectoCoordinadorNacional)
            MultiView1.ActiveViewIndex = 0
        End If


    End Sub

    Private Sub ucBusqueda_GridRowCommand(sender As Object, e As GridViewCommandEventArgs) Handles ucBusqueda.GridRowCommand
        Try
            If e.CommandName = "Select" Then

                Dim IdProyecto As Integer = e.CommandArgument

                'Habilitar el boton de regresar
                pnlBotonRegresar.Visible = True
                BtnRegresar.Visible = True

                'Habilitar los tabs de generales, adjuntos y historial
                TabsDatosProyecto.Visible = True

                'Habilitar la vista de generales
                MultiView1.ActiveViewIndex = 1

                'Asignar clase activo al boton de generales
                BtnGenerales.Attributes.Add("class", "nav-link bg-light text-dark active")

                'Cargar los datos generales del proyecto
                ucGeneralesProyecto.CargarDatos(IdProyecto, True)

                'Cargar las generales del adjunto
                ucEncabezadosAdjunto.InicializarControl(ucEncabezados.Modelo.EncabezadoAdjunto, IdProyecto)
                CargarDatosProtectoAdj(IdProyecto, ucGeneralesProyecto.ObtenerDatosFormularioAdj())

                'Cargar el encabezado de la pestaña de historial
                'ucDatosVeeduria.CargarDatos(IdProyecto)
                ucEncabezadosHistorial.InicializarControl(ucEncabezados.Modelo.EncabezadoAdjunto, IdProyecto)

                'Cargar el historial de los RTRD
                ucHistorialRTRD.CargarHistorial(IdProyecto, True, "Coord")

                BtnDocumentos.Attributes.Add("class", "nav-link bg-light text-dark")
                BtnHistorial.Attributes.Add("class", "nav-link bg-light text-dark")

                ''Mostrar los Adjuntos
                'ucAdjuntarProyectos.InicializarControl(ucAdjuntarProyectos.Modo.ListarAdjuntos, IdProyecto)

                ''Cargar el Historial del RTRD
                'ucHistorialRTRD.CargarHistorial(IdProyecto, True)

            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub CargarDatosProtectoAdj(ByVal IdProyecto As Integer, ByVal Datos As Dictionary(Of String, String))
        Inicializar()
        'Dim objInterFJ As New InterfazFJ

        'Dim dtDatos As New DataTable
        'dtDatos = objInterFJ.VerDatosProyectoPorEditar(IdProyecto)
        If Datos.Count > 0 Then
            txtEntidadADJ.Text = Datos("entidad")
            txtDependenciaADJ.Text = Datos("dependencia")
            txtDescripcionProyectoADJ.Text = Datos("DescripcionProyecto")
            txtExpedienteADJ.Text = Datos("secuencial")
            txtEstadoADJ.Text = Datos("estadoProyecto")
            ucAdjuntarProyectos.InicializarControl(ucAdjuntarProyectos.Modo.ListarAdjuntos, IdProyecto, Datos("secuencial"))
        End If
    End Sub

    Private Sub Inicializar()
        txtEntidadADJ.Text = ""
        txtDependenciaADJ.Text = ""
        txtDescripcionProyectoADJ.Text = ""
        txtExpedienteADJ.Text = ""
        txtEstadoADJ.Text = ""

        txtEntidadADJ.Attributes.Add("readonly", "readonly")
        txtDependenciaADJ.Attributes.Add("readonly", "readonly")
        txtDescripcionProyectoADJ.Attributes.Add("readonly", "readonly")
        txtExpedienteADJ.Attributes.Add("readonly", "readonly")
        txtEstadoADJ.Attributes.Add("readonly", "readonly")
    End Sub

    Private Sub BtnRegresar_Click(sender As Object, e As EventArgs) Handles BtnRegresar.Click
        TabsDatosProyecto.Visible = False
        ucBusqueda.InicializarControl(ucBusqueda.Pantalla.SeguimientoProyectoCoordinadorNacional)
        MultiView1.ActiveViewIndex = 0
        BtnRegresar.Visible = False
    End Sub

    Private Sub BtnGenerales_ServerClick(sender As Object, e As EventArgs) Handles BtnGenerales.ServerClick
        MultiView1.ActiveViewIndex = 1 'VistaGenerales
        pnlBotonRegresar.Visible = True

        BtnGenerales.Attributes.Add("class", "nav-link bg-light text-dark active")

        BtnDocumentos.Attributes.Remove("class")
        BtnDocumentos.Attributes.Add("class", "nav-link bg-light text-dark")

        BtnHistorial.Attributes.Remove("class")
        BtnHistorial.Attributes.Add("class", "nav-link bg-light text-dark")
    End Sub

    Private Sub BtnDocumentos_ServerClick(sender As Object, e As EventArgs) Handles BtnDocumentos.ServerClick
        MultiView1.ActiveViewIndex = 2

        'MultiView1.ActiveViewIndex = 1 'VistaDocumentosAdjuntos
        pnlBotonRegresar.Visible = True

        BtnDocumentos.Attributes.Add("class", "nav-link bg-light text-dark active")

        BtnGenerales.Attributes.Remove("class")
        BtnGenerales.Attributes.Add("class", "nav-link bg-light text-dark")

        BtnHistorial.Attributes.Remove("class")
        BtnHistorial.Attributes.Add("class", "nav-link bg-light text-dark")

    End Sub

    Private Sub BtnHistorial_ServerClick(sender As Object, e As EventArgs) Handles BtnHistorial.ServerClick
        MultiView1.ActiveViewIndex = 3
        pnlBotonRegresar.Visible = True

        BtnHistorial.Attributes.Add("class", "nav-link bg-light text-dark active")

        BtnGenerales.Attributes.Remove("class")
        BtnGenerales.Attributes.Add("class", "nav-link bg-light text-dark")

        BtnDocumentos.Attributes.Remove("class")
        BtnDocumentos.Attributes.Add("class", "nav-link bg-light text-dark")

    End Sub

    Private Sub ucHistorialRTRD_GridRowCommand(sender As Object, e As GridViewCommandEventArgs) Handles ucHistorialRTRD.GridRowCommand
        If e.CommandName = "Select" Then
            Dim idRtrd As Integer = e.CommandArgument
            Dim idProyecto = ucHistorialRTRD.IdProyecto

            'dt = ObjInterfacesJP.ObservacionesRTRD_Fiscalizador(idProyecto, idRtrd)

            '''Mostrar el Boton de Regresar Superior color verder
            dvRegresarSuperior.Visible = True

            '''Habilitar la vista 4 detalles del rtrd
            MultiView1.ActiveViewIndex = 4

            '''Ocultar los usercontrol
            dvUserControlEnviarRTRDEntidad.Visible = False
            dvUserControlEnviarRTRDCoordinador.Visible = False

            'Ocultar los tabs del menú
            TabsDatosProyecto.Visible = False

            'Buscar los datos y cargar el formulario
            CargarDatosRTRD(idProyecto, idRtrd)

            'ucAdjuntar.CargarAdjuntos()
            pnlBotonRegresar.Visible = False

            '' Carga datos del RTRD en el formulario
            ucFormularioVeeduria.CargarRegistro(idProyecto, idRtrd, True)
            ucFormularioVeeduria.MostrarFechaPoblacion(False)
            ucFormularioVeeduria.MostrarFecha(False)
            ucFormularioVeeduria.MostrarAsteriscos(False)
            '
            FlagOpcionRetorno.Value = 1

            'MostrarEncabezado.Visible = True
            'ucEncabezadosDetalleRTRD.InicializarControl(ucEncabezados.Modelo.EncabezadoGeneral, FlagIdProyecto.Value, FlagIdRTRD.Value, FlagIdVisitaRTRD.Value)
            ucEncabezadosDetalleRTRD.InicializarControl(ucEncabezados.Modelo.EncabezadoGeneral, idProyecto, idRtrd, FlagIdVisitaRTRD.Value)
        End If
    End Sub

    Private Sub CargarDatosRTRD(ByVal idProyecto As Integer, ByVal IdRTRD As Integer)

        Dim TieneSeguimientoEntidad As Boolean = False

        ''' Carga datos del RTRD y el proyecto en el encabezado
        Dim dtDatosRTRD As New DataTable
        dtDatosRTRD = objInterRF.VerDatosRTRD(idProyecto, IdRTRD)


        '''Validar el estado del RTRD
        If (dtDatosRTRD.Rows.Count > 0) Then
            FlagIdProyecto.Value = idProyecto
            Dim secuencialRtrd As Integer = dtDatosRTRD.Rows(0)("SecuencialRtrd")
            FlagIdRTRD.Value = secuencialRtrd
            Dim fechaVisita As String = dtDatosRTRD.Rows(0)("FechaVisita")
            Dim veedor As String = dtDatosRTRD.Rows(0)("Veedor")
            Dim fechaRegistro As String = dtDatosRTRD.Rows(0)("FechaRegistro")
            Dim poblacionBeneficiada As String = dtDatosRTRD.Rows(0)("PoblacionBeneficiada")
            Dim idEstadoRtrd As Integer = dtDatosRTRD.Rows(0)("IdEstadoRtRd")
            FlagEstadoRTRD.Value = idEstadoRtrd
            FlagIdVisitaRTRD.Value = dtDatosRTRD.Rows(0)("IdVisita")

            'lblSecuencialRTRD.Text = secuencialRtrd
            'lblFechaVisita.Text = fechaVisita
            'lblFechaVisita1.Text = fechaVisita

            'lblVeedor.Text = veedor
            'lblVeedor1.Text = veedor

            'lblFechaRegistro.Text = fechaRegistro
            'lblFechaRegistro1.Text = fechaRegistro
            'TxtNumRTRD.Text = secuencialRtrd

            Dim FlagRtRdSinHallazgos As Integer = dtDatosRTRD.Rows(0)("FlagRtRdSinHallazgos")



            '''-------------------------------Consultar si el RTRD tiene observaciones--------------------------------- 
            dt = ObjInterfacesJP.ValidarObservacionesRTRD(FlagIdProyecto.Value, FlagIdRTRD.Value)

            If dt.Rows.Count > 0 Then

                If dt.Rows(0).Item("EstadoRtRd") = 5 Then

                    Dim dvObs As New DataView
                    dvObs = dt.DefaultView
                    dvObs.RowFilter = "FlagObsCoordinador = 0 and FlagObsCoordinador <> 1"
                    If dvObs.Count > 0 Then
                        dt = dvObs.ToTable()
                        TieneSeguimientoEntidad = True
                    Else
                        TieneSeguimientoEntidad = False
                    End If

                End If

            End If

            '---------------------------------------------------------------------------------------------------------


            If idEstadoRtrd = 1 Then
                BtnRTRD_SinHallazgos.Visible = False
                BtnRTRD_EnviarEntidad.Visible = False
                BtnRTRD_Investigacion.Visible = False
                BtnRTRD_Devolver.Visible = False
                BtnRTRD_EnviarCoordinador.Visible = False
                'BtnRTRD_Seguimiento.Visible = False
                BtnRTRD_VerInvestigacion.Visible = False
                'FlagOpcionRetorno.Value = "2"
                FlagOpcionRetorno.Value = "1"

            ElseIf idEstadoRtrd = 2 Then
                BtnRTRD_SinHallazgos.Visible = False
                BtnRTRD_EnviarEntidad.Visible = False
                BtnRTRD_EnviarCoordinador.Visible = True
                'BtnRTRD_Seguimiento.Visible = False
                BtnRTRD_Investigacion.Visible = False
                BtnRTRD_Devolver.Visible = False
                FlagOpcionRetorno.Value = "1"


            ElseIf idEstadoRtrd = 3 And FlagRtRdSinHallazgos = 1 Then

                '''cuando el estado es 3 solo aparece un boton se debe alinear a la derecha de la pantalla
                dvBotonesAtencionRTRD.Attributes.Remove("class")
                dvBotonesAtencionRTRD.Attributes.Add("class", "col-6 text-right pt-1")

                BtnRTRD_SinHallazgos.Visible = True
                BtnRTRD_SinHallazgos.Text = "Ver Seguimiento"
                BtnRTRD_EnviarEntidad.Visible = False
                BtnRTRD_Investigacion.Visible = False
                BtnRTRD_Devolver.Visible = False
                BtnRTRD_EnviarCoordinador.Visible = False
                'BtnRTRD_Seguimiento.Visible = False
                BtnRTRD_VerInvestigacion.Visible = False
                'BtnCerrarRTRD.Visible = False
                'FlagOpcionRetorno.Value = "2"
                FlagOpcionRetorno.Value = "1"
                'btnBitacora.Visible = True


            ElseIf idEstadoRtrd = 4 Or idEstadoRtrd = 6 Then 'Por Subsanar entidad

                '''cuando el estado es 4 solo aparece un boton se debe alinear a la derecha de la pantalla
                dvBotonesAtencionRTRD.Attributes.Remove("class")
                dvBotonesAtencionRTRD.Attributes.Add("class", "col-6 text-right pt-1")

                BtnRTRD_SinHallazgos.Visible = False
                BtnRTRD_EnviarEntidad.Visible = False
                BtnRTRD_Investigacion.Visible = False
                BtnRTRD_Devolver.Visible = False
                'BtnRTRD_Seguimiento.Visible = False
                BtnRTRD_VerInvestigacion.Visible = False
                If TieneSeguimientoEntidad = True Then
                    BtnRTRD_EnviarEntidad.Text = "Ver Seguimiento"
                Else
                    BtnRTRD_EnviarEntidad.Visible = False
                End If


                BtnRTRD_EnviarCoordinador.Visible = False
                FlagOpcionRetorno.Value = "1"



            ElseIf idEstadoRtrd = 5 Then

                '''cuando el estado es 5 solo aparece un boton se debe alinear a la derecha de la pantalla
                dvBotonesAtencionRTRD.Attributes.Remove("class")
                dvBotonesAtencionRTRD.Attributes.Add("class", "col-6 text-right pt-1")

                BtnRTRD_SinHallazgos.Visible = False
                BtnRTRD_EnviarEntidad.Visible = False
                BtnRTRD_EnviarCoordinador.Visible = True
                BtnRTRD_EnviarCoordinador.Text = "Ver Seguimiento"
                BtnRTRD_Investigacion.Visible = True
                BtnRTRD_VerInvestigacion.Visible = False
                BtnRTRD_EnviarEntidad.Visible = False
                BtnRTRD_Devolver.Visible = True

                'If TieneSeguimientoEntidad = True Then
                '    BtnRTRD_EnviarEntidad.Text = "Ver Seguimiento"
                '    BtnRTRD_Seguimiento.Visible = True
                '    BtnRTRD_Investigacion.Visible = True
                '    BtnRTRD_Devolver.Visible = True
                'Else
                '    BtnRTRD_Seguimiento.Visible = False
                '    BtnRTRD_Investigacion.Visible = True
                '    BtnRTRD_Devolver.Visible = True
                'End If
                'FlagOpcionRetorno.Value = "2"
                FlagOpcionRetorno.Value = "1"

            ElseIf idEstadoRtrd = 8 Or idEstadoRtrd = 9 Then

                '''cuando el estado es 5 solo aparece un boton se debe alinear a la derecha de la pantalla
                dvBotonesAtencionRTRD.Attributes.Remove("class")
                dvBotonesAtencionRTRD.Attributes.Add("class", "col-6 text-right pt-1")

                BtnRTRD_SinHallazgos.Visible = False
                BtnRTRD_EnviarEntidad.Visible = False
                BtnRTRD_Investigacion.Visible = False
                BtnRTRD_Devolver.Visible = False
                BtnRTRD_EnviarCoordinador.Visible = True
                'BtnRTRD_Seguimiento.Visible = False
                BtnRTRD_VerInvestigacion.Visible = True
                BtnRTRD_EnviarCoordinador.Text = "Ver Seguimiento"
                'FlagOpcionRetorno.Value = "2"
                FlagOpcionRetorno.Value = "1"



            Else

                '''
                dvBotonesAtencionRTRD.Attributes.Remove("class")
                dvBotonesAtencionRTRD.Attributes.Add("class", "col-6 d-flex justify-content-around pt-1")

                BtnRTRD_SinHallazgos.Visible = True
                BtnRTRD_SinHallazgos.Text = "RTRD sin Hallazgos"
                BtnRTRD_EnviarEntidad.Visible = True
                BtnRTRD_EnviarEntidad.Text = "Enviar a Entidad"
                'BtnRTRD_EnviarCoordinador.Visible = True
                'BtnRTRD_EnviarCoordinador.Text = "Enviar al Coordinador"

                'Consulto todos los RTRD del EXPEDIENTE
                Dim dtCantRTRD_Expediente As DataTable = ObjInterfacesJP.ConsultarEstadoRTRDbyExpediente(idProyecto)

                If dtCantRTRD_Expediente.Rows.Count > 0 Then
                    Dim UltimoRTRD As String = dtCantRTRD_Expediente.Rows.Count

                    '''Validar si este es el ultimo RTRD y si el estado es atendido fiscalizador.
                    If (secuencialRtrd = UltimoRTRD And idEstadoRtrd = 2) Then
                        Dim CantRTRDAtendidosFisc As Integer = 0
                        'Dim CantRTRDCerrados As Integer = 0
                        Dim CantRTRDPorSubsanar As Integer = 0
                        Dim CantRTRDEnviadoCoordinador As Integer = 0
                        'Dim SecuencialRow As Integer = 0
                        'Dim Valid As Boolean = False

                        ''''Verifico los estados de todos los RTRD anteriores 
                        For Each row In dtCantRTRD_Expediente.Rows
                            If row.Item("EstadoRtRd") = "2" Then 'Atendido Fiscalizador
                                CantRTRDAtendidosFisc = CantRTRDAtendidosFisc + 1
                                '    ElseIf row.Item("EstadoRtRd") = "3" 'Cerrado Lider Provincial
                                '        CantRTRDCerrados = CantRTRDCerrados + 1
                            ElseIf row.Item("EstadoRtRd") = "4" Then 'Por subsanar Entidad
                                CantRTRDPorSubsanar = CantRTRDPorSubsanar + 1
                            ElseIf row.Item("EstadoRtRd") = "5" Then 'Por atender Coordinador Nacional
                                CantRTRDEnviadoCoordinador = CantRTRDEnviadoCoordinador + 1
                            End If

                        Next

                        If (secuencialRtrd = UltimoRTRD And CantRTRDAtendidosFisc = 1 And CantRTRDPorSubsanar = 0 And CantRTRDEnviadoCoordinador = 0) Then
                            'BtnRTRD_EnviarCoordinador.Visible = True
                            'BtnRTRD_EnviarCoordinador.Text = "Enviar al Coordinador"
                        Else
                            'BtnRTRD_EnviarCoordinador.Visible = False
                        End If

                    ElseIf (secuencialRtrd <> UltimoRTRD And idEstadoRtrd = 2) Then
                        'BtnRTRD_EnviarCoordinador.Visible = False
                    Else
                        'BtnRTRD_EnviarCoordinador.Visible = False
                    End If

                End If

                ''SI ENTRA AQUI EL EStado del rtrd es atendido fiscalizador
                'Dim dtCant As DataTable = ObjInterfacesJP.ConsultarCantidadRTRDEnviadoCoordinador(idProyecto)

                'If dtCant.Rows.Count > 0 Then

                '    If dtCant.Rows(0).Item("Cantidad") >= "1" Then
                '        BtnRTRD_EnviarCoordinador.Visible = False
                '    Else
                '        BtnRTRD_EnviarCoordinador.Visible = True
                '        BtnRTRD_EnviarCoordinador.Text = "Enviar al Coordinador"
                '    End If

                'End If



            End If



        End If

        Dim dtDatosProyecto As New DataTable
        dtDatosProyecto = objInterRF.VerDatosProyectos(idProyecto)

        If (dtDatosProyecto.Rows.Count > 0) Then

            Dim IdtipoDocumento As String = dtDatosProyecto.Rows(0)("IdTipoDocumento")

            'If (IdtipoDocumento = "1") Then
            'lblTitulodTipoDocumento.Text = "Contrato: "
            'ElseIf (IdtipoDocumento = "2") Then
            'lblTitulodTipoDocumento.Text = "Orden de Compra: "
            'End If

            Dim secuencialProyecto As Integer = dtDatosProyecto.Rows(0)("Secuencial")
            FlagIdExpediente.Value = secuencialProyecto
            Dim estadoProyecto As String = dtDatosProyecto.Rows(0)("EstadoProyecto")

            'Dim rtrd As Integer = 0
            'rtrd = FlagIdRTRD.Value
            'lblExpediente.Text = secuencialProyecto
            'lblEstadoProyecto.Text = estadoProyecto
            'lblInfoRTRD.Text = FlagIdRTRD.Value
            'lblInfoNumExpediente.Text = secuencialProyecto


            Dim provincia As String = dtDatosProyecto.Rows(0)("NombreProvincia")
            Dim distrito As String = dtDatosProyecto.Rows(0)("NombreDistrito")
            Dim corregimiento As String = dtDatosProyecto.Rows(0)("NombreCorregimiento")
            Dim lugarPoblado As String = dtDatosProyecto.Rows(0)("LugarPoblado")
            Dim dependencia As String = dtDatosProyecto.Rows(0)("Dependencia")
            Dim tipoDocumento As String = dtDatosProyecto.Rows(0)("TipoDocumento")
            Dim numeroDocumento As String = dtDatosProyecto.Rows(0)("NumeroDocumento")

            Dim monto As Decimal = dtDatosProyecto.Rows(0)("Monto")
            Dim descripcionProyecto As String = dtDatosProyecto.Rows(0)("DescripcionProyecto")
            Dim empresa As String = dtDatosProyecto.Rows(0)("Empresa")

        End If

        Dim dtTitulos As New DataTable
        dtTitulos = ObjInterfacesJP.ObtenerParametroInteger(66)
        LblTitulosRTRD.Text = dtTitulos(0)("ValueString")
    End Sub

    Private Sub BtnRTRD_SinHallazgos_Click(sender As Object, e As EventArgs) Handles BtnRTRD_SinHallazgos.Click
        '''Inicializar el UserControl del encabezado
        ucEncabezados.InicializarControl(ucEncabezados.Modelo.EncabezadoGeneral, FlagIdProyecto.Value, FlagIdRTRD.Value, FlagIdVisitaRTRD.Value)

        '''Mostrar el modal de Cierre de RTRD
        notifyModal = "$('#mdlCerrarRTRDSinHallazgos').modal('show');"


        '''Cargar el FLAG y el panel de motivo de cierre
        dt = ObjInterfacesJP.ConsultarMotivoCierreByFiltro("CR", 2)
        If dt.Rows.Count > 0 Then
            FlagIdMotivoCierre.Value = dt.Rows(0).Item("IdMotivo")
            txtResultadoMotivoCierre.Text = dt.Rows(0).Item("Motivo")
        End If


        dt = ObjInterfacesJP.ValidarObservacionesRTRD(FlagIdProyecto.Value, FlagIdRTRD.Value)

        If dt.Rows.Count > 0 Then

            lbletiquetaTxtMotivoCierre.InnerText = "Motivo de Cierre:"
            TxtObservacion_SinHallazgos.Text = dt.Rows(0).Item("Observacion")
            TxtObservacion_SinHallazgos.CssClass = ("form-control")


            If dt.Rows(0).Item("EstadoRtRd") = 3 Then


                lblTituloModalCierreRTRD.InnerText = "RTRD cerrado sin Hallazgos"

                PnlSeguimientoEntidad.Visible = True
                dvgridBitacora.Visible = True

                '''Mostrar los contactos que recibieron correo
                BuscarDestinatariosEnvioEntidad(False)

                '''
                ValidarSiTieneEvaluacionTecnica()

                '''Ocultar el asterisco de obligatorio
                RequeridoMotivoCierre.Visible = False

                '''Bloquear el Panel de Motivo Cierre
                pnlMotivoCierre.Visible = True

                '''Cambia el titulo del campo de texto de la observación
                lbletiquetaTxtMotivoCierre.InnerText = "Observación del Líder Provincial:"

                txtResultadoMotivoCierre.Enabled = False

                '''llena el campo de Motivo de cierre
                If (Not IsDBNull(dt.Rows(0).Item("MotivoCierre")) Or dt.Rows(0).Item("MotivoCierre") <> "") Then
                    txtResultadoMotivoCierre.Text = dt.Rows(0).Item("MotivoCierre")
                    txtResultadoMotivoCierre.CssClass = ("form-control")
                End If


                '''Bloquear el panel de observaciones
                TxtObservacion_SinHallazgos.Enabled = False

                '''Desaparecer el botón de guardar
                pnlbtnGuardarObs.Visible = False

                '''Desaparecer el botón de cerrar RTRD
                BtnCerrarRTRD.Visible = False

                '''Mostrar el panel con los datos del usuario que
                pnlDatosLider.Visible = True

                lblDatosLiderProvincial.Text = dt.Rows(0).Item("UsuarioAtendioRTRD")

                ucAdjuntarObservacionRTRD.InicializarControl(ucAdjuntarObservacionRTRD.Modo.ListarAdjuntos, FlagIdProyecto.Value, FlagIdRTRD.Value, dt.Rows(0).Item("IdObs"), True, "mdlCerrarRTRDSinHallazgos", "bg-success text-bold text-white", "card border-success")

                'Dim dtSeguimientoRTRD As DataTable = ObjInterfacesJP.ConsultarSeguimientoRTRD(FlagIdProyecto.Value, FlagIdRTRD.Value)

                ''' Validar si el RTRD FUE SUBSANADO POR LA ENTIDAD.
                Dim dtSeguimientoRTRD As DataTable = ObjInterfacesJP.ConsultarSeguimientoRTRD(FlagIdProyecto.Value, FlagIdRTRD.Value)

                'If dtSeguimientoRTRD.Rows.Count = 0 Then
                '    lblTituloModalCierreRTRD.InnerText = "RTRD cerrado sin Hallazgos"
                '    PnlSeguimientoEntidad.Visible = False
                'Else
                '    lblTituloModalCierreRTRD.InnerText = "RTRD cerrado"
                'End If

                If dtSeguimientoRTRD.Rows.Count = 0 Then
                    lblTituloModalCierreRTRD.InnerText = "RTRD cerrado sin Hallazgos"
                    PnlSeguimientoEntidad.Visible = False
                Else
                    lblTituloModalCierreRTRD.InnerText = "RTRD cerrado"
                    ' End If

                    'If dt.Rows(0).Item("IdAutoridad") = "" Or dt.Rows(0).Item("IdAutoridad") = "-1" Then
                    '    PnlSeguimientoEntidad.Visible = False

                    'Else

                    PnlSeguimientoEntidad.Visible = True

                    '''Consultar los datos del usuario que envio el correo a la entidad
                    '''Consultar los datos del correo enviado a la entidad
                    Dim dtCorreoEntidad As New DataTable
                    dtCorreoEntidad = ObjInterfacesJP.ConsultarCorreoEnvioEntidad(FlagIdProyecto.Value, FlagIdRTRD.Value)

                    If dtCorreoEntidad.Rows.Count > 0 Then
                        'lblEtiquetaDatosCorreo.InnerText = "Correo Enviado a la entidad por el Líder Provincial - " + dtDatosRTRD.Rows(0).Item("UsuarioAtendioRTRD") + " - " + dtDatosRTRD.Rows(0).Item("FechaEnviaEntidad")
                        lblEtiquetaDatosCorreo.Visible = False
                        dvDatosCorreoEnviaEntidad.Visible = True

                        lblDatosUsuarioEnviaCorreoEntidad.InnerText = "Correo enviado a la Entidad por el Líder Provincial – " + dtCorreoEntidad.Rows(0).Item("NombreUsuario") + " el " + dtCorreoEntidad.Rows(0).Item("FechaEnvioCorreo")
                        TxtMensajeCorreoEnviadoEntidad.Text = dtCorreoEntidad.Rows(0).Item("ObservacionEnviadaEntidad")
                    End If


                    '''Ocultar el label de la lista de personas que se le enviaron correo
                    dvlblListadeAutoridades.Visible = False

                    '''Consultar las observaciones
                    Dim dtObservaciones As New DataTable
                    Dim dvObs As New DataView
                    dtObservaciones = ObjInterfacesJP.ValidarObservacionesRTRD(FlagIdProyecto.Value, FlagIdRTRD.Value)

                    '''Filtrar las observaciones que tengan seguimiento
                    dvObs = dtObservaciones.DefaultView
                    dvObs.RowFilter = "FlagObsCoordinador <> 1"
                    If dvObs.Count > 0 Then
                        dtObservaciones = dvObs.ToTable()
                    End If

                    '''Cargar el gridview
                    If dtObservaciones.Rows.Count > 0 Then
                        PnlSeguimientoEntidad.Visible = True
                        'dvgridBitacora.Visible = True
                        grvBitacora.DataSource = dtObservaciones
                        grvBitacora.DataBind()
                    Else
                        'dvgridBitacora.Visible = False
                        PnlSeguimientoEntidad.Visible = False
                    End If

                End If



            Else
                lblTituloModalCierreRTRD.InnerText = "Cerrar RTRD sin Hallazgos"
                PnlSeguimientoEntidad.Visible = False
                dvgridBitacora.Visible = False

                ValidarSiTieneEvaluacionTecnica()

                BtnCerrarRTRD.Visible = True
                BtnCerrarRTRD.Enabled = True
                BtnCerrarRTRD.ToolTip = ("")
                ucAdjuntarObservacionRTRD.InicializarControl(ucAdjuntarObservacionRTRD.Modo.AdjuntarDocumentosConCabecera, FlagIdProyecto.Value, FlagIdRTRD.Value, dt.Rows(0).Item("IdObs"), True, "mdlCerrarRTRDSinHallazgos", "card-header bg-success text-bold text-white", "card border-success")
                '(ByVal funcionamiento As Modo, ByVal idProyecto As Integer, Optional ByVal SecuencialRTRD As Integer = 0, Optional ByVal IdObs As String = "", Optional ByVal MostrarModal As Boolean = False, Optional ByVal NombreModal As String = "", Optional ByVal ClaseColorPanel As String = "bg-primary", Optional ByVal ClaseColorBordePanel As String = "card border-primary")
            End If


        Else

            lblTituloModalCierreRTRD.InnerText = "Cerrar RTRD sin Hallazgos"

            PnlSeguimientoEntidad.Visible = False
            dvgridBitacora.Visible = False

            btnVerEvaluacion.Visible = False
            'dvgridBitacora.Visible = False

            '''Mostrar el asterisco de obligatorio
            RequeridoMotivoCierre.Visible = True

            '''Cambia el titulo del campo de texto de la observación
            lbletiquetaTxtMotivoCierre.InnerText = "Motivo de cierre:"

            '''Habilitar el panel de motivo de cierre
            TxtObservacion_SinHallazgos.Text = ""
            TxtObservacion_SinHallazgos.Enabled = True

            '''Mostrar el botón de guardar el motivo de cierre
            pnlbtnGuardarObs.Visible = False

            '''Mostrar el botón de cerrar RTRD
            BtnCerrarRTRD.Visible = True

            '''Ocultar el panel con los datos del usuario que realizo el cierre del RTRD
            pnlDatosLider.Visible = False

            '''Bloquear el boton de cerrar el RTRD cuando no tiene observacion almacenada
            BtnCerrarRTRD.Enabled = False

            '''Agregando mensaje de tooltip al boton de cerrar RTRD
            BtnCerrarRTRD.ToolTip = ("No se puede cerrar el RTRD sin Observación")

            '''Inicializar el UserControl de los adjuntos en Modo sin cabecera ya que no tiene observaciones almacenadas
            ucAdjuntarObservacionRTRD.InicializarControl(ucAdjuntarObservacionRTRD.Modo.AdjuntarDocumentoSinCabecera, FlagIdProyecto.Value, FlagIdRTRD.Value, , True, "mdlCerrarRTRDSinHallazgos", "card-header bg-success text-bold text-white", "card border-success")

        End If
    End Sub

    Private Sub BtnRTRD_EnviarEntidad_Click(sender As Object, e As EventArgs) Handles BtnRTRD_EnviarEntidad.Click

        '''
        '''Invocar el metodo que inicia el UserControl en Forma de Modal
        'ucEnviarRtrdEntidad.InicializarControlModal(FlagIdProyecto.Value, FlagIdRTRD.Value, FlagIdVisitaRTRD.Value, FlagIdExpediente.Value)

        Dim dtRTRD As New DataTable
        dtRTRD = objInterRF.VerDatosRTRD(FlagIdProyecto.Value, FlagIdVisitaRTRD.Value)

        '''Valida el estado del RTRD para configurar la pantalla 
        'If dtRTRD.Rows(0)("IdEstadoRtRd") = 2 Then '''Atendido Fiscalizador

        '    MultiView3.ActiveViewIndex = 4

        '    '''Oculta el seguimiento
        '    dvSeguimientoEntidad.Visible = False

        '    dvUserControlEnviarRTRDCoordinador.Visible = False
        '    dvUserControlEnviarRTRDEntidad.Visible = True

        '    '''Invocar el metodo que inicia el UserControl
        '    ucEnviarRtrdEntidad.InicializarControlModal(FlagIdProyecto.Value, FlagIdRTRD.Value, FlagIdVisitaRTRD.Value, FlagIdExpediente.Value)

        '    '''Ocultar el UserControl de envio al coordinador
        '   ' DvUcCoordinador.Visible = False

        If dtRTRD.Rows(0)("IdEstadoRtRd") = 4 Then '''Por subsanar entidad
            FlagEnvioDirectoCoordinador.Text = "0"

            FlagOpcionRetorno.Value = "2"
            '''
            'dvBtnNuevoSeguimiento.Enabled = True
            dvBtnEnviarEvaluacion.Enabled = True
            'dvBtnCerrarRTRD.Enabled = True
            'dvBtnEnviarCoordinador.Enabled = True

            '''
            MultiView1.ActiveViewIndex = 6

            '''
            AdministrarBotonEvaluacionTecnica()

            '''Habilita la pantalla de seguimiento
            dvSeguimientoEntidad.Visible = True

            '''Carga el encabezado
            ucEncabezadosDetalleRTRD1.InicializarControl(ucEncabezados.Modelo.EncabezadoGeneral, FlagIdProyecto.Value, FlagIdRTRD.Value, FlagIdVisitaRTRD.Value)

            '''
            dvRegresarSuperior.Visible = True

            '''Ocultar el UserControl de envio al coordinador
            DvUcCoordinador.Visible = False

            '''Consultar los datos del correo enviado a la entidad
            Dim dtCorreoEntidad As New DataTable
            dtCorreoEntidad = ObjInterfacesJP.ConsultarCorreoEnvioEntidad(FlagIdProyecto.Value, FlagIdRTRD.Value)

            If dtCorreoEntidad.Rows.Count > 0 Then
                lblDatosUserEnviaCorreoEntidad.InnerText = "Correo enviado a la Entidad por el Líder Provincial – " + dtCorreoEntidad.Rows(0).Item("NombreUsuario") + " el " + dtCorreoEntidad.Rows(0).Item("FechaEnvioCorreo")
                TxtMensajCorreoEnviadoEntidad.Text = dtCorreoEntidad.Rows(0).Item("ObservacionEnviadaEntidad")
            End If

            If dtRTRD.Rows(0).Item("IdContactosAutoridad") <> "" Then 'si tiene data busca las autoridades

                '''Realizar la consulta de las autoridades que recibieron correo al enviar el RTRD a la entidad
                Dim dtAutoridades As DataTable = ObjInterfacesJP.ListarAutoridades_RecibieronCorreo(FlagIdProyecto.Value, FlagIdRTRD.Value)

                If dtAutoridades.Rows.Count > 0 Then
                    grvContactoSeguimiento.Visible = True
                    grvContactoSeguimiento.DataSource = dtAutoridades
                    grvContactoSeguimiento.DataBind()
                Else
                    grvContactoSeguimiento.DataSource = dtEmpty
                    grvContactoSeguimiento.DataBind()
                End If


                '''almacenar las autoridades que recibieron correo en una variable para luego ser utilizada
                Dim ListId As New List(Of String)
                Dim ListaIdContactos As String = ""
                For Each row As DataRow In dtAutoridades.Rows()
                    ListId.Add(row.Item("Identificador"))
                    ListaIdContactos = String.Join(",", ListId.ToArray())
                Next

                ParamIdListaContactosEntidad.Value = ListaIdContactos

            Else

                '''Buscar las autoridades en la tabla de observaciones del rtrd
                'BuscarAutoridades_ObservacionesRTRD()

                grvContactoSeguimiento.DataSource = dtEmpty
                grvContactoSeguimiento.DataBind()

            End If

            '''Cargar la tabla con los registros del seguimiento a la entidad
            CargarRegistrosSeguimientoEntidad()

            '''Listar todas las Autoridades y almacenarla en una variable para luego ser utilizada
            'ListarAutoridades(FlagIdProyecto.Value)


            'Consulto todos los RTRD del EXPEDIENTE
            Dim dtCantRTRD_Expediente As DataTable = ObjInterfacesJP.ConsultarEstadoRTRDbyExpediente(FlagIdProyecto.Value)

            If dtCantRTRD_Expediente.Rows.Count > 0 Then
                Dim UltimoRTRD As String = dtCantRTRD_Expediente.Rows.Count
                Dim CantRTRDPorSubsanar As Integer = 0
                Dim CantRTRDAtendidosFisc As Integer = 0
                Dim CantRTRDEnviadoCoordinador As Integer = 0

                ''''Verifico los estados de todos los RTRD anteriores 
                For Each row In dtCantRTRD_Expediente.Rows
                    If row.Item("EstadoRtRd") = "4" Or row.Item("EstadoRtRd") = "6" Then 'Por subsanar Entidad
                        CantRTRDPorSubsanar = CantRTRDPorSubsanar + 1
                    ElseIf row.Item("EstadoRtRd") = "2" Then '
                        CantRTRDAtendidosFisc = CantRTRDAtendidosFisc + 1
                    ElseIf row.Item("EstadoRtRd") = "5" Then 'Por atender Coordinador Nacional
                        CantRTRDEnviadoCoordinador = CantRTRDEnviadoCoordinador + 1
                    End If
                Next

                '''Validar si este es el ultimo RTRD y si el estado es atendido fiscalizador o se esta subsanando.
                If (FlagIdRTRD.Value = UltimoRTRD And CantRTRDPorSubsanar = 1 And CantRTRDAtendidosFisc >= 0 And CantRTRDEnviadoCoordinador = 0) Then
                    'dvBtnEnviarCoordinador.Visible = True
                Else
                    'dvBtnEnviarCoordinador.Visible = False
                End If

            End If
            '



        End If

    End Sub

    Private Sub CargarRegistrosSeguimientoEntidad()
        dt = ObjInterfacesJP.ValidarObservacionesRTRD(FlagIdProyecto.Value, FlagIdRTRD.Value)
        grvBitacoraSeguimientos.Visible = True
        Dim dvObs As New DataView
        dvObs = dt.DefaultView
        dvObs.RowFilter = "FlagObsCoordinador = 0 and FlagObsCoordinador <> 1"
        If dvObs.Count > 0 Then
            dt = dvObs.ToTable()

            ''' Cargar el grid con los seguimientos
            grvBitacoraSeguimientos.DataSource = dt
            grvBitacoraSeguimientos.DataBind()

        Else
            dt.Clear()
            grvBitacoraSeguimientos.DataSource = dt
            grvBitacoraSeguimientos.DataBind()
        End If


        'If dt.Rows.Count > 0 Then

        '''Crear La tabla donde se almacenara los contactos que estan sepados por coma en la IdAutoridad del dt de Observaciones
        'Dim dtContactosEntidad As New DataTable
        '    dtContactosEntidad.Columns.Add("IdContacto")
        '    dtContactosEntidad.Columns.Add("NombreCompleto")
        '    dtContactosEntidad.Columns.Add("BuzonCorreo")
        '    dtContactosEntidad.Columns.Add("Telefono")
        '    dtContactosEntidad.Columns.Add("Rol")

        '    Dim CadenaSepararadaporComa As String = ""

        '    For Each row In dt.Rows

        '        If row.Item("IdAutoridad") <> "" Then

        '            CadenaSepararadaporComa = row.Item("IdAutoridad")

        '            For Each IdAutoridad In CadenaSepararadaporComa.Split(",")

        '                Dim dtDatosAutoridad As New DataTable
        '                dtDatosAutoridad = ObjInterfacesLD.BuscarAutoridadID(IdAutoridad)

        '                Dim Nombre As String = dtDatosAutoridad.Rows(0).Item("Nombre")
        '                Dim Apellido As String = dtDatosAutoridad.Rows(0).Item("Apellido")
        '                Dim NombreCompleto = String.Format("{0} {1}", Nombre, Apellido)
        '                Dim BuzonCorreo As String = dtDatosAutoridad.Rows(0).Item("BuzonCorreo")
        '                Dim Telefono As String = dtDatosAutoridad.Rows(0).Item("Telefono")

        '                dtContactosEntidad.Rows.Add(IdAutoridad, NombreCompleto, BuzonCorreo, Telefono, "Autoridades")

        '            Next

        '            '''Validar que la tabla tenga registros de contactos y luego Cargar el grid con los contactos que recibieron correo Electronico
        '            If dtContactosEntidad.Rows.Count > 0 Then
        '                grvContactoSeguimiento.Visible = True
        '                grvContactoSeguimiento.DataSource = Nothing
        '                grvContactoSeguimiento.DataSource = dtContactosEntidad
        '                grvContactoSeguimiento.DataBind()
        '                ParamIdListaContactosEntidad.Text = CadenaSepararadaporComa
        '            End If

        '            '''Agregara un nuevo registro que guardara el correo del jefe Regional
        '            Dim dtJefeRegional_DFG As DataTable = ObjInterfacesJP.ConsultarCorreo_DFG(ParamIdProyecto.Text)
        '            If dtJefeRegional_DFG.Rows.Count > 0 Then

        '                Dim Id As String = dtJefeRegional_DFG.Rows(0).Item("idReg")
        '                Dim Nombre As String = dtJefeRegional_DFG.Rows(0).Item("Nombre")
        '                Dim Correo As String = dtJefeRegional_DFG.Rows(0).Item("Correo")
        '                dtContactosEntidad.Rows.Add(Id, Nombre, Correo, "", "DFG")

        '            End If


        '            '''Finalizar el recorrido de las observaciones por que ya se encontro un registro con los Ids de las autoridades
        '            Exit For

        '        End If

        '    Next







        'End If




    End Sub

    Private Sub AdministrarBotonEvaluacionTecnica()

        ''' Consultar si esta opción esta habilitada para el usuario
        Dim PantallaEvaluacionTecnica = lws.ObtenerParametroString(85)

        If PantallaEvaluacionTecnica = "N" Then
            dvBtnEnviarEvaluacion.Visible = False
        Else

            Dim dtDatosRTRD As New DataTable
            Dim EstadoRtrd As Integer = 0
            dtDatosRTRD = objInterRF.VerDatosRTRD(FlagIdProyecto.Value, FlagIdVisitaRTRD.Value)

            If (dtDatosRTRD.Rows.Count > 0) Then
                EstadoRtrd = dtDatosRTRD.Rows(0)("IdEstadoRtRd")

                If (EstadoRtrd = 4 Or EstadoRtrd = 6) Then

                    '''Consultar 
                    Dim dtRTRDEvaluacion As DataTable = ObjInterfacesJP.ConsultarEvaluacionTecnicaRTRD(FlagIdProyecto.Value, FlagIdRTRD.Value)

                    ''''Consultar el estado en la tabla de parametros si el boton de evaluacion se muestra con control de fecha o no 
                    Dim MostrarBtnEvaluacionTecnicaSinControlFecha = lws.ObtenerParametroString(73)

                    If dtDatosRTRD.Rows(0).Item("EstadoEvaluacion") <> "N" Then

                        '''Mostrar el boton
                        dvBtnEnviarEvaluacion.Visible = True

                        '''cambiar tooltip
                        Dim dtEnviarEvaluacion = ObjInterfacesJP.ConsultarMensaje("TOOL5", "")
                        dvBtnEnviarEvaluacion.ToolTip = (dtEnviarEvaluacion.Rows(0).Item("Mensaje"))

                        '''Cambiar el texto del boton
                        dvBtnEnviarEvaluacion.Text = "Ver Evaluación Técnica"

                    Else

                        '''VALIDAR EL BOTON SE MUESTRA O NO
                        If MostrarBtnEvaluacionTecnicaSinControlFecha = "S" Then

                            If dtRTRDEvaluacion.Rows.Count > 0 Then

                                If dtRTRDEvaluacion.Rows(0).Item("CantDiasRTRD") >= dtRTRDEvaluacion.Rows(0).Item("CantidadDiasEvaluacionTecnica") And dtRTRDEvaluacion.Rows(0).Item("EstadoRtRd") = 4 Then
                                    dvBtnEnviarEvaluacion.Visible = True

                                Else
                                    dvBtnEnviarEvaluacion.Visible = False

                                End If

                            Else
                                dvBtnEnviarEvaluacion.Visible = False
                            End If


                        ElseIf MostrarBtnEvaluacionTecnicaSinControlFecha = "N" Then
                            dvBtnEnviarEvaluacion.Visible = False
                        Else

                            If dtRTRDEvaluacion.Rows.Count > 0 Then

                                If dtRTRDEvaluacion.Rows(0).Item("CantDiasRTRD") >= dtRTRDEvaluacion.Rows(0).Item("CantidadDiasEvaluacionTecnica") Then
                                    dvBtnEnviarEvaluacion.Visible = True
                                Else
                                    dvBtnEnviarEvaluacion.Visible = False
                                End If

                            Else
                                dvBtnEnviarEvaluacion.Visible = False
                            End If



                        End If


                        '''cambiar tooltip
                        Dim dtEnviarEvaluacion = ObjInterfacesJP.ConsultarMensaje("TOOL2", "")
                        dvBtnEnviarEvaluacion.ToolTip = (dtEnviarEvaluacion.Rows(0).Item("Mensaje"))

                        '''Cambiar el texto del boton
                        dvBtnEnviarEvaluacion.Text = "Enviar a Evaluación Técnica"


                    End If

                Else
                    dvBtnEnviarEvaluacion.Visible = False
                End If

            End If

        End If

    End Sub

    Private Sub BuscarDestinatariosEnvioEntidad(Optional ByVal ConsultarJefeRegional As Boolean = False)

        '''Crear La tabla donde se almacenara los contactos que estan sepados por barra vertical 
        'dtContactosEntidad.Reset()
        Dim dtContactosEntidad As New DataTable
        dtContactosEntidad.Columns.Add("IdContacto")
        dtContactosEntidad.Columns.Add("NombreCompleto")
        dtContactosEntidad.Columns.Add("BuzonCorreo")
        dtContactosEntidad.Columns.Add("Telefono")
        dtContactosEntidad.Columns.Add("Rol")

        Dim dt As New DataTable

        dt = ObjInterfacesJP.ListarAutoridadesLP(FlagIdProyecto.Value)

        If dt.Rows.Count > 0 Then

            For Each row In dt.Rows()
                Dim IdAutoridad = row.Item("IdAutoridad")
                Dim NombreCompleto = row.Item("NombreCompleto")
                Dim BuzonCorreo As String = row.Item("BuzonCorreo")
                Dim Telefono As String = row.Item("Telefono")
                dtContactosEntidad.Rows.Add(IdAutoridad, NombreCompleto, BuzonCorreo, Telefono, "Autoridades")
            Next

            '''Validar que la tabla tenga registros de contactos y luego Cargar el grid con los contactos que recibieron correo Electronico
            If dtContactosEntidad.Rows.Count > 0 Then
                grvContactos.DataSource = dtContactosEntidad
                grvContactos.DataBind()
            End If


            If ConsultarJefeRegional = True Then

                '''Agregara un nuevo registro que guardara el correo del jefe Regional
                Dim dtJefeRegional_DFG As DataTable = ObjInterfacesJP.ConsultarCorreo_DFG(FlagIdProyecto.Value)

                If dtJefeRegional_DFG.Rows.Count > 0 Then

                    For Each row In dtJefeRegional_DFG.Rows()
                        Dim Id As String = row.Item("idReg")
                        Dim Nombre As String = row.Item("Nombre")
                        Dim Correo As String = row.Item("Correo")
                        dtContactosEntidad.Rows.Add(Id, Nombre, Correo, "", "DFG")
                    Next

                Else

                    dtMensaje = ObjInterfacesJP.ConsultarMensaje("NOJFR", "")
                    Funciones.ModalAlertCodeBehind(dtMensaje.Rows(0).Item("Mensaje"))

                End If

            End If

            'Return dtContactosEntidad

        End If

        'Session("TablaContactos") = dtContactosEntidad
        'Return Nothing

    End Sub

    Private Sub ValidarSiTieneEvaluacionTecnica()

        Dim dtRTRDEvaluacion As DataTable = ObjInterfacesJP.ConsultarFlagEvaluacionTecnicaRTRD(FlagIdProyecto.Value, FlagIdRTRD.Value)

        If dtRTRDEvaluacion.Rows.Count > 0 Then
            If dtRTRDEvaluacion.Rows(0).Item("FlagEvaluacionTecnica") = "A" Or dtRTRDEvaluacion.Rows(0).Item("FlagEvaluacionTecnica") = "S" Then
                btnVerEvaluacion.Visible = True
                'dvgridBitacora.Visible = True
            Else
                btnVerEvaluacion.Visible = False
                'dvgridBitacora.Visible = False
            End If
        End If

    End Sub

    Private Sub btnRegresarCB_Click(sender As Object, e As EventArgs) Handles btnRegresarCB.Click

        Dim dtRTRD As New DataTable
        dtRTRD = objInterRF.VerDatosRTRD(FlagIdProyecto.Value, FlagIdVisitaRTRD.Value)

        'Dim FlagRtRdSinHallazgos As Integer = dtRTRD.Rows(0)("FlagRtRdSinHallazgos")

        '''Valida el estado del RTRD para configurar la pantalla de Retorno
        If dtRTRD.Rows(0)("IdEstadoRtRd") = 3 And dtRTRD.Rows(0)("FlagRtRdSinHallazgos") = 1 Then

            dvBotonesAtencionRTRD.Attributes.Add("class", "col-6 text-right pt-1")
            'BtnRTRD_SinHallazgos.Visible = True
            'BtnRTRD_SinHallazgos.Text = "Ver Seguimiento"
            'BtnRTRD_EnviarEntidad.Visible = False
            'BtnRTRD_EnviarCoordinador.Visible = False
            'FlagOpcionRetorno.Value = "2"
            'FlagOpcionRetorno.Value = "1"

        ElseIf dtRTRD.Rows(0)("IdEstadoRtRd") = 4 Then

            '''
            dvBotonesAtencionRTRD.Attributes.Remove("class")
            dvBotonesAtencionRTRD.Attributes.Add("class", "col-6 text-right pt-1")

            'BtnRTRD_SinHallazgos.Visible = False
            'BtnRTRD_EnviarEntidad.Visible = True
            'BtnRTRD_EnviarEntidad.Text = "Dar Seguimiento"
            'BtnRTRD_EnviarCoordinador.Visible = False
            'FlagOpcionRetorno.Value = "2"
            'FlagOpcionRetorno.Value = "1"

        ElseIf dtRTRD.Rows(0)("IdEstadoRtRd") = 5 Then
            dvBotonesAtencionRTRD.Attributes.Remove("class")
            dvBotonesAtencionRTRD.Attributes.Add("class", "col-6 text-right pt-1")
            'BtnRTRD_SinHallazgos.Visible = False
            'BtnRTRD_EnviarEntidad.Visible = False
            'BtnRTRD_EnviarCoordinador.Visible = True
            'BtnRTRD_EnviarCoordinador.Text = "Ver Seguimiento"
            If MultiView1.ActiveViewIndex = 4 Then
                FlagOpcionRetorno.Value = "1"
            Else
                FlagOpcionRetorno.Value = "2"
            End If

        ElseIf dtRTRD.Rows(0)("IdEstadoRtRd") = 2 Then

            '''
            dvBotonesAtencionRTRD.Attributes.Remove("class")
            dvBotonesAtencionRTRD.Attributes.Add("class", "col-6 d-flex justify-content-around pt-1")

            'FlagOpcionRetorno.Value = "1"
            'BtnRTRD_SinHallazgos.Visible = True
            'BtnRTRD_SinHallazgos.Text = "RTRD sin Hallazgos"
            'BtnRTRD_EnviarEntidad.Visible = True
            'BtnRTRD_EnviarEntidad.Text = "Enviar a Entidad"
            'BtnRTRD_EnviarCoordinador.Visible = True
            'BtnRTRD_EnviarCoordinador.Text = "Enviar al Coordinador"
        End If

        '''Valida la pantalla de Retorno
        If (FlagOpcionRetorno.Value = "1") Then ''Regresa al Historial del RTRD
            TabsDatosProyecto.Visible = True
            MultiView1.ActiveViewIndex = 3
            pnlBotonRegresar.Visible = True
            dvRegresarSuperior.Visible = False
            ucHistorialRTRD.CargarHistorial(FlagIdProyecto.Value, True, "Coord")

        ElseIf (FlagOpcionRetorno.Value = "2" Or "3") Then ''Regresa al detalle del RTRD abierto
            TabsDatosProyecto.Visible = False
            MultiView1.ActiveViewIndex = 4
            pnlBotonRegresar.Visible = False
            FlagOpcionRetorno.Value = "1"
            dvRegresarSuperior.Visible = True
        End If


        'If FlagOpcionRetorno.Value = "2" Then
        '    dvRegresarSuperior.Visible = True
        'End If

        ucEncabezadosDetalleRTRD.InicializarControl(ucEncabezados.Modelo.EncabezadoGeneral, FlagIdProyecto.Value, FlagIdRTRD.Value, FlagIdVisitaRTRD.Value)
        'dvBotonesAtencionRTRD.Attributes.Remove("class")


    End Sub

    'Private Sub BtnRTRD_Seguimiento_Click(sender As Object, e As EventArgs) Handles BtnRTRD_Seguimiento.Click
    '    Dim dtRTRD As New DataTable
    '    dtRTRD = objInterRF.VerDatosRTRD(FlagIdProyecto.Value, FlagIdVisitaRTRD.Value)
    '    FlagEnvioDirectoCoordinador.Text = "0"

    '    FlagOpcionRetorno.Value = "2"
    '    '''
    '    'dvBtnNuevoSeguimiento.Enabled = True
    '    dvBtnEnviarEvaluacion.Enabled = True
    '    'dvBtnCerrarRTRD.Enabled = True
    '    'dvBtnEnviarCoordinador.Enabled = True

    '    '''
    '    MultiView1.ActiveViewIndex = 6

    '    '''
    '    AdministrarBotonEvaluacionTecnica()

    '    '''Habilita la pantalla de seguimiento
    '    dvSeguimientoEntidad.Visible = True

    '    '''Carga el encabezado
    '    ucEncabezadosDetalleRTRD1.InicializarControl(ucEncabezados.Modelo.EncabezadoGeneral, FlagIdProyecto.Value, FlagIdRTRD.Value, FlagIdVisitaRTRD.Value)

    '    '''
    '    dvRegresarSuperior.Visible = True

    '    '''Ocultar el UserControl de envio al coordinador
    '    DvUcCoordinador.Visible = True
    '    dvUserControlEnviarRTRDCoordinador.Visible = True
    '    grvBitacoraSeguimientos.Visible = True

    '    '''Consultar los datos del correo enviado a la entidad
    '    Dim dtCorreoEntidad As New DataTable
    '    dtCorreoEntidad = ObjInterfacesJP.ConsultarCorreoEnvioEntidad(FlagIdProyecto.Value, FlagIdRTRD.Value)

    '    If dtCorreoEntidad.Rows.Count > 0 Then
    '        lblDatosUserEnviaCorreoEntidad.InnerText = "Correo enviado a la Entidad por el Líder Provincial – " + dtCorreoEntidad.Rows(0).Item("NombreUsuario") + " el " + dtCorreoEntidad.Rows(0).Item("FechaEnvioCorreo")
    '        TxtMensajCorreoEnviadoEntidad.Text = dtCorreoEntidad.Rows(0).Item("ObservacionEnviadaEntidad")
    '    End If

    '    If dtRTRD.Rows(0).Item("IdContactosAutoridad") <> "" Then 'si tiene data busca las autoridades

    '        '''Realizar la consulta de las autoridades que recibieron correo al enviar el RTRD a la entidad
    '        Dim dtAutoridades As DataTable = ObjInterfacesJP.ListarAutoridades_RecibieronCorreo(FlagIdProyecto.Value, FlagIdRTRD.Value)

    '        If dtAutoridades.Rows.Count > 0 Then
    '            grvContactoSeguimiento.Visible = True
    '            grvContactoSeguimiento.DataSource = dtAutoridades
    '            grvContactoSeguimiento.DataBind()
    '        Else
    '            grvContactoSeguimiento.DataSource = dtEmpty
    '            grvContactoSeguimiento.DataBind()
    '        End If


    '        '''almacenar las autoridades que recibieron correo en una variable para luego ser utilizada
    '        Dim ListId As New List(Of String)
    '        Dim ListaIdContactos As String = ""
    '        For Each row As DataRow In dtAutoridades.Rows()
    '            ListId.Add(row.Item("Identificador"))
    '            ListaIdContactos = String.Join(",", ListId.ToArray())
    '        Next

    '        ParamIdListaContactosEntidad.Value = ListaIdContactos

    '    Else

    '        '''Buscar las autoridades en la tabla de observaciones del rtrd
    '        'BuscarAutoridades_ObservacionesRTRD()

    '        grvContactoSeguimiento.DataSource = dtEmpty
    '        grvContactoSeguimiento.DataBind()

    '    End If

    '    '''Cargar la tabla con los registros del seguimiento a la entidad
    '    CargarRegistrosSeguimientoEntidad()

    '    '''Listar todas las Autoridades y almacenarla en una variable para luego ser utilizada
    '    'ListarAutoridades(FlagIdProyecto.Value)


    '    'Consulto todos los RTRD del EXPEDIENTE
    '    Dim dtCantRTRD_Expediente As DataTable = ObjInterfacesJP.ConsultarEstadoRTRDbyExpediente(FlagIdProyecto.Value)

    '    If dtCantRTRD_Expediente.Rows.Count > 0 Then
    '        Dim UltimoRTRD As String = dtCantRTRD_Expediente.Rows.Count
    '        Dim CantRTRDPorSubsanar As Integer = 0
    '        Dim CantRTRDAtendidosFisc As Integer = 0
    '        Dim CantRTRDEnviadoCoordinador As Integer = 0
    '        Dim CantRTRDInvestigacion As Integer = 0

    '        ''''Verifico los estados de todos los RTRD anteriores 
    '        For Each row In dtCantRTRD_Expediente.Rows
    '            If row.Item("EstadoRtRd") = "4" Or row.Item("EstadoRtRd") = "6" Then 'Por subsanar Entidad
    '                CantRTRDPorSubsanar = CantRTRDPorSubsanar + 1
    '            ElseIf row.Item("EstadoRtRd") = "2" Then '
    '                CantRTRDAtendidosFisc = CantRTRDAtendidosFisc + 1
    '            ElseIf row.Item("EstadoRtRd") = "5" Then 'Por atender Coordinador Nacional
    '                CantRTRDEnviadoCoordinador = CantRTRDEnviadoCoordinador + 1
    '            ElseIf row.Item("EstadoRtRd") = "8" Then 'En investigación
    '                CantRTRDInvestigacion = CantRTRDInvestigacion + 1
    '            End If
    '        Next

    '        '''Validar si este es el ultimo RTRD y si el estado es atendido fiscalizador o se esta subsanando.
    '        If (FlagIdRTRD.Value = UltimoRTRD And CantRTRDPorSubsanar = 1 And CantRTRDAtendidosFisc >= 0 And CantRTRDEnviadoCoordinador = 0) Then
    '            'dvBtnEnviarCoordinador.Visible = True
    '        Else
    '            'dvBtnEnviarCoordinador.Visible = False
    '        End If

    '    End If
    'End Sub

    Private Sub btnVerEvaluacion_Click(sender As Object, e As EventArgs) Handles btnVerEvaluacion.Click
        ucEnviarEvaluacionTecnica.InicializarControl(FlagIdProyecto.Value, FlagIdRTRD.Value, FlagIdVisitaRTRD.Value)
    End Sub

    Private Sub dvBtnEnviarEvaluacion_Click(sender As Object, e As EventArgs) Handles dvBtnEnviarEvaluacion.Click
        ucEnviarEvaluacionTecnica.InicializarControl(FlagIdProyecto.Value, FlagIdRTRD.Value, FlagIdVisitaRTRD.Value)
    End Sub

    Private Sub BtnRTRD_Investigacion_Click(sender As Object, e As EventArgs) Handles BtnRTRD_Investigacion.Click

        CargarCatalogoMotivos()
        txtConclusionCierreExp.Text = ""
        notifyModal = "$('#mdlCerrarExpediente').modal('show');"
    End Sub

    Private Sub BtnRTRD_Devolver_Click(sender As Object, e As EventArgs) Handles BtnRTRD_Devolver.Click
        notifyModal = "$('#mdlDevolverExpediente').modal('show');"
    End Sub

    Private Sub RegistrarMotivoInvestigacionExp(ByVal IdProyecto As String, ByVal SecuencialRTRD As String)
        Dim Identificador As String = Session("IdentificadorUsuario")
        FlagIdProyecto.Value = IdProyecto
        FlagIdRTRD.Value = SecuencialRTRD

        Dim dt As New DataTable
        dt = ObjInterLSD.InsertarMotivoInvestigacionExp(FlagIdProyecto.Value, DdlMotivoCierreEx.SelectedValue, FlagIdRTRD.Value, txtConclusionCierreExp.Text, Identificador)
        If dt.Rows(0).Item("respuesta") = "OK" Then 'respuesta
            'EditUser.Value = "NO"

            dtMensaje = ObjInterfacesJP.ConsultarMensaje("INEXT", "")
            Funciones.ModalAlertCodeBehind(dtMensaje.Rows(0).Item("Mensaje"))
            Dim EstadoExp As String = "6"
            Dim EstadoRTRD As String = "8"

            Dim dt2 As New DataTable
            dt2 = ObjInterLSD.UpdateEstadoExp(FlagIdProyecto.Value, EstadoExp, Identificador)

            Dim dt3 As New DataTable
            dt3 = ObjInterLSD.UpdateEstadoRTRD(FlagIdProyecto.Value, SecuencialRTRD, EstadoRTRD, Identificador)

            BtnRTRD_Investigacion.Visible = False
            BtnRTRD_Devolver.Visible = False
            BtnRTRD_VerInvestigacion.Visible = True
        Else
            dtMensaje = ObjInterfacesJP.ConsultarMensaje("INERR", "")
            Funciones.ModalAlertCodeBehind(dtMensaje.Rows(0).Item("Mensaje"))
        End If
    End Sub

    Private Sub RegistrarMotivoDevolverExp(ByVal IdProyecto As String, ByVal SecuencialRTRD As String)
        Dim Identificador As String = Session("IdentificadorUsuario")
        FlagIdProyecto.Value = IdProyecto
        FlagIdRTRD.Value = SecuencialRTRD


        Dim dt As New DataTable
        dt = ObjInterLSD.InsertarMotivoDevolverExp(FlagIdProyecto.Value, FlagIdRTRD.Value, txtMotivoDevolver.Text, Identificador)
        If dt.Rows(0).Item("respuesta") = "OK" Then 'respuesta
            'EditUser.Value = "NO"

            dtMensaje = ObjInterfacesJP.ConsultarMensaje("INEXT", "")
            Funciones.ModalAlertCodeBehind(dtMensaje.Rows(0).Item("Mensaje"))
            Dim EstadoExp As String = "4"

            Dim dt2 As New DataTable
            dt2 = ObjInterLSD.UpdateEstadoExp(FlagIdProyecto.Value, EstadoExp, Identificador)

            Dim dt3 As New DataTable
            dt3 = ObjInterLSD.UpdateEstadoDevolverRTRD(FlagIdProyecto.Value, SecuencialRTRD, Identificador)

            BtnRTRD_Investigacion.Visible = False
            BtnRTRD_Devolver.Visible = False
            BtnRTRD_VerInvestigacion.Visible = False

            EnviarCorreo(FlagIdProyecto.Value, FlagIdRTRD.Value)
        End If

    End Sub

    Private Sub EnviarCorreo(ByVal IdProyecto As String, ByVal SecuencialRTRD As String)
        '''Buscar los correos de los destinatarios asignados al proyecto.
        Dim IdUsuario As String = Session("IdentificadorUsuario")
        Dim dtCorreos As New DataTable
        Dim Destinatarios As String = ""
        FlagIdProyecto.Value = IdProyecto
        FlagIdRTRD.Value = SecuencialRTRD

        'dtCorreos = ObjInterLSD.ListarEmailEnvioLider(FlagIdProyecto.Value)
        dtCorreos = ObjInterLSD.ListarEmailEnvioLider2(FlagIdProyecto.Value)

        If dtCorreos.Rows.Count > 0 Then
            For Each row As DataRow In dtCorreos.Rows()
                If row.Item("Email") <> "" Then
                    Destinatarios = Destinatarios + row.Item("Email") + ";"
                End If
            Next

            '''Consultar Motivo de devolucion
            Dim dt As New DataTable
            dt = ObjInterLSD.ConsultarMotivoDevolucion(FlagIdProyecto.Value, FlagIdRTRD.Value)
            Dim Motivo As String = dt.Rows(0).Item("MotivoAccion")


            '''Consultar los datos del Proyecto
            Dim dtDatosProyecto As New DataTable
            dtDatosProyecto = objInterRF.VerDatosProyectos(FlagIdProyecto.Value)

            If dtDatosProyecto.Rows.Count > 0 Then
                Dim resultadoEnvioCorreo As Boolean = False

                Dim EntidadDependencia As String = ""
                Dim Entidad As String = dtDatosProyecto.Rows(0).Item("Entidad")
                Dim Expediente As String = dtDatosProyecto.Rows(0).Item("Secuencial").ToString
                'Dim SecuencialRTRD As String = FlagIdProyecto.Value

                '''Consultar los nombres de usuario con el Identificador
                Dim dtNombreLP As New DataTable
                dtNombreLP = ObjInterfacesJP.ConsultarNombreUsuario_ById(IdUsuario)

                Dim NombreLiderProvincial As String = dtNombreLP.Rows(0).Item("NombreUsuario")
                Dim provincia As String = dtDatosProyecto.Rows(0)("NombreProvincia")
                Dim distrito As String = dtDatosProyecto.Rows(0)("NombreDistrito")
                Dim corregimiento As String = dtDatosProyecto.Rows(0)("NombreCorregimiento")
                Dim lugarPoblado As String = dtDatosProyecto.Rows(0)("LugarPoblado")
                Dim Ubicacion As String = ""
                If String.IsNullOrWhiteSpace(lugarPoblado) Then
                    Ubicacion = String.Format("{0} / {1} / {2}", provincia, distrito, corregimiento)
                Else
                    Ubicacion = String.Format("{0}/{1}/{2}/{3}", provincia, distrito, corregimiento, lugarPoblado)
                End If
                Dim Asunto As String = "Control Social en los Gobiernos Locales"
                Dim Parametros As String = Expediente + "|" + SecuencialRTRD + "|" + NombreLiderProvincial + "|" + Entidad + "|" + Ubicacion + "|" + Motivo
                resultadoEnvioCorreo = lws.EnviarCorreoPlantilla("NERTRDL", Destinatarios, Asunto, Parametros)

            Else
                Funciones.ModalErrorCodeBehind("No se encontraron los datos del proyecto para enviar al Lider")
            End If
        End If

        '''Mostrar mensaje de confirmacion de envio al Lider
        dtMensaje = ObjInterfacesJP.ConsultarMensaje("RTENV", "")
        Funciones.ModalAlertCodeBehind(dtMensaje.Rows(0).Item("Mensaje"))
    End Sub

    Private Sub CargarCatalogoMotivos()
        DdlMotivoCierreEx.Items.Clear()
        Dim dt As New DataTable

        dt = ObjInterLSD.ListarMotivos()

        ''limpiar Dropdown
        DdlMotivoCierreEx.Items.Clear()

        'DdlMotivoCierreEx.Items.Add(New ListItem("-- Seleccione Motivo --", -1))

        For Each row As DataRow In dt.Rows
            DdlMotivoCierreEx.Items.Add(New ListItem(row("Motivo"), row("IdMotivo")))
        Next
    End Sub

    Protected Sub grvAdjuntos_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Select" Then
            'Dim gvAdjuntos As GridView = TryCast(e.Row.FindControl("grvAdjuntos"), GridView)
            Dim gvAdjuntos As GridView = DirectCast(sender, System.Web.UI.WebControls.GridView)

            Dim index As Integer = e.CommandArgument
            Dim NombreArch As String = gvAdjuntos.DataKeys(index)("URLImg")
            AbrirArchivoFTP(NombreArch)
        End If
    End Sub

    Private Sub AbrirArchivoFTP(ByVal nombreArchivo As String)


        Try
            Dim objGen As New InterfazGeneral
            Dim servidorFTP As String = objGen.ObtenerParametroString(26)
            Dim usuarioFTP As String = objGen.ObtenerParametroString(28)
            Dim passwordFTP As String = objGen.ObtenerParametroString(29)
            Dim puertoFTP As Integer = objGen.ObtenerParametroString(30)
            Dim directorioFTP As String = objGen.ObtenerParametroString(31)

            Using ftp As New FtpClient(servidorFTP, usuarioFTP, passwordFTP)
                ftp.Port = puertoFTP

                ftp.Connect()
                'ftp.SetWorkingDirectory(directorioFTP)

                Dim byteArray As Byte()

                If ftp.Download(byteArray, nombreArchivo) Then

                    '' Clear all content output from the buffer stream
                    Response.Clear()

                    '' Set the HTTP MIME type of the output stream
                    Response.ContentType = "application/octet-stream"

                    '' Add a HTTP header to the output stream that specifies the default filename
                    '' for the browser's download dialog
                    Response.AddHeader("Content-Disposition", "attachment; filename=" & nombreArchivo)
                    '' Add a HTTP header to the output stream that contains the 
                    '' content length(File Size). This lets the browser know how much data is being transfered
                    Response.AddHeader("Content-Length", byteArray.Length.ToString())

                    '' Write the data out to the client.
                    Response.BinaryWrite(byteArray)

                    Response.Flush()
                    Response.End()

                End If
            End Using

        Catch ex As Exception

            'dtMensaje = ObjInterfacesJP.ConsultarMensaje("ERRDF", "")
            'Funciones.ModalAlertCodeBehind(dtMensaje.Rows(0).Item("Mensaje"))

        End Try

    End Sub

    Private Sub btnEnviarInvestigacionExp_Click(sender As Object, e As EventArgs) Handles btnEnviarInvestigacionExp.Click
        RegistrarMotivoInvestigacionExp(FlagIdProyecto.Value, FlagIdRTRD.Value)
        BtnUpdateEncabezado_Click(Nothing, Nothing)
    End Sub

    Private Sub btnDevolver_Click(sender As Object, e As EventArgs) Handles btnDevolver.Click
        RegistrarMotivoDevolverExp(FlagIdProyecto.Value, FlagIdRTRD.Value)
        BtnUpdateEncabezado_Click(Nothing, Nothing)
        BtnRTRD_Investigacion.Visible = False
        BtnRTRD_Devolver.Visible = False
        BtnRTRD_EnviarCoordinador.Visible = False
        'BtnRTRD_Seguimiento.Visible = False
        BtnRTRD_SinHallazgos.Visible = False
        BtnRTRD_VerInvestigacion.Visible = False
    End Sub

    Private Sub BtnRTRD_VerInvestigacion_Click(sender As Object, e As EventArgs) Handles BtnRTRD_VerInvestigacion.Click
        VerInvestigacion(FlagIdProyecto.Value, FlagIdRTRD.Value)
        notifyModal = "$('#mdlConsultaExpediente').modal('show');"
    End Sub

    Private Sub VerInvestigacion(ByVal IdProyecto As String, ByVal SecuencialRTRD As String)
        FlagIdProyecto.Value = IdProyecto
        FlagIdRTRD.Value = SecuencialRTRD
        Dim dt As New DataTable

        dt = ObjInterLSD.ConsultarMotivoInvestigacion(FlagIdProyecto.Value, FlagIdRTRD.Value)


        For Each row As DataRow In dt.Rows
            DdlConsultaMotivo.Items.Add(New ListItem(row("Motivo"), row("IdMotivo")))
            txtConsultaConclusion.Text = row("MotivoAccion")
        Next

        DdlConsultaMotivo.Enabled = False
        txtConsultaConclusion.Enabled = False
    End Sub

    Private Sub BtnUpdateEncabezado_Click(sender As Object, e As EventArgs) Handles BtnUpdateEncabezado.Click
        ucEncabezadosDetalleRTRD.InicializarControl(ucEncabezados.Modelo.EncabezadoGeneral, FlagIdProyecto.Value, FlagIdRTRD.Value, FlagIdVisitaRTRD.Value)
    End Sub

    Private Sub BtnRTRD_EnviarCoordinador_Click(sender As Object, e As EventArgs) Handles BtnRTRD_EnviarCoordinador.Click
        'dvRegresarSuperior.Visible = True

        '''Habilita la vista 7
        'MultiView1.ActiveViewIndex = 7

        '''
        FlagOpcionRetorno.Value = "1"

        '''
        'dvUserControlEnviarRTRDCoordinador.Visible = True

        '''Ocultar el UserControl de envio al coordinador
        'DvUcCoordinador.Visible = True

        '''
        dvUserControlEnviarRTRDEntidad.Visible = False

        dvUserControlEnviarRTRDCoordinador.Visible = True


        ''' setear la variable en 1 para identificar que se esta enviando directo al coordinador
        FlagEnvioDirectoCoordinador.Text = "1"

        '''Invocar el metodo que inicia el UserControl en Forma de Modal
        ucEnviarRtrdCoordinador3.InicializarControlModal(FlagIdProyecto.Value, FlagIdRTRD.Value, FlagIdVisitaRTRD.Value)


    End Sub
End Class
