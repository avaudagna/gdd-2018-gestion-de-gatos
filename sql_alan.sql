
USE GD2C2018;
go

/*sp_addmessage @msgnum = 50001,  
              @severity = 10,  
              @msgtext = 'Usuario no encontrado.'; */
GO
IF (OBJECT_ID('sp_autenticar_usuario', 'P') IS NOT NULL) DROP PROCEDURE sp_autenticar_usuario 
GO
CREATE PROCEDURE sp_autenticar_usuario
(
    @user VARCHAR(20),
	@password VARCHAR(32),
	@salida INT OUTPUT)
AS
BEGIN
    SET @salida = (SELECT ISNULL((SELECT COUNT(1)
		FROM [GD2C2018].[GESTION_DE_GATOS].Usuarios
		WHERE [Usuario_Username] = @user AND [Usuario_Password] = HASHBYTES('SHA2_256', @password)
		GROUP BY [Usuario_Estado]), 0));
	IF @salida = 0
	BEGIN
		RAISERROR (50001, 10, 1)
		RETURN;
	END
END
go

IF (OBJECT_ID('sp_buscar_usuario', 'P') IS NOT NULL) DROP PROCEDURE sp_buscar_usuario 
go
CREATE PROCEDURE sp_buscar_usuario @username varchar(20)
AS BEGIN
		SELECT u.Usuario_Id, u.Usuario_Username, u.Usuario_Estado
		FROM GESTION_DE_GATOS.Usuarios u
				JOIN  GESTION_DE_GATOS.Rol_Por_Usuario
					ON GESTION_DE_GATOS.Rol_Por_Usuario.Usuario_Id = u.Usuario_Id
                JOIN GESTION_DE_GATOS.Roles
					ON GESTION_DE_GATOS.Roles.Rol_Id = GESTION_DE_GATOS.Rol_Por_Usuario.Rol_Id
		WHERE u.Usuario_Username = @username
END
go

IF (OBJECT_ID('sp_roles_usuario', 'P') IS NOT NULL) DROP PROCEDURE sp_roles_usuario
go
CREATE PROCEDURE sp_roles_usuario @user_id varchar(20)
AS BEGIN
		SELECT GESTION_DE_GATOS.Roles.Rol_Id, GESTION_DE_GATOS.Roles.Rol_Nombre
			FROM GESTION_DE_GATOS.Rol_Por_Usuario 
			JOIN GESTION_DE_GATOS.Roles 
				ON GESTION_DE_GATOS.Roles.Rol_Id = GESTION_DE_GATOS.Rol_Por_Usuario.Rol_Id
			WHERE Rol_Estado = 1 AND GESTION_DE_GATOS.Rol_Por_Usuario.Usuario_Id = @user_id
			GROUP BY GESTION_DE_GATOS.Roles.Rol_Id, GESTION_DE_GATOS.Roles.Rol_Nombre;
END
go

IF (OBJECT_ID('sp_funcionalidades_por_rol', 'P') IS NOT NULL) DROP PROCEDURE sp_funcionalidades_por_rol
go
CREATE PROCEDURE sp_funcionalidades_por_rol @rol_id INT
AS BEGIN
	SELECT [GESTION_DE_GATOS].Funcionalidades.Func_Id, [GESTION_DE_GATOS].Funcionalidades.Func_Descr
	  FROM [GESTION_DE_GATOS].[Funcionalidad_Por_Rol]
	  JOIN [GESTION_DE_GATOS].Funcionalidades 
		ON [GESTION_DE_GATOS].Funcionalidad_Por_Rol.Func_Id = [GESTION_DE_GATOS].Funcionalidades.Func_Id
	  WHERE Rol_id = @rol_id;
END 
go



IF (OBJECT_ID('sp_crear_empresa', 'P') IS NOT NULL) DROP PROCEDURE sp_crear_empresa
go
CREATE procedure dbo.sp_crear_empresa (@razon_social nvarchar(255), @cuit nvarchar(255),
								@mail nvarchar(50),@telefono numeric(20),@calle nvarchar(50),@nro numeric(18),
								@depto nvarchar(50),@localidad varchar(20),@piso numeric(18),@cp nvarchar(50))
as
begin
	declare @nuevoUsuario int
	declare @nuevoDomicilio int
	begin transaction
		begin try
			begin
				INSERT INTO GESTION_DE_GATOS.Usuarios 
				(Usuario_Username,Usuario_Password,Usuario_Estado) 
				VALUES (@cuit,
					HASHBYTES('SHA2_256','palconet2018'),
					1)
				SET @nuevoUsuario = (SELECT TOP 1 Usuario_Id FROM GESTION_DE_GATOS.Usuarios ORDER BY Usuario_Id ASC)
				INSERT INTO GESTION_DE_GATOS.Domicilios 
					(Dom_Cod_Postal,Dom_Piso,Dom_Depto,Dom_Nro_Calle,Dom_Calle) VALUES (@cp,@piso,@depto,@nro,@calle)
				SET @nuevoDomicilio = (SELECT TOP 1 Dom_Id FROM GESTION_DE_GATOS.Domicilios ORDER BY Dom_Id ASC)
				INSERT INTO GESTION_DE_GATOS.Empresas 
				(Emp_Razon_Social, Emp_Cuit, Emp_Fecha_Creacion, Emp_Mail, Emp_Dom_Id,
					Emp_Usuario_Id, Emp_Tel)
				VALUES (@razon_social, @cuit, GETDATE(), @mail, @nuevoDomicilio, @nuevoUsuario, @telefono)
			end
		end try
		begin catch
			IF @@TRANCOUNT > 0  
				rollback transaction;
			declare @mensajeError nvarchar(4000)
			SELECT @mensajeError = ERROR_MESSAGE() 
			RAISERROR('Hubo un error al crear la empresa, motivo: %s',11,1,@mensajeError)
		end catch
		IF @@TRANCOUNT > 0  
        	commit transaction;
end
GO
