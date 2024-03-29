USE [challengen5]
GO
/****** Object:  UserDefinedTableType [dbo].[TYPE_IC_PRODUCT]    Script Date: 11/06/2022 06:23:15 ******/
CREATE TYPE [dbo].[TYPE_IC_PRODUCT] AS TABLE(
	[ProductId] [int] NULL
)
GO
/****** Object:  StoredProcedure [dbo].[SP_IC_CREATE_UPDATE_ORDER]    Script Date: 11/06/2022 06:23:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_IC_CREATE_UPDATE_ORDER](
	@Id				INT,
	@UserId			INT,
	@AddProducts	TYPE_IC_PRODUCT READONLY,
	@DeleteProducts	TYPE_IC_PRODUCT READONLY
)
AS
BEGIN
	DECLARE @message NVARCHAR(max), @status INT;

	BEGIN TRY  
		BEGIN TRANSACTION
		
		IF (SELECT COUNT(*) FROM @AddProducts) = 0 AND (SELECT COUNT(*) FROM @DeleteProducts) = 0 
		BEGIN
			RAISERROR('No hay productos para agregar ni eliminar', 16, 1)
			RETURN;
		END

		PRINT('REGISTRAR')

		IF @Id = 0
		BEGIN
			INSERT INTO ICOrders(UserId, AddedOn) VALUES (@UserId, GETDATE())
			
			SET @Id = SCOPE_IDENTITY();

			--INSERTAMOS LOS ID´s DE LOS PRODUCTOS
			INSERT INTO ICOrderProducts(OrderId, ProductId, AddedOn)
			SELECT @Id, ProductId, GETDATE() FROM @AddProducts
		END
		ELSE
		BEGIN
			IF NOT EXISTS(SELECT 1 FROM ICOrders WHERE Id = @Id)
			BEGIN
				RAISERROR('Orden de Compra no existe',16,1);
				RETURN;
			END

			--AQUI UNICAMENTE ACTUALIZAMOS LOS PRODUCTOS DE LA ORDEN
			UPDATE ICOrders SET ModifiedOn = GETDATE() WHERE Id = @Id

			--ELIMINAMOS LOS PRODUCTOS DE LA ORDEN QUE NO ESTÁN EN LA LISTA
			DELETE FROM ICOrderProducts 
			WHERE OrderId = @Id 
				AND ProductId IN (SELECT ProductId FROM @DeleteProducts)

			--INSERTAMOS LOS PRODUCTOS NUEVOS
			INSERT INTO ICOrderProducts(OrderId, ProductId, AddedOn)
			SELECT @Id, ProductId, GETDATE() FROM @AddProducts
		END

		COMMIT TRANSACTION;

		SELECT * FROM ICOrders WHERE Id = @Id
	END TRY  
	BEGIN CATCH  
		 ROLLBACK;

		 SET @status = ERROR_STATE();
		 SET @message = ERROR_MESSAGE();

		 RAISERROR (@message, @status, 1);  
	END CATCH 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_IC_CREATE_UPDATE_USER]    Script Date: 11/06/2022 06:23:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_IC_CREATE_UPDATE_USER](
	@Id				INT,
	@Username		NVARCHAR(50),
	@PasswordHash	VARBINARY(MAX),
	@PasswordSalt	VARBINARY(MAX)
)
AS
BEGIN
	DECLARE @message NVARCHAR(max), @status INT;

	BEGIN TRY  
		BEGIN TRANSACTION
			IF EXISTS(SELECT 1 FROM ICUsers WHERE Username = @Username AND Id <> @Id)
			BEGIN
				RAISERROR ('Ya existe este usuario',16,1); 
			END
			ELSE
			BEGIN
				IF @Id = 0
				BEGIN
					INSERT INTO ICUsers(Username, PasswordHash, PasswordSalt, AddedOn)
					VALUES (@Username, @PasswordHash, @PasswordSalt, GETDATE())

					SET @Id = SCOPE_IDENTITY();
				END
				ELSE
				BEGIN
					IF (SELECT COUNT(*) FROM ICUsers WHERE Id = @Id) = 0
					BEGIN
						RAISERROR ('Usuario no encontrado',16,1); 
					END
					ELSE
					BEGIN
						UPDATE ICUsers
						SET 
							Username = @Username,
							ModifiedOn = GETDATE()
						WHERE Id = @Id
					END
				END
			END
			
		COMMIT TRANSACTION;

		SELECT * FROM ICUsers WHERE Id = @Id
	END TRY  
	BEGIN CATCH  
		 ROLLBACK;

		 SET @status = ERROR_STATE();
		 SET @message = ERROR_MESSAGE();

		 RAISERROR (@message, @status, 1);  
	END CATCH 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_IC_LIST_ORDERS_BY_USERID]    Script Date: 11/06/2022 06:23:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_IC_LIST_ORDERS_BY_USERID](
	@UserId INT
)
AS
BEGIN
	SELECT * FROM ICOrders WHERE UserId = @UserId
END
GO
