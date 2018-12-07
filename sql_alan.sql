
USE GD2C2018;
go

sp_addmessage @msgnum = 50001,  
              @severity = 10,  
              @msgtext = 'Usuario no encontrado.';  
GO
IF (OBJECT_ID('sp_autenticar_usuario', 'P') IS NOT NULL) DROP PROCEDURE sp_autenticar_usuario GO
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

IF (OBJECT_ID('sp_buscar_usuario', 'P') IS NOT NULL) DROP PROCEDURE sp_buscar_usuario GO
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

IF (OBJECT_ID('sp_roles_usuario', 'P') IS NOT NULL) DROP PROCEDURE sp_roles_usuario GO
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

IF (OBJECT_ID('sp_funcionalidades_por_rol', 'P') IS NOT NULL) DROP PROCEDURE sp_funcionalidades_por_rol GO
CREATE PROCEDURE sp_funcionalidades_por_rol @rol_id INT
AS BEGIN
	SELECT [GESTION_DE_GATOS].Funcionalidades.Func_Id, [GESTION_DE_GATOS].Funcionalidades.Func_Descr
	  FROM [GESTION_DE_GATOS].[Funcionalidad_Por_Rol]
	  JOIN [GESTION_DE_GATOS].Funcionalidades 
		ON [GESTION_DE_GATOS].Funcionalidad_Por_Rol.Func_Id = [GESTION_DE_GATOS].Funcionalidades.Func_Id
	  WHERE Rol_id = @rol_id;
END 
go