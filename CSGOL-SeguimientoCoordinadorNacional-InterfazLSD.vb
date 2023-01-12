Public Class InterfazLSD
    Private _codigoError As Integer = 0
    Dim lws As New sGolGenerico.GolsGenericoClient
    Dim wsSsAE As New sSSAE.wsUsuarioSSAESoapClient
    Dim IntJF As New InterfazJF
    Dim IntGeneral As New InterfazGeneral
    Dim cadenaConexion As String = ConfigurationManager.AppSettings.Get("ConexionRendicionCuenta")
    Dim dt As DataTable

    Public Function MostrarAutoridades() As DataTable
        Try
            dt = lws.ListarGenericoSinArg("PE_L_CSGOL_MostrarTotalAutoridad", cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("MostrarAutoridades", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - MostrarAutoridades", _codigoError)
        End Try

        Return dt

    End Function

    ''' <summary>
    ''' Mostrar autoridades
    ''' </summary>
    ''' <returns></returns>
    Public Function ListarEntidades(ByVal IdProvincia As String) As DataTable
        Try
            dt = lws.ListarGenericoArgCadena("PE_L_CSGOL_ListarEntidades", IdProvincia, cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("ListarEntidades", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - ListarEntidades", _codigoError)
        End Try

        Return dt

    End Function

    Public Function BuscarAutoCedula(ByVal CedulaProvincia As String, ByVal CedulaTomo As String, ByVal CedulaAsiento As String, ByVal Correo As String)
        Try
            Dim ArrParametros(3) As String
            ArrParametros(0) = CedulaProvincia
            ArrParametros(1) = CedulaTomo
            ArrParametros(2) = CedulaAsiento
            ArrParametros(3) = Correo

            dt = lws.ListarGenericoArgArregloCadena("PE_L_CSGOL_BuscarAutoridadCedula", ArrParametros, cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("PE_L_CSGOL_BuscarAutoridadCedula", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - BuscarAutoCedula", _codigoError)
        End Try

        Return dt
    End Function

    Public Function ListarDependencias(ByVal CodEntidad As String) As DataTable
        Try
            dt = lws.ListarGenericoArgCadena("PE_L_CSGOL_ListarDependencias", CodEntidad, cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("ListarDependencias", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - ListarDependencias", _codigoError)
        End Try

        Return dt

    End Function

    ''' <param name="CodEntidad"></param>
    ''' <param name="CodDependencia"></param>
    ''' <param name="IdProvincia"></param>
    ''' <param name="IdDistrito"></param>
    ''' <param name="IdCorregimiento"></param>
    ''' <param name="BuzonCorreo"></param>
    ''' <param name="Telefono"></param>
    ''' <param name="Celular"></param>
    ''' <param name="Estado"></param>
    ''' <param name="IdUserRegistra"></param>
    ''' <returns></returns>
    Public Function InsertarAutoridad(ByVal Nombre As String, ByVal Apellido As String, ByVal IdCodProv As String, ByVal IdSigla As String, ByVal codProv As String, ByVal CedulaTomo As String,
                                      ByVal CedulaAsiento As String, ByVal CodEntidad As String, ByVal CodDependencia As String,
                                      ByVal CodArea As String,
                                      ByVal IdProvincia As String, ByVal IdDistrito As String, ByVal IdCorregimiento As String,
                                      ByVal BuzonCorreo As String, ByVal Telefono As String, ByVal Celular As String, ByVal Estado As String,
                                      ByVal IdUserRegistra As String)
        Try
            Dim ArrParametros(17) As String
            ArrParametros(0) = Nombre
            ArrParametros(1) = Apellido
            ArrParametros(2) = IdCodProv
            ArrParametros(3) = IdSigla
            ArrParametros(4) = codProv
            ArrParametros(5) = CedulaTomo
            ArrParametros(6) = CedulaAsiento
            ArrParametros(7) = CodArea
            ArrParametros(8) = CodEntidad
            ArrParametros(9) = CodDependencia
            ArrParametros(10) = IdProvincia
            ArrParametros(11) = IdDistrito
            ArrParametros(12) = IdCorregimiento
            ArrParametros(13) = BuzonCorreo
            ArrParametros(14) = Telefono
            ArrParametros(15) = Celular
            ArrParametros(16) = IdUserRegistra
            ArrParametros(17) = Estado


            'dt = lws.ListarGenericoArgArregloCadena("PE_I_CSGOL_GuardarAutoridad", ArrParametros, cadenaConexion)
            dt = lws.ListarGenericoArgArregloCadena("PE_I_CSGOL_Registrar_Autoridad", ArrParametros, cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("PE_I_CSGOL_GuardarAutoridad", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - InsertarAutoridad", _codigoError)
        End Try

        Return dt
    End Function

    Public Function UpdateDatosAuto(ByVal IdAutoridad As String, ByVal Nombre As String, ByVal Apellido As String,
                                    ByVal IdCodProv As String, ByVal IdSigla As String, ByVal CedulaProvincia As String, ByVal CedulaTomo As String, ByVal CedulaAsiento As String,
                                     ByVal CodArea As String, ByVal CodEntidad As String, ByVal CodDependencia As String,
                                     ByVal IdProvincia As String, ByVal IdDistrito As String, ByVal IdCorregimiento As String,
                                     ByVal BuzonCorreo As String, ByVal Telefono As String, ByVal Celular As String, ByVal Estado As String,
                                     ByVal UsuarioModifica As String)
        Try
            Dim ArrParametros(18) As String
            ArrParametros(0) = IdAutoridad
            ArrParametros(1) = Nombre
            ArrParametros(2) = Apellido
            ArrParametros(3) = IdCodProv
            ArrParametros(4) = IdSigla
            ArrParametros(5) = CedulaProvincia
            ArrParametros(6) = CedulaTomo
            ArrParametros(7) = CedulaAsiento
            ArrParametros(8) = CodArea
            ArrParametros(9) = CodEntidad
            ArrParametros(10) = CodDependencia
            ArrParametros(11) = IdProvincia
            ArrParametros(12) = IdDistrito
            ArrParametros(13) = IdCorregimiento
            ArrParametros(14) = BuzonCorreo
            ArrParametros(15) = Telefono
            ArrParametros(16) = Celular
            ArrParametros(17) = Estado
            ArrParametros(18) = UsuarioModifica


            dt = lws.ListarGenericoArgArregloCadena("PE_U_CSGOL_UPDATE_AUTORIDAD", ArrParametros, cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("PE_U_CSGOL_UPDATE_AUTORIDAD", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - UpdateDatosAuto", _codigoError)
        End Try

        Return dt
    End Function

    Public Function ListarProvincias() As DataTable
        Try
            dt = lws.ListarGenericoSinArg("PE_L_TF_PROVINCIA_REC", cadenaConexion)

        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("PE_L_TF_PROVINCIA_REC", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - ListarProvincias", _codigoError)
        End Try

        Return dt
    End Function

    Public Function BuscarAutoridadID(ByVal IdAutoridad As String)

        Try
            dt = lws.ListarGenericoArgCadena("PE_L_CSGOL_BuscarAutoridadBY_ID", IdAutoridad, cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("PE_L_CSGOL_BuscarAutoridadBY_ID", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - BuscarAutoridadID", _codigoError)
        End Try

        Return dt
    End Function

    Public Function ListarProvinciasRol(ByVal Identificador As String) As DataTable
        Try
            dt = lws.ListarGenericoArgCadena("PE_L_CSGOL_ListarProvinciasXRol", Identificador, cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("ListarProvinciasRol", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - ListarProvinciasRol", _codigoError)
        End Try

        Return dt

    End Function

    Public Function ListarProvinciasID(ByVal Identificador As String) As DataTable
        Try
            dt = lws.ListarGenericoArgCadena("PE_L_CSGOL_ListarProvinciasXIdentificador", Identificador, cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("ListarProvinciasID", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - ListarProvinciasID", _codigoError)
        End Try

        Return dt

    End Function

    'Public Function ListarUsuariosProvincias(ByVal Identificador As String) As DataTable
    '    Try
    '        dt = lws.ListarGenericoArgCadena("PE_L_CSGOL_ListarUsuariosXProvincia", Identificador, cadenaConexion)
    '    Catch ex As Exception
    '        ' Regresa tabla vacia
    '        dt = New DataTable()
    '        _codigoError = 0

    '        '' Obtiene datos del Error y registra
    '        Dim mensaje As String = ex.Message.ToString()
    '        Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
    '        _codigoError = IntGeneral.RegistrarError("ListarUsuariosProvincias", mensaje, detalle)

    '        Funciones.ModalErrorCodeBehind("Error al realizar la consulta - ListarUsuariosProvincias", _codigoError)
    '    End Try

    '    Return dt

    'End Function

    Public Function ListarUsuariosProvincias(ByVal Identificador As String) As DataTable
        Try
            'dt = lws.ListarGenericoArgCadena("PE_L_CSGOL_ListarUsuariosXProvincia_V4", Identificador, cadenaConexion)
            dt = lws.ListarGenericoArgCadena("PE_L_CSGOL_ListarUsuariosXProvincia_V2_EXTRA", Identificador, cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("PE_L_CSGOL_ListarUsuariosXProvincia_V2_EXTRA", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - ListarUsuariosProvincias", _codigoError)
        End Try

        Return dt

    End Function


    Public Function IdentificadorVSrol(ByVal Identificador As String) As DataTable
        Try
            dt = lws.ListarGenericoArgCadena("PE_L_CSGOL_IdentificadorXRol", Identificador, cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("IdentificadorVSrol", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - IdentificadorVSrol", _codigoError)
        End Try

        Return dt

    End Function

    Public Function ConteoExpedientes() As DataTable
        Try
            dt = lws.ListarGenericoSinArg("PE_L_CSGOL_ConteoExpediente", cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("ConteoExpedientes", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - ConteoExpedientes", _codigoError)
        End Try

        Return dt

    End Function

    Public Function NombreProvincia(ByVal Secuencial As Integer) As DataTable
        Try
            dt = lws.ListarGenericoArgEntero("PE_L_CSGOL_ProvinciaExpediente", Secuencial, cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("NombreProvincia", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - NombreProvincia", _codigoError)
        End Try

        Return dt

    End Function

    'Public Function ConsultaEstadoRTRD(ByVal IdProyecto As String) As DataTable
    '    Try
    '        dt = lws.ListarGenericoArgCadena("PE_C_CSGOL_EstadoRTRD", IdProyecto, cadenaConexion)
    '    Catch ex As Exception
    '        ' Regresa tabla vacia
    '        dt = New DataTable()
    '        _codigoError = 0

    '        '' Obtiene datos del Error y registra
    '        Dim mensaje As String = ex.Message.ToString()
    '        Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
    '        _codigoError = IntGeneral.RegistrarError("ConsultaEstadoRTRD", mensaje, detalle)

    '        Funciones.ModalErrorCodeBehind("Error al realizar la consulta - ConsultaEstadoRTRD", _codigoError)
    '    End Try

    '    Return dt

    'End Function

    Public Function MuestraFechaRtRd(ByVal IdVisita As String) As DataTable
        Try

            dt = lws.ListarGenericoArgCadena("PE_L_CSGOL_MostrarFechaRtRd", IdVisita, cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("MuestraFechaRtRd", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - MuestraFechaRtRd", _codigoError)
        End Try

        Return dt

    End Function

    Public Function IdentificadorVSNombre(ByVal Identificador As String) As DataTable
        Try
            dt = lws.ListarGenericoArgCadena("PE_L_CSGOL_IdentificadorVSNombre", Identificador, cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("IdentificadorVSNombre", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - IdentificadorVSNombre", _codigoError)
        End Try

        Return dt

    End Function

    Public Function CargarTablaCN() As DataTable
        Try
            dt = lws.ListarGenericoSinArg("PE_L_CSGOL_ListarProyectosSeguimientoCN", cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("CargarTablaCN", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - CargarTablaCN", _codigoError)
        End Try

        Return dt

    End Function

    Public Function CargarTablaRTRDInvestigacion() As DataTable
        Try
            dt = lws.ListarGenericoSinArg("PE_L_CSGOL_ListarProyectosInvestigacion", cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("CargarTablaRTRDInvestigacion", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - CargarTablaRTRDInvestigacion", _codigoError)
        End Try

        Return dt

    End Function

    Public Function CargarDdlEntidadCN() As DataTable
        Try
            dt = lws.ListarGenericoSinArg("PE_L_CSGOL_ListarEntidadesAsignadasCN", cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("CargarDdlEntidadCN", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - CargarDdlEntidadCN", _codigoError)
        End Try

        Return dt

    End Function

    Public Function CargarDdlDependenciasCN(ByVal CodigoArea As String, ByVal CodigoEntidad As String) As DataTable

        Try
            Dim parametros(1) As String

            parametros(0) = CodigoArea
            parametros(1) = CodigoEntidad

            dt = lws.ListarGenericoArgArregloCadena("PE_L_CSGOL_ListarDependenciasAsignadasCN", parametros, cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("CargarDdlDependenciasCN", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - CargarDdlDependenciasCN", _codigoError)
        End Try

        Return dt

    End Function

    Public Function CargarDdlFinanciamientoCN() As DataTable
        Try
            dt = lws.ListarGenericoSinArg("PE_L_CSGOL_ListarFuentesFinanciamientoCN", cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("CargarDdlFinanciamientoCN", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - CargarDdlFinanciamientoCN", _codigoError)
        End Try

        Return dt

    End Function

    Public Function CargarDdlAñoCN() As DataTable
        Try
            dt = lws.ListarGenericoSinArg("[PE_L_CSGOL_ListarAñosProyectosAsignadosCN]", cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("CargarDdlAñoCN", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - CargarDdlAñoCN", _codigoError)
        End Try

        Return dt

    End Function

    Public Function CargarBusquedaFiltrada(ByVal Identificador As Integer, ByVal NumeroExpediente As Integer, ByVal CodigoArea As String, ByVal CodigoEntidad As String, ByVal CodigoDependencia As String,
                                               ByVal IdFuente As Integer, ByVal Año As Integer, ByVal Estado As Integer) As DataTable

        Try
            Dim parametros(7) As String
            parametros(0) = Identificador
            parametros(1) = NumeroExpediente
            parametros(2) = CodigoArea
            parametros(3) = CodigoEntidad
            parametros(4) = CodigoDependencia
            parametros(5) = IdFuente
            parametros(6) = Año
            parametros(7) = Estado

            dt = lws.ListarGenericoArgArregloCadena("PE_L_CSGOL_ListarProyectosSeguimientoCNFiltro", parametros, cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("CargarBusquedaFiltrada", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - CargarBusquedaFiltrada", _codigoError)
        End Try

        Return dt

    End Function

    Public Function CargarBusquedaFiltrada2(ByVal Identificador As Integer, ByVal NumeroExpediente As Integer, ByVal CodigoArea As String, ByVal CodigoEntidad As String, ByVal CodigoDependencia As String,
                                               ByVal IdFuente As Integer, ByVal Año As Integer, ByVal Estado As Integer) As DataTable

        Try
            Dim parametros(7) As String
            parametros(0) = Identificador
            parametros(1) = NumeroExpediente
            parametros(2) = CodigoArea
            parametros(3) = CodigoEntidad
            parametros(4) = CodigoDependencia
            parametros(5) = IdFuente
            parametros(6) = Año
            parametros(7) = Estado

            dt = lws.ListarGenericoArgArregloCadena("PE_L_CSGOL_ListarProyectosSeguimientoCNFiltro2", parametros, cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("CargarBusquedaFiltrada2", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - CargarBusquedaFiltrada2", _codigoError)
        End Try

        Return dt

    End Function

    Public Function ListarMotivos() As DataTable
        Try
            dt = lws.ListarGenericoSinArg("PE_L_CSGOL_ListarMotivosCierreEx", cadenaConexion)

        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("PE_L_CSGOL_ListarMotivosCierreEx", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - ListarMotivos", _codigoError)
        End Try

        Return dt
    End Function

    Public Function InsertarMotivoInvestigacionExp(ByVal IdProyecto As String, ByVal IdMotivo As String, ByVal SecuencialRtRd As String, ByVal MotivoAccion As String,
                                      ByVal UsuarioRegistra As String)
        Try
            Dim ArrParametros(4) As String
            ArrParametros(0) = IdProyecto
            ArrParametros(1) = IdMotivo
            ArrParametros(2) = SecuencialRtRd
            ArrParametros(3) = MotivoAccion
            ArrParametros(4) = UsuarioRegistra

            dt = lws.ListarGenericoArgArregloCadena("PE_I_CSGOL_GuardarMotivoExp", ArrParametros, cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("InsertarMotivoInvestigacionExp", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - InsertarMotivoInvestigacionExp", _codigoError)
        End Try

        Return dt
    End Function

    Public Function InsertarMotivoDevolverExp(ByVal IdProyecto As String, ByVal SecuencialRtRd As String, ByVal MotivoAccion As String, ByVal UsuarioRegistra As String)
        Try
            Dim ArrParametros(3) As String
            ArrParametros(0) = IdProyecto
            ArrParametros(1) = SecuencialRtRd
            ArrParametros(2) = MotivoAccion
            ArrParametros(3) = UsuarioRegistra

            dt = lws.ListarGenericoArgArregloCadena("PE_I_CSGOL_GuardarMotivoDevolverExp", ArrParametros, cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("InsertarMotivoDevolverExp", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - InsertarMotivoDevolverExp", _codigoError)
        End Try

        Return dt
    End Function

    Public Function UpdateEstadoExp(ByVal IdProyecto As String, ByVal EstadoProyecto As String, ByVal UsuarioModifica As String)
        Try
            Dim ArrParametros(2) As String
            ArrParametros(0) = IdProyecto
            ArrParametros(1) = EstadoProyecto
            ArrParametros(2) = UsuarioModifica

            dt = lws.ListarGenericoArgArregloCadena("PE_U_CSGOL_ESTADOPROYECTO", ArrParametros, cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("UpdateEstadoExp", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - UpdateEstadoExp", _codigoError)
        End Try

        Return dt
    End Function

    Public Function UpdateEstadoRTRD(ByVal IdProyecto As String, ByVal Secuencial As String, ByVal EstadoRtRd As String, ByVal UsuarioModifica As String)
        Try
            Dim ArrParametros(3) As String
            ArrParametros(0) = IdProyecto
            ArrParametros(1) = Secuencial
            ArrParametros(2) = EstadoRtRd
            ArrParametros(3) = UsuarioModifica

            dt = lws.ListarGenericoArgArregloCadena("PE_U_CSGOL_ESTADORTRD", ArrParametros, cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("UpdateEstadoRTRD", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - UpdateEstadoRTRD", _codigoError)
        End Try

        Return dt
    End Function

    Public Function ConsultarMotivoInvestigacion(ByVal IdProyecto As String, ByVal Secuencial As String)
        Try
            Dim ArrParametros(1) As String
            ArrParametros(0) = IdProyecto
            ArrParametros(1) = Secuencial


            dt = lws.ListarGenericoArgArregloCadena("PE_C_CSGOL_ConsultarMotivosInvestigacion", ArrParametros, cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("ConsultarMotivoInvestigacion", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - ConsultarMotivoInvestigacion", _codigoError)
        End Try

        Return dt
    End Function

    Public Function ConsultarMotivoDevolucion(ByVal IdProyecto As String, ByVal Secuencial As String)
        Try
            Dim ArrParametros(1) As String
            ArrParametros(0) = IdProyecto
            ArrParametros(1) = Secuencial


            dt = lws.ListarGenericoArgArregloCadena("PE_C_CSGOL_ConsultarMotivosDevolucion", ArrParametros, cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("ConsultarMotivoInvestigacion", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - ConsultarMotivoInvestigacion", _codigoError)
        End Try

        Return dt
    End Function

    Public Function UpdateEstadoDevolverRTRD(ByVal IdProyecto As String, ByVal Secuencial As String, ByVal UsuarioModifica As String)
        Try
            Dim ArrParametros(2) As String
            ArrParametros(0) = IdProyecto
            ArrParametros(1) = Secuencial
            ArrParametros(2) = UsuarioModifica

            dt = lws.ListarGenericoArgArregloCadena("PE_U_CSGOL_ESTADODEVOLUCIONRTRD", ArrParametros, cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("UpdateEstadoDevolverRTRD", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - UpdateEstadoDevolverRTRD", _codigoError)
        End Try

        Return dt
    End Function

    Public Function ListarEmailEnvioLider(ByVal IdProyecto As String) As DataTable

        Try

            dt = lws.ListarGenericoArgCadena("PE_L_CSGOL_ListarEmailDevolverLider", IdProyecto, cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("ListarEmailEnvioLider", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - ListarEmailEnvioLider", _codigoError)
        End Try

        Return dt
    End Function

    Public Function ListarEmailEnvioLider2(ByVal IdProyecto As String) As DataTable

        Try

            dt = lws.ListarGenericoArgCadena("PE_C_CSGOL_ConsultarLideres", IdProyecto, cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("ListarEmailEnvioLider", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - ListarEmailEnvioLider", _codigoError)
        End Try

        Return dt
    End Function

    Public Function LideresXProvincia(ByVal IdProyecto As String) As DataTable

        Try

            dt = lws.ListarGenericoArgCadena("PE_C_CSGOL_ConsultarLideres", IdProyecto, cadenaConexion)
        Catch ex As Exception
            ' Regresa tabla vacia
            dt = New DataTable()
            _codigoError = 0

            '' Obtiene datos del Error y registra
            Dim mensaje As String = ex.Message.ToString()
            Dim detalle As String = DirectCast(ex, System.Exception).StackTrace.ToString()
            _codigoError = IntGeneral.RegistrarError("LideresXProvincia", mensaje, detalle)

            Funciones.ModalErrorCodeBehind("Error al realizar la consulta - LideresXProvincia", _codigoError)
        End Try

        Return dt
    End Function

End Class